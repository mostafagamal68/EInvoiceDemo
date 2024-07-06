using Blazored.Modal;
using Blazored.Toast.Services;
using EInvoiceDemo.Client.Components;
using EInvoiceDemo.Client.Services;
using EInvoiceDemo.Shared.Helpers;
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

    //[CascadingParameter]
    //public IModalService Modal { get; set; } = default!;

    [CascadingParameter]
    public IModalService ModalService { get; set; }

    [Parameter]
    public Guid? Id { get; set; }

    [Parameter]
    public bool AsModal { get; set; } = false;

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
    //public async Task<bool> ShowConfirm(string title, string message)
    //{
    //    var confirm = Modal.Show<ConfirmDialog>(title, new ModalParameters().Add(nameof(ConfirmDialog.Text), message),
    //    new ModalOptions()
    //    {
    //        Size = ModalSize.Medium,
    //        Position = ModalPosition.TopCenter,
    //        DisableBackgroundCancel = true,
    //        HideCloseButton = true,
    //        AnimationType = ModalAnimationType.PopInOut,
    //    });
    //    var result = await confirm.Result;
    //    return result.Confirmed;
    //}
    public async Task<bool> ShowConfirm(string title, string message)
    {
        var confirm = ModalService.Show(typeof(ConfirmDialog), title, new ModalParameters().Add(nameof(ConfirmDialog.Text), message), null);
        await confirm.Closing;

        return confirm.Value.CastTo<bool?>() == true;
    }

    //public async Task ShowModal(Type component, string title, ModalParameters? parameters = null, Func<Task>? afterClose = null)
    //{
    //    var modal = Modal.Show(component, title, parameters ?? new ModalParameters().Add(nameof(AsModal), true));
    //    await modal.Result;
    //    if (afterClose is not null)
    //    {
    //        LoaderService.ToggleLoader();
    //        await afterClose.Invoke();
    //        LoaderService.ToggleLoader();
    //    }
    //    else await InvokeAsync(StateHasChanged);
    //}
    public async Task ShowModal(Type component, string title, ModalParameters? parameters, Func<Task>? afterClose)
    {
        var modal = ModalService.Show(component, title, parameters ?? new ModalParameters().Add(nameof(AsModal), true), afterClose);
        await modal.Closing;

        if (modal.Value is not null)
        {

        }

        if (modal.AfterClose is not null)
            await modal.AfterClose.Invoke();
    }
    public void GoTo(string PageRoute, Guid? Id = null)
        => Navigation.NavigateTo($"/{PageRoute}{(Id.HasValue ? "/" + Id : "")}");
}
