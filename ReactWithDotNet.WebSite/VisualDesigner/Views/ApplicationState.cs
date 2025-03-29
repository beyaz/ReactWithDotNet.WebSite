namespace ReactWithDotNet.VisualDesigner.Views;

public enum LeftPanelTab
{
    ElementTree,
    Props,
    State
}

public sealed class ApplicationState
{
    // @formatter:off

    // APPLICATION STATE
    public int Scale { get; set; }

    public int ScreenHeight { get; init; }

    public int ScreenWidth { get; set; }
    
    public int ProjectId { get; set; }
    
    public int ComponentId { get; set; }
    
    public VisualElementModel ComponentRootElement { get; set; }
    
    public bool CurrentProjectSettingsPopupIsVisible { get; set; }
    
    public LeftPanelTab LeftPanelCurrentTab { get; set; }
    
    public string JsonTextInComponentSettings { get; set; }
    
    // VISUAL ELEMENT STATE
    
    public string CurrentVisualElementTreePath { get; set; }
    
    public string HoveredVisualElementTreeItemPath { get; set; }
    
    // STYLE
    public int? CurrentStyleGroupIndex { get; set; }
    
    public int? CurrentPropertyIndexInStyleGroup { get; set; }
    
    // PROPS
    public int? CurrentPropertyIndexInProps { get; set; }
    
    public string UserId { get; init; }

    // @formatter:on
}