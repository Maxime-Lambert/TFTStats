using System.Text.Json.Serialization;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed record TraitResponse(
    string name,
    [property: JsonPropertyName("num_units")] int NumberOfUnits,
    [property: JsonPropertyName("tier_current")] int CurrentTier,
    [property: JsonPropertyName("tier_total")] int TotalTier);
