using Refit;

namespace TFTStats.Summoners.RiotApi.LeagueApi;

public interface ILeagueApi
{
    [Get("/tft/league/v1/master")]
    Task<ApiResponse<LeagueResponse>> GetMasterSummonersAsync([Query] GetLeagueQueryParameters parameters);

    [Get("/tft/league/v1/grandmaster")]
    Task<ApiResponse<LeagueResponse>> GetGrandMasterSummonersAsync([Query] GetLeagueQueryParameters parameters);

    [Get("/tft/league/v1/challenger")]
    Task<ApiResponse<LeagueResponse>> GetChallengerSummonersAsync([Query] GetLeagueQueryParameters parameters);
}
