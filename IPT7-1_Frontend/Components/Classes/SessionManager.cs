using IPT_71_Frontend.Components.Classes;
using Microsoft.AspNetCore.Components;

namespace IPT7_1_Frontend.Components.Classes
{
    public class SessionManager : ComponentBase
    {
    [Inject]
    protected GlobalVars GlobalState { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!GlobalState.IsSessionActive)
        {
            Navigation.NavigateTo("/");
        }
        await base.OnInitializedAsync();
    }
}
}
