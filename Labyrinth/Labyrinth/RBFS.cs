using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth;
internal static class RBFS
{
    private static OrderedList<State> _prioritiets = new OrderedList<State>(new StateComparer());
    private static State _currentState = null!;
    private static int _iteration = 0;
    public static SearchResult Solve(State state, bool printEachState = false)
    {
        return SolveRec(state, int.MaxValue, printEachState);
        //#region w/o rec
        //_currentState = state;
        //int iteration = 0;
        //do
        //{
        //    if (printEachState)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine($"================={iteration}=====================");
        //        sb.AppendLine(_currentState.ToString());
        //        sb.AppendLine(_currentState.Maze.ToString());
        //        sb.AppendLine($"================={iteration++}=====================");
        //        Console.WriteLine(sb.ToString());
        //        Console.ReadLine();
        //    }

        //    foreach (State child in _currentState.GetChildren())
        //    {
        //        _prioritiets.Add(child);
        //    }

        //    if (_prioritiets.Count == 0)
        //        break;

        //    _currentState = _prioritiets.First();
        //    _prioritiets.RemoveAt(0);
        //} while (_currentState.Distance != 1);
        //if (_currentState.Distance != 1)
        //    return new SearchResult(null, int.MaxValue);
        //List<State> result = _currentState.GetPath();
        //return new SearchResult(_currentState, _currentState.Generation);
        //#endregion
    }
    public static SearchResult SolveRec(State state, int limit, bool printEachState = false)
    {
        if (state.Distance == 0)
            return new SearchResult(state, state.Generation);

        List<State> children = state.GetChildren();

        if (children.Count == 0)
            return new SearchResult(null, int.MaxValue);

        while (true)
        {
            _iteration++;
            (int bestIndex, int secondBestIndex) = GetTwoBest(children);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"================={_iteration}=====================");
            sb.AppendLine(children[bestIndex].ToString());
            sb.AppendLine(children[bestIndex].Maze.ToString());
            sb.AppendLine($"================={_iteration++}=====================");
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

            if (children[bestIndex].Evaluation > limit)
                return new SearchResult(null, children[bestIndex].Evaluation);

            SearchResult sr = SolveRec(children[bestIndex], Math.Min(limit, children[secondBestIndex].Evaluation));

            if (sr.State is not null)
            {
                return sr;
            }
            children[bestIndex].Evaluation = sr.Path;
        }

        static (int best, int second) GetTwoBest(List<State> children)
        {
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
