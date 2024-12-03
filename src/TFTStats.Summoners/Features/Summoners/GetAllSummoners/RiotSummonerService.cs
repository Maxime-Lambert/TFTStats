using Microsoft.Extensions.Options;

using TFTStats.Summoners.Persistence;
using TFTStats.Summoners.Entities;
using TFTStats.Summoners.Options;
using TFTStats.Summoners.RiotApi;
using TFTStats.Summoners.RiotApi.LeagueApi;
using TFTStats.Summoners.RiotApi.SummonerApi;

namespace TFTStats.Summoners.Features.Summoners.GetAllSummoners;

internal sealed class RiotSummonerService(IRiotApi riotApi, ApplicationDbContext applicationDbContext, IOptions<RiotApiOptions> riotApiOptions) : IGetSummoners
{
    private const string RankedQueueText = "RANKED_TFT";
    private readonly IRiotApi _riotApi = riotApi;
    private readonly ApplicationDbContext _context = applicationDbContext;
    private readonly IOptions<RiotApiOptions> _riotApiOptions = riotApiOptions;

    public async Task AddMasterSummoners()
    {
        var mastersResponse = await _riotApi.GetMasterSummonersAsync(new GetLeagueQueryParameters(RankedQueueText, _riotApiOptions.Value.Key)).ConfigureAwait(false);

        if (mastersResponse.Error is not null)
        {
            throw new BadHttpRequestException("Can't request master summoners");
        }

        await AddSummonersPuuid(mastersResponse.Content!.entries.Select(entry => entry.summonerId)).ConfigureAwait(false);

        _ = await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task AddGrandMasterSummoners()
    {
        var grandMastersResponse = await _riotApi.GetGrandMasterSummonersAsync(new GetLeagueQueryParameters(RankedQueueText, _riotApiOptions.Value.Key)).ConfigureAwait(false);

        if (grandMastersResponse.Error is not null)
        {
            throw new BadHttpRequestException("Can't request grandmaster summoners");
        }

        await AddSummonersPuuid(grandMastersResponse.Content!.entries.Select(entry => entry.summonerId)).ConfigureAwait(false);

        _ = await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task AddChallengerSummoners()
    {
        var challengerResponse = await _riotApi.GetChallengerSummonersAsync(new GetLeagueQueryParameters(RankedQueueText, _riotApiOptions.Value.Key)).ConfigureAwait(false);

        if (challengerResponse.Error is not null)
        {
            throw new BadHttpRequestException("Can't request challengers summoners");
        }

        await AddSummonersPuuid(challengerResponse.Content!.entries.Select(entry => entry.summonerId)).ConfigureAwait(false);

        _ = await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task AddSummonersPuuid(IEnumerable<string> summonerIds)
    {
        foreach (var summonerId in summonerIds)
        {
            if (!_context.Summoners.Any(summoner => summoner.Id == summonerId))
            {
                var summonerResponse = await _riotApi.GetSummonerPuuidAsync(summonerId, new GetSummonersQueryParameters(_riotApiOptions.Value.Key)).ConfigureAwait(false);

                if (summonerResponse.Error is not null)
                {
                    if (summonerResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        return;
                    }
                    throw new BadHttpRequestException($"Can't request puuid for summoner {summonerId}");
                }

                _ = await _context.Summoners.AddAsync(new Summoner(summonerId, summonerResponse.Content!.Puuid)).ConfigureAwait(false);
            }
        }
    }
}
