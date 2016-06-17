using System.Linq;
using System.Collections.Generic;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                              KLASA MATRIX GRAPH                             *
//                                                                             *
//                    Klasa implementujaca interfejs GRAPH,                    *
//              reprezentujaca graf za pomoca macierzy sasiedztwa.             *
//                                                                             *
// *****************************************************************************

namespace Graph.Matrix
{
    public class MatrixGraph : IGraph
    {
        private int[][] matrix;     // macierz sasiedztwa grafu
        private int edges;          // liczba krawedzi grafu

        // Konstruktor tworzacy pusty graf.
        public MatrixGraph()
        {
            Clear();
        }

        // Konstruktor wczytujacy graf z pliku.
        public MatrixGraph(string fileName) : this()
        {
            Load(fileName);
        }

        // Konstruktor wczytujacy graf z tablicy.
        public MatrixGraph(int[][] graph) : this()
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
                return (matrix == null) ? 0 : matrix.Length;
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

                return (edges > 0) ? 
                    edges : 
                    matrix.Sum(i => i.Count(j => j != 0)) / 2;
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
            matrix = MatrixGraphGenerator.
                Generate(vertices, density, maxWeight);

            if (matrix == null)
            {
                Clear();
                throw new IncorrectGraphException();
            }
            else
            {
                edges = MatrixGraphGenerator.
                    CalculateEdges(vertices, density);
            }
        }

        // *********************************************************************
        // Zwraca tablice krawedzi wychodzacych z podanego wierzcholka.
        // Wyrzuca NoSuchVertexException w przypadku,
        // gdy podano niestniejacy wierzcholek.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        public Edge[] GetEdges(int vertex)
        {
            if (matrix == null || matrix.Length < 2 || 
                vertex > matrix.Length || vertex < 0)
            {
                return new Edge[0];
            }

            List<Edge> list = new List<Edge>();
            
            for (int i = 0; i < matrix[vertex].Length; i++)
            {
                if (matrix[vertex][i] != 0)
                {
                    list.Add(new Edge(vertex, i, matrix[vertex][i]));
                }
            }

            return list.ToArray();
        }

        // *********************************************************************
        // Zwraca graf w postaci tablicy o 3 kolumnach:
        // wierzcholekPoczatkowy wierzcholekKoncowy waga
        // *********************************************************************

        public int[][] GetGraph()
        {
            return GraphTranslator.MatrixToGraph(matrix);
        }

        // *********************************************************************
        // Zwraca graf w postaci macierzy sasiedztwa.
        // *********************************************************************

        public int[][] GetGraphMatrix()
        {
            return matrix.ToArray();
        }

        // *********************************************************************
        // Zwraca tablice krawedzi o najmniejszej istnieacej wadze,
        // jednak nie mniejszej niz podany parametr minimalWeight.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        public Edge[] GetMinimalEdges(int minimalWeight)
        {
            if (matrix == null || matrix.Length < 2)
            {
                return new Edge[0];
            }

            int minimum = matrix.Min(i => i.Min(j => 
                (j >= minimalWeight) ? j : int.MaxValue));

            List<Edge> edgesList = new List<Edge>();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == minimum)
                    {
                        bool alreadyAdded =
                            edgesList.Count(e =>
                                e.Start == i && e.End == j ||
                                e.Start == j && e.End == i) > 0;

                        if (!alreadyAdded)
                        {
                            edgesList.Add(new Edge(i, j, matrix[i][j]));
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
            matrix = MatrixGraphLoader.Load(edges);
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // Format: 
        //   1 wiersz: liczba_krawedzi liczba_wierzcholkow
        //   nastepne: start end weight
        // *********************************************************************

        public void Load(string filename)
        {
            matrix = MatrixGraphLoader.Load(filename);
        }

        // *********************************************************************
        // Wczytuje graf z tablicy (wiersze: start, end, weight).
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public void Load(int[][] graph)
        {
            matrix = MatrixGraphLoader.Load(graph);
        }

        // *********************************************************************
        // Wczytuje graf z podanej tablicy, bedacej macierza sasiedztwa.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public void LoadMatrix(int[][] graph)
        {
            matrix = MatrixGraphLoader.LoadMatrix(graph);
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie (postac macierzowa).
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // *********************************************************************

        public void LoadMatrix(string filename)
        {
            matrix = MatrixGraphLoader.LoadMatrix(filename);
        }

        // *********************************************************************
        // Zapisuje graf w postaci macierzowej do pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public void MatrixToFile(string fileName)
        {
            MatrixGraphSaver.MatrixToFile(matrix, fileName);
        }

        // *********************************************************************
        // Zwraca maksymalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        public int Max()
        {
            if (matrix == null || matrix.Length == 0)
            {
                return 0;
            }

            return matrix.Max(i => i.Max());
        }

        // *********************************************************************
        // Zwraca minimalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        public int Min()
        {
            if (matrix == null || matrix.Length == 0)
            {
                return 0;
            }

            MatrixGraphValidator.ValidateGraph(matrix);
            return matrix.Min(i => i.Min(j => (j > 0) ? j : int.MaxValue));
        }

        // *********************************************************************
        // Zapisuje graf do pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public void ToFile(string fileName)
        {
            MatrixGraphSaver.ToFile(matrix, fileName);
        }

        // *********************************************************************
        // Czysci zawartosc pol.
        // *********************************************************************

        private void Clear()
        {
            matrix = new int[0][];
            edges = 0;
        }

    }
}
