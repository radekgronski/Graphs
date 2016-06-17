using System;
using System.Linq;
using System.Collections.Generic;
using Graph.Matrix;
using Graph.List;

// *****************************************************************************
//                                                                             *
//                                  KLASA PRIM                                 *
//                                                                             *
//             Klasa dziedziczaca po klasie MINIMUM SPANNING TREE,             *
//               wyznaczajaca minimalne drzewo rozpinajace grafu               *
//                         za pomoca algorytmu Prima.                          *
//                                                                             *
// *****************************************************************************

namespace Graph.SpanningTree
{
    public class Prim : MinimumSpanningTree
    {
        // *********************************************************************
        // Zwraca minimalne drzewo rozpinajace (w postaci grafu) na podstawie 
        // podanego grafu. Zwracane drzewo jest w typie przyjmowanego obiektu.
        // Zaczyna algorytm od wierzcholka 0.
        // *********************************************************************

        public override IGraph GetMinimumSpanningTree(IGraph graph)
        {
            return GetMinimumSpanningTree(graph, 0);
        }

        // *********************************************************************
        // Zwraca minimalne drzewo rozpinajace (w postaci grafu) na podstawie 
        // podanego grafu. Zwracane drzewo jest w typie przyjmowanego obiektu.
        // Zaczyna algorytm od podanego wierzcholka.
        // *********************************************************************

        public IGraph GetMinimumSpanningTree(IGraph graph, int startVertex)
        {
            ValidateArguments(graph, startVertex);
            IGraph tree = initiateTree(graph);

            Edge minimalEdge = null;
            List<Edge> treeList = new List<Edge>();           
            SortedSet<int> addedVertices = new SortedSet<int>();
            SortedSet<Edge> availableEdges = 
                new SortedSet<Edge>(new EdgeComparer());

            addedVertices.Add(startVertex);

            while (addedVertices.Count != graph.Vertices)
            {
                foreach (int vertex in addedVertices)
                {
                    graph.GetEdges(vertex).
                        ToList().
                        ForEach(e => availableEdges.Add(e));
                }

                for (int i = 0; i < availableEdges.Count; i++)
                {
                    minimalEdge = availableEdges.ElementAt(i);

                    if (treeList.Count( e => 
                            minimalEdge.End == e.Start || 
                            minimalEdge.End == e.End) == 0)
                    {
                        break;
                    }
                }
                
                treeList.Add(minimalEdge);
                addedVertices.Add(minimalEdge.Start);
                addedVertices.Add(minimalEdge.End);
            }

            tree.Load(treeList.ToArray());

            return tree;
        }

        // *********************************************************************
        // Sprawdza poprawnosc podanych argumentow dla metody 
        // GetMinimumSpanningTree(IGraph graph, int startVertex).
        // *********************************************************************

        private void ValidateArguments(IGraph graph, int startVertex)
        {
            if (startVertex < 0 || startVertex >= graph.Vertices)
            {
                throw new IndexOutOfRangeException();
            }

            if (graph == null)
            {
                throw new NullReferenceException();
            }
        }

    }
}
