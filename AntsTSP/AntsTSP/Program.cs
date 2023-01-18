using AntsTSP;

internal class Program
{
    private static void Main(string[] args)
    {
        int[,] graph = BuildGraph();
        Config.LminInit(graph);
        List<int> minCycle = TSPAlgorithm.AntColonyOptimization(graph);
        Console.ReadLine();
    }

    public static int[,] BuildGraph()
    {
        int[,] graph = new int[Config.VerticesCount, Config.VerticesCount];
        for (int i = 0; i < graph.GetLength(0); i++)
        {
            for (int j = 0; j < graph.GetLength(1); j++)
            {
                if (i != j)
                {
                    graph[i, j] = Config.Random.Next(Config.WeightRange.Min, Config.WeightRange.Max + 1);
                }
            }
        }

        return graph;
    }
}