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

                new FlexRow(Flex(1,1,0), OverflowYAuto)
                {
                    MainContent
                },
                
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
            new h3 { "React Visual Designer" },

           new FlexRowCentered(Gap(24))
           {
               PartMediaSizeButtons,
            
               PartScale
           },
            
            new LogoutButton(),

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
                new FlexColumn(AlignItemsCenter, FlexGrow(1), Padding(7), MarginLeft(40), ScaleStyle, OverflowXAuto)
                {
                    createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(12),
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
        return new FlexColumn(AlignItemsCenter, Gap(16), BorderLeft(1, dotted, "#d9d9d9"), OverflowYAuto)
        {
            new FlexColumn(WidthFull)
            {
                new label{"Style"},
                new StyleEditor()
            },
            
            new FlexColumn(WidthFull)
            {
                new label{"UserName"},
                new StyleEditor()
            },
            new FlexColumn(WidthFull)
            {
                new label{"UserName"},
                new StyleEditor()
            },
            new FlexColumn(WidthFull)
            {
                new label{"UserName"},
                new StyleEditor()
            },
            new FlexColumn(WidthFull)
            {
                new label{"UserName"},
                new StyleEditor()
            },
            new FlexColumn(WidthFull)
            {
                new label{"UserName"},
                new StyleEditor()
            }
            
        };
    }
    
    Task OnElementTreeTabClicked(MouseEvent e)
    {
        state.LeftPanelSelectedTabName = LeftPanelSelectedTabNames.ElementTree;
        
        return Task.CompletedTask;
    }
    Task OnSettingsTabClicked(MouseEvent e)
    {
        state.LeftPanelSelectedTabName = LeftPanelSelectedTabNames.Settings;
        
        return Task.CompletedTask;
    }
    Task OnSaveTabClicked(MouseEvent e)
    {
        state.LeftPanelSelectedTabName = LeftPanelSelectedTabNames.Save;
        
        return Task.CompletedTask;
    }
    
    Element PartLeftPanel()
    {
        var componentSelector = new MagicInput
        {
            Suggestions = ["abc", "abc3"]
            
        };

        return new FlexColumn(WidthFull, AlignItemsCenter, BorderRight(1, dotted, "#d9d9d9"))
        {
            componentSelector,
            new FlexRow(WidthFull, AlignItemsCenter, Padding(8,4), JustifyContentSpaceAround, BorderBottom(1, dotted, "#d9d9d9"), BorderTop(1, dotted, "#d9d9d9"))
            {
                Color(Gray300),
                
                new FlexRowCentered(WidthFull, OnClick(OnElementTreeTabClicked))
                {
                    new IconLayers() +Size(18)+ (state.LeftPanelSelectedTabName == LeftPanelSelectedTabNames.ElementTree ? Color(Blue300): null)
                },
                new FlexRowCentered(WidthFull,OnClick(OnSettingsTabClicked))
                {
                    new IconSettings() + Size(24) + (state.LeftPanelSelectedTabName == LeftPanelSelectedTabNames.Settings ? Color(Blue300): null)
                },
                new FlexRowCentered(WidthFull, OnClick(OnSaveTabClicked))
                {
                    new IconSave() + Size(24) + (state.LeftPanelSelectedTabName == LeftPanelSelectedTabNames.Save ? Color(Blue300): null)
                },
            },
            
            new VisualElementTreeView
            {
                SelectionChanged = OnVisualElementTreeSelected,
                SelectedPath     = state.SelectedVisualElementTreePath,
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

    

    Task OnVisualElementTreeSelected(string treePath)
    {
        state.SelectedVisualElementTreePath = treePath;

        return Task.CompletedTask;
    }

    static Element createLabel(string text)
    {
        return new small(Text(text), Color(rgb(73, 86, 193)), FontWeight600, UserSelect(none), WhiteSpaceNoWrap);
    }
    
    Element PartScale()
    {
        return new FlexRow(WidthFull, PaddingLeftRight(3), AlignItemsCenter, Gap(5))
        {
            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
            {
                OnClick(async _ =>
                {
                    if (state.Scale <= 20)
                    {
                        return;
                    }

                    state.Scale -= 10;

                    await SaveState();
                }),
                new IconMinus()
            },
            
            $"%{state.Scale}",
            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
            {
                OnClick(async _ =>
                {
                    if (state.Scale >= 100)
                    {
                        return;
                    }

                    state.Scale += 10;

                    await SaveState();
                }),
                new IconPlus()
            }
        };
    }
    Element PartMediaSizeButtons()
    {
        return new FlexRow(JustifyContentSpaceAround, AlignItemsCenter, Gap(16))
        {
            new[] { "M", "SM", "MD", "LG", "XL", "XXL" }.Select(x => new FlexRowCentered
            {
                x,
                FontSize16,
                FontWeight300,
                CursorDefault,
                PaddingTopBottom(3),
                FlexGrow(1),

                Data("value", x),
                OnClick(OnCommonSizeClicked),
                Hover(Color("#2196f3")),

                (x == "M" && state.ScreenWidth == 320) ||
                (x == "SM" && state.ScreenWidth == 640) ||
                (x == "MD" && state.ScreenWidth == 768) ||
                (x == "LG" && state.ScreenWidth == 1024) ||
                (x == "XL" && state.ScreenWidth == 1280) ||
                (x == "XXL" && state.ScreenWidth == 1536)
                    ? FontWeight500 + Color("#2196f3")
                    : null
            })
        };
    }
    
    async Task OnCommonSizeClicked(MouseEvent e)
    {
        state.ScreenWidth = e.currentTarget.data["value"] switch
        {
            "M"   => 320,
            "SM"  => 640,
            "MD"  => 768,
            "LG"  => 1024,
            "XL"  => 1280,
            "XXL" => 1536,
            _     => throw new ArgumentOutOfRangeException()
        };
        

        await SaveState();
    }
    
    async Task SaveState()
    {
        await Task.Delay(111);
    }


    class LeftPanelSelectedTabNames
    {
        public const string ElementTree = "ElementTree";
        public const string Settings = "Settings";
        public const string Save = "Save";
    }
    internal class State
    {
        public string LeftPanelSelectedTabName { get; set; } = LeftPanelSelectedTabNames.ElementTree;

        public string SelectedVisualElementTreePath { get; set; }
        
        public int ScreenWidth { get; set; } = 400;
        
        public int ScreenHeight { get; init; } = 400;
    
        public int Scale { get; set; } = 100;
        
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