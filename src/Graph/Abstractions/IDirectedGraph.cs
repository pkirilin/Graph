namespace Graph.Abstractions
{
    /// <summary>
    /// Provides specific methods for unweighted directed graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public interface IDirectedGraph<TVertex>
    {
        /// <summary>
        /// Adds new one-way connected edge between two vertices (from source to destination)
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        void AddDirectedEdge(TVertex source, TVertex destination);

        /// <summary>
        /// Removes existing one-way connected edge between two vertices (from source to destination)
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        void RemoveDirectedEdge(TVertex source, TVertex destination);
    }
}
