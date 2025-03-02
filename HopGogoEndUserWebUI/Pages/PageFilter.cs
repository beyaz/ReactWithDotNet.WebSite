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

                new SectionFilter(),
                
                SpaceY(24)
                
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
            return new FlexColumn(SizeFull, Gap(24), Padding(24), Background("#F5F5F5"), BoxShadow(0, 2, 4, rgba(25, 33, 61, 0.16)), BorderRadius(16))
            {
                new FlexRow(Gap(24), JustifyContentSpaceBetween)
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
                    },
                    
                    new PassengerSelector()
                },
                
                new FlexRow(PaddingX(24), AlignItemsCenter,Height(50), Background(White), Border(1, "#6A6A6A", solid, 13))
                {
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        "Departure:"
                    },
                    new div(Font(600, 16, "Outfit", "black"))
                    {
                        "Tue, 28 Jan - Fri, 28 Feb"
                    }
                },
                
                new FlexColumn(Gap(4))
                {
                    new FlexRow(JustifyContentSpaceBetween)
                    {
                        new div(Font(400, 16, "Outfit", "black"))
                        {
                            "Add Destinations"
                        },
                        new FlexRowCentered
                        {
                            Svg_Plus + Color("#2659C3"),
                            new div(Font(400, 16, "Outfit", "#2659C3"))
                            {
                                "Add Destination"
                            }
                        }
                    },
                   
                    new FlexRow(Gap(36), WidthFull)
                    {
                        new FlexRow(Width(2,3), PaddingX(24), AlignItemsCenter, Height(50),  Background(White), Border(1, "#6A6A6A", solid, 13))
                        {
                            new div(Font(400, 16, "Outfit", "#6A6A6A"))
                            {
                                "First Destination"
                            }
                        },
                        
                        new FlexRow(Width(1,3),PaddingX(24), AlignItemsCenter,Height(50), Background(White), Border(1, "#6A6A6A", solid, 13))
                        {
                            new div(Font(400, 16, "Outfit", "#6A6A6A"), WhiteSpaceNoWrap)
                            {
                                "Stay 2 night"
                            }
                        },
                    }
                    
                    
                    
                    
                    
                },
                
                new FlexRow(WidthFull, JustifyContentFlexEnd)
                {
                    SearchTripButton
                }
            };
        }

        Element SearchTripButton()
        {
            return new FlexRowCentered(Width(135), Height(50), Padding(10), Background("#0CBCC5"), BorderRadius(10), Font(700, 16, "Outfit", "white"))
            {
                "Search Trip",
                Hover(Background(Gray400))
            };
        }
    }
}
