using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Depth-first search (DFS) algorithm for traversing/searching graph vertices
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class DepthFirstSearch<TGraph, TVertex> : IActionAlgorithm<TGraph, TVertex, TVertex, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
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
            var verticesComparer = new GraphVertexEqualityComparer<TVertex>();
            var visitedVertices = new HashSet<TVertex>(verticesComparer);
            var queuedVertices = new HashSet<TVertex>(verticesComparer);

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
