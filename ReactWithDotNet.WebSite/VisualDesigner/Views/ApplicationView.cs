using System.Diagnostics;
using Dapper.Contrib.Extensions;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using Page = ReactWithDotNet.WebSite.Page;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationView : Component<ApplicationState>
{
    enum Icon
    {
        add,
        remove
    }

    PropertyGroupModel CurrentStyleGroup => CurrentVisualElement.StyleGroups[state.SelectedStyleGroupIndex!.Value];

    PropertyModel CurrentStyleProperty
    {
        get
        {
            Debug.Assert(state.SelectedPropertyIndexInStyleGroup != null, "state.CurrentPropertyIndex != null");

            return CurrentStyleGroup.Items[state.SelectedPropertyIndexInStyleGroup.Value];
        }
    }

    VisualElementModel CurrentVisualElement => FindTreeNodeByTreePath(state.ComponentRootElement, state.SelectedVisualElementTreeItemPath);

    IReadOnlyList<string> StyleAttributeNameSuggestions
    {
        get { return StyleProperties.Select(x => x.Name).ToList(); }
    }

    public Task On_CurrentPropertyIndexInStyle_Changed(string senderName)
    {
        StyleInputLocation location = senderName;

        state.SelectedStyleGroupIndex = location.StyleGroupIndex;

        state.SelectedPropertyIndexInStyleGroup = location.PropertyIndexAtGroup;

        return Task.CompletedTask;
    }

    protected override async Task constructor()
    {
        var userName = Environment.UserName; // future: get userName from cookie or url

        // try take from memory cache
        {
            var userLastState = GetUserLastState(userName);
            if (userLastState is not null)
            {
                state = userLastState;

                return;
            }
        }

        // try take from db cache
        {
            var lastUsage = (await GetLastUsageInfoByUserName(userName)).FirstOrDefault();
            if (lastUsage is not null)
            {
                state = DeserializeFromJson<ApplicationState>(lastUsage.StateAsJson);

                return;
            }
        }

        // create new state

        state = new()
        {
            UserName = userName,

            Preview = new()
            {
                Width  = 600,
                Height = 100,
                Scale  = 100
            }
        };

        var projectId = await GetFirstProjectId();
        if (projectId.HasValue)
        {
            await ChangeSelectedProject(projectId.Value);
        }
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        SetUserLastState(state);

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
        Style style = [Size(size), BorderRadius(16), BorderWidth(1), BorderStyle(solid), BorderColor(Gray200), Color(Gray200)];

        if (name == Icon.add)
        {
            return new(style)
            {
                new IconPlus(),

                Hover(BorderColor(Blue300), Color(Blue300)),
                modifiers
            };
        }

        if (name == Icon.remove)
        {
            return new(style)
            {
                new IconMinus(), modifiers
            };
        }

        throw new NotImplementedException(name.ToString());
    }

    Task AddNewLayerClicked(MouseEvent e)
    {
        // add as root
        if (state.ComponentRootElement is null)
        {
            state.ComponentRootElement = new()
            {
                Tag = "div"
            };

            state.SelectedVisualElementTreeItemPath = "0";
            state.HoveredVisualElementTreeItemPath  = null;

            return Task.CompletedTask;
        }

        var node = FindTreeNodeByTreePath(state.ComponentRootElement, state.SelectedVisualElementTreeItemPath);

        (node.Children ??= []).Add(new()
        {
            Tag = "div"
        });

        state.SelectedVisualElementTreeItemPath = state.SelectedVisualElementTreeItemPath + "," + (node.Children.Count - 1);
        state.HoveredVisualElementTreeItemPath  = null;

        return Task.CompletedTask;
    }

    Task AddNewPropsClicked(MouseEvent e)
    {
        var properties = CurrentVisualElement.Properties ??= [];

        properties.Add(new());

        state.SelectedPropertyIndexInProps = properties.Count - 1;

        return Task.CompletedTask;
    }

    async Task ChangeSelectedComponent(int componentId)
    {
        state.ComponentId = componentId;

        var componentModel = await GetSelectedComponent(state);

        var componentRootElement = DeserializeFromJson<VisualElementModel>(componentModel.RootElementAsJson ?? string.Empty);

        state = new()
        {
            UserName = state.UserName,

            ProjectId = state.ProjectId,

            Preview = state.Preview,

            LeftPanelSelectedTab = LeftPanelTab.ElementTree,

            ComponentId = componentId,

            ComponentRootElement = componentRootElement
        };

    }

    async Task ChangeSelectedProject(int projectId)
    {
        var userName = Environment.UserName; // future: get userName from cookie or url

        // try take from db cache
        {
            var lastUsage = (await GetLastUsageInfoByUserName(userName)).FirstOrDefault();
            if (lastUsage is not null)
            {
                state = DeserializeFromJson<ApplicationState>(lastUsage.StateAsJson);

                return;
            }
        }

        state = new()
        {
            UserName = state.UserName,

            ProjectId = projectId,

            Preview = state.Preview,

            LeftPanelSelectedTab = LeftPanelTab.ElementTree
        };

        // try select first component
        {
            var component = await GetFirstComponentInProject(projectId);
            if (component is null)
            {
                return;
            }

            await ChangeSelectedComponent(component.Id);
        }
    }

    Element createHorizontalRuler()
    {
        const int step = 50;
        var max = state.Preview.Width / step + 1;

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
            while (distance <= state.Preview.Width)
            {
                cursor++;

                distance = cursor * miniStep;

                if (distance % step == 0 || distance > state.Preview.Width)
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

    Task JsonTextInComponentSettingsUpdatedByUser()
    {
        state.JsonText = JsonPrettify(state.JsonText);

        switch (state.LeftPanelSelectedTab)
        {
            case LeftPanelTab.Props:
                return DbSave(state, x => x with { PropsAsJson = state.JsonText });

            case LeftPanelTab.State:
                return DbSave(state, x => x with { StateAsJson = state.JsonText });

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    Task LayersTabRemoveSelectedItemClicked(MouseEvent e)
    {
        var intArray = state.SelectedVisualElementTreeItemPath.Split(',');
        if (intArray.Length == 1)
        {
            state.ComponentRootElement = null;
        }
        else
        {
            var node = state.ComponentRootElement;

            for (var i = 1; i < intArray.Length - 1; i++)
            {
                node = node.Children[int.Parse(intArray[i])];
            }

            node.Children.RemoveAt(int.Parse(intArray[^1]));
        }

        state.SelectedVisualElementTreeItemPath = null;
        state.HoveredVisualElementTreeItemPath  = null;

        return Task.CompletedTask;
    }

    async Task<Element> MainContent()
    {
        var scaleStyle = TransformOrigin("0 0") + Transform($"scale({state.Preview.Scale / (double)100})");

        return new SplitRow
        {
            sizes = [20, 60, 20],
            children =
            {
                (await PartLeftPanel()) + BorderBottomLeftRadius(8) + OverflowAuto,

                new FlexColumn(state.Preview.Width < 768 ? AlignItemsCenter : AlignItemsFlexStart, FlexGrow(1), Padding(7), MarginLeft(40), scaleStyle, OverflowXAuto)
                {
                    createHorizontalRuler() + Width(state.Preview.Width) + MarginTop(12),
                    PartPreview
                },

                PartRightPanel() + BorderBottomRightRadius(8)
            }
        };
    }

    Task On_CurrentPropertyNameInProps_Changed(string senderName, string newValue)
    {
        PropInputLocation location = senderName;

        state.SelectedPropertyIndexInProps = location.Index;

        CurrentVisualElement.Properties[state.SelectedPropertyIndexInProps!.Value].Name = newValue;

        return Task.CompletedTask;
    }

    Task On_CurrentPropertyNameInStyle_Changed(string senderName, string newValue)
    {
        StyleInputLocation location = senderName;

        state.SelectedStyleGroupIndex           = location.StyleGroupIndex;
        state.SelectedPropertyIndexInStyleGroup = location.PropertyIndexAtGroup;

        CurrentStyleProperty.Name = newValue;

        return Task.CompletedTask;
    }

    Task OnCommonSizeClicked(MouseEvent e)
    {
        state.Preview.Width = e.currentTarget.data["value"] switch
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

    Task OnComponentNameChanged(string newValue)
    {
        return ChangeSelectedComponent(GetAllComponentsInProject(state).First(x => x.Name == newValue).Id);
    }

    Element PartApplicationTopPanel()
    {
        return new FlexRow(UserSelect(none))
        {
            new FlexRowCentered(Gap(16))
            {
                new h3 { "React Visual Designer" },

                PartProject
            },
            new FlexRowCentered(Gap(32))
            {
                new FlexRowCentered(Gap(4))
                {
                    new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                    {
                        OnClick(_ =>
                        {
                            state.Preview.Width -= 10;

                            return Task.CompletedTask;
                        }),

                        new IconMinus()
                    },
                    $"{state.Preview.Width}px",
                    new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                    {
                        OnClick(_ =>
                        {
                            state.Preview.Width += 10;

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

    async Task<Element> PartLeftPanel()
    {
        var componentSelector = new MagicInput
        {
            Name = string.Empty,

            Suggestions       = GetSuggestionsForComponentSelection(state),
            Value             = await GetSelectedComponentName(state),
            OnChange          = (_, componentName) => OnComponentNameChanged(componentName),
            IsTextAlignCenter = true,
            IsBold            = true
        };

        var removeIconInLayersTab = CreateIcon(Icon.remove, 16);
        if (state.LeftPanelSelectedTab == LeftPanelTab.ElementTree && state.SelectedVisualElementTreeItemPath.HasValue())
        {
            removeIconInLayersTab.Add(Hover(Color(Blue300), BorderColor(Blue300)), OnClick(LayersTabRemoveSelectedItemClicked));
        }
        else
        {
            removeIconInLayersTab.Add(VisibilityCollapse);
        }

        var addIconInLayersTab = CreateIcon(Icon.add, 16);
        if (state.LeftPanelSelectedTab == LeftPanelTab.ElementTree && (state.ComponentRootElement is null || state.SelectedVisualElementTreeItemPath.HasValue()))
        {
            addIconInLayersTab.Add(Hover(Color(Blue300), BorderColor(Blue300)), OnClick(AddNewLayerClicked));
        }
        else
        {
            addIconInLayersTab.Add(VisibilityCollapse);
        }

        return new FlexColumn(WidthFull, AlignItemsCenter, BorderRight(1, dotted, "#d9d9d9"), Background(White))
        {
            componentSelector,
            new FlexRow(WidthFull, FontWeightBold, AlignItemsCenter, Padding(8, 4), JustifyContentSpaceAround, BorderBottom(1, dotted, "#d9d9d9"), BorderTop(1, dotted, "#d9d9d9"))
            {
                Color(Gray300), CursorDefault, UserSelect(none),

                new FlexRowCentered(WidthFull)
                {
                    removeIconInLayersTab,

                    new FlexRowCentered(WidthFull)
                    {
                        new IconLayers() + Size(18) + (state.LeftPanelSelectedTab == LeftPanelTab.ElementTree ? Color(Gray500) : null),

                        OnClick(_ =>
                        {
                            state.LeftPanelSelectedTab = LeftPanelTab.ElementTree;

                            return Task.CompletedTask;
                        })
                    },

                    addIconInLayersTab
                },

                new FlexRowCentered(WidthFull, When(state.LeftPanelSelectedTab == LeftPanelTab.Props, Color(Gray500)))
                {
                    "Props",
                    PaddingX(8), OnClick(async _ =>
                    {
                        state.LeftPanelSelectedTab = LeftPanelTab.Props;

                        state.JsonText = (await GetSelectedComponent(state)).PropsAsJson;

                        await DbOperationForCurrentComponent(state, x => { state.JsonText = x.PropsAsJson; });
                    })
                },
                new FlexRowCentered(WidthFull, Opacity(0.7), When(state.LeftPanelSelectedTab == LeftPanelTab.State, Color(Gray500)))
                {
                    "State",
                    PaddingX(8), OnClick(async _ =>
                    {
                        state.LeftPanelSelectedTab = LeftPanelTab.State;

                        state.JsonText = (await GetSelectedComponent(state)).StateAsJson;

                        await DbOperationForCurrentComponent(state, x => { state.JsonText = x.StateAsJson; });
                    })
                }
            },

            When(state.LeftPanelSelectedTab == LeftPanelTab.ElementTree, () => new VisualElementTreeView
            {
                TreeItemHover = treeItemPath =>
                {
                    state.HoveredVisualElementTreeItemPath = treeItemPath;

                    return Task.CompletedTask;
                },
                MouseLeave = () =>
                {
                    state.HoveredVisualElementTreeItemPath = null;
                    return Task.CompletedTask;
                },
                SelectionChanged = treeItemPath =>
                {
                    state.SelectedVisualElementTreeItemPath = treeItemPath;
                    return Task.CompletedTask;
                },
                SelectedPath = state.SelectedVisualElementTreeItemPath,
                Model        = state.ComponentRootElement
            }),

            When(state.LeftPanelSelectedTab == LeftPanelTab.Props || state.LeftPanelSelectedTab == LeftPanelTab.State, () => new FlexColumnCentered(SizeFull)
            {
                new Editor
                {
                    defaultLanguage          = "json",
                    valueBind                = () => state.JsonText,
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
            })
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

                (x == "M" && state.Preview.Width == 320) ||
                (x == "SM" && state.Preview.Width == 640) ||
                (x == "MD" && state.Preview.Width == 768) ||
                (x == "LG" && state.Preview.Width == 1024) ||
                (x == "XL" && state.Preview.Width == 1280) ||
                (x == "XXL" && state.Preview.Width == 1536)
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

            Width(state.Preview.Width),
            Height(state.Preview.Height * percent),
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

    Element PartProject()
    {
        return new FlexRowCentered(Gap(4))
        {
            new FlexRowCentered
            {
                new IconPlus() + Size(24) + Color(state.IsProjectSettingsPopupVisible ? Gray600 : Gray300) + Hover(Color(Gray600)),

                OnClick(_ =>
                {
                    state.IsProjectSettingsPopupVisible = !state.IsProjectSettingsPopupVisible;

                    return Task.CompletedTask;
                }),

                state.IsProjectSettingsPopupVisible ? PositionRelative : null,
                state.IsProjectSettingsPopupVisible ? new FlexColumn(PositionAbsolute, Top(24), Left(16), Zindex2)
                {
                    Background(White), Border(Solid(1, Theme.BorderColor)), BorderRadius(4), Padding(8),

                    Width(300),

                    new MagicInput
                    {
                        Placeholder = "New component name",
                        Name        = string.Empty,
                        Value       = string.Empty,
                        AutoFocus   = true,
                        OnChange = async (_, newValue) =>
                        {
                            if (newValue.HasNoValue())
                            {
                                this.FailNotification("Component name is empty.");

                                return;
                            }

                            if (GetAllComponentsInProject(state).Any(x => x.Name == newValue))
                            {
                                this.FailNotification("Has already same named component.");

                                return;
                            }

                            var newDbRecord = new ComponentEntity
                            {
                                Name      = newValue,
                                ProjectId = state.ProjectId,
                                UserName  = state.UserName
                            };

                            await DbOperation(db => db.InsertAsync(newDbRecord));

                            await OnComponentNameChanged(newValue);

                            state.IsProjectSettingsPopupVisible = false;
                        }
                    }
                } : null
            },

            new MagicInput
            {
                Name = string.Empty,

                Suggestions = GetProjectNames(state),
                Value       = GetAllProjects().FirstOrDefault(p => p.Id == state.ProjectId)?.Name,
                OnChange = async (_, projectName) =>
                {
                    var projectEntity = GetAllProjects().FirstOrDefault(x => x.Name == projectName);
                    if (projectEntity is null)
                    {
                        this.FailNotification("Project not found. @" + projectName);

                        return;
                    }

                    await ChangeSelectedProject(projectEntity.Id);
                },
                FitContent = true
            }
        };
    }

    Element PartRightPanel()
    {
        if (!state.SelectedVisualElementTreeItemPath.HasValue())
        {
            return new div();
        }

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
                    Suggestions = GetTagSuggestions(state),
                    OnChange = (_, newValue) =>
                    {
                        CurrentVisualElement.Tag = newValue;

                        return Task.CompletedTask;
                    }
                } + Width(6, 10)
            },

            new FlexRow(WidthFull, Gap(4))
            {
                new label { "Text", FontWeightBold, Width(4, 10), TextAlignRight },
                " : ",
                new MagicInput
                {
                    Name  = string.Empty,
                    Value = visualElementModel.Text,
                    OnChange = (_, newValue) =>
                    {
                        CurrentVisualElement.Text = newValue;

                        return Task.CompletedTask;
                    }
                } + Width(6, 10)
            },

            new FlexRow(WidthFull, AlignItemsCenter)
            {
                CreateIcon(Icon.remove, 32, state.SelectedStyleGroupIndex.HasValue ?
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
                            CreateIcon(Icon.remove, 28, state.SelectedPropertyIndexInStyleGroup.HasValue && state.SelectedStyleGroupIndex == styleGroupIndex ?
                                           [
                                               OnClick(_ =>
                                               {
                                                   CurrentStyleGroup.Items.Remove(CurrentStyleProperty);

                                                   state.SelectedPropertyIndexInStyleGroup = null;

                                                   return Task.CompletedTask;
                                               }),
                                               Hover(Color(Blue300))
                                           ] :
                                           [
                                               Color(Gray100),
                                               BorderColor(Gray100)
                                           ]),

                            new MagicInput
                            {
                                Name = styleGroupIndex.ToString(),
                                OnFocus = senderNameAsStyleGroupIndex =>
                                {
                                    state.SelectedStyleGroupIndex = int.Parse(senderNameAsStyleGroupIndex);

                                    return Task.CompletedTask;
                                },
                                Value             = styleGroup.Condition,
                                IsTextAlignCenter = true,
                                Suggestions       = GetStyleGroupConditionSuggestions(state)
                            } + FlexGrow(1),

                            CreateIcon(Icon.add, 28) + OnClick(_ =>
                            {
                                CurrentStyleGroup.Items.Add(new());

                                return Task.CompletedTask;
                            })
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
                                    Name    = new StyleInputLocation { PropertyIndexAtGroup = index, IsName = false, StyleGroupIndex = styleGroupIndex, IsValue = true },
                                    Value   = property.Value,
                                    OnFocus = On_CurrentPropertyIndexInStyle_Changed,
                                    OnChange = (_, newValue) =>
                                    {
                                        CurrentStyleProperty.Value = newValue;

                                        return Task.CompletedTask;
                                    },
                                    Placeholder = "red"
                                }
                            }
                        })
                    };
                })
            },

            new FlexRow(WidthFull, AlignItemsCenter)
            {
                CreateIcon(Icon.remove, 32, state.SelectedPropertyIndexInProps.HasValue ?
                               [
                                   OnClick(_ =>
                                   {
                                       CurrentVisualElement.Properties.RemoveAt(state.SelectedPropertyIndexInProps!.Value);

                                       state.SelectedPropertyIndexInProps = null;

                                       return Task.CompletedTask;
                                   }),
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
                            OnFocus = senderName =>
                            {
                                state.SelectedPropertyIndexInProps = ((PropInputLocation)senderName).Index;

                                return Task.CompletedTask;
                            },

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
                            Name  = new PropInputLocation { Index = index, IsName = false, IsValue = true },
                            Value = property.Value,
                            OnFocus = senderName =>
                            {
                                state.SelectedPropertyIndexInProps = ((PropInputLocation)senderName).Index;

                                return Task.CompletedTask;
                            },
                            OnChange = (_, newValue) =>
                            {
                                CurrentVisualElement.Properties[state.SelectedPropertyIndexInProps!.Value].Value = newValue;

                                return Task.CompletedTask;
                            },
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
                    if (state.Preview.Scale <= 20)
                    {
                        return Task.CompletedTask;
                    }

                    state.Preview.Scale -= 10;

                    return Task.CompletedTask;
                }),
                new IconMinus()
            },

            $"%{state.Preview.Scale}",
            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
            {
                OnClick(_ =>
                {
                    if (state.Preview.Scale >= 100)
                    {
                        return Task.CompletedTask;
                    }

                    state.Preview.Scale += 10;

                    return Task.CompletedTask;
                }),
                new IconPlus()
            }
        };
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

        state.SelectedStyleGroupIndex = styleGroups.Count - 1;

        return Task.CompletedTask;
    }

    Task StyleGroupRemoveClicked(MouseEvent e)
    {
        CurrentVisualElement.StyleGroups.Remove(CurrentStyleGroup);

        state.SelectedStyleGroupIndex           = null;
        state.SelectedPropertyIndexInStyleGroup = null;

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