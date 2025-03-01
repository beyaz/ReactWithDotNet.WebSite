namespace HopGogoEndUserWebUI;

sealed class Header : Component
{
    protected override Element render()
    {
        return new FlexRow(JustifyContentSpaceBetween, Padding(24,48), Background(White), BoxShadow(0, 4.428116321563721, 17.712465286254883, rgba(0, 0, 0, 0.25)))
        {
            new div(Font(700, 35.42, "Outfit", "black"))
            {
                "HopGogo"
            },

            new FlexRow(AlignItemsCenter, Gap(32))
            {
                MenuItem("Discover"),
                MenuItem("Saved"),
                MenuItem("My Trips"),
                MenuItem("Sign Up")
            }
        };

        Element MenuItem(string label)
        {
            return new div(Opacity(0.70), Font(500, 17.71, "Outfit", Black))
            {
                label
            };
        }
    }
}