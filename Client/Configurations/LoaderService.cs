namespace EInvoiceDemo.Client.Configurations;

public class LoaderService
{
    public bool IsLoading { get; set; } = false;
    public event Action OnChange;
    public void ToggleLoader()
    {
        IsLoading = !IsLoading;
        OnChange.Invoke();
    }
}
