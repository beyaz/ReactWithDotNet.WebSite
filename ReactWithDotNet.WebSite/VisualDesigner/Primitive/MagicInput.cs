namespace ReactWithDotNet.VisualDesigner.Primitive;

sealed class MagicInput : Component<MagicInput.State>
{
    [CustomEvent]
    public Func<string, Task> OnChange { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; } = [];

    public string Value { get; init; }

    public bool FitContent { get; init; }

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
        return new FlexColumnCentered(FitContent is false ? WidthFull : null)
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
                    FitContent ? Width(CalculateTextWidth(state.Value)) : WidthFull,
                    
                    EditorFont()
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