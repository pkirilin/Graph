using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface IFloydWarshall<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IDictionary<TVertex, IDictionary<TVertex, int>>>
        where TGraph : WeightedGraph<TVertex, int>
        where TVertex : IComparable<TVertex>
    {
    }
}
