using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs.EInvoice;

namespace EInvoiceDemo.Server.Handlers.EInvoiceHandler;

public interface IEInvoiceHandler : IGenericHandler<EInvoice, EInvoiceDto, EInvoicesFilter>
{
}
