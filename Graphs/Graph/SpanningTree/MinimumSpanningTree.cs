using System;
using Graph.Matrix;
using Graph.List;

// *****************************************************************************
//                                                                             *
//                   ABSTRAKCYJNA KLASA MINIMUM SPANNING TREE                  *
//                                                                             *
//              Klasa abstrakcyjna przeznaczona do dziedziczenia               *
//         przez klasy wyznaczajace minimalne drzewo rozpinajace grafu.        *
//                                                                             *
// *****************************************************************************

namespace Graph.SpanningTree
{
    public abstract class MinimumSpanningTree
    {
        // *********************************************************************
        // Zwraca minimalne drzewo rozpinajace (w postaci grafu) na podstawie 
        // podanego grafu. Zwracane drzewo jest w typie przyjmowanego obiektu.
        // *********************************************************************

        public abstract IGraph GetMinimumSpanningTree(IGraph graph);

        // *********************************************************************
        // Rozpoznaje typ obiektu implementujacego interfejs IGraph i zwraca
        // nowy obiekt o tym samym typie.
        // *********************************************************************

        protected IGraph initiateTree(IGraph graph)
        {
            if (graph.GetType().IsInstanceOfType(new MatrixGraph()))
            {
                return new MatrixGraph();
            }
            else if (graph.GetType().IsInstanceOfType(new ListGraph()))
            {
                return new ListGraph();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
