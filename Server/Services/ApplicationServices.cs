using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Server.Handlers.EInvoiceHandler;
using EInvoiceDemo.Server.Mappers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Server.Repositories.EInvoiceRepo;
using EInvoiceDemo.Shared.DTOs.Customers;
using EInvoiceDemo.Shared.DTOs.Item;
using EInvoiceDemo.Shared.DTOs.Tax;
using EInvoiceDemo.Shared.Services;

namespace EInvoiceDemo.Server.Services;

internal static class ApplicationServices
{
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddScoped(typeof(IGenericHandler<,,>), typeof(GenericHandler<,,>));
        services.AddScoped(typeof(IEInvoiceHandler), typeof(EInvoiceHandler));
        services.AddScoped<IMapper<CustomerDto, Customer>, CustomerMapper>();
        services.AddScoped<IMapper<TaxDto, Tax>, TaxMapper>();
        services.AddScoped<IMapper<ItemDto, Item>, ItemMapper>();
        services.AddScoped<EInvoiceMapper>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEInvoiceRepository, EInvoiceRepository>();
        return services;
    }
}
