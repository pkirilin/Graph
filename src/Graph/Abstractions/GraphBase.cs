using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Graph.Abstractions
{
    /// <summary>
    /// Base class for all graph types
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public abstract class GraphBase<TVertex> where TVertex : IComparable<TVertex>
    {
        protected readonly IDictionary<TVertex, IList<TVertex>> _adjacencyLists;

        /// <summary>
        /// Gets graph adjacency lists as a dictionary, where keys are vertices
        /// and values are lists of vertices adjacent to its entry key vertex
        /// </summary>
        public IReadOnlyDictionary<TVertex, IReadOnlyList<TVertex>> AdjacencyLists
        {
            get
            {
                return new ReadOnlyDictionary<TVertex, IReadOnlyList<TVertex>>
                (
                    _adjacencyLists.ToDictionary
                    (
                        p => p.Key,
                        p => new ReadOnlyCollection<TVertex>(p.Value) as IReadOnlyList<TVertex>
                    )
                );
            }
        }

        /// <summary>
        /// Initializes an empty graph
        /// </summary>
        protected GraphBase()
        {
            _adjacencyLists = new Dictionary<TVertex, IList<TVertex>>(new GraphVertexEqualityComparer<TVertex>());
        }

        /// <summary>
        /// Initializes graph with specified vertices
        /// </summary>
        /// <param name="vertices">Initial vertices</param>
        protected GraphBase(IEnumerable<TVertex> vertices) : this()
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);
        }

        /// <summary>
        /// Ensures that graph contains both source and destination vertices of graph edge.
        /// If not, throws KeyNotFoundException
        /// </summary>
        /// <param name="source">First graph edge vertex</param>
        /// <param name="destination">Second graph edge vertex</param>
        /// <exception cref="KeyNotFoundException"></exception>
        protected void EnsureEdgeVerticesCorrect(TVertex source, TVertex destination)
        {
            if (!_adjacencyLists.ContainsKey(source))
                throw new KeyNotFoundException($"Vertex with key = '{source}' doesn't exist in graph");
            if (!_adjacencyLists.ContainsKey(destination))
                throw new KeyNotFoundException($"Vertex with key = '{destination}' doesn't exist in graph");
        }

        /// <summary>
        /// Adds new vertex to graph
        /// </summary>
        /// <param name="vertex">New vertex</param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void AddVertex(TVertex vertex)
        {
            if (_adjacencyLists.ContainsKey(vertex))
                throw new ArgumentException($"Vertex '{vertex}' already exists in graph", nameof(vertex));

            _adjacencyLists.Add(vertex, new List<TVertex>());
        }

        /// <summary>
        /// Removes existing vertex from graph with all connected edges
        /// </summary>
        /// <param name="vertex">Vertex to remove</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual void RemoveVertex(TVertex vertex)
        {
            if (!_adjacencyLists.ContainsKey(vertex))
                throw new KeyNotFoundException($"Vertex '{vertex}' doesn't exist in graph");

            _adjacencyLists[vertex].Clear();
            _adjacencyLists.Remove(vertex);
        }
    }
}
