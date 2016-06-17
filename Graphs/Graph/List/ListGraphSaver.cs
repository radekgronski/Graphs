using System;
using System.IO;

namespace Graph.List
{
    class ListGraphSaver
    {
        // *********************************************************************
        // Zapisuje graf w postaci listowej (list) do pliku 
        // o podanej nazwie jako liste sasiedztwa.
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public static void ListToFile(int[][][] list, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);

            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    writer.Write("{0} ({1})\t", list[i][j][0], list[i][j][1]);
                }

                writer.WriteLine();
            }

            writer.Close();
        }

        // *********************************************************************
        // Zapisuje graf (list - lista sasiedztwa) do pliku o podanej nazwie
        // Wyrzuca wyjatek FileNotFoundException w przypadku, 
        // gdy nie mozna uzyskac dostepu do pliku.
        // *********************************************************************

        public static void ToFile(int[][][] list, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
