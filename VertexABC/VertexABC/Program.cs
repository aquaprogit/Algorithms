using VertexABC;

namespace VertexABC;
class Program
{
    static void Main(string[] args)
    {
        // Generate a random graph with 50 vertices and a maximum of 25 edges per vertex
        Graph graph = Graph.GenerateGraph(50, 25);

        // Create an instance of the ABC class with the graph and the number of colors to use
        ABC abc = new ABC(graph, 5, 35, 5, 5, 10000);

        // Solve the graph coloring problem
        abc.Solve();

        int[][] solution = abc.BestSolution;

        // Print the solution
        Console.WriteLine("Solution:");
        foreach (int[] vertex in solution)
        {
            Console.WriteLine($"Vertex {vertex[0]}: Color {vertex[1]}");
        }

        Console.ReadKey();
    }
    private static bool IsCorrect(Graph graph, int[] solution)
    {
        return graph.Vertices.All(v => v.IsValid);
    }
}

