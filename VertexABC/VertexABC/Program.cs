using VertexABC;

internal class Program
{
    private static void Main(string[] args)
    {
        int[][] graph = GenerateGraph(250, 25);
        //    new int[5][] {
        //    new int[] { 1},
        //    new int[] {0,3,4},
        //    new int[] {3,4},
        //    new int[] {1,2,4},
        //    new int[] {1,3,2}
        //};
        int numBees = 35;
        int numOnlookers = 5;
        int numScouts = 2;
        int lowerBound = 1;
        int upperBound = 100;

        ABC abc = new ABC(graph, numBees, numOnlookers, numScouts, lowerBound, upperBound);

        Solution bestSolution = abc.Solve(true);
        int colors = bestSolution.ColorSet.Distinct().Count();
        if (IsCorrect(graph, bestSolution.ColorSet))
        {
            Console.WriteLine("Colors used: " + colors);
            Console.WriteLine("Chromatic number: " + ChromaticNumber(graph));
            Console.WriteLine(bestSolution);
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
            int edges = 0;
            for (int j = 0; j < numVertices; j++)
            {
                if (i != j && rand.NextDouble() < 0.5 && edges < maxEdges)
                {
                    neighbors.Add(j);
                    edges++;
                }
            }
            while (neighbors.Count < 2)
            {
                int neighbor = rand.Next(numVertices);
                if (neighbor != i && !neighbors.Contains(neighbor))
                    neighbors.Add(neighbor);
            }
            graph[i] = neighbors.ToArray();
        }
        for (int i = 0; i < numVertices * 0.995; i++)
        {
            int vertex = rand.Next(numVertices / 2);
            int neighbor = graph[vertex][rand.Next(graph[vertex].Length)];
            graph[vertex] = graph[vertex].Where(x => x != neighbor).ToArray();
            graph[neighbor] = graph[neighbor].Where(x => x != vertex).ToArray();
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
    private static int ChromaticNumber(int[][] graph)
    {
        int[] colors = new int[graph.Length];
        Array.Fill(colors, -1);
        colors[0] = 0;

        bool[] availableColors = new bool[graph.Length];
        for (int i = 0; i < graph.Length; i++)
        {
            Array.Fill(availableColors, true);
            for (int j = 0; j < graph[i].Length; j++)
            {
                int neighbor = graph[i][j];
                if (colors[neighbor] != -1)
                    availableColors[colors[neighbor]] = false;
            }

            int color = 0;
            for (int j = 0; j < availableColors.Length; j++)
            {
                if (availableColors[j])
                {
                    color = j;
                    break;
                }
            }
            colors[i] = color;
        }

        return colors.Max() + 1;
    }
}