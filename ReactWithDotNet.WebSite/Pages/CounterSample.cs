using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Pages;


class CounterParentComponent : Component<CounterParentComponent.State>
{
    internal class State
    {
        public int Count { get; set; }
    }

    protected override Element render()
    {
        return new FlexColumn
        {
            new div { "Parent:" + state.Count },
            new CounterSample
            {
                OnValueChange = async count =>
                {
                    state.Count = count;
                    await Task.CompletedTask;
                }
            }
        };
    }
}


class CounterSample : Component<CounterSample.State>
{
    [CustomEvent]
    public Func<int, Task> OnValueChange { get; set; }

    protected override Element render()
    {
        return new div
        {
            new div
            {
                "Current count: ", state.Count.ToString()
            },
            new button
            {
                "Increment", OnClick(OnIncrement)
            },
            new input{ type = "text", valueBind = ()=>state.Text },
            new input{ type = "text", valueBind = ()=>state.Text, valueBindDebounceTimeout = 1000, valueBindDebounceHandler = OnTypeFinished},
            new TextField
            {
                size                     = "small",
                valueBind                = () => state.Text,
                valueBindDebounceTimeout = 700,
                valueBindDebounceHandler = OnTypeFinished,
                style                    = { WidthFull }
            }
        };
    }

    async Task OnTypeFinished()
    {
        await Task.Delay(3000);

        state.Count++;
    }

    [StopPropagation]
    Task OnIncrement(MouseEvent e)
    {
        state.Count++;

        DispatchEvent(OnValueChange, [state.Count]);

        return Task.CompletedTask;
    }

    internal class State
    {
        public int Count { get; set; }
        public string Text { get; set; }
    }
}