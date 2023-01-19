using System.Data;

namespace AntsTSP;
internal static class Extensions
{
    public static void Print<T>(this List<T> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            if (i != arr.Count - 1)
            {
                Console.Write(arr[i] + " -> ");
                if (Console.CursorLeft > Console.WindowWidth - 10)
                    Console.WriteLine();
            }
            else
            {
                Console.WriteLine(arr[i]);
            }
        }
    }

    public static void Print<T>(this T[,] values)
    {
        for (int i = 0; i < values.GetLength(0); i++)
        {
            for (int j = 0; j < values.GetLength(1); j++)
            {
                Console.Write(Math.Round(Convert.ToDouble(values[i, j]), 3).ToString().PadLeft(5) + " ");
            }
            Console.WriteLine();
        }
    }
    public static List<List<T>> ToListOfLists<T>(this T[,] array)
    {
        List<List<T>> listOfLists = new List<List<T>>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            List<T> innerList = new List<T>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                innerList.Add(array[i, j]);
            }
            listOfLists.Add(innerList);
        }
        return listOfLists;
    }
    public static T[,] ToArray2D<T>(this List<List<T>> values)
    {
        T[,] array = new T[values.Count, values[0].Count];
        for (int i = 0; i < values.Count; i++)
        {
            for (int j = 0; j < values[i].Count; j++)
            {
                array[i, j] = values[i][j];
            }
        }
        return array;
    }
    public static void Print(this SolutionInfo self)
    {
        Console.WriteLine("Solving results:");
        Console.WriteLine($"""
            ==================================
            Iterations:         {self.Iterations},
            Taken time:         {self.Seconds}s,
            Common ants count:  {self.CommonAnts},
            Elite ants count:   {self.EliteAnts},
            Alfa value:         {self.Alfa},
            Beta value:         {self.Beta},
            Ro value:           {self.Ro}
            Cycle length:       {self.CycleLength}
            ===================================
            """);
    }
}