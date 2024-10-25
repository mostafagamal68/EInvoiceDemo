using Blazored.Toast.Services;
using EInvoiceDemo.Client.Components;
using EInvoiceDemo.Client.Services;
using EInvoiceDemo.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace EInvoiceDemo.Client.Shared;

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

    [Inject]
    public IModalService ModalService { get; set; }

    [CascadingParameter]
    public ModalData Modal { get; set; }

    public bool InModal => Modal is not null;

    public async Task ShowResultMessage(HttpResponseMessage result)
    {
        if (result.StatusCode == HttpStatusCode.Created)
            ToastService.ShowSuccess("Added Successfully");
        else if (result.IsSuccessStatusCode)
            ToastService.ShowSuccess(await result.Content.ReadAsStringAsync());
        else
            ToastService.ShowError(await result.Content.ReadAsStringAsync());
    }

    public async Task<bool> ShowConfirm(string title, string message)
    {
        var parameters = new Dictionary<string, object> { { nameof(ConfirmDialog.Text), message } };
        var confirm = ModalService.Show(typeof(ConfirmDialog), title, parameters, null);
        await confirm.Closing;

        return confirm.Value.CastTo<bool?>() == true;
    }

    public async Task<bool> ShowDeleteConfirmation(string item)
        => await ShowConfirm("Delete Confirmation", $"Are you sure to delete this {item}?");

    public void GoTo(string PageRoute, Guid? Id = null)
        => Navigation.NavigateTo($"/{PageRoute}{(Id.HasValue ? "/" + Id : "")}");
}
