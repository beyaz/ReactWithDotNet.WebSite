namespace ReactWithDotNet.WebSite.Showcases;



sealed class WhatsAppUIDemo : Component
{
    protected override Element render()
    {
        return new FlexColumn(Width(360), Height(640), Margin("30px auto"), BorderRadius(24), OverflowHidden, FontFamily("-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Inter, sans-serif"), BackgroundColor("#efeae2"), BoxShadow("0 20px 40px rgba(0,0,0,0.25)"))
        {
            new FlexRow(Height(64), Padding("0 16px"), AlignItemsCenter, Gap(12), BackgroundColor("#008069"), Color("#ffffff"), BoxSizingBorderBox)
            {
                new div(Size(36), BorderRadius("50%"), BackgroundColor("rgba(255,255,255,0.3)")),
                new div(Flex(1))
                {
                    new div(FontSize15, FontWeight500)
                    {
                        "Alex"
                    },
                    new div(FontSize12, Opacity(0.85))
                    {
                        "online"
                    }
                },
                new div(FontSize18, Opacity(0.9))
                {
                    "⋮"
                }
            },
            new FlexColumn(Flex(1), Padding(14), Gap(10), BoxSizingBorderBox, OverflowYAuto)
            {
                new div(MaxWidth("75%"), AlignSelfFlexStart, BackgroundColor("#ffffff"), Padding(10, 12), BorderRadius("10px 10px 10px 0"), FontSize14, Color(Gray900))
                {
                    @"
            Hey, are you available right now?
            ",
                    new div(FontSize11, MarginTop(4), Color(Gray500), TextAlignRight)
                    {
                        "10:42"
                    }
                },
                new div(MaxWidth("75%"), AlignSelfFlexEnd, BackgroundColor("#d9fdd3"), Padding(10, 12), BorderRadius("10px 10px 0 10px"), FontSize14, Color(Gray900))
                {
                    @"
            Yes, I am. What’s up?
            ",
                    new div(FontSize11, MarginTop(4), Color(Gray500), TextAlignRight)
                    {
                        "10:43 ✓✓"
                    }
                },
                new div(MaxWidth("75%"), AlignSelfFlexStart, BackgroundColor("#ffffff"), Padding(10, 12), BorderRadius("10px 10px 10px 0"), FontSize14, Color(Gray900))
                {
                    @"
            I wanted to ask you about the dashboard design.
            ",
                    new div(FontSize11, MarginTop(4), Color(Gray500), TextAlignRight)
                    {
                        "10:44"
                    }
                },
                new div(MaxWidth("75%"), AlignSelfFlexEnd, BackgroundColor("#d9fdd3"), Padding(10, 12), BorderRadius("10px 10px 0 10px"), FontSize14, Color(Gray900))
                {
                    @"
            Sure. I think the light theme looks clean and modern.
            ",
                    new div(FontSize11, MarginTop(4), Color(Gray500), TextAlignRight)
                    {
                        "10:45 ✓✓"
                    }
                },
                new div(MaxWidth("75%"), AlignSelfFlexStart, BackgroundColor("#ffffff"), Padding(10, 12), BorderRadius("10px 10px 10px 0"), FontSize14, Color(Gray900))
                {
                    @"
            Great, let’s go with that then.
            ",
                    new div(FontSize11, MarginTop(4), Color(Gray500), TextAlignRight)
                    {
                        "10:46"
                    }
                }
            },
            new FlexRow(Height(62), Padding(10), AlignItemsCenter, Gap(10), BoxSizingBorderBox, BackgroundColor("#f0f2f5"), BorderTop(1, solid, "#e5e7eb"))
            {
                new div(FontSize20, Opacity(0.6))
                {
                    "😊"
                },
                new input(input.Type("text"), input.Placeholder("Type a message"), Flex(1), Height(42), Padding("0 14px"), BorderRadius(21), BorderNone, OutlineNone, BackgroundColor("#ffffff"), FontSize14),
                new FlexRowCentered(Size(42), BorderRadius("50%"), BackgroundColor("#008069"), Color("#ffffff"), FontSize18)
                {
                    "➤"
                }
            }
        };
    }
}