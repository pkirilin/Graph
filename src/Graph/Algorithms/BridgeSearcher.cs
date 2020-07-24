using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// An algorithm for searching bridges in graph.
    /// A bridge is an edge, with the removal of which the number of graph's connected components increases.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class BridgeSearcher<TGraph, TVertex> : IBridgeSearcher<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IConnectedComponentsCounter<TGraph, TVertex> _connectedComponentsCounter;
        private readonly IEqualityComparer<TVertex> _verticesComparer;

        public BridgeSearcher(IConnectedComponentsCounter<TGraph, TVertex> connectedComponentsCounter)
        {
            _connectedComponentsCounter = connectedComponentsCounter ?? throw new ArgumentNullException(nameof(connectedComponentsCounter));
            _verticesComparer = new GraphVertexEqualityComparer<TVertex>();
        }

        public IEnumerable<Edge<TVertex>> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (_connectedComponentsCounter.Execute(graph) > 1)
                throw new InvalidOperationException("Target graph should be highly connected for searching bridges in it");
            if (!graph.Vertices.Any())
                return Enumerable.Empty<Edge<TVertex>>();

            #region Algorithm description

            // The algorithm is divided into 3 stages:

            // 1. Preparing graph for searching edge double-connectivity components.
            // Starts the depth-first search from an initial vertex.
            // In the process of walking along the edge [A -> B], it is "oriented" against the direction of movement.
            // When a new (unvisited) vertex is found, it is put at the end of the queue.

            // 2. Finding edge double-connectivity components.
            // The loop is started until the queue is empty.
            // The first vertex from the queue is retrieved, and if this vertex is unvisited, then depth-first search is started from this vertex.
            // In this case, the next component of the edge double-connectivity is formed.
            // As a result, when the queue is empty, components of edge double-connectivity will be formed.

            // 3. Finding those edges that connect vertices of different edge double-connectivity components.
            // Those edges are bridges.

            #endregion

            var initialVertex = graph.Vertices.First();
            var graphClone = PrepareToSearchForEdgeDoubleConnectivityComponents(graph, initialVertex, out var passedVertices);
            var edgeDoubleConnectivityComponents = FindEdgeDoubleConnectivityComponents(graphClone, passedVertices);
            var bridges = FindBridges(graphClone, edgeDoubleConnectivityComponents);

            return bridges;
        }

        /// <summary>
        /// Starts depth-first search from an initial vertex, orienting each visited edge against its direction
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="initialVertex">Initial vertex for DFS</param>
        /// <param name="passedVertices">Passed vertices queue</param>
        /// <returns>Modified (mixed) graph with each visited edge oriented against its direction</returns>
        private MixedGraph<TVertex> PrepareToSearchForEdgeDoubleConnectivityComponents(TGraph graph, TVertex initialVertex, out Queue<TVertex> passedVertices)
        {
            var graphClone = new MixedGraph<TVertex>(
                graph.Vertices,
                Enumerable.Empty<Edge<TVertex>>(),
                graph.ReducedEdges);
            var visitedVertices = new HashSet<TVertex>(new TVertex[] { initialVertex }, _verticesComparer);

            DfsGraphAndRemoveVisitedEdges(graphClone, initialVertex, visitedVertices);
            passedVertices = new Queue<TVertex>(visitedVertices);
            visitedVertices.Clear();
            return graphClone;
        }

        /// <summary>
        /// Finds edge double-connectivity components for graph
        /// </summary>
        /// <param name="graphClone">Modified graph from previous stage</param>
        /// <param name="passedVertices">Passed vertices queue from previous stage</param>
        /// <returns>Dictionary where keys are vertices and values are edge double-connectivity component's id, which corresponding key vertex belongs to
        /// </returns>
        private IDictionary<TVertex, int?> FindEdgeDoubleConnectivityComponents(MixedGraph<TVertex> graphClone, Queue<TVertex> passedVertices)
        {
            var curEdgeDoubleConnectivityComponentId = 0;
            var edgeDoubleConnectivityComponents = graphClone.Vertices.ToDictionary(v => v, v => new int?());

            while (passedVertices.Any())
            {
                var curVertex = passedVertices.Dequeue();

                if (edgeDoubleConnectivityComponents[curVertex].HasValue)
                    continue;

                var visitedVertices = new HashSet<TVertex>(new TVertex[] { curVertex }, _verticesComparer);
                edgeDoubleConnectivityComponents[curVertex] = curEdgeDoubleConnectivityComponentId;
                DfsEdgeDoubleConnectivityComponent(graphClone, curVertex, visitedVertices, edgeDoubleConnectivityComponents, curEdgeDoubleConnectivityComponentId);
                curEdgeDoubleConnectivityComponentId++;
            }

            return edgeDoubleConnectivityComponents;
        }

        /// <summary>
        /// Finds edges that connect vertices of different edge double-connectivity components
        /// </summary>
        /// <param name="graphClone">Modified graph</param>
        /// <param name="edgeDoubleConnectivityComponents">Vertex to edge double-connectivity component mapping dictionary</param>
        /// <returns>Edges that connect vertices of different edge double-connectivity components</returns>
        private IEnumerable<Edge<TVertex>> FindBridges(MixedGraph<TVertex> graphClone, IDictionary<TVertex, int?> edgeDoubleConnectivityComponents)
        {
            var bridges = new List<Edge<TVertex>>();

            foreach (var edge in graphClone.ReducedEdges)
            {
                var sourceComponentId = edgeDoubleConnectivityComponents[edge.Source];
                var destinationComponentId = edgeDoubleConnectivityComponents[edge.Destination];

                if (sourceComponentId != destinationComponentId)
                    bridges.Add(edge);
            }

            return bridges;
        }

        #region Specific DFS methods

        /// <summary>
        /// Performs depth-first search on graph, orienting each visited edge against its direction
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="sourceVertex">Current vertex</param>
        /// <param name="visitedVertices">Visited vertices</param>
        private void DfsGraphAndRemoveVisitedEdges(MixedGraph<TVertex> graph, TVertex sourceVertex, ISet<TVertex> visitedVertices)
        {
            var notVisitedNeighbourVertices = graph.AdjacencyLists[sourceVertex].ToList();

            foreach (var neighbourVertex in notVisitedNeighbourVertices)
            {
                if (!visitedVertices.Contains(neighbourVertex))
                {
                    visitedVertices.Add(neighbourVertex);
                    graph.RemoveDirectedEdge(sourceVertex, neighbourVertex);
                    DfsGraphAndRemoveVisitedEdges(graph, neighbourVertex, visitedVertices);
                }
            }
        }

        /// <summary>
        /// Performs depth-first search on single graph's edge double-connectivity component and marks this component's vertices with component's id
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="sourceVertex">Current vertex</param>
        /// <param name="visitedVertices">Visited vertices</param>
        /// <param name="edgeDoubleConnectivityComponents">Vertex to edge double-connectivity component mapping dictionary</param>
        /// <param name="curEdgeDoubleConnectivityComponentId">Current edge double-connectivity component's ID</param>
        private void DfsEdgeDoubleConnectivityComponent(MixedGraph<TVertex> graph, TVertex sourceVertex, ISet<TVertex> visitedVertices, IDictionary<TVertex, int?> edgeDoubleConnectivityComponents, int curEdgeDoubleConnectivityComponentId)
        {
            var notVisitedNeighbourVertices = graph.AdjacencyLists[sourceVertex].ToList();

            foreach (var neighbourVertex in notVisitedNeighbourVertices)
            {
                if (!visitedVertices.Contains(neighbourVertex) && !edgeDoubleConnectivityComponents[neighbourVertex].HasValue)
                {
                    visitedVertices.Add(neighbourVertex);
                    edgeDoubleConnectivityComponents[neighbourVertex] = curEdgeDoubleConnectivityComponentId;
                    DfsEdgeDoubleConnectivityComponent(graph, neighbourVertex, visitedVertices, edgeDoubleConnectivityComponents, curEdgeDoubleConnectivityComponentId);
                }
            }
        }

        #endregion
    }
}
