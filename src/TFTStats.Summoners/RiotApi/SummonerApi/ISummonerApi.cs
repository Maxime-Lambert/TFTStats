using Refit;

namespace TFTStats.Summoners.RiotApi.SummonerApi;

public interface ISummonerApi
{
    [Get("/tft/summoner/v1/summoners/{encryptedSummonerId}")]
    Task<ApiResponse<SummonerResponse>> GetSummonerPuuidAsync(string encryptedSummonerId, [Query] GetSummonersQueryParameters parameters);
}
