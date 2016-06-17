using System.Linq;
using System.Collections.Generic;
using Graph.List;
using Graph.Matrix;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                            KLASA GRAPH TRANSLATOR                           *
//                                                                             *
//                  Klasa transformujaca rozne reprezentacje                   *
//                          grafow oraz zwracajace je.                         *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    class GraphTranslator
    {
        // *********************************************************************
        // Zwraca graf w postaci tablicy o 3 kolumnach:
        // wierzcholekPoczatkowy wierzcholekKoncowy waga
        // na podstawie przekazanej listy sasiedztwa.
        // *********************************************************************

        public static int[][] ListToGraph(int[][][] list)
        {
            if (!ListGraphValidator.ValidateGraph(list))
            {
                throw new IncorrectGraphException();
            }

            HashSet<Edge> edges = new HashSet<Edge>(new EqualityEdgeComparer());

            for (int i = 0; i < list.Length; i++)
            {
                foreach (int[] link in list[i])
                {
                    edges.Add(new Edge(i, link[0], link[1]));
                }
            }

            return edges.Select(e => e.ToArray()).ToArray();
        }

        // *********************************************************************
        // Zwraca graf w postaci tablicy o 3 kolumnach:
        // wierzcholekPoczatkowy wierzcholekKoncowy waga
        // na podstawie przekazanej macierzy sasiedztwa.
        // *********************************************************************

        public static int[][] MatrixToGraph(int[][] matrix)
        {
            if (!MatrixGraphValidator.ValidateGraph(matrix))
            {
                throw new IncorrectGraphException();
            }

            HashSet<Edge> edges = new HashSet<Edge>(new EqualityEdgeComparer());

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        edges.Add(new Edge(i, j, matrix[i][j]));
                    }
                }
            }

            return edges.Select(e => e.ToArray()).ToArray();
        }

    }
}
