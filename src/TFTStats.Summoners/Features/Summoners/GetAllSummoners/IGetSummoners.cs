namespace TFTStats.Summoners.Features.Summoners.GetAllSummoners;

public interface IGetSummoners
{
    public Task<Task> AddMasterSummoners();

    public Task<Task> AddGrandMasterSummoners();

    public Task<Task> AddChallengerSummoners();
}
