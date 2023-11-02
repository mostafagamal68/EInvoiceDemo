using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace EInvoiceDemo.Shared.Helpers;

public static class Extensions
{
    public static bool HasValue(this string? Value)
        => !string.IsNullOrEmpty(Value) && !string.IsNullOrWhiteSpace(Value);

    public static string ToDateString(this DateTime Value)
        => Value.ToString("dd/MM/yyyy hh:mm:ss tt");

    public static IQueryable<T> OrderWith<T>(this IQueryable<T> source, string? Field, bool Asc = true)
        => Field.HasValue() ?
           Asc ?
           source.OrderBy(c => EF.Property<object>(c, Field))
           :
           source.OrderByDescending(c => EF.Property<object>(c, Field))
           : 
           source;

    public static T CloneJson<T>(this T source)
        => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
}
