using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using EInvoiceDemo.Client.Components;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace EInvoiceDemo.Client.Configurations;

public class GeneralComponent : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public LoaderService LoaderService { get; set; }

    [Inject]
    public HeaderService HeaderService { get; set; }

    [Inject]
    public IToastService ToastService { get; set; }

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    [Parameter]
    public bool IsModal { get; set; } = false;

    public ModalOptions ModalOptions { get; set; } = new()
    {
        ActivateFocusTrap = true,
        AnimationType = ModalAnimationType.FadeInOut,
        Position = ModalPosition.Middle,
        Size = ModalSize.Large,
    };

    public async Task ShowResultMessage(HttpResponseMessage result)
    {
        if (result.StatusCode == HttpStatusCode.Created)
            ToastService.ShowSuccess("Added Successfully");
        else if (result.IsSuccessStatusCode)
            ToastService.ShowSuccess(await result.Content.ReadAsStringAsync());
        else
            ToastService.ShowError(await result.Content.ReadAsStringAsync());
    }
    public async Task<bool> ShowDeleteConfirmation(string item)
        => await ShowConfirm("Delete Confirmation", $"Are you sure to delete this {item}?");
    public async Task<bool> ShowConfirm(string title, string message)
    {
        var confirm = Modal.Show<ConfirmDialog>(title, new ModalParameters().Add(nameof(ConfirmDialog.Text), message),
        new ModalOptions()
        {
            Size = ModalSize.Medium,
            Position = ModalPosition.TopCenter,
            DisableBackgroundCancel = true,
            HideCloseButton = true,
            AnimationType = ModalAnimationType.PopInOut,
        });
        var result = await confirm.Result;
        return result.Confirmed;
    }

    public async Task ShowModal(Type component, string title, ModalParameters? parameters = null, Func<Task>? afterClose = null)
    {
        var modal = Modal.Show(component, title, parameters ?? new ModalParameters().Add(nameof(IsModal), true), ModalOptions);
        await modal.Result;
        if (afterClose is not null)
        {
            LoaderService.ToggleLoader();
            await afterClose.Invoke();
            LoaderService.ToggleLoader();
        }
        else await InvokeAsync(StateHasChanged);
    }

    public void GoTo(string PageRoute, Guid? Id = null)
        => Navigation.NavigateTo($"/{PageRoute}{(Id.HasValue ? "/" + Id : "")}");
}
