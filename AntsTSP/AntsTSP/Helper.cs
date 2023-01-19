namespace AntsTSP;

public static class Helper
{
    private static Random _random = new Random();
    private static int _greedyLength = int.MaxValue;
    public static int[,] BuildGraph(int vertexCount, int minWeight = 1, int maxWeight = 40)
    {
        if (minWeight > maxWeight)
            throw new ArgumentOutOfRangeException("Min value should be less than Max");

        int[,] graph = new int[vertexCount, vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = 0; j < vertexCount; j++)
            {
                if (i != j)
                    graph[i, j] = _random.Next(minWeight, maxWeight);
            }
        }

        return graph;
    }
    public static int GreedyLength(int[,] weights)
    {
        if (_greedyLength == int.MaxValue)
        {
            List<int> visited = new List<int>(AntsSettings.VerticesCount) { 0 };
            for (int i = 0; i < AntsSettings.VerticesCount - 1; i++)
            {
                int lowestWeightInd = visited.Contains((visited.Last() + 1) % weights.GetLength(1)) ?
                    (visited.Last() + 2) % weights.GetLength(1) :
                    (visited.Last() + 1) % weights.GetLength(1);

                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    if (j != visited.Last() && visited.Contains(j) == false &&
                        weights[visited.Last(), j] < weights[visited.Last(), lowestWeightInd])
                    {
                        lowestWeightInd = j;
                    }
                }
                visited.Add(lowestWeightInd);
            }
            visited.Add(0);
            //Console.WriteLine("Greedy algorithm\'s path: ");
            //visited.Print();
            _greedyLength = GetCycleLength(visited, weights);
            Console.WriteLine("Greedy length is " + _greedyLength);
        }

        return _greedyLength;
    }
    public static int GetCycleLength(List<int> cycle, int[,] weights)
    {
        if (cycle.Count == 0)
            return int.MaxValue;

        int length = 0;
        for (int i = 0; i < cycle.Count - 1; i++)
        {
            length += weights[cycle[i], cycle[i + 1]];
        }
        return length;
    }
}