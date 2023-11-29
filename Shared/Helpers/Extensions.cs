using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace EInvoiceDemo.Shared.Helpers;

public static class Extensions
{
    public static string? ToString(this object Value, int Format)
    {
        if (Value.GetType() == typeof(decimal))
        {
            if ((decimal)Value == 0M)
                return "0";
            StringBuilder stringBuilder = new();
            for (int i = 0; i < new string[Format].Length; i++)
                stringBuilder.Append('0');
            return string.Format($"{{0:#.{stringBuilder}}}", Value);
        }
        return Value.ToString();
    }

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
