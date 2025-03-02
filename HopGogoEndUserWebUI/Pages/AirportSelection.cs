namespace HopGogoEndUserWebUI.Pages;

record AirportInfo
{
    public string MiniDescription { get; init; }
    public string Name { get; init; }
}

sealed class AirportSelection : AutoFilterBox<AirportInfo>
{
    protected override IReadOnlyList<AirportInfo> GetDefaultSuggestions()
    {
        // dummy data
        return Enumerable.Range(1, 4).Select(i => new AirportInfo
        {
            Name            = "Airport Default" + i,
            MiniDescription = DummySentence(3)
        }).ToList();
    }

    protected override IReadOnlyList<AirportInfo> GetItemsSource()
    {
        // dummy data
        return Enumerable.Range(1, 4).Select(i => new AirportInfo
        {
            Name            = "Airport " + i,
            MiniDescription = DummySentence(3)
        }).ToList();
    }

    public string Prefix  { get; init; } 
    
    protected override string GetSelectedValueText(AirportInfo airportInfo)
    {
        return Prefix + ": " + airportInfo.Name;
    }

    protected override Element RenderItem(AirportInfo record)
    {
        return new FlexColumn(WhiteSpaceNoWrap)
        {
            new div(Font(400, 16, "Outfit", "black"))
            {
                record.Name
            },
            new div(Font(400, 13, "Outfit", "#777373"))
            {
                record.MiniDescription
            }
        };
    }
}