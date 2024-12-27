using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Shared.Helpers;

public static class QueryExtensions
{
    public static IQueryable<T> OrderWith<T, TDto>(this IQueryable<T> source, GlobalFilter<TDto> filter) where TDto : DtoBase
    {
        if (string.IsNullOrEmpty(filter.SortField))
            filter.SortField = nameof(Entity.ModifiedDate);

        return 
            filter.SortApproach == SortingType.Desc ?
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
}
