using Labyrinth;
using Labyrinth.Enums;

#region Walls
//const int height = 5;
//const int width = 20;
//Maze maze = new Maze(height, width);
//maze[0, 2].CellState = CellType.Wall;
//maze[1, 2].CellState = CellType.Wall;
//maze[2, 2].CellState = CellType.Wall;
//maze[2, 1].CellState = CellType.Wall;
//maze[2, 4].CellState = CellType.Wall;
//maze[2, 5].CellState = CellType.Wall;
//maze[2, 6].CellState = CellType.Wall;
//maze[2, 7].CellState = CellType.Wall;
//maze[2, 8].CellState = CellType.Wall;
//maze[2, 9].CellState = CellType.Wall;
//maze[2, 10].CellState = CellType.Wall;
//maze[2, 11].CellState = CellType.Wall;
//maze[2, 12].CellState = CellType.Wall;
//maze[2, 13].CellState = CellType.Wall;
//maze[2, 13].CellState = CellType.Wall;
//maze[3, 13].CellState = CellType.Wall;
//maze[4, 13].CellState = CellType.Wall;
#endregion

Maze maze = Maze.LoadFromFile(@"C:\Users\vladd\source\repos\wpf\wpf\bin\Debug\net6.0-windows\result.json");

Console.WindowHeight = 26;
Console.WindowWidth  = 48;

Console.WriteLine(maze.ToString());

State state = new State(maze, null);

var res = RBFS.Solve(state, true);
if (res == null)
    Console.WriteLine("There is no way");
else
{
    Console.WriteLine(res.Last().Maze);
    foreach (var st in res)
    {
        Console.Write(st.Maze.Selected.Coordinate);
        Console.Write(", ");
    }
}
Console.ReadLine();
