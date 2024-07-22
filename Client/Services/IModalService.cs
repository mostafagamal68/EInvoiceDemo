namespace EInvoiceDemo.Client.Services;

public interface IModalService
{
    event Func<ModalData, Task> OnModalOpened;
    event Func<ModalData, Task> OnModalClosed;

    Task Show(ModalData modal);
    ModalData Show(Type component, string title, Dictionary<string, object> parameters, Func<Task>? afterClose);
    void Close(ModalData modal);
}
