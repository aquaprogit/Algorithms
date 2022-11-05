using Labyrinth.Enums;

namespace Labyrinth;
internal class Cell
{
    public (int X, int Y) Coordinate { get; private set; }
    public CellType CellState { get; set; }

    public Cell(CellType type, (int Y, int X) coordinate)
    {
        CellState = type;
        Coordinate = coordinate;
    }

    public Cell Offset(int xOffset = 0, int yOffset = 0)
    {
        Cell cell = Clone();
        cell.Coordinate = (cell.Coordinate.X + xOffset, cell.Coordinate.Y + yOffset);
        return cell;
    }
    public static double DistanceBetween(Cell source, Cell destination)
    {
        double squareDistance = Math.Pow(destination.Coordinate.X - source.Coordinate.X, 2)
                                + Math.Pow(destination.Coordinate.Y - source.Coordinate.Y, 2);

        return Math.Ceiling(Math.Sqrt(squareDistance));
    }
    public static double DistanceBetween((int x, int y) source, (int x, int y) destination)
    {
        double squareDistance = Math.Pow(destination.x - source.x, 2)
                                + Math.Pow(destination.y - source.y, 2);

        return Math.Round(Math.Sqrt(squareDistance), 2);
    }
    public override string ToString()
    {
        return $"{CellState} at {Coordinate}";
    }
    public Cell Clone()
    {
        return new Cell(CellState, Coordinate);
    }
}
