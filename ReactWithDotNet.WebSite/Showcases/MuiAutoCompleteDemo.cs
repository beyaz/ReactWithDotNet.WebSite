using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

class MuiAutoCompleteDemo : Component<MuiAutoCompleteDemo.State>
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
                defaultExpanded = true,

                onChange = OnChange,

                children =
                {
                    new AccordionSummary
                    {
                        expandIcon = new ExpandMoreIcon(),
                        children =
                        {
                            new Typography
                            {
                                "Accordion 1"
                            }
                        }
                    },

                    new AccordionDetails
                    {
                        """
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse
                               malesuada lacus ex, sit amet blandit leo lobortis eget.
                        """
                    }
                }
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