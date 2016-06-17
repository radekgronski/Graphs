using System;
using System.Linq;
using System.Collections.Generic;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                           KLASA LIST GRAPH LOADER                           *
//                                                                             *
//          Klasa wczytujaca pliki i obiekty z informacjami o grafie           *
//              i produkujaca liste sasiedztwa wczytanego grafu.               *
//                                                                             *
// *****************************************************************************

namespace Graph.List
{
    class ListGraphLoader
    {
        // *********************************************************************
        // Wczytuje graf z podanej tablicy krawedzi i zwraca liste sasiedztwa.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public static int[][][] Load(Edge[] edges)
        {
            if (!ListGraphValidator.ValidateEdges(edges))
            {
                throw new IncorrectGraphException();
            }

            // Znalezienie maksymalnego wierzcholka maxVertex:
            int maxVertex = edges.Max(e => Math.Max(e.Start, e.End));

            // Utworzenie listy sasiedztwa i zainicjowanie jej:
            List<int[]>[] graph = new List<int[]>[maxVertex + 1];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int[]>();
            }

            // Dodanie do listy przekazanych metodzie krawedzi:
            edges.ToList().ForEach(
                e => ListGraphGenerator.InsertConnection(
                    graph, e.Start, e.End, e.Weight));

            if (!ListGraphValidator.ValidateGraph(graph))
            {
                throw new IncorrectGraphException();
            }

            return graph.Select(i => i.ToArray()).ToArray();
        }

        // *********************************************************************
        // Wczytuje graf z tablicy (wiersze: start, end, weight).
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje, w innym przypadku zwraca liste sasiedztwa.
        // *********************************************************************

        public static int[][][] Load(int[][] graph)
        {
            if (graph == null || graph.Length < 2)
            {
                throw new IncorrectGraphException();
            }

            int maxVertex = graph.Max(i => Math.Max(i[0], i[1]));
            List<int[]>[] edges = new List<int[]>[maxVertex + 1];

            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = new List<int[]>();
            }

            foreach (int[] edge in graph)
            {
                if (edge == null || edge.Length != 3 || 
                    edge[0] < 0 || edge[1] < 0 || edge[2] <=0)
                {
                    throw new IncorrectGraphException();
                }

                ListGraphGenerator.InsertConnection(
                    edges, edge[0], edge[1], edge[2]);
            }

            return edges.Select(e => e.ToArray()).ToArray();
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // Format: 
        //   1 wiersz: liczba_krawedzi liczba_wierzcholkow
        //   nastepne: start end weight
        // Zwraca liste sasiedztwa.
        // *********************************************************************

        public static int[][][] Load(string filename)
        {
            throw new NotImplementedException();
        }

        // *********************************************************************
        // Wczytuje graf z podanej tablicy, bedacej lista sasiedztwa.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje. Zwraca poprawna liste sasiedztwa.
        // *********************************************************************

        public static int[][][] LoadList(int[][][] graph)
        {
            if (graph == null)
            {
                throw new IncorrectGraphException();
            }

            int[][][] list = new int[graph.Length][][];

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i] == null || graph[i].Length == 0)
                {
                    throw new IncorrectGraphException();
                }

                list[i] = new int[graph[i].Length][];

                for (int j = 0; j < graph[i].Length; j++)
                {
                    if (graph[i][j] == null || graph[i][j].Length != 2)
                    {
                        throw new IncorrectGraphException();
                    }

                    list[i][j] = new int[2];
                    list[i][j][0] = graph[i][j][0];
                    list[i][j][1] = graph[i][j][1];
                }
            }

            if (!ListGraphValidator.ValidateGraph(list))
            {
                throw new IncorrectGraphException();
            }

            return list;
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie (postac listowa).
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane. Zwraca wczytana liste sasiedztwa.
        // *********************************************************************

        public static int[][][] LoadList(string filename)
        {
            throw new NotImplementedException();
        }

    }
}
