using System.Diagnostics;
using Page = ReactWithDotNet.WebSite.Page;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationView : Component<ApplicationView.State>
{
    internal static State AppState;

    enum Icon
    {
        add,
        remove
    }

    public static string UrlPath => "/$";
    internal static string UrlPathOfComponentPreview => $"{UrlPath}?preview=true";

    IReadOnlyList<string> BooleanSuggestions => ["MD", "XXL", "state.user.isActive", "MD: state.user.isActive", "XXL: state.user.isActive"];

    PropertyModel CurrentStyleProperty
    {
        get
        {
            Debug.Assert(state.CurrentPropertyIndexAtStyleGroup != null, "state.CurrentPropertyIndex != null");

            return CurrentStyleGroup.Items[state.CurrentPropertyIndexAtStyleGroup.Value];
        }
    }

    PropertyGroupModel CurrentStyleGroup => CurrentVisualElement.StyleGroups[state.CurrentStyleGroupIndex!.Value];

    VisualElementModel CurrentVisualElement
    {
        get { return FindTreeNodeByTreePath(state.Project.Components.First(x => x.Name == state.CurrentComponentName).RootElement, state.CurrentVisualElementTreePath); }
    }

    StyleModifier ScaleStyle => TransformOrigin("0 0") + Transform($"scale({state.Scale / (double)100})");

    IReadOnlyList<string> StyleAttributeNameSuggestions
    {
        get { return StyleProperties.Select(x => x.Name).ToList(); }
    }

    public Task On_CurrentPropertyIndexInStyle_Changed(string senderName)
    {
        StyleInputLocation location = senderName;

        state.CurrentStyleGroupIndex = location.StyleGroupIndex;
        
        state.CurrentPropertyIndexAtStyleGroup = location.PropertyIndexAtGroup;

        return Task.CompletedTask;
    }

    public Task OnStyleGroupSelected(string senderNameAsStyleGroupIndex)
    {
        state.CurrentStyleGroupIndex = int.Parse(senderNameAsStyleGroupIndex);

        return Task.CompletedTask;
    }

    protected override Task constructor()
    {
        AppState = state = new()
        {
            ScreenWidth                  = 400,
            ScreenHeight                 = 400,
            Scale                        = 100,
            LeftPanelCurrentTabName      = LeftPanelSelectedTabNames.ElementTree,
            Project                      = Dummy.ProjectModel,
            CurrentVisualElementTreePath = null
        };

        if (state.CurrentComponentName is null && state.Project.Components.Count > 0)
        {
            state.CurrentComponentName = state.Project.Components[0].Name;
        }

        return Task.CompletedTask;
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        AppState = state;

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexRow(Padding(10), SizeFull, Background(Theme.BackgroundColor))
        {
            EditorFontLinks,
            EditorFont(),
            new FlexColumn
            {
                applicationTopPanel,

                new FlexRow(Flex(1, 1, 0), OverflowYAuto)
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

    static FlexRowCentered CreateIcon(Icon name, int size, Modifier[] modifiers = null)
    {
        return name switch
        {
            Icon.add    => new(Size(size), BorderRadius(16), Border(1, solid, Gray200), Color(Gray200), Hover(BorderColor(Blue300), Color(Blue300))) { new IconPlus(), modifiers },
            Icon.remove => new(Size(size), BorderRadius(16), Border(1, solid, Gray200), Color(Gray200)) { new IconMinus() , modifiers},
            _           => throw new NotImplementedException(name.ToString())
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

    Task CurrentStyleGroup_CurrentProperty_Add_Clicked(MouseEvent _)
    {
        CurrentStyleGroup.Items.Add(new());

        return Task.CompletedTask;
    }

    Task CurrentStyleGroup_CurrentProperty_Delete_Clicked(MouseEvent _)
    {
        CurrentStyleGroup.Items.Remove(CurrentStyleProperty);

        state.CurrentPropertyIndexAtStyleGroup = null;

        return Task.CompletedTask;
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

    Task On_CurrentPropertyNameInStyle_Changed(string senderName, string newValue)
    {
        StyleInputLocation location = senderName;

        state.CurrentStyleGroupIndex = location.StyleGroupIndex;
        state.CurrentPropertyIndexAtStyleGroup   = location.PropertyIndexAtGroup;
        
        CurrentStyleProperty.Name = newValue;

        return Task.CompletedTask;
    }

    async Task On_CurrentPropertyValueInStyle_Changed(string senderName, string newValue)
    {
        CurrentStyleProperty.Value = newValue;

        await SaveState();
    }
    
    

    Task OnElementTreeTabClicked(MouseEvent e)
    {
        state.LeftPanelCurrentTabName = LeftPanelSelectedTabNames.ElementTree;

        return Task.CompletedTask;
    }

    Task OnTagNameChanged(string senderName, string newValue)
    {
        CurrentVisualElement.Tag = newValue;

        return Task.CompletedTask;
    }
    
    Task OnComponentNameChanged(string senderName, string newValue)
    {
        state.CurrentComponentName = newValue;

        state.CurrentVisualElementTreePath = null;

        return Task.CompletedTask;
    }

    Task OnSaveTabClicked(MouseEvent e)
    {
        state.LeftPanelCurrentTabName = LeftPanelSelectedTabNames.Save;

        return Task.CompletedTask;
    }

    Task OnSettingsTabClicked(MouseEvent e)
    {
        state.LeftPanelCurrentTabName = LeftPanelSelectedTabNames.Settings;

        return Task.CompletedTask;
    }

    Task OnVisualElementTreeSelected(string treePath)
    {
        state.CurrentVisualElementTreePath = treePath;

        return Task.CompletedTask;
    }

    Element PartLeftPanel()
    {
        var componentSelector = new MagicInput
        {
            Name = SenderName.ComponentName,

            Suggestions = state.Project.Components.Select(x => x.Name).ToList(),
            Value       = state.CurrentComponentName,
            OnChange    = OnComponentNameChanged
        };

        return new FlexColumn(WidthFull, AlignItemsCenter, BorderRight(1, dotted, "#d9d9d9"))
        {
            componentSelector,
            new FlexRow(WidthFull, AlignItemsCenter, Padding(8, 4), JustifyContentSpaceAround, BorderBottom(1, dotted, "#d9d9d9"), BorderTop(1, dotted, "#d9d9d9"))
            {
                Color(Gray300),

                new FlexRowCentered(WidthFull, OnClick(OnElementTreeTabClicked))
                {
                    new IconLayers() + Size(18) + (state.LeftPanelCurrentTabName == LeftPanelSelectedTabNames.ElementTree ? Color(Blue300) : null)
                },
                new FlexRowCentered(WidthFull, OnClick(OnSettingsTabClicked))
                {
                    new IconSettings() + Size(24) + (state.LeftPanelCurrentTabName == LeftPanelSelectedTabNames.Settings ? Color(Blue300) : null)
                },
                new FlexRowCentered(WidthFull, OnClick(OnSaveTabClicked))
                {
                    new IconSave() + Size(24) + (state.LeftPanelCurrentTabName == LeftPanelSelectedTabNames.Save ? Color(Blue300) : null)
                }
            },

            new VisualElementTreeView
            {
                SelectionChanged = OnVisualElementTreeSelected,
                SelectedPath     = state.CurrentVisualElementTreePath,
                Model            = state.Project.Components.FirstOrDefault(x => x.Name == state.CurrentComponentName)?.RootElement
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
                src   = Page.VisualDesignerPreview.Url,
                style = { BorderNone, WidthFull, HeightFull },
                title = "Component Preview"
            };
        }
    }

    class StyleInputLocation
    {
        public required int StyleGroupIndex { get; init; }
        public required int PropertyIndexAtGroup { get; init; }
        public required bool IsName { get; init; }
        public required bool IsValue { get; init; }

        public override string ToString() =>
            $"{StyleGroupIndex},{PropertyIndexAtGroup},{IsName},{IsValue}";

        static StyleInputLocation Parse(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 4)
                throw new FormatException("Invalid input format");

            return new StyleInputLocation
            {
                StyleGroupIndex = int.Parse(parts[0]),
                PropertyIndexAtGroup           = int.Parse(parts[1]),
                IsName          = bool.Parse(parts[2]),
                IsValue         = bool.Parse(parts[3])
            };
        }

        public static implicit operator StyleInputLocation(string input) => Parse(input);

        public static implicit operator string(StyleInputLocation location) => location.ToString();
    }
    
    class PropInputLocation
    {
        public required int PropertyIndexAtGroup { get; init; }
        public required bool IsName { get; init; }
        public required bool IsValue { get; init; }

        public override string ToString() =>
            $"{PropertyIndexAtGroup},{IsName},{IsValue}";

        static PropInputLocation Parse(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 4)
                throw new FormatException("Invalid input format");

            return new ()
            {
                PropertyIndexAtGroup = int.Parse(parts[0]),
                IsName               = bool.Parse(parts[1]),
                IsValue              = bool.Parse(parts[2])
            };
        }

        public static implicit operator PropInputLocation(string input) => Parse(input);

        public static implicit operator string(PropInputLocation location) => location.ToString();
    }


    
    Element PartRightPanel()
    {
        if (!state.CurrentVisualElementTreePath.HasValue())
        {
            return new div();
        }

        var tagSuggestions = new List<string>(TagNameList);

        tagSuggestions.AddRange(state.Project.Components.Where(c => c.Name != state.CurrentComponentName).Select(x => x.Name));

        var visualElementModel = CurrentVisualElement;

        return new FlexColumn(BorderLeft(1, dotted, "#d9d9d9"), PaddingX(2), Gap(8), OverflowYAuto, Background(White))
        {
            new FlexRow(WidthFull, Gap(4))
            {
                new label { "Tag", FontWeightBold, Width(4, 10), TextAlignRight },
                " : ",
                new MagicInput
                {
                    Name = SenderName.Tag, 
                    Value = visualElementModel.Tag, 
                    Suggestions = tagSuggestions, 
                    OnChange = OnTagNameChanged
                } + Width(6, 10)
            },

            new FlexRow(WidthFull, AlignItemsCenter)
            {
                CreateIcon(Icon.remove, 32, state.CurrentStyleGroupIndex.HasValue ?
                               [
                                   OnClick(StyleGroupRemoveClicked),
                                   Hover(Color(Blue300))
                               ] :
                               [
                                   Color(Gray100),
                                   BorderColor(Gray100)
                               ]),

                new div { Height(1), FlexGrow(1), Background(Gray200) },
                new span { "S T Y L E", WhiteSpaceNoWrap, UserSelect(none), PaddingX(4) },
                new div { Height(1), FlexGrow(1), Background(Gray200) },

                CreateIcon(Icon.add, 32) + OnClick(StyleGroupAddClicked)
            },

            new FlexColumnCentered(WidthFull)
            {
                visualElementModel.StyleGroups?.Select((styleGroup, styleGroupIndex )=>
                {
                    return new FlexColumn(WidthFull, Gap(4))
                    {
                        new FlexRow(WidthFull, AlignItemsCenter, Gap(4), PaddingX(2))
                        {
                            CreateIcon(Icon.remove, 28, state.CurrentPropertyIndexAtStyleGroup.HasValue &&  state.CurrentStyleGroupIndex == styleGroupIndex ?
                                [
                                    OnClick(CurrentStyleGroup_CurrentProperty_Delete_Clicked),
                                    Hover(Color(Blue300))
                                ] :
                                [
                                    Color(Gray100),
                                    BorderColor(Gray100)
                                ]),

                            new MagicInput
                            {
                                Name              = styleGroupIndex.ToString(),
                                OnFocus           = OnStyleGroupSelected,
                                Value             = styleGroup.Condition,
                                IsTextAlignCenter = true,
                                Suggestions       = BooleanSuggestions
                            } + FlexGrow(1),
                            
                            CreateIcon(Icon.add, 28) + OnClick(CurrentStyleGroup_CurrentProperty_Add_Clicked)
                        },

                        styleGroup.Items.Select((property, index) => new FlexRow(Gap(4))
                        {
                            new FlexRow(JustifyContentFlexEnd, Width(4, 10))
                            {
                                new MagicInput
                                {
                                    OnFocus = On_CurrentPropertyIndexInStyle_Changed,

                                    Name             = new StyleInputLocation { PropertyIndexAtGroup = index, IsName = true, StyleGroupIndex = styleGroupIndex, IsValue = false},
                                    Value            = property.Name,
                                    OnChange         = On_CurrentPropertyNameInStyle_Changed,
                                    IsBold           = true,
                                    IsTextAlignRight = true,
                                    Suggestions      = StyleAttributeNameSuggestions,
                                    Placeholder = "color",
                                    AutoFocus = property.Name.HasNoValue()
                                }
                            },
                            " : ",
                            new FlexRow(Width(6, 10))
                            {
                                new MagicInput
                                {
                                    Name        = new StyleInputLocation { PropertyIndexAtGroup = index, IsName = false, StyleGroupIndex = styleGroupIndex, IsValue = true},
                                    Value       = property.Value,
                                    OnFocus     = On_CurrentPropertyIndexInStyle_Changed,
                                    OnChange    = On_CurrentPropertyValueInStyle_Changed,
                                    Placeholder = "red"
                                }
                            }
                        })
                    };
                })
            },
            
            new FlexRow(WidthFull, AlignItemsCenter)
            {
                CreateIcon(Icon.remove, 32, state.CurrentPropertyIndexAtProps.HasValue ?
                               [
                                   OnClick(RemoveCurrentPropertyAtProps),
                                   Hover(Color(Blue300))
                               ] :
                               [
                                   Color(Gray100),
                                   BorderColor(Gray100)
                               ]),

                new div { Height(1), FlexGrow(1), Background(Gray200) },
                new span { "P R O P S", WhiteSpaceNoWrap, UserSelect(none), PaddingX(4) },
                new div { Height(1), FlexGrow(1), Background(Gray200) },

                CreateIcon(Icon.add, 32) + OnClick(AddNewPropsClicked)
            },
            
            new FlexColumnCentered(WidthFull)
            {
                visualElementModel.Properties?.Select((property, index )=> new FlexRow(Gap(4))
                {
                    new FlexRow(JustifyContentFlexEnd, Width(4, 10))
                    {
                        new MagicInput
                        {
                            OnFocus = On_CurrentPropertyIndexInStyle_Changed,

                            Name             = new PropInputLocation { PropertyIndexAtGroup = index, IsName = true, IsValue = false},
                            Value            = property.Name,
                            OnChange         = On_CurrentPropertyNameInStyle_Changed,
                            IsBold           = true,
                            IsTextAlignRight = true,
                            Suggestions      = StyleAttributeNameSuggestions,
                            Placeholder      = "? ? ?",
                            AutoFocus        = property.Name.HasNoValue()
                        }
                    },
                    " : ",
                    new FlexRow(Width(6, 10))
                    {
                        new MagicInput
                        {
                            Name        = new PropInputLocation { PropertyIndexAtGroup = index, IsName = false, IsValue = true},
                            Value       = property.Value,
                            OnFocus     = On_CurrentPropertyIndexInStyle_Changed,
                            OnChange    = On_CurrentPropertyValueInStyle_Changed,
                            Placeholder = "? ? ?"
                        }
                    }
                })
            },
            
        };
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

    async Task SaveState()
    {
        AppState = state;

        await Task.Delay(1);
    }

    Task StyleGroupAddClicked(MouseEvent e)
    {
        var styleGroups = CurrentVisualElement.StyleGroups ??= [];

        PropertyGroupModel newStyleGroup;
        if (styleGroups.Count == 0)
        {
            newStyleGroup = new()
            {
                Condition = "*",
                Items     = [new PropertyModel()]
            };
        }
        else
        {
            newStyleGroup = new()
            {
                Condition = "? ? ? ?",
                Items     = [new PropertyModel()]
            };
        }

        styleGroups.Add(newStyleGroup);

        state.CurrentStyleGroupIndex = styleGroups.Count -1;

        return Task.CompletedTask;
    }
    
    Task AddNewPropsClicked(MouseEvent e)
    {
        var properties = CurrentVisualElement.Properties ??= [];

        properties.Add(new ());

        state.CurrentPropertyIndexAtProps = properties.Count - 1;

        return Task.CompletedTask;
    }

    Task StyleGroupRemoveClicked(MouseEvent e)
    {
        CurrentVisualElement.StyleGroups.Remove(CurrentStyleGroup);
        
        state.CurrentStyleGroupIndex = null;
        state.CurrentPropertyIndexAtStyleGroup = null;

        return Task.CompletedTask;
    }
    
    Task RemoveCurrentPropertyAtProps(MouseEvent e)
    {
        CurrentVisualElement.Properties.RemoveAt(state.CurrentPropertyIndexAtProps!.Value);
        
        state.CurrentPropertyIndexAtProps = null;

        return Task.CompletedTask;
    }

    static class SenderName
    {
        public static readonly string ComponentName = nameof(ComponentName);
        public static readonly string Tag = nameof(Tag);
    }

    internal class State
    {
        public string CurrentComponentName { get; set; }

        public int? CurrentStyleGroupIndex { get; set; }
        
        public int? CurrentPropertyIndexAtStyleGroup { get; set; }
        
        public int? CurrentPropertyIndexAtProps { get; set; }

        public string CurrentVisualElementTreePath { get; set; }
        
        public string LeftPanelCurrentTabName { get; set; }

        public ProjectModel Project { get; set; }

        public int Scale { get; set; }

        public int ScreenHeight { get; init; }

        public int ScreenWidth { get; set; }
    }

    class LeftPanelSelectedTabNames
    {
        public const string ElementTree = "ElementTree";
        public const string Save = "Save";
        public const string Settings = "Settings";
    }
}

sealed class ApplicationPreview : Component
{
    public Task Refresh()
    {
        Client.GotoMethod(1000, Refresh);

        return Task.CompletedTask;
    }

    protected override Element componentDidCatch(Exception exceptionOccurredInRender)
    {
        return new div(Background(Gray100))
        {
            exceptionOccurredInRender.ToString()
        };
    }

    protected override Task constructor()
    {
        Client.GotoMethod(1000, Refresh);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var appState = ApplicationView.AppState;

        if (appState is null)
        {
            return new div(Size(200), Background(Gray100))
            {
                "Has no state"
            };
        }

        var componentModel = appState.Project.Components.FirstOrDefault(x => x.Name == appState.CurrentComponentName);
        if (componentModel is null)
        {
            return new div(Size(200), Background(Gray100))
            {
                "Has no component"
            };
        }

        return new div(Size(333))
        {
            renderElement(componentModel.RootElement)
        };

        Element renderElement(VisualElementModel model)
        {
            var element = new div
            {
                model.Tag
            };

            foreach (var styleGroup in model.StyleGroups ?? [])
            {
                foreach (var styleAttribute in styleGroup.Items ?? [])
                {
                    var value = styleAttribute.Value;

                    var isValueDouble = double.TryParse(value, out var valueAsDouble);

                    switch (styleAttribute.Name)
                    {
                        case "display":
                        {
                            element.Add(Display(value));
                            continue;
                        }

                        case "background":
                        {
                            element.Add(Background(value));
                            continue;
                        }

                        case "width":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Width(valueAsDouble));
                                continue;
                            }

                            element.Add(Width(value));
                            continue;
                        }

                        case "height":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Height(valueAsDouble));
                                continue;
                            }

                            element.Add(Height(value));
                            continue;
                        }

                        case "border-radius":
                        {
                            if (isValueDouble)
                            {
                                element.Add(BorderRadius(valueAsDouble));
                                continue;
                            }

                            element.Add(BorderRadius(value));
                            continue;
                        }

                        case "gap":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Gap(valueAsDouble));
                                continue;
                            }

                            element.Add(Gap(value));
                            continue;
                        }

                        case "padding":
                        {
                            if (isValueDouble)
                            {
                                element.Add(Padding(valueAsDouble));
                                continue;
                            }

                            element.Add(Padding(value));
                            continue;
                        }
                    }
                }
            }

            if (model.HasChild is false)
            {
                return element;
            }

            element.children.AddRange(model.Children.Select(renderElement));

            return element;
        }
    }
}