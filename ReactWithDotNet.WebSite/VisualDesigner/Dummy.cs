
namespace ReactWithDotNet.VisualDesigner.Models;

static class Dummy
{
    public static ProjectModel ProjectModel => new()
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