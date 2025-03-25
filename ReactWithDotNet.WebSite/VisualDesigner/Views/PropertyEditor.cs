//namespace ReactWithDotNet.VisualDesigner.Views;

//sealed class PropertyEditor : Component<PropertyEditor.State>
//{
//    public PropertyModel Model { get; init; }

//    [CustomEvent]
//    public Func<PropertyModel, Task> OnChange { get; init; }

//    public IReadOnlyList<PropertyInfo> PropertySuggestions { get; init; } = StyleProperties;

//    protected override Task constructor()
//    {
//        InitializeState();

//        return Task.CompletedTask;
//    }

//    protected override Task OverrideStateFromPropsBeforeRender()
//    {
//        if (Model is not null && state.InitialModel is not null && Model.Name != state.InitialModel.Name)
//        {
//            InitializeState();

//            return Task.CompletedTask;
//        }

//        return Task.CompletedTask;
//    }

//    protected override Element render()
//    {
//        var nameNotSelectedYet = string.IsNullOrWhiteSpace(state.Model.Name);
//        if (nameNotSelectedYet)
//        {
//            List<string> suggestions = [];
//            foreach (var propertyInfo in PropertySuggestions)
//            {
//                suggestions.Add(propertyInfo.Name);

//                foreach (var suggestion in propertyInfo.Suggestions)
//                {
//                    suggestions.Add(propertyInfo.Name + ": " + suggestion);
//                }
//            }

//            return new FlexRowCentered(Color(Gray600), WidthFitContent, BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Background(White), Gap(4))
//            {
//                new MagicInput { FitContent  = true,  Suggestions = suggestions, Value = null, OnChange = OnFirstValueChange }
//            };
//        }

//        if (state.IsEditMode is false)
//        {
//            return new FlexColumn(Color(Gray600), WidthFitContent, BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Gap(4))
//            {
//                new FlexRow(AlignItemsCenter, Gap(4))
//                {
//                    new span(FontWeight600) { state.Model.Name },
//                    new span { ":" },
//                    new span(Width(CalculateTextWidth(state.Model.Value)), LetterSpacingNormal) { state.Model.Value },
//                },

//                When(state.Model.Condition.HasValue(), () => new FlexRowCentered(Gap(4))
//                {
//                    new span(FontWeight600) { "on:" },
//                    new span { state.Model.Condition }
//                }),

//                OnMouseEnter(OnMouseEnterHandler)
//            };
//        }

//        // Edit Mode ON
//        {
//            List<string> suggestions = [];
//            {
//                foreach (var propertyInfo in PropertySuggestions)
//                {
//                    if (propertyInfo.Name != state.Model.Name)
//                    {
//                        continue;
//                    }

//                    foreach (var suggestion in propertyInfo.Suggestions)
//                    {
//                        suggestions.Add(suggestion);
//                    }
//                }
//            }

//            var closeIcon = new FlexRowCentered(Size(24), PositionAbsolute, Padding(4), Top(0), Right(0))
//            {
//                Color(Gray600),
//                Hover(Color(Gray700)),
//                Background(White),
//                BorderTopRightRadius(16),
//                BorderBottomLeftRadius(8),

//                BorderLeft(1, solid, Gray300),
//                BorderBottom(1, solid, Gray300),

//                Hover(BorderColor(Gray500), Background(Gray300)),
//                new IconClose()
//            };

//            var isActiveIcon = new FlexRowCentered(Size(24), Padding(4), PositionAbsolute, Top(-4), Left(-16))
//            {
//                Color(Gray600),
//                Hover(Color(Gray700)),
//                Background(White),
//                BorderRadius(24),
//                Border(1, solid, Gray300),
//                Hover(BorderColor(Gray500)),
//                new IconChecked()
//            };

//            return new FlexColumn(WidthFull, Color(Gray600), BorderRadius(16), Border(1, solid, Gray300), Padding(4, 8), Background(White), Gap(4))
//            {
//                new FlexRow(AlignItemsCenter, Gap(4))
//                {
//                    new span(FontWeight600) { state.Model.Name },

//                    new span { ":" },

//                    new MagicInput { Value = state.Model.Value, Suggestions = suggestions, OnChange = OnPropertyValueChanged }
//                },

//                new FlexRow(AlignItemsCenter, Gap(4))
//                {
//                    new span(FontWeight600) { "on:" },

//                    new MagicInput
//                    {
//                        Value       = state.Model.Condition,
//                        Suggestions = ["state.IsEndUser", "prop.HasNewValue", "prop.IsNewRecord"],
//                        OnChange    = OnConditionChanged
//                    }
//                },

//                OnMouseLeave(OnMouseLeaveHandler),

//                PositionRelative,
//                closeIcon,
//                isActiveIcon
//            };
//        }
//    }

//    Task CloseEditMode()
//    {
//        state.IsEditMode = false;

//        return Task.CompletedTask;
//    }

//    void InitializeState()
//    {
//        state = new()
//        {
//            Model        = Model ?? new PropertyModel(),
//            InitialModel = Model
//        };
//    }

//    Task OnConditionChanged(string newValue)
//    {
//        state.Model.Condition = newValue;

//        DispatchEvent(OnChange, [state.Model]);

//        return Task.CompletedTask;
//    }

//    Task OnFirstValueChange(string newValue)
//    {
//        var model = state.Model = TryParsePropertyValue(newValue);

//        if (model is null || model.Value is null)
//        {
//            return Task.CompletedTask;
//        }

//        DispatchEvent(OnChange, [state.Model]);

//        return Task.CompletedTask;
//    }

//    Task OnMouseEnterHandler(MouseEvent e)
//    {
//        state.IsEditMode = true;

//        return Task.CompletedTask;
//    }

//    Task OnMouseLeaveHandler(MouseEvent e)
//    {
//        Client.GotoMethod(1000, CloseEditMode);

//        return Task.CompletedTask;
//    }

//    Task OnPropertyValueChanged(string newValue)
//    {
//        state.Model.Value = newValue;

//        DispatchEvent(OnChange, [state.Model]);

//        return Task.CompletedTask;
//    }

//    internal class State
//    {
//        public PropertyModel InitialModel { get; init; }

//        public bool IsDeleteButtonVisible { get; set; }

//        public bool IsEditMode { get; set; }

//        public PropertyModel Model { get; set; }
//    }
//}