namespace HopGogoEndUserWebUI;

record AutoFilterBoxState<TRecord>
{
    public bool IsSuggestionsVisible { get; init; }

    public int SelectedIndex { get; set; } = -1;

    public IReadOnlyList<TRecord> Suggestions { get; init; } = [];

    public string UserEnteredText { get; init; }

    internal TRecord SelectedRecord => SelectedIndex >= 0 ? Suggestions[SelectedIndex] : default;
}

abstract class AutoFilterBox<TRecord> : Component<AutoFilterBoxState<TRecord>>
{
    public string Placeholder { get; init; }

    protected override Task constructor()
    {
        state = new()
        {
            Suggestions = GetDefaultSuggestions()
        };

        return Task.CompletedTask;
    }

    protected abstract IReadOnlyList<TRecord> GetDefaultSuggestions();

    protected abstract IReadOnlyList<TRecord> GetItemsSource();

    protected abstract string GetSelectedValueText(TRecord record);

    protected override Element render()
    {
        var rightIcon = Svg_Chevron_down_minor;
        if (state.IsSuggestionsVisible)
        {
            rightIcon = Svg_Chevron_up_minor;
        }

        var inputElement = new input
        {
            type                     = "text",
            valueBind                = () => state.UserEnteredText,
            valueBindDebounceTimeout = 600,
            valueBindDebounceHandler = OnSearchTypeFinished,
            style                    = { Border(none), OutlineNone },
            autoFocus                = true,
            onKeyDown                = SearchTextBoxOnKeyDown
        };
        if (state.IsSuggestionsVisible is false)
        {
            inputElement = null;
        }

        var visibleText = state.SelectedRecord == null ? Placeholder : GetSelectedValueText(state.SelectedRecord);
        if (state.IsSuggestionsVisible)
        {
            visibleText = null;
        }

        return new FlexRowCentered(PaddingX(16), Height(50), Background(White), Border(1, "#6A6A6A", solid, 13), Font(400, 16, "Outfit", "black"))
        {
            visibleText,

            inputElement,

            rightIcon + MarginLeft(16),

            OnClick(OnClicked),

            PositionRelative,
            When(state.IsSuggestionsVisible, () =>
                     new FlexColumn(PositionAbsolute, Gap(16), WidthFull, Top(50), Left(0), Border(1, "#6A6A6A", solid, 13))
                     {
                         Background(White),

                         state.Suggestions.Select((item, index) => new FlexRow(JustifyContentSpaceBetween)
                         {
                             OnClick(OnSuggestionItemClicked),
                             Hover(Background("#F0F2F5")),

                             Padding(16),

                             RenderItem(item),

                             Svg_Plus + Size(24),

                             When(index == state.SelectedIndex, Background(Gray300)),

                             When(index == 0, BorderTopLeftRadius(13), BorderTopRightRadius(13)),

                             Data("index", index)
                         })
                     })
        };
    }

    protected abstract Element RenderItem(TRecord record);

    [StopPropagation]
    Task OnClicked(MouseEvent e)
    {
        state = state with { IsSuggestionsVisible = !state.IsSuggestionsVisible };

        return Task.CompletedTask;
    }

    Task OnSearchTypeFinished()
    {
        state = state with
        {
            Suggestions = GetItemsSource()
        };

        return Task.CompletedTask;
    }

    [StopPropagation]
    Task OnSuggestionItemClicked(MouseEvent e)
    {
        state = state with
        {
            IsSuggestionsVisible = false,

            SelectedIndex = int.Parse(e.currentTarget.data["index"])
        };

        return Task.CompletedTask;
    }

    [KeyboardEventCallOnly("Enter", "ArrowDown", "ArrowUp")]
    Task SearchTextBoxOnKeyDown(KeyboardEvent e)
    {
        if (state.Suggestions.Count == 0)
        {
            return Task.CompletedTask;
        }

        if (e.key == "ArrowDown")
        {
            state.SelectedIndex++;
        }

        if (e.key == "ArrowUp")
        {
            state.SelectedIndex--;
        }

        if (state.SelectedIndex >= state.Suggestions.Count)
        {
            state.SelectedIndex = state.Suggestions.Count - 1;
        }

        if (state.SelectedIndex < 0)
        {
            state.SelectedIndex = 0;
        }

        if (e.key == "Enter")
        {
            state = state with
            {
                UserEnteredText = GetSelectedValueText(state.SelectedRecord),
                IsSuggestionsVisible = false
            };
        }

        return Task.CompletedTask;
    }
}