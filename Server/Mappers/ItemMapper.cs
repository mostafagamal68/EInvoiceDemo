using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs.Item;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

[Mapper]
public partial class ItemMapper : KeyValueMapper, IMapper<ItemDto, Item>
{
    public partial ItemDto CreateDtoFromEntity(Item entity);

    public partial Item CreateEntityFromDto(ItemDto dto);

    public KeyValue CreateKeyValue(Item entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.ItemName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(ItemDto dto, Item entity);
}
