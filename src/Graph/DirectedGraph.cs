using Graph.Abstractions;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents a directed unweighted graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public class DirectedGraph<TVertex> : Graph<TVertex>, IDirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Initializes an empty directed unweighted graph with no vertices and edges
        /// </summary>
        public DirectedGraph()
        {
        }

        /// <summary>
        /// Initializes a directed unweighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public DirectedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes a directed unweighted graph with vertices and edges
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="edges">Initial edges represented as a pair of vertices</param>
        public DirectedGraph(IEnumerable<TVertex> vertices, IEnumerable<KeyValuePair<TVertex, TVertex>> edges) : this(vertices)
        {
            foreach (var edge in edges)
                AddDirectedEdge(edge.Key, edge.Value);
        }

        #region IDirectedGraph<TVertex>

        public void AddDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesCorrect(source, destination);
            _adjacencyLists[source].Add(destination);
        }

        public void RemoveDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesCorrect(source, destination);
            _adjacencyLists[source].Remove(destination);
        }

        #endregion
    }
}
