using System.Text.Json.Serialization;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed record UnitResponse(
    [property: JsonPropertyName("character_id")] string CharacterId,
    IReadOnlyList<string> itemNames,
    int tier);
