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
    public static List<State> Solve(State state)
    {
        _currentState = state;
        //int i = 0;
        do
        {
            foreach (State child in _currentState.GetChildren())
            {
                _prioritiets.Add(child);
            }
            //  Console.WriteLine($"================={i}=====================");
            _currentState = _prioritiets.First();
            _prioritiets.RemoveAt(0);
            Console.WriteLine(_currentState);
            Console.WriteLine(_currentState.Maze);
            //  Console.WriteLine($"================={i++}=====================");

        } while (_currentState.Distance != 1 && _prioritiets.Count != 0);
        List<State> result = _currentState.GetPath();
        result.Reverse();
        return result;
    }
}
