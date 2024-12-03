namespace Contracts.SummonerEvents;

public sealed record SendSummonersPuuidEvent(IEnumerable<string> SummonersPuuids) { }
