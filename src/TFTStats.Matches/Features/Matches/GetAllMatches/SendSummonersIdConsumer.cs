using Contracts.SummonerEvents;

using MassTransit;

using Microsoft.Extensions.Options;

using Refit;

using TFTStats.Matches.Entities;
using TFTStats.Matches.Options;
using TFTStats.Matches.Persistence;
using TFTStats.Matches.RiotApi;
using TFTStats.Matches.RiotApi.MatchesApi;

namespace TFTStats.Matches.Features.Matches.GetAllMatches;

internal sealed class SendSummonersIdConsumer(ApplicationDbContext dbContext, IRiotApi riotApi, IOptions<RiotApiOptions> options, ILogger<SendSummonersIdConsumer> logger) : IConsumer<SendSummonersPuuidEvent>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IRiotApi _riotApi = riotApi;
    private readonly IOptions<RiotApiOptions> _options = options;
    private readonly ILogger<SendSummonersIdConsumer> _logger = logger;

    async Task IConsumer<SendSummonersPuuidEvent>.Consume(ConsumeContext<SendSummonersPuuidEvent> context)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));

        _logger.LogRabbitMQConsumed(nameof(SendSummonersPuuidEvent));

        foreach (var summonerId in context.Message.SummonersPuuids)
        {
            var matchesReponse = await _riotApi.GetSummonerMatchIds(summonerId, new GetSummonerMatchIdsParameters(0, 5, _options.Value.Key)).ConfigureAwait(false);
            if (matchesReponse.Error is not null)
            {
                throw new BadHttpRequestException($"Can't request matches for summoner {summonerId}");
            }
            foreach (var matchId in matchesReponse.Content!)
            {
                if (!_dbContext.Matches.Any(match => match.Id == matchId))
                {
                    var matchInformation = await _riotApi.GetMatchInformations(matchId, new GetMatchInformationsParameters(_options.Value.Key)).ConfigureAwait(false);
                    if (matchInformation.Error is not null)
                    {
                        if (matchInformation.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                        {
                            return;
                        }
                        throw new BadHttpRequestException($"Can't request match {matchId} informations");
                    }
                    await AddMatchInformations(matchId, matchInformation).ConfigureAwait(false);
                }
            }
        }
    }

    private async Task AddMatchInformations(string matchId, ApiResponse<MatchResponse> matchInformation)
    {
        var matchInformations = matchInformation.Content!.info;
        var matchEntity = new Match(matchId, matchInformations.GameVersion, matchInformations.GameType,
            matchInformations.participants.Select(p => new Participant(p.level, p.placement, p.win,
                                                            p.traits.Select(t => new Trait(t.name.Split('_')[1], t.NumberOfUnits, t.CurrentTier, t.TotalTier, p.puuid, Guid.NewGuid(), matchId)).ToList(),
                                                            p.units.Select(u => new Unit(u.CharacterId.Split('_')[1], u.tier, u.itemNames.AsEnumerable(), p.puuid, Guid.NewGuid(), matchId)).ToList(),
                                                            matchId,
                                                            p.puuid)).ToList());
        _ = await _dbContext.Matches.AddAsync(matchEntity).ConfigureAwait(false);
        _ = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
