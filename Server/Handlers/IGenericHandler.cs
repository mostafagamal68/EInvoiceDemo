using EInvoiceDemo.Shared.Models;
using System.Linq.Expressions;

namespace EInvoiceDemo.Server.Handlers;

public interface IGenericHandler<TEntity, TDto, TFilter>
    where TEntity : Entity
    where TDto : DtoBase
    where TFilter : GlobalFilter<TDto>
{
    Task<List<KeyValue>> GetKeyValue(string? filter, Expression<Func<TEntity, bool>> filterPredicate);
    Task<int> GetCode();
    Task<TFilter> GetList(TFilter? filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryable);
    Task<TDto> GetSingle(Guid id);
    Task Create(TDto dto);
    Task Delete(Guid id);
    Task Edit(TDto dto);
    Task<string> Bulk(Bulk bulk);
}
