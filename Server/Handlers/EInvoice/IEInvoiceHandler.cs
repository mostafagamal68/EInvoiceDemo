using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Handlers;

public interface IEInvoiceHandler : IGenericHandler<EInvoice, EInvoiceDto, EInvoicesFilter>
{
}
