using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface IShortestDistancesSearcherFromVertex<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IDictionary<TVertex, int>, TVertex>
        where TGraph : WeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
    }
}
