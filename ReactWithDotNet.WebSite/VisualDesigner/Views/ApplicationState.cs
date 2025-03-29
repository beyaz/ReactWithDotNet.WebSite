namespace ReactWithDotNet.VisualDesigner.Views;

public enum LeftPanelTab
{
    ElementTree,
    Settings
}

public enum SettingsTab
{
    Props,
    State,
    Other
}

public sealed class ApplicationState
{
    // @formatter:off

    // APPLICATION STATE
    public int Scale { get; set; }

    public int ScreenHeight { get; init; }

    public int ScreenWidth { get; set; }
    
    public SettingsTab SettingsCurrentTab { get; set; }

    public LeftPanelTab LeftPanelCurrentTab { get; set; }
    
    public string JsonTextInComponentSettings { get; set; }
    
    // VISUAL ELEMENT STATE
    public ProjectModel Project { get; set; }
    
    public string CurrentComponentName { get; set; }
    
    public string CurrentVisualElementTreePath { get; set; }
    
    public string HoveredVisualElementTreeItemPath { get; set; }
    
    // STYLE
    public int? CurrentStyleGroupIndex { get; set; }
    
    public int? CurrentPropertyIndexInStyleGroup { get; set; }
    
    // PROPS
    public int? CurrentPropertyIndexInProps { get; set; }
    
    // @formatter:on
}