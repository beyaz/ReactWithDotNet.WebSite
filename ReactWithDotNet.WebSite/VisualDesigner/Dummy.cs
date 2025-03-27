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
                    Tag       = "div",
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
                                new()
                                {
                                    Tag = "span", Text  = "Abc2" ,
                                    StyleGroups =
                                    [
                                        new PropertyGroupModel
                                        {
                                            Items =
                                            [
                                                new PropertyModel { Name = "background", Value    = "green" },
                                                new PropertyModel { Name = "width", Value         = "50" },
                                                new PropertyModel { Name = "height", Value        = "50" },
                                                new PropertyModel { Name = "border-radius", Value = "4" }
                                            ]
                                        }
                                    ]
                                },
                                new()
                                {
                                    Tag  = "ul",
                                    Text = "Abc3",
                                    StyleGroups =
                                    [
                                        new PropertyGroupModel
                                        {
                                            Items =
                                            [
                                                new PropertyModel { Name = "background", Value = "yellow" },
                                                new PropertyModel { Name = "width", Value      = "100" },
                                                new PropertyModel { Name = "height", Value     = "100" },
                                                new PropertyModel { Name = "border-radius", Value     = "8" }
                                            ]
                                        }
                                    ]
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
                                        new PropertyModel { Name = "gap", Value        = "8" },
                                        new PropertyModel { Name = "width", Value      = "auto" },
                                        new PropertyModel { Name = "background", Value = "yellow" }
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