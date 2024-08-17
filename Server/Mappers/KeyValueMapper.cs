using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

[Mapper]
public partial class KeyValueMapper
{
    [MapperIgnoreTarget(nameof(KeyValue.Id))]
    [MapProperty(nameof(Entity.Id), nameof(KeyValue.Key))]
    public partial KeyValue MapKeyValue(Entity entity);
}
