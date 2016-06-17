using System;

// *****************************************************************************
//                                                                             *
//                                 KLASA EDGE                                  *
//                                                                             *
//         Klasa reprezentujaca krawedz grafu w postaci zbioru 3 liczb:        *
//                        - nr poczatkowego wierzcholka                        *
//                         - nr koncowego wierzcholka                          *
//                               - waga krawedzi                               *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    public class Edge
    {
        private int startVertex;  // nr poczatkowego wierzcholka
        private int endVertex;    // nr koncowego wierzcholka
        private int weight;       // waga krawedzi

        public Edge(int startVertex, int endVertex, int weight)
        {
            this.startVertex = startVertex;
            this.endVertex = endVertex;
            this.weight = weight;
        }

        // *********************************************************************
        // Nr poczatkowego wezla.
        // *********************************************************************

        public int Start
        {
            get
            {
                return startVertex;
            }
            
        }

        // *********************************************************************
        // Nr koncowego wezla.
        // *********************************************************************

        public int End
        {
            get
            {
                return endVertex;
            }
        }

        // *********************************************************************
        // Waga krawedzi.
        // *********************************************************************

        public int Weight
        {
            get
            {
                return weight;
            }
        }

        // *********************************************************************
        // Zwraca krawedz w postaci tablicy 3 liczb: start, end, weight.
        // *********************************************************************

        public int[] ToArray()
        {
            int[] array = { Start, End, Weight };
            return array;
        }

        // *********************************************************************
        // Zwraca dane o krawedzi w postaci ciagu znakow.
        // *********************************************************************

        public override string ToString()
        {
            return Start + " " + End + " " + Weight;
        }

        // *********************************************************************
        // Przeciazenie metody porownywania.
        // *********************************************************************

        public override bool Equals(object obj)
        {
            if (obj.GetType().IsInstanceOfType(this.GetType()))
            {
                return this == (Edge)obj;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        // *********************************************************************
        // Przeciazenie metody GetHashCode().
        // *********************************************************************

        public override int GetHashCode()
        {
            return (Start + End) * 10 + Weight;
        }

        // *********************************************************************
        // Operator porownania.
        // *********************************************************************

        public static bool operator == (Edge x, Edge y)
        {
            if ((Object)x == null || (Object)y == null)
            {
                if ((Object)x == null && (Object)y == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (x.Weight != y.Weight)
            {
                return false;
            }

            if (x.Start == y.Start && x.End == y.End ||
                x.Start == y.End && x.End == y.Start)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // *********************************************************************
        // Operator nierownosci.
        // *********************************************************************

        public static bool operator != (Edge x, Edge y)
        {
            return !(x == y);
        }

    }
}
