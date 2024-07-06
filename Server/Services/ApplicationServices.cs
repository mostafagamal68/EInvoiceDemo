using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Server.Repositories.Mappers;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Services;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandler>();
        services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        services.AddScoped<IMapper<CustomerDto, Customer>, CustomerMapper>();
        services.AddScoped<IMapper<TaxDto, Tax>, TaxMapper>();
        services.AddScoped<IMapper<ItemDto, Item>, ItemMapper>();
        services.AddScoped<IEInvoiceRepository, EInvoiceRepository>();
        return services;
    }
}
