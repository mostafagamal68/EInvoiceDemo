using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class EInvoiceTypesService : IEInvoiceTypesService
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    const string api = "api/EInvoiceTypes";
    public EInvoiceTypesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<EInvoiceTypesFilter?> GetList(EInvoiceTypesFilter? filter)
    {
        var response = await _httpClient.PostAsJsonAsync($"{api}/Filter", filter);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<EInvoiceTypesFilter>(content, _options);
    }
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
    => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{api}/KeyValue?filter={filter}"); 
    public async Task<EInvoiceTypeDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<EInvoiceTypeDto>($"{api}/{Id}");
    public async Task<HttpResponseMessage> Create(EInvoiceTypeDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(EInvoiceTypeDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
