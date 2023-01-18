using static AntsTSP.Ant;

namespace AntsTSP;
internal static class TSPAlgorithm
{
    public static List<int> AntColonyOptimization(int[,] weights)
    {
        double[,] visibility = new double[Config.VerticesCount, Config.VerticesCount];
        double[,] feromon = new double[Config.VerticesCount, Config.VerticesCount];

        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                if (i == j)
                    continue;
                visibility[i, j] = 1 / (double)weights[i, j];
                feromon[i, j] = Config.Random.Next(10, 40) / 100.0;
            }
        }
        List<int> bestCycle = new List<int>(Config.VerticesCount + 1);
        int previousLength = -1;
        int currentLength;
        int stableStrike = 0;
        int iteration = 0;
        do
        {
            List<Ant> ants = new List<Ant>(Config.AntAmount);
            for (int i = 0; i < Config.AntAmount; i++)
            {
                AntType type;
                type = i < Config.EliteAntAmount ? AntType.FeromoneElite : AntType.Ordinary;
                ants.Add(new Ant(type));

                //ants travel
                int plantingIndex = Config.Random.Next(0, Config.VerticesCount);
                ants[i].MoveToVertix(plantingIndex);
                while (ants[i].UnvisitedVertices.Count > 0)
                {
                    ants[i].MoveToNext(visibility, feromon);
                }
                ants[i].MoveToVertix(plantingIndex);

                if (bestCycle.Any() == false)
                    bestCycle = ants[i].Path;
                else if (GetCycleLength(ants[i].Path, weights) < GetCycleLength(bestCycle, weights))
                    bestCycle = ants[i].Path;
            }

            for (int i = 0; i < Config.VerticesCount; i++)
            {
                for (int j = 0; j < Config.VerticesCount; j++)
                {
                    feromon[i, j] -= Config.Ro * feromon[i, j]; //feromon evaporation
                }
            }
            for (int i = 0; i < Config.AntAmount; i++)
            {
                double feromonAmount = ants[i].GetFeromones(weights);
                ants[i].PlantFeromon(feromonAmount, feromon);
            }

            currentLength = GetCycleLength(bestCycle, weights);
            if (currentLength == previousLength)
            {
                stableStrike++;
            }
            else
            {
                Console.WriteLine($"Length changed to: {currentLength}");
                stableStrike = 0;
            }
            previousLength = currentLength;
        }
        while ( /*currentLength>Config.Lmin && stableStrike<150 */ iteration < 1000);
        PrintCycle(bestCycle, weights);

        return bestCycle;
    }
    public static int GetCycleLength(List<int> cycle, int[,] weights)
    {
        int length = 0;
        for (int i = 0; i < cycle.Count - 1; i++)
        {
            length += weights[cycle[i], cycle[i + 1]];
        }
        return length;
    }
    public static void PrintCycle(List<int> cycle, int[,] graph)
    {
        Console.WriteLine("The shortest Hamiltonian cycle contains vertices in order: ");
        cycle.Print();
        Console.WriteLine($"Lmin: {Config.Lmin}\tL: {GetCycleLength(cycle, graph)}");
    }
}