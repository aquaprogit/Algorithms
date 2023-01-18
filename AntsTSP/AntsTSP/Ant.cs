namespace AntsTSP;
internal class Ant
{
    public Ant(AntType antType = AntType.Ordinary)
    {
        UnvisitedVertices = Enumerable.Range(0, Config.VerticesCount).ToList();
        Path = new List<int>();
        Type = antType;
    }

    public AntType Type { get; set; }
    public List<int> UnvisitedVertices { get; set; }
    public List<int> Path { get; set; }
    public void MoveToVertix(int transitInd)
    {
        Path.Add(transitInd);
        UnvisitedVertices.Remove(transitInd);
    }
    public void MoveToNext(double[,] visibility, double[,] feromon)
    {
        int transitInd = GetTransitVertixInd(visibility, feromon);
        MoveToVertix(transitInd);
    }
    public int GetTransitVertixInd(double[,] visibility, double[,] feromon)
    {
        int? transitVertexInd = null;

        List<double> transitProbabilities = new List<double>(UnvisitedVertices.Count);
        FindTransitProbabilities(ref transitProbabilities, visibility, feromon);
        if (Math.Round(transitProbabilities.Sum()) != 1)
            throw new Exception("Probabilities don\'t add up to 1");
        //
        var minStr = transitProbabilities.Min().ToString();
        var start = minStr.IndexOf('.');
        int precision = minStr.Substring(start == -1 ? 0 : start).Length;
        double randValue = Config.Random.Next(0, precision) / (double)precision;
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
    public void FindTransitProbabilities(ref List<double> transitProbabilities, double[,] visibility, double[,] feromon)
    {
        for (int i = 0; i < UnvisitedVertices.Count; i++)
        {
            transitProbabilities.Add(
                Math.Pow(feromon[Path.Last(), UnvisitedVertices[i]], Config.Alpha) *
                Math.Pow(visibility[Path.Last(), UnvisitedVertices[i]], Config.Beta)
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
    }
    public double GetFeromones(int[,] weights)
    {
        if (Path.Count != Config.IterFeromonAdd + 1)
            throw new InvalidOperationException("Can`t get pheromones. Ant has not completed the path");
        if (Config.Lmin == null)
            throw new NullReferenceException(nameof(Config.Lmin));

        int cycleLength = TSPAlgorithm.GetCycleLength(Path, weights);

        return Type switch {
            AntType.Ordinary => (double)Config.Lmin! / cycleLength,
            AntType.FeromoneElite => 2 * (double)Config.Lmin! / cycleLength,
            _ => throw new Exception("Ant type is not specified, cant determine Pheromones"),
        };
    }
    public void PlantFeromon(double feromonAmount, double[,] feromon)
    {
        if (Path.Count != Config.IterFeromonAdd + 1)
            throw new InvalidOperationException("Can`t get pheromones. Ant has not completed the path");

        for (int i = 0; i < Path.Count - 1; i++)
        {
            feromon[Path[i], Path[i + 1]] += feromonAmount;
        }
    }
    public enum AntType
    {
        Ordinary,
        FeromoneElite,
    }
}