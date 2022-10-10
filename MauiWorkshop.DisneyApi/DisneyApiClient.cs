using Newtonsoft.Json;

namespace MauiWorkshop.DisneyApi;

public class DisneyApiClient
{
    private HttpClient _httpClient;

    public DisneyApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.disneyapi.dev/")
        };
    }

    public async Task<CharactersResponse> GetCharacters(int? page = null)
    {
        var route = "/characters";

        if (page != null)
            route += $"?page={page}";

        try
        {
            var httpResponse = await _httpClient.GetAsync(route).ConfigureAwait(false);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<CharactersResponse>(jsonResponse);

            return response;
        }
        catch(Exception e)
        {
            throw new ApiClientException("Failed to get characters", e);
        }
    }
}

public class ApiClientException : Exception
{
    public ApiClientException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
