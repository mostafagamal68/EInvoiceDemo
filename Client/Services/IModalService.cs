using Blazored.Modal;

namespace EInvoiceDemo.Client.Services;

public interface IModalService
{
    event Func<ModalData, Task> OnModalAdded;
    event Func<ModalData, Task> OnModalClosed;

    Task Show(ModalData modal);
    ModalData Show(Type component, string title, ModalParameters parameters, Func<Task>? afterClose);
    void Close(ModalData modal);
}
