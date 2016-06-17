using System;
using System.IO;

// *****************************************************************************
//                                                                             *
//                           KLASA MATRIX GRAPH SAVER                          *
//                                                                             *
//                Klasa zapisujaca graf reprezentowany w postaci               *
//                        macierzy sasiedztwa do pliku.                        *
//                                                                             *
// *****************************************************************************

namespace Graph.Matrix
{
    class MatrixGraphSaver
    {
        // *********************************************************************
        // Zapisuje graf w postaci macierzowej (matrix) do pliku 
        // o podanej nazwie jako macierz sasiedztwa.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public static void MatrixToFile(int[][] matrix, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    writer.Write("{0}\t", matrix[i][j]);
                }

                writer.WriteLine();
            }

            writer.Close();
        }

        // *********************************************************************
        // Zapisuje graf (matrix - macierz sasiedztwa) do pliku o podanej nazwie
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public static void ToFile(int[][] matrix, string fileName)
        {
            throw new NotImplementedException();
        }

    }
}
