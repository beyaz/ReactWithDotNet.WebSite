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
                    Condition = "state.isWebUser",
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
            },

            new ComponentModel
            {
                Name        = "SiteTitle",
                PropsAsJson = "{'isActive': true}",
                StateAsJson = "{'user': { 'name': 'Tom', 'year': 41 }}",
                RootElement = new()
                {
                    Tag = "div",
                    Children =
                    [
                        new()
                        {
                            Tag  = "span",
                            Text = "Abc2",

                            StyleGroups =
                            [
                                new PropertyGroupModel
                                {
                                    Condition = null,
                                    Items =
                                    [
                                        new PropertyModel { Name = "gap", Value   = "8" },
                                        new PropertyModel { Name = "width", Value = "auto" }
                                    ]
                                },
                                new PropertyGroupModel
                                {
                                    Condition = "hover",
                                    Items =
                                    [
                                        new PropertyModel { Name = "gap", Value   = "4" },
                                        new PropertyModel { Name = "width", Value = "fit-content" }
                                    ]
                                }
                            ]
                        }
                    ]
                }
            }
        ]
    };
}