namespace HopGogoEndUserWebUI;

sealed class Breadcrumbs : PureComponent
{
    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, Background(White), Padding(8,16))
        {
            new div(Font(400, 14, 21, "Euclid Circular B", "#2659C3"))
            {
                "Home"
            },
            
            SvgIcon.Chevron_right_minor,
            
            new div(Font(400, 14, 21, "Euclid Circular B", "#2659C3"))
            {
                "Package Listing"
            },
            
            SvgIcon.Chevron_right_minor,
            
            new div(WordWrapBreakWord, Font(400, 14, 21, "Euclid Circular B", "#6A6A6A"))
            {
                "Sri Lanka Culture Package"
            }
        };
    }
}