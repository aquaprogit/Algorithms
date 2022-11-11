using Labyrinth.Model;
using Labyrinth.Utils;

namespace Labyrinth.PathSolver;
internal class IDS : IPathSolver
{
    private int _iteration = 0;
    public SearchResult Solve(State state, bool printSteps = false)
    {
        _iteration = 0;
        List<State> states = state.GetDepth();
        for (int depth = 0; depth < states.Count; depth++)
        {
            SearchResult searchResult = DFS(state, state, depth, printSteps);
            if (searchResult.State != null)
                return new SearchResult(state, state.Generation);
        }
        return new SearchResult(null, int.MaxValue);
    }

    private SearchResult DFS(State init, State current, int depth, bool printSteps)
    {
        if (current.Generation < depth)
        {
            if (printSteps)
                current.PrintState(++_iteration);

            if (current.Distance == 1)
                return new SearchResult(current, current.Generation);
            foreach (State child in current.GetChildren())
            {
                SearchResult result = DFS(init, child, depth, printSteps);
                if (result.State != null)
                    return result;
            }
        }
        return new SearchResult(null, int.MaxValue);
    }
}
