namespace ReactWithDotNet.WebSite.Showcases;

sealed class BasicCounterDemo : Component
{
    protected override Element render()
    {
        return new div
        {
            Counter
        };
    }
    
    static Element Counter()
    {
        return FC(cmp =>
        {
            var count = 0;

            Task onIncrementClicked(MouseEvent e)
            {
                count++;
                return Task.CompletedTask;
            }

            Task onDecrementClicked(MouseEvent e)
            {
                count--;
                return Task.CompletedTask;
            }
            
            return new div
            {
                new div { $"Counter: {count}" },
                new button
                {
                    children = { "Increment" },
                    onClick  = onIncrementClicked
                },
                new button
                {
                    children = { "Decrement" },
                    onClick  = onDecrementClicked
                }
            };
        });
    }
}