using VertexABC;

internal class Program
{
    private static void Main(string[] args)
    {
        int[][] graph = GenerateGraph(300, 25);
        //    new int[22][] {
        //    new int[] { 1 },
        //    new int[] { 0, 2, 4 },
        //    new int[] { 1, 6 },
        //    new int[] { 4 },
        //    new int[] { 3, 1, 5 },
        //    new int[] { 4, 7 },
        //    new int[] { 7, 9, 10, 2 },
        //    new int[] { 5, 8, 20, 9, 6 },
        //    new int[] { 7 },
        //    new int[] { 7, 6 },
        //    new int[] { 6, 21, 11 },
        //    new int[] { 10, 14, 13, 12 },
        //    new int[] { 11 },
        //    new int[] { 11 },
        //    new int[] { 11, 21, 15 },
        //    new int[] { 14, 19 },
        //    new int[] { 19 },
        //    new int[] { 21, 18 },
        //    new int[] { 17 },
        //    new int[] { 17, 16, 15 },
        //    new int[] { 7, 17 },
        //    new int[] { 10, 14, 17 }
        //};

        int numBees = 50;
        int numOnlookers = 10;
        int numScouts = 20;
        int lowerBound = 1;
        int upperBound = 50;

        ABC abc = new ABC(graph, numBees, numOnlookers, numScouts, lowerBound, upperBound);

        int[] bestSolution = abc.Solve();
        int colors = bestSolution.Distinct().Count();
        if (IsCorrect(graph, bestSolution))
        {
            Console.WriteLine(string.Join(", ", bestSolution));
        }
        else
        {
            Console.WriteLine("The solution is incorrect!");
        }
        Console.ReadLine();
    }
    private static int[][] GenerateGraph(int numVertices, int maxEdges)
    {
        Random rand = new Random();
        int[][] graph = new int[numVertices][];
        for (int i = 0; i < numVertices; i++)
        {
            List<int> neighbors = new List<int>();
            for (int j = 0; j < numVertices; j++)
            {
                if (i != j && rand.NextDouble() < 0.5)
                    neighbors.Add(j);
            }
            while (neighbors.Count < 2)
            {
                int neighbor = rand.Next(numVertices);
                if (neighbor != i && !neighbors.Contains(neighbor))
                    neighbors.Add(neighbor);
            }

            while (neighbors.Count > maxEdges)
            {
                int index = rand.Next(neighbors.Count);
                neighbors.RemoveAt(index);
            }
            graph[i] = neighbors.ToArray();
        }
        return graph;
    }
    private static bool IsCorrect(int[][] graph, int[] solution)
    {
        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                if (solution[i] == solution[graph[i][j]])
                {
                    return false;
                }
            }
        }
        return true;
    }


}