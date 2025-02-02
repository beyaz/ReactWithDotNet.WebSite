using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

class MuiAutoCompleteDemo : Component<MuiAutoCompleteDemo.State>
{
    protected override Element render()
    {
        return new FlexColumnCentered
        {
            new div
            {
                $"Selected Country is '{state.SelectedCountryType?.label}'"
            },
            new Autocomplete<CountryType>
            {
                options = Countries,
                // autoHighlight = true
                getOptionLabel = x => x.label,
                renderOption = (_, option)=> new FlexRow
                {
                    new img(Size(20))
                    {
                        src = $"https://flagcdn.com/w20/{option.code.ToLower()}.png"
                    },
                        
                    option.label
                },
                onChange = OnChange,
                // renderInput = 

            }
        };
    }

    Task OnChange(ChangeEvent arg1, CountryType selectedItem)
    {
        state.SelectedCountryType = selectedItem;
        
        return Task.CompletedTask;
    }

   

    internal class State
    {
        public CountryType SelectedCountryType { get; set; }
    }
}