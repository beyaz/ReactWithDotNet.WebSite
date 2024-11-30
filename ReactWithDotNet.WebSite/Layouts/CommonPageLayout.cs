﻿namespace ReactWithDotNet.WebSite;

sealed class CommonPageLayout : Component
{
    protected override Element render()
    {
        return new PageLayout
        {
            new main(PaddingY(48))
            {
                new FlexRowCentered(WidthFull)
                {
                    new FlexColumn(ContainerStyle)
                    {
                        children
                    }
                }
            }
        };
    }
}