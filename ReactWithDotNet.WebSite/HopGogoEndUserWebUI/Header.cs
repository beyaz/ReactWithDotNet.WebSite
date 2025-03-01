namespace HopGogoEndUserWebUI;

class Header : Component
{
    protected override Element render()
    {
        var menuLabels = new[]
        {
            "Discover",
            "Saved",
            "My Trips",
            "Sign Up"
        };

        return new FlexRow(JustifyContentSpaceBetween, PaddingTopBottom(27, 22), Background(White), BoxShadow(0, 4.428116321563721, 17.712465286254883, rgba(0, 0, 0, 0.25)))
        {
            new div(Font(700, 35.42, "Outfit", "black"))
            {
                "HopGogo"
            },

            new FlexRow(AlignItemsCenter, Gap(32))
            {
                menuLabels.Select(label => new div(Opacity(0.70), Font(500, 17.71, "Outfit", Black))
                {
                    label
                })
            }
        };
    }
}