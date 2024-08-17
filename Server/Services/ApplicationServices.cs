using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Server.Mappers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Services;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandler>();
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
