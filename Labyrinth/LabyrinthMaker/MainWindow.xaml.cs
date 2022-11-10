using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

using Newtonsoft.Json;

namespace LabyrinthMaker;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int _size = 10;
    Brush _selectedBrush = Brushes.Black;
    public int Size
    {
        get => _size;
        set {
            _size = value;
            Grid grid = new Grid();
            for (int i = 0; i < Size; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Border border = new Border {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(0.5)
                    };

                    Grid cell = new Grid {
                        Background = Brushes.White
                    };

                    cell.MouseEnter += Cell_MouseEnter;
                    border.Child = cell;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    grid.Children.Add(border);
                }
            }
            grid.Height = 500;
            grid.Width = 500;
            Main_Grid.Children.Clear();
            Main_Grid.Children.Add(grid);
        }
    }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Size_IUP.Value = 10;
    }

    private void Cell_MouseEnter(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if (_selectedBrush == Brushes.Red || _selectedBrush == Brushes.Green)
            {
                List<Grid> cells = ((Grid)Main_Grid.Children[0]).Children.Cast<Border>().Select(b => b.Child).Cast<Grid>().ToList();
                Grid? lastPrinted = cells.FirstOrDefault(g => g.Background == _selectedBrush);
                if (lastPrinted != null)
                    lastPrinted.Background = Brushes.White;
            }
            ((Grid)sender).Background = _selectedBrush;
        }
    }

    private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _selectedBrush = ((Grid)sender).Background;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        CompressedCell[,] cells = new CompressedCell[Size, Size];
        for (int rowIndex = 0; rowIndex < Size; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < Size; columnIndex++)
            {
                Grid? curr = ((Grid)Main_Grid.Children[0]).Children.Cast<Border>().First(b => Grid.GetRow(b) == rowIndex && Grid.GetColumn(b) == columnIndex).Child as Grid;
                CellState state = CellState.Empty;
                if (curr == null)
                    continue;

                if (curr.Background == Brushes.Black)
                    state = CellState.Wall;
                else if (curr.Background == Brushes.Red)
                    state = CellState.Destination;
                else if (curr.Background == Brushes.Green)
                    state = CellState.Source;

                cells[rowIndex, columnIndex] = new CompressedCell((columnIndex, rowIndex), state);
            }
        }
        string result = JsonConvert.SerializeObject(cells, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("result.json", result);
        MessageBox.Show("Saved successfully");
    }
}
struct CompressedCell
{
    public (int, int) Coordinate;
    public CellState State;

    public CompressedCell((int, int) coordinate, CellState state)
    {
        Coordinate = coordinate;
        State = state;
    }
}
enum CellState
{
    Empty,
    Source,
    Destination,
    Visited,
    Selected,
    Wall
}
