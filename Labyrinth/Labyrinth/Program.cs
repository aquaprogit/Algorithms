using Labyrinth;
using Labyrinth.Enums;

const int height = 5;
const int width = 20;
Maze maze = new Maze(height, width);
State state = new State(maze, null);

foreach (var st in RBFS.Solve(state))
{
    Console.Write(st.Maze.Selected.Coordinate);
    Console.Write(", ");
}

Console.ReadLine();
