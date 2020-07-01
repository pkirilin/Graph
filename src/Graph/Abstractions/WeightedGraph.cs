using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Graph.Abstractions
{
    /// <summary>
    /// Base class for all weighted graph types
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public abstract class WeightedGraph<TVertex, TWeight> : GraphBase<TVertex> where TVertex : IComparable<TVertex>
    {
        protected readonly IDictionary<KeyValuePair<TVertex, TVertex>, TWeight> _weights = new Dictionary<KeyValuePair<TVertex, TVertex>, TWeight>();

        /// <summary>
        /// Gets weight values of all graph edges
        /// </summary>
        public IReadOnlyDictionary<KeyValuePair<TVertex, TVertex>, TWeight> Weights
            => new ReadOnlyDictionary<KeyValuePair<TVertex, TVertex>, TWeight>(_weights);

        protected WeightedGraph()
        {
        }

        protected WeightedGraph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }

        protected void InitWeight(TVertex source, TVertex destination, TWeight weight)
        {
            var edge = new KeyValuePair<TVertex, TVertex>(source, destination);
            
            if (!_weights.ContainsKey(edge))
                throw new InvalidOperationException($"Attempted to set weight '{weight}' to not existing edge '{edge.Key}' -> '{edge.Value}'");
            _weights[edge] = weight;
        }

        /// <summary>
        /// Removes existing vertex from graph with all connected edges and weights
        /// </summary>
        /// <param name="vertex"><inheritdoc/></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public override void RemoveVertex(TVertex vertex)
        {
            base.RemoveVertex(vertex);

            // Removing all weights from edges containing target vertex
            var weightsRelatedToVertex = _weights.Where(w =>
                w.Key.Key.CompareTo(vertex) == 0 ||
                w.Key.Value.CompareTo(vertex) == 0);

            foreach (var weight in weightsRelatedToVertex)
                _weights.Remove(weight);
        }
    }
}
