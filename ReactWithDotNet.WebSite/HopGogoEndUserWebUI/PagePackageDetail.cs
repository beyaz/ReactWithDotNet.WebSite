namespace HopGogoEndUserWebUI;

public class PagePackageDetail: Component
{
    protected override Element render()
    {


        return new div
        {
            new link
            {
                href = "https://fonts.cdnfonts.com/css/euclid-circular-b", rel = "stylesheet"
            },
            new link
            {
                href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&family=Outfit:wght@100..900&family=Plus+Jakarta+Sans:ital,wght@0,200..800;1,200..800&family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet"
            },
            
            AdditionalPreferences()
        };
    }

    Element AdditionalPreferences()
    {
        return new FlexRow(AlignItemsCenter, JustifyContentSpaceBetween, Background("#F4F4F4"), Height(50))
        {
            new FlexRow(AlignItemsCenter)
            {
                new span(Color("black"), FontSize13, FontFamily("Outfit"), FontWeight400)
                {
                    "Additional Preferences"
                },
            
                new[]
                {
                    "Nature",
                    "Skiing",
                    "Casa Cook Miami",
                    "Nature",
                    "Skiing",
                    "Casa Cook Miami"
                
                }.Select(label => new div(TextAlignCenter, Padding(8), BorderRadius(6), MarginLeft(8), Background("#EEDAFF"), Color("#210835"), FontSize13, FontFamily("Outfit"), FontWeight500, LineHeight16)
                {
                    label
                })
            },
            
            new FlexRowCentered(BorderRadius(10), Border("1px #360D57 solid"), Padding(5, 12, 5, 8))
            {
                Color("#360D57"), FontSize14, FontFamily("Euclid Circular B"), FontWeight500, LineHeight20, WhiteSpaceNoWrap,
                
                    "Change your preferences"
                
            }
        };
    }
}