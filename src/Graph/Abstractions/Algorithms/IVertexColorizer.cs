using Graph.Abstractions.Algorithms.Base;
using System;
using System.Collections.Generic;

namespace Graph.Abstractions.Algorithms
{
    public interface IVertexColorizer<TGraph, TVertex> : IFunctionAlgorithm<TGraph, TVertex, IDictionary<TVertex, int>>
        where TGraph : Graph<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
    }
}
