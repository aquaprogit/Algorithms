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
    public static List<State>? Solve(State state, bool printEachState = false)
    {
        _currentState = state;
        int iteration = 0;
        do
        {
            if (printEachState)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"================={iteration}=====================");
                sb.AppendLine(_currentState.ToString());
                sb.AppendLine(_currentState.Maze.ToString());
                sb.AppendLine($"================={iteration++}=====================");
                Console.WriteLine(sb.ToString());
            }

            foreach (State child in _currentState.GetChildren())
            {
                _prioritiets.Add(child);
            }

            if (_prioritiets.Count == 0)
                break;

            _currentState = _prioritiets.First();
            _prioritiets.RemoveAt(0);
        } while (_currentState.Distance != 1);
        if (_currentState.Distance != 1)
            return null;
        List<State> result = _currentState.GetPath();
        result.Reverse();
        return result;
    }
}
