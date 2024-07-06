namespace EInvoiceDemo.Client.Services;

public class ModalObject
{
    private readonly TaskCompletionSource _result = new();// (TaskCreationOptions.RunContinuationsAsynchronously);
    public Type ComponentType { get; set; }
    public string Title { get; set; }
    public IDictionary<string, object>? Parameters { get; set; }
    public Func<Task>? AfterClose { get; set; }
    public object? Value { get; set; }
    public void EndAwait() => _result.SetResult();
    public Task Closing => _result.Task;    
}
