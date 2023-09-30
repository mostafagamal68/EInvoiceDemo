using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using EInvoiceDemo.Client.Components;
using EInvoiceDemo.Client.Services;
using EInvoiceDemo.Client.Shared;
using EInvoiceDemo.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EInvoiceDemo.Client.Configurations;

public class GeneralComponent : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }

    //[Inject]
    //public Session CurrentSession { get; set; }

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public IToastService ToastService { get; set; }

    [CascadingParameter]
    public MainLayout Layout { get; set; }

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    //[Parameter]
    //public bool IsModal { get; set; } = false;

    public async Task ShowResultMessage(HttpResponseMessage result)
    {
        if (result.IsSuccessStatusCode)
            ToastService.ShowSuccess(await result.Content.ReadAsStringAsync());
        else
            ToastService.ShowError(await result.Content.ReadAsStringAsync());
    }
    public async Task<bool> ShowConfirm(string title, string message)
    {
        var confirm = Modal.Show<ConfirmDialog>(title,
        new ModalParameters().Add(nameof(ConfirmDialog.Text), message),
        new ModalOptions()
        {
            Size = ModalSize.Medium,
            Position = ModalPosition.TopCenter,
            DisableBackgroundCancel = true,
            HideCloseButton = true,
        });
        var result = await confirm.Result;
        return result.Confirmed;
    }

    public void GoTo(string PageRoute, string PageName, bool WithId = false, decimal? Id = null)
    {
        Session.Set("PageTitle", (WithId ? !Id.HasValue ? "Add - " : "Edit - " : "") + PageName);
        if (WithId)
            Navigation.NavigateTo($"/{PageRoute}/{Id}");
        else
            Navigation.NavigateTo($"/{PageRoute}");
    }

    //protected override async Task OnInitializedAsync()
    //{
    //    Layout.ToggleLoader();
    //    Layout.SetTitle();
    //    await base.OnInitializedAsync();
    //}
}
