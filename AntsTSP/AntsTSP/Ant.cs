namespace AntsTSP;
internal partial class Ant
{
    private readonly Random _random;

    public Ant(AntType antType = AntType.Ordinary)
    {
        _random = new Random();

        UnvisitedVertices = Enumerable.Range(0, AntsSettings.VerticesCount).ToList();
        Path = new List<int>();
        Type = antType;
    }

    public AntType Type { get; set; }
    public List<int> UnvisitedVertices { get; set; }
    public List<int> Path { get; private set; }

    public void Travel(int plantingVertexId, double[,] visibility, double[,] pheramons)
    {
        MoveToVertix(plantingVertexId);
        while (UnvisitedVertices.Count > 0)
        {
            MoveToNext(visibility, pheramons);
        }
        MoveToVertix(plantingVertexId);
    }

    public void MoveToVertix(int transitInd)
    {
        Path.Add(transitInd);
        UnvisitedVertices.Remove(transitInd);
    }
    public void MoveToNext(double[,] visibility, double[,] pheromons)
    {
        int transitInd = GetTransitVertixInd(visibility, pheromons);
        MoveToVertix(transitInd);
    }
    public int GetTransitVertixInd(double[,] visibility, double[,] pheromons)
    {
        int? transitVertexInd = null;

        List<double> transitProbabilities = FindTransitProbabilities(visibility, pheromons);

        if (Math.Round(transitProbabilities.Sum()) != 1)
            throw new Exception("Probabilities don\'t add up to 1");
        //
        var minStr = transitProbabilities.Min().ToString();
        var start = minStr.IndexOf('.');
        int precision = minStr[(start == -1 ? 0 : start)..].Length;
        double randValue = _random.Next(0, precision) / (double)precision;
        // 0 [] 0.00000001 [] 1
        double lowerBound = 0;
        for (int i = 0; i < transitProbabilities.Count; i++)
        {
            if (lowerBound <= randValue && randValue <= lowerBound + transitProbabilities[i])
            {
                transitVertexInd = UnvisitedVertices[i];
                i = transitProbabilities.Count;
            }
            else
            {
                lowerBound += transitProbabilities[i];
            }
        }
        return transitVertexInd != null ? transitVertexInd.Value : throw new Exception("Transition index search failed");
    }
    public List<double> FindTransitProbabilities(double[,] visibility, double[,] pheromons)
    {
        List<double> transitProbabilities = new List<double>();
        for (int i = 0; i < UnvisitedVertices.Count; i++)
        {
            transitProbabilities.Add(
                Math.Pow(pheromons[Path.Last(), UnvisitedVertices[i]], AntsSettings.Alfa) *
                Math.Pow(visibility[Path.Last(), UnvisitedVertices[i]], AntsSettings.Beta)
                );
        }
        double adjacentNodeHeuristicSum = transitProbabilities.Sum();
        for (int i = 0; i < transitProbabilities.Count; i++)
        {
            if (adjacentNodeHeuristicSum == 0)
                transitProbabilities[i] = 1.0 / transitProbabilities.Count;
            else
                transitProbabilities[i] /= adjacentNodeHeuristicSum;
        }
        return transitProbabilities;
    }
    public double GetPheromones(int cycleLength, int estimatedLength)
    {
        if (Path.Count != AntsSettings.VerticesCount + 1)
            throw new InvalidOperationException("Can`t get pheromones. Ant has not completed the path");

        return Type switch {
            AntType.Ordinary => (double)estimatedLength / cycleLength,
            AntType.Elite => 2 * (double)estimatedLength / cycleLength,
            _ => throw new NotImplementedException()
        };
    }
    public void PlantPheromon(double pheromonAmount, double[,] pheromons)
    {
        if (Path.Count != AntsSettings.VerticesCount + 1)
            throw new InvalidOperationException("Can`t get pheromones. Ant has not completed the path");

        for (int i = 0; i < Path.Count - 1; i++)
        {
            pheromons[Path[i], Path[i + 1]] += pheromonAmount;
        }
    }
}