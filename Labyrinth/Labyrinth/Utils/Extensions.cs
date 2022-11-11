using System.Text;
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
}
