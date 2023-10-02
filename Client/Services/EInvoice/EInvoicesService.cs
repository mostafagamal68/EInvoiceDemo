using EInvoiceDemo.Shared.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class EInvoicesService : IEInvoicesService
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    const string api = "api/EInvoices";
    public EInvoicesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<EInvoicesFilter?> GetList(EInvoicesFilter? filter)
    {
        var response = await _httpClient.PostAsJsonAsync($"{api}/Filter", filter);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<EInvoicesFilter>(content, _options);
    }
    public async Task<EInvoiceDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<EInvoiceDto>($"{api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{api}/Code");
    public async Task<HttpResponseMessage> Create(EInvoiceDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(EInvoiceDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
