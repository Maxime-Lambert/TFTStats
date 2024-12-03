namespace TFTStats.Matches.Features.Compositions.GetFrequentCompositions;

internal interface ICalculateFrequentCompositions
{
    Task<IEnumerable<string>> GetFrequentCompositionsAsync();
}
