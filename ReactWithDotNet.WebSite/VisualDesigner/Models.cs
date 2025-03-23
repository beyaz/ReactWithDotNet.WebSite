namespace ReactWithDotNet.VisualDesigner;

sealed record PropertyInfo
{
    public string Name { get; init; }

    public IReadOnlyList<string> Suggestions { get; init; }
}

sealed record PropertyModel
{
    public string Condition { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }
}


sealed record VisualElementModel
{
    public List<VisualElementModel> Children { get; set; }

    public List<PropertyModel> Properties { get; set; }

    public List<PropertyModel> StyleAttributes { get; set; }
    public string Tag { get; set; }

    public string Text { get; set; }

    internal bool HasChild => Children?.Count > 0;
}

sealed class ComponentModel
{
    public VisualElementModel RootElement { get; set; }
    
    public string Name { get; set; }

    public string PropsAsJson { get; set; }
    
    public string StateAsJson { get; set; }
}

sealed class ProjectModel
{
    public List<ComponentModel> Components { get; set; } = [];

    public string OutputDirectoryPath { get; set; }
    
    public string Name { get; set; }
}

sealed class ApplicationModel
{
    public List<ProjectModel> Projects { get; set; } = [];
}