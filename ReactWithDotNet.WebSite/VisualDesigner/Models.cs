namespace ReactWithDotNet.VisualDesigner.Models;

public sealed record PropertyInfo
{
    public string Name { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }
}

public sealed record PropertyModel
{
    // @formatter:off
    
    public string Name { get; set; }

    public string Value { get; set; }
    
    // @formatter:on
}

public sealed record PropertyGroupModel
{
    // @formatter:off
    
    public string Condition { get; set; }

    public List<PropertyModel> Items { get; set; }
    
    // @formatter:on
}

public sealed record VisualElementModel
{
    // @formatter:off
    
    public string Tag { get; set; }
    
    public List<PropertyGroupModel> StyleGroups { get; set; } = [];
    
    public List<PropertyModel> Properties { get; set; } = [];
    
    public string Text { get; set; }
    
    public List<VisualElementModel> Children { get; set; }

    internal bool HasChild => Children?.Count > 0;
    
    // @formatter:on
}

public sealed class ComponentModel
{
    // @formatter:off
    
    public string Name { get; set; }

    public string PropsAsJson { get; set; }

    public string StateAsJson { get; set; }
    
    public VisualElementModel RootElement { get; set; }
    
    // @formatter:on
}

public sealed class ProjectModel
{
    // @formatter:off
    
    public string Name { get; set; }
    
    public List<ComponentModel> Components { get; set; } = [];
    
    // @formatter:on
}