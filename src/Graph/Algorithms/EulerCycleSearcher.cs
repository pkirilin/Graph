using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// An algorithm for finding a single euler cycle in graph.
    /// Accepts target graph and initial vertex as a starting point for searching euler cycle.
    /// Returns a sequence of vertices, through which first found euler cycle passes.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class EulerCycleSearcher<TGraph, TVertex> : IEulerCycleSearcher<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IConnectedComponentsCounter<TGraph, TVertex> _connectedComponentsCounter;

        public EulerCycleSearcher(IConnectedComponentsCounter<TGraph, TVertex> connectedComponentsCounter)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
        }

        public IEnumerable<TVertex> Execute(TGraph graph, TVertex initialVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (!graph.Vertices.Contains(initialVertex))
                throw new ArgumentException(nameof(initialVertex));
            if (!IsEulerGraph(graph))
                throw new InvalidOperationException("Target graph cannot contain any euler cycle");

            // The main idea is to use two stacks. First one is for remembering visited vertices.
            // Visited edges are remembered too. On each iteration, if there's a vertex on top of
            // the source stack that has all its edges visited, it is moved to destination stack.
            // Eventually, the destination stack will contain vertices through which an euler cycle passes.
            var sourceStack = new Stack<TVertex>(new TVertex[] { initialVertex });
            var destinationStack = new Stack<TVertex>();
            var visitedEdges = new HashSet<Edge<TVertex>>();

            while (sourceStack.Any())
            {
                var curVertex = sourceStack.Peek();
                var notVisitedEdgesOfCurVertex = graph.AdjacencyLists[curVertex]
                    .Select(v => new Edge<TVertex>(curVertex, v))
                    .Where(e => !visitedEdges.Contains(e));

                if (notVisitedEdgesOfCurVertex.Any())
                {
                    var notVisitedEdge = notVisitedEdgesOfCurVertex.First();
                    sourceStack.Push(notVisitedEdge.Destination);
                    visitedEdges.Add(notVisitedEdge);
                    // For undirected graph reverse edge is also marked as visited
                    visitedEdges.Add(new Edge<TVertex>(notVisitedEdge.Destination, notVisitedEdge.Source));
                }
                else
                {
                    sourceStack.Pop();
                    destinationStack.Push(curVertex);
                }
            }

            return destinationStack.AsEnumerable();
        }

        /// <summary>
        /// Checks whether target graph can contain euler cycles
        /// </summary>
        /// <param name="graph">Target graph</param>
        private bool IsEulerGraph(TGraph graph)
        {
            var isAnyOddVertexDeg = graph.Vertices.Any(v => graph.GetVertexDeg(v) % 2 != 0);
            var connectedComponentsCount = _connectedComponentsCounter.Execute(graph);

            return !isAnyOddVertexDeg && connectedComponentsCount < 2;
        }
    }
}
