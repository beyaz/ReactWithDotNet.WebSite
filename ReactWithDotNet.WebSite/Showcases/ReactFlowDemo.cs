using ReactWithDotNet.ThirdPartyLibraries.xyflow_react;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class ReactFlowDemo : Component
{
    protected override Element render()
    {
        var node1 = new
        {
            id       = "1",
            position = new { x     = 100, y = 100 },
            data     = new { label = "Node 1" }
        };

        var node2 = new
        {
            id       = "2",
            position = new { x     = 100, y = 100 },
            data     = new { label = "Node 2" }
        };

        var nodes = new[]
        {
            node1, node2
        };

        var edges = new[]
            {
                new
                {
                    id     = "e1-2",
                    source = "1",
                    target = '2'
                }
            };

        return new FlexColumn(SizeFull)
        {
            new ReactFlow { nodes = nodes, edges = edges }
        };
    }
}