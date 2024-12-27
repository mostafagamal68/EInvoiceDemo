namespace EInvoiceDemo.Shared.Helpers;

public static class DateTimeExtensions
{
    public static string ToDateString(this DateTime Value)
        => Value.ToString("dd/MM/yyyy");

    public static string ToDateTimeString(this DateTime Value)
        => Value.ToString("dd/MM/yyyy hh:mm:ss tt");
}
