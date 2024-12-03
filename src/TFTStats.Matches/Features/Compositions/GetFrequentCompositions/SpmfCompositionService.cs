using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

using Microsoft.EntityFrameworkCore;

using TFTStats.Matches.Persistence;

namespace TFTStats.Matches.Features.Compositions.GetFrequentCompositions;

internal sealed class SpmfCompositionService(ApplicationDbContext context) : ICalculateFrequentCompositions
{
    private const string featuresFolderName = "Features";
    private const string compositionsFolderName = "Compositions";
    private const string getFrequentCompositionsFolderName = "GetFrequentCompositions";
    private const string spmfFolderName = "SPMF";
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<string>> GetFrequentCompositionsAsync()
    {
        DirectoryInfo di = new(Assembly.GetEntryAssembly()!.Location);
        var spmfPath = Path.Combine(di.Parent!.FullName, featuresFolderName, compositionsFolderName, getFrequentCompositionsFolderName, spmfFolderName);
        var unitLists = await _context.Matches.Select(m => m.Participants.Select(p => p.Units.Select(u => u.Name))).ToListAsync().ConfigureAwait(false);
        var intToUnitConversion = await WriteInputFile(spmfPath, unitLists).ConfigureAwait(false);
        await WriteAprioriOutputFile(spmfPath).ConfigureAwait(false);
        var compositions = await GetCompositionsFromOutputFile(spmfPath).ConfigureAwait(false);
        return compositions.Select(compo => string.Join(' ', compo.Select(unit => intToUnitConversion[int.Parse(unit, CultureInfo.InvariantCulture)]).Order()));
    }

    private static async Task<IEnumerable<IEnumerable<string>>> GetCompositionsFromOutputFile(string spmfPath)
    {
        var compos = await File.ReadAllLinesAsync(Path.Combine(spmfPath, "output.txt")).ConfigureAwait(false);
        var sevenChampionsCompos = compos.Where(line => line.Split(' ').Length == 10).OrderBy(line =>
        {
            var splittedLine = line.Split(' ');
            return int.Parse(splittedLine[^1], CultureInfo.InvariantCulture);
        });
        IEnumerable<IEnumerable<string>> result = [];
        foreach (var comp in sevenChampionsCompos.Select(comp => comp.Split(' ').Take(7)))
        {
            if (!result.Any(x => x.Intersect(comp).Count() > 4))
            {
                result = result.Append(comp);
            }
        }

        return result;
    }

    private static async Task WriteAprioriOutputFile(string spmfPath)
    {
        using Process myProcess = new();
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.RedirectStandardOutput = true;
        myProcess.StartInfo.FileName = "java";
        var test = Path.Combine(spmfPath, "spmf.jar");
        var test2 = Path.Combine(spmfPath, "unitList.txt");
        var test3 = Path.Combine(spmfPath, "output.txt");
        myProcess.StartInfo.Arguments = $"-jar {test} run Apriori {test2} {test3} 2% 7";
        _ = myProcess.Start();
        await myProcess.WaitForExitAsync().ConfigureAwait(false);
    }

    private static async Task<Dictionary<int, string>> WriteInputFile(string spmfPath, List<IEnumerable<IEnumerable<string?>>> unitLists)
    {
        var unitToIntConversion = new Dictionary<string, int>();
        var intToUnitConversion = new Dictionary<int, string>();
        var namingCounter = 0;
        StringBuilder inputFile = new();
        foreach (var match in unitLists)
        {
            foreach (var participant in match)
            {
                foreach (var unit in participant)
                {
                    if (unit is not "Sion" and not "JayceSummon" and not "Prime" && unitToIntConversion.TryAdd(unit!, namingCounter))
                    {
                        AddIntToUnitConversion(intToUnitConversion, namingCounter, unit);
                        namingCounter++;
                    }
                }
                inputFile = inputFile.Append(string.Join(' ', participant.Where(p => p is not "Sion" and not "JayceSummon" and not "Prime").Select(p => unitToIntConversion[p!]).Order()) + Environment.NewLine);
            }
        }
        await File.WriteAllTextAsync(Path.Combine(spmfPath, "unitList.txt"), inputFile.ToString()).ConfigureAwait(false);
        return intToUnitConversion;
    }

    private static void AddIntToUnitConversion(Dictionary<int, string> intToUnitConversion, int namingCounter, string? unit)
    {
        switch (unit)
        {
            case "Red": intToUnitConversion.Add(namingCounter, "Violet"); break;
            case "Blue": intToUnitConversion.Add(namingCounter, "Powder"); break;
            case "Lieutenant": intToUnitConversion.Add(namingCounter, "Sevika"); break;
            case "Beardy": intToUnitConversion.Add(namingCounter, "Loris"); break;
            case "Gremlin": intToUnitConversion.Add(namingCounter, "Smeech"); break;
            case "Fish": intToUnitConversion.Add(namingCounter, "Steb"); break;
            case "Chainsaw": intToUnitConversion.Add(namingCounter, "Renni"); break;
            case "elise": intToUnitConversion.Add(namingCounter, "Elise"); break;
            case "FlyGuy": intToUnitConversion.Add(namingCounter, "Scar"); break;
            default: intToUnitConversion.Add(namingCounter, unit!); break;
        }
    }
}
