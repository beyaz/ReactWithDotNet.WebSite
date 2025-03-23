using static ReactWithDotNet.WebSite.Components2.Extensions;

namespace ReactWithDotNet.WebSite.Components2;

sealed record PropertyModel
{
    public string Condition { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }
}

sealed record VisualElementModel
{
    public List<VisualElementModel> Children { get; set; }

    public List<PropertyModel> Properties { get; set; }

    public List<PropertyModel> StyleAttributes { get; set; }
    public string Tag { get; set; }

    public string Text { get; set; }

    internal bool HasChild => Children?.Count > 0;
}

class Demo : Component
{
    protected override Element render()
    {
        return new VisualElementTreeViewer
        {
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
        };
    }
}

sealed class VisualElementTreeViewer : Component<VisualElementTreeViewer.State>
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
        return new div(MarginLeftRight(3), OverflowYScroll, CursorPointer, Padding(5), Border(Solid(1, rgb(217, 217, 217))), BorderRadius(3))
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
        var returnList = new List<Element>
        {
            new FlexRow(AlignItemsCenter, PaddingLeft(indent * 16), Id(path), OnClick(OnTreeItemClicked))
            {
                arrangeBackground,
                new div { Text(node.Tag), MarginLeft(5), FontSize13 }
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

        void arrangeBackground(HtmlElement el)
        {
            var isSelected = state.SelectedPath == path;

            if (isSelected)
            {
                el += BackgroundImage(linear_gradient(90, rgb(136, 195, 242), rgb(242, 246, 249))) + BorderRadius(3);
            }
            else
            {
                el += Hover(BackgroundImage(linear_gradient(90, rgb(190, 220, 244), rgb(242, 246, 249))) + BorderRadius(3));
            }

            el.onClick = OnTreeItemClicked;
        }
    }

    internal class State
    {
        public VisualElementModel InitialModel { get; init; }

        public string InitialSelectedPath { get; set; }
        public VisualElementModel Model { get; init; }

        public string SelectedPath { get; set; }
    }
}

sealed record PropertyInfo
{
    public string Name { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }
}

static class Extensions
{
    public static IReadOnlyList<PropertyInfo> StyleProperties = new List<PropertyInfo>
    {
        new()
        {
            Name        = "width",
            Suggestions = ["auto", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "100%", "75%", "50%", "25%"]
        },
        new()
        {
            Name        = "height",
            Suggestions = ["auto", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "100%", "50px", "calc(100vh - 10px)"]
        },
        new()
        {
            Name        = "max-width",
            Suggestions = ["none", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "100%", "500px"]
        },
        new()
        {
            Name        = "max-height",
            Suggestions = ["none", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "100vh", "400px"]
        },
        new()
        {
            Name        = "min-width",
            Suggestions = ["0", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "200px"]
        },
        new()
        {
            Name        = "min-height",
            Suggestions = ["0", "fit-content", "max-content", "min-content", "inherit", "initial", "unset", "150px"]
        },
        new()
        {
            Name        = "display",
            Suggestions = ["block", "inline", "inline-block", "flex", "grid", "none", "contents", "table", "table-row", "table-cell", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "position",
            Suggestions = ["static", "relative", "absolute", "fixed", "sticky", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "top",
            Suggestions = ["auto", "inherit", "initial", "unset", "50px", "10%"]
        },
        new()
        {
            Name        = "bottom",
            Suggestions = ["auto", "inherit", "initial", "unset", "20px", "5%"]
        },
        new()
        {
            Name        = "left",
            Suggestions = ["auto", "inherit", "initial", "unset", "30px", "10%"]
        },
        new()
        {
            Name        = "right",
            Suggestions = ["auto", "inherit", "initial", "unset", "40px", "15%"]
        },
        new()
        {
            Name        = "z-index",
            Suggestions = ["auto", "inherit", "initial", "unset", "10", "1000"]
        },
        new()
        {
            Name        = "flex-direction",
            Suggestions = ["row", "row-reverse", "column", "column-reverse", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "justify-content",
            Suggestions = ["flex-start", "flex-end", "center", "space-between", "space-around", "space-evenly", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "align-items",
            Suggestions = ["stretch", "flex-start", "flex-end", "center", "baseline", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "gap",
            Suggestions = ["normal", "inherit", "initial", "unset", "10px", "1rem"]
        },
        new()
        {
            Name        = "overflow",
            Suggestions = ["visible", "hidden", "scroll", "auto", "clip", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "visibility",
            Suggestions = ["visible", "hidden", "collapse", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "opacity",
            Suggestions = ["0", "0.1", "0.5", "1", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "cursor",
            Suggestions = ["auto", "default", "pointer", "wait", "text", "move", "not-allowed", "crosshair", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "background",
            Suggestions = ["none", "inherit", "initial", "unset", "red", "url('image.jpg')", "linear-gradient(to right, red, blue)"]
        },
        new()
        {
            Name        = "border",
            Suggestions = ["none", "inherit", "initial", "unset", "1px solid black", "2px dashed red"]
        },
        new()
        {
            Name        = "border-radius",
            Suggestions = ["0", "inherit", "initial", "unset", "10px", "50%"]
        },
        new()
        {
            Name        = "box-shadow",
            Suggestions = ["none", "inherit", "initial", "unset", "2px 2px 5px gray"]
        },
        new()
        {
            Name        = "color",
            Suggestions = ["inherit", "initial", "unset", "black", "red", "blue", "#ff0000"]
        },
        new()
        {
            Name        = "font-size",
            Suggestions = ["small", "medium", "large", "inherit", "initial", "unset", "16px", "1.2rem"]
        },
        new()
        {
            Name        = "font-weight",
            Suggestions = ["normal", "bold", "bolder", "lighter", "100", "200", "300", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "line-height",
            Suggestions = ["normal", "inherit", "initial", "unset", "1.5", "2"]
        },
        new()
        {
            Name        = "text-align",
            Suggestions = ["left", "right", "center", "justify", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "text-decoration",
            Suggestions = ["none", "underline", "overline", "line-through", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "white-space",
            Suggestions = ["normal", "nowrap", "pre", "pre-wrap", "pre-line", "inherit", "initial", "unset"]
        },
        new()
        {
            Name        = "pointer-events",
            Suggestions = ["auto", "none", "inherit", "initial", "unset"]
        }
    };

    public static double CalculateTextWidth(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 50;
        }

        var textLegth = text.Length;

        if (textLegth == 1)
        {
            textLegth = 2;
        }

        return textLegth * 7.8;
    }

    public static Element EditorFont()
    {
        return new Fragment
        {
            new link { href = "https://fonts.googleapis.com", rel = "preconnect" },

            new link { href = "https://fonts.gstatic.com", rel = "preconnect", crossOrigin = "true" },

            new link { href = "https://fonts.googleapis.com/css2?family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet" }
        };
    }

    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static PropertyModel TryParsePropertyValue(string nameValueCombined)
    {
        if (string.IsNullOrWhiteSpace(nameValueCombined))
        {
            return null;
        }

        var colonIndex = nameValueCombined.IndexOf(':');
        if (colonIndex < 0)
        {
            return new()
            {
                Name  = nameValueCombined,
                Value = null
            };
        }

        var name = nameValueCombined[..colonIndex];

        var value = nameValueCombined[(colonIndex + 1)..];

        return new()
        {
            Name  = name,
            Value = value
        };
    }
}

class StyleEditor : Component<StyleEditor.State>
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
            EditorFont,
            FontFamily("'Wix Madefor Text', sans-serif"),
            FontSize(14),
            LetterSpacingNormal,
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

sealed class IconClose : PureComponent
{
    protected override Element render()
    {
        return new svg(Fill("currentColor"), ViewBox(0, 0, 18, 18))
        {
            new path
            {
                d = "M8.44 9.5L6 7.06A.75.75 0 1 1 7.06 6L9.5 8.44 11.94 6A.75.75 0 0 1 13 7.06L10.56 9.5 13 11.94A.75.75 0 0 1 11.94 13L9.5 10.56 7.06 13A.75.75 0 0 1 6 11.94L8.44 9.5z"
            }
        };
    }
}

sealed class IconChecked : PureComponent
{
    protected override Element render()
    {
        return new svg(Fill("currentColor"), ViewBox(0, 0, 14, 14))
        {
            new path
            {
                d    = "M4.86199 11.5948C4.78717 11.5923 4.71366 11.5745 4.64596 11.5426C4.57826 11.5107 4.51779 11.4652 4.46827 11.4091L0.753985 7.69483C0.683167 7.64891 0.623706 7.58751 0.580092 7.51525C0.536478 7.44299 0.509851 7.36177 0.502221 7.27771C0.49459 7.19366 0.506156 7.10897 0.536046 7.03004C0.565935 6.95111 0.613367 6.88 0.674759 6.82208C0.736151 6.76416 0.8099 6.72095 0.890436 6.69571C0.970973 6.67046 1.05619 6.66385 1.13966 6.67635C1.22313 6.68886 1.30266 6.72017 1.37226 6.76792C1.44186 6.81567 1.4997 6.8786 1.54141 6.95197L4.86199 10.2503L12.6397 2.49483C12.7444 2.42694 12.8689 2.39617 12.9932 2.40745C13.1174 2.41873 13.2343 2.47141 13.3251 2.55705C13.4159 2.64268 13.4753 2.75632 13.4938 2.87973C13.5123 3.00315 13.4888 3.1292 13.4271 3.23768L5.2557 11.4091C5.20618 11.4652 5.14571 11.5107 5.07801 11.5426C5.01031 11.5745 4.9368 11.5923 4.86199 11.5948Z",
                fill = "currentColor"
            }
        };
    }
}

sealed class MagicInput : Component<MagicInput.State>
{
    [CustomEvent]
    public Func<string, Task> OnChange { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }

    public string Value { get; init; }

    protected override Task constructor()
    {
        InitializeState();

        return Task.CompletedTask;
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        if (Value != state.InitialValue)
        {
            InitializeState();
        }

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexColumnCentered
        {
            new input
            {
                type                     = "text",
                valueBind                = () => state.Value,
                valueBindDebounceTimeout = 700,
                valueBindDebounceHandler = OnTypingFinished,
                onKeyDown                = OnKeyDown,
                onClick                  = OnInputClicked,
                style =
                {
                    OutlineNone,
                    BorderNone,
                    Appearance(none),
                    PaddingTopBottom(4),
                    Color(rgb(0, 6, 36)),
                    Height(24),
                    Width(CalculateTextWidth(state.Value)),
                    FontFamily("'Wix Madefor Text', sans-serif"),
                    FontSize14,
                    LetterSpacingNormal
                },
                autoFocus = true
            },
            ViewSuggestions
        };
    }

    void InitializeState()
    {
        state = new()
        {
            InitialValue = Value,

            Value = Value,

            FilteredSuggestions = Suggestions ?? []
        };
    }

    Task OnInputClicked(MouseEvent e)
    {
        state.ShowSuggestions = !state.ShowSuggestions;

        return Task.CompletedTask;
    }

    [KeyboardEventCallOnly("ArrowDown", "ArrowUp", "Enter")]
    Task OnKeyDown(KeyboardEvent e)
    {
        if (state.ShowSuggestions is false)
        {
            state.ShowSuggestions = true;

            if (state.SelectedSuggestionOffset >= 0)
            {
                return Task.CompletedTask;
            }
        }

        var suggestions = state.FilteredSuggestions ?? [];
        if (suggestions.Count == 0)
        {
            state.ShowSuggestions = false;

            if (e.key == "Enter")
            {
                if (state.Value?.Length > 0)
                {
                    DispatchEvent(OnChange, [state.Value]);

                    return Task.CompletedTask;
                }

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        if (e.key == "ArrowDown")
        {
            state.SelectedSuggestionOffset ??= -1;

            state.SelectedSuggestionOffset++;

            if (state.SelectedSuggestionOffset >= suggestions.Count)
            {
                state.SelectedSuggestionOffset = suggestions.Count - 1;
            }
        }

        if (e.key == "ArrowUp")
        {
            state.SelectedSuggestionOffset ??= 1;

            state.SelectedSuggestionOffset--;

            if (state.SelectedSuggestionOffset < 0)
            {
                state.SelectedSuggestionOffset = 0;
            }
        }

        if (e.key == "Enter")
        {
            state.ShowSuggestions = false;

            if (state.SelectedSuggestionOffset is null)
            {
                return Task.CompletedTask;
            }

            if (suggestions.Count > state.SelectedSuggestionOffset.Value)
            {
                state.Value = suggestions[state.SelectedSuggestionOffset.Value];

                DispatchEvent(OnChange, [state.Value]);
            }
        }

        return Task.CompletedTask;
    }

    [StopPropagation]
    Task OnSuggestionItemClicked(MouseEvent e)
    {
        state.ShowSuggestions = false;

        state.SelectedSuggestionOffset = int.Parse(e.target.data["INDEX"]);

        state.Value = state.FilteredSuggestions[state.SelectedSuggestionOffset.Value];

        DispatchEvent(OnChange, [state.Value]);

        return Task.CompletedTask;
    }

    Task OnTypingFinished()
    {
        state.ShowSuggestions = true;

        state.SelectedSuggestionOffset = null;

        state.FilteredSuggestions = Suggestions.Where(x => x.Contains((state.Value + string.Empty).Trim(), StringComparison.OrdinalIgnoreCase))
            .Take(5).ToList();

        return Task.CompletedTask;
    }

    Element ViewSuggestions()
    {
        if (state.ShowSuggestions is false)
        {
            return null;
        }

        var suggestions = state.FilteredSuggestions ?? [];

        if (suggestions.Count == 0)
        {
            return null;
        }

        return new FlexColumn(PositionRelative, SizeFull)
        {
            Zindex3,
            new FlexColumn(PositionAbsolute, Top(4), HeightAuto, Background(White), BoxShadow(0, 6, 6, 0, rgba(22, 45, 61, .06)), Padding(5), BorderRadius(5))
            {
                Zindex4,
                suggestions.Select(ToOption)
            }
        };

        Element ToOption(string code, int index)
        {
            return new div(BorderRadius(4), OnClick(OnSuggestionItemClicked))
            {
                Data("INDEX", index),

                code,
                PaddingLeft(5),
                Color(rgb(0, 6, 36)),
                WhiteSpaceNoWrap,
                CursorDefault,

                Hover(Background(Gray100)),

                index == state.SelectedSuggestionOffset ? Color("#495cef") + Background("#e7eaff") : null
            };
        }
    }

    internal class State
    {
        public IReadOnlyList<string> FilteredSuggestions { get; set; }

        public string InitialValue { get; init; }

        public int? SelectedSuggestionOffset { get; set; }

        public bool ShowSuggestions { get; set; }

        public string Value { get; set; }
    }
}