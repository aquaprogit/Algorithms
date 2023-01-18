namespace VertexABC.Hive;

public class ScoutBee
{
    public ScoutBee(Graph graph)
    {
        Graph = graph;
        AlreadySelected = new HashSet<Vertex>(graph.Vertices.Count);
        SelectedVertexId = -1;
    }

    public Graph Graph { get; }
    public int SelectedVertexId { get; set; }
    public HashSet<Vertex> AlreadySelected { get; }

    public Vertex SelectVertex()
    {
        Vertex vertex = Graph.Vertices
            .Where(v => !AlreadySelected.Contains(v))
            .MaxBy(v => v.Degree)!;

        SelectedVertexId = vertex.Id;
        AlreadySelected.Add(vertex);
        return vertex;
    }

    public double GetVertexValue(Vertex vertex, IEnumerable<Vertex> selectedVertices, int onlookersCount)
    {
        int degreeSum = selectedVertices.Sum(v => v.Degree);
        return onlookersCount * ((double)vertex.Degree / degreeSum);
    }

    public static IEnumerable<Vertex> SelectBestVerticesFromGraph(Graph graph, int count)
    {
        return graph.Vertices
            .OrderByDescending(v => v.Degree)
            .Take(count);
    }
}