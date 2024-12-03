namespace TFTStats.Matches.Entities;

internal sealed class Participant
{
    public int? Level { get; set; }
    public int? Placement { get; set; }
    public bool? Win { get; set; }
    public IEnumerable<Trait> Traits { get; set; }
    public IEnumerable<Unit> Units { get; set; }

    public string? MatchId { get; set; }
    public string? Puuid { get; set; }

    public Participant()
    {
        Traits = [];
        Units = [];
    }

    public Participant(int? level, int? placement, bool? win, IEnumerable<Trait> traits, IEnumerable<Unit> units, string? matchId, string puuid)
    {
        Level = level;
        Placement = placement;
        Win = win;
        Traits = traits;
        Units = units;
        MatchId = matchId;
        Puuid = puuid;
    }
}