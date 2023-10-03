using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceDemo.Shared.Helpers;

public static class Extensions
{
    public static bool HasValue(this string? Value) => !string.IsNullOrEmpty(Value) && !string.IsNullOrWhiteSpace(Value);
    public static string ToDateString(this DateTime Value) => Value.ToString("dd/MM/yyyy hh:mm:ss tt");
    public static IQueryable<T> OrderWith<T>(this IQueryable<T> source, string? Field, bool Asc = true)
        => Field.HasValue() ?
           Asc ?
           source.OrderBy(c => EF.Property<object>(c, Field))
           :
           source.OrderByDescending(c => EF.Property<object>(c, Field))
           : 
           source;

}
