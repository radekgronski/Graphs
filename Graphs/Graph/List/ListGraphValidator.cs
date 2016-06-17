using System.Linq;
using System.Collections.Generic;

// *****************************************************************************
//                                                                             *
//                          KLASA LIST GRAPH VALIDATOR                         *
//                                                                             *
//                     Klasa sprawdzajaca poprawnosc grafu                     *
//                 reprezentowanego w postaci listy sasiedztwa.                *
//                                                                             *
// *****************************************************************************

namespace Graph.List
{
    class ListGraphValidator : GraphValidator
    {
        // *********************************************************************
        // Zwraca true, jesli graf jest poprawny lub false jesli nie
        // (nie sprawdza spojnosci grafu).
        // list - lista sasiedztwa grafu
        // *********************************************************************

        public static bool ValidateGraph(int[][][] list)
        {
            if (list == null || list.Length < 2)
            {
                return false;
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Length == 0)
                {
                    return false;
                }

                for (int j = 0; j < list[i].Length; j++)
                {                    
                    if (list[i][j][1] < 0)
                    {
                        return false;
                    }

                    if (list[i][j][0] == i)
                    {
                        return false;
                    }                    
                }
            }

            return true;
        }

        // *********************************************************************
        // Zwraca true, jesli graf jest poprawny lub false jesli nie
        // (nie sprawdza spojnosci grafu).
        // list - lista sasiedztwa grafu
        // *********************************************************************

        public static bool ValidateGraph(List<int[]>[] list)
        {
            return ValidateGraph(list.Select(i => i.ToArray()).ToArray());
        }
    }
}
