using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Lee's wave algorithm implementation.
    /// Algorithm finds shortest path (counted as edges number) between two specified graph vertices.
    /// It is also commonly used for navigating the maze (if the maze can be represented as graph).
    /// Returns a sequence of vertices, through which first found shortest path passes from start to end.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class Lee<TGraph, TVertex> : IMazeNavigator<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IConnectedComponentsCounter<TGraph, TVertex> _connectedComponentsCounter;
        private readonly IEqualityComparer<TVertex> _verticesComparer;

        public Lee(IConnectedComponentsCounter<TGraph, TVertex> connectedComponentsCounter)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
            _verticesComparer = new GraphVertexEqualityComparer<TVertex>();
        }

        public IEnumerable<TVertex> Execute(TGraph graph, TVertex startVertex, TVertex endVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (startVertex == null)
                throw new ArgumentNullException(nameof(startVertex));
            if (endVertex == null)
                throw new ArgumentNullException(nameof(endVertex));
            if (!graph.AdjacencyLists.ContainsKey(startVertex))
                throw new ArgumentException($"Start vertex = '{startVertex}' doesn't exist in graph");
            if (!graph.AdjacencyLists.ContainsKey(endVertex))
                throw new ArgumentException($"End vertex = '{endVertex}' doesn't exist in graph");
            if (_connectedComponentsCounter.Execute(graph) > 1)
                throw new InvalidOperationException("For Lee algorithm graph should be highly connected");

            #region Algorithm description

            // The algorithm is using the "waves" term.
            // Wave is an integer value describing the number of edges needed to go from one vertex to another.
            // Start vertex has the wave value of 0.

            // The algorithm is divided into 2 stages:

            // 1. Calculating the waves
            // Each vertex (starting from initial one) is viewed.
            // For each its neighbour, that has unset wave value, wave is calculated as current vertex's wave + 1.

            // 2. Finding the path itself
            // To find the path from start to end vertex itself, graph traversal is started from end vertex,
            // passing through the vertices in which the value of the wave decreases by 1 until it reaches start vertex.

            #endregion

            var waves = CalculateWaves(graph, startVertex);
            var shortestPath = FindShortestPath(graph, startVertex, endVertex, waves);

            return shortestPath;
        }

        /// <summary>
        /// Calculates the wave values for all graph vertices
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="startVertex">Start vertex</param>
        /// <returns>A dictionary where keys are graph vertices and values are calculated wave values</returns>
        private IDictionary<TVertex, int?> CalculateWaves(TGraph graph, TVertex startVertex)
        {
            var waves = graph.Vertices.ToDictionary(v => v, v => new int?(), _verticesComparer);
            var queuedVertices = new Queue<TVertex>(new TVertex[] { startVertex });
            waves[startVertex] = 0;

            while (queuedVertices.Any())
            {
                var curVertex = queuedVertices.Dequeue();
                var nextWave = waves[curVertex] + 1;

                foreach (var neighbourVertex in graph.AdjacencyLists[curVertex])
                {
                    if (!waves[neighbourVertex].HasValue)
                    {
                        queuedVertices.Enqueue(neighbourVertex);
                        waves[neighbourVertex] = nextWave;
                    }
                }
            }

            return waves;
        }

        /// <summary>
        /// Finds first (in lexicographical order) shortest path between start and end graph vertices, using wave values
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="startVertex">Start vertex</param>
        /// <param name="endVertex">End vertex</param>
        /// <param name="waves">Wave values dictionary</param>
        /// <returns>A sequence of vertices, through which found shortest path passes from start to end</returns>
        private IEnumerable<TVertex> FindShortestPath(TGraph graph, TVertex startVertex, TVertex endVertex, IDictionary<TVertex, int?> waves)
        {
            var shortestPath = new Stack<TVertex>(new TVertex[] { endVertex });
            var curVertex = endVertex;

            while (curVertex.CompareTo(startVertex) != 0)
            {
                curVertex = graph.AdjacencyLists[curVertex]
                    .Where(v => waves[v] == waves[curVertex] - 1)
                    .First();
                shortestPath.Push(curVertex);
            }

            return shortestPath;
        }
    }
}
