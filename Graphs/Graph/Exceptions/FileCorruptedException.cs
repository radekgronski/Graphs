using System;

// *****************************************************************************
//                                                                             *
//                          FILE CORRUPTED EXCEPTION                           *
//                                                                             *
//                  Wyjatek spowodowany niepoprawnymi danymi                   *
//                    w pliku zawierajacym wczytywany graf.                    *
//                                                                             *
// *****************************************************************************

namespace Graph.Exceptions
{
    public class FileCorruptedException : Exception
    {
        public FileCorruptedException() : base()
        {
        }

        public FileCorruptedException(string message) : base(message)
        {
        }

        public FileCorruptedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
