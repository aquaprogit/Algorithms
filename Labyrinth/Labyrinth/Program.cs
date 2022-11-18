using System.Diagnostics;

using Labyrinth.Model;
using Labyrinth.PathSolver;
using Labyrinth.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = Directory.GetCurrentDirectory().Replace(@"Labyrinth\bin", @"LabyrinthMaker\bin") + "-windows\\" + "result.json";
        bool printSteps = true;

        Maze maze = Maze.LoadFromFile(path);

        ResizeWindow(maze.Cells.GetLength(0), maze.Cells.GetLength(1) * 2 + 4);
        Console.WriteLine(maze.ToString() + "\n");

        State state = new State(maze, null);
        Console.Write("Choose rbfs or ids solving method: ");
        IPathSolver solver = (Console.ReadLine()!.ToLower() == "rbfs") ? new RBFS() : new IDS();


        Stopwatch sw = Stopwatch.StartNew();
        SearchResult res = solver.Solve(state, printSteps);
        sw.Stop();
        Console.WriteLine("Algorithm took " + sw.ElapsedMilliseconds + "ms");

        if (res.State == null)
        {
            Console.WriteLine("There is no way");
        }
        else
        {
            IEnumerable<State> solutionPath = res.State.GetPath().AsEnumerable();
            Console.WriteLine(solutionPath.First(st => st.Distance == 1).Maze);

            Console.WriteLine("Dead ends: " + state.GetTotalImpasses());
            Console.WriteLine("Total nodes: " + state.GetTotalNodes().Count);
            Console.WriteLine("Stored states: " + res.Path.ToString());

            Console.WriteLine(string.Join(" -> ", solutionPath.Reverse().Select(part => part.Maze.Selected.Coordinate.ToString())));
        }
        Console.ReadLine();
    }
    private static void ResizeWindow(int height, int width)
    {
        Console.WindowHeight = height + 6;
        Console.WindowWidth = width;
    }
}