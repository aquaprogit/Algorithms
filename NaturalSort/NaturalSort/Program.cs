using NaturalSort.Generators;
using NaturalSort.Sorts;

using System.Diagnostics;

new TxtGenerator().GenerateSize(10, "file.txt");
File.Delete("baseUnsorted.txt");
File.Copy("file.txt", "baseUnsorted.txt");
Stopwatch sw = Stopwatch.StartNew();
new AdaptiveSort().Sort("file.txt");
sw.Stop();
Console.WriteLine(sw.Elapsed.TotalSeconds);
Console.ReadLine();