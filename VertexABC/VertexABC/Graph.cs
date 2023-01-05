namespace VertexABC;
public class Graph
{
    public List<Vertex> Vertices { get; init; }

    public Graph()
    {
        Vertices = new List<Vertex>();
    }
    public Vertex AddVertex(Vertex vertex)
    {
        Vertices.Add(vertex);
        return Vertices.Find(v => v.Equals(vertex))!;
    }
    public void AddEdge(Vertex v1, Vertex v2)
    {
        v1.AddNeighbor(v2);
    }
    public static Graph FromJaggedArray(int[][] array)
    {
        Graph graph = new Graph();

        for (int i = 0; i < array.Length; i++)
        {
            Vertex vertex = new Vertex(array[i][0]);
            graph.AddVertex(vertex);
            for (int j = 1; j < array[i].Length; j++)
            {
                Vertex neighbor = new Vertex(array[i][j]);
                graph.AddVertex(neighbor);
                graph.AddEdge(vertex, neighbor);
            }
        }

        return graph;
    }

    public static Graph GenerateGraph(int numVertices, int maxEdges)
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
        return FromJaggedArray(graph);
    }
}

