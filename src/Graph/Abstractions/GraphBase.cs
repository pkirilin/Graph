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
        protected void EnsureEdgeVerticesExist(TVertex source, TVertex destination)
        {
            if (!_adjacencyLists.ContainsKey(source))
                throw new KeyNotFoundException($"Vertex with key = '{source}' doesn't exist in graph");
            if (!_adjacencyLists.ContainsKey(destination))
                throw new KeyNotFoundException($"Vertex with key = '{destination}' doesn't exist in graph");
        }

        /// <summary>
        /// Checks if graph edge's source and destination vertices are equal (in this case they create loop).
        /// If so, throws InvalidOperationException
        /// </summary>
        /// <param name="source">Source edge vertex</param>
        /// <param name="destination">Destination edge vertex</param>
        /// <exception cref="InvalidOperationException"></exception>
        protected void ForbidLoop(TVertex source, TVertex destination)
        {
            if (source.CompareTo(destination) == 0)
                throw new InvalidOperationException("Graph can't contain loops");
        }

        /// <summary>
        /// Checks if edge with specified source and destination vertices already exists in graph.
        /// If so, throws InvalidOperationException
        /// </summary>
        /// <param name="source">Source edge vertex</param>
        /// <param name="destination">Destination edge vertex</param>
        /// <exception cref="InvalidOperationException"></exception>
        protected void ForbidMultipleEdge(TVertex source, TVertex destination)
        {
            if (_adjacencyLists[source].Any(v => v.CompareTo(destination) == 0))
                throw new InvalidOperationException($"Attempted to insert edge connecting vertices '{source}' and '{destination}' which already exists, but multiple edges are forbidden");
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

            // Removing target vertex from each adjacency list
            var adjacencyListsKeys = _adjacencyLists.Keys.ToList();
            foreach (var key in adjacencyListsKeys)
            {
                _adjacencyLists[key] = _adjacencyLists[key]
                    .Where(v => v.CompareTo(vertex) != 0)
                    .ToList();
            }

            _adjacencyLists[vertex].Clear();
            _adjacencyLists.Remove(vertex);
        }
    }
}
