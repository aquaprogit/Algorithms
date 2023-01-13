using System.Diagnostics.CodeAnalysis;

namespace VertexABC;

class Solution : IEqualityComparer<Solution>
{
    public int[] ColorSet { get; set; }
    public static int[][] Graph { get; set; }
    public static int TotalEdges { get; set; }

    public Solution(int[] solution)
    {
        ColorSet = solution;
    }
    public double Fitness
    {
        get {
            int violations = 0;
            for (int i = 0; i < Graph.Length; i++)
            {
                for (int j = 0; j < Graph[i].Length; j++)
                {
                    if (ColorSet[i] == ColorSet[Graph[i][j]])
                        violations++;
                }
            }
            return (double)violations / TotalEdges;
        }
    }
    public override string ToString()
    {
        string result = string.Empty;
        for (int i = 0; i < ColorSet.Length; i++)
        {
            result += $"Vertice #{i} has color: {ColorSet[i]}\n";
        }
        return result;
    }
    public bool Equals(Solution? x, Solution? y)
    {
        if (x == y)
            return true;
        
        if (x == null ^ y == null)
            return false;
        
        return x!.ColorSet.Equals(y!.ColorSet);
    }

    public int GetHashCode([DisallowNull] Solution obj)
    {
        return ColorSet.GetHashCode();
    }
}
