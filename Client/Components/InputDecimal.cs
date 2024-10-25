using EInvoiceDemo.Shared.Helpers;
using Microsoft.AspNetCore.Components.Forms;

namespace EInvoiceDemo.Client.Components;

public class InputDecimal : InputNumber<decimal?>
{
    protected override string? FormatValueAsString(decimal? value)
    {
        return base.FormatValueAsString(value.Rnd());
    }
}
