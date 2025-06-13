namespace CarInfo.UI.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpService> _logger;

    public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // Generic GET Request
    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while making Get request to {Url}", url);
            return default; // Returns null if an error occurs
        }
    }
}
