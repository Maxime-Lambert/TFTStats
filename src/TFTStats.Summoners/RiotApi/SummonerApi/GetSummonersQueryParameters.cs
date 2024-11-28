using Refit;

namespace TFTStats.Summoners.RiotApi.SummonerApi;

public class GetSummonersQueryParameters(string riotApiKey)
{
    [AliasAs("api_key")]
    public string? RiotApiKey { get; set; } = riotApiKey;
};