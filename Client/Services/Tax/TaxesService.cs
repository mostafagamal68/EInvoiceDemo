using EInvoiceDemo.Shared.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class TaxesService : ITaxesService
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    const string api = "api/Taxes";
    public TaxesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<TaxesFilter?> GetList(TaxesFilter? filter)
    {
        var response = await _httpClient.PostAsJsonAsync($"{api}/Filter", filter);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TaxesFilter>(content, _options);
    }
    public async Task<TaxDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<TaxDto>($"{api}/{Id}");
    public async Task<HttpResponseMessage> Create(TaxDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(TaxDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
