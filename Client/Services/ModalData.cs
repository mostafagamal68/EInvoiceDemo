namespace EInvoiceDemo.Client.Services;

public class ModalData
{
    private readonly TaskCompletionSource _result = new();// (TaskCreationOptions.RunContinuationsAsynchronously);
    public ModalData(Type componentType, string title)
    {
        ComponentType = componentType;
        Title = title;
    }
    public ModalData(Type componentType, string title, IDictionary<string, object> parameters)
    {
        ComponentType = componentType;
        Title = title;
        Parameters = parameters;
    }
    public ModalData(Type componentType, string title, IDictionary<string, object> parameters, Func<Task> afterClose)
    {
        ComponentType = componentType;
        Title = title;
        Parameters = parameters;
        AfterClose = afterClose;
    }
    public ModalData(Type componentType, string title, Func<Task> afterClose)
    {
        ComponentType = componentType;
        Title = title;
        AfterClose = afterClose;
    }
    public Type ComponentType { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public IDictionary<string, object>? Parameters { get; set; }
    public Func<Task>? AfterClose { get; set; }
    public object? Value { get; set; }
    public void EndAwait() => _result.SetResult();
    public Task Closing => _result.Task;    
}
