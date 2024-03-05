using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;

namespace EInvoiceDemo.Client.Services;

internal class EInvoicesService : HttpClientService, IEInvoicesService
{
    public EInvoicesService(HttpClient httpClient) : base(httpClient, "EInvoices") { }

    public async Task<EInvoicesFilter?> GetList(EInvoicesFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<EInvoicesFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}"); 
    public async Task<EInvoiceDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<EInvoiceDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(EInvoiceDto dto)
        => await _httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(EInvoiceDto dto)
        => await _httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{_api}/{Id}");
    public async Task<HttpResponseMessage> BulkDelete(List<Guid> Ids)
        => await _httpClient.PostAsJsonAsync($"{_api}/Bulk/", new Bulk { Guids = Ids, BulkOperation = BulkOperation.Delete });
}
