using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class GenericService<TFilter, TDto>(HttpClient httpClient, JsonSerializerOptions options) : IGenericService<TFilter, TDto>
    where TDto : DtoBase
    where TFilter : GlobalFilter<TDto>
{
    readonly string _api = $"api/{typeof(TFilter).Name.Replace("Filter", "")}";
    public string api => _api.Replace("api/", "");
    public async Task<TFilter> GetList(TFilter filter)
        => await (await httpClient.PostAsJsonAsync($"{_api}/Filter", filter)).Content.ReadFromJsonAsync<TFilter>(options);
    public async Task<List<KeyValue>?> GetKeyValue(string? filter)
        => await httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}");
    public async Task<TDto> GetSingle(Guid? Id)
        => await httpClient.GetFromJsonAsync<TDto>($"{_api}/{Id}");
    public async Task<int> GetCode()
        => await httpClient.GetFromJsonAsync<int>($"{_api}/Code");
    public async Task<HttpResponseMessage> Create(TDto dto)
        => await httpClient.PostAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Edit(TDto dto)
        => await httpClient.PutAsJsonAsync(_api, dto);
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await httpClient.DeleteAsync($"{_api}/{Id}");
    public async Task<HttpResponseMessage> Bulk(List<Guid> Ids, BulkOperation operation)
        => await httpClient.PostAsJsonAsync($"{_api}/Bulk/", new Bulk { Guids = Ids, BulkOperation = operation });
}
