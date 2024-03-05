using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;

namespace EInvoiceDemo.Client.Services;

internal class TaxesService : HttpClientService, ITaxesService
{
    public TaxesService(HttpClient httpClient) : base(httpClient, "Taxes") { }

    public async Task<TaxesFilter?> GetList(TaxesFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<TaxesFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}"); 
    public async Task<TaxDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<TaxDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(TaxDto dto)
        => await _httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(TaxDto dto)
        => await _httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{_api}/{Id}");
}
