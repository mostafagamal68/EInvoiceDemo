using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer, CustomerDto, CustomersFilter>
{
}
