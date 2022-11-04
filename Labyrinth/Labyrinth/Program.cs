using Labyrinth;
using Labyrinth.Enums;

const int height = 5;
const int width = 20;
Maze maze = new Maze(height, width);
State state = new State(maze, null);

do
{
    Console.WriteLine(state.ToString());
    Console.WriteLine(state.Maze.ToString());
    int index = int.Parse(Console.ReadLine()!);
    state = state.GetChildren()[index];
} while (true);

Console.ReadLine();
