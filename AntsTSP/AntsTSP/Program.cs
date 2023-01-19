using System.Text.Json;
using System.Text.Json.Serialization;

using AntsTSP;

internal class Program
{
    private const string graphPath = "graph.json";
    static Dictionary<Parameter, List<SolutionInfo>> _solutions = new Dictionary<Parameter, List<SolutionInfo>>();
    static int[,] _weight;
    private static void Main(string[] args)
    {
        AntsSettings.VerticesCount = 100;

        AntsSettings.CommonAnts = 35; // 30
        AntsSettings.EliteAnts = 10; // 35
        AntsSettings.Alfa = 3; // 2.5
        AntsSettings.Beta = 2; // 7.5
        AntsSettings.Ro = 0.7; // 0.3
        #region Finding best parameters
        //if (File.Exists(graphPath) == false)
        //{
        //    _weight = Helper.BuildGraph(AntsSettings.VerticesCount, 5, 150);

        //    File.WriteAllText(graphPath, JsonSerializer.Serialize(_weight.ToListOfLists(), new JsonSerializerOptions() { WriteIndented = true }));
        //}
        //else
        //{
        //    _weight = JsonSerializer.Deserialize<List<List<int>>>(File.ReadAllText(graphPath))!.ToArray2D();
        //}
        //FillInfoWith(Parameter.Alfa, 1, 10, 0.5);
        //FillInfoWith(Parameter.Beta, 1, 10, 0.5);
        //FillInfoWith(Parameter.Ro, 0.1, 0.9, 0.1);
        //FillInfoWith(Parameter.CommonAnts, 10, 50, 5);
        //FillInfoWith(Parameter.EliteAnts, 10, 50, 5);

        //foreach (var item in Enum.GetValues(typeof(Parameter)))
        //{
        //    _solutions[(Parameter)item] = _solutions[(Parameter)item].OrderBy(s => s.CycleLength).ToList();
        //}
        #endregion

        _weight = Helper.BuildGraph(AntsSettings.VerticesCount, 5, 150);
        TSPAlgorithm algorithm = new TSPAlgorithm(_weight);
        var info = algorithm.Solve(true);
        info.Print();
        Console.ReadLine();
    }
    static void FillInfoWith(Parameter param, double min, double max, double step)
    {
        if (File.Exists($"Data\\{param}.json"))
        {
            _solutions[param] = JsonSerializer.Deserialize<List<SolutionInfo>>(File.ReadAllText($"Data\\{param}.json"))!;
            var mi = _solutions[param].MinBy(s => s.CycleLength)!;
            AntsSettings.ApplyParameter(param, mi.Get(param));
            return;
        }
        Console.WriteLine($"{param} searching");
        _solutions[param] = new List<SolutionInfo>();
        for (double i = min; i < max; i += step)
        {
            AntsSettings.ApplyParameter(param, i);
            TSPAlgorithm algorithm = new TSPAlgorithm(_weight);
            var info = algorithm.Solve();
            _solutions[param].Add(info);
            Console.WriteLine($"\t{i} done in {info.Seconds}s. Path: {info.CycleLength}");
        }
        var minS = _solutions[param].MinBy(s => s.CycleLength)!;
        AntsSettings.ApplyParameter(param, minS.Get(param));
        File.WriteAllText($"Data\\{param}.json", JsonSerializer.Serialize(_solutions[param], new JsonSerializerOptions() { WriteIndented = true }));
    }
}