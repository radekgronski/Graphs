using System;

// *****************************************************************************
//                                                                             *
//                               INTERFEJS GRAPH                               *
//                                                                             *
//                  Interfejs dla klas reprezentujacych graf:                  *
//                       - w postaci macierzy sasiedztwa                       *
//                        - w postaci listy sasiedztwa                         *
//                                                                             *
// *****************************************************************************

namespace Graph
{
    public interface IGraph
    {
        // *********************************************************************
        // Liczba krawedzi grafu.
        // *********************************************************************

        int Edges { get; }

        // *********************************************************************
        // Liczba wierzcholkow grafu.
        // *********************************************************************

        int Vertices { get; }

        // *********************************************************************
        // Generuje losowy graf o zadanej liczbie wierzcholkow i procentowej 
        // gestosci (max. 100; min. gestosc jest automatycznie dostosowywana, 
        // jednak deklarowana nie moze byc mniejsza niz 1), uwzgledniajac
        // maksymalna wage krawedzi.
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany.
        // *********************************************************************

        void Generate(int vetrices, int density, int maxWeight);

        // *********************************************************************
        // Generuje losowy graf o zadanej liczbie wierzcholkow i procentowej 
        // gestosci (max. 100; min. gestosc jest automatycznie dostosowywana, 
        // jednak deklarowana nie moze byc mniejsza niz 1).
        // W pzypadku niepoprawnych parametrow, graf nie zostanie wygenerowany.
        // *********************************************************************

        void Generate(int vetrices, int density);

        // *********************************************************************
        // Wczytuje graf z podanej tablicy krawedzi.
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        void Load(Edge[] edges);

        // *********************************************************************
        // Wczytuje graf z pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException wprzypadku nieznalezienia pliku
        // lub FileCorruptedException w przypadku, gdy plik zawiera 
        // niepoprawne dane.
        // *********************************************************************

        void Load(String filename);

        // *********************************************************************
        // Wczytuje graf z tablicy (wiersze: start, end, weight).
        // Wyrzuca IncorrectGraphException w przypadku, gdy tablica zawiera
        // nieprawidlowe informacje.
        // *********************************************************************

        void Load(int[][] graph);

        // *********************************************************************
        // Zwraca graf w postaci tablicy o 3 kolumnach:
        // wierzcholekPoczatkowy wierzcholekKoncowy waga
        // *********************************************************************

        int[][] GetGraph();

        // *********************************************************************
        // Zapisuje graf do pliku o podanej nazwie.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        void ToFile(String fileName);

        // *********************************************************************
        // Zwraca tablice krawedzi wychodzacych z podanego wierzcholka.
        // Wyrzuca NoSuchVertexException w przypadku,
        // gdy podano niestniejacy wierzcholek.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        Edge[] GetEdges(int vertex);

        // *********************************************************************
        // Zwraca tablice krawedzi o najmniejszej istnieacej wadze,
        // jednak nie mniejszej niz podany parametr minimalWeight.
        // W szczegolnosci moze zwrocic pusta tablice.
        // *********************************************************************

        Edge[] GetMinimalEdges(int minimalWeight);

        // *********************************************************************
        // Zwraca maksymalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        int Max();

        // *********************************************************************
        // Zwraca minimalna wage z grafu (w szczegolnosci 0, jesli graf
        // nie istnieje).
        // *********************************************************************

        int Min();

    }
}
