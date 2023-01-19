using System.Diagnostics;

using static AntsTSP.Helper;

namespace AntsTSP;
internal class TSPAlgorithm
{
    private readonly Random _random;
    private readonly int[,] _weights;

    private double[,] _visibility;
    private double[,] _pheromons;

    private List<int> _bestCycle;
    private int _previousLength;
    private int _currentLength;
    private int _stableStrike = 0;
    private int _iteration = 0;

    private static List<Ant> InitializeAnts(int commonAmount, int eliteAmount)
    {
        List<Ant> result = new List<Ant>();

        result.AddRange(Enumerable.Range(0, commonAmount).Select(_ => new Ant()));
        result.AddRange(Enumerable.Range(0, eliteAmount).Select(_ => new Ant(AntType.Elite)));

        return result;
    }
    private void FillPheramons(int[,] weights)
    {
        for (int i = 0; i < AntsSettings.VerticesCount; i++)
        {
            for (int j = 0; j < AntsSettings.VerticesCount; j++)
            {
                if (i == j)
                    continue;

                _visibility[i, j] = 1 / (double)weights[i, j];
                _pheromons[i, j] = _random.Next(10, 40) / 100.0;
            }
        }
    }
    private void EvaporatePheramon()
    {
        for (int i = 0; i < AntsSettings.VerticesCount; i++)
        {
            for (int j = 0; j < AntsSettings.VerticesCount; j++)
            {
                _pheromons[i, j] -= AntsSettings.Ro * _pheromons[i, j];
            }
        }
    }
    private void SendAnts(List<Ant> ants)
    {
        for (int i = 0; i < AntsSettings.CommonAnts + AntsSettings.EliteAnts; i++)
        {
            int plantingIndex = _random.Next(0, AntsSettings.VerticesCount);
            ants[i].Travel(plantingIndex, _visibility, _pheromons);
        }
    }
    private void UpdateBestCycle(List<Ant> ants)
    {
        var bestAntsResult = ants.Select(a => new { Ant = a, Length = GetCycleLength(a.Path, _weights), Path = a.Path }).MinBy(anon => anon.Length)!;
        if (bestAntsResult.Length < GetCycleLength(_bestCycle, _weights))
            _bestCycle = bestAntsResult.Path;
    }
    private void UpdatePheramones(List<Ant> ants)
    {
        for (int i = 0; i < AntsSettings.EliteAnts + AntsSettings.CommonAnts; i++)
        {
            double pheromonOffset = ants[i].GetPheromones(GetCycleLength(ants[i].Path, _weights), Helper.GreedyLength(_weights));
            ants[i].PlantPheromon(pheromonOffset, _pheromons);
        }
    }
    public TSPAlgorithm(int[,] weights)
    {
        _random = new Random();
        _weights = weights;
        _iteration = 0;
        _previousLength = int.MaxValue;
        _currentLength = int.MaxValue;

        _visibility = new double[AntsSettings.VerticesCount, AntsSettings.VerticesCount];
        _pheromons = new double[AntsSettings.VerticesCount, AntsSettings.VerticesCount];

        _bestCycle = new List<int>();
    }
    public SolutionInfo Solve(bool printSteps = false)
    {
        Stopwatch sw = Stopwatch.StartNew();
        FillPheramons(_weights);
        //Console.WriteLine($"Lmin: {GreedyLength(_weights)}");
        do
        {
            List<Ant> ants = InitializeAnts(AntsSettings.CommonAnts, AntsSettings.EliteAnts);

            SendAnts(ants);

            UpdateBestCycle(ants);

            EvaporatePheramon();
            UpdatePheramones(ants);

            _currentLength = GetCycleLength(_bestCycle, _weights);
            if (_currentLength == _previousLength)
            {
                _stableStrike++;
            }
            else
            {
                if (printSteps)
                    Console.WriteLine($"New best length equals: {_currentLength}");
                _stableStrike = 0;
            }
            _previousLength = _currentLength;
        }
        while (_stableStrike < 150 && _iteration++ < 1000);
        sw.Stop();
        var info = new SolutionInfo(
            _bestCycle,
            GetCycleLength(_bestCycle, _weights),
            _iteration,
            (int)sw.Elapsed.TotalSeconds,
            AntsSettings.CommonAnts,
            AntsSettings.EliteAnts,
            AntsSettings.Alfa,
            AntsSettings.Beta,
            AntsSettings.Ro);
        //Console.WriteLine("Best cycle after optimization:");
        //_bestCycle.Print();

        return info;
    }
}