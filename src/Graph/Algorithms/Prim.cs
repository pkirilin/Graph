using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Prim's algorithm implementation.
    /// Finds graph's spanning tree of minimal weight.
    /// The result of algorithm execution is a collection of spanning tree edges.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class Prim<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IEnumerable<Edge<TVertex>>>
        where TGraph : WeightedGraph<TVertex, int>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IFunctionAlgorithm<TGraph, TVertex, int> _connectedComponentsCounter;

        public Prim(IFunctionAlgorithm<TGraph, TVertex, int> connectedComponentsCounter)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
        }

        public IEnumerable<Edge<TVertex>> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var connectedComponentsCount = _connectedComponentsCounter.Execute(graph);

            if (connectedComponentsCount > 1)
                throw new InvalidOperationException("For Prim's algorithm graph cannot contain more than one connected component");
            if (!graph.Vertices.Any() || !graph.Weights.Any())
                return Enumerable.Empty<Edge<TVertex>>();

            var initialConsideredVertices = new List<TVertex>() { graph.Vertices.First() };
            var verticesComparer = new GraphVertexEqualityComparer<TVertex>();
            // Vertices that are already included in spanning tree
            var consideredVertices = new HashSet<TVertex>(initialConsideredVertices, verticesComparer);
            // Edges that are already included in spanning tree
            var spanningTreeEdges = new HashSet<Edge<TVertex>>();

            while (graph.Vertices.Any(v => !consideredVertices.Contains(v)))
            {
                // For each considered vertex algorithm tries to find an edge of minimal weight
                // which is not included in spanning tree and doesn't create cycle
                var minWeight = Int32.MaxValue;
                var edgeToIncludeToSpanningTree = default(Edge<TVertex>);

                foreach (var sourceVertex in consideredVertices)
                {
                    foreach (var destinationVertex in graph.AdjacencyLists[sourceVertex])
                    {
                        var edge = new Edge<TVertex>(sourceVertex, destinationVertex);
                        var weight = graph.Weights[edge];

                        if (IsEdgeValid(edge, consideredVertices, spanningTreeEdges) && weight < minWeight)
                        {
                            minWeight = weight;
                            edgeToIncludeToSpanningTree = edge;
                        }
                    }
                }

                consideredVertices.Add(edgeToIncludeToSpanningTree.Destination);
                spanningTreeEdges.Add(edgeToIncludeToSpanningTree);
            }

            return spanningTreeEdges;
        }

        /// <summary>
        /// Determines whether graph edge can be included in spanning tree.
        /// Graph edge can be included in spanning tree if it hasn't been included yet
        /// or it doesn't create cycle (doesn't connect both considered vertices).
        /// </summary>
        /// <param name="edge">Target edge</param>
        /// <param name="consideredVertices">Vertices that are already included in spanning tree</param>
        /// <param name="spanningTreeEdges">Edges that are already included in spanning tree</param>
        private bool IsEdgeValid(Edge<TVertex> edge, ISet<TVertex> consideredVertices, ISet<Edge<TVertex>> spanningTreeEdges)
        {
            var containsSource = consideredVertices.Any(v => v.CompareTo(edge.Source) == 0);
            var containsDestination = consideredVertices.Any(v => v.CompareTo(edge.Destination) == 0);
            return !spanningTreeEdges.Contains(edge) && (containsSource != containsDestination);
        }
    }
}
