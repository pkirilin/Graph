namespace Graph
{
    /// <summary>
    /// Represents a single connection between two graph vertices
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public struct Edge<TVertex>
    {
        /// <summary>
        /// First endpoint of the edge
        /// </summary>
        public TVertex Source { get; }

        /// <summary>
        /// Second endpoint of the edge
        /// </summary>
        public TVertex Destination { get; }

        public Edge(TVertex source, TVertex destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}
