namespace Graph.Abstractions
{
    /// <summary>
    /// Provides specific methods for weighted directed graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    /// <typeparam name="TWeight">Graph edge weight type</typeparam>
    interface IDirectedWeightedGraph<TVertex, TWeight> : IDirectedGraph<TVertex>
    {
        /// <summary>
        /// Adds new one-way connected edge between two vertices (from source to destination)
        /// and sets specified weight value for this edge
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        /// <param name="weight">Edge weight value</param>
        void AddDirectedEdge(TVertex source, TVertex destination, TWeight weight);
    }
}
