using System;
using System.Collections.Generic;

namespace Graph.Abstractions
{
    /// <summary>
    /// Base class for all unweighted graph types
    /// </summary>
    /// <typeparam name="TVertex">Graph vertex data type</typeparam>
    public abstract class Graph<TVertex> : GraphBase<TVertex> where TVertex : IComparable<TVertex>
    {
        protected Graph()
        {
        }

        protected Graph(IEnumerable<TVertex> vertices) : base(vertices)
        {
        }
    }
}
