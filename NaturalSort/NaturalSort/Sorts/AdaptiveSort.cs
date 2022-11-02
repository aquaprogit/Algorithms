namespace NaturalSort.Sorts;
internal class AdaptiveSort
{
    public void Sort(string sourceFile)
    {
        RecSort(sourceFile);
    }

    private int count = 1;
    private void RecSort(string sourceFile)
    {
        Console.WriteLine("Stage " + count++);
        SplitBySeries(sourceFile, "fileB.txt", "fileC.txt");
        bool isFinal = MergeParts("fileB.txt", "fileC.txt", sourceFile);
        if (isFinal)
        {
            Console.WriteLine("Sorted");
        }
        else
        {
            RecSort(sourceFile);
        }
    }

    public bool MergeParts(string fileB, string fileC, string sourceFile)
    {
        using var bReader = new StreamReader(fileB);
        using var cReader = new StreamReader(fileC);
        using var sourceWriter = new StreamWriter(sourceFile);

        int topFromB = ReadInt(bReader);
        int topFromC = ReadInt(cReader);

        bool canMoveB = true;
        bool canMoveC = true;

        int bFileGroupsCount = 0;
        int cFileGroupsCount = 0;

        int prevB = 0, prevC = 0;

        while (topFromB != int.MaxValue || topFromC != int.MaxValue)
        {
            if (canMoveB && (topFromB < topFromC || canMoveC == false))
            {
                sourceWriter.WriteLine(topFromB);
                prevB = topFromB;
                topFromB = ReadInt(bReader);

                canMoveB = prevB <= topFromB && topFromB != int.MaxValue;
                if (canMoveB == false)
                {
                    bFileGroupsCount++;
                }
            }
            else if (canMoveC && (topFromC <= topFromB || canMoveB == false))
            {
                sourceWriter.WriteLine(topFromC);
                prevC = topFromC;
                topFromC = ReadInt(cReader);

                canMoveC = prevC <= topFromC && topFromC != int.MaxValue;
                if (canMoveC == false)
                {
                    cFileGroupsCount++;
                }
            }

            if (canMoveB == canMoveC)
            {
                canMoveC = canMoveB = true;
                if (topFromB == int.MaxValue)
                {
                    canMoveB = false;
                }

                if (topFromC == int.MaxValue)
                {
                    canMoveC = false;
                }
            }
        }

        Console.WriteLine(bFileGroupsCount + " : " + cFileGroupsCount);
        return bFileGroupsCount == 1 && cFileGroupsCount == 1;
    }
    private void SplitBySeries(string sourceFile, string fileB, string fileC)
    {
        Console.Write("File A: ");
        Console.WriteLine(string.Join("; " , File.ReadAllLines(sourceFile).ToArray()));

        using var sourceReader = new StreamReader(sourceFile);
        using var bWriter = new StreamWriter(fileB);
        using var cWriter = new StreamWriter(fileC);

        int prev = int.MaxValue;
        bool isOddGroup = true;
        while (sourceReader.Peek() >= 0)
        {
            int current = ReadInt(sourceReader);
            if (current == int.MaxValue)
                break;

            if (current < prev)
            {
                isOddGroup = !isOddGroup;
            }

            if (isOddGroup)
            {
                bWriter.WriteLine(current);
            }
            else
            {
                cWriter.WriteLine(current);
            }

            prev = current;
        }
        bWriter.Close();
        cWriter.Close();
        Console.Write("File B: ");
        Console.WriteLine(string.Join("; " , File.ReadAllLines(fileB).ToArray()));
        Console.Write("File C : ");
        Console.WriteLine(string.Join("; " , File.ReadAllLines(fileC).ToArray()));
    }

    private int ReadInt(StreamReader reader)
    {
        return int.Parse(reader.ReadLine() ?? $"{int.MaxValue}");
    }
}