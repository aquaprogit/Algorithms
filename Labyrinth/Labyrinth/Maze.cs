using System.Text;

using Labyrinth.Enums;

namespace Labyrinth;
internal class Maze
{
    private (int x, int y) _selectedCoord;
    private (int x, int y) _sourceCoord;
    private (int x, int y) _destinationCoord;

    public Cell[,] Cells { get; init; }

    public Cell Selected
    {
        get => this[_selectedCoord.x, _selectedCoord.y] ?? throw new ArgumentOutOfRangeException(nameof(_selectedCoord));
        set => this[_selectedCoord.x, _selectedCoord.y] = value;
    }
    public Cell Source
    {
        get => this[_sourceCoord.x, _sourceCoord.y] ?? throw new ArgumentOutOfRangeException(nameof(_sourceCoord));
        set => this[_sourceCoord.x, _sourceCoord.y] = value;
    }
    public Cell Destination
    {
        get => this[_destinationCoord.x, _destinationCoord.y] ?? throw new ArgumentOutOfRangeException(nameof(_destinationCoord));
        set => this[_destinationCoord.x, _destinationCoord.y] = value;
    }

    public Maze(Cell[,] cells, (int x, int y) source, (int x, int y) destination)
    {
        Cells = cells ?? throw new ArgumentNullException(nameof(cells));
        _sourceCoord = source;
        _destinationCoord = destination;
    }

    public Maze(int height, int width)
    {
        Cells = new Cell[height, width];
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                Cells[i, j] = new Cell(CellType.Empty, (i, j));

        Cells[0, 0] = new Cell(CellType.Source, (0, 0));
        _sourceCoord = (0, 0);

        Cells[height - 1, width - 1] = new Cell(CellType.Destination, (height - 1, width - 1));
        _destinationCoord = (height - 1, width - 1);
    }
    private Maze() { }

    public void MoveSelection(Direction direction)
    {
        int horizontalOffset = 0, verticalOffset = 0;
        switch (direction)
        {
            case Direction.Up:
                verticalOffset = -1;
                break;
            case Direction.Down:
                verticalOffset = 1;
                break;
            case Direction.Left:
                horizontalOffset = -1;
                break;
            case Direction.Right:
                horizontalOffset = 1;
                break;
        }
        (int x, int y) currCoord = (_selectedCoord.x + verticalOffset, _selectedCoord.y + horizontalOffset);
        int width = Cells.GetLength(0);
        int height = Cells.GetLength(1);

        if (currCoord.x < 0 || currCoord.x >= width)
            return;
        if (currCoord.y < 0 || currCoord.y >= height)
            return;

        if (Cells[currCoord.x, currCoord.y].CellState is not CellType.Empty)
            return;

        if (Selected.CellState != CellType.Source)
        {
            Selected.CellState = CellType.Visited;
        }
        _selectedCoord = currCoord;
        Selected.CellState = CellType.Selected;

    }
    public Maze Clone()
    {
        int rows = Cells.GetLength(0);
        int cols = Cells.GetLength(1);
        Cell[,] cells = new Cell[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                cells[i, j] = Cells[i, j].Clone();

        return new Maze() {
            Cells = cells,
            _selectedCoord = this._selectedCoord,
            _destinationCoord = this._destinationCoord,
            _sourceCoord = this._sourceCoord
        };
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            sb.Append('\n');
            for (int column = 0; column < Cells.GetLength(1); column++)
            {
                string value = Cells[row, column].CellState switch {
                    CellType.Empty => " ",
                    CellType.Source => "s",
                    CellType.Destination => "d",
                    CellType.Visited => "*",
                    CellType.Selected => "@",
                    CellType.Wall => "#",
                    _ => ""
                };
                sb.Append(value + " ");
            }
        }
        return sb.ToString();
    }
    public Cell? this[int x, int y]
    {
        get {
            return Enumerable.Range(0, Cells.GetLength(0)).Contains(x)
                && Enumerable.Range(0, Cells.GetLength(1)).Contains(y)
                ? Cells[x, y]
                : null;
        }

        set => Cells[x, y] = value ?? throw new ArgumentNullException(nameof(value));
    }
}
