using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Shared.Helpers;

public static class Extensions
{
    public static decimal? Rnd(this decimal? Value)
    {
        if (Value.HasValue)
            return Value.Value.Rnd();
        return Value;
    }
    public static decimal Rnd(this decimal Value)
    {
        return Math.Round(Value, 2);
    }

    public static void GetPagination<TEntity, TDto>(this GlobalFilter<TDto> filter, IQueryable<TEntity> query)
        where TDto : DtoBase
        where TEntity : Entity
    {
        var count = query.Count();
        filter.Pagination.CurrentPage.ToString();
        while ((filter.Pagination.PageNo * filter.Pagination.RowsCount) > count) filter.Pagination.PageNo--;
        filter.Pagination = new Pagination
        {
            PageNo = filter.Pagination.PageNo,
            CurrentPage = filter.Pagination.PageNo,
            RowsCount = filter.Pagination.RowsCount,
            TotalRows = count,
            StartPage = Pagination.GetFirstPage((filter.Pagination.PageNo + 1).ToString()),
            PagesCount = (int)Math.Ceiling((decimal)count / filter.Pagination.RowsCount)
        };
    }

    public static bool HasValue(this string? Value)
        => !string.IsNullOrEmpty(Value) && !string.IsNullOrWhiteSpace(Value);

    public static T? CastTo<T>(this object? value)
        => (T?)value;

    public static T? CastAs<T>(this object value) where T : class
        => value as T;

    public static string ToDateString(this DateTime Value)
        => Value.ToString("dd/MM/yyyy hh:mm:ss tt");

    public static IQueryable<T> OrderWith<T, TDto>(this IQueryable<T> source, GlobalFilter<TDto> filter) where TDto : DtoBase
    {
        if (string.IsNullOrEmpty(filter.SortField))
            filter.SortField = nameof(Entity.ModifiedDate);

        return filter.SortApproach == SortingType.Desc ?
        source.OrderByDescending(c => EF.Property<object>(c!, filter.SortField))
        :
        source.OrderBy(c => EF.Property<object>(c!, filter.SortField))
        ;
    }

    public static IQueryable<TEntity> Paginate<TEntity, TDto>(this IQueryable<TEntity> source, GlobalFilter<TDto> filter)
        where TDto : DtoBase
        => source
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount);

    public static IQueryable<TEntity> OrderAndPaginate<TEntity, TDto>(this IQueryable<TEntity> source, GlobalFilter<TDto> filter)
        where TDto : DtoBase
        => source.OrderWith(filter).Paginate(filter);

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool Condition, Expression<Func<T, bool>> expression)
        => Condition ? source.Where(expression) : source;

    public static async Task<T> FindOrErrorAsync<T>(this DbSet<T> source, Guid id, Exception? exception = null) where T : Entity
        => await source.FindAsync(id) ?? throw exception ?? new KeyNotFoundException();

    public static async Task<T> FirstOrErrorAsync<T>(this IQueryable<T> source, Guid id, Exception? exception = null) where T : Entity
        => await source.FirstOrDefaultAsync(c => c.Id == id) ?? throw exception ?? new KeyNotFoundException();

    public static MemberExpression? GetMemberExpression<T, TValue>(this Expression<Func<T, TValue>> expr)
    {
        if (expr.Body is UnaryExpression unary)
            return unary.Operand as MemberExpression;

        return expr.Body as MemberExpression;
    }
    //public static T CloneJson<T>(this T source)
    //    => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace })!;
}
