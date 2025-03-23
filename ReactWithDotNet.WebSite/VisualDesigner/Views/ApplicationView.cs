
using static ReactWithDotNet.WebSite.Components.RenderPreview;

namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ApplicationView: Component<ApplicationView.State>
{
    protected override Element render()
    {
        return new FlexRow(Padding(10), SizeFull, Background(Theme.BackgroundColor))
        {
            new FlexColumn
            {
                applicationTopPanel,

                MainContent,
                new ProjectView
                {
                    Model = new()
                    {
                        Name = "Demo Project",
                        Components =
                        [
                            new ComponentModel
                            {
                                Name        = "LoginIcon",
                                PropsAsJson = "{'isActive': true}",
                                StateAsJson = "{'user': { 'name': 'Tom', 'year': 41 }}",
                                RootElement = new()
                                {
                                    Tag = "div",
                                    Children =
                                    [
                                        new() { Tag = "label", Text = "Abc1" },
                                        new() { Tag = "span", Text  = "Abc2" },
                                        new() { Tag = "ul", Text    = "Abc3" },

                                        new()
                                        {
                                            Tag = "div",
                                            Children =
                                            [
                                                new() { Tag = "label", Text = "Abc1" },
                                                new() { Tag = "span", Text  = "Abc2" },
                                                new()
                                                {
                                                    Tag  = "ul",
                                                    Text = "Abc3"
                                                }
                                            ]
                                        }
                                    ]
                                }
                            }
                        ]
                    }
                },
                
                new Style
                {
                    Border(Solid(1, Theme.BorderColor)),
                    SizeFull,
                    Background(Theme.WindowBackgroundColor),
                    BorderRadius(10),
                    BoxShadow(0, 30, 30, 0, rgba(69, 42, 124, 0.15))
                },
                NotificationHost
            }
        };
    }
    
    Element applicationTopPanel()
    {
        return new FlexRow
        {
            new FlexRow(AlignItemsCenter, Gap(5))
            {
                new h3 { "React Visual Designer" }
            },

            new FlexRow(Gap(20))
            {
                //GetEnvironment,
                //new LogoutButton()
            },

            new Style
            {
                JustifyContentSpaceBetween,
                AlignItemsCenter,
                BorderBottom(Solid(1, Theme.BorderColor)),
                Padding(5, 30)
            }
        };
    }

    Element MainContent()
    {
        return new SplitRow
        {
            sizes = [20, 60, 20],
            children =
            {
                PartLeftPanel,
                new div { "preview" },
                new div { "Props" }
            }
        };
    }
    
    Element PartLeftPanel()
    {
        var componentSelector = new MagicInput();

        return new FlexColumn(AlignItemsCenter)
        {
            componentSelector,
            new FlexRow(WidthFull)
            {
                new FlexRowCentered()
                {
                    "Visual Tree"
                },
                new FlexRowCentered()
                {
                    "Definition"
                },
                new FlexRowCentered()
                {
                    "Export"
                }
            },
            
        };
    }
    
    
    internal class State
    {
        public ProjectModel Model { get; set; } = new()
        {
            Name = "Demo Project",
            Components =
            [
                new ComponentModel
                {
                    Name        = "LoginIcon",
                    PropsAsJson = "{'isActive': true}",
                    StateAsJson = "{'user': { 'name': 'Tom', 'year': 41 }}",
                    RootElement = new()
                    {
                        Tag = "div",
                        Children =
                        [
                            new() { Tag = "label", Text = "Abc1" },
                            new() { Tag = "span", Text  = "Abc2" },
                            new() { Tag = "ul", Text    = "Abc3" },

                            new()
                            {
                                Tag = "div",
                                Children =
                                [
                                    new() { Tag = "label", Text = "Abc1" },
                                    new() { Tag = "span", Text  = "Abc2" },
                                    new()
                                    {
                                        Tag  = "ul",
                                        Text = "Abc3"
                                    }
                                ]
                            }
                        ]
                    }
                }
            ]
        };
    }
}