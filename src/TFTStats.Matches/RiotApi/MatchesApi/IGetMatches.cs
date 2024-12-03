using Refit;

namespace TFTStats.Matches.RiotApi.MatchesApi;

internal interface IGetMatches
{

    [Get("/tft/match/v1/matches/by-puuid/{encryptedSummonerPuuid}/ids")]
    Task<ApiResponse<IReadOnlyList<string>>> GetSummonerMatchIds(string encryptedSummonerPuuid, [Query] GetSummonerMatchIdsParameters parameters);

    [Get("/tft/match/v1/matches/{matchId}")]
    Task<ApiResponse<MatchResponse>> GetMatchInformations(string matchId, [Query] GetMatchInformationsParameters parameters);
}
