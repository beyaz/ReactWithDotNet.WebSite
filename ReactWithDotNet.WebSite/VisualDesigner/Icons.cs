namespace ReactWithDotNet.VisualDesigner;

sealed class IconClose : PureComponent
{
    protected override Element render()
    {
        return new svg(Fill("currentColor"), ViewBox(0, 0, 18, 18))
        {
            new path
            {
                d = "M8.44 9.5L6 7.06A.75.75 0 1 1 7.06 6L9.5 8.44 11.94 6A.75.75 0 0 1 13 7.06L10.56 9.5 13 11.94A.75.75 0 0 1 11.94 13L9.5 10.56 7.06 13A.75.75 0 0 1 6 11.94L8.44 9.5z"
            }
        };
    }
}

sealed class IconChecked : PureComponent
{
    protected override Element render()
    {
        return new svg(Fill("currentColor"), ViewBox(0, 0, 14, 14))
        {
            new path
            {
                d    = "M4.86199 11.5948C4.78717 11.5923 4.71366 11.5745 4.64596 11.5426C4.57826 11.5107 4.51779 11.4652 4.46827 11.4091L0.753985 7.69483C0.683167 7.64891 0.623706 7.58751 0.580092 7.51525C0.536478 7.44299 0.509851 7.36177 0.502221 7.27771C0.49459 7.19366 0.506156 7.10897 0.536046 7.03004C0.565935 6.95111 0.613367 6.88 0.674759 6.82208C0.736151 6.76416 0.8099 6.72095 0.890436 6.69571C0.970973 6.67046 1.05619 6.66385 1.13966 6.67635C1.22313 6.68886 1.30266 6.72017 1.37226 6.76792C1.44186 6.81567 1.4997 6.8786 1.54141 6.95197L4.86199 10.2503L12.6397 2.49483C12.7444 2.42694 12.8689 2.39617 12.9932 2.40745C13.1174 2.41873 13.2343 2.47141 13.3251 2.55705C13.4159 2.64268 13.4753 2.75632 13.4938 2.87973C13.5123 3.00315 13.4888 3.1292 13.4271 3.23768L5.2557 11.4091C5.20618 11.4652 5.14571 11.5107 5.07801 11.5426C5.01031 11.5745 4.9368 11.5923 4.86199 11.5948Z",
                fill = "currentColor"
            }
        };
    }
}

sealed class IconMinus : PureComponent
{
    protected override Element render()
    {
        return new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
        {
            new path { fill = "currentColor", d = "M12 8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h8c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
        };
    }
}

sealed class IconPlus : PureComponent
{
    protected override Element render()
    {
        return new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
        {
            new path { fill = "currentColor", d = "M12 8.667H8.667V12c0 .367-.3.667-.667.667A.669.669 0 0 1 7.333 12V8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h3.333V4c0-.366.3-.667.667-.667.367 0 .667.3.667.667v3.333H12c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
        };
    }
}

sealed class IconLayers : PureComponent
{
    protected override Element render()
    {
        return new svg( svg.FocusableFalse, Fill("currentColor"), ViewBox(0, 0, 16, 16))
        {
            new path
            {
                fillRule = "evenodd",
                clipRule = "evenodd",
                d        = "M7.65399 0.0817342C7.87195 -0.0272447 8.1285 -0.0272447 8.34646 0.0817342L15.5723 3.69466C15.8346 3.8258 16.0003 4.09387 16.0003 4.38712C16.0003 4.68036 15.8346 4.94844 15.5723 5.07958L8.34646 8.6925C8.1285 8.80148 7.87195 8.80148 7.65399 8.6925L0.428149 5.07958C0.165863 4.94844 0.000183105 4.68036 0.000183105 4.38712C0.000183105 4.09387 0.165863 3.8258 0.428149 3.69466L7.65399 0.0817342ZM2.50554 4.38712L8.00022 7.13446L13.4949 4.38712L8.00022 1.63978L2.50554 4.38712Z"
            },
            new path
            {
                fillRule = "evenodd",
                clipRule = "evenodd",
                d        = "M0.0819038 7.65372C0.273122 7.27128 0.738162 7.11627 1.1206 7.30749L8.00021 10.7473L14.8798 7.30749C15.2623 7.11627 15.7273 7.27128 15.9185 7.65372C16.1097 8.03616 15.9547 8.5012 15.5723 8.69242L8.34644 12.3053C8.12848 12.4143 7.87194 12.4143 7.65398 12.3053L0.428135 8.69242C0.0456986 8.5012 -0.109315 8.03616 0.0819038 7.65372Z"
            },
            new path
            {
                fillRule = "evenodd",
                clipRule = "evenodd",
                d        = "M0.0819038 11.2666C0.273122 10.8842 0.738162 10.7292 1.1206 10.9204L8.00021 14.3602L14.8798 10.9204C15.2623 10.7292 15.7273 10.8842 15.9185 11.2666C16.1097 11.6491 15.9547 12.1141 15.5723 12.3053L8.34644 15.9183C8.12848 16.0272 7.87194 16.0272 7.65398 15.9183L0.428135 12.3053C0.0456986 12.1141 -0.109315 11.6491 0.0819038 11.2666Z"
            }
        };
    }
}


sealed class IconSettings : PureComponent
{
    protected override Element render()
    {
        return new svg(Fill(none), ViewBox(0, 0, 24, 24), Size("1em"))
        {
            new path
            {
                d              = "M5.621 14.963l1.101.172c.813.127 1.393.872 1.333 1.71l-.081 1.137a.811.811 0 00.445.787l.814.4c.292.145.641.09.88-.134l.818-.773a1.55 1.55 0 012.138 0l.818.773a.776.776 0 00.88.135l.815-.402a.808.808 0 00.443-.785l-.08-1.138c-.06-.838.52-1.583 1.332-1.71l1.101-.172a.798.798 0 00.651-.62l.201-.9a.816.816 0 00-.324-.847l-.918-.643a1.634 1.634 0 01-.476-2.132l.555-.988a.824.824 0 00-.068-.907l-.563-.723a.78.78 0 00-.85-.269l-1.064.334a1.567 1.567 0 01-1.928-.949l-.407-1.058a.791.791 0 00-.737-.511l-.903.002a.791.791 0 00-.734.516l-.398 1.045a1.566 1.566 0 01-1.93.956l-1.11-.348a.78.78 0 00-.851.27l-.56.724a.823.823 0 00-.062.91l.568.99c.418.73.213 1.666-.469 2.144l-.907.636a.817.817 0 00-.324.847l.2.9c.072.325.33.57.651.62z",
                stroke         = "currentColor",
                strokeWidth    = 1.5,
                strokeLinecap  = "round",
                strokeLinejoin = "round"
            },
            new path
            {
                d              = "M13.591 10.409a2.25 2.25 0 11-3.183 3.182 2.25 2.25 0 013.183-3.182z",
                stroke         = "currentColor",
                strokeWidth    = 1.5,
                strokeLinecap  = "round",
                strokeLinejoin = "round"
            }
        };
    }
}


sealed class IconSave : PureComponent
{
    protected override Element render()
    {
        return new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Stroke("currentColor"), StrokeWidth("2"), StrokeLinecap("round"), StrokeLinejoin("round"))
        {
            new path
            {
                d = "M17 21H7a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h10l4 4v12a2 2 0 0 1-2 2z"
            },
            new path
            {
                d = "M9 21V12h6v9"
            },
            new path
            {
                d = "M9 3h6v5H9z"
            }
        };
    }
}


sealed class IconExport : PureComponent
{
    protected override Element render()
    {
        return new svg(ViewBox(0, 0, 24, 24), Fill(none), svg.Size(24), Stroke("currentColor"), StrokeWidth("2"), StrokeLinecap("round"), StrokeLinejoin("round"))
        {
            new path
            {
                d = "M5 12v6a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2v-6"
            },
            new polyline(polyline.Points("16 6 12 2 8 6")),
            new line
            {
                x1 = 12,
                y1 = 2,
                x2 = 12,
                y2 = 15
            }
        };
    }
}