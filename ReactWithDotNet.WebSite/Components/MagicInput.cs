namespace ReactWithDotNet.WebSite.Components;

sealed class MagicInput : Component<MagicInput.State>
{
    [CustomEvent]
    public Func<string, Task> OnChange { get; init; }

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
        return new FlexColumn
        {
            new style
            {
                """
                input:focus { outline:none; }
                """
            },
            new input
            {
                type                     = "text",
                valueBind                = () => Value,
                valueBindDebounceTimeout = 700,
                valueBindDebounceHandler = OnTypingFinished,
                onBlur                   = OnBlur,
                onKeyDown                = OnKeyDown,
                style =
                {
                    Background(White),
                    Border(Solid(0.1, "#bcc4e3")),
                    BorderRadius(3),
                    PaddingLeft(3),
                    PaddingTopBottom(4),
                    FlexGrow(1), FontFamily("Arial"), FontSize12,
                    Color(rgb(0, 6, 36)),
                    LetterSpacing(0.3)
                },
                autoFocus = true
            },
            Suggestions
        };
    }

    IReadOnlyList<string> GetCurrentSuggestions()
    {
        return ["A", "B", "C"];
        //return AllSuggestions.Where(x => x.Contains(Value + "", StringComparison.OrdinalIgnoreCase))
        //  .Take(5).ToList();
    }

    void InitializeState()
    {
        state = new()
        {
            InitialValue = Value,
            Value        = Value
        };
    }

    Task OnBlur(FocusEvent e)
    {
        state.ShowSuggestions = false;

        return Task.CompletedTask;
    }

    [KeyboardEventCallOnly("ArrowDown", "ArrowUp", "Enter")]
    Task OnKeyDown(KeyboardEvent e)
    {
        if (e.key == "ArrowDown")
        {
            state.SelectedSuggestionOffset ??= -1;

            state.SelectedSuggestionOffset++;
        }

        if (e.key == "ArrowUp")
        {
            state.SelectedSuggestionOffset ??= 1;

            state.SelectedSuggestionOffset--;
        }

        if (e.key == "Enter")
        {
            if (state.SelectedSuggestionOffset is null)
            {
                return Task.CompletedTask;
            }

            state.ShowSuggestions = false;

            if (GetCurrentSuggestions().Count > state.SelectedSuggestionOffset.Value)
            {
                state.Value = GetCurrentSuggestions()[state.SelectedSuggestionOffset.Value];

                DispatchEvent(OnChange, [state.Value]);
            }
        }

        return Task.CompletedTask;
    }

    Task OnTypingFinished()
    {
        state.ShowSuggestions = true;

        state.SelectedSuggestionOffset = null;

        return Task.CompletedTask;
    }

    Element Suggestions()
    {
        if (state.ShowSuggestions is false)
        {
            return null;
        }

        var suggestionItemList = GetCurrentSuggestions();

        if (suggestionItemList.Count == 0)
        {
            return null;
        }

        return new FlexColumn(PositionRelative, SizeFull)
        {
            new FlexColumn(PositionAbsolute, SizeFull, HeightAuto, Background("white"), BoxShadow(0, 6, 6, 0, rgba(22, 45, 61, .06)), Padding(5), BorderRadius(5))
            {
                suggestionItemList.Select(ToOption)
            }
        };

        Element ToOption(string code, int index)
        {
            return new div(BorderRadius(4))
            {
                code,
                PaddingLeft(5),
                Color(rgb(0, 6, 36)),

                index == state.SelectedSuggestionOffset ? Color("#495cef") + Background("#e7eaff") : null
            };
        }
    }

    internal class State
    {
        public string InitialValue { get; init; }

        public int? SelectedSuggestionOffset { get; set; }

        public bool ShowSuggestions { get; set; }

        public string Value { get; set; }
    }
}