﻿namespace ReactWithDotNet.WebSite.Components;

class HamburgerButton : Component
{
    public required bool IsOpen { get; init; }

    public required MouseEventHandler Click { get; init; }

    protected override Element render()
    {
        var transition = Transition("all 0.2s ease 0s");
        
        return new button(DisplayFlexRowCentered, OnClick(Click))
        {
            Border(Solid(1,Theme.grey_300)),
            Background(transparent),
            BorderRadius(10),

            When(IsOpen,Background(Theme.grey_50)),

            Padding(8),
            new svg(svg.Width(16),svg.Height(16), svg.ViewBox(0, 0, 16, 16), svg.Fill(none))
            {
                new rect(transition, When(IsOpen, Transform("translate(6px, -2px) rotateZ(45deg)")))
                {
                    x      = 1,
                    y      = 5,
                    width  = 14,
                    height = 1.5,
                    rx     = 1,
                    fill   = Theme.primary_main
                },
                new rect(transition, When(IsOpen, Transform("translate(-5px, 6px) rotateZ(-45deg)")))
                {
                    x      = 1,
                    y      = 9,
                    width  = 14,
                    height = 1.5,
                    rx     = 1,
                    fill   = Theme.primary_main
                }
            }
        };
    }
}