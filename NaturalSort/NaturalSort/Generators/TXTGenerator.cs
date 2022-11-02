using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSort.Generators;
internal class TxtGenerator
{
    private readonly Random _random = new Random();

    public void GenerateSize(int mb, string fileName)
    {
        using (var writer = new StreamWriter(fileName))
        {
            for (int i = 1; i % 10_000 != 0 || !(new FileInfo(fileName).Length >= Math.Pow(2, 20) * mb); i++)
            {
                writer.WriteLine(_random.Next(0, 100_000_000));
            }
        }
        Console.WriteLine($"Generated {fileName} with size of {mb} MB.");
    }

    public void GenerateLines(int linesCount, string fileName)
    {
        using var writer = new StreamWriter(fileName);
        for (int i = 0; i < linesCount; i++)
        {
            writer.WriteLine(_random.Next(0, 100));
        }

        Console.WriteLine($"Generated {fileName} with {linesCount} lines.");
    }
}
