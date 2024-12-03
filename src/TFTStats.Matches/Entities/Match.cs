namespace TFTStats.Matches.Entities;

internal sealed class Match
{
    public string? Id { get; set; }
    public string? GameVersion { get; set; }
    public string? GameType { get; set; }
    public IEnumerable<Participant> Participants { get; set; }

    public Match()
    {
        Participants = [];
    }

    public Match(string? id, string? gameVersion, string? gameType, IEnumerable<Participant> participants)
    {
        Id = id;
        GameVersion = gameVersion;
        GameType = gameType;
        Participants = participants;
    }
}
