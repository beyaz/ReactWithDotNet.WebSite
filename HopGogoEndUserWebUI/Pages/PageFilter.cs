namespace HopGogoEndUserWebUI.Pages;

sealed class PageFilter : Component
{
    protected override Element render()
    {
        return new div
        {
            new Header(),
            SpaceY(1),
            BigTitle(),
            new FlexRow(PaddingX(36), Gap(16))
            {
               new FlexColumn(Gap(24))
               {
                   new FlexRowCentered(Width(180), Height(46), Background("white"), BoxShadow(0, 2, 4, rgba(25, 33, 61, 0.25)), BorderRadius(100), Font(500, 18, "Euclid Circular B", "black"))
                   {
                       "Chat"
                   },
                   new FlexRowCentered(Width(180), Height(46), Background("white"), BoxShadow(0, 2, 4, rgba(25, 33, 61, 0.25)), BorderRadius(100), Font(500, 18, "Euclid Circular B", "black"))
                   {
                       "Explore with us"
                   },
                   new FlexRowCentered(Width(180), Height(46), Background("#0CBCC5"), BoxShadow(0, 2, 4, rgba(25, 33, 61, 0.25)), BorderRadius(100), Font(500, 18, "Euclid Circular B", White))
                   {
                       "Filter"
                   }
               },
               
               new SectionFilter()
            }
            
        };
    }

    Element BigTitle()
    {
        return new div(PaddingY(16), Font(500, 40, 56, "Euclid Circular B", "black"), PaddingX(36))
        {
            "Your adventure, your way – customize your perfect trip now!"
        };

    }

    
    class SectionFilter : Component
    {
        protected override Element render()
        {
            return new FlexColumn(SizeFull, MinHeight(500), Padding(24), Background("#F5F5F5"), BoxShadow(0, 2, 4, rgba(25, 33, 61, 0.16)), BorderRadius(16))
            {
                new FlexRow
                {
                    new AutoFilterBox()
                }
            };
        }
    }

}

record AutoFilterBoxState
{
    public bool IsSuggestionsVisible { get; init; }
}

class AutoFilterBox : Component<AutoFilterBoxState>
{
    public string SelectedValue { get; set; }
        
    protected override Element render()
    {
        return new FlexRowCentered(PaddingX(16), Height(50), Background(White), Border(1, "#6A6A6A", solid, 13), Font(400, 16, "Outfit", "black"))
        {
            "From: İstanbul, Türkey",

            Svg_Chevron_down_minor + MarginLeft(16),

            OnClick(OnClicked),

            PositionRelative,
            When(state.IsSuggestionsVisible, () =>
                     new FlexColumn(PositionAbsolute, Gap(16), WidthFull, Top(50), Left(0), Border(1, "#6A6A6A", solid, 13))
                     {
                         Background(White),
                         Enumerable.Range(1, 4).Select(i => new FlexRow(JustifyContentSpaceBetween)
                         {
                             OnClick(OnSuggestionItemClicked),
                             Hover(Background("#F0F2F5")),

                             Padding(16),

                             new FlexColumn(AlignItemsCenter)
                             {
                                 new div(Font(400, 16, "Outfit", "black"), WhiteSpaceNoWrap)
                                 {
                                     "Tekirdağ, Turkey " + i
                                 },
                                 new div(Font(400, 13, "Outfit", "#777373"))
                                 {
                                     "102 km from Istanbul"
                                 }
                             },
                             Svg_Plus + Size(24)
                         })
                     })
        };
    }

    [StopPropagation]
    Task OnSuggestionItemClicked(MouseEvent e)
    {
        state = state with { IsSuggestionsVisible = false };
        
        return Task.CompletedTask;
    }
    
    [StopPropagation]
    Task OnClicked(MouseEvent e)
    {
        state = state with { IsSuggestionsVisible = !state.IsSuggestionsVisible};
        
        return Task.CompletedTask;
    }
    
    
}