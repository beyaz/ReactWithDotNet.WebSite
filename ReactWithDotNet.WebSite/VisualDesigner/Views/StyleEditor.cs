//namespace ReactWithDotNet.VisualDesigner.Views;

//sealed class StyleEditor : Component<StyleEditor.State>
//{
//    [CustomEvent]
//    public Func<IReadOnlyList<PropertyModel>, Task> OnChange { get; init; }

//    public IReadOnlyList<PropertyModel> Value { get; init; } = [new() { Name = "gap", Value = "5" }];

//    protected override Element render()
//    {
//        return new FlexColumn(WidthFull, Padding(8, 16), Gap(16), Background(White))
//        {
//            Value.Select(x => new PropertyEditor
//            {
//                Model               = x,
//                PropertySuggestions = StyleProperties
//            }),
//            new PropertyEditor
//            {
//                PropertySuggestions = StyleProperties,
//                OnChange            = OnAddNewItem
//            }
//        };
//    }

//    Task OnAddNewItem(PropertyModel newModel)
//    {
//        var newValue = new List<PropertyModel> { newModel };

//        newValue.AddRange(Value);

//        DispatchEvent(OnChange, [newValue]);

//        return Task.CompletedTask;
//    }

//    internal class State
//    {
//        public int? SelectedItemIndex { get; set; }
//    }
//}