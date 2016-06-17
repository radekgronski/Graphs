using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                          KLASA MATRIX GRAPH LOADER                          *
//                                                                             *
//          Klasa wczytujaca pliki i obiekty z informacjami o grafie           *
//             i produkujaca macierz sasiedztwa wczytanego grafu.              *
//                                                                             *
// *****************************************************************************

namespace Graph.Matrix
{
    class MatrixGraphLoader
    {
        // *********************************************************************
        // Wczytuje graf z podanej tablicy krawedzi i zwraca macierz sasiedztwa.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        public static int[][] Load(Edge[] edges)
        {
            if (!MatrixGraphValidator.ValidateEdges(edges))
            {
                throw new IncorrectGraphException();
            }

            // Znalezienie maksymalnego wierzcholka maxVertex:
            int maxVertex = edges.Max(e => Math.Max(e.Start, e.End));

            // Stworzenie nowej macierzy:
            int[][] graph = MatrixGraphGenerator.CreateMatrix(maxVertex + 1);

            foreach (Edge edge in edges)
            {
                graph[edge.Start][edge.End] = edge.Weight;
                graph[edge.End][edge.Start] = edge.Weight;
            }

            if (!MatrixGraphValidator.ValidateGraph(graph))
            {
                throw new IncorrectGraphException();
            }

            return graph;
        }

        // *********************************************************************
        // Wczytuje graf z tablicy (wiersze: start, end, weight).
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje, w innym przypadku zwraca macierz sasiedztwa.
        // *********************************************************************

        public static int[][] Load(int[][] graph)
        {
            throw new NotImplementedException();
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // Format: 
        //   1 wiersz: liczba_krawedzi liczba_wierzcholkow
        //   nastepne: start end weight
        // Zwraca macierz sasiedztwa.
        // *********************************************************************

        public static int[][] Load(string filename)
        {
            throw new NotImplementedException();
        }

        // *********************************************************************
        // Wczytuje graf z podanej tablicy, bedacej macierza sasiedztwa.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje. Zwraca poprawna macierz sasiedztwa.
        // *********************************************************************

        public static int[][] LoadMatrix(int[][] graph)
        {
            if (graph == null)
            {
                throw new IncorrectGraphException();
            }

            int[][] matrix = MatrixGraphGenerator.CreateMatrix(graph.Length);

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i] == null || graph[i].Length != graph.Length)
                {
                    throw new IncorrectGraphException();
                }

                for (int j = 0; j < graph[i].Length; j++)
                {
                    matrix[i][j] = graph[i][j];
                }
            }

            if (!MatrixGraphValidator.ValidateGraph(matrix))
            {
                throw new IncorrectGraphException();
            }

            return matrix;
        }

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie (postac macierzowa).
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane. Zwraca wczytana macierz sasiedztwa.
        // *********************************************************************

        public static int[][] LoadMatrix(string filename)
        {
            if (File.Exists(filename))
            {
                int[][] matrix;
                StreamReader reader = new StreamReader(filename);

                string[] splittedLine = Regex.Split(reader.ReadLine(), @"\s+");
                int vertices = 0;
                int weight;

                foreach (string s in splittedLine)
                {
                    if (int.TryParse(s, out weight))
                    {
                        vertices++;
                    }
                    else
                    {
                        throw new FileCorruptedException();
                    }
                }

                if (vertices > 0)
                {
                    matrix = MatrixGraphGenerator.CreateMatrix(vertices);

                    for (int i = 0; i < vertices; i++)
                    {
                        matrix[0][i] = int.Parse(splittedLine[i]);
                    }

                    int line = 1;

                    while (!reader.EndOfStream)
                    {
                        splittedLine = Regex.Split(reader.ReadLine(), @"\s+");

                        if (splittedLine.Length != vertices)
                        {
                            throw new FileCorruptedException();
                        }

                        if (line > vertices)
                        {
                            throw new FileCorruptedException();
                        }

                        for (int j = 0; j < vertices; j++)
                        {
                            if (int.TryParse(splittedLine[j], out weight))
                            {
                                matrix[line][j] = weight;
                            }
                            else
                            {
                                throw new FileCorruptedException();
                            }
                        }

                        line++;
                    }
                }
                else
                {
                    throw new FileCorruptedException();
                }

                reader.Close();

                if (!MatrixGraphValidator.ValidateGraph(matrix))
                {
                    throw new IncorrectGraphException();
                }

                return matrix;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

    }
}
