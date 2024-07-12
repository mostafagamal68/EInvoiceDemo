namespace EInvoiceDemo.Client.Services;

public class ModalData
{
    private readonly TaskCompletionSource _result = new();// (TaskCreationOptions.RunContinuationsAsynchronously);
    
    public Type ComponentType { get; init; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = [];
    public Func<Task>? AfterClose { get; set; }
    public object? Value { get; set; }
    public void EndAwait() => _result.SetResult();
    public Task Closing => _result.Task;

}
