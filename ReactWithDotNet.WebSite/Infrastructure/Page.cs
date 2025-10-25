global using static ReactWithDotNet.WebSite.Routes;
using ReactWithDotNet.WebSite.HelperApps;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

sealed record PageRouteInfo(string Url, Type page);

static class Routes
{
    // H o m e
    public  const string Home = "/";
    
    // H o m e   b u t t o n s
    public const string Milestones = "/" +nameof(Milestones);
    public  const string Showcase = "/" + nameof(Showcase);
    
    
    // T e c h n i c a l
    public const string  TechnicalDetail =  "/" +nameof(TechnicalDetail);
    public const string  Modifiers = "/" +nameof(Modifiers);
    public const string  ReactContexts = "/" +nameof(ReactContexts);
    
    // D o c
    public const string  DocStart = "/doc/start";
    public const string  DocSetup = "/doc/setup";
    
    // H e l p e r   A p p s
    public const string  LiveEditor = "/LiveEditor";
    public const string  LivePreview = "/" +nameof(LivePreview);
    public const string  Designer = "/" +nameof(Designer);
    public const string  MadeBy = "/" +nameof(MadeBy);
    
    // i n t e r n a l
    public const string  DemoPreview = "/" + nameof(DemoPreview);
}

static class Page
{
   
   

    public static string DemoPreviewUrl(string fullTypeName)
    {
        return Routes.DemoPreview + $"?{Pages.DemoPreview.QueryParameterNameOfFullTypeName}={fullTypeName}";
    }

    public static string LivePreviewUrl(string guid)
    {
        return Routes.LivePreview + $"?{Components.LivePreview.QueryParameterNameOfGuid}={guid}";
    }
}