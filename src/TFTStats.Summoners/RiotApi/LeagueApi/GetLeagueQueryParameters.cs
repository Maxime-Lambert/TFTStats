using Refit;

namespace TFTStats.Summoners.RiotApi.LeagueApi;

internal sealed class GetLeagueQueryParameters(string queue, string riotApiKey)
{
    public string? Queue { get; set; } = queue;
    [AliasAs("api_key")]
    public string? RiotApiKey { get; set; } = riotApiKey;
};