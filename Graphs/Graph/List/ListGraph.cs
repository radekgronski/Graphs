using System;
using System.Linq;
using System.Collections.Generic;

// *****************************************************************************
//                                                                             *
//                               KLASA LIST GRAPH                              *
//                                                                             *
//                    Klasa implementujaca interfejs GRAPH,                    *
//               reprezentujaca graf za pomoca listy sasiedztwa.               *
//                                                                             *
// *****************************************************************************

namespace Graph.List
{
    public class ListGraph : IGraph
    {
        private int[][][] list;   // lista sasiedztwa grafu
        private int edges;        // liczba krawedzi grafu 

        // Konstruktor tworzacy pusty graf.
        public ListGraph()
        {
            Clear();
        }

        // Konstruktor wczytujacy graf z pliku.
        public ListGraph(string fileName) : this()
        {
            Load(fileName);
        }

        // Konstruktor wczytujacy graf z tablicy.
        public ListGraph(int[][] graph) : this()
        {
            Load(graph);
        }

        // *********************************************************************
        // Liczba wierzcholkow grafu.
        // *********************************************************************

        public int Vertices
        {
            get
            {
                return (list == null) ? 0 : list.Length;
            }
        }

        // *********************************************************************
        // Liczba krawedzi grafu.
        // *********************************************************************

        public int Edges
        {
            get
            {
                if (Vertices == 0)
                {
                    return 0;
                }

                return (edges > 0) ? edges : list.Sum(i => i.Count()) / 2;
            }
        }

        // *********************************************************************
        // Generuje losowy graf o zadanej liczbie wierzcholkow i procentowej 
        // gestosci (max. 100; min. gestosc jest automatycznie dostosowywana, 
        // jednak deklarowana nie moze byc mniejsza niz 1).
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany.
        // *********************************************************************

        public void Generate(int vertices, int density)
        {
            Generate(vertices, density, int.MaxValue);
        }

        // *********************************************************************
        // Generuje losowy graf o zadanej liczbie wierzcholkow i procentowej 
        // gestosci (max. 100; min. gestosc jest automatycznie dostosowywana, 
        // jednak deklarowana nie moze byc mniejsza niz 1), uwzgledniajac
        // maksymalna wage krawedzi.
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany.
        // *********************************************************************

        public void Generate(int vertices, int density, int maxWeight)
        {
            list = ListGraphGenerator.Generate(vertices, density, maxWeight);            
        }

        // *********************************************************************
        // Zwraca tablice krawedzi wychodzacych z podanego wierzcholka.
        // Wyrzuca NoSuchVertexException w przypadku,
        // gdy podano niestniejacy wierzcholek.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        public Edge[] GetEdges(int vertex)
        {
            if (list == null || list.Length < 2 || 
                vertex > list.Length || vertex < 0)
            {
                return new Edge[0];
            }

            List<Edge> edgesList = new List<Edge>();

            foreach (int[] link in list[vertex])
            {
                edgesList.Add(new Edge(vertex, link[0], link[1]));
            }

            return edgesList.ToArray();
        }

        // *********************************************************************
        // Zwraca graf w postaci tablicy o 3 kolumnach:
        // wierzcholekPoczatkowy wierzcholekKoncowy waga
        // *********************************************************************

        public int[][] GetGraph()
        {
            return GraphTranslator.ListToGraph(list);
        }

        // *********************************************************************
        // Zwraca graf w postaci listy sasiedztwa.
        // *********************************************************************

        public int[][][] GetGraphList()
        {
            return list.ToArray();
        }

        // *********************************************************************
        // Zwraca tablice krawedzi o najmniejszej istnieacej wadze,
        // jednak nie mniejszej niz podany parametr minimalWeight.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        public Edge[] GetMinimalEdges(int minimalWeight)
        {
            if (list == null || list.Length < 2)
            {
                return new Edge[0];
            }

            int minimum = list.Min(i => i.Min(j => 
                (j[1] >= minimalWeight) ? j[1] : int.MaxValue ));

            List<Edge> edgesList = new List<Edge>();

            for (int i = 0; i < list.Length; i++)
            {
                foreach (int[] link in list[i])
                {
                    if (link[1] == minimum)
                    {
                        bool alreadyAdded = 
                            edgesList.Count(e => 
                                e.Start == i && e.End == link[0] || 
                                e.Start == link[0] && e.End == i) > 0;

                        if (!alreadyAdded)
                        {
                            edgesList.Add(new Edge(i, link[0], link[1]));
                        }
                    }
                }
            }

            return edgesList.ToArray();
        }

        // *********************************************************************
        // Wczytuje graf z podanej tablicy krawedzi.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public void Load(Edge[] edges)
        {
            list = ListGraphLoader.Load(edges);
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // *********************************************************************

        public void Load(string filename)
        {
            list = ListGraphLoader.Load(filename);
        }

        // *********************************************************************
        // Wczytuje graf z tablicy (wiersze: start, end, weight).
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public void Load(int[][] graph)
        {
            list = ListGraphLoader.Load(graph);
        }

        // *********************************************************************
        // Zwraca maksymalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        public int Max()
        {
            if (list == null || list.Length == 0)
            {
                return 0;
            }

            return list.Max(i => i.Max(j => j[1]));
        }

        // *********************************************************************
        // Zwraca minimalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        public int Min()
        {
            if (list == null || list.Length == 0)
            {
                return 0;
            }

            ListGraphValidator.ValidateGraph(list);
            return list.Min(i => i.Min(j => (j[1] > 0) ? j[1] : int.MaxValue));
        }

        // *********************************************************************
        // Zapisuje graf do pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public void ToFile(string fileName)
        {
            ListGraphSaver.ToFile(list, fileName);
        }

        // *********************************************************************
        // Czysci zawartosc pol.
        // *********************************************************************

        private void Clear()
        {
            list = new int[0][][];
            edges = 0;
        }

    }
}
