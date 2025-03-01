using System.Linq;
using static HopGogoEndUserWebUI.SvgIcon;

namespace HopGogoEndUserWebUI;

public class PagePackageDetail: Component
{
    protected override Element render()
    {
        return new div
        {
            new link
            {
                href = "https://fonts.cdnfonts.com/css/euclid-circular-b", rel = "stylesheet"
            },
            new link
            {
                href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&family=Outfit:wght@100..900&family=Plus+Jakarta+Sans:ital,wght@0,200..800;1,200..800&family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet"
            },
            
            new Header(),
            BreadCrumbPaths(),
            AdditionalPreferences(),
            
            Detail()
        };
    }

    
    
    Element AdditionalPreferences()
    {
        return new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween, Background("#F4F4F4"), Height(50))
        {
            new FlexRow(AlignItemsCenter)
            {
                new span(Color("black"), FontSize13, FontFamily("Outfit"), FontWeight400)
                {
                    "Additional Preferences"
                },

                SpaceX(8),
                
                new FlexRow(Gap(8))
                {
                    new[]
                    {
                        "Nature",
                        "Skiing",
                        "Casa Cook Miami",
                        "Nature",
                        "Skiing",
                        "Casa Cook Miami"

                    }.Select(label => new span(TextAlignCenter, Padding(8), BorderRadius(6), Background("#EEDAFF"), Font(500, 13, 16, "Outfit", "#210835"))
                    {
                        label
                    })
                }
            },

            new FlexRowCentered(Padding(5, 12, 5, 8), Border(1, "#360D57", solid, 10), Font(500, 14, 20, "Euclid Circular B", "#360D57"))
            {
                "Change your preferences"
            }
        };
    }

    Element BreadCrumbPaths()
    {
        return new FlexRow(AlignItemsCenter, Background(White), PaddingY(8))
        {
            new div(Font(400, 14, 21, "Euclid Circular B", "#2659C3"))
            {
                "Home"
            },
            
            Chevron_right_minor,
            
            new div(Font(400, 14, 21, "Euclid Circular B", "#2659C3"))
            {
                "Package Listing"
            },
            
            Chevron_right_minor,
            
            new div(WordWrapBreakWord, Font(400, 14, 21, "Euclid Circular B", "#6A6A6A"))
            {
                "Sri Lanka Culture Package"
            }
        };
    }

    Element Detail()
    {
        return new FlexColumn(Background(White), PaddingLeft(64), PaddingTop(24))
        {
            TopSummaryCard,
            SectionDestination
        };
    }

    Element SectionDestination()
    {
        var items = new[]
        {
            new
            {
                Label = "İstanbul", 
                Day  = 0
            },
            new
            {
                Label = "Bali", 
                Day      = 2
            },
            new
            {
                Label = "Sri Lanka", 
                Day = 1
            },
            new
            {
                Label = "Dubai", 
                Day     = 2
            },
            new
            {
                Label = "İstanbul",
                Day  = 0
            }
        };
        
        return new FlexColumn(Gap(8))
        {
            new div(TextDecorationUnderline, WordWrapBreakWord, Font(600, 15, 20, "Euclid Circular B", "#210835"))
            {
                "Destinations"
            },
            
            new FlexRow(JustifyContentSpaceAround)
            {
                items.Select((item, index) => new FlexColumn(AlignItemsCenter, Gap(8))
                {
                    new img(Src(DummySrc(64)), Size(64), BorderRadius(50)),
                    
                    new FlexColumnCentered(Border(1, solid, "#6A6A6A", 8), Padding(8), PositionRelative)
                    {
                        SvgClose + PositionAbsolute + TopRight(4),
                        
                        new div(WordWrapBreakWord, Font(600, 13, 20, "Euclid Circular B", "black"))
                        {
                            item.Label
                        },
                        new FlexRow(Gap(8), AlignItemsCenter)
                        {
                            SvgNegative + When(index == 0 || index == items.Length -1, VisibilityHidden),
                            
                            new div(WordWrapBreakWord, Font(400, 13, 20, "Euclid Circular B", "black"))
                            {
                                index == 0 ?"From" : index == items.Length-1 ? "Return" : $"{item.Day} days"
                            },

                            SvgPlus + When(index == 0 || index == items.Length-1, VisibilityHidden),
                        }
                    }
                })
            }
            
        };
    }
    Element TopSummaryCard()
    {
        return new FlexRow(Gap(8), Background(White), WidthFull)
            {
                new img(Src(DummySrc(175,150)), Size(175, 150)),
                
                new FlexColumn(JustifyContentSpaceBetween, WidthFull)
                {
                    new FlexRow(Gap(24), JustifyContentSpaceBetween)
                    {
                        new FlexColumn
                        {
                            new div(WhiteSpaceNoWrap, Font(600, 18, 20, "Euclid Circular B", "#210835"))
                            {
                                "Sri Lanka Culture Package"
                            },
                    
                            SpaceY(5),
                            new FlexRow(AlignItemsCenter)
                            {
                                new div(Font(600, 13, 11, "Euclid Circular B", "black"))
                                {
                                    "98%"
                                },
                                new div(Font(300, 13, "Euclid Circular B", "black"))
                                {
                                    "match to your preferences"
                                },
                            },
                        
                            SpaceY(10),
                            new FlexRow(Gap(8))
                            {
                                Dislike, Like
                            }
                        },
                    
                        new FlexColumn(AlignItemsFlexEnd, Gap(4))
                        {
                           new FlexRow(AlignItemsCenter, Gap(4))
                           {
                               Refresh_bold,
                               new div(WordWrapBreakWord, Font(600, 13, "Euclid Circular B", "#6A6A6A"))
                               {
                                   "$1.200"
                               },
                               new div(WordWrapBreakWord, Font(600, 16, "Euclid Circular B", "black"))
                               {
                                   "$890"
                               },
                           },
                            new div(WordWrapBreakWord, Font(300, 10, "Euclid Circular B", "black"))
                            {
                                "5 min. ago price"
                            },
                        
                            new FlexRowCentered(Background("#0CBCC5"), Border(1, "#0CBCC5", solid, 10), Padding(6,12), WidthFitContent)
                            {
                                Font(500, 14, 20, "Euclid Circular B", "white"),
                                "Buy Package"
                            }
                        },
                    },
                    
                   new FlexColumn(Gap(4))
                   {
                       new FlexRow(AlignItemsCenter, Gap(8), Border(1, solid, "#6A6A6A", 6), Padding(4), WidthFitContent)
                       {
                           new div(Font(400, 13, "Outfit", "black"))
                           {
                               "Dates:"
                           },
                           new div(Font(600, 13, "Outfit", "black"))
                           {
                               "Tue, 28 Jan - Fri, 4 Feb"
                           }
                       },
                    
                       new FlexRow(Gap(12))
                       {
                           new FlexRow(AlignItemsCenter, Gap(8), Border(1, solid, "#6A6A6A", 6), Padding(4), WidthFitContent)
                           {
                               PersonWithBaggage,
                               new div(Font(600, 13, "Outfit", "black"))
                               {
                                   "2 Adults"
                               }
                           },
                           new FlexRow(AlignItemsCenter, Gap(8), Border(1, solid, "#6A6A6A", 6), Padding(4), WidthFitContent)
                           {
                               new div(Font(600, 13, "Outfit", "black"))
                               {
                                   "5 days"
                               }
                           }
                       }
                   }
                }
            };
    }
    
}