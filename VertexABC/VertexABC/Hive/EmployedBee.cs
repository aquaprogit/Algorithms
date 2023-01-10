using System.Security.Cryptography;

namespace VertexABC.Hive;

public class EmployedBee : Bee
{
    public EmployedBee(Graph graph) : base(graph)
    {

    }

    public override void Search()
    {
        // Select a random vertex and color
        int vertexIndex = _random.Next(0, Graph.Vertices.Count - 1);
        int color = _random.Next(1, Graph.Vertices.Count);

        // Create a new solution by modifying the color of the selected vertex
        int[][] newSolution = Solution.Select(x => x.ToArray()).ToArray();
        newSolution[vertexIndex][1] = color;

        // Update the solution if the new solution is better
        if (Fitness(newSolution) > Fitness(Solution))
            Solution = newSolution;
    }
}

