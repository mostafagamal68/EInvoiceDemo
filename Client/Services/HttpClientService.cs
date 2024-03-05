using System.Text.Json;

namespace EInvoiceDemo.Client.Services;

internal abstract class HttpClientService
{
    protected readonly HttpClient _httpClient;
    protected readonly JsonSerializerOptions _options;
    protected readonly string _api;
    public HttpClientService(HttpClient httpClient, string api)
    {
        _api = $"api/{api}";
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
}
