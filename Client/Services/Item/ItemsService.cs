using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;

namespace EInvoiceDemo.Client.Services;

internal class ItemsService : HttpClientService, IItemsService
{
    public ItemsService(HttpClient httpClient) : base(httpClient, "Items") { }

    public async Task<ItemsFilter?> GetList(ItemsFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<ItemsFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}"); 
    public async Task<ItemDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<ItemDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(ItemDto dto)
        => await _httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(ItemDto dto)
        => await _httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{_api}/{Id}");
}
