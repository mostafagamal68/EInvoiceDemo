using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceDemo.Shared.Helpers;

public static class Extensions
{
    public static bool HasValue(this string? Value) => !string.IsNullOrEmpty(Value) && !string.IsNullOrWhiteSpace(Value);

}
