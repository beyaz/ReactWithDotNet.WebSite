namespace HopGogoEndUserWebUI.Pages;

sealed record PassengerInfo
{
    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }
    public int NumberOfInfants { get; set; }
}

sealed record PassengerSelectorState
{
    public PassengerInfo PassengerInfo { get; set; }
    
    public bool IsPopupVisible { get; set; }
}

sealed class PassengerSelector : Component<PassengerSelectorState>
{
    protected override Task constructor()
    {
        state = new()
        {
            PassengerInfo = new() { NumberOfAdults = 1 }
        };
        
        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var calculatedText = $"{state.PassengerInfo.NumberOfAdults} Adult";
        if (state.PassengerInfo.NumberOfChildren > 0)
        {
            calculatedText += $", {state.PassengerInfo.NumberOfChildren} Children";
        }

        if (state.PassengerInfo.NumberOfInfants > 0)
        {
            calculatedText += $", {state.PassengerInfo.NumberOfInfants} Infants";
        }

        var icon = Svg_Chevron_down_minor;
        if (state.IsPopupVisible)
        {
            icon = Svg_Chevron_up_minor;
        }
        
        return new FlexRowCentered(PaddingX(16),Gap(4), Height(50), WidthFitContent, Background(White), Border(1, "#6A6A6A", solid, 13), Font(400, 16, "Outfit", "black"))
        {
            Svg_PersonWithBaggage, calculatedText, icon + MarginLeft(16),

            PositionRelative,
            When(state.IsPopupVisible, ()=>PopupContent() + PositionAbsolute + Top(51)+ Left(0)),

            OnClick(TogglePopup)
        };
    }

    Task TogglePopup(MouseEvent e)
    {
        state.IsPopupVisible = !state.IsPopupVisible;
        
        return Task.CompletedTask;
    }

    Element PopupContent()
    {
        return new FlexColumn(Padding(16), Gap(16), Background(White), Border(1, "#6A6A6A", solid, 13), Font(400, 16, "Outfit", "black"))
        {
            new div(Font(500, 16, "Outfit", "#2659C3"))
            {
                "Passengers"
            },
            
            new FlexRow(JustifyContentSpaceBetween,Gap(50))
            {
                new FlexColumn(Gap(4))
                {
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        "Adults"
                    },
                    new div(Font(400, 13, "Outfit", "#777373"))
                    {
                        "Over 11"
                    }
                },
                
                new FlexRowCentered(Gap(12))
                {
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfAdultsNegativeClicked))
                    {
                        Svg_Negative
                    }
                    ,
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        state.PassengerInfo.NumberOfAdults.ToString()
                    },
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfAdultsPlusClicked))
                    {
                        Svg_Plus
                    }
                }
            },
            
            new FlexRow(JustifyContentSpaceBetween)
            {
                new FlexColumn(Gap(4))
                {
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        "Children"
                    },
                    new div(Font(400, 13, "Outfit", "#777373"))
                    {
                        "2-11"
                    }
                },
                
                new FlexRowCentered(Gap(12))
                {
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfChildrenNegativeClicked))
                    {
                        Svg_Negative
                    }
                    ,
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        state.PassengerInfo.NumberOfChildren.ToString()
                    },
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfChildrenPlusClicked))
                    {
                        Svg_Plus
                    }
                }
            },
            
            new FlexRow(JustifyContentSpaceBetween)
            {
                new FlexColumn(Gap(4))
                {
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        "Infants"
                    },
                    new div(Font(400, 13, "Outfit", "#777373"))
                    {
                        "Under 2"
                    }
                },
                
                new FlexRowCentered(Gap(12))
                {
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfInfantsNegativeClicked))
                    {
                        Svg_Negative
                    }
                    ,
                    new div(Font(400, 16, "Outfit", "black"))
                    {
                        state.PassengerInfo.NumberOfInfants.ToString()
                    },
                    new FlexRowCentered(Background("#D9D9D9"), BorderRadius(16), Size(24), OnClick(OnNumberOfInfantsPlusClicked))
                    {
                        Svg_Plus
                    }
                }
            }
            
        };
    }

    [StopPropagation]
    Task OnNumberOfAdultsNegativeClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfAdults--;
        
        return Task.CompletedTask;
    }
    
    [StopPropagation]
    Task OnNumberOfAdultsPlusClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfAdults++;
        
        return Task.CompletedTask;
    }
    
    
    [StopPropagation]
    Task OnNumberOfChildrenNegativeClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfChildren--;
        
        return Task.CompletedTask;
    }
    
    [StopPropagation]
    Task OnNumberOfChildrenPlusClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfChildren++;
        
        return Task.CompletedTask;
    }
    
    [StopPropagation]
    Task OnNumberOfInfantsNegativeClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfInfants--;
        
        return Task.CompletedTask;
    }
    
    [StopPropagation]
    Task OnNumberOfInfantsPlusClicked(MouseEvent e)
    {
        state.PassengerInfo.NumberOfInfants++;
        
        return Task.CompletedTask;
    }
}