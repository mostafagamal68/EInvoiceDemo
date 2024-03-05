using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Services;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandler>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEInvoiceRepository, EInvoiceRepository>();
        services.AddScoped<IEInvoiceTypeRepository, EInvoiceTypeRepository>();
        return services;
    }
}
