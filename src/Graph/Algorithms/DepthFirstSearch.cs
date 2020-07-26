using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Depth-first search (DFS) algorithm for traversing/searching graph data structure
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class DepthFirstSearch<TGraph, TVertex> : IGraphSearcher<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IEqualityComparer<TVertex> _verticesComparer;

        public DepthFirstSearch()
        {
            _verticesComparer = new GraphVertexEqualityComparer<TVertex>();
        }

        public void Execute(TGraph graph, TVertex initialVertex, Action<TVertex> action)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (initialVertex == null)
                throw new ArgumentNullException(nameof(initialVertex));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (!graph.AdjacencyLists.ContainsKey(initialVertex))
                throw new ArgumentException($"Initial vertex = '{initialVertex}' doesn't exist in graph", nameof(initialVertex));

            var verticesForNextVisit = new Stack<TVertex>(new TVertex[] { initialVertex });
            var visitedVertices = new HashSet<TVertex>(_verticesComparer);
            var queuedVertices = new HashSet<TVertex>(_verticesComparer);

            while (verticesForNextVisit.Any())
            {
                var vertex = verticesForNextVisit.Pop();

                foreach (var connectedVertex in graph.AdjacencyLists[vertex])
                {
                    if (!visitedVertices.Contains(connectedVertex) && !queuedVertices.Contains(connectedVertex))
                    {
                        verticesForNextVisit.Push(connectedVertex);
                        queuedVertices.Add(connectedVertex);
                    }
                }

                action(vertex);
                visitedVertices.Add(vertex);
            }
        }
    }
}
