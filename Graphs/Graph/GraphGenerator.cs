using System;

// *****************************************************************************
//                                                                             *
//                            KLASA GRAPH GENERATOR                            *
//                                                                             *
//              Klasa pomocnicza do generowania losowych grafow,               *
//                    przeznaczona do dziedziczenia po niej.                   *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    class GraphGenerator
    {
        // *********************************************************************
        // Zwraca obliczona na podstawie liczby wierzcholkow i gestosci
        // liczbe krawedzi.
        // Jesli liczba krawedzi bylaby mniejsza od minimalnej dozwolonej, 
        // przyjmuje minimalna.
        // *********************************************************************

        public static int CalculateEdges(int vertices, int density)
        {
            // Obliczenie liczby krawedzi na podstawie gestosci:
            int edges = (int)
                (((float)density) / 100 * vertices * (vertices - 1) / 2);

            // Jesli liczba krawedzi bylaby mniejsza od minimalnej
            // dozwolonej, przyjmij minimalna.
            if (edges < (vertices - 1))
            {
                edges = vertices - 1;
            }

            return edges;
        }
    }
}
