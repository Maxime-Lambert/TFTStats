using Refit;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed class GetSummonerMatchIdsParameters(int start, int count, string apiKey)
{
    [AliasAs("start")]
    public int Start { get; set; } = start;
    [AliasAs("count")]
    public int Count { get; set; } = count;
    [AliasAs("api_key")]
    public string ApiKey { get; set; } = apiKey;
}