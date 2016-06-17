using System;

// *****************************************************************************
//                                                                             *
//                          NO SUCH VERTEX EXCEPTION                           *
//                                                                             *
//                Wyjatek wyrzucany w przypadku, gdy nastepuje                 *
//           proba odwolania sie do wierzcholka, ktory nie istnieje.           *
//                                                                             *
// *****************************************************************************

namespace Graph.Exceptions
{
    public class NoSuchVertexException : Exception
    {
        public NoSuchVertexException() : base()
        {
        }

        public NoSuchVertexException(string message) : base(message)
        {
        }

        public NoSuchVertexException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
