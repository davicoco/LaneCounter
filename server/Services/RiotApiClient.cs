namespace server.Services;

public class RiotApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public RiotApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}