using Labyrinth.Model;
using Labyrinth.Utils;

namespace Labyrinth.PathSolver;
internal class RBFS : IPathSolver
{
    private int _iteration = 0;
    public SearchResult Solve(State state, bool printSteps = false)
    {
        return SolveRec(state, int.MaxValue, printSteps);
    }

    private SearchResult SolveRec(State state, int limit, bool printSteps)
    {
        if (state.Distance == 1)
            return new SearchResult(state, state.Generation);

        List<State> children = state.GetChildren();

        if (children.Count == 0)
            return new SearchResult(null, int.MaxValue);

        while (true)
        {
            if (printSteps)
                state.PrintState(++_iteration);

            (int bestIndex, int secondBestIndex) = GetTwoBest(children);
            if (children[bestIndex].Evaluation > limit)
                return new SearchResult(null, children[bestIndex].Evaluation);

            SearchResult solution = SolveRec(children[bestIndex], Math.Min(limit, children[secondBestIndex].Evaluation), printSteps);

            if (solution.State is not null)
            {
                return solution;
            }
            children[bestIndex].Evaluation = solution.Path;
        }

        (int best, int second) GetTwoBest(List<State> children)
        {
            int best = int.MaxValue;
            int second = int.MaxValue;
            int bestIndex = 0;
            int secondIndex = 0;

            for (int index = 0; index < children.Count; index++)
            {
                if (children[index].Evaluation < best)
                {
                    second = best;
                    secondIndex = bestIndex;
                    best = children[index].Evaluation;
                    bestIndex = index;
                }
                else if (children[index].Evaluation < second)
                {
                    second = children[index].Evaluation;
                    secondIndex = index;
                }
            }

            return (bestIndex, secondIndex);
        }
    }

}
internal record SearchResult(State? State, int Path);
