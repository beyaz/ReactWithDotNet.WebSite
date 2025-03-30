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
    
    public string UserId { get; init; }

    // @formatter:on
}