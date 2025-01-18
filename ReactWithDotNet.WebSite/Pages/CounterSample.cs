namespace ReactWithDotNet.WebSite.Pages;

class CounterSample: Component<CounterSample.State>
{
    protected override Element render()
    {
        return new div
        {
            new div { "Current count: ", state.Count.ToString() },
            new button { "Increment", OnClick(OnIncrement) }
        };
    }

    Task OnIncrement(MouseEvent e)
    {
        state.Count++;
        return Task.CompletedTask;
    }

    internal class State
    {
        public int Count { get; set; }
    }
}