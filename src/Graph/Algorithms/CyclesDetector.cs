using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Algorithm that determines whether target graph contains at least one cycle.
    /// It runs a customized Depth-first search for graph starting from specified vertex,
    /// and for each vertex it tries to find an edge connecting it with one of already visited ones.
    /// If it finds any, cycle is found.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class CyclesDetector<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, bool, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        public bool Execute(TGraph graph, TVertex initialVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (!graph.AdjacencyLists.ContainsKey(initialVertex))
                throw new ArgumentException($"Initial vertex = '{initialVertex}' doesn't exist in graph", nameof(initialVertex));

            var verticesForNextVisit = new Stack<TVertex>(new TVertex[] { initialVertex });
            var visitedVertices = new HashSet<TVertex>(new GraphVertexEqualityComparer<TVertex>());
            var queuedVertices = new HashSet<TVertex>(new GraphVertexEqualityComparer<TVertex>());
            var excludedEdges = new HashSet<Edge<TVertex>>();

            while (verticesForNextVisit.Any())
            {
                var curVertex = verticesForNextVisit.Pop();

                foreach (var connectedVertex in graph.AdjacencyLists[curVertex])
                {
                    if (!visitedVertices.Contains(connectedVertex) && !queuedVertices.Contains(connectedVertex))
                    {
                        verticesForNextVisit.Push(connectedVertex);
                        queuedVertices.Add(connectedVertex);
                        // For undirected graphs: excluding the reverse edge for correct cycle detection
                        excludedEdges.Add(new Edge<TVertex>(connectedVertex, curVertex));
                    }
                }

                // If edge to any already visited vertex exists (except excluded edges), there's a cycle in graph
                if (visitedVertices.Any(v => IsEdgeToVisitedVertexExists(graph, curVertex, v, excludedEdges)))
                    return true;

                visitedVertices.Add(curVertex);
            }

            return false;
        }

        /// <summary>
        /// Checks if edge where destination is visited vertex and source is current vertex exists
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="curVertex">Current vertex</param>
        /// <param name="visitedVertex">Visited vertex</param>
        /// <param name="excludedEdges">The edges that must not be considered in this check</param>
        /// <returns>True if edge exists, false - if not</returns>
        private bool IsEdgeToVisitedVertexExists(TGraph graph, TVertex curVertex, TVertex visitedVertex, ISet<Edge<TVertex>> excludedEdges)
        {
            return !excludedEdges.Contains(new Edge<TVertex>(curVertex, visitedVertex))
                && graph.AdjacencyLists[curVertex].Contains(visitedVertex);
        }
    }
}
