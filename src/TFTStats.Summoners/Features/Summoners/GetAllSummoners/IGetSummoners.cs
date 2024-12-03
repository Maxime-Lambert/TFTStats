namespace TFTStats.Summoners.Features.Summoners.GetAllSummoners;

internal interface IGetSummoners
{
    public Task AddMasterSummoners();

    public Task AddGrandMasterSummoners();

    public Task AddChallengerSummoners();
}
