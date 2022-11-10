using Labyrinth.Utils;

namespace Labyrinth;
internal static class RBFS
{
    private static int _iteration = 0;
    public static SearchResult Solve(State state)
    {
        return SolveRec(state, int.MaxValue);
    }
    public static SearchResult SolveRec(State state, int limit)
    {
        if (state.Distance == 1)
            return new SearchResult(state, state.Generation);

        List<State> children = state.GetChildren();

        if (children.Count == 0)
            return new SearchResult(null, int.MaxValue);

        while (true)
        {
            state.PrintState(++_iteration);
            (int bestIndex, int secondBestIndex) = GetTwoBest(children);
            if (children[bestIndex].Evaluation > limit)
                return new SearchResult(null, children[bestIndex].Evaluation);

            SearchResult solution = SolveRec(children[bestIndex], Math.Min(limit, children[secondBestIndex].Evaluation));

            if (solution.State is not null)
            {
                return solution;
            }
            children[bestIndex].Evaluation = solution.Path;
        }

        static (int best, int second) GetTwoBest(List<State> children)
        {
            //  List<State> result = children.OrderBy(x => x.Evaluation).Take(2).ToList();
            int best = int.MaxValue;
            int alt = int.MaxValue;
            int bestIndex = 0;
            int altIndex = 0;
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Evaluation < best)
                {
                    alt = best;
                    altIndex = bestIndex;
                    best = children[i].Evaluation;
                    bestIndex = i;
                }
                else if (children[i].Evaluation < alt)
                {
                    alt = children[i].Evaluation;
                    altIndex = i;
                }
            }

            return (bestIndex, altIndex);
        }
    }

}
internal record SearchResult(State? State, int Path);
