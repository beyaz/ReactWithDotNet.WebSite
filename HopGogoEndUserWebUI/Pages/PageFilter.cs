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
                new FlexRow(Gap(24))
                {
                    new AirportSelection
                    {
                        Placeholder = "Select from airport",
                        Prefix = "From"
                    },
                    
                    new AirportSelection
                    {
                        Placeholder = "Select from airport",
                        Prefix      = "Return"
                    }
                }
            };
        }
    }
}