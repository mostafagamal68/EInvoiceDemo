using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

public interface IMapper<TDto, TEntity>
    where TDto : DtoBase
    where TEntity : Entity
{
    [MapperIgnoreTarget(nameof(Entity.CreationDate))]
    TDto CreateDtoFromEntity(TEntity entity);
    TEntity CreateEntityFromDto(TDto dto);
    KeyValue CreateKeyValue(TEntity entity);
    void UpdateEntityFromDto(TDto dto, TEntity entity);
}
