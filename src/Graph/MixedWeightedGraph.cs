using Graph.Abstractions;
using Graph.Structures;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents a mixed weighted graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    /// <typeparam name="TWeight">Graph edge weight type</typeparam>
    public class MixedWeightedGraph<TVertex, TWeight> : WeightedGraph<TVertex, TWeight>,
        IDirectedWeightedGraph<TVertex, TWeight>,
        IUndirectedWeightedGraph<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Initializes an empty mixed weighted graph with no vertices and edges
        /// </summary>
        public MixedWeightedGraph()
        {
        }

        /// <summary>
        /// Initializes a mixed weighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public MixedWeightedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes a mixed unweighted graph with vertices and edges with default weight values
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="directedEdges">Initial directed edges represented as a pair of vertices</param>
        /// <param name="undirectedEdges">Initial undirected edges represented as a pair of vertices</param>
        public MixedWeightedGraph(
            IEnumerable<TVertex> vertices,
            IEnumerable<Edge<TVertex>> directedEdges,
            IEnumerable<Edge<TVertex>> undirectedEdges) : this(vertices)
        {
            foreach (var edge in directedEdges)
                AddDirectedEdge(edge.Source, edge.Destination);
            foreach (var edge in undirectedEdges)
                AddUndirectedEdge(edge.Source, edge.Destination);
        }

        /// <summary>
        /// Initializes a mixed unweighted graph with vertices, edges and edge values
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="directedEdges">Initial directed edges represented as a pair of vertices</param>
        /// <param name="undirectedEdges">Initial undirected edges represented as a pair of vertices</param>
        /// <param name="directedWeights">Initial weight values for directed edges</param>
        /// <param name="undirectedWeights">Initial weight values for undirected edges</param>
        public MixedWeightedGraph(
            IEnumerable<TVertex> vertices,
            IEnumerable<Edge<TVertex>> directedEdges,
            IEnumerable<Edge<TVertex>> undirectedEdges,
            IDictionary<Edge<TVertex>, TWeight> directedWeights,
            IDictionary<Edge<TVertex>, TWeight> undirectedWeights) : this(vertices, directedEdges, undirectedEdges)
        {
            foreach (var weight in directedWeights)
            {
                var source = weight.Key.Source;
                var destination = weight.Key.Destination;
                InitWeight(source, destination, weight.Value);
            }

            foreach (var weight in undirectedWeights)
            {
                var source = weight.Key.Source;
                var destination = weight.Key.Destination;
                InitWeight(source, destination, weight.Value);
                InitWeight(destination, source, weight.Value);
            }
        }

        #region IDirectedWeightedGraph<TVertex, TWeight>

        public void AddDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            ForbidLoop(source, destination);
            ForbidMultipleEdge(source, destination);

            _adjacencyLists[source].Add(destination);
            _weights.Add(new Edge<TVertex>(source, destination), default);
        }

        public void AddDirectedEdge(TVertex source, TVertex destination, TWeight weight)
        {
            AddDirectedEdge(source, destination);
            _weights[new Edge<TVertex>(source, destination)] = weight;
        }

        public void RemoveDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);

            _weights.Remove(new Edge<TVertex>(source, destination));
            _adjacencyLists[source].Remove(destination);
        }

        #endregion

        #region IUndirectedWeightedGraph<TVertex, TWeight>

        public void AddUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            ForbidLoop(source, destination);
            ForbidMultipleEdge(source, destination);
            ForbidMultipleEdge(destination, source);

            _adjacencyLists[source].Add(destination);
            _adjacencyLists[destination].Add(source);
            _weights.Add(new Edge<TVertex>(source, destination), default);
            _weights.Add(new Edge<TVertex>(destination, source), default);
        }

        public void AddUndirectedEdge(TVertex source, TVertex destination, TWeight weight)
        {
            AddUndirectedEdge(source, destination);
            _weights[new Edge<TVertex>(source, destination)] = weight;
            _weights[new Edge<TVertex>(destination, source)] = weight;
        }

        public void RemoveUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);

            _weights.Remove(new Edge<TVertex>(source, destination));
            _weights.Remove(new Edge<TVertex>(destination, source));
            _adjacencyLists[source].Remove(destination);
            _adjacencyLists[destination].Remove(source);
        }

        #endregion
    }
}
