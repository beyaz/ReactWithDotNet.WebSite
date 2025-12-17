namespace ReactWithDotNet.WebSite.Showcases;

record BasicDashboardDemoState
{
    public string SelectedMenuId { get; init; }
}

sealed class BasicDashboardDemo : Component<BasicDashboardDemoState>
{
    protected override Element render()
    {
        return new FlexRow(WidthFull, MaxWidth(1200), HeightAuto, Margin("30px auto"), BorderRadius(18), OverflowHidden, FontFamily("-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Inter, sans-serif"), BackgroundColor(Slate50), BoxShadow("0 20px 40px rgba(0,0,0,0.08)"))
        {
            new div(Width(240), Padding(24), BoxSizingBorderBox, Background("linear-gradient(180deg, #ffffff, #f1f5f9)"), BorderRight(1, solid, "#e2e8f0"))
            {
                new div(FontSize18, FontWeight600, MarginBottom(32), Color(Slate900))
                {
                    "Dashboard"
                },
                new div(FontSize14, Padding("10px 0"), Color(Slate700))
                {
                    "Profile"
                },
                new div(FontSize14, Padding("10px 0"), Color(Slate700))
                {
                    "Account"
                },
                new div(FontSize14, Padding("10px 0"), Color(Slate700))
                {
                    "Settings"
                },
                new div(FontSize14, Padding("10px 0"), Color(Slate700))
                {
                    "Security"
                }
            },
            new FlexColumn(Flex(1), BackgroundColor(Slate50))
            {
                new FlexRow(Height(70), Padding("0 28px"), AlignItemsCenter, JustifyContentSpaceBetween, BoxSizingBorderBox, BackgroundColor("#ffffff"), BorderBottom(1, solid, "#e2e8f0"))
                {
                    new div(FontSize16, FontWeight500, Color(Slate900))
                    {
                        "User Settings"
                    },
                    new FlexRow(AlignItemsCenter, Gap(12))
                    {
                        new div(Size(36), BorderRadius("50%"), BackgroundColor(Slate200)),
                        new div(FontSize14, Color(Slate700))
                        {
                            "John Doe"
                        }
                    }
                },
                new div(Flex(1), Padding(28), BoxSizingBorderBox, OverflowAuto)
                {
                    new div(BackgroundColor("#ffffff"), Padding(24), BoxSizingBorderBox, Border(1, solid, Slate200, 14))
                    {
                        new div(FontSize17, FontWeight600, MarginBottom(22), Color(Slate900))
                        {
                            "Profile Information"
                        },
                        new div(Display("grid"), GridTemplateColumns("repeat(2, 1fr)"), Gap(20))
                        {
                            new div
                            {
                                new div(FontSize13, MarginBottom(6), Color(Slate600))
                                {
                                    "First Name"
                                },
                                new input(input.Type("text"), WidthFull, Padding(12, 14), Border(1, solid, "#cbd5f5", 10), BackgroundColor(Slate50), OutlineNone)
                            },
                            new div
                            {
                                new div(FontSize13, MarginBottom(6), Color(Slate600))
                                {
                                    "Last Name"
                                },
                                new input(input.Type("text"), WidthFull, Padding(12, 14), Border(1, solid, "#cbd5f5", 10), BackgroundColor(Slate50), OutlineNone)
                            },
                            new div
                            {
                                new div(FontSize13, MarginBottom(6), Color(Slate600))
                                {
                                    "Email"
                                },
                                new input(input.Type("email"), WidthFull, Padding(12, 14), Border(1, solid, "#cbd5f5", 10), BackgroundColor(Slate50), OutlineNone)
                            },
                            new div
                            {
                                new div(FontSize13, MarginBottom(6), Color(Slate600))
                                {
                                    "Phone"
                                },
                                new input(input.Type("text"), WidthFull, Padding(12, 14), Border(1, solid, "#cbd5f5", 10), BackgroundColor(Slate50), OutlineNone)
                            }
                        }
                    }
                }
            }
        };
    }
}