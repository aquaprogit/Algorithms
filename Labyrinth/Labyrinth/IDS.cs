using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Labyrinth.Utils;

namespace Labyrinth;
internal class IDS
{
    private static int _iteration = 0;
    public static SearchResult Solve(State init)
    {
        _iteration = 0;
        for (int depth = 0; depth < init.GetDepth().Count; depth++)
        {
            if (DFS(init, depth).State != null)
                return new SearchResult(init, init.Generation);
        }
        return new SearchResult(null, int.MaxValue);
    }
    private static SearchResult DFS(State init, int depth)
    {
        init.PrintState(++_iteration);
        if (init.Distance == 1)
            return new SearchResult(init, init.Generation);

        foreach (var child in init.GetChildren())
        {
            SearchResult result = DFS(child, depth - 1);
            if (result.State != null)
                return result;
        }
        return new SearchResult(null, int.MaxValue);
    }
}
