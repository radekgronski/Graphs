using System;

// *****************************************************************************
//                                                                             *
//                          INCORRECT GRAPH EXCEPTION                          *
//                                                                             *
//                Wyjatek wyrzucany w przypadku, gdy nastepuje                 *
//         proba zaladowania niepoprawnego grafu (z tablicy lub pliku)         *
//                    lub operacji na niepoprawnym grafie.                     *
//                                                                             *
// *****************************************************************************

namespace Graph.Exceptions
{
    public class IncorrectGraphException : Exception
    {
        public IncorrectGraphException() : base()
        {
        }

        public IncorrectGraphException(string message) : base(message)
        {
        }

        public IncorrectGraphException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
