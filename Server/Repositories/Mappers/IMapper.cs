using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Server.Repositories.Mappers;

public interface IMapper<TDto, TEntity>
    where TDto : DtoBase
    where TEntity : Entity
{
    TDto CreateDtoFromEntity(TEntity entity);
    KeyValue CreateKeyValue(TEntity entity);
    void UpdateEntityFromDto(TDto dto, TEntity entity);
}
