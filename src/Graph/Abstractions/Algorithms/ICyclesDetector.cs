using Graph.Abstractions.Algorithms.Base;
using System;

namespace Graph.Abstractions.Algorithms
{
    public interface ICyclesDetector<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, bool, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
