using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

class MuiAccordionDemo : Component<MuiAccordionDemo.State>
{
    protected override Element render()
    {
        return new FlexColumnCentered
        {
            new div
            {
                state.IsExpanded is true ? "Expanded" : "Collapsed"
            },
            new Accordion
            {
                Accordion.DefaultExpanded(true),
                new AccordionSummary
                {
                    AccordionSummary.ExpandIcon(new ExpandMoreIcon()),
                    new Typography { children = { "Accordion 1" } }
                },

                new AccordionDetails
                {
                    """
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse
                           malesuada lacus ex, sit amet blandit leo lobortis eget.
                    """
                },

                Accordion.OnChange(OnChange)
            }
        };
    }

    Task OnChange(MouseEvent arg1, bool? expanded)
    {
        state.IsExpanded = expanded;

        return Task.CompletedTask;
    }

    internal class State
    {
        public bool? IsExpanded { get; set; }
    }
}