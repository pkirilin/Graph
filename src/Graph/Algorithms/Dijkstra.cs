using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Dijkstra algorithm implementation.
    /// Searches shortest distances in weighted graph between one particular vertex and all other vertices.
    /// The result of algorithm execution is a dictionary where keys are graph vertices
    /// and values are shortest distances to corresponding key vertex.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class Dijkstra<TGraph, TVertex> : IDijkstra<TGraph, TVertex>
        where TGraph : WeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
        public IDictionary<TVertex, int> Execute(TGraph graph, TVertex initialVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (initialVertex == null)
                throw new ArgumentNullException(nameof(initialVertex));
            if (!graph.AdjacencyLists.ContainsKey(initialVertex))
                throw new ArgumentException($"Initial vertex = '{initialVertex}' doesn't exist in graph", nameof(initialVertex));
            if (graph.Weights.Any(w => w.Value < 0))
                throw new InvalidOperationException("For Dijkstra algorithm graph cannot contain negative weights");

            var shortestDistances = InitShortestDistances(graph, initialVertex);
            var visitedVertices = new HashSet<TVertex>(new GraphVertexEqualityComparer<TVertex>());

            while (TryFindNextVertex(shortestDistances, visitedVertices, out var curVertex))
            {
                var notVisitedNeighbourVertices = graph.AdjacencyLists[curVertex]
                    .Where(v => !visitedVertices.Contains(v));

                foreach (var neighbourVertex in notVisitedNeighbourVertices)
                {
                    var distanceToCurrentVertex = shortestDistances[curVertex];
                    var distanceToNeighbourVertex = shortestDistances[neighbourVertex];
                    var currentEdgeWeight = graph.Weights[new Edge<TVertex>(curVertex, neighbourVertex)];

                    if (distanceToCurrentVertex + currentEdgeWeight < distanceToNeighbourVertex)
                        shortestDistances[neighbourVertex] = distanceToCurrentVertex + currentEdgeWeight;
                }

                visitedVertices.Add(curVertex);
            }

            return shortestDistances;
        }

        /// <summary>
        /// Initializes a dictionary containing distances to each graph vertex, where
        /// distance to initial vertex is 0 and distances to other vertices are "infinite" (have a very large value)
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="initialVertex">Initial vertex</param>
        /// <returns>New distances dictionary</returns>
        private IDictionary<TVertex, int> InitShortestDistances(TGraph graph, TVertex initialVertex)
        {
            return graph.Vertices.ToDictionary(
                v => v,
                v => v.CompareTo(initialVertex) == 0 ? 0 : Int32.MaxValue
            );
        }

        /// <summary>
        /// Searches for available vertex for next Dijkstra algorithm iteration.
        /// This must be a reachable vertex that is not visited yet
        /// </summary>
        /// <param name="shortestDistances">Shortest distances dictionary</param>
        /// <param name="visitedVertices">Visited vertices set</param>
        /// <param name="nextVertex">A vertex for next Dijkstra algorithm iteration</param>
        /// <returns>True if vertex is found, false - if not</returns>
        private bool TryFindNextVertex(IDictionary<TVertex, int> shortestDistances, ISet<TVertex> visitedVertices, out TVertex nextVertex)
        {
            nextVertex = default;

            var reachableNotVisitedDistances = shortestDistances.Where(p =>
                p.Value != Int32.MaxValue && !visitedVertices.Contains(p.Key));

            if (!reachableNotVisitedDistances.Any())
                return false;

            var minDistance = Int32.MaxValue;

            foreach (var pathEntry in reachableNotVisitedDistances)
            {
                if (pathEntry.Value < minDistance)
                {
                    minDistance = pathEntry.Value;
                    nextVertex = pathEntry.Key;
                }
            }

            return true;
        }
    }
}
