using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using Riok.Mapperly.Abstractions;

namespace EInvoiceDemo.Server.Mappers;

[Mapper]
public partial class EInvoiceMapper : IMapper<EInvoiceDto, EInvoice>
{
    [MapNestedProperties(nameof(EInvoice.Customer))]
    public partial EInvoiceDto CreateDtoFromEntity(EInvoice entity);

    [MapNestedProperties(nameof(EInvoiceLine.Item))]
    private partial EInvoiceLineDto CreateEInvoiceLineDto(EInvoiceLine entity);

    [MapNestedProperties(nameof(EInvoiceLineTax.Tax))]
    private partial EInvoiceLineTaxDto CreateEInvoiceLineTaxDto(EInvoiceLineTax entity);

    public partial EInvoice CreateEntityFromDto(EInvoiceDto dto);

    public partial EInvoiceLine CreateEInvoiceLine(EInvoiceLineDto dto);

    public partial EInvoiceLineTax CreateEInvoiceLineTax(EInvoiceLineTaxDto dto);

    public KeyValue CreateKeyValue(EInvoice entity)
    {
        throw new NotImplementedException();
    }

    [MapperIgnoreTarget(nameof(EInvoice.EInvoiceLines))]
    public partial void UpdateEntityFromDto(EInvoiceDto dto, EInvoice entity);

    [MapperIgnoreTarget(nameof(EInvoiceLine.EInvoiceLineTaxes))]
    public partial void UpdateEinvoiceLine(EInvoiceLineDto dto, EInvoiceLine entity);

    public partial void UpdateEinvoiceLineTax(EInvoiceLineTaxDto dto, EInvoiceLineTax entity);

}
