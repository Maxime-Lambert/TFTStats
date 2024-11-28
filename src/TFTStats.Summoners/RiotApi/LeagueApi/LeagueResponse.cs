namespace TFTStats.Summoners.RiotApi.LeagueApi;

public sealed record LeagueResponse(
    IReadOnlyList<EntryResponse> entries
);
