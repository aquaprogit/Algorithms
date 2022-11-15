using Labyrinth.Enums;

namespace LabyrinthMaker;

internal struct CompressedCell
{
    public (int X, int Y) Coordinate;
    public CellType State;

    public CompressedCell((int, int) coordinate, CellType state)
    {
        Coordinate = coordinate;
        State = state;
    }
}
