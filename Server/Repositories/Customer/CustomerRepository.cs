using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

internal class CustomerRepository : ContextService, ICustomerRepository
{
    public CustomerRepository(EInvoiceContext context) : base(context) { }

    public async Task Add(Customer model)
    {
        DbModel().Add(model);
        await _context.SaveChangesAsync();
    }

    public Task<string> Bulk(Bulk ids)
    {
        throw new NotImplementedException();
    }

    public DbSet<Customer> DbModel() => _context.Customers;
    
    public async Task Delete(Guid id)
    {
        var customer = await DbModel().FindOrErrorAsync(id);
        if (_context.EInvoices.Any(c => c.CustomerId == id))
            throw new Exception("This Customer used with E-Invoice and cannot be deleted.");

        DbModel().Remove(customer);
        await _context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.CustomerId == id);
    
    public async Task<int> GetCode() => (await DbModel().MaxAsync(c => (int?)c.CustomerCode) ?? 0) + 1;
    
    public async Task<List<KeyValue>> GetKeyValue(string? filter)
        => await DbModel()
                .WhereIf(filter.HasValue(), c => (c.CustomerCode + " " + c.CustomerName).Contains(filter!))
                .Select(c => new KeyValue
                {
                    Key = c.CustomerId,
                    Value = c.CustomerCode + " - " + c.CustomerName
                })
                .Take(30)
                .ToListAsync();
    
    public async Task<CustomersFilter> GetList(CustomersFilter? filter)
    {
        filter ??= new CustomersFilter();

        var query = Query()
                    .WhereIf(filter.CustomerName.HasValue(), c => (c.CustomerCode + " " + c.CustomerName).Contains(filter.CustomerName!));
        
        filter.Pagination = Pagination.GetPagination<Customer, CustomersFilter, CustomerDto>(query, filter);

        filter.Items = await query
            .Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                CustomerCode = c.CustomerCode,
            })
            .OrderWith(filter.SortField, filter.SortApproach)
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount)
            .ToListAsync();

        return filter;
    }

    public async Task<CustomerDto> GetSingle(Guid id)
    {
        var customer = await DbModel().FindOrErrorAsync(id);

        return new CustomerDto
        {
            CustomerId = customer.CustomerId,
            CustomerCode = customer.CustomerCode,
            CustomerName = customer.CustomerName,
        };
    }

    public IQueryable<Customer> Query()
    {
        return DbModel().AsQueryable();
    }

    public async Task Update(CustomerDto dto)
    {
        var Customer = await DbModel().FindAsync(dto.CustomerId);

        Customer.CustomerCode = dto.CustomerCode;
        Customer.CustomerName = dto.CustomerName;

        _context.Entry(Customer).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
