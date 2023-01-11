using VertexABC;

internal class Program
{
    private static void Main(string[] args)
    {
        // Graph graph = Graph.GenerateGraph(50, 25);

        int[][] graph = GenerateGraph(50, 25);
        Console.WriteLine("Graph generated...");
        // Create an instance of the ABC class
        ABC abc = new ABC(graph);

        // Run the ABC algorithm
        int[] bestSolution = abc.Solve();

        // Print the best solution
        Console.WriteLine("Best solution: " + string.Join(", ", bestSolution));
        Console.WriteLine("Used colors: " + bestSolution.ToList().Distinct().Count());
        Console.WriteLine(IsCorrect(graph, bestSolution) ? "Correct" : "Incorrect");

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
            foreach (int neighbor in graph[i])
            {
                if (solution[i] == solution[neighbor])
                    return false;
            }
        }

        return true;
    }
}