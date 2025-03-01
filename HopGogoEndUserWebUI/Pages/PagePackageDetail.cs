using static HopGogoEndUserWebUI.SvgIcon;

namespace HopGogoEndUserWebUI.Pages;

sealed class PagePackageDetail : Component
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
            SpaceY(1),
            new Breadcrumbs(),
            SectionAdditionalPreferences(),

            new FlexRow(Gap(8), PaddingBottom(16))
            {
                Sections, SectionImageGalery
            },
            
        };
    }

    Element SectionAdditionalPreferences()
    {
        return new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween, Background("#F4F4F4"), Height(50), PaddingX(32))
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
                    Item("Nature"),
                    Item("Skiing"),
                    Item("Casa Cook Miami"),
                    Item("Nature"),
                    Item("Skiing"),
                    Item("Casa Cook Miami")
                }
            },

            new FlexRowCentered(Padding(5, 12, 5, 8), Border(1, "#360D57", solid, 10), Font(500, 14, 20, "Euclid Circular B", "#360D57"))
            {
                "Change your preferences"
            }
        };

        Element Item(string label)
        {
            return new span(TextAlignCenter, Padding(8), BorderRadius(6), Background("#EEDAFF"), Font(500, 13, 16, "Outfit", "#210835"))
            {
                label
            };
        }
    }

    Element SectionBuy()
    {
        return new FlexRowCentered
        {
            new FlexRowCentered(Background("#0CBCC5"), Border(1, solid, "#6A6A6A", 9), Padding(8, 16), Font(500, 14, 20, "Euclid Circular B", "white"))
            {
                "Buy Package"
            }
        };
    }

    Element SectionDestination()
    {
        var items = new[]
        {
            new
            {
                Label = "İstanbul",
                Day   = 0
            },
            new
            {
                Label = "Bali",
                Day   = 2
            },
            new
            {
                Label = "Sri Lanka",
                Day   = 1
            },
            new
            {
                Label = "Dubai",
                Day   = 2
            },
            new
            {
                Label = "İstanbul",
                Day   = 0
            }
        };

        return new FlexColumn(Gap(8))
        {
            new div(TextDecorationUnderline, WordWrapBreakWord, Font(600, 15, 20, "Euclid Circular B", "#210835"))
            {
                "Destinations"
            },

            new FlexRow(JustifyContentSpaceBetween)
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
                            SvgNegative + When(index == 0 || index == items.Length - 1, VisibilityHidden),

                            new div(WordWrapBreakWord, Font(400, 13, 20, "Euclid Circular B", "black"))
                            {
                                index == 0 ? "From" : index == items.Length - 1 ? "Return" : $"{item.Day} days"
                            },

                            SvgPlus + When(index == 0 || index == items.Length - 1, VisibilityHidden)
                        }
                    }
                })
            }
        };
    }

    Element SectionImageGalery()
    {
        return new FlexColumn(Gap(8))
        {
            new FlexRow(Gap(8), Height(500), WidthFull)
            {
                new FlexColumn(Gap(8), Width(1, 2))
                {
                    new img(Height(50 * percent)) { src = DummySrc(250) },
                    new img(Height(50 * percent)) { src = DummySrc(250) }
                },
                new img(Width(1, 2)) { src = DummySrc(250, 500) }
            },
            SpaceY(8),
            new img { src = DummySrc(500, 400) }
        };
    }

    Element SectionOverview()
    {
        return new FlexColumn(Gap(8))
        {
            new div(TextDecorationUnderline, Font(600, 15, 20, "Euclid Circular B", "#210835"))
            {
                "Overview"
            },

            new FlexColumn(Gap(8))
            {
                Item("28 Jan 2025", "Flight to Bali", "TK0945", "09.15", "18:15", 80, 2),
                Item("28 Jan 2025", "Flight to Bali", "TK0945", "09.15", "18:15", 80, 2),
                Item("28 Jan 2025", "Flight to Bali", "TK0945", "09.15", "18:15", 80, 2),
                Item("28 Jan 2025", "Flight to Bali", "TK0945", "09.15", "18:15", 80, 2),
                Item("28 Jan 2025", "Flight to Bali", "TK0945", "09.15", "18:15", 80, 2)
            }
        };

        static Element Item(string beginDate, string flightTo, string flyNumber, string departure, string arrival, decimal amount, int numberOfAdult)
        {
            return new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween, Border(1, solid, "#6A6A6A", 10), Padding(18, 12), Font(600, 15, 20, "Euclid Circular B", "black"))
            {
                new div
                {
                    beginDate
                },

                new FlexColumn
                {
                    new div { flightTo },
                    new div { flyNumber }
                },

                new FlexColumn
                {
                    new div { "Departure: " + departure },
                    new div { "Arrival: " + arrival }
                },

                new FlexColumn
                {
                    new div(Font(600, 20, "Euclid Circular B", "#3E14FB"))
                    {
                        "$" + amount
                    },
                    new div(Font(400, 12, 20, "Euclid Circular B", "#6A6A6A"))
                    {
                        numberOfAdult + " adults"
                    }
                }
            };
        }
    }

    Element Sections()
    {
        return new FlexColumn(Background(White), PaddingLeft(64), PaddingY(24), Gap(24))
        {
            SectionTopSummary,
            SectionDestination,
            SectionOverview,
            SectionBuy
        };
    }

    Element SectionTopSummary()
    {
        return new FlexRow(Gap(8), Background(White), WidthFull)
        {
            new img(Src(DummySrc(175, 150)), Size(175, 150)),

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
                            }
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
                            }
                        },
                        new div(WordWrapBreakWord, Font(300, 10, "Euclid Circular B", "black"))
                        {
                            "5 min. ago price"
                        },

                        new FlexRowCentered(Background("#0CBCC5"), Border(1, "#0CBCC5", solid, 10), Padding(6, 12), WidthFitContent)
                        {
                            Font(500, 14, 20, "Euclid Circular B", "white"),
                            "Buy Package"
                        }
                    }
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