using System;
using System.Linq;
using System.Collections.Generic;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                          KLASA LIST GRAPH GENERATOR                         *
//                                                                             *
//                        Klasa generujaca losowe grafy                        *
//                         w postaci listy sasiedztwa.                         *
//                                                                             *
// *****************************************************************************

namespace Graph.List
{
    class ListGraphGenerator : GraphGenerator
    {
        // *********************************************************************
        // Generuje i zwraca w postaci listy sasiedztwa losowy graf o zadanej 
        // liczbie wierzcholkow i procentowej gestosci (max. 100; min. gestosc
        // jest automatycznie dostosowywana, jednak deklarowana nie moze byc
        //  mniejsza niz 1).
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany
        // i zwrocona zostanie wartosc null.
        // *********************************************************************

        public static int[][][] Generate(int vertices, int density)
        {
            return Generate(vertices, density, int.MaxValue);
        }

        // *********************************************************************
        // Generuje i zwraca w postaci listy sasiedztwa losowy graf o zadanej
        // liczbie wierzcholkow i procentowej gestosci (max. 100; min. gestosc
        // jest automatycznie dostosowywana, jednak deklarowana nie moze byc
        // mniejsza niz 1), uwzgledniajac maksymalna wage krawedzi.
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany
        // i zwrocona zostanie wartosc null.
        // *********************************************************************

        public static int[][][] Generate(
            int vertices, int density, int maxWeight)
        {
            // Jesli podane dane sa niepoprawne, zwroc null.
            if (vertices < 2 || density < 1 || density > 100 || maxWeight < 1)
            {
                return null;
            }

            // Obliczenie liczby krawedzi na podstawie gestosci:
            int edges = CalculateEdges(vertices, density);

            // Zapewnienie jednego polaczenia dla kazdego wierzcholka 
            // z losowym innym wierzcholkiem:
            List<int[]>[] graph = CreateMinimalEdges(vertices, maxWeight);

            // Dopelnienie grafu pozostalymi krawedziami:
            CompleteEdges(graph, edges - vertices + 1, maxWeight);

            // Sortowanie elementow list w kolejnosci rosnacej:
            graph.ToList().ForEach(a => a.Sort((i, j) => i[0].CompareTo(j[0])));

            // Sprawdzenie poprawnosci wygenrowanyego grafu:
            if (!ListGraphValidator.ValidateGraph(graph))
            {
                throw new IncorrectGraphException();
            }

            return graph.Select(i => i.ToArray()).ToArray();
        }

        // *********************************************************************
        // Dopelnia graf podana liczba dodatkowych losowych krawedzi, 
        // gdzie list to lista sasiedztwa, na ktorej wykonywane sa operacje,
        // a maxWeight to maksymalna losowana waga krawedzi.
        // *********************************************************************

        private static void CompleteEdges(
            List<int[]>[] list, int edges, int maxWeight)
        {
            if (edges <= 0)
            {
                return;
            }

            Random random = new Random();
            int startVertex, endVertex;
            int vertices = list.Length;

            while (edges > 0)
            {
                do
                {
                    startVertex = random.Next(vertices);
                }
                while (list[startVertex].Count == vertices - 1);

                do
                {
                    endVertex = random.Next(vertices);
                } while (list[endVertex].Count(i => i[0] == startVertex) != 0 ||
                    endVertex == startVertex);

                InsertConnection(
                    list, startVertex, endVertex, random.Next(maxWeight) + 1);
                edges--;
            }
        }

        // *********************************************************************
        // Tworzy po jednym polaczeniu dla kazdej pary wierzcholkow.
        // vertices - liczba wierzcholkow, maxWeight - mksymalna losowana waga 
        // dla krawedzi.
        // *********************************************************************

        private static List<int[]>[] CreateMinimalEdges(
            int vertices, int maxWeight)
        {
            List<int[]>[] graph = new List<int[]>[vertices];
            Random random = new Random();

            for (int i = 0; i < vertices; i++)
            {
                graph[i] = new List<int[]>();
            }

            for (int i = 0; i < vertices - 1; i++)
            {
                InsertConnection(graph, i, i + 1, random.Next(maxWeight) + 1);
            }

            return graph;
        }

        // *********************************************************************
        // Wstawia polaczenie o wadze weight pomiedzy podanymi wezlami do listy
        // sasiedztwa grafu list. Nie sprawdza poprawnosci parametrow!
        // *********************************************************************

        public static void InsertConnection(
            List<int[]>[] list, int startVertex, int endVertex, int weight)
        {
            int[] vertexA = new int[2];
                vertexA[0] = endVertex;
                vertexA[1] = weight;
            list[startVertex].Add(vertexA);

            int[] vertexB = new int[2];
                vertexB[0] = startVertex;
                vertexB[1] = weight;
            list[endVertex].Add(vertexB);
        }
    }
}
