using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Topological sotring algorithm implementation.
    /// Topological sorting is a way of numbering the vertices of a directed graph,
    /// in which each edge leads from a vertex with a lower number to a vertex with a higher number.
    /// Another formulation of topological sorting is to arrange the vertices of the given graph
    /// on a horizontal line so that all the edges of the graph go from left to right.
    /// Algorithm accepts initial vertex and returns sorted vertices collection.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class TopologicalSorter<TGraph, TVertex> : ITopologicalSorter<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>, IDirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly ICyclesDetector<TGraph, TVertex> _cyclesDetector;

        public TopologicalSorter(ICyclesDetector<TGraph, TVertex> cyclesDetector)
        {
            _cyclesDetector = cyclesDetector ?? throw new ArgumentNullException(nameof(cyclesDetector));
        }

        public IEnumerable<TVertex> Execute(TGraph graph, TVertex initialVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (!graph.Vertices.Contains(initialVertex))
                throw new ArgumentException(nameof(initialVertex));
            if (IsAnyCycleDetected(graph))
                throw new InvalidOperationException("For topological sorting target graph should not contain any cycle, but it does");

            var verticesComparer = new GraphVertexEqualityComparer<TVertex>();
            var notVisitedVertices = new HashSet<TVertex>(graph.Vertices.ToList(), verticesComparer);
            var queuedVertices = new HashSet<TVertex>(verticesComparer);
            var verticesForNextVisit = new Stack<TVertex>(new TVertex[] { initialVertex });
            var sortedVertices = new Stack<TVertex>();

            // Algorithm runs a customized Depth-first search from initial vertex and tries to visit all reachable vertices.
            // As soon as it turns out that a vertex is a dead-end (has no unvisited neighbours), then it is pushed onto the top of the result stack.
            // As a result, all vertices become dead ends and are pushed onto the stack.
            // Eventually, the result stack will contain sorted vertices (from top to bottom).

            while (TryFindNextVertex(verticesForNextVisit, notVisitedVertices, out var curVertex))
            {
                var notVisitedNeighbours = graph.AdjacencyLists[curVertex]
                    .Where(v => notVisitedVertices.Contains(v) && !queuedVertices.Contains(v));

                if (!notVisitedNeighbours.Any())
                {
                    verticesForNextVisit.Pop();
                    notVisitedVertices.Remove(curVertex);
                    sortedVertices.Push(curVertex);
                    continue;
                }

                foreach (var neighbourVertex in notVisitedNeighbours)
                {
                    verticesForNextVisit.Push(neighbourVertex);
                    queuedVertices.Add(neighbourVertex);
                }
            }

            return sortedVertices.AsEnumerable();
        }

        /// <summary>
        /// Checks if target graph contains any cycle
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <returns>True if cycle is found, false if not</returns>
        private bool IsAnyCycleDetected(TGraph graph)
        {
            return graph.Vertices.Any(v => _cyclesDetector.Execute(graph, v));
        }

        /// <summary>
        /// Searches for a vertex to be used as current on next topological sorting iteration 
        /// </summary>
        /// <param name="verticesForNextVisit"></param>
        /// <param name="notVisitedVertices"></param>
        /// <param name="nextVertex"></param>
        /// <returns>True if vertex is found, false if not</returns>
        private bool TryFindNextVertex(Stack<TVertex> verticesForNextVisit, ISet<TVertex> notVisitedVertices, out TVertex nextVertex)
        {
            if (verticesForNextVisit.Any())
            {
                nextVertex = verticesForNextVisit.Peek();
                return true;
            }

            if (notVisitedVertices.Any())
            {
                verticesForNextVisit.Push(notVisitedVertices.First());
                nextVertex = notVisitedVertices.First();
                return true;
            }

            nextVertex = default;
            return false;
        }
    }
}
