using EInvoiceDemo.Server.Data;

namespace EInvoiceDemo.Server.Services;

internal abstract class ContextService
{
    protected readonly EInvoiceContext _context;
    public ContextService(EInvoiceContext context)
    {
        _context = context;
    }
}
