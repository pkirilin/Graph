using Graph.Abstractions;
using Graph.Structures;
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
            IEnumerable<Edge<TVertex>> directedEdges,
            IEnumerable<Edge<TVertex>> undirectedEdges) : this(vertices)
        {
            foreach (var edge in directedEdges)
                AddDirectedEdge(edge.Source, edge.Destination);
            foreach (var edge in undirectedEdges)
                AddUndirectedEdge(edge.Source, edge.Destination);
        }

        #region IDirectedGraph<TVertex>

        public void AddDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            ForbidLoop(source, destination);
            ForbidMultipleEdge(source, destination);

            _adjacencyLists[source].Add(destination);
        }

        public void RemoveDirectedEdge(TVertex source, TVertex destination)
        {
            EnsureEdgeVerticesExist(source, destination);
            _adjacencyLists[source].Remove(destination);
        }

        #endregion

        #region IUndirectedGraph<TVertex>

        public void AddUndirectedEdge(TVertex source, TVertex destination)
        {
            AddDirectedEdge(source, destination);
            AddDirectedEdge(destination, source);
        }

        public void RemoveUndirectedEdge(TVertex source, TVertex destination)
        {
            RemoveDirectedEdge(source, destination);
            RemoveDirectedEdge(destination, source);
        }

        #endregion
    }
}
