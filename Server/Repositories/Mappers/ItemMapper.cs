using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Repositories.Mappers;

[Mapper]
public partial class ItemMapper : KeyValueMapper, IMapper<ItemDto, Item>
{
    public partial ItemDto CreateDtoFromEntity(Item entity);

    public KeyValue CreateKeyValue(Item entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.ItemName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(ItemDto dto, Item entity);
}
