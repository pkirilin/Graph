using Graph.Abstractions;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents an undirected weighted graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    /// <typeparam name="TWeight">Graph edge weight type</typeparam>
    public class UndirectedWeightedGraph<TVertex, TWeight> : WeightedGraph<TVertex, TWeight>, IUndirectedWeightedGraph<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Initializes an empty undirected weighted graph with no vertices and edges
        /// </summary>
        public UndirectedWeightedGraph()
        {
        }

        /// <summary>
        /// Initializes an undirected weighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public UndirectedWeightedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes an undirected weighted graph with vertices and edges with default weight values
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        public UndirectedWeightedGraph(IEnumerable<TVertex> vertices, IEnumerable<KeyValuePair<TVertex, TVertex>> edges) : this(vertices)
        {
            foreach (var edge in edges)
                AddUndirectedEdge(edge.Key, edge.Value);
        }

        /// <summary>
        /// Initializes an undirected weighted graph with vertices, edges and edge weights
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        /// <param name="weights">Initial edge weights</param>
        public UndirectedWeightedGraph(
            IEnumerable<TVertex> vertices,
            IEnumerable<KeyValuePair<TVertex, TVertex>> edges,
            IDictionary<KeyValuePair<TVertex, TVertex>, TWeight> weights) : this(vertices, edges)
        {
            foreach (var weight in weights)
            {
                var source = weight.Key.Key;
                var destination = weight.Key.Value;
                InitWeight(source, destination, weight.Value);
                InitWeight(destination, source, weight.Value);
            }
        }

        #region IUndirectedWeightedGraph<TVertex, TWeight>

        public void AddUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            ForbidLoop(source, destination);
            ForbidMultipleEdge(source, destination);
            ForbidMultipleEdge(destination, source);

            _adjacencyLists[source].Add(destination);
            _adjacencyLists[destination].Add(source);
            _weights.Add(new KeyValuePair<TVertex, TVertex>(source, destination), default);
            _weights.Add(new KeyValuePair<TVertex, TVertex>(destination, source), default);
        }

        public void AddUndirectedEdge(TVertex source, TVertex destination, TWeight weight)
        {
            AddUndirectedEdge(source, destination);
            _weights[new KeyValuePair<TVertex, TVertex>(source, destination)] = weight;
            _weights[new KeyValuePair<TVertex, TVertex>(destination, source)] = weight;
        }

        public void RemoveUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);

            _weights.Remove(new KeyValuePair<TVertex, TVertex>(source, destination));
            _weights.Remove(new KeyValuePair<TVertex, TVertex>(destination, source));
            _adjacencyLists[source].Remove(destination);
            _adjacencyLists[destination].Remove(source);
        }

        #endregion
    }
}
