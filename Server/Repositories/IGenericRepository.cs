using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Server.Repositories;

public interface IGenericRepository<TModel, TDto, TFilter>
    where TModel : Entity 
    where TDto : DtoBase 
    where TFilter : GlobalFilter<TDto>
{
    DbSet<TModel> DbModel();
    IQueryable<TModel> Query();
    Task<List<KeyValue>> GetKeyValue(string? filter, Expression<Func<TModel, bool>>? filterPredicate);
    Task<int> GetCode();
    Task<TFilter> GetList(TFilter? filter, Func<IQueryable<TModel>>? queryable);
    Task<TDto> GetSingle(Guid id);
    Task Update(TDto dto);
    Task Add(TModel model);
    Task Delete(Guid id);
    Task<string> Bulk(Bulk bulk);
    bool Exists(Guid id);
}
