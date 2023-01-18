using VertexABC;

namespace VertexABC;

public class Graph : ICloneable
{
    public List<Vertex> Vertices { get; init; } = new();
    public int ChromaticNumber => Vertices.Select(v => v.ColorValue).Distinct().Count();
    public bool IsValid => Vertices.All(v => v.IsValid);

    public Vertex AddVertex(Vertex vertex)
    {
        if (Vertices.Any(v => v.Id == vertex.Id))
            throw new InvalidOperationException(nameof(vertex));

        Vertices.Add(vertex);
        return Vertices.Find(v => v.Equals(vertex))!;
    }

    public void AddEdge(Vertex firstVertex, Vertex secondVertex)
    {
        Vertex first = Vertices.First(vertex => vertex.Id == firstVertex.Id);
        Vertex second = Vertices.First(vertex => vertex.Id == secondVertex.Id);

        if (first == null || second == null)
            throw new ArgumentOutOfRangeException(nameof(firstVertex), "One of passed parameters out of bound");

        first.LinkTo(second);
    }

    public object Clone()
    {
        int[][] adjacencyArray = new int[Vertices.Count][];
        int[] colors = Vertices.Select(vertex => vertex.ColorValue).ToArray();
        foreach (Vertex vertex in Vertices)
        {
            adjacencyArray[vertex.Id] = new int[vertex.Degree];
            int i = 0;
            foreach (Vertex linked in vertex.Neighbors)
            {
                adjacencyArray[vertex.Id][i++] = linked.Id;
            }
        }

        Graph graph = FromJaggedArray(adjacencyArray);

        graph.Vertices.ForEach(vertex => vertex.ColorValue = colors[graph.Vertices.IndexOf(vertex)]);

        return graph;
    }

    public override string ToString()
    {
        return string.Join("\n", Vertices);
    }

    public static Graph GenerateGraph(int numVertices, int maxEdges)
    {
        Random random = new Random();
        Graph graph = new Graph();

        for (int i = 0; i < numVertices; i++)
        {
            graph.AddVertex(new Vertex(i));
        }

        List<Vertex> orderedRandomly = graph.Vertices.OrderBy(_ => random.Next(numVertices)).ToList();
        for (int vertexIndex = 0; vertexIndex < orderedRandomly.Count; vertexIndex++)
        {
            Vertex first = orderedRandomly[vertexIndex];

            int nextIndex = vertexIndex != orderedRandomly.Count - 1 ? vertexIndex + 1 : 0;
            Vertex second = orderedRandomly[nextIndex];

            graph.AddEdge(first, second);
        }

        while (graph.Vertices.All(vertex => vertex.Degree != maxEdges))
        {
            orderedRandomly = graph.Vertices.OrderBy(_ => random.Next(numVertices)).ToList();
            Vertex first = orderedRandomly.First();
            Vertex second = orderedRandomly.Last();

            graph.AddEdge(first, second);
        }
        return graph;
    }

    public static Graph FromJaggedArray(int[][] array)
    {
        Graph graph = new Graph();

        for (int i = 0; i < array.Length; i++)
        {
            graph.AddVertex(new Vertex(i));
        }

        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                graph.Vertices[i].LinkTo(graph.Vertices[array[i][j]]);
            }
        }

        return graph;
    }
}