namespace Labyrinth;
internal class StateComparer : IComparer<State>
{
    public int Compare(State? x, State? y)
    {
        if (x == null && y == null)
            return 0;

        if (x == null ^ y == null)
            throw new ArgumentNullException(nameof(x), "One of the arguments is null");

        bool evalutionEqual = x!.Evaluation.CompareTo(y!.Evaluation) == 0;
        bool generationEqual = x!.Generation.CompareTo(y!.Generation) == 0;

        if (evalutionEqual && generationEqual)
            return x!.Distance.CompareTo(y!.Distance);
        if (evalutionEqual && generationEqual == false)
            return x!.Generation.CompareTo(y!.Generation) * -1;

        return x!.Evaluation.CompareTo(y!.Evaluation);

    }
}
