using Dapper.Contrib.Extensions;

namespace ReactWithDotNet.VisualDesigner.DbModels;

[Table("Project")]
sealed class ProjectEntity
{
    public int RecordId { get; init; }
    
    public string Name { get; init; }
}

[Table("Component")]
public sealed class ComponentEntity
{
    // @formatter:off
    
    public int RecordId { get; init; }
    
    public int ProjectId { get; init; }
    
    public string Name { get; init; }

    public string PropsAsJson { get; init; }

    public string StateAsJson { get; init; }
    
    public string RootElementAsJson { get; init; }

    public string UserName { get; init; }
    
    // @formatter:on
}