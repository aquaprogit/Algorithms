using VertexABC;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = Graph.GenerateGraph(50, 25);

        
        //int[][] graph = GenerateGraph(50, 25);
        //Console.WriteLine("Graph generated...");
        //// Declare and initialize the number of colors
        //int numColors = 100;

        //// Create an instance of the ABC class
        //ABC abc = new ABC(graph, numColors);

        //// Run the ABC algorithm
        //int[] bestSolution = abc.Solve();

        //// Print the best solution
        //Console.WriteLine("Best solution: " + string.Join(", ", bestSolution));
        //Console.WriteLine("Used colors: " + bestSolution.ToList().Distinct().Count());
        //Console.WriteLine(IsCorrect(graph, bestSolution) ? "Correct" : "Incorrect");

        Console.ReadLine();

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