namespace VertexABC;

public class Vertex : IEquatable<Vertex>
{
    private static int _idMax = 0;
    public int Id { get; set; }
    public int ColorValue { get; private set; }
    public HashSet<Vertex> Neighbors { get; init; }

    public bool IsValid => Neighbors.Any(v => v.ColorValue == ColorValue) == false;

    public Vertex(int colorValue = -1)
    {
        ColorValue = colorValue;
        Neighbors = new HashSet<Vertex>();
        Id = _idMax++;
    }

    public void AddNeighbor(Vertex neighbor)
    {
        Neighbors.Add(neighbor);
        neighbor.Neighbors.Add(this);
    }

    public override string ToString()
    {
        return $"{Id} for {ColorValue}";
    }
    public bool Equals(Vertex? other)
    {
        return other?.Id == Id;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as Vertex);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

