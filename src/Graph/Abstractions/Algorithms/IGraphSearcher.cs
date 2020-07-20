using Graph.Abstractions.Algorithms.Base;
using System;

namespace Graph.Abstractions.Algorithms
{
    public interface IGraphSearcher<TGraph, TVertex> : IActionAlgorithm<TGraph, TVertex, TVertex, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
