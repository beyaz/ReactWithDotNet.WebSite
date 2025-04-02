namespace ReactWithDotNet.VisualDesigner.Views;

class ProjectCondifModel
{
    public IReadOnlyDictionary<string, string> Colors { get; init; }

    public IReadOnlyDictionary<string, string> Styles { get; init; }
}

static class Plugin
{
    public static StyleModifier TryProcessStyleAttribute(string styleAttribute)
    {
        {
            var (success, name, value) = TryParsePropertyValue(styleAttribute);
            if (success)
            {
                if (name == "color")
                {
                    if (Project.Colors.TryGetValue(value, out var realColor))
                    {
                        return Color(realColor);
                    }
                }
            }
        }

        {
            if (!Project.Styles.TryGetValue(styleAttribute, out var value))
            {
                return null;
            }

            return Style.ParseCss(value);
        }
    }
}