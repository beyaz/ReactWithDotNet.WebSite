namespace ReactWithDotNet.VisualDesigner.Views;

delegate Task OnTreeItemHover(string treeItemPath);

enum DragPosition
{
    Before,
    After,
    Inside
}

delegate Task OnTreeItemMove(string treeItemPathSourge, string treeItemPathTarget, DragPosition position);

sealed class VisualElementTreeView : Component<VisualElementTreeView.State>
{
    public VisualElementModel Model { get; init; }

    [CustomEvent]
    public Func<Task> MouseLeave { get; init; }

    public string SelectedPath { get; init; }

    [CustomEvent]
    public Func<string, Task> SelectionChanged { get; init; }

    [CustomEvent]
    public OnTreeItemHover TreeItemHover { get; init; }

    [CustomEvent]
    public OnTreeItemMove TreeItemMove { get; init; }

    protected override Element render()
    {
        if (Model is null)
        {
            return new FlexRowCentered(SizeFull) { "Empty" };
        }

        return new div(CursorDefault, Padding(5), OnMouseLeave(OnMouseLeaveHandler))
        {
            ToVisual(Model, 0, "0"),
            WidthFull, HeightFull
        };
    }

    Task OnDragEntered(DragEvent e)
    {
        state.CurrentDragOveredPath = e.currentTarget.id;
        return Task.CompletedTask;
    }

    Task OnDragStarted(DragEvent e)
    {
        if (e.currentTarget.id == "0")
        {
            return Task.CompletedTask;
        }
        
        state.DragStartedTreeItemPath = e.currentTarget.id;

        return Task.CompletedTask;
    }

    Task OnDroped(DragEvent e)
    {
        var treeItemPathTarget = e.currentTarget.id;

        if (treeItemPathTarget != state.DragStartedTreeItemPath)
        {
            DispatchEvent(TreeItemMove, [state.DragStartedTreeItemPath, treeItemPathTarget, state.DragPosition]);
        }

        state.CurrentDragOveredPath = null;

        state.DragStartedTreeItemPath = null;

        return Task.CompletedTask;
    }

    Task OnMouseEnterHandler(MouseEvent e)
    {
        var selectedPath = e.currentTarget.id;

        DispatchEvent(TreeItemHover, [selectedPath]);

        return Task.CompletedTask;
    }

    Task OnMouseLeaveHandler(MouseEvent e)
    {
        DispatchEvent(MouseLeave, []);

        return Task.CompletedTask;
    }

    Task OnTreeItemClicked(MouseEvent e)
    {
        var selectedPath = e.currentTarget.id;

        DispatchEvent(SelectionChanged, [selectedPath]);

        return Task.CompletedTask;
    }

    Task ToggleDragPositionAfter(DragEvent e)
    {
        if (state.DragPosition == DragPosition.After)
        {
            state.DragPosition = DragPosition.Inside;
        }
        else
        {
            state.DragPosition = DragPosition.After;
        }

        return Task.CompletedTask;
    }

    Task ToggleDragPositionBefore(DragEvent e)
    {
        if (state.DragPosition == DragPosition.Before)
        {
            state.DragPosition = DragPosition.Inside;
        }
        else
        {
            state.DragPosition = DragPosition.Before;
        }

        return Task.CompletedTask;
    }

    IReadOnlyList<Element> ToVisual(VisualElementModel node, int indent, string path)
    {
        var isSelected = SelectedPath == path;

        var isDragHoveredElement = path == state.CurrentDragOveredPath && path != state.DragStartedTreeItemPath;

        Element beforePositionElement = null;
        if (isDragHoveredElement)
        {
            beforePositionElement = new FlexRow(WidthFull, Height(4), DraggableTrue, Background(Gray100))
            {
                OnDragEnter(ToggleDragPositionBefore),
                OnDragLeave(ToggleDragPositionBefore),

                BorderBottomLeftRadius(16), BorderBottomRightRadius(16),

                When(state.DragPosition == DragPosition.Before, Background(Blue300)),

                PositionAbsolute, Top(0)
            };
        }
        
        Element afterPositionElement = null;
        if (isDragHoveredElement)
        {
            afterPositionElement = new FlexRow(WidthFull, Height(4), DraggableTrue, Background(Gray100))
            {
                OnDragEnter(ToggleDragPositionAfter),
                OnDragLeave(ToggleDragPositionAfter),

                BorderTopLeftRadius(16), BorderTopRightRadius(16),

                When(state.DragPosition == DragPosition.After, Background(Blue300)),
                
                PositionAbsolute, Bottom(0)
            };
        }
        
        var returnList = new List<Element>
        {
            new FlexColumn(PaddingLeft(indent * 16), Id(path), OnClick(OnTreeItemClicked), OnMouseEnter(OnMouseEnterHandler))
            {
                PositionRelative,
                
                beforePositionElement,

                new div { Text(node.Tag), MarginLeft(5), FontSize13 },

                state.DragStartedTreeItemPath.HasNoValue() && isSelected ? Background(Blue100) + BorderRadius(3) : null,

                state.DragStartedTreeItemPath.HasNoValue() && !isSelected ? Hover(Background(Blue50), BorderRadius(3)) : null,

                DraggableTrue,
                OnDragStart(OnDragStarted),
                OnDragEnter(OnDragEntered),
                OnDrop(OnDroped),

                afterPositionElement
            }
        };

        if (node.HasNoChild())
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
        public string CurrentDragOveredPath { get; set; }

        public DragPosition DragPosition { get; set; }

        public string DragStartedTreeItemPath { get; set; }
    }
}