using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class CustomersService : ICustomersService
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    const string api = "api/Customers";
    public CustomersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<CustomersFilter?> GetList(CustomersFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{api}/Filter", filter)).Content.ReadFromJsonAsync<CustomersFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{api}/KeyValue?filter={filter}");
    public async Task<CustomerDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<CustomerDto>($"{api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{api}/Code");
    public async Task<HttpResponseMessage> Create(CustomerDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(CustomerDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
}
