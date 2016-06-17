using System;
using System.Linq;
using System.Collections.Generic;

// *****************************************************************************
//                                                                             *
//                                KLASA KRUSKAL                                *
//                                                                             *
//             Klasa dziedziczaca po klasie MINIMUM SPANNING TREE,             *
//               wyznaczajaca minimalne drzewo rozpinajace grafu               *
//                        za pomoca algorytmu Kruskala.                        *
//                                                                             *
// *****************************************************************************

namespace Graph.SpanningTree
{
    public class Kruskal : MinimumSpanningTree
    {
        // *********************************************************************
        // Prywatna klasa przechowujaca krawedz i kod jej "koloru"
        // dla algorytmu Kruskala.
        // *********************************************************************

        private class EdgeTuple
        {
            public Edge Edge { get; set; }
            public int Color { get; set; }

            public EdgeTuple(Edge edge, int color)
            {
                Edge = edge;
                Color = color;
            }
        }

        // *********************************************************************
        // Zwraca minimalne drzewo rozpinajace (w postaci grafu) na podstawie 
        // podanego grafu. Zwracane drzewo jest w typie przyjmowanego obiektu.
        // *********************************************************************

        public override IGraph GetMinimumSpanningTree(IGraph graph)
        { 
            if (graph == null)
            {
                throw new NullReferenceException();
            }

            IGraph tree = initiateTree(graph);
            var treeList = new List<EdgeTuple>();
            var startList = new List<EdgeTuple>();
            var endList = new List<EdgeTuple>();
            Edge[] edges = null;
            int atLeastWeight = 1;

            while (treeList.Count != graph.Vertices - 1)
            {
                edges = graph.GetMinimalEdges(atLeastWeight);

                foreach (Edge edge in edges)
                {
                    startList.Clear();
                    endList.Clear();

                    CollectAssociations(treeList, edge, startList, endList);
                    AddEdgeIfMatching(treeList, edge, startList, endList);
                }

                if (edges != null && edges.Length > 0)
                {
                    atLeastWeight = edges[0].Weight + 1;
                }
            }
           
            tree.Load(treeList.Select(t => t.Edge).ToArray());

            return tree;
        }

        // *********************************************************************
        // Dodaje krawedz i przypisuje jej odpowiedni kolor, jesli krawedz
        // spelnia warunki dodania do listy.
        // *********************************************************************

        private void AddEdgeIfMatching(List<EdgeTuple> treeList, Edge edge,
            List<EdgeTuple> startList, List<EdgeTuple> endList)
        {        
            int startColor = (startList.Count > 0) ? startList.First()
                .Color : -1;
            int endColor = (endList.Count > 0) ? endList.First().Color : -1;

            // Zapetlenia na listach startList i endList:
            bool startLoop = (startList.Count > 0) ? startList
                .Where(t => t.Color != startColor).Any() : false;
            bool endLoop = (endList.Count > 0) ? endList
                .Where(t => t.Color != endColor).Any() : false;

            // Jesli obie listy sa puste, dodaj krawedz o nowym kolorze:
            if (startList.Count == 0 && endList.Count == 0)
            {
                int newColor = (treeList.Count == 0) ? 0 :
                    treeList.Max(t => t.Color) + 1;
                treeList.Add(new EdgeTuple(edge, newColor));
            }
            // Jesli obie nie sa puste:
            else if (startList.Count > 0 && endList.Count > 0)
            {
                // Jesli w obu nie ma petli i sa to rozne grupy,
                // dodanie krawedzi i scalenie obu list:
                if (!(startLoop || endLoop) && startColor != endColor)
                {
                    treeList.Add(new EdgeTuple(edge, startColor));
                    treeList.Where(t => t.Color == endColor).ToList().
                        ForEach(t => t.Color = startColor);
                }
            }
            // Jesli tylko startList nie jest pusta, dodanie do niej:
            else if (startList.Count > 0)
            {
                treeList.Add(new EdgeTuple(edge, startColor));
            }
            // Jesli tylko endList nie jest pusta, dodanie do niej:
            else
            {
                treeList.Add(new EdgeTuple(edge, endColor));
            }
        }

        // *********************************************************************
        // Dodawanie krotek z krawedziami do list:
        //  - wspolny wierzcholek krawedzi edge z wierzcholkiem 
        //    poczatkowym aktualnej krawedzi
        //  - wspolny wierzcholek krawedzi edge z wierzcholkiem 
        //    koncowym aktualnej krawedzi
        // *********************************************************************

        private void CollectAssociations(List<EdgeTuple> treeList, Edge edge,
            List<EdgeTuple> startList, List<EdgeTuple> endList)
        {
            foreach (EdgeTuple tuple in treeList)
            {
                if (edge.Start == tuple.Edge.Start ||
                    edge.Start == tuple.Edge.End)
                {
                    startList.Add(tuple);
                }

                if (edge.End == tuple.Edge.Start ||
                    edge.End == tuple.Edge.End)
                {
                    endList.Add(tuple);
                }
            }
        }

    }
}
