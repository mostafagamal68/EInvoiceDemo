using Blazored.Modal;

namespace EInvoiceDemo.Client.Services;

public class ModalService : IModalService
{
    public event Func<ModalData, Task> OnModalAdded;
    public event Func<ModalData, Task> OnModalClosed;

    public void Close(ModalData modal)
    {
        OnModalClosed.Invoke(modal);
    }

    public ModalData Show(Type component, string title, IDictionary<string, object> parameters, Func<Task>? afterClose)
    {
        var modal  = new ModalData(component, title, parameters, afterClose);
        OnModalAdded?.Invoke(modal);
        return modal;
    }

    public async Task Show(ModalData modal)
    {
        OnModalAdded?.Invoke(modal);
        await modal.Closing;

        if (modal.Value is not null)
        {

        }

        if (modal.AfterClose is not null)
            await modal.AfterClose.Invoke();
    }
}
