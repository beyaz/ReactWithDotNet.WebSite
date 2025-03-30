using Dapper.Contrib.Extensions;

namespace ReactWithDotNet.VisualDesigner.DbModels;

[Table("Project")]
sealed class ProjectEntity
{
    [Key]
    public int Id { get; init; }
    
    public string Name { get; init; }
    
    public string OutputDirectory { get; init; }
}

[Table("Component")]
public sealed record ComponentEntity
{
    // @formatter:off
    
    [Key]
    public int Id { get; init; }
    
    public int ProjectId { get; init; }
    
    public string Name { get; init; }

    public string PropsAsJson { get; init; }

    public string StateAsJson { get; init; }
    
    public string RootElementAsJson { get; init; }

    public string UserName { get; init; }
    
    // @formatter:on
}

[Table("LastUsageInfo")]
public sealed record LastUsageInfoEntity
{
    // @formatter:off
    
    [Key]
    public int Id { get; init; }
    
    public string UserName { get; init; }
    
    public int ProjectId { get; init; }
    
    public int ComponentId { get; init; }
    
    public int Scale { get; init; }

    public int ScreenWidth { get; init; }

    public DateTime AccessTime { get; init; }
    
    // @formatter:on
}