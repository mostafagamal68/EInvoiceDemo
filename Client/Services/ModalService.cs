namespace EInvoiceDemo.Client.Services;

public class ModalService : IModalService
{
    public event Func<ModalData, Task> OnModalOpened;
    public event Func<ModalData, Task> OnModalClosed;

    public void Close(ModalData modal)
    {
        OnModalClosed.Invoke(modal);
    }

    public ModalData Show(Type component, string title, Dictionary<string, object> parameters, Func<Task>? afterClose)
    {
        var modal  = new ModalData
        {
            ComponentType = component,
            Title = title,
            Parameters = parameters,
            OnClose = afterClose
        };
        OnModalOpened?.Invoke(modal);
        return modal;
    }

    public async Task Show(ModalData modal)
    {
        OnModalOpened?.Invoke(modal);
        await modal.Closing;

        if (modal.Value is not null)
        {

        }

        if (modal.OnClose is not null)
            await modal.OnClose.Invoke();
    }
}
