using Graph.Abstractions.Algorithms.Base;
using System;

namespace Graph.Abstractions.Algorithms
{
    public interface IConnectedComponentsCounter<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, int>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
