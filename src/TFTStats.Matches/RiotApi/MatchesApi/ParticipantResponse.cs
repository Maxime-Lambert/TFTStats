namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed record ParticipantResponse(
    int level,
    int placement,
    IReadOnlyList<TraitResponse> traits,
    IReadOnlyList<UnitResponse> units,
    bool win,
    string puuid);
