namespace MauiWorkshop.DisneyApi;

public interface IDisneyApiClient
{
    Task<CharactersResponse> GetCharacters(int? page = null);
}
