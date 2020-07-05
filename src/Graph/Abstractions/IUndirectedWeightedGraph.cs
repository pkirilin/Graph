﻿namespace Graph.Abstractions
{
    /// <summary>
    /// Provides specific methods for weighted undirected graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    /// <typeparam name="TWeight">Graph edge weight type</typeparam>
    public interface IUndirectedWeightedGraph<TVertex, TWeight> : IUndirectedGraph<TVertex>
    {
        /// <summary>
        /// Adds new two-way connected edge between two vertices (from source to destination and vice-versa)
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>

        /// <summary>
        /// Adds new two-way connected edge between two vertices (from source to destination and vice-versa)
        /// and sets specified weight value for this edge
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        /// <param name="weight">Edge weight value</param>
        void AddUndirectedEdge(TVertex source, TVertex destination, TWeight weight);
    }
}
