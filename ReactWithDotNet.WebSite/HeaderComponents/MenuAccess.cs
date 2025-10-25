namespace ReactWithDotNet.WebSite.HeaderComponents;

public class Menu
{
    public string Title { get; set; }
    public IReadOnlyList<MenuItem> Children { get; set; }
}
public class MenuItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PageLink { get; set; }
    public string SvgFileName { get; set; } = "doc.svg";
}

static class MenuAccess
{
    public static IReadOnlyList<Menu> MenuList =>
    [
        new()
        {
            Title = "Technical",
            Children =
            [
                new()
                {
                    Title       = "Technical Detail",
                    PageLink    = TechnicalDetail,
                    Description = "Technical details of ReactWithDotnet"
                },
                new()
                {
                    Title       = "Modifiers",
                    PageLink    = Modifiers,
                    Description = "What is modifier"
                },
                new()
                {
                    Title       = "React Context",
                    PageLink    = ReactContexts,
                    Description = "How to implement react contexts"
                }
            ]
        },
            
        new()
        {
            Title = "Helper App",
            Children =
            [
                new()
                {
                    Title       = "Designer",
                    PageLink    = Designer,
                    Description = "Preview components in hotreload mode",
                    SvgFileName = "design.svg"
                },
                new()
                {
                    Title       = "Import Html",
                    PageLink    = LiveEditor,
                    Description = "Import any html / Live Editor",
                    SvgFileName = "import.svg"
                },
                new()
                {
                    Title       = "Made by ReactWithDotNet",
                    PageLink    = MadeBy,
                    Description = "Web sites or apps using ReactWithDotNet",
                    SvgFileName = "import.svg"
                }
            ]
        }
    ];

}