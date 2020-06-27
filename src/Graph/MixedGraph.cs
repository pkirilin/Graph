using Graph.Abstractions;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Represents a mixed unweighted graph, which can contain both directed and undirected edges
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public class MixedGraph<TVertex> : Graph<TVertex>, IDirectedGraph<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Initializes an empty mixed unweighted graph with no vertices and edges
        /// </summary>
        public MixedGraph()
        {
        }

        /// <summary>
        /// Initializes a mixed unweighted graph with vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        public MixedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        /// <summary>
        /// Initializes a mixed unweighted graph with vertices and edges
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        /// <param name="directedEdges">Initial directed edges represented as a pair of vertices</param>
        /// <param name="undirectedEdges">Initial undirected edges represented as a pair of vertices</param>
        public MixedGraph(
            IEnumerable<TVertex> vertices,
            IEnumerable<KeyValuePair<TVertex, TVertex>> directedEdges,
            IEnumerable<KeyValuePair<TVertex, TVertex>> undirectedEdges) : this(vertices)
        {
            foreach (var edge in directedEdges)
                AddDirectedEdge(edge.Key, edge.Value);
            foreach (var edge in undirectedEdges)
                AddUndirectedEdge(edge.Key, edge.Value);
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

        #region IUndirectedGraph<TVertex>

        public void AddUndirectedEdge(TVertex source, TVertex destination)
        {
            AddDirectedEdge(source, destination);
            _adjacencyLists[destination].Add(source);
        }

        public void RemoveUndirectedEdge(TVertex source, TVertex destination)
        {
            RemoveDirectedEdge(source, destination);
            _adjacencyLists[destination].Remove(source);
        }

        #endregion
    }
}
