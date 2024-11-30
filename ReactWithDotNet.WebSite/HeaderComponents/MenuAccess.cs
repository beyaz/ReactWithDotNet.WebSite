﻿namespace ReactWithDotNet.WebSite.HeaderComponents;

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
                    PageLink    = Page.TechnicalDetail.Url,
                    Description = "Technical details of ReactWithDotnet"
                },
                new()
                {
                    Title       = "Modifiers",
                    PageLink    = Page.Modifiers.Url,
                    Description = "What is modifier"
                },
                new()
                {
                    Title       = "React Context",
                    PageLink    = Page.ReactContexts.Url,
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
                    PageLink    = Page.Designer.Url,
                    Description = "Preview components in hotreload mode",
                    SvgFileName = "design.svg"
                },
                new()
                {
                    Title       = "Import Html",
                    PageLink    = Page.LiveEditor.Url,
                    Description = "Import any html / Live Editor",
                    SvgFileName = "import.svg"
                }
            ]
        }
    ];

}