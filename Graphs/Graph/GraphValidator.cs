using System;

// *****************************************************************************
//                                                                             *
//                            KLASA GRAPH VALIDATOR                            *
//                                                                             *
//                    Klasa sprawdzajaca poprawnosc grafu.                     *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    class GraphValidator
    {
        // *********************************************************************
        // Zwraca true, jesli tablica krawedzi zawiera poprawne informacje 
        // o grafie (np. brak konfliktow co do tej samej krawedzi);
        // false jesli informacje niepoprawne (nie sprawdza spojnosci grafu).
        // *********************************************************************

        public static bool ValidateEdges(Edge[] edges)
        {
            if (edges == null || edges.Length < 1)
            {
                return false;
            }

            int start, end;

            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i] == null || edges[i].Weight < 1 ||
                    edges[i].Start < 0 || edges[i].End < 0)
                {
                    return false;
                }

                start = edges[i].Start;
                end = edges[i].End;

                for (int a = 0; a < i; a++)
                {
                    if (edges[a].Start == start && edges[a].End == end ||
                        edges[a].End == start && edges[a].Start == end)
                    {
                        return false;
                    }
                }

                for (int b = i + 1; b < i; b++)
                {
                    if (edges[b].Start == start && edges[b].End == end ||
                        edges[b].End == start && edges[b].Start == end)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
