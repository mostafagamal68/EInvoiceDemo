using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs.Tax;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

[Mapper]
public partial class TaxMapper : KeyValueMapper, IMapper<TaxDto, Tax>
{
    public partial TaxDto CreateDtoFromEntity(Tax entity);

    public partial Tax CreateEntityFromDto(TaxDto dto);

    public KeyValue CreateKeyValue(Tax entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.TaxName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(TaxDto dto, Tax entity);
}
