using Labyrinth.Model;
using Labyrinth.PathSolver;
using Labyrinth.Utils;

internal class Program
{
    private static readonly string _fileName = "result.json";
    private static void Main(string[] args)
    {
        string path = Directory.GetCurrentDirectory().Replace(@"Labyrinth\bin", @"LabyrinthMaker\bin") + "-windows\\" + _fileName;

        Maze maze = Maze.LoadFromFile(path);

        ResizeWindow(maze.Cells.GetLength(0), maze.Cells.GetLength(1) * 4 + 5);

        State state = new State(maze, null);
        Console.WriteLine(state.Maze.ToString());
        IPathSolver solver = new RBFS();
        SearchResult res = solver.Solve(state, true);
        Console.WriteLine("Stored states: " + res.Path.ToString());
        Console.WriteLine("Total nodes: " + state.GetTotalNodes());
        Console.WriteLine(state.Maze.ToString());
        if (res.State == null)
        {
            Console.WriteLine("There is no way");
        }
        else
        {
            IEnumerable<State> solutionPath = res.State.GetPath().AsEnumerable();
            Console.WriteLine(solutionPath.First(st => st.Distance == 1).Maze);
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