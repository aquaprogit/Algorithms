using System.Diagnostics;

using Labyrinth.Model;
using Labyrinth.PathSolver;
using Labyrinth.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        //for (int i = 1; i <= 20; i++)
        //{
        string path = Directory.GetCurrentDirectory().Replace(@"Labyrinth\bin", @"LabyrinthMaker\bin") + "-windows\\" + "result.json";

        Maze maze = Maze.LoadFromFile(path);

        State state = new State(maze, null);
        IPathSolver solver = new RBFS();
        Stopwatch sw = Stopwatch.StartNew();
        SearchResult res = solver.Solve(state, false);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        if (res.State == null)
        {
            Console.WriteLine("There is no way");
        }
        else
        {
            IEnumerable<State> solutionPath = res.State.GetPath().AsEnumerable();
            //Console.WriteLine(solutionPath.First(st => st.Distance == 1).Maze);
            Console.WriteLine("Dead ends: " + state.GetTotalImpasses());
            Console.WriteLine("Total nodes: " + state.GetTotalNodes().Count);
            Console.WriteLine("Stored states: " + res.Path.ToString());
            Console.WriteLine(string.Join(" -> ", solutionPath.Reverse().Select(part => part.Maze.Selected.Coordinate.ToString())));
        }
    }
    private static void ResizeWindow(int height, int width)
    {
        Console.WindowHeight = height + 6;
        Console.WindowWidth = width;
    }
}