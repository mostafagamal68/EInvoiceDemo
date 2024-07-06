using Blazored.Modal;

namespace EInvoiceDemo.Client.Services;

public interface IMgModal
{
    event Func<ModalObject, Task> OnModalAdded;
    event Func<ModalObject, Task> OnModalClosed;

    ModalObject Show(Type component, string title, ModalParameters parameters, Func<Task>? afterClose = null);
    void Close(ModalObject modal);
}
