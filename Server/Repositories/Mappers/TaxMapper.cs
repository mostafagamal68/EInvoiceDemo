using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Repositories.Mappers;

[Mapper]
public partial class TaxMapper : KeyValueMapper, IMapper<TaxDto, Tax>
{
    public partial TaxDto CreateDtoFromEntity(Tax entity);

    public KeyValue CreateKeyValue(Tax entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.TaxName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(TaxDto dto, Tax entity);
}
