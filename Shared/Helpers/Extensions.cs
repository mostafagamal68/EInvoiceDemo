using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
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
    public static T? CastTo<T>(this object? value) => (T?)value;
    public static T? CastAs<T>(this object value) where T : class => value as T;
    public static string ToDateString(this DateTime Value)
        => Value.ToString("dd/MM/yyyy hh:mm:ss tt");

    public static IQueryable<T> OrderWith<T>(this IQueryable<T> source, string? Field, SortingType SortingType = SortingType.Asc)
        => Field.HasValue() ?
           SortingType == SortingType.Asc ?
           source.OrderBy(c => EF.Property<object>(c, Field!))
           :
           source.OrderByDescending(c => EF.Property<object>(c, Field!))
           :
           source;

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool Condition, Expression<Func<T, bool>> expression)
        => Condition ? source.Where(expression) : source;

    public static async ValueTask<T> FindOrErrorAsync<T>(this DbSet<T> source, Guid Id, Exception? exception = null) where T : class
        => await source.FindAsync(Id) ?? throw exception ?? new KeyNotFoundException();

    public static async Task<T> FirstOrErrorAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> expression, Exception? exception = null) where T : class
        => await source.FirstOrDefaultAsync(expression) ?? throw exception ?? new KeyNotFoundException();
        
    public static T CloneJson<T>(this T source)
        => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace })!;
}
