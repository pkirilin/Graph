using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface ITopologicalSorter<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IEnumerable<TVertex>, TVertex>
        where TGraph : GraphBase<TVertex>, IDirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
