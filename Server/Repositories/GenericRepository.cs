using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Repositories.Mappers;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Server.Repositories;

public class GenericRepository<TModel, TDto, TFilter>(EInvoiceContext context, IMapper<TDto, TModel> mapper)
    : IGenericRepository<TModel, TDto, TFilter>
    where TModel : Entity
    where TDto : DtoBase, new()
    where TFilter : GlobalFilter<TDto>, new()
{

    public async Task Add(TModel model)
    {
        DbModel().Add(model);
        await context.SaveChangesAsync();
    }

    public async Task<string> Bulk(Bulk bulk)
    {
        switch (bulk.BulkOperation)
        {
            case BulkOperation.Delete:
                {
                    await Query().Where(c => bulk.Guids.Contains(c.Id)).ExecuteDeleteAsync();
                    return "Deleted Successfully";
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public DbSet<TModel> DbModel() => context.Set<TModel>();

    public async Task Delete(Guid id)
    {
        var model = await DbModel().FindOrErrorAsync(id);
        //if (Query().Any(c => c.Id == id))
        //    throw new Exception("This TModel used with E-Invoice line taxes and cannot be deleted.");
        DbModel().Remove(model);
        await context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.Id == id);

    public async Task<int> GetCode() => (await Query().MaxAsync(c => EF.Property<int>(c, "Code"))).CastTo<int>() + 1;

    public async Task<List<KeyValue>> GetKeyValue(string? filter, Expression<Func<TModel, bool>>? predicate)
        => await Query()
                .WhereIf(filter.HasValue(), predicate)
                .Select(c => mapper.CreateKeyValue(c))
                .Take(30)
                .ToListAsync();

    public async Task<TFilter> GetList(TFilter? filter, Func<IQueryable<TModel>>? queryable)
    {
        var query = queryable?.Invoke() ?? Query();
        filter ??= new TFilter();
        filter.Pagination = Pagination.GetPagination<TModel, TFilter, TDto>(query, filter);
        filter.Items = await query
            .OrderAndPaginate(filter)
            .Select(c => mapper.CreateDtoFromEntity(c))
            .AsNoTracking()
            .ToListAsync();
        return filter;
    }

    public async Task<TDto> GetSingle(Guid id)
    {
        var model = await DbModel().FindOrErrorAsync(id);
        return mapper.CreateDtoFromEntity(model);
    }

    public IQueryable<TModel> Query() => DbModel().AsQueryable();

    public async Task Update(TDto dto)
    {
        var model = await DbModel().FindOrErrorAsync(dto.Id);
        mapper.UpdateEntityFromDto(dto, model);
        context.Update(model);
        await context.SaveChangesAsync();
    }
}
