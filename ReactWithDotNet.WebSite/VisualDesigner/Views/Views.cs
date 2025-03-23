global using static ReactWithDotNet.VisualDesigner.Extensions;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class VisualElementTreeView : Component<VisualElementTreeView.State>
{
    public VisualElementModel Model { get; init; }

    public string SelectedPath { get; init; }

    [CustomEvent]
    public Func<string, Task> SelectionChanged { get; set; }

    protected override Task constructor()
    {
        InitializeState();

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new div(CursorPointer, Padding(5), Border(Solid(1, rgb(217, 217, 217))), BorderRadius(3))
        {
            ToVisual(state.Model, 0, "0"),
            WidthFull, HeightFull
        };
    }

    void InitializeState()
    {
        state = new()
        {
            Model               = Model,
            SelectedPath        = SelectedPath,
            InitialModel        = Model,
            InitialSelectedPath = SelectedPath
        };
    }

    Task OnTreeItemClicked(MouseEvent e)
    {
        DispatchEvent(SelectionChanged, [e.currentTarget.id]);

        return Task.CompletedTask;
    }

    IReadOnlyList<Element> ToVisual(VisualElementModel node, int indent, string path)
    {
        var isSelected = state.SelectedPath == path;

        var returnList = new List<Element>
        {
            new FlexRow(AlignItemsCenter, PaddingLeft(indent * 16), Id(path), OnClick(OnTreeItemClicked))
            {
                new div { Text(node.Tag), MarginLeft(5), FontSize13 },

                isSelected ? BackgroundImage(linear_gradient(90, rgb(136, 195, 242), rgb(242, 246, 249))) + BorderRadius(3) : null,

                !isSelected ? Hover(BackgroundImage(linear_gradient(90, rgb(190, 220, 244), rgb(242, 246, 249))) + BorderRadius(3)) : null
            }
        };

        if (node.HasChild is false)
        {
            return returnList;
        }

        for (var i = 0; i < node.Children.Count; i++)
        {
            var child = node.Children[i];

            returnList.AddRange(ToVisual(child, indent + 1, $"{path},{i}"));
        }

        return returnList;
    }

    internal class State
    {
        public VisualElementModel InitialModel { get; init; }

        public string InitialSelectedPath { get; set; }

        public VisualElementModel Model { get; init; }

        public string SelectedPath { get; set; }
    }
}

sealed class StyleEditor : Component<StyleEditor.State>
{
    public IReadOnlyList<PropertyModel> Value { get; init; } = [new() { Name = "gap", Value = "5" }];

    protected override Task constructor()
    {
        state = new()
        {
            InitialValue = Value ?? [],

            Value = (Value ?? []).ToList()
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, FlexWrap, Border(1, solid, Gray300), BorderRadius(4), Padding(5, 10), Gap(16), Background(White))
        {
            EditorFontLinks,
            EditorFont(),
            
            state.Value.Select(x => new PropertyEditor
            {
                Model               = x,
                PropertySuggestions = StyleProperties
            }),
            new PropertyEditor
            {
                PropertySuggestions = StyleProperties,
                OnChange            = OnAddNewItem
            }
        };
    }

    Task OnAddNewItem(PropertyModel newModel)
    {
        state.Value.Add(newModel);

        return Task.CompletedTask;
    }

    internal class State
    {
        public IReadOnlyList<PropertyModel> InitialValue { get; init; }

        public List<PropertyModel> Value { get; init; }
    }
}

sealed class PropertyEditor : Component<PropertyEditor.State>
{
    public PropertyModel Model { get; init; }

    [CustomEvent]
    public Func<PropertyModel, Task> OnChange { get; init; }

    public IReadOnlyList<PropertyInfo> PropertySuggestions { get; init; } = StyleProperties;

    protected override Task constructor()
    {
        InitializeState();

        return Task.CompletedTask;
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        if (Model is not null && state.InitialModel is not null && Model.Name != state.InitialModel.Name)
        {
            InitializeState();

            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var nameNotSelectedYet = string.IsNullOrWhiteSpace(state.Model.Name);
        if (nameNotSelectedYet)
        {
            List<string> suggestions = [];
            foreach (var propertyInfo in PropertySuggestions)
            {
                suggestions.Add(propertyInfo.Name);

                foreach (var suggestion in propertyInfo.Suggestions)
                {
                    suggestions.Add(propertyInfo.Name + ": " + suggestion);
                }
            }

            return new FlexRowCentered(Color(Gray600), WidthFitContent, BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Background(White), Gap(4))
            {
                new MagicInput { Suggestions = suggestions, Value = null, OnChange = OnFirstValueChange }
            };
        }

        if (state.IsEditMode is false)
        {
            return new FlexRowCentered(Color(Gray600), WidthFitContent, BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Gap(4))
            {
                new span(FontWeight600) { state.Model.Name },
                new span { ":" },
                new span(Width(CalculateTextWidth(state.Model.Value)), LetterSpacingNormal) { state.Model.Value },

                When(state.Model.Condition.HasValue(), () => new FlexRowCentered(Gap(4))
                {
                    new span(FontWeight600) { "on:" },
                    new span { state.Model.Condition }
                }),

                OnMouseEnter(OnMouseEnterHandler)
            };
        }

        {
            List<string> suggestions = [];
            {
                foreach (var propertyInfo in PropertySuggestions)
                {
                    if (propertyInfo.Name != state.Model.Name)
                    {
                        continue;
                    }

                    foreach (var suggestion in propertyInfo.Suggestions)
                    {
                        suggestions.Add(suggestion);
                    }
                }
            }

            var closeIcon = new FlexRowCentered(Size(24), PositionAbsolute, Padding(4), Top(0), Right(0))
            {
                Color(Gray600),
                Hover(Color(Gray700)),
                Background(White),
                BorderTopRightRadius(16),
                BorderBottomLeftRadius(8),

                BorderLeft(1, solid, Gray300),
                BorderBottom(1, solid, Gray300),

                Hover(BorderColor(Gray500), Background(Gray300)),
                new IconClose()
            };

            var isActiveIcon = new FlexRowCentered(Size(24), Padding(4), PositionAbsolute, Top(-4), Left(-16))
            {
                Color(Gray600),
                Hover(Color(Gray700)),
                Background(White),
                BorderRadius(24),
                Border(1, solid, Gray300),
                Hover(BorderColor(Gray500)),
                new IconChecked()
            };

            return new FlexRowCentered(Color(Gray600), WidthFitContent, BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Background(White), Gap(4))
            {
                new FlexRowCentered(Gap(4))
                {
                    new span(FontWeight600) { state.Model.Name },

                    new span { ":" },

                    new MagicInput { Value = state.Model.Value, Suggestions = suggestions, OnChange = OnPropertyValueChanged }
                },

                new FlexRowCentered(Gap(4))
                {
                    new span(FontWeight600) { "on:" },

                    new MagicInput
                    {
                        Value       = state.Model.Condition,
                        Suggestions = ["state.IsEndUser", "prop.HasNewValue", "prop.IsNewRecord"],
                        OnChange    = OnConditionChanged
                    }
                },

                OnMouseLeave(OnMouseLeaveHandler),

                PositionRelative,
                closeIcon,
                isActiveIcon
            };
        }
    }

    Task CloseEditMode()
    {
        state.IsEditMode = false;

        return Task.CompletedTask;
    }

    void InitializeState()
    {
        state = new()
        {
            Model        = Model ?? new PropertyModel(),
            InitialModel = Model
        };
    }

    Task OnConditionChanged(string newValue)
    {
        state.Model.Condition = newValue;

        DispatchEvent(OnChange, [state.Model]);

        return Task.CompletedTask;
    }

    Task OnFirstValueChange(string newValue)
    {
        var model = state.Model = TryParsePropertyValue(newValue);

        if (model is null || model.Value is null)
        {
            return Task.CompletedTask;
        }

        DispatchEvent(OnChange, [state.Model]);

        return Task.CompletedTask;
    }

    Task OnMouseEnterHandler(MouseEvent e)
    {
        state.IsEditMode = true;

        return Task.CompletedTask;
    }

    Task OnMouseLeaveHandler(MouseEvent e)
    {
        Client.GotoMethod(1000, CloseEditMode);

        return Task.CompletedTask;
    }

    Task OnPropertyValueChanged(string newValue)
    {
        state.Model.Value = newValue;

        DispatchEvent(OnChange, [state.Model]);

        return Task.CompletedTask;
    }

    internal class State
    {
        public PropertyModel InitialModel { get; init; }

        public bool IsDeleteButtonVisible { get; set; }

        public bool IsEditMode { get; set; }

        public PropertyModel Model { get; set; }
    }
}

sealed class DemoVisualElementTreeViewer : Component<DemoVisualElementTreeViewer.State>
{
    protected override Element render()
    {
        return new FlexColumn
        {
            new span { state.SelectedPath },
            new VisualElementTreeView
            {
                SelectionChanged = SelectionChanged,
                SelectedPath     = state.SelectedPath,
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

    Task SelectionChanged(string selectedPath)
    {
        state.SelectedPath = selectedPath;

        return Task.CompletedTask;
    }

    internal class State
    {
        public string SelectedPath { get; set; }
    }
}