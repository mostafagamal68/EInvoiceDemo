namespace EInvoiceDemo.Shared.Helpers;

public static class DecimalExtensions
{
    public static decimal? TrimZeroes(this decimal value)
        => value.ToString().TrimEnd('0', '.').ToDecimal();

    public static decimal RoundTwo(this decimal value)
        => Math.Round(value, 2);

    public static decimal? ToDecimal(this string value)
        => decimal.TryParse(value, out var decimalValue) switch
        {
            true => decimalValue,
            false => null,
        };
}
