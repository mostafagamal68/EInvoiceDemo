using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Repositories.Mappers;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Repositories;

[Mapper]
public partial class CustomerMapper : KeyValueMapper, IMapper<CustomerDto, Customer>
{
    public partial CustomerDto CreateDtoFromEntity(Customer entity);

    public KeyValue CreateKeyValue(Customer entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.CustomerName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(CustomerDto dto, Customer entity);
}
