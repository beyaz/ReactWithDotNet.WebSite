using System.Diagnostics.CodeAnalysis;

namespace ReactWithDotNet.WebSite.Showcases;

[SuppressMessage("ReSharper", "UnusedParameter.Local")]
sealed class BasicCounterDemo : Component
{
    protected override Element render()
    {
        var count = 0;

        return FC(cmp =>
        {
            return new div
            {
                new div { $"Counter: {count}" },
                new button
                {
                    "Increment",
                    OnClick(e =>
                    {
                        count++;
                        return Task.CompletedTask;
                    })
                },
                new button
                {
                    "Decrement",
                    OnClick(e =>
                    {
                        count--;
                        return Task.CompletedTask;
                    })
                }
            };
        });
    }
}