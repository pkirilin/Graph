﻿using Graph.Abstractions;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents an undirected unweighted graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public class UndirectedGraph<TVertex> : Graph<TVertex>, IUndirectedGraph<TVertex>
    {
        /// <summary>
        /// Initializes an empty undirected unweighted graph with no vertices and edges
        /// </summary>
        public UndirectedGraph()
        {
        }

        /// <summary>
        /// Initializes an undirected unweighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public UndirectedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes an undirected unweighted graph with vertices and edges
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        public UndirectedGraph(IEnumerable<TVertex> vertices, IEnumerable<KeyValuePair<TVertex, TVertex>> edges) : this(vertices)
        {
            foreach (var edge in edges)
                AddUndirectedEdge(edge.Key, edge.Value);
        }

        #region IUndirectedGraph<TVertex>

        public void AddUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesCorrect(source, destination);
            _adjacencyLists[source].Add(destination);
            _adjacencyLists[destination].Add(source);
        }

        public void RemoveUndirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesCorrect(source, destination);
            _adjacencyLists[source].Remove(destination);
            _adjacencyLists[destination].Remove(source);
        }

        #endregion
    }
}
