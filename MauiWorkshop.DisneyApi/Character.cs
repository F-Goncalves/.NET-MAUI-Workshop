using System;
using Newtonsoft.Json;

namespace MauiWorkshop.DisneyApi;

public class Character
{
    [JsonProperty("films")]
    public List<string> Films { get; set; }

    [JsonProperty("shortFilms")]
    public List<string> ShortFilms { get; set; }

    [JsonProperty("tvShows")]
    public List<string> TvShows { get; set; }

    [JsonProperty("videoGames")]
    public List<string> VideoGames { get; set; }

    [JsonProperty("parkAttractions")]
    public List<string> ParkAttractions { get; set; }

    [JsonProperty("_id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}