using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

class MuiAccordionDemo : PureComponent
{
    protected override Element render()
    {
        return new FlexRowCentered
        {
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
                }
            }
            
            
        };
    }
}