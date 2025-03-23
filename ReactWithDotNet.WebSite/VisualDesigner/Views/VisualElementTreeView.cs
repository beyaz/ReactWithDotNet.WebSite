namespace ReactWithDotNet.VisualDesigner.Views;

sealed class VisualElementTreeView : Component<VisualElementTreeView.State>
{
    public VisualElementModel Model { get; init; }

    public string SelectedPath { get; init; }

    [CustomEvent]
    public Func<string, Task> SelectionChanged { get; set; }

    protected override Task constructor()
    {
        InitializeState();

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
            InitialSelectedPath = SelectedPath
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

        public string InitialSelectedPath { get; set; }

        public VisualElementModel Model { get; init; }

        public string SelectedPath { get; set; }
    }
}

sealed class DemoVisualElementTreeViewer : Component<DemoVisualElementTreeViewer.State>
{
    protected override Element render()
    {
        return new FlexColumn
        {
            new span { state.SelectedPath },
            new VisualElementTreeView
            {
                SelectionChanged = SelectionChanged,
                SelectedPath     = state.SelectedPath,
                Model = new()
                {
                    Tag = "div",
                    Children =
                    [
                        new() { Tag = "label", Text = "Abc1" },
                        new() { Tag = "span", Text  = "Abc2" },
                        new() { Tag = "ul", Text    = "Abc3" },

                        new()
                        {
                            Tag = "div",
                            Children =
                            [
                                new() { Tag = "label", Text = "Abc1" },
                                new() { Tag = "span", Text  = "Abc2" },
                                new()
                                {
                                    Tag  = "ul",
                                    Text = "Abc3"
                                }
                            ]
                        }
                    ]
                }
            }
        };
    }

    Task SelectionChanged(string selectedPath)
    {
        state.SelectedPath = selectedPath;

        return Task.CompletedTask;
    }

    internal class State
    {
        public string SelectedPath { get; set; }
    }
}