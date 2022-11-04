using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth;
internal class RBFS
{
    private SortedList<int, State> _prioritiets;

    public RBFS()
    {
        _prioritiets = new SortedList<int, State>();
    }
}
