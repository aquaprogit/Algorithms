using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
