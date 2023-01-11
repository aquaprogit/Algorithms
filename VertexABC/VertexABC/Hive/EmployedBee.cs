namespace VertexABC.Hive;

class EmployedBee : Bee
{
    private readonly int _lowerBound;
    private readonly int _upperBound;

    public EmployedBee(int lowerBound, int upperBound) : base()
    {
        _lowerBound = lowerBound;
        _upperBound = upperBound;
    }

    public override int[] GenerateSolution(int[][] graph, int[]? currentSolution)
    {
        if (currentSolution == null)
            throw new ArgumentNullException(nameof(currentSolution));
        int[] newSolution = (int[])currentSolution.Clone();
        int randomIndex = _rand.Next(graph.Length);
        int newValue = _rand.Next(_upperBound - _lowerBound) + _lowerBound;
        newSolution[randomIndex] = newValue;
        return newSolution;
    }
}