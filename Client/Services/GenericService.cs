using Blazored.Toast.Services;
using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

public class GenericService<TFilter, TDto>(HttpClient httpClient, JsonSerializerOptions options, IToastService toast) : IGenericService<TFilter, TDto>
    where TDto : DtoBase, new()
    where TFilter : GlobalFilter<TDto>, new()
{
    private readonly string _api = $"api/{typeof(TFilter).Name.Replace("Filter", "")}";
    public string Api => _api.Replace("api/", "");
    public async Task<TFilter> GetList(TFilter filter)
        => await TryRequest(async () =>
        {
            var response = await httpClient.PostAsJsonAsync($"{_api}/Filter", filter);
            return await response.Content.ReadFromJsonAsync<TFilter>(options) ?? new();
        });

    public async Task<List<KeyValue>> GetKeyValue(string? filter)
        => await TryRequest(async () =>
        {
            return await httpClient.GetFromJsonAsync<List<KeyValue>>($"{_api}/KeyValue?filter={filter}") ?? [];
        });

    public async Task<TDto> GetSingle(Guid? Id)
        => await TryRequest(async () =>
        {
            return await httpClient.GetFromJsonAsync<TDto>($"{_api}/{Id}") ?? new();
        });

    public async Task<int> GetCode()
        => await TryRequest(async () =>
        {
            return await httpClient.GetFromJsonAsync<int>($"{_api}/Code");
        });

    public async Task<HttpResponseMessage> Create(TDto dto)
        => await TryRequest(async () =>
        {
            return await httpClient.PostAsJsonAsync(_api, dto);
        });

    public async Task<HttpResponseMessage> Edit(TDto dto)
        => await TryRequest(async () =>
        {
            return await httpClient.PutAsJsonAsync(_api, dto);
        });
    public async Task<HttpResponseMessage> Delete(Guid? Id)
        => await TryRequest(async () =>
        {
            return await httpClient.DeleteAsync($"{_api}/{Id}");
        });

    public async Task<HttpResponseMessage> Bulk(List<Guid> Ids, BulkOperation operation)
        => await TryRequest(async () =>
        {
            return await httpClient.PostAsJsonAsync($"{_api}/Bulk/", new Bulk { Guids = Ids, BulkOperation = operation });
        });

    private async Task<T> TryRequest<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (UnauthorizedAccessException)
        {
            toast.ShowError("UnAuthorized");
        }
        catch (Exception ex)
        {
            toast.ShowError(ex.InnerException?.Message ?? ex.Message);
        }

        return await Task.FromResult((T)default!);
    }
}
