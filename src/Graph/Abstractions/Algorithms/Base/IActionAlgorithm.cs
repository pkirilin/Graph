using System;

namespace Graph.Abstractions.Algorithms.Base
{
    /// <summary>
    /// Provides execution method for graph algorithm which takes parameter and performs some action without returning any result
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    /// <typeparam name="TParameter">Input parameter for algorithm</typeparam>
    /// <typeparam name="TActionParameter">Parameter for executing action</typeparam>
    public interface IActionAlgorithm<TGraph, TVertex, TParameter, TActionParameter>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Executes action algorithm on graph
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="parameter">Input parameter for algorithm</param>
        /// <param name="action">Action to be executed</param>
        void Execute(TGraph graph, TParameter parameter, Action<TActionParameter> action);
    }
}
