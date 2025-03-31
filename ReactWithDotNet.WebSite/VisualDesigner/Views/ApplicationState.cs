using System.Collections.Concurrent;

namespace ReactWithDotNet.VisualDesigner.Views;

public enum LeftPanelTab
{
    ElementTree,
    Props,
    State
}

public sealed class ApplicationPreviewInfo
{
    public int Scale { get; set; }
    
    public int Height { get; init; }

    public int Width { get; set; }
}

public sealed class ApplicationState
{
    // @formatter:off

    // APPLICATION STATE
    public required ApplicationPreviewInfo Preview { get; init; }
    
    public int ProjectId { get; set; }
    
    public int ComponentId { get; set; }
    
    public VisualElementModel ComponentRootElement { get; set; }
    
    public bool IsProjectSettingsPopupVisible { get; set; }
    
    public LeftPanelTab LeftPanelSelectedTab { get; set; }
    
    public string JsonText { get; set; }
    
    
    // VISUAL ELEMENT STATE
    
    public string SelectedVisualElementTreeItemPath { get; set; }
    
    public string HoveredVisualElementTreeItemPath { get; set; }
    
    // STYLE
    public int? SelectedStyleGroupIndex { get; set; }
    
    public int? SelectedPropertyIndexInStyleGroup { get; set; }
    
    // PROPS
    public int? SelectedPropertyIndexInProps { get; set; }
    
    public string UserName { get; init; }

    // @formatter:on
}

static class ApplicationStateMemoryCache
{
    internal static readonly ConcurrentDictionary<string, ApplicationState> ApplicationStateCache = new();

    public static ApplicationState GetUserLastState(string userName)
    {
        ApplicationStateCache.TryGetValue(userName, out var state);

        return state;
    }

    public static void SetUserLastState(ApplicationState state)
    {
        ApplicationStateCache.AddOrUpdate(state.UserName, state, (_, _) => state);
    }
}