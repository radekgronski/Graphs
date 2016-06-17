using System;
using System.Collections.Generic;

// *****************************************************************************
//                                                                             *
//                             KLASA EDGE COMPARER                             *
//                                                                             *
//                  Klasa implementujaca interfejs IComparer,                  *
//                   sluzaca do porownywania dwoch krawedzi.                   *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    class EdgeComparer : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            if (x.Start == y.Start && x.End == y.End ||
                x.Start == y.End && x.End == y.Start)
            {
                return x.Weight - y.Weight;
            }

            if (x.Weight != y.Weight)
            {
                return x.Weight - y.Weight;
            }    
            
            if (x.Start != y.Start)
            {
                return x.Start - y.Start;
            }      
            else
            {
                return x.End - y.End;
            }
        }
    }

    class EqualityEdgeComparer : IEqualityComparer<Edge>
    {
        public bool Equals(Edge x, Edge y)
        {
            return x == y;
        }

        public int GetHashCode(Edge obj)
        {
            return obj.GetHashCode();
        }
    }
}
