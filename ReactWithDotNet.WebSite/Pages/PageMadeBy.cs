namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageMadeBy : PureComponent
{
    protected override Element render()
    {
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
            new FlexRow(Gap(32), FlexWrap)
            {
                new Card
                {
                    new h5 { "Special Villa Promotion and Rezervation" },
                    new img(SizeFull, BorderRadius(5))
                    {
                        Src(Asset("alyavillas.com.jpg"))
                    },
                    new a
                    {
                        href = "alyavillas.com",
                        text = "alyavillas.com"
                    }
                },
                
                new Card
                {
                    new h5 { "Postman alternative for .net core dlls" },
                    new img(Size(100), BorderRadius(5),ObjectFitCover)
                    {
                        Src(Asset("api.inspector.png"))
                    },
                    new a
                    {
                        href = "https://github.com/beyaz/ApiInspector",
                        text = "ApiInspector"
                    }
                },
                
                new Card
                {
                    new h5 { "Tourism agency (hotel search)" },
                    new img(SizeFull, BorderRadius(5))
                    {
                        Src(Asset("hotel.app.com.jpg"))
                    },
                    new a
                    {
                        href = "https://www.elcitur.com.tr/hotel",
                        text = "https://www.elcitur.com.tr/hotel"
                    }
                },
            }
            
        };
    }

    class Card : PureComponent
    {
        protected override Element render()
        {
            return new FlexColumn(Size(250), AlignItemsCenter, Gap(4), Padding(16), Background(White), BorderRadius(8), Border(1, solid, Gray200), Hover(BorderColor(Gray300)))
            {
                children,
                
                TextAlignCenter
            };
        }
    }

}