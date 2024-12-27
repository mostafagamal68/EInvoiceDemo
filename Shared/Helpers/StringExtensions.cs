namespace EInvoiceDemo.Shared.Helpers;

public static class StringExtensions
{
    public static int? ToInt(this string value)
        => int.TryParse(value, out var intValue) switch
        {
            true => intValue,
            false => null,
        };

    public static bool HasValue(this string? Value)
        => !string.IsNullOrEmpty(Value) && !string.IsNullOrWhiteSpace(Value);
}
