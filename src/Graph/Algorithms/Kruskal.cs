using Graph.Abstractions.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Kruskal's algorithm implementation.
    /// Finds graph's spanning tree of minimal weight.
    /// The result of algorithm execution is a collection of spanning tree edges.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class Kruskal<TGraph, TVertex> : ISpanningTreeSearcher<TGraph, TVertex>
        where TGraph : UndirectedWeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
        private readonly IConnectedComponentsCounter<TGraph, TVertex> _connectedComponentsCounter;
        private readonly ICyclesDetector<TGraph, TVertex> _cyclesDetector;

        public Kruskal(IConnectedComponentsCounter<TGraph, TVertex> connectedComponentsCounter, ICyclesDetector<TGraph, TVertex> cyclesDetector)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
            _cyclesDetector = cyclesDetector ?? throw new ArgumentNullException(nameof(cyclesDetector));
        }

        public IEnumerable<Edge<TVertex>> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var connectedComponentsCount = _connectedComponentsCounter.Execute(graph);

            if (connectedComponentsCount > 1)
                throw new InvalidOperationException("For Kruskal's algorithm graph cannot contain more than one connected component");

            var edges = graph.ReducedEdges;
            var sortedEdgeWeightEntries = graph.Weights.Where(w => edges.Contains(w.Key))
                .OrderBy(w => w.Value)
                .ToList();

            var graphCopy = new UndirectedWeightedGraph<TVertex, int>(graph.Vertices) as TGraph;
            var spanningTreeEdges = new List<Edge<TVertex>>();

            foreach (var edgeWeightEntry in sortedEdgeWeightEntries)
            {
                var edge = edgeWeightEntry.Key;
                var weight = edgeWeightEntry.Value;

                graphCopy.AddUndirectedEdge(edge.Source, edge.Destination, weight);

                if (IsAnyCycleDetected(graphCopy))
                    graphCopy.RemoveUndirectedEdge(edge.Source, edge.Destination);
                else
                    spanningTreeEdges.Add(new Edge<TVertex>(edge.Source, edge.Destination));
            }

            return spanningTreeEdges;
        }

        /// <summary>
        /// Determines whether target graph contains at least one cycle
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <returns>True if target graph contains at least one cycle, false if not</returns>
        private bool IsAnyCycleDetected(TGraph graph)
        {
            return graph.Vertices.Any(v => _cyclesDetector.Execute(graph, v));
        }
    }
}
