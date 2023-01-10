using System.Security.Cryptography;

namespace VertexABC.Hive;

public class OnlookerBee : Bee
{
    private double[][] Probabilities { get; set; }

    public OnlookerBee(Graph graph, double[][] probabilities) : base(graph)
    {
        Probabilities = probabilities;
    }

    public override void Search()
    {
        // Select a random vertex and color based on the probabilities
        int vertexIndex = SelectNeighbor(Probabilities);
        int color = _random.Next(1, Graph.Vertices.Count);

        // Create a new solution by modifying the color of the selected vertex
        int[][] newSolution = Solution.Select(x => x.ToArray()).ToArray();
        newSolution[vertexIndex][1] = color;

        // Update the solution if the new solution is better
        if (Fitness(newSolution) > Fitness(Solution))
            Solution = newSolution;
    }
}
