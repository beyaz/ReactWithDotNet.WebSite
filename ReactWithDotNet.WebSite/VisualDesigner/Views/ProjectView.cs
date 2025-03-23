namespace ReactWithDotNet.VisualDesigner.Views;

sealed class ProjectView : Component<ProjectView.State>
{
    public ProjectModel Model { get; set; }

    protected override Task constructor()
    {
        InitializeState();

        return Task.CompletedTask;
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        if (Model.Name != state.Model.Name)
        {
            InitializeState();
        }

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new SplitRow
        {
            sizes = [20, 60, 20],
            children =
            {
                new div { "Left" },
                new div { "preview" },
                new div { "Props" }
            }
        };
    }

    void InitializeState()
    {
        state = new()
        {
            Model        = Model,
            InitialModel = Model
        };
    }

    internal class State
    {
        public ProjectModel InitialModel { get; set; }
        public ProjectModel Model { get; set; }
    }
}