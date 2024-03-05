using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;

namespace EInvoiceDemo.Client.Services;

internal class EInvoiceTypesService : HttpClientService, IEInvoiceTypesService
{
    public EInvoiceTypesService(HttpClient httpClient) : base(httpClient, "EInvoiceTypes") { }

    public async Task<EInvoiceTypesFilter?> GetList(EInvoiceTypesFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<EInvoiceTypesFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}");
    public async Task<EInvoiceTypeDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<EInvoiceTypeDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(EInvoiceTypeDto dto)
        => await _httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(EInvoiceTypeDto dto)
        => await _httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{_api}/{Id}");

}
