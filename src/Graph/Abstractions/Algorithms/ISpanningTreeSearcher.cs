using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface ISpanningTreeSearcher<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IEnumerable<Edge<TVertex>>>
        where TGraph : UndirectedWeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
    }
}
