using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;

namespace EInvoiceDemo.Client.Services;

internal class CustomersService : HttpClientService, ICustomersService
{
    public CustomersService(HttpClient httpClient) : base(httpClient, "Customers") { }

    public async Task<CustomersFilter?> GetList(CustomersFilter? filter)
        => await (await _httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<CustomersFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}");
    public async Task<CustomerDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<CustomerDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(CustomerDto dto)
        => await _httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(CustomerDto dto)
        => await _httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{_api}/{Id}");
}
