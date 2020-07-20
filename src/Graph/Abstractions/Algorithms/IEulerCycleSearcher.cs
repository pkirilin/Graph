using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface IEulerCycleSearcher<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IEnumerable<TVertex>, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
