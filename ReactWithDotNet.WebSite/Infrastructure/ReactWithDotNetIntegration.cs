using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;

namespace ReactWithDotNet.WebSite;

public static class ReactWithDotNetIntegration
{
    public static void ConfigureReactWithDotNet(this WebApplication app)
    {
        app.UseMiddleware<ReactWithDotNetJavaScriptFiles>();

        

        RequestHandlerPath = "/" + nameof(HandleReactWithDotNetRequest);

        var routes = RouteHelper.GetRoutesFromAssembly(typeof(ReactWithDotNetIntegration).Assembly);

        app.Use(async (httpContext, next) =>
        {
            var path = httpContext.Request.Path.Value ?? string.Empty;

            if (path == RequestHandlerPath)
            {
                await HandleReactWithDotNetRequest(httpContext);
                return;
            }

            if (routes.TryGetValue(path, out var route))
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), route.Page);
                return;
            }

            if (path == "/UploadFile")
            {
                await UploadFileAndWriteResponse(httpContext);
                return;
            }

            #if DEBUG
            if (path == ReactWithDotNetDesigner.UrlPath)
            {
                await WriteHtmlResponse(httpContext, typeof(MainLayout), typeof(ReactWithDotNetDesigner));
                return;
            }
            #endif

            await next();
        });
    }

    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        return ProcessReactWithDotNetComponentRequest(new()
        {
            HttpContext           = httpContext,
            OnReactContextCreated = OnReactContextCreated
        });
    }

    static Task OnReactContextCreated(ReactContext context)
    {
        return Task.CompletedTask;
    }

    static async Task<IResult> UploadFile(HttpContext httpContext)
    {
        var request = httpContext.Request;
        if (!request.HasFormContentType)
        {
            return Results.BadRequest("The request doesn't contain form content type");
        }

        var form = await request.ReadFormAsync();
        var file = form.Files["file"];
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("The file is empty or not provided");
        }

        var filePath = Path.Combine(@"C:\Users\beyaz\Downloads\", file.FileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Results.Ok(new { FilePath = filePath });
    }

    static async Task UploadFileAndWriteResponse(HttpContext httpContext)
    {
        var uploadResult = await UploadFile(httpContext);

        await uploadResult.ExecuteAsync(httpContext);
    }

    static Task WriteHtmlResponse(HttpContext httpContext, Type layoutType, Type mainContentType)
    {
        httpContext.Response.ContentType = "text/html; charset=UTF-8";

        httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
        httpContext.Response.Headers[HeaderNames.Expires]      = "0";
        httpContext.Response.Headers[HeaderNames.Pragma]       = "no-cache";

        return ProcessReactWithDotNetPageRequest(new()
        {
            LayoutType            = layoutType,
            MainContentType       = mainContentType,
            HttpContext           = httpContext,
            OnReactContextCreated = OnReactContextCreated
        });
    }
}