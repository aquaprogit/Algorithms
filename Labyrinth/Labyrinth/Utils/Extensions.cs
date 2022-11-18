using System.ComponentModel.Design;
using System.Text;

using Labyrinth.Enums;
using Labyrinth.Model;

namespace Labyrinth.Utils;
internal static class Extensions
{
    public static Cell ToNormalCell(this CompressedCell self)
    {
        return new Cell(self.State, (self.Coordinate.Item2, self.Coordinate.Item1));
    }
    public static void PrintState(this State self, int? iteration = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"================={(iteration?.ToString()) ?? "="}=====================");
        sb.AppendLine(self.ToString());
        sb.AppendLine(self.Maze.ToString());
        sb.AppendLine($"================={(iteration?.ToString()) ?? "="}=====================");
        Console.WriteLine(sb.ToString());
        Thread.Sleep(1);
    }
    public static List<T> ToList<T>(this T[,] self)
    {
        List<T> list = new List<T>();
        for (int rowIndex = 0; rowIndex < self.GetLength(0); rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < self.GetLength(1); columnIndex++)
            {
                list.Add(self[rowIndex, columnIndex]);
            }
        }

        return list;
    }
    public static string ToString<T>(this (T item1, T item2) self)
    {
        return $"[{self.item1}; {self.item2}]";
    }
    public static List<State> GetTotalNodes(this State state, List<State>? result = null)
    {
        result ??= new List<State>();
        result.Add(state);
        List<State> children = state.GetChildren();
        foreach (State child in children)
        {
            child.GetTotalNodes(result);
        }
        return result;
    }
    public static int GetTotalImpasses(this State state)
    {
        return state.Maze.Cells.ToList().Count(cell => cell.Coordinate.X != 0 && cell.Coordinate.Y != 0 && cell.Coordinate.X != state.Maze.Cells.GetLength(0) - 1 && cell.Coordinate.Y != state.Maze.Cells.GetLength(1) && state.Maze.Neighbors(cell.Coordinate).Count(n => n == null || n.Type == CellType.Wall) == 3);
    }
}
