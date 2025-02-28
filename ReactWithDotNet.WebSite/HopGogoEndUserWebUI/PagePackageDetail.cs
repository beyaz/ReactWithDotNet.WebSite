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
            
            SvgIcon.Chevron_right_minor,
            
            new div(Font(400, 14, 21, "Euclid Circular B", "#2659C3"))
            {
                "Package Listing"
            },
            
            SvgIcon.Chevron_right_minor,
            
            new div(WordWrapBreakWord, Font(400, 14, 21, "Euclid Circular B", "#6A6A6A"))
            {
                "Sri Lanka Culture Package"
            }
        };
    }

    Element Detail()
    {
        return new FlexColumn
        {
            new FlexRow
            {
                new div(Width(449), Height(150), PositionRelative)
                {
                    new div(Width(264), Height(146), Left(185), Top(4), PositionAbsolute)
                    {
                        new div(Width(207), Height(30), Left(1), Top(83), PositionAbsolute)
                        {
                            new div(Width(207), Height(30), Left(0), Top(0), PositionAbsolute, Background("white"), Border(1, "#6A6A6A", solid, 6)),
                            new div(Width(38), Height(18), Left(14), Top(6), PositionAbsolute, WordWrapBreakWord, Font(400, 13, "Outfit", "black"))
                            {
                                "Dates:"
                            },
                            new div(Width(144), Height(18), Left(60), Top(6), PositionAbsolute, WordWrapBreakWord, Font(600, 13, "Outfit", "black"))
                            {
                                "Tue, 28 Jan - Fri, 4 Feb"
                            }
                        },
                        new div(Width(94), Height(30), Left(0), Top(116), PositionAbsolute)
                        {
                            new div(Width(94), Height(30), Left(0), Top(0), PositionAbsolute, Background("white"), Border(1, "#6A6A6A", solid, 6)),
                            new div(Width(66), Height(16.31), Left(15), Top(6.72), PositionAbsolute)
                            {
                                new div(Left(20), Top(0.28), PositionAbsolute, WordWrapBreakWord, Font(500, 13, "Outfit", "black"))
                                {
                                    "2 Adults"
                                },
                                new div(Data("svg-wrapper", ""), Left(0), Top(0), PositionAbsolute)
                                {
                                    new svg(svg.Width(12), svg.Height(18), ViewBox(0, 0, 12, 18), Fill(none))
                                    {
                                        new path
                                        {
                                            d    = "M3.392 4.35099C4.25327 4.35099 4.95153 3.22573 4.95153 2.22471C4.95153 1.22281 4.25327 0.723633 3.392 0.723633C2.53029 0.723633 1.83203 1.22281 1.83203 2.22471C1.83203 3.22573 2.53029 4.35099 3.392 4.35099Z",
                                            fill = "black"
                                        },
                                        new path
                                        {
                                            d    = "M11.0083 11.76H10.7193V10.9716H10.8845C11.1132 10.9716 11.2978 10.7868 11.2978 10.5569C11.2978 10.3279 11.1132 10.1422 10.8845 10.1422H9.90653C10.0136 9.80129 9.8779 9.41928 9.55674 9.23092L7.35578 7.94251C7.35578 7.94251 5.47685 5.29318 5.45923 5.26975L5.44866 5.25427C5.44822 5.25516 5.44822 5.25516 5.44822 5.25604C5.27596 5.03939 5.01472 4.89746 4.71824 4.89746C4.71824 4.89746 1.95954 4.90188 1.94457 4.90277C1.52737 4.92089 0.820735 5.01684 0.381953 5.44528C0.135248 5.68625 0 6.0068 0 6.34593V11.0428C0 11.4748 0.348912 11.8254 0.779765 11.8254C0.876244 11.8254 0.967437 11.8055 1.05246 11.7737V16.0917C1.05246 16.6108 1.47186 17.0312 1.98862 17.0312C2.50582 17.0312 2.92478 16.6108 2.92478 16.0917V11.4341H3.78252V16.0912C3.78252 16.6103 4.20148 17.0308 4.71824 17.0308C5.23499 17.0308 5.65395 16.6103 5.65395 16.0912V10.4946V10.1033V8.2666L6.1967 9.00322C6.2597 9.08855 6.33944 9.16018 6.43063 9.21412L8.01659 10.1426H7.91086C7.68266 10.1426 7.49763 10.3283 7.49763 10.5574C7.49763 10.7873 7.68266 10.9721 7.91086 10.9721H8.07607V11.7604H7.78707C7.23947 11.7604 6.79584 12.2052 6.79584 12.7553V14.7449C6.79584 15.2941 7.23947 15.7397 7.78707 15.7397H11.0088C11.5564 15.7397 12 15.2941 12 14.7449V12.7553C11.9996 12.2043 11.5559 11.76 11.0083 11.76ZM10.1823 11.76H8.61265V10.9716H10.1823V11.76Z",
                                            fill = "black"
                                        },
                                        new path
                                        {
                                            d    = "M7.99369 17.0318C8.25232 17.0318 8.46199 16.8214 8.46199 16.5618C8.46199 16.3022 8.25232 16.0918 7.99369 16.0918C7.73506 16.0918 7.52539 16.3022 7.52539 16.5618C7.52539 16.8214 7.73506 17.0318 7.99369 17.0318Z",
                                            fill = "black"
                                        },
                                        new path
                                        {
                                            d    = "M10.8013 17.0318C11.0599 17.0318 11.2696 16.8214 11.2696 16.5618C11.2696 16.3022 11.0599 16.0918 10.8013 16.0918C10.5427 16.0918 10.333 16.3022 10.333 16.5618C10.333 16.8214 10.5427 17.0318 10.8013 17.0318Z",
                                            fill = "black"
                                        }
                                    }
                                }
                            }
                        },
                        new div(Width(68), Height(30), Left(106), Top(116), PositionAbsolute)
                        {
                            new div(Width(68), Height(30), Left(0), Top(0), PositionAbsolute, Background("white"), Border(1, "#6A6A6A", solid, 6)),
                            new div(Left(13), Top(7), PositionAbsolute, WordWrapBreakWord, Font(500, 13, "Outfit", "black"))
                            {
                                "5 days"
                            }
                        },
                        new div(Left(31), Top(21), PositionAbsolute, WordWrapBreakWord, Font(300, 13, "Euclid Circular B", "black"))
                        {
                            "match to your preferences"
                        },
                        new div(Left(0), Top(25), PositionAbsolute, WordWrapBreakWord, Font(600, 13, 11, "Euclid Circular B", "black"))
                        {
                            "98%"
                        },
                        new div(Width(264), Left(0), Top(0), PositionAbsolute, WordWrapBreakWord, Font(600, 18, 20, "Euclid Circular B", "#210835"))
                        {
                            "Sri Lanka Culture Package"
                        }
                    },
                    new img(Src("https://placehold.co/175x150"), Width(175), Height(150), Left(0), Top(0), PositionAbsolute),
                    new img(Src("https://placehold.co/175x150"), Width(175), Height(150), Left(0), Top(0), PositionAbsolute),
                    new div(Data("svg-wrapper", ""), Left(1), Top(62.80), PositionAbsolute)
                    {
                        new svg(svg.Width(20), svg.Height(21), ViewBox(0, 0, 20, 21), Fill(none))
                        {
                            new path
                            {
                                fillRule = "evenodd",
                                clipRule = "evenodd",
                                d        = "M11.6672 15.8539C11.4539 15.8539 11.2405 15.7719 11.078 15.6089L6.91137 11.4271C6.58553 11.1 6.58553 10.5715 6.91137 10.2444L11.078 6.06265C11.4039 5.73563 11.9305 5.73563 12.2564 6.06265C12.5822 6.38966 12.5822 6.91824 12.2564 7.24526L8.67887 10.8358L12.2564 14.4262C12.5822 14.7533 12.5822 15.2818 12.2564 15.6089C12.0939 15.7719 11.8805 15.8539 11.6672 15.8539Z",
                                fill     = "white"
                            }
                        }
                    },
                    new div(Data("svg-wrapper", ""), Left(177), Top(82.87), PositionAbsolute)
                    {
                        new svg(svg.Width(20), svg.Height(21), ViewBox(0, 0, 20, 21), Fill(none))
                        {
                            new path
                            {
                                fillRule = "evenodd",
                                clipRule = "evenodd",
                                d        = "M8.3328 5.81699C8.54613 5.81699 8.75947 5.89895 8.92197 6.06204L13.0886 10.2438C13.4145 10.5709 13.4145 11.0994 13.0886 11.4265L8.92197 15.6083C8.59613 15.9353 8.06947 15.9353 7.74363 15.6083C7.4178 15.2812 7.4178 14.7527 7.74363 14.4256L11.3211 10.8351L7.74363 7.24465C7.4178 6.91764 7.4178 6.38906 7.74363 6.06204C7.90613 5.89895 8.11947 5.81699 8.3328 5.81699Z",
                                fill     = "white"
                            }
                        }
                    },
                    new div(Data("svg-wrapper", ""), Left(185), Top(53), PositionAbsolute)
                    {
                        new svg(svg.Width(16), svg.Height(17), ViewBox(0, 0, 16, 17), Fill(none))
                        {
                            new path
                            {
                                d    = "M12.667 10.625H15.3337V2.125H12.667M10.0003 2.125H4.00033C3.44699 2.125 2.97366 2.47917 2.77366 2.98917L0.760326 7.98292C0.700326 8.14583 0.666992 8.31583 0.666992 8.5V9.91667C0.666992 10.2924 0.807468 10.6527 1.05752 10.9184C1.30757 11.1841 1.6467 11.3333 2.00033 11.3333H6.20699L5.57366 14.5704C5.56033 14.6413 5.55366 14.7121 5.55366 14.79C5.55366 15.0875 5.66699 15.3496 5.84699 15.5408L6.55366 16.2917L10.9403 11.6238C11.187 11.3688 11.3337 11.0146 11.3337 10.625V3.54167C11.3337 3.16594 11.1932 2.80561 10.9431 2.53993C10.6931 2.27426 10.3539 2.125 10.0003 2.125Z",
                                fill = "black"
                            }
                        }
                    },
                    new div(Data("svg-wrapper", ""), Left(207), Top(50), PositionAbsolute)
                    {
                        new svg(ViewBox(0, 0, 16, 16), Fill(none), svg.Size(16))
                        {
                            new path
                            {
                                d      = "M9.29767 5.23087L9.17105 5.83366H9.78699H14.0003C14.2213 5.83366 14.4333 5.92146 14.5896 6.07774C14.7459 6.23402 14.8337 6.44598 14.8337 6.66699V8.00033C14.8337 8.10434 14.8146 8.20069 14.7773 8.29815L12.7674 12.9901L12.7674 12.9901L12.7655 12.9947C12.6414 13.2924 12.3475 13.5003 12.0003 13.5003H6.00033C5.77931 13.5003 5.56735 13.4125 5.41107 13.2562C5.25479 13.1 5.16699 12.888 5.16699 12.667V6.00033C5.16699 5.77094 5.25763 5.56346 5.41388 5.40721L9.44866 1.37243L9.80011 1.72055C9.80035 1.72079 9.8006 1.72104 9.80085 1.72129C9.89076 1.81159 9.94699 1.93902 9.94699 2.07366C9.94699 2.11725 9.94299 2.15668 9.93708 2.18703L9.29767 5.23087ZM2.83366 6.50033V13.5003H1.16699V6.50033H2.83366Z",
                                fill   = "white",
                                stroke = "black"
                            }
                        }
                    }
                }
            },

            new div(TextDecorationUnderline, WordWrapBreakWord, Font(600, 15, 20, "Euclid Circular B", "#210835"))
            {
                "Destinations"
            }
        };
    }
}

class Header : Component
{
    protected override Element render()
    {

        var menuLabels = new[]
        {
            "Discover",
            "Saved",
            "My Trips",
            "Sign Up"
        };
        
        return new FlexRow(JustifyContentSpaceBetween, PaddingTopBottom(27,22), Background(White), BoxShadow(0, 4.428116321563721, 17.712465286254883, rgba(0, 0, 0, 0.25)))
        {
            new div(Font(700, 35.42, "Outfit", "black"))
            {
                "HopGogo"
            },
            
            new FlexRow(AlignItemsCenter, Gap(32))
            {
                menuLabels.Select(label=>new div(Opacity(0.70), Font(500, 17.71, "Outfit", Black))
                {
                    label
                })
            }
            
        };
    }
}

sealed class SvgIcon : PureComponent
{
    readonly string _name;

    public SvgIcon(string name)
    {
        _name = name;
    }
    
    public static SvgIcon Chevron_right_minor => new SvgIcon(nameof(Chevron_right_minor));

    protected override Element render()
    {
        return _name switch
        {
            nameof(Chevron_right_minor)
                => new svg(svg.Size(20, 21), ViewBox(0, 0, 20, 21), Fill(none))
                {
                    new path
                    {
                        fillRule = "evenodd",
                        clipRule = "evenodd",
                        d        = "M7.50053 15.4545C7.2872 15.4545 7.07387 15.3725 6.91137 15.2094C6.58553 14.8824 6.58553 14.3538 6.91137 14.0268L10.4889 10.4363L6.91137 6.84584C6.58553 6.51883 6.58553 5.99025 6.91137 5.66323C7.2372 5.33621 7.76387 5.33621 8.0897 5.66323L12.2564 9.84503C12.5822 10.172 12.5822 10.7006 12.2564 11.0276L8.0897 15.2094C7.9272 15.3725 7.71387 15.4545 7.50053 15.4545Z",
                        fill     = "#878787"
                    }
                },



            _ => throw new NotSupportedException(_name)
        };
    }
}