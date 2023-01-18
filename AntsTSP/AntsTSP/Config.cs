using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntsTSP;
internal static class Config
{
    public static int VerticesCount { get; private set; } = 100;
    public static int Alpha { get; private set; } = 3;
    public static int Beta { get; private set; } = 2;
    public static double Ro { get; private set; } = 0.7;
    public static int AntAmount { get; private set; } = 45;
    public static int EliteAntAmount { get; private set; } = 10;
    public static int IterFeromonAdd { get; private set; } = VerticesCount;
    public static readonly Random Random = new Random();
    public static (int Min, int Max) WeightRange { get; set; } = (1, 40);
    public static int? Lmin;

    public static void LminInit(int[,] weights) //needed refactoring like in FindTransitProbability
    {
        if (Lmin == null)
        {
            List<int> visited = new List<int>(VerticesCount) { 0 };
            for (int i = 0; i < VerticesCount - 1; i++)
            {
                int lowestWeightInd = visited.Contains((visited.Last() + 1) % weights.GetLength(1)) ?
                    (visited.Last() + 2) % weights.GetLength(1) :
                    (visited.Last() + 1) % weights.GetLength(1);

                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    if (j != visited.Last() && !visited.Contains(j) &&
                        weights[visited.Last(), j] < weights[visited.Last(), lowestWeightInd])
                    {
                        lowestWeightInd = j;
                    }
                }
                visited.Add(lowestWeightInd);
            }
            visited.Add(0);

            Lmin = TSPAlgorithm.GetCycleLength(visited, weights);
            TSPAlgorithm.PrintCycle(visited, weights);
        }
        else
        {
            throw new Exception();
        }
    }
}