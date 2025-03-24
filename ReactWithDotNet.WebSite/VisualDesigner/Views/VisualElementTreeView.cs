namespace ReactWithDotNet.VisualDesigner.Views;

sealed class VisualElementTreeView : Component
{
    public VisualElementModel Model { get; init; }

    public string SelectedPath { get; init; }

    [CustomEvent]
    public Func<string, Task> SelectionChanged { get; init; }

    protected override Element render()
    {
        return new div(CursorPointer, Padding(5))
        {
            ToVisual(Model, 0, "0"),
            WidthFull, HeightFull
        };
    }

    Task OnTreeItemClicked(MouseEvent e)
    {
        var selectedPath = e.currentTarget.id;

        DispatchEvent(SelectionChanged, [selectedPath]);

        return Task.CompletedTask;
    }

    IReadOnlyList<Element> ToVisual(VisualElementModel node, int indent, string path)
    {
        var isSelected = SelectedPath == path;

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
}