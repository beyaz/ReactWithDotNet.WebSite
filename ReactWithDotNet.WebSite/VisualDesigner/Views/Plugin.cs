namespace ReactWithDotNet.VisualDesigner.Views;

class PluginModel
{
    public IReadOnlyDictionary<string, string> NamedEmbeddedStyles => new Dictionary<string, string>
    {
        { "bg-strong", "background: #0E121B" },
        { "bg-surface", "background: #222530" },
        { "bg-sub", "background: #CACFD8" },
        { "bg-soft", "background: #E1E4EA" },
        { "bg-weak", "background: #F5F7FA" },
        { "bg-white", "background: #FFFFFF" },
        
        {"heading-05","""
                      font-family: "Host Grotesk";
                      font-size: 24px;
                      font-style: normal;
                      font-weight: 600;
                      line-height:  32px;
                      """}
    };
    
    public IReadOnlyDictionary<string, string> NamedEmbeddedColors => new Dictionary<string, string>
    {
        { "content-weak", "#F5F7FA" }
    };
}
static class Plugin
{
    public static readonly PluginModel Model = new();
    
    public static IReadOnlyList<string> GetStyleAttributeSuggestions(ApplicationState state)
    {
        return Model.NamedEmbeddedStyles.Select(x=>x.Key).ToList();
    }

    public static StyleModifier TryProcessStyleAttribute(string styleAttribute)
    {
        {
            var (success, name, value) = TryParsePropertyValue(styleAttribute);
            if (success)
            {
                if (name == "color")
                {
                    if (Model.NamedEmbeddedColors.TryGetValue(value, out var realColor))
                    {
                        return Color(realColor);
                    }
                }
            }
        }

        {
            if (!Model.NamedEmbeddedStyles.TryGetValue(styleAttribute, out var value))
            {
                return null;
            }

        

            return Style.ParseCss(value);
        }
    }
}