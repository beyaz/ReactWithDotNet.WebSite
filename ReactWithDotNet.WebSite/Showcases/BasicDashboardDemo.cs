using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ReactWithDotNet.WebSite.Showcases;

// Look like : https://nextjs-demo.tailadmin.com/chat

record BasicDashboardDemoState
{
    public string SelectedMenuId { get; init; }
}

sealed class BasicDashboardDemo : Component<BasicDashboardDemoState>
{
    protected override Element render()
    {
        return new FlexRow(WidthFull, HeightAuto)
        {
            new LeftMenu(),
            new FlexColumn
            {
                new HeaderBar(),
                new ChatApp()
            }
        };
    }

    class ChatApp : Component
    {
        protected override Element render()
        {
            return new div(Padding(24), MaxWidth(1536), MarginLeft(auto), MarginRight(auto), BoxSizingBorderBox)
            {
                new div(BoxSizingBorderBox)
                {
                    new FlexRow(Gap(12), WebkitBoxPack("justify"), JustifyContentSpaceBetween, AlignItemsCenter, MarginBottom(24), BoxSizingBorderBox)
                    {
                        new h2(Color(rgb(29, 41, 57)), FontWeight600, FontSize20, LineHeight28, Margin(0), BoxSizingBorderBox)
                        {
                            "Chat"
                            // x-text = "pageName"
                        },
                        new nav(BoxSizingBorderBox)
                        {
                            new ol(Gap(6), AlignItemsCenter, DisplayFlex, ListStyle("outside none none"), Margin(0), Padding(0), BoxSizingBorderBox)
                            {
                                new li(BoxSizingBorderBox)
                                {
                                    new a(Href("index.html"), Color(rgb(102, 112, 133)), FontSize14, LineHeight20, Gap(6), AlignItemsCenter, DisplayInlineFlex, TextDecoration("none solid rgb(102, 112, 133)"), BoxSizingBorderBox)
                                    {
                                        @"
                            Home
                            ",
                                        new svg(svg.Width(17), svg.Height(16), ViewBox(0, 0, 17, 16), Fill(none), Stroke(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                        {
                                            new path(BoxSizingBorderBox)
                                            {
                                                d              = "M6.0765 12.667L10.2432 8.50033L6.0765 4.33366",
                                                stroke         = "",
                                                strokeWidth    = 1.2,
                                                strokeLinecap  = "round",
                                                strokeLinejoin = "round"
                                            }
                                        }
                                    }
                                },
                                new li(Color(rgb(29, 41, 57)), FontSize14, LineHeight20, BoxSizingBorderBox)
                                {
                                    "Chat"
                                    // x-text = "pageName"
                                }
                            }
                        }
                    }
                },
                new div(Height(501), OverflowHidden, BoxSizingBorderBox)
                {
                    new FlexRow(Gap(20), WebkitBoxOrient("horizontal"), WebkitBoxDirection("normal"), Height(501), BoxSizingBorderBox)
                    {
                        new FlexColumn(Width("25%"), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), BorderStyle(solid))
                        {
                            new div(PositionSticky, BoxSizingBorderBox, Padding(20, 20, 0, 20))
                            {
                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                {
                                    new div(BoxSizingBorderBox)
                                    {
                                        new h3(FontSize24, LineHeight32, Color(rgb(29, 41, 57)), FontWeight600, Margin(0), BoxSizingBorderBox)
                                        {
                                            "Chats"
                                        }
                                    },
                                    new div(PositionRelative, BoxSizingBorderBox)
                                    {
                                        new button("openDropDown ? 'text-gray-700 dark:text-white' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-white'", Color(rgb(102, 112, 133)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M10.2441 6C10.2441 5.0335 11.0276 4.25 11.9941 4.25H12.0041C12.9706 4.25 13.7541 5.0335 13.7541 6C13.7541 6.9665 12.9706 7.75 12.0041 7.75H11.9941C11.0276 7.75 10.2441 6.9665 10.2441 6ZM10.2441 18C10.2441 17.0335 11.0276 16.25 11.9941 16.25H12.0041C12.9706 16.25 13.7541 17.0335 13.7541 18C13.7541 18.9665 12.9706 19.75 12.0041 19.75H11.9941C11.0276 19.75 10.2441 18.9665 10.2441 18ZM11.9941 10.25C11.0276 10.25 10.2441 11.0335 10.2441 12C10.2441 12.9665 11.0276 13.75 11.9941 13.75H12.0041C12.9706 13.75 13.7541 12.9665 13.7541 12C13.7541 11.0335 12.9706 10.25 12.0041 10.25H11.9941Z",
                                                    fill     = ""
                                                }
                                            }
                                        },
                                        new div(DisplayNone, BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(16, 24, 40, 0.08) 0px 12px 16px -4px, rgba(16, 24, 40, 0.03) 0px 4px 6px -2px"), Padding(8), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), Width("10rem"), ZIndex("40"), Top("100%"), Right(0), PositionAbsolute, BorderStyle(solid))
                                        {
                                            new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(0), Padding(8, 12), BoxSizingBorderBox)
                                            {
                                                "View More"
                                            },
                                            new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), MarginTop(4), MarginBottom(0), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(4, 0, 0), Padding(8, 12), BoxSizingBorderBox)
                                            {
                                                "Delete"
                                            }
                                        }
                                    }
                                },
                                new FlexRow(Gap(12), AlignItemsCenter, MarginTop(16), BoxSizingBorderBox)
                                {
                                    new button(DisplayNone, Color(rgb(52, 64, 84)), BorderColor(rgb(208, 213, 221)), BorderWidth(1), BorderRadius(8), JustifyContentCenter, AlignItemsCenter, MaxWidth(44), WidthFull, Height(44), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BorderStyle(solid))
                                    {
                                        new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(52, 64, 84)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                        {
                                            new path(BoxSizingBorderBox)
                                            {
                                                fillRule = "evenodd",
                                                clipRule = "evenodd",
                                                d        = "M3.25 6C3.25 5.58579 3.58579 5.25 4 5.25H20C20.4142 5.25 20.75 5.58579 20.75 6C20.75 6.41421 20.4142 6.75 20 6.75L4 6.75C3.58579 6.75 3.25 6.41422 3.25 6ZM3.25 18C3.25 17.5858 3.58579 17.25 4 17.25L20 17.25C20.4142 17.25 20.75 17.5858 20.75 18C20.75 18.4142 20.4142 18.75 20 18.75L4 18.75C3.58579 18.75 3.25 18.4142 3.25 18ZM4 11.25C3.58579 11.25 3.25 11.5858 3.25 12C3.25 12.4142 3.58579 12.75 4 12.75L20 12.75C20.4142 12.75 20.75 12.4142 20.75 12C20.75 11.5858 20.4142 11.25 20 11.25L4 11.25Z",
                                                fill     = ""
                                            }
                                        }
                                    },
                                    new div(WidthFull, PositionRelative, BoxSizingBorderBox, MarginTopBottom(8))
                                    {
                                        new form(BoxSizingBorderBox)
                                        {
                                            new button(Transform("matrix(1, 0, 0, 1, 0, -10)"), Top(22), Left(16), PositionAbsolute, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Color(rgb(0, 0, 0)), Margin(0), Padding(0), BoxSizingBorderBox)
                                            {
                                                new svg(ViewBox(0, 0, 20, 20), Fill(none), svg.Size(20), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                                {
                                                    new path(BoxSizingBorderBox)
                                                    {
                                                        fillRule = "evenodd",
                                                        clipRule = "evenodd",
                                                        d        = "M3.04199 9.37381C3.04199 5.87712 5.87735 3.04218 9.37533 3.04218C12.8733 3.04218 15.7087 5.87712 15.7087 9.37381C15.7087 12.8705 12.8733 15.7055 9.37533 15.7055C5.87735 15.7055 3.04199 12.8705 3.04199 9.37381ZM9.37533 1.54218C5.04926 1.54218 1.54199 5.04835 1.54199 9.37381C1.54199 13.6993 5.04926 17.2055 9.37533 17.2055C11.2676 17.2055 13.0032 16.5346 14.3572 15.4178L17.1773 18.2381C17.4702 18.531 17.945 18.5311 18.2379 18.2382C18.5308 17.9453 18.5309 17.4704 18.238 17.1775L15.4182 14.3575C16.5367 13.0035 17.2087 11.2671 17.2087 9.37381C17.2087 5.04835 13.7014 1.54218 9.37533 1.54218Z",
                                                        fill     = ""
                                                    }
                                                }
                                            },
                                            new input(input.Type("text"), input.Placeholder("Search..."), BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(16, 24, 40, 0.05) 0px 1px 2px 0px"), Color(rgb(29, 41, 57)), FontSize14, LineHeight20, BackgroundColor("rgba(0, 0, 0, 0)"), BorderColor(rgb(208, 213, 221)), BorderWidth(1), BorderRadius(8), WidthFull, Height(44), Appearance(none), Padding(10, 14, 10, 42), FontFamily("Outfit, sans-serif"), FontWeight400, LetterSpacingNormal, Margin(0), BorderStyle(solid))
                                        }
                                    }
                                }
                            },
                            new FlexColumn("isMobile ? 'flex fixed xl:static top-0 left-0 z-999999 h-screen bg-white dark:bg-gray-900' : 'hidden xl:flex'", PositionStatic, ScrollbarWidth(none), BackgroundColor(rgb(255, 255, 255)), OverflowAuto, WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), Height(371), ZIndex("999999"), Top(0), Left(0), BoxSizingBorderBox)
                            {
                                new div(DisplayNone, Padding(20), BorderColor(rgb(228, 231, 236)), BorderBottomWidth("1px"), WebkitBoxPack("justify"), JustifyContentSpaceBetween, AlignItemsCenter, BorderWidth("0px 0px 1px"), BorderStyle(solid))
                                {
                                    new div(BoxSizingBorderBox)
                                    {
                                        new h3(FontSize24, LineHeight32, Color(rgb(29, 41, 57)), FontWeight600, Margin(0), BoxSizingBorderBox)
                                        {
                                            "Chat"
                                        }
                                    },
                                    new FlexRow(Gap(4), AlignItemsCenter, BoxSizingBorderBox)
                                    {
                                        new div(MarginBottom(-6), PositionRelative, BoxSizingBorderBox)
                                        {
                                            new button("openDropDown ? 'text-gray-700 dark:text-white' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-white'", Color(rgb(102, 112, 133)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                            {
                                                new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                                {
                                                    new path(BoxSizingBorderBox)
                                                    {
                                                        fillRule = "evenodd",
                                                        clipRule = "evenodd",
                                                        d        = "M10.2441 6C10.2441 5.0335 11.0276 4.25 11.9941 4.25H12.0041C12.9706 4.25 13.7541 5.0335 13.7541 6C13.7541 6.9665 12.9706 7.75 12.0041 7.75H11.9941C11.0276 7.75 10.2441 6.9665 10.2441 6ZM10.2441 18C10.2441 17.0335 11.0276 16.25 11.9941 16.25H12.0041C12.9706 16.25 13.7541 17.0335 13.7541 18C13.7541 18.9665 12.9706 19.75 12.0041 19.75H11.9941C11.0276 19.75 10.2441 18.9665 10.2441 18ZM11.9941 10.25C11.0276 10.25 10.2441 11.0335 10.2441 12C10.2441 12.9665 11.0276 13.75 11.9941 13.75H12.0041C12.9706 13.75 13.7541 12.9665 13.7541 12C13.7541 11.0335 12.9706 10.25 12.0041 10.25H11.9941Z",
                                                        fill     = ""
                                                    }
                                                }
                                            },
                                            new div(DisplayNone, BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(16, 24, 40, 0.08) 0px 12px 16px -4px, rgba(16, 24, 40, 0.03) 0px 4px 6px -2px"), Padding(8), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), Width("10rem"), ZIndex("40"), Top("100%"), Right(0), PositionAbsolute, BorderStyle(solid))
                                            {
                                                new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(0), Padding(8, 12), BoxSizingBorderBox)
                                                {
                                                    "View More"
                                                },
                                                new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), MarginTop(4), MarginBottom(0), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(4, 0, 0), Padding(8, 12), BoxSizingBorderBox)
                                                {
                                                    "Delete"
                                                }
                                            }
                                        },
                                        new button(Color(rgb(52, 64, 84)), BorderColor(rgb(208, 213, 221)), BorderWidth(1), BorderRadius(9999), JustifyContentCenter, AlignItemsCenter, Width("2.5rem"), Height(40), DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BorderStyle(solid))
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(52, 64, 84)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M6.21967 7.28131C5.92678 6.98841 5.92678 6.51354 6.21967 6.22065C6.51256 5.92775 6.98744 5.92775 7.28033 6.22065L11.999 10.9393L16.7176 6.22078C17.0105 5.92789 17.4854 5.92788 17.7782 6.22078C18.0711 6.51367 18.0711 6.98855 17.7782 7.28144L13.0597 12L17.7782 16.7186C18.0711 17.0115 18.0711 17.4863 17.7782 17.7792C17.4854 18.0721 17.0105 18.0721 16.7176 17.7792L11.999 13.0607L7.28033 17.7794C6.98744 18.0722 6.51256 18.0722 6.21967 17.7794C5.92678 17.4865 5.92678 17.0116 6.21967 16.7187L10.9384 12L6.21967 7.28131Z",
                                                    fill     = ""
                                                }
                                            }
                                        }
                                    }
                                },
                                new FlexColumn(OverflowAuto, WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), MaxHeight("100%"), BoxSizingBorderBox, PaddingLeftRight(20))
                                {
                                    new div(OverflowAuto, MaxHeight("100%"), BoxSizingBorderBox)
                                    {
                                        new FlexRow(Padding(12), BorderRadius(8), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img { Size(50), Src(DummySrc(50)), BorderRadius(50) },
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Kaiya George"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Project Manager"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "15 mins"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-17.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Lindsey Curtis"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Designer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "30 mins"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-19.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Zain Geidt"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Content Writer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "45 mins"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-05.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(247, 144, 9)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Carla George"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Front-end Developer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "2 days"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-20.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Abram Schleifer"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Digital Marketer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "1 hour"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-34.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Lincoln Donin"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Project ManagerProduct Designer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "3 days"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-35.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Erin Geidthem"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Copyrighter"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "5 days"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-36.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(240, 68, 56)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Alena Baptista"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "SEO Expert"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "2 hours"
                                                    }
                                                }
                                            }
                                        },
                                        new FlexRow(Padding(12), BorderRadius(8), MarginTop(4), MarginBottom(0), Gap(12), AlignItemsCenter, CursorPointer, BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/user/user-37.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                                new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                            },
                                            new div(WidthFull, BoxSizingBorderBox)
                                            {
                                                new FlexRow(WebkitBoxPack("justify"), JustifyContentSpaceBetween, WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                                {
                                                    new div(BoxSizingBorderBox)
                                                    {
                                                        new h5(Color(rgb(29, 41, 57)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                        {
                                                            "Wilium vamos"
                                                        },
                                                        new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(2), Margin(2, 0, 0), BoxSizingBorderBox)
                                                        {
                                                            "Content Writer"
                                                        }
                                                    },
                                                    new span(Color(rgb(152, 162, 179)), FontSize12, LineHeight18, BoxSizingBorderBox)
                                                    {
                                                        "5 days"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new FlexColumn(Width("75%"), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), OverflowHidden, WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), Height(501), BorderStyle(solid))
                        {
                            new FlexRow(BorderColor(rgb(228, 231, 236)), BorderBottomWidth("1px"), WebkitBoxPack("justify"), JustifyContentSpaceBetween, AlignItemsCenter, PositionSticky, BorderWidth("0px 0px 1px"), BorderStyle(solid), Padding(16, 24))
                            {
                                new FlexRow(Gap(12), AlignItemsCenter, BoxSizingBorderBox)
                                {
                                    new div(BorderRadius(9999), MaxWidth(48), WidthFull, Height(48), PositionRelative, BoxSizingBorderBox)
                                    {
                                        new img(Src("src/images/user/user-17.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(48), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox),
                                        new span(BackgroundColor(rgb(18, 183, 106)), BorderColor(rgb(255, 255, 255)), BorderWidth(1.5), BorderRadius(9999), Width("0.75rem"), Height(12), DisplayBlock, Right(0), Bottom(0), PositionAbsolute, BorderStyle(solid))
                                    },
                                    new h5(Color(rgb(102, 112, 133)), FontWeight500, FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                    {
                                        "Lindsey Curtis"
                                    }
                                },
                                new FlexRow(Gap(12), AlignItemsCenter, BoxSizingBorderBox)
                                {
                                    new button(Color(rgb(52, 64, 84)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                    {
                                        new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Stroke(rgb(52, 64, 84)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                        {
                                            new path(BoxSizingBorderBox)
                                            {
                                                d           = "M5.54488 11.7254L8.80112 10.056C8.94007 9.98476 9.071 9.89524 9.16639 9.77162C9.57731 9.23912 9.66722 8.51628 9.38366 7.89244L7.76239 4.32564C7.23243 3.15974 5.7011 2.88206 4.79552 3.78764L3.72733 4.85577C3.36125 5.22182 3.18191 5.73847 3.27376 6.24794C3.9012 9.72846 5.56003 13.0595 8.25026 15.7497C10.9405 18.44 14.2716 20.0988 17.7521 20.7262C18.2615 20.8181 18.7782 20.6388 19.1442 20.2727L20.2124 19.2045C21.118 18.2989 20.8403 16.7676 19.6744 16.2377L16.1076 14.6164C15.4838 14.3328 14.7609 14.4227 14.2284 14.8336C14.1048 14.929 14.0153 15.06 13.944 15.1989L12.2747 18.4552",
                                                stroke      = "",
                                                strokeWidth = 1.5
                                            }
                                        }
                                    },
                                    new button(Color(rgb(52, 64, 84)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                    {
                                        new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(52, 64, 84)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                        {
                                            new path(BoxSizingBorderBox)
                                            {
                                                fillRule = "evenodd",
                                                clipRule = "evenodd",
                                                d        = "M4.25 5.25C3.00736 5.25 2 6.25736 2 7.5V16.5C2 17.7426 3.00736 18.75 4.25 18.75H15.25C16.4926 18.75 17.5 17.7426 17.5 16.5V15.3957L20.1118 16.9465C20.9451 17.4412 22 16.8407 22 15.8716V8.12838C22 7.15933 20.9451 6.55882 20.1118 7.05356L17.5 8.60433V7.5C17.5 6.25736 16.4926 5.25 15.25 5.25H4.25ZM17.5 10.3488V13.6512L20.5 15.4325V8.56756L17.5 10.3488ZM3.5 7.5C3.5 7.08579 3.83579 6.75 4.25 6.75H15.25C15.6642 6.75 16 7.08579 16 7.5V16.5C16 16.9142 15.6642 17.25 15.25 17.25H4.25C3.83579 17.25 3.5 16.9142 3.5 16.5V7.5Z",
                                                fill     = ""
                                            }
                                        }
                                    },
                                    new div(MarginBottom(-6), PositionRelative, BoxSizingBorderBox)
                                    {
                                        new button("openDropDown ? 'text-gray-800 dark:text-white/90' : 'text-gray-700 dark:text-gray-400 hover:text-gray-800 dark:hover:text-white/90'", Color(rgb(52, 64, 84)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(52, 64, 84)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M10.2441 6C10.2441 5.0335 11.0276 4.25 11.9941 4.25H12.0041C12.9706 4.25 13.7541 5.0335 13.7541 6C13.7541 6.9665 12.9706 7.75 12.0041 7.75H11.9941C11.0276 7.75 10.2441 6.9665 10.2441 6ZM10.2441 18C10.2441 17.0335 11.0276 16.25 11.9941 16.25H12.0041C12.9706 16.25 13.7541 17.0335 13.7541 18C13.7541 18.9665 12.9706 19.75 12.0041 19.75H11.9941C11.0276 19.75 10.2441 18.9665 10.2441 18ZM11.9941 10.25C11.0276 10.25 10.2441 11.0335 10.2441 12C10.2441 12.9665 11.0276 13.75 11.9941 13.75H12.0041C12.9706 13.75 13.7541 12.9665 13.7541 12C13.7541 11.0335 12.9706 10.25 12.0041 10.25H11.9941Z",
                                                    fill     = ""
                                                }
                                            }
                                        },
                                        new div(DisplayNone, BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(16, 24, 40, 0.08) 0px 12px 16px -4px, rgba(16, 24, 40, 0.03) 0px 4px 6px -2px"), Padding(8), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), Width("10rem"), ZIndex("40"), Top("100%"), Right(0), PositionAbsolute, BorderStyle(solid))
                                        {
                                            new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(0), Padding(8, 12), BoxSizingBorderBox)
                                            {
                                                "View More"
                                            },
                                            new button(Color(rgb(102, 112, 133)), FontWeight500, FontSize12, LineHeight18, TextAlignLeft, BorderRadius(8), MarginTop(4), MarginBottom(0), WidthFull, DisplayFlex, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(4, 0, 0), Padding(8, 12), BoxSizingBorderBox)
                                            {
                                                "Delete"
                                            }
                                        }
                                    }
                                }
                            },
                            new div(Padding(24), OverflowAuto, WebkitBoxFlex("1"), Flex("1 1 0%"), MaxHeight("100%"), BoxSizingBorderBox)
                            {
                                new div(MaxWidth(350), BoxSizingBorderBox)
                                {
                                    new FlexRow(Gap(16), WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                    {
                                        new div(BorderRadius(9999), MaxWidth(40), WidthFull, Height(40), BoxSizingBorderBox)
                                        {
                                            new img(Src("src/images/user/user-17.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(40), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox)
                                        },
                                        new div(BoxSizingBorderBox)
                                        {
                                            new div(BackgroundColor(rgb(242, 244, 247)), BorderTopLeftRadius(2), BorderRadius(2, 8, 8), BoxSizingBorderBox, Padding(8, 12))
                                            {
                                                new p(Color(rgb(29, 41, 57)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                {
                                                    "I want to make an appointment tomorrow from 2:00 to 5:00pm?"
                                                }
                                            },
                                            new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(8), Margin(8, 0, 0), BoxSizingBorderBox)
                                            {
                                                "Lindsey, 2 hours ago"
                                            }
                                        }
                                    }
                                },
                                new div(MarginTop(32), MarginBottom(0), TextAlignRight, MaxWidth(350), MarginLeft(auto), BoxSizingBorderBox)
                                {
                                    new div(BackgroundColor(rgb(70, 95, 255)), BorderTopRightRadius(2), BorderRadius(8, 2, 8, 8), MaxWidth("max-content"), MarginLeft(auto), BoxSizingBorderBox, Padding(8, 12))
                                    {
                                        new p(Color(rgb(255, 255, 255)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                        {
                                            "If don’t like something, I’ll stay away from it."
                                        }
                                    },
                                    new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(8), Margin(8, 0, 0), BoxSizingBorderBox)
                                    {
                                        "2 hours ago"
                                    }
                                },
                                new div(MarginTop(32), MarginBottom(0), MaxWidth(350), BoxSizingBorderBox)
                                {
                                    new FlexRow(Gap(16), WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                    {
                                        new div(BorderRadius(9999), MaxWidth(40), WidthFull, Height(40), BoxSizingBorderBox)
                                        {
                                            new img(Src("src/images/user/user-17.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(40), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox)
                                        },
                                        new div(BoxSizingBorderBox)
                                        {
                                            new div(BackgroundColor(rgb(242, 244, 247)), BorderTopLeftRadius(2), BorderRadius(2, 8, 8), BoxSizingBorderBox, Padding(8, 12))
                                            {
                                                new p(Color(rgb(29, 41, 57)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                {
                                                    "I want more detailed information."
                                                }
                                            },
                                            new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(8), Margin(8, 0, 0), BoxSizingBorderBox)
                                            {
                                                "Lindsey, 2 hours ago"
                                            }
                                        }
                                    }
                                },
                                new div(MarginTop(32), MarginBottom(0), TextAlignRight, MaxWidth(350), MarginLeft(auto), BoxSizingBorderBox)
                                {
                                    new div(BackgroundColor(rgb(70, 95, 255)), BorderTopRightRadius(2), BorderRadius(8, 2, 8, 8), MaxWidth("max-content"), MarginLeft(auto), BoxSizingBorderBox, Padding(8, 12))
                                    {
                                        new p(Color(rgb(255, 255, 255)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                        {
                                            "If don’t like something, I’ll stay away from it."
                                        }
                                    },
                                    new div(BackgroundColor(rgb(70, 95, 255)), BorderTopRightRadius(2), BorderRadius(8, 2, 8, 8), MaxWidth("max-content"), MarginTop(8), MarginLeft(auto), BoxSizingBorderBox, Padding(8, 12))
                                    {
                                        new p(Color(rgb(255, 255, 255)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                        {
                                            "They got there early, and got really good seats."
                                        }
                                    },
                                    new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(8), Margin(8, 0, 0), BoxSizingBorderBox)
                                    {
                                        "2 hours ago"
                                    }
                                },
                                new div(MarginTop(32), MarginBottom(0), MaxWidth(350), BoxSizingBorderBox)
                                {
                                    new FlexRow(Gap(16), WebkitBoxAlign("start"), AlignItemsFlexStart, BoxSizingBorderBox)
                                    {
                                        new div(BorderRadius(9999), MaxWidth(40), WidthFull, Height(40), BoxSizingBorderBox)
                                        {
                                            new img(Src("src/images/user/user-17.jpg"), Alt("profile"), ObjectPosition("50% 50%"), ObjectFitCover, BorderRadius(9999), OverflowHidden, WidthFull, Height(40), MaxWidth("100%"), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox)
                                        },
                                        new div(BoxSizingBorderBox)
                                        {
                                            new div(BorderRadius(8), OverflowHidden, MaxWidth(270), WidthFull, MarginBottom(8), BoxSizingBorderBox)
                                            {
                                                new img(Src("src/images/chat/chat.jpg"), Alt("chat"), MaxWidth("100%"), HeightAuto, DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox)
                                            },
                                            new div(BackgroundColor(rgb(242, 244, 247)), BorderTopLeftRadius(2), BorderRadius(2, 8, 8), MaxWidth("max-content"), BoxSizingBorderBox, Padding(8, 12))
                                            {
                                                new p(Color(rgb(29, 41, 57)), FontSize14, LineHeight20, Margin(0), BoxSizingBorderBox)
                                                {
                                                    "Please preview the image"
                                                }
                                            },
                                            new p(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, MarginTop(8), Margin(8, 0, 0), BoxSizingBorderBox)
                                            {
                                                "Lindsey, 2 hours ago"
                                            }
                                        }
                                    }
                                }
                            },
                            new div(Padding(12), BorderColor(rgb(228, 231, 236)), BorderTopWidth("1px"), Bottom(0), PositionSticky, BorderWidth("1px 0px 0px"), BorderStyle(solid))
                            {
                                new form(WebkitBoxPack("justify"), JustifyContentSpaceBetween, AlignItemsCenter, DisplayFlex, BoxSizingBorderBox)
                                {
                                    new div(WidthFull, PositionRelative, BoxSizingBorderBox)
                                    {
                                        new button(Left(12), Color(rgb(102, 112, 133)), Transform("matrix(1, 0, 0, 1, 0, -12)"), Top(18), PositionAbsolute, CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2ZM3.5 12C3.5 7.30558 7.30558 3.5 12 3.5C16.6944 3.5 20.5 7.30558 20.5 12C20.5 16.6944 16.6944 20.5 12 20.5C7.30558 20.5 3.5 16.6944 3.5 12ZM10.0001 9.23256C10.0001 8.5422 9.44042 7.98256 8.75007 7.98256C8.05971 7.98256 7.50007 8.5422 7.50007 9.23256V9.23266C7.50007 9.92301 8.05971 10.4827 8.75007 10.4827C9.44042 10.4827 10.0001 9.92301 10.0001 9.23266V9.23256ZM15.2499 7.98256C15.9403 7.98256 16.4999 8.5422 16.4999 9.23256V9.23266C16.4999 9.92301 15.9403 10.4827 15.2499 10.4827C14.5596 10.4827 13.9999 9.92301 13.9999 9.23266V9.23256C13.9999 8.5422 14.5596 7.98256 15.2499 7.98256ZM9.23014 13.7116C8.97215 13.3876 8.5003 13.334 8.17625 13.592C7.8522 13.85 7.79865 14.3219 8.05665 14.6459C8.97846 15.8037 10.4026 16.5481 12 16.5481C13.5975 16.5481 15.0216 15.8037 15.9434 14.6459C16.2014 14.3219 16.1479 13.85 15.8238 13.592C15.4998 13.334 15.0279 13.3876 14.7699 13.7116C14.1205 14.5274 13.1213 15.0481 12 15.0481C10.8788 15.0481 9.87961 14.5274 9.23014 13.7116Z",
                                                    fill     = ""
                                                }
                                            }
                                        },
                                        new input(input.Type("text"), input.Placeholder("Type a message"), Outline("rgba(0, 0, 0, 0) solid 2px"), OutlineOffset("2px"), Color(rgb(29, 41, 57)), FontSize14, LineHeight20, PaddingRight(20), PaddingLeft(48), BackgroundColor("rgba(0, 0, 0, 0)"), BorderStyle(none), WidthFull, Height(36), Appearance(none), BorderColor(rgb(102, 112, 133)), BorderWidth(0), BorderRadius(0), Padding(8, 20, 8, 48), FontFamily("Outfit, sans-serif"), FontWeight400, LetterSpacingNormal, Margin(0), BoxSizingBorderBox)
                                    },
                                    new FlexRow(WebkitBoxAlign("center"), AlignItemsCenter, BoxSizingBorderBox)
                                    {
                                        new button(Color(rgb(102, 112, 133)), MarginRight(8), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0, 8, 0, 0), Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M12.9522 14.4422C12.9522 14.452 12.9524 14.4618 12.9527 14.4714V16.1442C12.9527 16.6699 12.5265 17.0961 12.0008 17.0961C11.475 17.0961 11.0488 16.6699 11.0488 16.1442V6.15388C11.0488 5.73966 10.7131 5.40388 10.2988 5.40388C9.88463 5.40388 9.54885 5.73966 9.54885 6.15388V16.1442C9.54885 17.4984 10.6466 18.5961 12.0008 18.5961C13.355 18.5961 14.4527 17.4983 14.4527 16.1442V6.15388C14.4527 6.14308 14.4525 6.13235 14.452 6.12166C14.4347 3.84237 12.5817 2 10.2983 2C8.00416 2 6.14441 3.85976 6.14441 6.15388V14.4422C6.14441 14.4492 6.1445 14.4561 6.14469 14.463V16.1442C6.14469 19.3783 8.76643 22 12.0005 22C15.2346 22 17.8563 19.3783 17.8563 16.1442V9.55775C17.8563 9.14354 17.5205 8.80775 17.1063 8.80775C16.6921 8.80775 16.3563 9.14354 16.3563 9.55775V16.1442C16.3563 18.5498 14.4062 20.5 12.0005 20.5C9.59485 20.5 7.64469 18.5498 7.64469 16.1442V9.55775C7.64469 9.55083 7.6446 9.54393 7.64441 9.53706L7.64441 6.15388C7.64441 4.68818 8.83259 3.5 10.2983 3.5C11.764 3.5 12.9522 4.68818 12.9522 6.15388L12.9522 14.4422Z",
                                                    fill     = ""
                                                }
                                            }
                                        },
                                        new button(Color(rgb(102, 112, 133)), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Margin(0), Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Stroke(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new rect(BoxSizingBorderBox)
                                                {
                                                    x           = 7,
                                                    y           = 2.75,
                                                    width       = 10,
                                                    height      = 12.5,
                                                    rx          = 5,
                                                    stroke      = "",
                                                    strokeWidth = 1.5
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d             = "M20 10.25C20 14.6683 16.4183 18.25 12 18.25C7.58172 18.25 4 14.6683 4 10.25",
                                                    stroke        = "",
                                                    strokeWidth   = 1.5,
                                                    strokeLinecap = "round"
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d              = "M10 21.25H14",
                                                    stroke         = "",
                                                    strokeWidth    = 1.5,
                                                    strokeLinecap  = "round",
                                                    strokeLinejoin = "round"
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d              = "M12 18.25L12 21.25",
                                                    stroke         = "",
                                                    strokeWidth    = 1.5,
                                                    strokeLinecap  = "round",
                                                    strokeLinejoin = "round"
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d              = "M12 7.5L12 10.5",
                                                    stroke         = "",
                                                    strokeWidth    = 1.5,
                                                    strokeLinecap  = "round",
                                                    strokeLinejoin = "round"
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d              = "M14.5 8.25L14.5 9.75",
                                                    stroke         = "",
                                                    strokeWidth    = 1.5,
                                                    strokeLinecap  = "round",
                                                    strokeLinejoin = "round"
                                                },
                                                new path(BoxSizingBorderBox)
                                                {
                                                    d              = "M9.5 8.25L9.5 9.75",
                                                    stroke         = "",
                                                    strokeWidth    = 1.5,
                                                    strokeLinecap  = "round",
                                                    strokeLinejoin = "round"
                                                }
                                            }
                                        },
                                        new button(MarginLeft(20), Color(rgb(255, 255, 255)), BackgroundColor(rgb(70, 95, 255)), BorderRadius(8), JustifyContentCenter, AlignItemsCenter, Width("2.25rem"), Height(36), DisplayFlex, CursorPointer, Appearance("button"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), FontSize16, FontWeight400, LineHeight24, LetterSpacingNormal, Padding(0), BoxSizingBorderBox)
                                        {
                                            new svg(ViewBox(0, 0, 20, 20), Fill(none), svg.Size(20), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                            {
                                                new path(BoxSizingBorderBox)
                                                {
                                                    fillRule = "evenodd",
                                                    clipRule = "evenodd",
                                                    d        = "M4.98481 2.44399C3.11333 1.57147 1.15325 3.46979 1.96543 5.36824L3.82086 9.70527C3.90146 9.89367 3.90146 10.1069 3.82086 10.2953L1.96543 14.6323C1.15326 16.5307 3.11332 18.4291 4.98481 17.5565L16.8184 12.0395C18.5508 11.2319 18.5508 8.76865 16.8184 7.961L4.98481 2.44399ZM3.34453 4.77824C3.0738 4.14543 3.72716 3.51266 4.35099 3.80349L16.1846 9.32051C16.762 9.58973 16.762 10.4108 16.1846 10.68L4.35098 16.197C3.72716 16.4879 3.0738 15.8551 3.34453 15.2223L5.19996 10.8853C5.21944 10.8397 5.23735 10.7937 5.2537 10.7473L9.11784 10.7473C9.53206 10.7473 9.86784 10.4115 9.86784 9.99726C9.86784 9.58304 9.53206 9.24726 9.11784 9.24726L5.25157 9.24726C5.2358 9.20287 5.2186 9.15885 5.19996 9.11528L3.34453 4.77824Z",
                                                    fill     = "white"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }

    class HeaderBar : Component
    {
        protected override Element render()
        {
            return new FlexRow(JustifyContentSpaceBetween, AlignItemsCenter, FlexGrow(1), PaddingLeftRight(24), Background(White))
            {
                new HeaderBar_SearchCommand(),
                new FlexRow("menuToggle ? 'flex' : 'hidden'", BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px"), WebkitBoxPack("end"), JustifyContentFlexEnd, Gap(16), AlignItemsCenter, WidthFull, BoxSizingBorderBox, Padding(16, 0))
                {
                    new HeaderBar_NotificationButton(),
                    new HeaderBar_UserButton()
                }
            };
        }

        class HeaderBar_NotificationButton : Component
        {
            protected override Element render()
            {
                return new FlexRow(Gap(12), AlignItemsCenter, BoxSizingBorderBox)
                {
                    new div(PositionRelative, BoxSizingBorderBox)
                    {
                        new button(TransitionProperty("color, background-color, border-color, text-decoration-color, fill, stroke"), TransitionTimingFunction("cubic-bezier(0.4, 0, 0.2, 1)"), TransitionDuration("0.15s"), Color(rgb(102, 112, 133)), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(9999), JustifyContentCenter, AlignItemsCenter, Width("2.75rem"), Height(44), DisplayFlex, PositionRelative, CursorPointer, Margin(0), Padding(0), BorderStyle(solid))
                        {
                            Hover(Background(Gray200)),

                            new span("!notifying ? 'hidden' : 'flex'", BackgroundColor(rgb(253, 133, 58)), BorderRadius(9999), Width("0.5rem"), Height(8), DisplayFlex, ZIndex("1"), Top(2), Right(0), PositionAbsolute, BoxSizingBorderBox)
                            {
                                new span(Opacity(0.0821308), BackgroundColor(rgb(253, 133, 58)), BorderRadius(9999), Animation("1s cubic-bezier(0, 0, 0.2, 1) 0s infinite normal none running ping"), WidthFull, Height(8), DisplayFlex, ZIndex("-1"), PositionAbsolute, BoxSizingBorderBox)
                            },
                            new svg(ViewBox(0, 0, 20, 20), Fill(none), svg.Size(20), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                            {
                                new path(BoxSizingBorderBox)
                                {
                                    fillRule = "evenodd",
                                    clipRule = "evenodd",
                                    d        = "M10.75 2.29248C10.75 1.87827 10.4143 1.54248 10 1.54248C9.58583 1.54248 9.25004 1.87827 9.25004 2.29248V2.83613C6.08266 3.20733 3.62504 5.9004 3.62504 9.16748V14.4591H3.33337C2.91916 14.4591 2.58337 14.7949 2.58337 15.2091C2.58337 15.6234 2.91916 15.9591 3.33337 15.9591H4.37504H15.625H16.6667C17.0809 15.9591 17.4167 15.6234 17.4167 15.2091C17.4167 14.7949 17.0809 14.4591 16.6667 14.4591H16.375V9.16748C16.375 5.9004 13.9174 3.20733 10.75 2.83613V2.29248ZM14.875 14.4591V9.16748C14.875 6.47509 12.6924 4.29248 10 4.29248C7.30765 4.29248 5.12504 6.47509 5.12504 9.16748V14.4591H14.875ZM8.00004 17.7085C8.00004 18.1228 8.33583 18.4585 8.75004 18.4585H11.25C11.6643 18.4585 12 18.1228 12 17.7085C12 17.2943 11.6643 16.9585 11.25 16.9585H8.75004C8.33583 16.9585 8.00004 17.2943 8.00004 17.7085Z",
                                    fill     = ""
                                }
                            }
                        }
                    }
                };
            }
        }

        class HeaderBar_SearchCommand : Component
        {
            protected override Element render()
            {
                return new FlexRow( PaddingLeftRight(12), AlignItemsCenter, WidthFull, Gap(8), Border(1,solid,rgb(228, 231, 236)),  BorderRadius(8))
                {
                    SvgIcon.Search,
                    new input(WidthFull, Color(rgb(29, 41, 57)), BorderNone, Background(none), FontSize14, LineHeight20,  Height(44),  FontFamily("Outfit, sans-serif"))
                    {
                        type = "text",
                        placeholder = "Search or type command...",
                        style =
                        {
                            Focus(BorderNone, OutlineNone)
                        }
                    },
                    new FlexRow(Color(rgb(102, 112, 133)),  FontSize12, LineHeight16, BackgroundColor(rgb(249, 250, 251)), Border(1,solid,rgb(228, 231, 236)), BorderRadius(8), Gap(2), AlignItemsCenter,  CursorPointer, FontFamily("Outfit, sans-serif"), FontWeight400, Padding(5, 7))
                    {
                        new span
                        {
                            "⌘"
                        },
                        new span
                        {
                            "K"
                        }
                    }
                };
            }
        }

        class HeaderBar_UserButton : Component
        {
            protected override Element render()
            {
                return new div(PositionRelative, BoxSizingBorderBox)
                {
                    new a(Href("#"), Color(rgb(52, 64, 84)), AlignItemsCenter, DisplayFlex, TextDecoration("none solid rgb(52, 64, 84)"), BoxSizingBorderBox)
                    {
                        new span(BorderRadius(9999), OverflowHidden, Width("2.75rem"), Height(44), MarginRight(12), BoxSizingBorderBox)
                        {
                            new img(Src(DummySrc(44)), Alt("User"), MaxWidth("100%"), HeightAuto, DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox)
                        },
                        new span(FontWeight500, FontSize14, LineHeight20, DisplayBlock, MarginRight(4), BoxSizingBorderBox)
                        {
                            "Test User"
                        },
                        new svg(svg.Width(18), svg.Height(20), ViewBox(0, 0, 18, 20), Fill(none), Stroke(rgb(102, 112, 133)))
                        {
                            new path(BoxSizingBorderBox)
                            {
                                d              = "M4.3125 8.65625L9 13.3437L13.6875 8.65625",
                                stroke         = "",
                                strokeWidth    = 1.5,
                                strokeLinecap  = "round",
                                strokeLinejoin = "round"
                            }
                        }
                    },
                    new div(DisplayNone, BoxShadow("rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(16, 24, 40, 0.08) 0px 12px 16px -4px, rgba(16, 24, 40, 0.03) 0px 4px 6px -2px"), Padding(12), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderWidth(1), BorderRadius(16), WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), FlexDirectionColumn, Width(260), MarginTop(17), Right(0), PositionAbsolute, BorderStyle(solid))
                    {
                        new div(BoxSizingBorderBox)
                        {
                            new span(Color(rgb(52, 64, 84)), FontWeight500, FontSize14, LineHeight20, DisplayBlock, BoxSizingBorderBox)
                            {
                                "Emirhan Boruch"
                            },
                            new span(Color(rgb(102, 112, 133)), FontSize12, LineHeight18, DisplayBlock, MarginTop(2), BoxSizingBorderBox)
                            {
                                "emirhanboruch51@gmail.com"
                            }
                        },
                        new ul(PaddingTop(16), PaddingBottom(12), BorderColor(rgb(228, 231, 236)), BorderBottomWidth("1px"), Gap(4), WebkitBoxOrient("vertical"), WebkitBoxDirection("normal"), FlexDirectionColumn, DisplayFlex, ListStyle("outside none none"), Margin(0), BorderWidth("0px 0px 1px"), BorderStyle(solid))
                        {
                            new li(BoxSizingBorderBox)
                            {
                                new a(Href("profile.html"), Color(rgb(52, 64, 84)), FontWeight500, FontSize14, LineHeight20, BorderRadius(8), Gap(12), AlignItemsCenter, DisplayFlex, TextDecoration("none solid rgb(52, 64, 84)"), BoxSizingBorderBox, Padding(8, 12))
                                {
                                    new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                    {
                                        new path(BoxSizingBorderBox)
                                        {
                                            fillRule = "evenodd",
                                            clipRule = "evenodd",
                                            d        = "M12 3.5C7.30558 3.5 3.5 7.30558 3.5 12C3.5 14.1526 4.3002 16.1184 5.61936 17.616C6.17279 15.3096 8.24852 13.5955 10.7246 13.5955H13.2746C15.7509 13.5955 17.8268 15.31 18.38 17.6167C19.6996 16.119 20.5 14.153 20.5 12C20.5 7.30558 16.6944 3.5 12 3.5ZM17.0246 18.8566V18.8455C17.0246 16.7744 15.3457 15.0955 13.2746 15.0955H10.7246C8.65354 15.0955 6.97461 16.7744 6.97461 18.8455V18.856C8.38223 19.8895 10.1198 20.5 12 20.5C13.8798 20.5 15.6171 19.8898 17.0246 18.8566ZM2 12C2 6.47715 6.47715 2 12 2C17.5228 2 22 6.47715 22 12C22 17.5228 17.5228 22 12 22C6.47715 22 2 17.5228 2 12ZM11.9991 7.25C10.8847 7.25 9.98126 8.15342 9.98126 9.26784C9.98126 10.3823 10.8847 11.2857 11.9991 11.2857C13.1135 11.2857 14.0169 10.3823 14.0169 9.26784C14.0169 8.15342 13.1135 7.25 11.9991 7.25ZM8.48126 9.26784C8.48126 7.32499 10.0563 5.75 11.9991 5.75C13.9419 5.75 15.5169 7.32499 15.5169 9.26784C15.5169 11.2107 13.9419 12.7857 11.9991 12.7857C10.0563 12.7857 8.48126 11.2107 8.48126 9.26784Z",
                                            fill     = ""
                                        }
                                    },
                                    @"
                            Edit profile
                        "
                                }
                            },
                            new li(BoxSizingBorderBox)
                            {
                                new a(Href("messages.html"), Color(rgb(52, 64, 84)), FontWeight500, FontSize14, LineHeight20, BorderRadius(8), Gap(12), AlignItemsCenter, DisplayFlex, TextDecoration("none solid rgb(52, 64, 84)"), BoxSizingBorderBox, Padding(8, 12))
                                {
                                    new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                    {
                                        new path(BoxSizingBorderBox)
                                        {
                                            fillRule = "evenodd",
                                            clipRule = "evenodd",
                                            d        = "M10.4858 3.5L13.5182 3.5C13.9233 3.5 14.2518 3.82851 14.2518 4.23377C14.2518 5.9529 16.1129 7.02795 17.602 6.1682C17.9528 5.96567 18.4014 6.08586 18.6039 6.43667L20.1203 9.0631C20.3229 9.41407 20.2027 9.86286 19.8517 10.0655C18.3625 10.9253 18.3625 13.0747 19.8517 13.9345C20.2026 14.1372 20.3229 14.5859 20.1203 14.9369L18.6039 17.5634C18.4013 17.9142 17.9528 18.0344 17.602 17.8318C16.1129 16.9721 14.2518 18.0471 14.2518 19.7663C14.2518 20.1715 13.9233 20.5 13.5182 20.5H10.4858C10.0804 20.5 9.75182 20.1714 9.75182 19.766C9.75182 18.0461 7.88983 16.9717 6.40067 17.8314C6.04945 18.0342 5.60037 17.9139 5.39767 17.5628L3.88167 14.937C3.67903 14.586 3.79928 14.1372 4.15026 13.9346C5.63949 13.0748 5.63946 10.9253 4.15025 10.0655C3.79926 9.86282 3.67901 9.41401 3.88165 9.06303L5.39764 6.43725C5.60034 6.08617 6.04943 5.96581 6.40065 6.16858C7.88982 7.02836 9.75182 5.9539 9.75182 4.23399C9.75182 3.82862 10.0804 3.5 10.4858 3.5ZM13.5182 2L10.4858 2C9.25201 2 8.25182 3.00019 8.25182 4.23399C8.25182 4.79884 7.64013 5.15215 7.15065 4.86955C6.08213 4.25263 4.71559 4.61859 4.0986 5.68725L2.58261 8.31303C1.96575 9.38146 2.33183 10.7477 3.40025 11.3645C3.88948 11.647 3.88947 12.3531 3.40026 12.6355C2.33184 13.2524 1.96578 14.6186 2.58263 15.687L4.09863 18.3128C4.71562 19.3814 6.08215 19.7474 7.15067 19.1305C7.64015 18.8479 8.25182 19.2012 8.25182 19.766C8.25182 20.9998 9.25201 22 10.4858 22H13.5182C14.7519 22 15.7518 20.9998 15.7518 19.7663C15.7518 19.2015 16.3632 18.8487 16.852 19.1309C17.9202 19.7476 19.2862 19.3816 19.9029 18.3134L21.4193 15.6869C22.0361 14.6185 21.6701 13.2523 20.6017 12.6355C20.1125 12.3531 20.1125 11.647 20.6017 11.3645C21.6701 10.7477 22.0362 9.38152 21.4193 8.3131L19.903 5.68667C19.2862 4.61842 17.9202 4.25241 16.852 4.86917C16.3632 5.15138 15.7518 4.79856 15.7518 4.23377C15.7518 3.00024 14.7519 2 13.5182 2ZM9.6659 11.9999C9.6659 10.7103 10.7113 9.66493 12.0009 9.66493C13.2905 9.66493 14.3359 10.7103 14.3359 11.9999C14.3359 13.2895 13.2905 14.3349 12.0009 14.3349C10.7113 14.3349 9.6659 13.2895 9.6659 11.9999ZM12.0009 8.16493C9.88289 8.16493 8.1659 9.88191 8.1659 11.9999C8.1659 14.1179 9.88289 15.8349 12.0009 15.8349C14.1189 15.8349 15.8359 14.1179 15.8359 11.9999C15.8359 9.88191 14.1189 8.16493 12.0009 8.16493Z",
                                            fill     = ""
                                        }
                                    },
                                    @"
                            Account settings
                        "
                                }
                            },
                            new li(BoxSizingBorderBox)
                            {
                                new a(Href("settings.html"), Color(rgb(52, 64, 84)), FontWeight500, FontSize14, LineHeight20, BorderRadius(8), Gap(12), AlignItemsCenter, DisplayFlex, TextDecoration("none solid rgb(52, 64, 84)"), BoxSizingBorderBox, Padding(8, 12))
                                {
                                    new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                                    {
                                        new path(BoxSizingBorderBox)
                                        {
                                            fillRule = "evenodd",
                                            clipRule = "evenodd",
                                            d        = "M3.5 12C3.5 7.30558 7.30558 3.5 12 3.5C16.6944 3.5 20.5 7.30558 20.5 12C20.5 16.6944 16.6944 20.5 12 20.5C7.30558 20.5 3.5 16.6944 3.5 12ZM12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2ZM11.0991 7.52507C11.0991 8.02213 11.5021 8.42507 11.9991 8.42507H12.0001C12.4972 8.42507 12.9001 8.02213 12.9001 7.52507C12.9001 7.02802 12.4972 6.62507 12.0001 6.62507H11.9991C11.5021 6.62507 11.0991 7.02802 11.0991 7.52507ZM12.0001 17.3714C11.5859 17.3714 11.2501 17.0356 11.2501 16.6214V10.9449C11.2501 10.5307 11.5859 10.1949 12.0001 10.1949C12.4143 10.1949 12.7501 10.5307 12.7501 10.9449V16.6214C12.7501 17.0356 12.4143 17.3714 12.0001 17.3714Z",
                                            fill     = ""
                                        }
                                    },
                                    @"
                            Support
                        "
                                }
                            }
                        },
                        new button(Color(rgb(52, 64, 84)), FontWeight500, FontSize14, LineHeight20, BorderRadius(8), Gap(12), AlignItemsCenter, DisplayFlex, MarginTop(12), CursorPointer, Appearance("button"), BackgroundColor("rgba(0, 0, 0, 0)"), BackgroundImage(none), TextTransform(none), FontFamily("Outfit, sans-serif"), LetterSpacingNormal, Margin(12, 0, 0), Padding(8, 12), BoxSizingBorderBox)
                        {
                            new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Fill(rgb(102, 112, 133)), DisplayBlock, VerticalAlignMiddle, BoxSizingBorderBox, TouchAction(none))
                            {
                                new path(BoxSizingBorderBox)
                                {
                                    fillRule = "evenodd",
                                    clipRule = "evenodd",
                                    d        = "M15.1007 19.247C14.6865 19.247 14.3507 18.9112 14.3507 18.497L14.3507 14.245H12.8507V18.497C12.8507 19.7396 13.8581 20.747 15.1007 20.747H18.5007C19.7434 20.747 20.7507 19.7396 20.7507 18.497L20.7507 5.49609C20.7507 4.25345 19.7433 3.24609 18.5007 3.24609H15.1007C13.8581 3.24609 12.8507 4.25345 12.8507 5.49609V9.74501L14.3507 9.74501V5.49609C14.3507 5.08188 14.6865 4.74609 15.1007 4.74609L18.5007 4.74609C18.9149 4.74609 19.2507 5.08188 19.2507 5.49609L19.2507 18.497C19.2507 18.9112 18.9149 19.247 18.5007 19.247H15.1007ZM3.25073 11.9984C3.25073 12.2144 3.34204 12.4091 3.48817 12.546L8.09483 17.1556C8.38763 17.4485 8.86251 17.4487 9.15549 17.1559C9.44848 16.8631 9.44863 16.3882 9.15583 16.0952L5.81116 12.7484L16.0007 12.7484C16.4149 12.7484 16.7507 12.4127 16.7507 11.9984C16.7507 11.5842 16.4149 11.2484 16.0007 11.2484L5.81528 11.2484L9.15585 7.90554C9.44864 7.61255 9.44847 7.13767 9.15547 6.84488C8.86248 6.55209 8.3876 6.55226 8.09481 6.84525L3.52309 11.4202C3.35673 11.5577 3.25073 11.7657 3.25073 11.9984Z",
                                    fill     = ""
                                }
                            },
                            @"

                    Sign out
                "
                        }
                    }
                };
            }
        }
    }

    class SvgIcon : PureComponent
    {
        public static SvgIcon Chat => new SvgIcon(nameof(Chat));
        
        public static SvgIcon Dashboard => new SvgIcon(nameof(Dashboard));
        
        public static SvgIcon ArrowDown => new SvgIcon(nameof(ArrowDown));
        
        public static SvgIcon UserProfile => new SvgIcon(nameof(UserProfile));

        public static SvgIcon Search => new SvgIcon(nameof(Search));
        

        public readonly string Name;

        public SvgIcon([NotNull] string name)
        {
            Name = name;
        }
        
        protected override Element render()
        {
            return Name switch
            {
                nameof(Chat) => new svg(Fill(none), svg.Size(24))
                {
                    new path
                    {
                        fill     = "currentColor",
                        fillRule = "evenodd",
                        d        = "M4 12.096a8 8 0 1 1 8 8H5.06l1.283-1.283a.75.75 0 0 0 0-1.06A7.97 7.97 0 0 1 4 12.096m8-9.5a9.5 9.5 0 0 0-7.227 15.666L2.72 20.315a.75.75 0 0 0 .53 1.28H12a9.5 9.5 0 0 0 0-19m-4.375 8.25a1.25 1.25 0 1 0 0 2.5 1.25 1.25 0 0 0 0-2.5m3.125 1.25a1.25 1.25 0 1 1 1.25 1.25c-.69 0-1.25-.559-1.25-1.25m5.625-1.25a1.25 1.25 0 0 0 0 2.5 1.25 1.25 0 0 0 0-2.5",
                        clipRule = "evenodd"
                    }
                },
                
                nameof(Dashboard) =>new svg(svg.Size(24))
                {
                    new path
                    {
                        fill     = "currentColor",
                        fillRule = "evenodd",
                        d        = "M5.5 3.25A2.25 2.25 0 0 0 3.25 5.5V9a2.25 2.25 0 0 0 2.25 2.25H9A2.25 2.25 0 0 0 11.25 9V5.5A2.25 2.25 0 0 0 9 3.25zM4.75 5.5a.75.75 0 0 1 .75-.75H9a.75.75 0 0 1 .75.75V9a.75.75 0 0 1-.75.75H5.5A.75.75 0 0 1 4.75 9zm.75 7.25A2.25 2.25 0 0 0 3.25 15v3.5a2.25 2.25 0 0 0 2.25 2.25H9a2.25 2.25 0 0 0 2.25-2.25V15A2.25 2.25 0 0 0 9 12.75zM4.75 15a.75.75 0 0 1 .75-.75H9a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-.75.75H5.5a.75.75 0 0 1-.75-.75zm8-9.5A2.25 2.25 0 0 1 15 3.25h3.5a2.25 2.25 0 0 1 2.25 2.25V9a2.25 2.25 0 0 1-2.25 2.25H15A2.25 2.25 0 0 1 12.75 9zM15 4.75a.75.75 0 0 0-.75.75V9c0 .414.336.75.75.75h3.5a.75.75 0 0 0 .75-.75V5.5a.75.75 0 0 0-.75-.75zm0 8A2.25 2.25 0 0 0 12.75 15v3.5A2.25 2.25 0 0 0 15 20.75h3.5a2.25 2.25 0 0 0 2.25-2.25V15a2.25 2.25 0 0 0-2.25-2.25zM14.25 15a.75.75 0 0 1 .75-.75h3.5a.75.75 0 0 1 .75.75v3.5a.75.75 0 0 1-.75.75H15a.75.75 0 0 1-.75-.75z",
                        clipRule = "evenodd"
                    }
                },
                
                nameof(ArrowDown) =>new svg(Fill(none), svg.Size(20), TransitionDuration("0.2s"), TransitionProperty("transform"), TransitionTimingFunction("cubic-bezier(0.4, 0, 0.2, 1)"), Width(1.25 * rem), Height(20), MarginLeft(auto), Border(0, solid, rgb(228, 231, 236)))
                {
                    new path
                    {
                        stroke         = "currentColor",
                        strokeLinecap  = "round",
                        strokeLinejoin = "round",
                        strokeWidth    = 1.5,
                        d              = "M4.792 7.396 10 12.604l5.208-5.208"
                    }
                },
                
                    
                nameof(UserProfile)=> new svg(Fill(none), svg.Size(24))
                {
                    new path
                    {
                        fill     = "currentColor",
                        fillRule = "evenodd",
                        d        = "M12 3.5a8.5 8.5 0 0 0-6.38 14.116 5.25 5.25 0 0 1 5.105-4.02h2.55a5.25 5.25 0 0 1 5.105 4.02A8.5 8.5 0 0 0 12 3.5m5.025 15.357v-.011a3.75 3.75 0 0 0-3.75-3.75h-2.55a3.75 3.75 0 0 0-3.75 3.75v.01A8.46 8.46 0 0 0 12 20.5c1.88 0 3.617-.61 5.025-1.643M2 12C2 6.477 6.477 2 12 2s10 4.477 10 10-4.477 10-10 10S2 17.523 2 12m10-4.75a2.018 2.018 0 1 0 0 4.036 2.018 2.018 0 0 0 0-4.036M8.48 9.268a3.518 3.518 0 1 1 7.036 0 3.518 3.518 0 0 1-7.036 0",
                        clipRule = "evenodd"
                    }
                },
                    
                nameof(Search) =>new svg(ViewBox(0, 0, 20, 20), Fill(none), svg.Size(20), Fill(rgb(102, 112, 133)))
                {
                    new path
                    {
                        fillRule = "evenodd",
                        clipRule = "evenodd",
                        d        = "M3.04175 9.37363C3.04175 5.87693 5.87711 3.04199 9.37508 3.04199C12.8731 3.04199 15.7084 5.87693 15.7084 9.37363C15.7084 12.8703 12.8731 15.7053 9.37508 15.7053C5.87711 15.7053 3.04175 12.8703 3.04175 9.37363ZM9.37508 1.54199C5.04902 1.54199 1.54175 5.04817 1.54175 9.37363C1.54175 13.6991 5.04902 17.2053 9.37508 17.2053C11.2674 17.2053 13.003 16.5344 14.357 15.4176L17.177 18.238C17.4699 18.5309 17.9448 18.5309 18.2377 18.238C18.5306 17.9451 18.5306 17.4703 18.2377 17.1774L15.418 14.3573C16.5365 13.0033 17.2084 11.2669 17.2084 9.37363C17.2084 5.04817 13.7011 1.54199 9.37508 1.54199Z",
                        fill     = ""
                    }
                },
                
                    _ => throw new ArgumentOutOfRangeException(Name)
            };
        }
    }

    class LeftMenu : Component
    {
        protected override Element render()
        {
            var menuItems = new[]
            {
                new
                {
                    Svg = SvgIcon.Chat.Name,
                    Label = "Chat",
                    IsSelected = true
                },
                new
                {
                    Svg        = SvgIcon.UserProfile.Name,
                    Label      = "User",
                    IsSelected = false
                }
            };

            var dashboardButton = new button(DisplayFlex, CursorPointer, BackgroundColor(rgb(242, 244, 247)), Color(rgb(52, 64, 84)), WidthFull, AlignItemsCenter, Gap(12), BorderRadius(8), Padding(8, 12), FontSize14, LineHeight20, FontWeight500, BorderNone)
            {
                new span(DisplayFlexRow, AlignItemsCenter, Color(rgb(52, 64, 84)))
                {
                    SvgIcon.Dashboard,
                },
                new span
                {
                    "Dashboard"
                },
                SvgIcon.ArrowDown
            };
            
            return new aside(Width(290), BackgroundColor(rgb(255, 255, 255)), BorderColor(rgb(228, 231, 236)), BorderRightWidth(1 * px), OverflowYHidden, Height100vh, DisplayFlexColumn, BorderWidth(0, 1, 0, 0), BorderStyle(solid), PaddingLeftRight(20))
            {
                new span(PaddingY(32), FontSize24, FontWeight700)
                {
                    "Company Name"
                },

                new nav
                {
                    new h3(Color(rgb(152, 162, 179)), LineHeight20, FontSize12, MarginBottom(16), FontWeight400)
                    {
                        "MENU"
                    },
                    new ul(DisplayFlexColumn, Gap(16))
                    {
                        new li(BoxSizingBorderBox, ListStyleNone)
                        {
                            dashboardButton,
                            new div(OverflowHidden)
                            {
                                new ul(DisplayFlexColumn, PaddingLeft(36), Gap(4), MarginTop(8), ListStyleNone, Margin(8, 0, 0), Padding(0, 0, 0, 36))
                                {
                                    menuItems.Select(item => new li
                                    {
                                        new FlexRow(WidthFull, AlignItemsCenter, Gap(12), BorderRadius(8), Padding(8, 12), FontSize14, LineHeight20, FontWeight500)
                                        {
                                            new SvgIcon(item.Svg),
                                            new span
                                            {
                                                item.Label
                                            },
                                            
                                            When(item.IsSelected, BackgroundColor(rgb(236, 243, 255)), Color(rgb(70, 95, 255))),
                                            When(!item.IsSelected, Hover(Background(Gray100)))
                                        }
                                    })
                                    
                                   
                                }
                            }
                        }
                    }
                }
            };
        }
    }


    static Element AAAA()
    {
        return new div(Size(200), Background(Yellow100), Hover(Background(Blue200)));
    }
}