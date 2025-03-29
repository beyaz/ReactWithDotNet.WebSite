using System.Diagnostics;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using Page = ReactWithDotNet.WebSite.Page;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationView : Component<ApplicationState>
{
    internal static ApplicationState AppState;
    internal static int AppStateVersion;

    enum Icon
    {
        add,
        remove
    }

    ComponentModel CurrentComponent => state.Project.Components.First(x => x.Name == state.CurrentComponentName);

    PropertyGroupModel CurrentStyleGroup => CurrentVisualElement.StyleGroups[state.CurrentStyleGroupIndex!.Value];

    PropertyModel CurrentStyleProperty
    {
        get
        {
            Debug.Assert(state.CurrentPropertyIndexInStyleGroup != null, "state.CurrentPropertyIndex != null");

            return CurrentStyleGroup.Items[state.CurrentPropertyIndexInStyleGroup.Value];
        }
    }

    VisualElementModel CurrentVisualElement
    {
        get { return FindTreeNodeByTreePath(state.Project.Components.First(x => x.Name == state.CurrentComponentName).RootElement, state.CurrentVisualElementTreePath); }
    }

    IReadOnlyList<string> StyleAttributeNameSuggestions
    {
        get { return StyleProperties.Select(x => x.Name).ToList(); }
    }

    public Task On_CurrentPropertyIndexInProps_Changed(string senderName)
    {
        PropInputLocation location = senderName;

        state.CurrentPropertyIndexInProps = location.Index;

        return Task.CompletedTask;
    }

    public Task On_CurrentPropertyIndexInStyle_Changed(string senderName)
    {
        StyleInputLocation location = senderName;

        state.CurrentStyleGroupIndex = location.StyleGroupIndex;

        state.CurrentPropertyIndexInStyleGroup = location.PropertyIndexAtGroup;

        return Task.CompletedTask;
    }

    public Task On_CurrentPropertyValueInProps_Changed(string senderName, string newValue)
    {
        CurrentVisualElement.Properties[state.CurrentPropertyIndexInProps!.Value].Value = newValue;

        return Task.CompletedTask;
    }

    public Task OnStyleGroupSelected(string senderNameAsStyleGroupIndex)
    {
        state.CurrentStyleGroupIndex = int.Parse(senderNameAsStyleGroupIndex);

        return Task.CompletedTask;
    }

    protected override async Task constructor()
    {
        AppState = state = ApplicationStateCache.ReadState() ?? new()
        {
            ScreenWidth                  = 400,
            ScreenHeight                 = 400,
            Scale                        = 100,
            LeftPanelCurrentTab          = LeftPanelTab.ElementTree,
            Project                      = Dummy.ProjectModel,
            CurrentVisualElementTreePath = null
        };

        if (state.CurrentComponentName is null && state.Project.Components.Count > 0)
        {
            await OnComponentNameChanged(state.Project.Components[0].Name);
        }
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        AppState = state;

        AppStateVersion++;

        Client.RefreshComponentPreview();

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
                PartApplicationTopPanel,

                new FlexRow(Flex(1, 1, 0), OverflowYAuto)
                {
                    MainContent
                },

                new Style
                {
                    Border(Solid(1, Theme.BorderColor)),
                    SizeFull,
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
            Icon.remove => new(Size(size), BorderRadius(16), Border(1, solid, Gray200), Color(Gray200)) { new IconMinus(), modifiers },
            _           => throw new NotImplementedException(name.ToString())
        };
    }

    Task AddNewPropsClicked(MouseEvent e)
    {
        var properties = CurrentVisualElement.Properties ??= [];

        properties.Add(new());

        state.CurrentPropertyIndexInProps = properties.Count - 1;

        return Task.CompletedTask;
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

        state.CurrentPropertyIndexInStyleGroup = null;

        return Task.CompletedTask;
    }

    Task JsonTextInComponentSettingsUpdatedByUser()
    {
        state.JsonTextInComponentSettings = JsonPrettify(state.JsonTextInComponentSettings);

        switch (state.SettingsCurrentTab)
        {
            case SettingsTab.Props:
                CurrentComponent.PropsAsJson = state.JsonTextInComponentSettings;
                return Task.CompletedTask;

            case SettingsTab.State:
                CurrentComponent.StateAsJson = state.JsonTextInComponentSettings;
                return Task.CompletedTask;

            case SettingsTab.Other:
                CurrentComponent.OtherAsJson = state.JsonTextInComponentSettings;
                return Task.CompletedTask;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    Element MainContent()
    {
        var scaleStyle = TransformOrigin("0 0") + Transform($"scale({state.Scale / (double)100})");

        return new SplitRow
        {
            sizes = [20, 60, 20],
            children =
            {
                PartLeftPanel() + BorderBottomLeftRadius(8) + OverflowAuto,

                new FlexColumn(state.ScreenWidth < 768 ? AlignItemsCenter : AlignItemsFlexStart, FlexGrow(1), Padding(7), MarginLeft(40), scaleStyle, OverflowXAuto)
                {
                    createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(12),
                    PartPreview
                },

                PartRightPanel() + BorderBottomRightRadius(8)
            }
        };
    }

    Task On_CurrentPropertyNameInProps_Changed(string senderName, string newValue)
    {
        PropInputLocation location = senderName;

        state.CurrentPropertyIndexInProps = location.Index;

        CurrentVisualElement.Properties[state.CurrentPropertyIndexInProps!.Value].Name = newValue;

        return Task.CompletedTask;
    }

    Task On_CurrentPropertyNameInStyle_Changed(string senderName, string newValue)
    {
        StyleInputLocation location = senderName;

        state.CurrentStyleGroupIndex           = location.StyleGroupIndex;
        state.CurrentPropertyIndexInStyleGroup = location.PropertyIndexAtGroup;

        CurrentStyleProperty.Name = newValue;

        return Task.CompletedTask;
    }

    Task On_CurrentPropertyValueInStyle_Changed(string senderName, string newValue)
    {
        CurrentStyleProperty.Value = newValue;

        return Task.CompletedTask;
    }

    Task OnCommonSizeClicked(MouseEvent e)
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

        return Task.CompletedTask;
    }

    Task OnComponentNameChanged(string senderName, string newValue)
    {
        return OnComponentNameChanged(newValue);
    }
    
    Task On_Project_Changed(string senderName, string newValue)
    {
        return On_Project_Changed(newValue);
    }
    
    
    Task On_Project_Changed(string newValue)
    {
        state.CurrentProjectName = newValue;
        
        // reload project state

        return Task.CompletedTask;
    }

    Task OnComponentNameChanged(string newValue)
    {
        state.CurrentComponentName = newValue;

        var componentModel = state.Project.Components.First(x => x.Name == newValue);

        if (state.SettingsCurrentTab == SettingsTab.Props)
        {
            state.JsonTextInComponentSettings = componentModel.PropsAsJson;
        }

        state.CurrentVisualElementTreePath = null;

        return Task.CompletedTask;
    }

    Task OnElementTreeTabClicked(MouseEvent e)
    {
        state.LeftPanelCurrentTab = LeftPanelTab.ElementTree;

        return Task.CompletedTask;
    }

    Task OnSaveTabClicked(MouseEvent e)
    {
        this.SuccessNotification("Successfully saved.");

        return Task.CompletedTask;
    }

    Task OnSettingsTabClicked(MouseEvent e)
    {
        state.LeftPanelCurrentTab = LeftPanelTab.Settings;

        return Task.CompletedTask;
    }

    Task OnTagNameChanged(string senderName, string newValue)
    {
        CurrentVisualElement.Tag = newValue;

        return Task.CompletedTask;
    }

    Task OnTextChanged(string _, string text)
    {
        CurrentVisualElement.Text = text;

        return Task.CompletedTask;
    }

    Task OnVisualElementTreeItemHovered(string treeItemPath)
    {
        state.HoveredVisualElementTreeItemPath = treeItemPath;

        return Task.CompletedTask;
    }

    Task OnVisualElementTreeSelected(string treePath)
    {
        state.CurrentVisualElementTreePath = treePath;

        return Task.CompletedTask;
    }

    Element PartApplicationTopPanel()
    {
        return new FlexRow(UserSelect(none))
        {
           new FlexRowCentered(Gap(16))
           {
               new h3 { "React Visual Designer" },

               new FlexRowCentered(Gap(4))
               {
                   new FlexRowCentered
                   {
                        new IconSettings() + Size(24) + Color(state.CurrentProjectSettingsPopupIsVisible ? Gray600: Gray300) + Hover(Color(Gray600)),
                        
                        OnClick(_ =>
                        {
                            state.CurrentProjectSettingsPopupIsVisible = !state.CurrentProjectSettingsPopupIsVisible;
                            
                            return Task.CompletedTask;
                        }),
                        
                        
                       state.CurrentProjectSettingsPopupIsVisible ? PositionRelative : null,
                       state.CurrentProjectSettingsPopupIsVisible ? new FlexColumnCentered(PositionAbsolute, Top(24), Left(16), Zindex2)
                       {
                           Background(White), Border(Solid(1, Theme.BorderColor)), BorderRadius(4), Padding(8),
                           
                           Width(400),
                           
                           new FlexColumn(WidthFull, Gap(16))
                           {
                               new FlexRow(Gap(4))
                               {
                                   new MagicInput
                                   {
                                      Placeholder = "New component name",
                                      Name = string.Empty,
                                      Value = null
                                   },
                                   
                                   new div(Padding(4),Border(Solid(1, Theme.BorderColor)), BorderRadius(4))
                                   {
                                       "Add New Component"
                                   }
                               },
                               
                               new FlexRowCentered(WidthFull, Height(300))
                               {
                                   new Editor
                                   {
                                       defaultLanguage          = "json",
                                       valueBind                = () => state.JsonTextInComponentSettings,
                                       valueBindDebounceTimeout = 700,
                                       valueBindDebounceHandler = JsonTextInComponentSettingsUpdatedByUser,
                                       options =
                                       {
                                           renderLineHighlight = "none",
                                           fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                                           fontSize            = 11,
                                           minimap             = new { enabled = false },
                                           formatOnPaste       = true,
                                           formatOnType        = true,
                                           automaticLayout     = true,
                                           lineNumbers         = false
                                       }
                                   }
                               }
                               
                           }
                           
                           
                           
                           
                       }: null
                       
                   },

                   new MagicInput
                   {
                       Name = string.Empty,

                       Suggestions = GetProjectNames(state),
                       Value       = state.CurrentProjectName,
                       OnChange    = On_Project_Changed,
                       FitContent  = true
                   }
               }
           },
            new FlexRowCentered(Gap(24))
            {
                new FlexRowCentered(Gap(4))
                {
                    new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                    {
                        OnClick(_ =>
                        {
                            state.ScreenWidth -= 10;

                            return Task.CompletedTask;
                        }),

                        new IconMinus()
                    },
                    $"{state.ScreenWidth}px",
                    new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                    {
                        OnClick(_ =>
                        {
                            state.ScreenWidth += 10;

                            return Task.CompletedTask;
                        }),
                        new IconPlus()
                    }
                },
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

    Element PartLeftPanel()
    {
        var componentSelector = new MagicInput
        {
            Name = string.Empty,

            Suggestions       = state.Project.Components.Select(x => x.Name).ToList(),
            Value             = state.CurrentComponentName,
            OnChange          = OnComponentNameChanged,
            IsTextAlignCenter = true,
            IsBold            = true
        };

        return new FlexColumn(WidthFull, AlignItemsCenter, BorderRight(1, dotted, "#d9d9d9"), Background(White))
        {
            componentSelector,
            new FlexRow(WidthFull, AlignItemsCenter, Padding(8, 4), JustifyContentSpaceAround, BorderBottom(1, dotted, "#d9d9d9"), BorderTop(1, dotted, "#d9d9d9"))
            {
                Color(Gray300),

                new FlexRowCentered(WidthFull, OnClick(OnElementTreeTabClicked))
                {
                    new IconLayers() + Size(18) + (state.LeftPanelCurrentTab == LeftPanelTab.ElementTree ? Color(Blue300) : null)
                },
                new FlexRowCentered(WidthFull, OnClick(OnSettingsTabClicked))
                {
                    new IconSettings() + Size(24) + (state.LeftPanelCurrentTab == LeftPanelTab.Settings ? Color(Blue300) : null)
                },
                new FlexRowCentered(WidthFull, OnClick(OnSaveTabClicked))
                {
                    new IconSave() + Size(24) + Hover(Color(Blue300))
                }
            },

            When(state.LeftPanelCurrentTab == LeftPanelTab.ElementTree, () => new VisualElementTreeView
            {
                TreeItemHover = OnVisualElementTreeItemHovered,
                MouseLeave = () =>
                {
                    state.HoveredVisualElementTreeItemPath = null;
                    return Task.CompletedTask;
                },
                SelectionChanged = OnVisualElementTreeSelected,
                SelectedPath     = state.CurrentVisualElementTreePath,
                Model            = state.Project.Components.FirstOrDefault(x => x.Name == state.CurrentComponentName)?.RootElement
            }),

            When(state.LeftPanelCurrentTab == LeftPanelTab.Settings, PartSettingsPanel)
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
                    Name        = string.Empty,
                    Value       = visualElementModel.Tag,
                    Suggestions = tagSuggestions,
                    OnChange    = OnTagNameChanged
                } + Width(6, 10)
            },

            new FlexRow(WidthFull, Gap(4))
            {
                new label { "Text", FontWeightBold, Width(4, 10), TextAlignRight },
                " : ",
                new MagicInput
                {
                    Name        = string.Empty,
                    Value       = visualElementModel.Text,
                    Suggestions = tagSuggestions,
                    OnChange    = OnTextChanged
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
                visualElementModel.StyleGroups?.Select((styleGroup, styleGroupIndex) =>
                {
                    return new FlexColumn(WidthFull, Gap(4))
                    {
                        new FlexRow(WidthFull, AlignItemsCenter, Gap(4), PaddingX(2))
                        {
                            CreateIcon(Icon.remove, 28, state.CurrentPropertyIndexInStyleGroup.HasValue && state.CurrentStyleGroupIndex == styleGroupIndex ?
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
                                Suggestions       = GetStyleGroupConditionSuggestions(state)
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

                                    Name             = new StyleInputLocation { PropertyIndexAtGroup = index, IsName = true, StyleGroupIndex = styleGroupIndex, IsValue = false },
                                    Value            = property.Name,
                                    OnChange         = On_CurrentPropertyNameInStyle_Changed,
                                    IsBold           = true,
                                    IsTextAlignRight = true,
                                    Suggestions      = StyleAttributeNameSuggestions,
                                    Placeholder      = "color",
                                    AutoFocus        = property.Name.HasNoValue()
                                }
                            },
                            " : ",
                            new FlexRow(Width(6, 10))
                            {
                                new MagicInput
                                {
                                    Name        = new StyleInputLocation { PropertyIndexAtGroup = index, IsName = false, StyleGroupIndex = styleGroupIndex, IsValue = true },
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
                CreateIcon(Icon.remove, 32, state.CurrentPropertyIndexInProps.HasValue ?
                               [
                                   OnClick(RemoveCurrentPropertyInProps),
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
                visualElementModel.Properties?.Select((property, index) => new FlexRow(Gap(4), WidthFull)
                {
                    new FlexRow(JustifyContentFlexEnd, Width(4, 10))
                    {
                        new MagicInput
                        {
                            OnFocus = On_CurrentPropertyIndexInProps_Changed,

                            Name             = new PropInputLocation { Index = index, IsName = true, IsValue = false },
                            Value            = property.Name,
                            OnChange         = On_CurrentPropertyNameInProps_Changed,
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
                            Name        = new PropInputLocation { Index = index, IsName = false, IsValue = true },
                            Value       = property.Value,
                            OnFocus     = On_CurrentPropertyIndexInProps_Changed,
                            OnChange    = On_CurrentPropertyValueInProps_Changed,
                            Placeholder = "? ? ?"
                        }
                    }
                })
            }
        };
    }

    Element PartScale()
    {
        return new FlexRow(WidthFull, PaddingLeftRight(3), AlignItemsCenter, Gap(4))
        {
            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
            {
                OnClick(_ =>
                {
                    if (state.Scale <= 20)
                    {
                        return Task.CompletedTask;
                    }

                    state.Scale -= 10;

                    return Task.CompletedTask;
                }),
                new IconMinus()
            },

            $"%{state.Scale}",
            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
            {
                OnClick(_ =>
                {
                    if (state.Scale >= 100)
                    {
                        return Task.CompletedTask;
                    }

                    state.Scale += 10;

                    return Task.CompletedTask;
                }),
                new IconPlus()
            }
        };
    }

    Element PartSettingsPanel()
    {
        return new FlexColumn(SizeFull)
        {
            new FlexRow(JustifyContentSpaceAround, Background(Gray100), PaddingY(4), CursorDefault, Opacity(0.7))
            {
                new FlexRowCentered(When(state.SettingsCurrentTab == SettingsTab.Props, FontWeightBold))
                {
                    "Props",
                    PaddingX(8), OnClick(_ =>
                    {
                        state.SettingsCurrentTab = SettingsTab.Props;

                        state.JsonTextInComponentSettings = CurrentComponent.PropsAsJson;

                        return Task.CompletedTask;
                    })
                },
                new FlexRowCentered(When(state.SettingsCurrentTab == SettingsTab.State, FontWeightBold))
                {
                    "State",
                    PaddingX(8), OnClick(_ =>
                    {
                        state.SettingsCurrentTab = SettingsTab.State;

                        state.JsonTextInComponentSettings = CurrentComponent.StateAsJson;

                        return Task.CompletedTask;
                    })
                },
                new FlexRowCentered(When(state.SettingsCurrentTab == SettingsTab.Other, FontWeightBold))
                {
                    "Other",
                    PaddingX(8), OnClick(_ =>
                    {
                        state.SettingsCurrentTab = SettingsTab.Other;

                        state.JsonTextInComponentSettings = CurrentComponent.OtherAsJson;

                        return Task.CompletedTask;
                    })
                }
            },
            new FlexColumnCentered(SizeFull)
            {
                new Editor
                {
                    defaultLanguage          = "json",
                    valueBind                = () => state.JsonTextInComponentSettings,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = JsonTextInComponentSettingsUpdatedByUser,
                    options =
                    {
                        renderLineHighlight = "none",
                        fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                        fontSize            = 11,
                        minimap             = new { enabled = false },
                        formatOnPaste       = true,
                        formatOnType        = true,
                        automaticLayout     = true,
                        lineNumbers         = false
                    }
                }
            }
        };
    }

    Task RemoveCurrentPropertyInProps(MouseEvent e)
    {
        CurrentVisualElement.Properties.RemoveAt(state.CurrentPropertyIndexInProps!.Value);

        state.CurrentPropertyIndexInProps = null;

        return Task.CompletedTask;
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

        state.CurrentStyleGroupIndex = styleGroups.Count - 1;

        return Task.CompletedTask;
    }

    Task StyleGroupRemoveClicked(MouseEvent e)
    {
        CurrentVisualElement.StyleGroups.Remove(CurrentStyleGroup);

        state.CurrentStyleGroupIndex           = null;
        state.CurrentPropertyIndexInStyleGroup = null;

        return Task.CompletedTask;
    }

    class PropInputLocation
    {
        public required int Index { get; init; }
        public required bool IsName { get; init; }
        public required bool IsValue { get; init; }

        public static implicit operator PropInputLocation(string input)
        {
            return Parse(input);
        }

        public static implicit operator string(PropInputLocation location)
        {
            return location.ToString();
        }

        public override string ToString()
        {
            return $"{Index},{IsName},{IsValue}";
        }

        static PropInputLocation Parse(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 3)
            {
                throw new FormatException("Invalid input format");
            }

            return new()
            {
                Index   = int.Parse(parts[0]),
                IsName  = bool.Parse(parts[1]),
                IsValue = bool.Parse(parts[2])
            };
        }
    }

    class StyleInputLocation
    {
        public required bool IsName { get; init; }
        public required bool IsValue { get; init; }
        public required int PropertyIndexAtGroup { get; init; }
        public required int StyleGroupIndex { get; init; }

        public static implicit operator StyleInputLocation(string input)
        {
            return Parse(input);
        }

        public static implicit operator string(StyleInputLocation location)
        {
            return location.ToString();
        }

        public override string ToString()
        {
            return $"{StyleGroupIndex},{PropertyIndexAtGroup},{IsName},{IsValue}";
        }

        static StyleInputLocation Parse(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 4)
            {
                throw new FormatException("Invalid input format");
            }

            return new()
            {
                StyleGroupIndex      = int.Parse(parts[0]),
                PropertyIndexAtGroup = int.Parse(parts[1]),
                IsName               = bool.Parse(parts[2]),
                IsValue              = bool.Parse(parts[3])
            };
        }
    }
}