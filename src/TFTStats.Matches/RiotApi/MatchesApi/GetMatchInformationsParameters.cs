using Refit;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed class GetMatchInformationsParameters(string apiKey)
{
    [AliasAs("api_key")]
    public string ApiKey { get; set; } = apiKey;
}
