using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Repositories;

public interface IEInvoiceTypeRepository : IGenericRepository<EInvoiceType, EInvoiceTypeDto, EInvoiceTypesFilter>
{
}
