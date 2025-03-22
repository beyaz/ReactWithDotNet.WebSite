namespace ReactWithDotNet.WebSite.Components;

class PropertyEditor : Component<PropertyEditor.State>
{
    protected override Element render()
    {
        return new FlexRow(PositionRelative, Border(1,solid,Red))
        {
            
            new MagicInput(),
            new span{":"},
            new MagicInput(),
            new FlexRowCentered(Size(24), PositionAbsolute, Top(-16), Right(-16))
            {
                Color(Gray600),
                Hover(Color(Gray700)),
                Background(White),
                BorderRadius(24),
                Border(1, solid, Gray300),
                Hover(BorderColor(Gray500)),
                new IconClose()
            }
        };
    }

    internal class State
    {
        public string InitialValue { get; init; }

        public int? SelectedSuggestionOffset { get; set; }

        public bool ShowSuggestions { get; set; }

        public string Value { get; set; }
    }

    class IconClose : PureComponent
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
}

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
        return new FlexColumn(WidthFull)
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
                valueBind                = () => state.Value,
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
        if (state.Value == "4")
        {
            return ["4","8","12","16"];
        }
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
        if (state.ShowSuggestions is false)
        {
            state.ShowSuggestions = true;
            
            if (state.SelectedSuggestionOffset >= 0)
            {
                return Task.CompletedTask;
            }
        }

        var suggestions = GetCurrentSuggestions();
        if (suggestions.Count == 0)
        {
            state.ShowSuggestions = false;
            
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
            
            if (state.SelectedSuggestionOffset < 0 )
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