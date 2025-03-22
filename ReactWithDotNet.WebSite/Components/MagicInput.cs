namespace ReactWithDotNet.WebSite.Components;

class StyleEditor : Component<StyleEditor.State>
{
    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, FlexWrap, Border(1, solid, Gray300), BorderRadius(4), Padding(5,10), Gap(4), Background(White))
        {
            new FlexRowCentered(BorderRadius(16), Padding(4,8), Background(Gray100), Gap(4))
            {
                "padding: 7"
            },
            new FlexRow
            {
                "abc2"
            },
            new PropertyEditor()
        };
    }

    internal class State
    {
        public string InitialValue { get; init; }

        public int? SelectedSuggestionOffset { get; set; }

        public bool ShowSuggestions { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public bool FocusKey { get; set; }
        public bool FocusValue { get; set; }
    }
}

class PropertyEditor : Component<PropertyEditor.State>
{
    protected override Element render()
    {
        return new FlexRow(PositionRelative)
        {
            new MagicInput{ Suggestions = ["gap","padding", "padding-top","padding-bottom"],Focus = state.FocusKey, Value = state.Key, OnChange = OnKeyChanged},
            new span{":"},
            // new MagicInput{ Focus = state.FocusValue},
            new FlexRowCentered(Size(24), PositionAbsolute, Top(-16), Right(-16))
            {
                Color(Gray600),
                Hover(Color(Gray700)),
                Background(White),
                BorderRadius(24),
                Border(1, solid, Gray300),
                Hover(BorderColor(Gray500)),
                new IconClose()
            },
            
            new span{ "Key:" +state.Key},
            new span{ "Val:" +state.Value},
        };
    }

    
    protected override Task constructor()
    {
        state = new()
        {
            FocusKey = true
        };
        
        return Task.CompletedTask;
    }

    Task OnKeyChanged(string newValue)
    {
        state.Key = newValue;
        
        state.FocusKey = false;
        state.FocusValue = true;

        return Task.CompletedTask;
    }

    internal class State
    {
        public string InitialValue { get; init; }

        public int? SelectedSuggestionOffset { get; set; }

        public bool ShowSuggestions { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public bool FocusKey { get; set; }
        public bool FocusValue { get; set; }
    }

   
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
sealed class MagicInput : Component<MagicInput.State>
{
    [CustomEvent]
    public Func<string, Task> OnChange { get; init; }

    public string Value { get; init; }

    public bool Focus { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }

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
                    //Background(White),
                    //Border(Solid(0.1, "#bcc4e3")),
                    //BorderRadius(3),
                    //PaddingLeft(3),
                    OutlineNone,
                    BorderNone,
                    Appearance(none),
                    PaddingTopBottom(4),
                    FlexGrow(1), FontFamily("Arial"), FontSize12,
                    Color(rgb(0, 6, 36)),
                    LetterSpacing(0.3)
                },
                autoFocus = Focus
            },
            ViewSuggestions
        };
    }

    IReadOnlyList<string> GetCurrentSuggestions()
    {
        return Suggestions;
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

    Element ViewSuggestions()
    {
        if (state.ShowSuggestions is false)
        {
            return null;
        }
        
        if (Suggestions.Count == 0)
        {
            return null;
        }

        return new FlexColumn(PositionRelative, SizeFull)
        {
            new FlexColumn(PositionAbsolute, SizeFull, HeightAuto, Background("white"), BoxShadow(0, 6, 6, 0, rgba(22, 45, 61, .06)), Padding(5), BorderRadius(5))
            {
                Suggestions.Select(ToOption)
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