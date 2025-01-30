namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageMadeBy : PureComponent
{
    protected override Element render()
    {
        var data = new[]
        {
            new
            {
                Name    = "Special Villa Promotion and Rezervation",
                Url     = "https://www.alyavillas.com",
                UrlText = "alyavillas.com",
                Image   = "alyavillas.com.jpg"
            },
            new
            {
                Name    = "Postman alternative for .net core assemblies",
                Url     = "https://github.com/beyaz/ApiInspector",
                UrlText = "Api Inspector",
                Image   = "api.inspector.png"
            },
            new
            {
                Name    = "Tourism agency (hotel search)",
                Url     = "https://www.elcitur.com.tr/hotel",
                UrlText = "Tourism Agency",
                Image   = "hotel.app.com.jpg"
            }
        };

        return new BlogPageLayout
        {
            new h1
            {
                "Sites or applications that using ReactWithDotNet"
            },
            SpaceY(16),
            new p
            {
                "Here is a list of some applications that using ReactWithDotNet technology."
            },
            SpaceY(16),
            new FlexRow(Gap(32), FlexWrap, JustifyContentCenter)
            {
                data.Select(x => new FlexColumn
                {
                    style =
                    {
                        Size(250, 220),
                        AlignItemsCenter,
                        Gap(4),
                        Padding(16),
                        Background(White),
                        BorderRadius(8),
                        Border(1, solid, Gray200),
                        Hover(BorderColor(Gray300)),
                        TextAlignCenter
                    },

                    children =
                    {
                        new h5 { x.Name },
                        new img(WidthFull, Height(100), BorderRadius(5))
                        {
                            Src(Asset(x.Image)),
                            ObjectFitCover
                        },
                        new a
                        {
                            href = x.Url,
                            text = x.UrlText
                        }
                    }
                })
            }
        };
    }
}