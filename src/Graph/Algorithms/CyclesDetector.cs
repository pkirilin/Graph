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
    public class CyclesDetector<TGraph, TVertex> : ICyclesDetector<TGraph, TVertex>
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
            var visitedEdges = new HashSet<Edge<TVertex>>();

            while (verticesForNextVisit.Any())
            {
                var curVertex = verticesForNextVisit.Pop();

                // If current vertex has already been visited and is connected with any visited one, graph contains cycle
                if (visitedVertices.Contains(curVertex) && IsEdgeToVisitedVertexExists(graph, curVertex, visitedVertices))
                    return true;

                foreach (var connectedVertex in graph.AdjacencyLists[curVertex])
                {
                    var edge = new Edge<TVertex>(curVertex, connectedVertex);

                    if (!visitedEdges.Contains(edge))
                    {
                        verticesForNextVisit.Push(connectedVertex);

                        // For undirected graph back edge should be excluded from cycle detection
                        // (e.g. path [A -> B, B -> A] is not a cycle in terms of undirected graphs)
                        if (graph is IUndirectedGraph<TVertex>)
                            visitedEdges.Add(new Edge<TVertex>(connectedVertex, curVertex));
                    }
                }

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
        /// <returns>True if edge exists, false if not</returns>
        private bool IsEdgeToVisitedVertexExists(TGraph graph, TVertex curVertex, ISet<TVertex> visitedVertices)
        {
            return graph.AdjacencyLists[curVertex].Any(v => visitedVertices.Contains(v));
        }
    }
}
