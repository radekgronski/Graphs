using System;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                              KLASA GRAPH PRINTER                            *
//                                                                             *
//           Klasa wypisujaca grafy w roznych postaciach na ekranie.           *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    class GraphPrinter
    {
        // *********************************************************************
        // Wypisuje w konsoli graf.
        // *********************************************************************

        public static void PrintGraph(int[][] graph)
        {
            if (graph == null)
            {
                throw new IncorrectGraphException();
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i] == null)
                {
                    throw new IncorrectGraphException();
                }

                for (int j = 0; j < graph[i].Length; j++)
                {
                    Console.Write("{0}\t", graph[i][j]);
                }

                Console.WriteLine();
            }
        }

        // *********************************************************************
        // Wypisuje w konsoli graf w postaci listy sasiedztwa.
        // *********************************************************************

        public static void PrintList(int[][][] list)
        {
            if (list == null)
            {
                throw new IncorrectGraphException();
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == null)
                {
                    throw new IncorrectGraphException();
                }

                Console.Write("{0}:\t", i);

                foreach (int[] link in list[i])
                {
                    if (link == null || link.Length != 2)
                    {
                        throw new IncorrectGraphException();
                    }

                    Console.Write("({0} {1})\t", link[0], link[1]);
                }

                Console.WriteLine();
            }
        }

        // *********************************************************************
        // Wypisuje w konsoli graf w postaci macierzy sasiedztwa.
        // *********************************************************************

        public static void PrintMatrix(int[][] matrix)
        {
            PrintGraph(matrix);
        }

    }
}
