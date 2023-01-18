using AntsTSP;

AntsSettings.VerticesCount = 100;
AntsSettings.CommonAnts = 35;
AntsSettings.EliteAnts = 10;
AntsSettings.Alfa = 3;
AntsSettings.Beta = 2;
AntsSettings.Ro = 0.7;

int[,] graph = Helper.BuildGraph(AntsSettings.VerticesCount, 1, 40);
TSPAlgorithm algorithm = new TSPAlgorithm(graph);
List<int> minCycle = algorithm.Solve();
Console.WriteLine("End of optimization");
Console.ReadLine();