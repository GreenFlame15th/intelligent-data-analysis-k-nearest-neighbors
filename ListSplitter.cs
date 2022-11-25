using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    static class ListSplitter
    {
        static public (List<T>, List<T>) SplitList<T>(this List<T> list, float firstListShare)
        {
            int splitPoint = (int)Math.Floor(firstListShare * list.Count);
            List<T> a = list.GetRange(0, splitPoint);
            List<T> b = list.GetRange(splitPoint, list.Count - splitPoint);
            return (a, b);
        }

        static public (List<T>, List<T>) RandomeSplitList<T>(this List<T> list, float firstListShare)
        {
            list.Shuffle();
            return list.SplitList(firstListShare);
        }
    }
}
