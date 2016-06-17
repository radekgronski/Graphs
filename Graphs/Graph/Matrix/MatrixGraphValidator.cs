
// *****************************************************************************
//                                                                             *
//                         KLASA MATRIX GRAPH VALIDATOR                        *
//                                                                             *
//                     Klasa sprawdzajaca poprawnosc grafu                     *
//               reprezentowanego w postaci macierzy sasiedztwa.               *
//                                                                             *
// *****************************************************************************

namespace Graph.Matrix
{
    class MatrixGraphValidator : GraphValidator
    {
        // *********************************************************************
        // Zwraca true, jesli graf jest poprawny lub false jesli nie
        // (nie sprawdza spojnosci grafu).
        // matrix - macierz sasiedztwa grafu
        // *********************************************************************

        public static bool ValidateGraph(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 2)
            {
                return false;
            }

            int connection;

            for (int i = 0; i < matrix.Length; i++)
            {
                connection = 0;

                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        if (matrix[i][j] < 0)
                        {
                            return false;
                        }

                        connection++;

                        if (i == j)
                        {
                            return false;
                        }
                    }
                }

                if (connection == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
