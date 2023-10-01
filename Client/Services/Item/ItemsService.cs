using EInvoiceDemo.Shared.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class ItemsService : IItemsService
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    const string api = "api/Items";
    public ItemsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<ItemsFilter?> GetList(ItemsFilter? filter)
    {
        var response = await _httpClient.PostAsJsonAsync($"{api}/Filter", filter);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ItemsFilter>(content, _options);
    }
    public async Task<ItemDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<ItemDto>($"{api}/{Id}");
    public async Task<HttpResponseMessage> Create(ItemDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(ItemDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
