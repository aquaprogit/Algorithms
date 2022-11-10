using Labyrinth;

Maze maze = Maze.LoadFromFile(@"C:\Users\vladd\source\repos\KPI\Algorithms\Labyrinth\LabyrinthMaker\bin\Debug\net6.0-windows\result.json");

Console.WindowHeight = maze.Cells.GetLength(0) + 6;
Console.WindowWidth = 45;

Console.WriteLine(maze.ToString());

State state = new State(maze, null);

var res = RBFS.Solve(state);
//if (res.State == null)
//    Console.WriteLine("There is no way");
//else
//{
//    foreach (var st in res.State.GetPath())
//    {
//        Console.Write(st.Maze.Selected.Coordinate);
//        Console.Write(", ");
//    }
//}
Console.ReadLine();
