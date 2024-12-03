using System.Text.Json.Serialization;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal sealed record InfoResponse(
    [property: JsonPropertyName("game_version")] string GameVersion,
    int mapId,
    IReadOnlyList<ParticipantResponse> participants,
    [property: JsonPropertyName("tft_game_type")] string GameType);
