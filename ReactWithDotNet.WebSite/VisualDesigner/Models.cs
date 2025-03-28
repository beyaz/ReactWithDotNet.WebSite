namespace ReactWithDotNet.VisualDesigner.Models;

sealed record PropertyInfo
{
    public string Name { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }
}

sealed record PropertyModel
{
    // @formatter:off
    
    public string Name { get; set; }

    public string Value { get; set; }
    
    // @formatter:on
}

sealed record PropertyGroupModel
{
    // @formatter:off
    
    public string Condition { get; set; }

    public List<PropertyModel> Items { get; set; }
    
    // @formatter:on
}

sealed record VisualElementModel
{
    // @formatter:off
    
    public string Tag { get; set; }
    
    public List<PropertyGroupModel> StyleGroups { get; set; } = [];
    
    public List<PropertyModel> PropertyGroups { get; set; }= [];
    
    public string Text { get; set; }
    
    public List<VisualElementModel> Children { get; set; }

    internal bool HasChild => Children?.Count > 0;
    
    // @formatter:on
}

sealed class ComponentModel
{
    // @formatter:off
    
    public string Name { get; set; }

    public string PropsAsJson { get; set; }

    public string StateAsJson { get; set; }
    
    public VisualElementModel RootElement { get; set; }
    
    // @formatter:on
}

sealed class ProjectModel
{
    // @formatter:off
    
    public string Name { get; set; }
    
    public List<ComponentModel> Components { get; set; } = [];
    
    public string OutputDirectoryPath { get; set; }
    
    // @formatter:on
}

sealed class ApplicationModel
{
    public List<ProjectModel> Projects { get; set; } = [];
}