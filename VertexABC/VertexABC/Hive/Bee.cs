using System.Security.Cryptography;

namespace VertexABC.Hive;

public abstract class Bee
{
    protected Graph Graph { get; set; }
    public int[][] Solution { get; set; }
    protected readonly Random _random;

    protected Bee(Graph graph)
    {
        Graph = graph;
        _random = new Random();
    }

    public abstract void Search();
    public double Fitness()
    {
        return Fitness(Solution);
    }
    protected static double Fitness(int[][] solution)
    {
        // Calculate the number of conflicts in the solution
        int conflicts = 0;
        for (int i = 0; i < solution.Length; i++)
        {
            for (int j = 0; j < solution[i].Length; j++)
            {
                if (solution[i][j] == solution[j][i])
                    conflicts++;
            }
        }

        // Return the inverse of the number of conflicts (larger solutions will have a lower fitness)
        return 1.0D / conflicts;
    }
    protected int SelectNeighbor(double[][] probabilities)
    {
        // Select a random number between 0 and 1
        double rand = _random.NextDouble();

        // Find the index of the probability that the random number falls into
        int index = -1;
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (rand < probabilities[i][0])
            {
                index = i;
                break;
            }
        }

        return index;
    }

}