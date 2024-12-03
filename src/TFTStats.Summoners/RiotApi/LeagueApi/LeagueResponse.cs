namespace TFTStats.Summoners.RiotApi.LeagueApi;

internal sealed record LeagueResponse(
    IReadOnlyList<EntryResponse> entries
);
