using EInvoiceDemo.Server.Mappers;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Server.Handlers;

public class GenericHandler<TEntity, TDto, TFilter>(IGenericRepository<TEntity> repository, IMapper<TDto, TEntity> mapper)
    : IGenericHandler<TEntity, TDto, TFilter>
    where TEntity : Entity
    where TDto : DtoBase
    where TFilter : GlobalFilter<TDto>, new()
{
    public async Task<int> GetCode() => (await repository.Query().MaxAsync(c => EF.Property<int>(c, "Code"))).CastTo<int>() + 1;

    public async Task<List<KeyValue>> GetKeyValue(string? filter, Expression<Func<TEntity, bool>> predicate)
        => await repository.Query()
                .WhereIf(filter.HasValue(), predicate)
                .Select(c => mapper.CreateKeyValue(c))
                .Take(30)
                .ToListAsync();

    public async Task<TFilter> GetList(TFilter? filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryable)
    {
        var query = queryable?.Invoke(repository.Query()) ?? repository.Query();
        filter ??= new TFilter();
        filter.GetPagination(query);
        filter.Items = await query
            .OrderAndPaginate(filter)
            .Select(c => mapper.CreateDtoFromEntity(c))
            .AsNoTracking()
            .ToListAsync();

        return filter;
    }

    public async Task<TDto> GetSingle(Guid id)
    {
        var model = await repository.GetAsync(id);
        return mapper.CreateDtoFromEntity(model);
    }

    public virtual async Task Create(TDto dto)
    {
        var model = mapper.CreateEntityFromDto(dto);
        repository.Add(model);
        await repository.SaveChangesAsync();
    }

    public virtual async Task Delete(Guid id)
    {
        var model = await repository.GetAsync(id);
        repository.Delete(model);
        await repository.SaveChangesAsync();
    }

    public virtual async Task Edit(TDto dto)
    {
        var model = await repository.GetAsync(dto.Id);
        mapper.UpdateEntityFromDto(dto, model);
        repository.Update(model);
        await repository.SaveChangesAsync();
    }

    public async Task<string> Bulk(Bulk bulk)
    {
        switch (bulk.BulkOperation)
        {
            case BulkOperation.Delete:
                {
                    await repository.Query().Where(c => bulk.Guids.Contains(c.Id)).ExecuteDeleteAsync();
                    return "Deleted Successfully";
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
