using VertexABC;

internal class Program
{
    private static void Main(string[] args)
    {
        int[][] graph =
            new int[5][] {
            new int[] { 1},
            new int[] {0,3,4},
            new int[] {3,4},
            new int[] {1,2,4},
            new int[] {1,3,2}
        };

        int numBees = 20;
        int numOnlookers = 8;
        int numScouts = 2;
        int lowerBound = 1;
        int upperBound = 5;

        ABC abc = new ABC(graph, numBees, numOnlookers, numScouts, lowerBound, upperBound);

        int[] bestSolution = abc.Solve();
        int colors = bestSolution.Distinct().Count();
        if (IsCorrect(graph, bestSolution))
        {
            Console.WriteLine("Colors used: " + colors);
            for (int i = 0; i < bestSolution.Length; i++)
            {
                Console.Write(i + " = " + bestSolution[i] + ",\n");
            }
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