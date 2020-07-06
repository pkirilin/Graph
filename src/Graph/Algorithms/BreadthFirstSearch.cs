using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Breadth-first search (BFS) algorithm for traversing/searching graph vertices
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class BreadthFirstSearch<TGraph, TVertex> : IActionAlgorithm<TGraph, TVertex, TVertex, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        public void Execute(TGraph graph, TVertex initialVertex, Action<TVertex> action)
        {
            if (!graph.AdjacencyLists.ContainsKey(initialVertex))
                throw new ArgumentException($"Initial vertex = '{initialVertex}' doesn't exist in graph", nameof(initialVertex));

            var verticesForNextVisit = new Queue<TVertex>(new TVertex[] { initialVertex });
            var visitedVertices = new HashSet<TVertex>(new GraphVertexEqualityComparer<TVertex>());
            var queuedVertices = new HashSet<TVertex>(new GraphVertexEqualityComparer<TVertex>());

            while (verticesForNextVisit.Any())
            {
                var vertex = verticesForNextVisit.Dequeue();

                foreach (var connectedVertex in graph.AdjacencyLists[vertex])
                {
                    if (!visitedVertices.Contains(connectedVertex) && !queuedVertices.Contains(connectedVertex))
                    {
                        verticesForNextVisit.Enqueue(connectedVertex);
                        queuedVertices.Add(connectedVertex);
                    }
                }

                action(vertex);
                visitedVertices.Add(vertex);
            }
        }
    }
}
