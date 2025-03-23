namespace ReactWithDotNet.VisualDesigner.Views;

sealed class StyleEditor : Component<StyleEditor.State>
{
    public IReadOnlyList<PropertyModel> Value { get; init; } = [new() { Name = "gap", Value = "5" }];

    protected override Task constructor()
    {
        state = new()
        {
            InitialValue = Value ?? [],

            Value = (Value ?? []).ToList()
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, FlexWrap, Border(1, solid, Gray300), BorderRadius(4), Padding(5, 10), Gap(16), Background(White))
        {
            EditorFontLinks,
            EditorFont(),
            
            state.Value.Select(x => new PropertyEditor
            {
                Model               = x,
                PropertySuggestions = StyleProperties
            }),
            new PropertyEditor
            {
                PropertySuggestions = StyleProperties,
                OnChange            = OnAddNewItem
            }
        };
    }

    Task OnAddNewItem(PropertyModel newModel)
    {
        state.Value.Add(newModel);

        return Task.CompletedTask;
    }

    internal class State
    {
        public IReadOnlyList<PropertyModel> InitialValue { get; init; }

        public List<PropertyModel> Value { get; init; }
    }
}