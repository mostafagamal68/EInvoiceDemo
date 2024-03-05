using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Repositories;

public interface ITaxRepository : IGenericRepository<Tax, TaxDto, TaxesFilter>
{
}
