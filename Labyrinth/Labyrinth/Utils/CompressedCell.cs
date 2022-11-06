using Labyrinth.Enums;

namespace Labyrinth.Utils;
internal struct CompressedCell
{
    public (int, int) Coordinate;
    public CellType State;

    public CompressedCell((int, int) coordinate, CellType state)
    {
        Coordinate = coordinate;
        State = state;
    }
}
