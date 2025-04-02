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
            beforePositionElement = new FlexRow(WidthFull, Height(6), DraggableTrue)
            {
                OnDragEnter(ToggleDragPositionBefore),
                OnDragLeave(ToggleDragPositionBefore),

                BorderBottomLeftRadius(16), BorderTopLeftRadius(16),

                When(state.DragPosition == DragPosition.Before, Background(Blue300)),

                PositionAbsolute, Top(0)
            };
        }
        
        Element afterPositionElement = null;
        if (isDragHoveredElement)
        {
            afterPositionElement = new FlexRow(WidthFull, Height(6), DraggableTrue)
            {
                OnDragEnter(ToggleDragPositionAfter),
                OnDragLeave(ToggleDragPositionAfter),

                BorderBottomLeftRadius(16), BorderTopLeftRadius(16),

                When(state.DragPosition == DragPosition.After, Background(Blue300)),
                
                PositionAbsolute, Bottom(0)
            };
        }

        Element icon = null;
        
        var commonStyles = node.StyleGroups?.FirstOrDefault(x => x.Condition == "*");
        if (commonStyles is not null)
        {
            var hasFlex = commonStyles.Items.Any(x =>
            {
                var (success, name, value) = TryParsePropertyValue(x);
                if (!success)
                {
                    return false;
                }

                if (name == "display" && value == "flex")
                {
                    return true;
                }

                return false;
            });

            var hasFlexDirectionColumn = commonStyles.Items.Any(x =>
            {
                var (success, name, value) = TryParsePropertyValue(x);
                if (!success)
                {
                    return false;
                }

                if (name == "flex-direction" && value == "column")
                {
                    return true;
                }

                return false;
            });
            
            var hasFlexDirectionRow = commonStyles.Items.Any(x =>
            {
                var (success, name, value) = TryParsePropertyValue(x);
                if (!success)
                {
                    return false;
                }

                if (name == "flex-direction" && value == "row")
                {
                    return true;
                }

                return false;
            });
            
            if (hasFlexDirectionColumn)
            {
                icon = new IconFlexColumn() + Size(16) + Color(Gray300);
            }
            else if (hasFlexDirectionRow || hasFlex)
            {
                icon = new IconFlexRow() + Size(16) + Color(Gray300);
            }
        }

        if (icon is null)
        {
            if (node.Text.HasValue())
            {
                if (node.Tag[0]== 'h')
                {
                    icon = new IconHeader() + Size(16) + Color(Gray300);    
                }
                else if (node.Tag == "a")
                {
                    icon = new IconLink() + Size(16) + Color(Gray300);    
                }
                else if (node.Tag == "img")
                {
                    icon = new IconImage() + Size(16) + Color(Gray300);    
                }
                else
                {
                    icon = new IconText() + Size(16) + Color(Gray300);
                }
                
            }
        }
        
        var returnList = new List<Element>
        {
            new FlexColumn(PaddingLeft(indent * 16), Id(path), OnClick(OnTreeItemClicked), OnMouseEnter(OnMouseEnterHandler))
            {
                PositionRelative,
                
                beforePositionElement,

                new FlexRow(Gap(4), AlignItemsCenter)
                {
                    MarginLeft(4), FontSize13,
                    
                    
                    
                    new span{ node.Tag },
                    
                    icon,
                    
                    
                },

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