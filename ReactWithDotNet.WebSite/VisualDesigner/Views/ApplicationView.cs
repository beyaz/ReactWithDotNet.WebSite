using System.Web;
using Microsoft.Net.Http.Headers;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationView: Component<ApplicationView.State>
{
    protected override Task constructor()
    {
        
        
        return Task.CompletedTask;
    }

    public static string UrlPath => "/$";
    internal static string UrlPathOfComponentPreview => $"{UrlPath}?preview=true";
    
    
    
    StyleModifier ScaleStyle => TransformOrigin("0 0") + Transform($"scale({state.Scale / (double)100})");
    
    protected override Element render()
    {
        return new FlexRow(Padding(10), SizeFull, Background(Theme.BackgroundColor))
        {
            EditorFontLinks,
            EditorFont(),
            new FlexColumn
            {
                applicationTopPanel,

                MainContent,
                
                new Style
                {
                    Border(Solid(1, Theme.BorderColor)),
                    SizeFull,
                    Background(Theme.WindowBackgroundColor),
                    BorderRadius(10),
                    BoxShadow(0, 30, 30, 0, rgba(69, 42, 124, 0.15))
                },
                NotificationHost
            }
        };
    }
    
    Element applicationTopPanel()
    {
        return new FlexRow
        {
            new FlexRow(AlignItemsCenter, Gap(5))
            {
                new h3 { "React Visual Designer" }
            },

            new FlexRow(Gap(20))
            {
                //GetEnvironment,
                //new LogoutButton()
            },

            new Style
            {
                JustifyContentSpaceBetween,
                AlignItemsCenter,
                BorderBottom(Solid(1, Theme.BorderColor)),
                Padding(5, 30)
            }
        };
    }

    Element MainContent()
    {
        return new SplitRow
        {
            sizes = [20, 60, 20],
            children =
            {
                PartLeftPanel,
                new FlexColumn(AlignItemsCenter, FlexGrow(1), Padding(7), MarginLeft(40), ScaleStyle)
                {
                    createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(5),
                    PartPreview
                },
                
                PartRightPanel
            }
        };
    }
    
    Element createHorizontalRuler()
    {
        const int step = 50;
        var max = state.ScreenWidth / step + 1;

        return new FlexRow(PositionRelative, WidthFull, Height(20))
        {
            Enumerable.Range(0, max).Select(number => new div(PositionAbsolute)
            {
                Bottom(3), Left(number * step),
                new FlexColumn(FontSize8, LineHeight6, FontWeight500, Gap(4))
                {
                    new div(MarginLeft(calculateMarginForCenterizeLabel(number)))
                    {
                        (number * step).ToString()
                    },
                    new div(BorderRadius(3))
                    {
                        Width(0.5),
                        Height(7),

                        Background("green")
                    }
                }
            }),
            createTenPoints()
        };

        IReadOnlyList<Element> createTenPoints()
        {
            var returnList = new List<Element>();

            var miniStep = 10;

            var cursor = 0;
            var distance = miniStep;
            while (distance <= state.ScreenWidth)
            {
                cursor++;

                distance = cursor * miniStep;

                if (distance % step == 0 || distance > state.ScreenWidth)
                {
                    continue;
                }

                returnList.Add(new div(PositionAbsolute)
                {
                    Bottom(3),
                    Left(distance),

                    Width(0.5),
                    Height(4),
                    Background("green")
                });
            }

            return returnList;
        }

        double calculateMarginForCenterizeLabel(int stepNumber)
        {
            var label = stepNumber * step;

            if (label < 10)
            {
                return -2;
            }

            if (label < 100)
            {
                return -4.5;
            }

            if (label < 1000)
            {
                return -7;
            }

            return -9;
        }
    }

    Element PartPreview()
    {
        return new FlexRow(JustifyContentFlexStart, PositionRelative)
        {
            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
            BackgroundSize("10px 10px"),

            createVerticleRuler,
            createElement(),

            Width(state.ScreenWidth),
            Height(state.ScreenHeight * percent),
            BoxShadow(0, 4, 12, 0, rgba(0, 0, 0, 0.1))
        };
        
        static Element createVerticleRuler()
        {
            const int maxHeight = 5000;

            const int step = 50;
            const int max = maxHeight / step + 1;

            return new div(SizeFull, Width(30), MarginLeft(-30), OverflowHidden, PositionRelative)
            {
                Enumerable.Range(0, max).Select(number => new div(PositionAbsolute)
                {
                    Right(3), Top(number * step),
                    new FlexRow(FontSize8, LineHeight6, FontWeight500, Gap(4))
                    {
                        new div(MarginTop(number == 0 ? 0 : -3))
                        {
                            (number * step).ToString()
                        },
                        new div
                        {
                            Height(0.5),
                            Width(7),

                            Background("green")
                        }
                    }
                }),

                createTenPoints()
            };

            IReadOnlyList<Element> createTenPoints()
            {
                var returnList = new List<Element>();

                var miniStep = 10;

                var cursor = 0;
                var distance = miniStep;
                while (distance <= maxHeight)
                {
                    cursor++;

                    distance = cursor * miniStep;

                    if (distance % step == 0 || distance > maxHeight)
                    {
                        continue;
                    }

                    returnList.Add(new div(PositionAbsolute)
                    {
                        Right(3),
                        Top(distance),

                        Height(0.5),
                        Width(4),
                        Background("green")
                    });
                }

                return returnList;
            }
        }

        static Element createElement()
        {
            return new iframe
            {
                id    = "ComponentPreview",
                src   = "https://www.google.com",
                style = { BorderNone, WidthFull, HeightFull },
                title = "Component Preview"
            };
        }
    }
    
    Element PartRightPanel()
    {
        return new FlexColumn(AlignItemsCenter,BorderLeft(1, dotted, "#d9d9d9"))
        {
            new StyleEditor()
        };
    }
    Element PartLeftPanel()
    {
        var componentSelector = new MagicInput();

        return new FlexColumn(AlignItemsCenter, BorderRight(1, dotted, "#d9d9d9"))
        {
            componentSelector,
            new FlexRow(WidthFull)
            {
                new FlexRowCentered()
                {
                    "Visual Tree"
                },
                new FlexRowCentered()
                {
                    "Definition"
                },
                new FlexRowCentered()
                {
                    "Export"
                }
            },
            
            new VisualElementTreeView
            {
                //SelectionChanged = SelectionChanged,
                //SelectedPath     = state.SelectedPath,
                Model = new()
                {
                    Tag = "div",
                    Children =
                    [
                        new() { Tag = "label", Text = "Abc1" },
                        new() { Tag = "span", Text  = "Abc2" },
                        new() { Tag = "ul", Text    = "Abc3" },

                        new()
                        {
                            Tag = "div",
                            Children =
                            [
                                new() { Tag = "label", Text = "Abc1" },
                                new() { Tag = "span", Text  = "Abc2" },
                                new()
                                {
                                    Tag  = "ul",
                                    Text = "Abc3"
                                }
                            ]
                        }
                    ]
                }
            }
            
        };
    }
    
    
    internal class State
    {
        public int ScreenWidth { get; init; } = 900;
        
        public int ScreenHeight { get; init; } = 100;
    
        public int Scale { get; init; } = 100;
        
        public ProjectModel Model { get; set; } = new()
        {
            Name = "Demo Project",
            Components =
            [
                new ComponentModel
                {
                    Name        = "LoginIcon",
                    PropsAsJson = "{'isActive': true}",
                    StateAsJson = "{'user': { 'name': 'Tom', 'year': 41 }}",
                    RootElement = new()
                    {
                        Tag = "div",
                        Children =
                        [
                            new() { Tag = "label", Text = "Abc1" },
                            new() { Tag = "span", Text  = "Abc2" },
                            new() { Tag = "ul", Text    = "Abc3" },

                            new()
                            {
                                Tag = "div",
                                Children =
                                [
                                    new() { Tag = "label", Text = "Abc1" },
                                    new() { Tag = "span", Text  = "Abc2" },
                                    new()
                                    {
                                        Tag  = "ul",
                                        Text = "Abc3"
                                    }
                                ]
                            }
                        ]
                    }
                }
            ]
        };
    }
}