﻿using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class BreadthFirstSearchTests
    {
        [Theory]
        [MemberData(nameof(BreadthFirstSearchTestData.MemberData_Execute), MemberType = typeof(BreadthFirstSearchTestData))]
        public void BreadthFirstSearch_ShouldPerformSpecifiedAction_ForAllVerticesInConnectedComponent(GraphBase<int> graph, int initialVertex, IEnumerable<int> expectedVisitedVertices)
        {
            var bfsAlgorithm = new BreadthFirstSearch<GraphBase<int>, int>();
            var result = new List<int>();

            bfsAlgorithm.Execute(graph, initialVertex, vertex =>
            {
                result.Add(vertex);
            });

            Assert.Equal(expectedVisitedVertices, result);
        }

        [Theory]
        [MemberData(nameof(BreadthFirstSearchTestData.MemberData_Execute_ArgumentException), MemberType = typeof(BreadthFirstSearchTestData))]
        public void BreadthFirstSearch_ShouldThrowArgumentException_WhenInitialVertexDoesNotExistInGraph(GraphBase<int> graph, int initialVertex)
        {
            var bfsAlgorithm = new BreadthFirstSearch<GraphBase<int>, int>();

            Assert.Throws<ArgumentException>(() =>
            {
                bfsAlgorithm.Execute(graph, initialVertex, vertex => { });
            });
        }
    }
}
