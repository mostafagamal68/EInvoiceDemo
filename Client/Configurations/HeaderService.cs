namespace EInvoiceDemo.Client.Configurations;

public class HeaderService
{
    public string Title { get; set; } = "Home";

    public event Action OnChange;
    public void ChangeHeaderAndTitle(string title)
    {
        Title = title;
        OnChange.Invoke();
    }
}
