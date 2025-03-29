namespace ReactWithDotNet.VisualDesigner.Views;

static class ApplicationLogic
{
    public static IReadOnlyList<string> GetStyleGroupConditionSuggestions(ApplicationState state)
    {
        return ["MD", "XXL", "state.user.isActive", "MD: state.user.isActive", "XXL: state.user.isActive"];
    }

    public static IReadOnlyList<string> GetProjectNames(ApplicationState state)
    {
        return ApplicationDatabase.GetAllProjects().Select(x => x.Name).ToList();
    }
}