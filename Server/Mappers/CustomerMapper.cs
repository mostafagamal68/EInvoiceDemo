using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs.Customers;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

[Mapper]
public partial class CustomerMapper : KeyValueMapper, IMapper<CustomerDto, Customer>
{
    public partial CustomerDto CreateDtoFromEntity(Customer entity);

    public partial Customer CreateEntityFromDto(CustomerDto dto);

    public KeyValue CreateKeyValue(Customer entity)
    {
        var keyValue = MapKeyValue(entity);
        keyValue.Value = $"{entity.Code} - {entity.CustomerName}";
        return keyValue;
    }

    public partial void UpdateEntityFromDto(CustomerDto dto, Customer entity);
}
