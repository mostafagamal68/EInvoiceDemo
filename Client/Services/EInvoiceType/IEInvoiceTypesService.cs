﻿using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

public interface IEInvoiceTypesService
{
    Task<EInvoiceTypesFilter?> GetList(EInvoiceTypesFilter? filter);
    Task<EInvoiceTypeDto?> GetSingle(decimal? Id);
    Task<HttpResponseMessage> Create(EInvoiceTypeDto dto);
    Task<HttpResponseMessage> Edit(EInvoiceTypeDto dto);
    Task<HttpResponseMessage> Delete(decimal? Id);
}
