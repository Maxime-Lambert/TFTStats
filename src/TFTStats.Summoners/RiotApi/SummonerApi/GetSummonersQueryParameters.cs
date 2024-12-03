using Refit;

namespace TFTStats.Summoners.RiotApi.SummonerApi;

internal sealed class GetSummonersQueryParameters(string riotApiKey)
{
    [AliasAs("api_key")]
    public string? RiotApiKey { get; set; } = riotApiKey;
};