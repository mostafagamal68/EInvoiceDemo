using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class GenericService<TFilter, TDto> : IGenericService<TFilter, TDto>
    where TDto : DtoBase
    where TFilter : GlobalFilter<TDto>
{
    readonly HttpClient _httpClient;
    readonly JsonSerializerOptions _options;
    string api;
    public GenericService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public string Api { set => api = $"api/{value}"; }
    public async Task<TFilter> GetList(TFilter filter)
        => await (await _httpClient.PostAsJsonAsync($"{api}/Filter", filter)).Content.ReadFromJsonAsync<TFilter>(_options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await _httpClient.GetFromJsonAsync<List<KeyValue>>($"{api}/KeyValue?filter={filter}");
    public async Task<TDto> GetSingle(Guid? Id)
        => await _httpClient.GetFromJsonAsync<TDto>($"{api}/{Id}");
    public async Task<int> GetCode()
        => await _httpClient.GetFromJsonAsync<int>($"{api}/Code");
    public async Task<HttpResponseMessage> Create(TDto dto)
        => await _httpClient.PostAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Edit(TDto dto)
        => await _httpClient.PutAsJsonAsync(api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await _httpClient.DeleteAsync($"{api}/{Id}");
    public async Task<HttpResponseMessage> Bulk(List<Guid> Ids, BulkOperation operation)
        => await _httpClient.PostAsJsonAsync($"{api}/Bulk/", new Bulk { Guids = Ids, BulkOperation = operation });
}
