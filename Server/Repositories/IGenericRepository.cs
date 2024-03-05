using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

public interface IGenericRepository<TModel, TDto, TFilter>
    where TModel : class 
    where TDto : DtoBase 
    where TFilter : GlobalFilter<TDto>
{
    DbSet<TModel> DbModel();
    IQueryable<TModel> Query();
    Task<List<KeyValue>> GetKeyValue(string? filter);
    Task<int> GetCode();
    Task<TFilter> GetList(TFilter? filter);
    Task<TDto> GetSingle(Guid id);
    Task Update(TDto dto);
    Task Add(TModel model);
    Task Delete(Guid id);
    Task<string> Bulk(Bulk ids);
    bool Exists(Guid id);
}
