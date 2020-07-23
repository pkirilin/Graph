using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// An algorithm for searching articulation points in graph.
    /// An articulation point is a vertex, with the removal of which the number of graph's connected components increases.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class ArticulationPointSearcher<TGraph, TVertex> : IArticulationPointSearcher<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IConnectedComponentsCounter<TGraph, TVertex> _connectedComponentsCounter;

        public ArticulationPointSearcher(IConnectedComponentsCounter<TGraph, TVertex> connectedComponentsCounter)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
        }

        public IEnumerable<TVertex> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (_connectedComponentsCounter.Execute(graph) > 1)
                throw new InvalidOperationException("Target graph should be highly connected for searching articulation points in it");

            var foundArticulationPoints = new List<TVertex>();

            // Cloning graph to save the original one
            var graphClone = new UndirectedGraph<TVertex>(graph.Vertices, graph.ReducedEdges) as TGraph;
            var graphVertices = graphClone.Vertices.ToList();

            foreach (var vertex in graphVertices)
            {
                var sourceEdges = graphClone.Edges.Where(e => e.Source.CompareTo(vertex) == 0);
                var destinationEdges = graphClone.Edges.Where(e => e.Destination.CompareTo(vertex) == 0);
                graphClone.RemoveVertex(vertex);

                if (_connectedComponentsCounter.Execute(graphClone) > 1)
                    foundArticulationPoints.Add(vertex);

                graphClone.AddVertex(vertex);
                foreach (var edge in sourceEdges)
                    graphClone.AddUndirectedEdge(edge.Source, edge.Destination);
            }

            return foundArticulationPoints;
        }
    }
}
