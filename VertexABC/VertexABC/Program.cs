using VertexABC;

int vertices = 250;
int maxDegree = 25;
int totalBees = 35;
int scouts = 3;
int onlookers = totalBees - scouts;

Graph graph = Graph.GenerateGraph(vertices, maxDegree);
ABC abc = new ABC(graph, scouts, onlookers);
Graph result = abc.Solve(printIterations: true);
Console.WriteLine("==================Result==================");
Console.WriteLine(result.IsValid ? $"ChromaticNumber: {result.ChromaticNumber}" : "Solution is incorrect");

Console.ReadLine();