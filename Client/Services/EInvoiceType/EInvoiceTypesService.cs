using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
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
    public async Task<EInvoiceTypeDto?> GetSingle(decimal? Id)
        => await _httpClient.GetFromJsonAsync<EInvoiceTypeDto>($"{api}/{Id}");
    public async Task<HttpResponseMessage> Create(EInvoiceTypeDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(EInvoiceTypeDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(decimal? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
