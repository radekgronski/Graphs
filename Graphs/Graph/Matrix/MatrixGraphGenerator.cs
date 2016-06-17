using System;
using Graph.Exceptions;

// *****************************************************************************
//                                                                             *
//                         KLASA MATRIX GRAPH GENERATOR                        *
//                                                                             *
//                        Klasa generujaca losowe grafy                        *
//                        w postaci macierzy sasiedztwa.                       *
//                                                                             *
// *****************************************************************************

namespace Graph.Matrix
{
    class MatrixGraphGenerator : GraphGenerator
    {
        // *********************************************************************
        // Tworzy nowa, pusta macierz o rozmiarze vetrices x vetrices 
        // (liczba wierzcholkow).
        // *********************************************************************

        public static int[][] CreateMatrix(int vetrices)
        {
            int[][] matrix;

            if (vetrices > 0)
            {
                matrix = new int[vetrices][];

                for (int i = 0; i < vetrices; i++)
                {
                    matrix[i] = new int[vetrices];
                }
            }
            else
            {
                matrix = new int[0][];
            }

            return matrix;
        }

        // *********************************************************************
        // Generuje i zwraca w postaci macierzy sasiedztwa losowy graf o zadanej 
        // liczbie wierzcholkow i procentowej gestosci (max. 100; min. gestosc
        // jest automatycznie dostosowywana, jednak deklarowana nie moze byc
        //  mniejsza niz 1).
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany
        // i zwrocona zostanie wartosc null.
        // *********************************************************************

        public static int[][] Generate(int vertices, int density)
        {
            return Generate(vertices, density, int.MaxValue);
        }

        // *********************************************************************
        // Generuje i zwraca w postaci macierzy sasiedztwa losowy graf o zadanej
        // liczbie wierzcholkow i procentowej gestosci (max. 100; min. gestosc
        // jest automatycznie dostosowywana, jednak deklarowana nie moze byc
        // mniejsza niz 1), uwzgledniajac maksymalna wage krawedzi.
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany
        // i zwrocona zostanie wartosc null.
        // *********************************************************************

        public static int[][] Generate(int vertices, int density, int maxWeight)
        {
            // Jesli podane dane sa niepoprawne, zwroc null.
            if (vertices < 2 || density < 1 || density > 100 || maxWeight < 1)
            {
                return null;
            }

            // Obliczenie liczby krawedzi na podstawie gestosci:
            int edges = CalculateEdges(vertices, density);

            // Utworzenie macierzy o odpowiednim rozmiarze.
            int[][] graph = CreateMatrix(vertices);

            // Zapewnienie jednego polaczenia dla kazdego wierzcholka 
            // z losowym innym wierzcholkiem:
            CreateMinimalEdges(graph, maxWeight);

            // Dopelnienie grafu pozostalymi krawedziami:
            CompleteEdges(graph, edges - vertices + 1, maxWeight);

            // Sprawdzenie poprawnosci wygenrowanyego grafu:
            if (!MatrixGraphValidator.ValidateGraph(graph))
            {
                throw new IncorrectGraphException();
            }

            return graph;
        }

        // *********************************************************************
        // Dopelnia graf podana liczba dodatkowych losowych krawedzi, 
        // gdzie matrix to macierz sasiedztwa, na ktorej wykonywane sa operacje,
        // a maxWeight to maksymalna losowana waga krawedzi.
        // *********************************************************************

        private static void CompleteEdges(
            int[][] matrix, int edges, int maxWeight)
        {
            if (edges <= 0)
            {
                return;
            }

            Random random = new Random();
            int startVertex, endVertex, weight;
            int vertices = matrix.Length;

            while (edges > 0)
            {
                do
                {
                    startVertex = random.Next(vertices);
                }
                while (VertexConnections(matrix, startVertex) == vertices - 1);

                do
                {
                    endVertex = random.Next(vertices);
                } while (matrix[startVertex][endVertex] != 0 ||
                    endVertex == startVertex);

                weight = random.Next(maxWeight) + 1;

                matrix[startVertex][endVertex] = weight;
                matrix[endVertex][startVertex] = weight;

                edges--;
            }
        }

        // *********************************************************************
        // Tworzy po jednym polaczeniu dla kazdej pary wierzcholkow.
        // matrix - macierz sasiedztwa, na ktorew wykonywane sa operacje,
        // maxWeight - mksymalna losowana waga dla krawedzi.
        // *********************************************************************

        private static void CreateMinimalEdges(int[][] matrix, int maxWeight)
        {
            int vertices = matrix.Length;
            int weight;
            Random random = new Random();

            for (int i = 0; i < vertices - 1; i++)
            {
                weight = random.Next(maxWeight) + 1;
                matrix[i][i + 1] = weight;
                matrix[i + 1][i] = weight;
            }
        }

        // *********************************************************************
        // Zwraca liczbe polaczen podanego wezla z innymi na podstawie
        // macierzy sasiedztwa (matrix).
        // *********************************************************************

        private static int VertexConnections(int[][] matrix, int vertexIndex)
        {
            int connections = 0;

            for (int j = 0; j < matrix.Length; j++)
            {
                if (matrix[vertexIndex][j] != 0)
                {
                    connections++;
                }
            }

            return connections;
        }

    }
}
