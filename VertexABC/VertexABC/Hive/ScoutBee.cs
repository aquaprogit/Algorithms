using System.Security.Cryptography;

namespace VertexABC.Hive;

public class ScoutBee : Bee
{
    private int Limit { get; set; }

    public ScoutBee(Graph graph, int limit) : base(graph)
    {
        Limit = limit;
    }

    public override void Search()
    {
        // Generate a new random solution
        Solution = Graph.Vertices.Select(x => new int[] { x.Id, _random.Next(1, Graph.Vertices.Count) }).ToArray();

        // Check if the solution has been evaluated more than the specified limit
        if (Limit > 0)
        {
            // Decrement the limit
            Limit--;
        }
        else
        {
            // Generate a new random solution
            Solution = Graph.Vertices.Select(x => new int[] { x.Id, _random.Next(1, Graph.Vertices.Count) }).ToArray();

            // Reset the limit
            Limit = _random.Next(0, Graph.Vertices.Count);
        }
    }
}

