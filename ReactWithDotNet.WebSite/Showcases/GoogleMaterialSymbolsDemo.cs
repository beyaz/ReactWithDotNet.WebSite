using ReactWithDotNet.ThirdPartyLibraries.GoogleMaterialSymbols;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class GoogleMaterialSymbolsDemo: PureComponent
{
    protected override Element render()
    {
        return new div
        {
            new MaterialSymbol
            {
                name         = "home",
                styleVariant = MaterialSymbolVariant.sharp,
                size         = 32,
                weight       = 500,
                fill         = 0,
                opticalSize  = 24
            },
        };
    }
}