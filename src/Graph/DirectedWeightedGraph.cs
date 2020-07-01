﻿using Graph.Abstractions;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents a directed weighted graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    /// <typeparam name="TWeight">Graph edge weight type</typeparam>
    public class DirectedWeightedGraph<TVertex, TWeight> : WeightedGraph<TVertex, TWeight>, IDirectedWeightedGraph<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Initializes an empty directed weighted graph with no vertices and edges
        /// </summary>
        public DirectedWeightedGraph()
        {
        }

        /// <summary>
        /// Initializes a directed weighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public DirectedWeightedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes a directed weighted graph with vertices and edges with default weight values
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        public DirectedWeightedGraph(IEnumerable<TVertex> vertices, IEnumerable<KeyValuePair<TVertex, TVertex>> edges) : base(vertices)
        {
            foreach (var edge in edges)
                AddDirectedEdge(edge.Key, edge.Value);
        }

        /// <summary>
        /// Initializes a directed weighted graph with vertices, edges and edge weights
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        /// <param name="weights">Initial edge weights</param>
        public DirectedWeightedGraph(
            IEnumerable<TVertex> vertices,
            IEnumerable<KeyValuePair<TVertex, TVertex>> edges,
            IDictionary<KeyValuePair<TVertex, TVertex>, TWeight> weights) : this(vertices, edges)
        {
            foreach (var weight in weights)
            {
                var source = weight.Key.Key;
                var destination = weight.Key.Value;
                InitWeight(source, destination, weight.Value);
            }
        }

        #region IDirectedWeightedGraph<TVertex, TWeight>

        public void AddDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            ForbidLoop(source, destination);
            ForbidMultipleEdge(source, destination);

            _adjacencyLists[source].Add(destination);
            _weights.Add(new KeyValuePair<TVertex, TVertex>(source, destination), default);
        }

        public void AddDirectedEdge(TVertex source, TVertex destination, TWeight weight)
        {
            AddDirectedEdge(source, destination);
            _weights[new KeyValuePair<TVertex, TVertex>(source, destination)] = weight;
        }

        public void RemoveDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);

            _weights.Remove(new KeyValuePair<TVertex, TVertex>(source, destination));
            _adjacencyLists[source].Remove(destination);
        }

        #endregion
    }
}
