﻿using System;

namespace Graph.Abstractions.Algorithms.Base
{
    /// <summary>
    /// Provides execution method for graph algorithm which returns some result
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    /// <typeparam name="TResult">Algorithm result type</typeparam>
    public interface IFunctionAlgorithm<TGraph, TVertex, TResult>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Executes algorithm on graph
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <returns>Algorithm result</returns>
        TResult Execute(TGraph graph);
    }

    /// <summary>
    /// Provides execution method for graph algorithm which takes parameter and returns some result
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    /// <typeparam name="TResult">Algorithm result type</typeparam>
    /// <typeparam name="TParameter">Input parameter for algorithm</typeparam>
    public interface IFunctionAlgorithm<TGraph, TVertex, TResult, TParameter>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Executes algorithm on graph
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="parameter">Input parameter for algorithm</param>
        /// <returns>Algorithm result</returns>
        TResult Execute(TGraph graph, TParameter parameter);
    }

    /// <summary>
    /// Provides execution method for graph algorithm which takes two parameters and returns some result
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    /// <typeparam name="TResult">Algorithm result type</typeparam>
    /// <typeparam name="TParameter1">First input parameter for algorithm</typeparam>
    /// <typeparam name="TParameter2">Second input parameter for algorithm</typeparam>
    public interface IFunctionAlgorithm<TGraph, TVertex, TResult, TParameter1, TParameter2>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Executes algorithm on graph
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="parameter">Input parameter for algorithm</param>
        /// <returns>Algorithm result</returns>
        TResult Execute(TGraph graph, TParameter1 parameter1, TParameter2 parameter2);
    }
}
