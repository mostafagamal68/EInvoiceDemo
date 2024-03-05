using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Repositories;

public interface IEInvoiceRepository : IGenericRepository<EInvoice, EInvoiceDto, EInvoicesFilter>
{
    EInvoice AddLogic(EInvoiceDto dto);
}
