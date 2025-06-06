﻿namespace HopGogoEndUserWebUI;

sealed class MainLayout : PureComponent, IPageLayout
{
    public string ContainerDomElementId => "app";

    public string InitialScript =>
        $$"""
          import {ReactWithDotNet} from '{{IndexJsFilePath}}';

          ReactWithDotNet.StrictMode = false;

          ReactWithDotNet.RequestHandlerPath = '{{RequestHandlerPath}}';

          ReactWithDotNet.RenderComponentIn({
            idOfContainerHtmlElement: '{{ContainerDomElementId}}',
            renderInfo: {{RenderInfo.ToJsonString()}}
          });
          """;

    public ComponentRenderInfo RenderInfo { get; set; }

    protected override Element render()
    {
        return new html
        {
            Lang("tr"),
            DirLtr,

            // Global Styles
            Margin(0),
            FontFamily("Outfit"),
            FontWeight400,
            FontSize(1 * rem),
            LineHeight(1.5 * CssUnit.em),
            Background(White),

            new head
            {
                new meta { charset = "utf-8" },
                new meta { name    = "viewport", content = "width=device-width, initial-scale=1" },
                new title { "HopGogo" },

                new link
                {
                    rel  = "stylesheet",
                    type = "text/css",
                    href = IndexCssFilePath
                },

                new style
                {
                    """

                    * {
                        margin: 0;
                        padding: 0;
                        box-sizing: border-box;
                    }

                    """
                },

                arrangeFonts
            },
            new body(Margin(0), Height100vh)
            {
                new div(Id(ContainerDomElementId), SizeFull)
            }
        };

        IEnumerable<Element> arrangeFonts()
        {
            return
            [
                new link { href = "https://fonts.gstatic.com", rel = "preconnect" },

                new link { href = "https://fonts.googleapis.com", rel = "preconnect" },

                new link
                {
                    href = "https://fonts.cdnfonts.com/css/euclid-circular-b", rel = "stylesheet"
                },
                new link
                {
                    href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&family=Outfit:wght@100..900&family=Plus+Jakarta+Sans:ital,wght@0,200..800;1,200..800&family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet"
                }
            ];
        }
    }
}