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
                Name        = "SampleComponent1",
                PropsAsJson = "{'isActive': true}",
                StateAsJson = "{'user': { 'name': 'Tom', 'year': 41 }}",
                RootElement = new()
                {
                    Tag       = "div",
                    StyleGroups =
                        [
                                    
                            new PropertyGroupModel
                            {
                                Condition = "*",
                                Items =
                                [
                                    new PropertyModel { Name = "padding", Value    = "16" }
                                ]
                            }
                        ],
                    Children =
                    [
                        new()
                        {
                            Tag = "div",
                            StyleGroups =
                                [
                                    
                                    new PropertyGroupModel
                                    {
                                        Condition = "*",
                                        Items =
                                        [
                                            new PropertyModel { Name = "background", Value    = "gray" },
                                            new PropertyModel { Name = "width", Value         = "400" },
                                            new PropertyModel { Name = "height", Value        = "400" },
                                            new PropertyModel { Name = "border-radius", Value = "8" },
                                            new PropertyModel { Name = "gap", Value = "16" },
                                            new PropertyModel { Name = "display", Value       = "flex" }
                                        ]
                                    }
                                ],
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
                                            Condition = "*",
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
                                            Condition = "*",
                                            
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
                Name        = "SampleComponent2",
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
                                    Condition = "*",
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