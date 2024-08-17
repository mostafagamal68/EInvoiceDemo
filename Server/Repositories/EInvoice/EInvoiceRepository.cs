using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

internal class EInvoiceRepository(EInvoiceContext context)
    : GenericRepository<EInvoice>(context)
    , IEInvoiceRepository
{
    public override IQueryable<EInvoice> Query()
        => base.Query()
            .Include(c => c.Customer)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.EInvoiceLineTaxes).ThenInclude(c => c.Tax)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.Item)
            .AsQueryable();
}
