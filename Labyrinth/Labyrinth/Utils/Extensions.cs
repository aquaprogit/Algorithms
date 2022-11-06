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
}
