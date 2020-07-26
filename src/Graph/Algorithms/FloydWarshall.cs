using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Floyd-Warshall's algorithm implementation.
    /// Searches shortest distances in weighted graph between each pair of vertices.
    /// The result of algorithm execution is a shortest distances martix.
    /// Actually, this is not a matrix, but dictionary of dictionaries (to support not only non-negative integers as a vertex type).
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class FloydWarshall<TGraph, TVertex> : IShortestDistancesSearcher<TGraph, TVertex>
        where TGraph : WeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
        public IDictionary<TVertex, IDictionary<TVertex, int>> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var distances = InitDistances(graph);

            foreach (var curVertex in graph.Vertices)
            {
                foreach (var source in distances.Keys.ToList())
                {
                    foreach (var destination in distances[source].Keys.ToList())
                    {
                        if (source.CompareTo(curVertex) != 0 && destination.CompareTo(curVertex) != 0)
                        {
                            var distFromCurToDest = distances[curVertex][destination];
                            var distFromSrcToCur = distances[source][curVertex];
                            var curDistance = distFromCurToDest + distFromSrcToCur;

                            if (distFromCurToDest != Int32.MaxValue && distFromSrcToCur != Int32.MaxValue && curDistance < distances[source][destination])
                                distances[source][destination] = curDistance;
                        }
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// Initializes shortest distances matrix
        /// </summary>
        /// <param name="graph">Target graph</param>
        private IDictionary<TVertex, IDictionary<TVertex, int>> InitDistances(TGraph graph)
        {
            var distances = new Dictionary<TVertex, IDictionary<TVertex, int>>();

            foreach (var sourceVertex in graph.Vertices)
            {
                distances.Add(sourceVertex, new Dictionary<TVertex, int>());

                foreach (var destinationVertex in graph.Vertices)
                {
                    var edge = new Edge<TVertex>(sourceVertex, destinationVertex);
                    var dist = GetInitialDistance(graph, edge);
                    distances[sourceVertex].Add(destinationVertex, dist);
                }
            }

            return distances;
        }

        /// <summary>
        /// Gets initial distance for distances matrix
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="edge">Target graph edge representing 'matrix[source][destination]' entry</param>
        /// <returns>Initial distance value</returns>
        private int GetInitialDistance(TGraph graph, Edge<TVertex> edge)
        {
            if (graph.Weights.ContainsKey(edge))
                return graph.Weights[edge];
            if (edge.Source.CompareTo(edge.Destination) == 0)
                return 0;
            return Int32.MaxValue;
        }
    }
}
