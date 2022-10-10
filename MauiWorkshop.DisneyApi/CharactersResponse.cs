using System;
using Newtonsoft.Json;

namespace MauiWorkshop.DisneyApi;

public class CharactersResponse
{
    [JsonProperty("data")]
    public List<Character> Data { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("totalPages")]
    public int TotalPages { get; set; }

    [JsonProperty("nextPage")]
    public string NextPage { get; set; }
}