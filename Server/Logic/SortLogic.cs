using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Logic;

public static class SortLogic
{
    public static IOrderedQueryable<T> OrderWith<T>(this IQueryable<T> source, string Field, bool Asc = true)
    {
        if (Asc)
            return source.OrderBy(c => EF.Property<object>(c, Field));
        else
            return source.OrderByDescending(c => EF.Property<object>(c, Field));
    }
}
