using Blazored.Modal;

namespace EInvoiceDemo.Client.Services;

public class MgModal : IMgModal
{
    public event Func<ModalObject, Task> OnModalAdded;
    public event Func<ModalObject, Task> OnModalClosed;

    public void Close(ModalObject modal)
    {
        OnModalClosed.Invoke(modal);
    }

    public ModalObject Show(Type component, string title, ModalParameters parameters, Func<Task>? afterClose = null)
    {
        var modal  = new ModalObject()
        {
            ComponentType = component,
            Title = title,
            Parameters = parameters.ToDictionary(),
            AfterClose = afterClose
        };
        OnModalAdded?.Invoke(modal);
        return modal;
    }
}
