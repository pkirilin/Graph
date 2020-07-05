namespace Graph.Abstractions
{
    /// <summary>
    /// Provides specific methods for unweighted undirected graph
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public interface IUndirectedGraph<TVertex>
    {
        /// <summary>
        /// Adds new two-way connected edge between two vertices (from source to destination and vice-versa)
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        void AddUndirectedEdge(TVertex source, TVertex destination);

        /// <summary>
        /// Removes existing two-way connected edge between two vertices (from source to destination and vice-versa)
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        void RemoveUndirectedEdge(TVertex source, TVertex destination);
    }
}
