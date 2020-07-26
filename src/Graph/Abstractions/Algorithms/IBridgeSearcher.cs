using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface IBridgeSearcher<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IEnumerable<Edge<TVertex>>>
        where TGraph : GraphBase<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
