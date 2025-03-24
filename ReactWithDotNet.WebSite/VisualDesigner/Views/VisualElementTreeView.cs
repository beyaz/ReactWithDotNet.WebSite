namespace ReactWithDotNet.VisualDesigner.Views;

sealed class VisualElementTreeView : Component<VisualElementTreeView.State>
{
    public VisualElementModel Model { get; init; }

    public string Name { get; init; }

    public string SelectedPath { get; init; }

    [CustomEvent]
    public Func<string, Task> SelectionChanged { get; init; }

    protected override Task constructor()
    {
        InitializeState();

        return Task.CompletedTask;
    }

    protected override Task OverrideStateFromPropsBeforeRender()
    {
        if (Name != state.InitialName)
        {
            InitializeState();
        }

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new div(CursorPointer, Padding(5))
        {
            ToVisual(state.Model, 0, "0"),
            WidthFull, HeightFull
        };
    }

    void InitializeState()
    {
        state = new()
        {
            Model               = Model,
            SelectedPath        = SelectedPath,
            InitialModel        = Model,
            InitialSelectedPath = SelectedPath,
            InitialName         = Name
        };
    }

    Task OnTreeItemClicked(MouseEvent e)
    {
        state.SelectedPath = e.currentTarget.id;

        DispatchEvent(SelectionChanged, [state.SelectedPath]);

        return Task.CompletedTask;
    }

    IReadOnlyList<Element> ToVisual(VisualElementModel node, int indent, string path)
    {
        var isSelected = state.SelectedPath == path;

        var returnList = new List<Element>
        {
            new FlexRow(AlignItemsCenter, PaddingLeft(indent * 16), Id(path), OnClick(OnTreeItemClicked))
            {
                new div { Text(node.Tag), MarginLeft(5), FontSize13 },

                isSelected ? BackgroundImage(linear_gradient(90, rgb(136, 195, 242), rgb(242, 246, 249))) + BorderRadius(3) : null,

                !isSelected ? Hover(BackgroundImage(linear_gradient(90, rgb(190, 220, 244), rgb(242, 246, 249))) + BorderRadius(3)) : null
            }
        };

        if (node.HasChild is false)
        {
            return returnList;
        }

        for (var i = 0; i < node.Children.Count; i++)
        {
            var child = node.Children[i];

            returnList.AddRange(ToVisual(child, indent + 1, $"{path},{i}"));
        }

        return returnList;
    }

    internal class State
    {
        public VisualElementModel InitialModel { get; init; }

        public string InitialName { get; init; }

        public string InitialSelectedPath { get; set; }

        public VisualElementModel Model { get; init; }

        public string SelectedPath { get; set; }
    }
}