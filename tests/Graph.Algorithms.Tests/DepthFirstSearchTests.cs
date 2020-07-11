using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class DepthFirstSearchTests
    {
        [Theory]
        [MemberData(nameof(DepthFirstSearchTestData.MemberData_Execute), MemberType = typeof(DepthFirstSearchTestData))]
        public void DepthFirstSearch_ShouldPerformSpecifiedAction_ForAllVerticesInConnectedComponent(GraphBase<int> graph, int initialVertex, IEnumerable<int> expectedVisitedVertices)
        {
            var dfsAlgorithm = new DepthFirstSearch<GraphBase<int>, int>();
            var result = new List<int>();

            dfsAlgorithm.Execute(graph, initialVertex, vertex =>
            {
                result.Add(vertex);
            });

            Assert.Equal(expectedVisitedVertices, result);
        }

        [Theory]
        [MemberData(nameof(DepthFirstSearchTestData.MemberData_WrongInitialVertex), MemberType = typeof(DepthFirstSearchTestData))]
        public void DepthFirstSearch_ShouldThrowArgumentException_WhenInitialVertexDoesNotExistInGraph(GraphBase<int> graph, int initialVertex)
        {
            var dfsAlgorithm = new DepthFirstSearch<GraphBase<int>, int>();

            Assert.Throws<ArgumentException>(() =>
            {
                dfsAlgorithm.Execute(graph, initialVertex, vertex => { });
            });
        }

        [Theory]
        [MemberData(nameof(DepthFirstSearchTestData.MemberData_ArgumentNullException), MemberType = typeof(DepthFirstSearchTestData))]
        public void DepthFirstSearch_ShouldThrowArgumentNullException_WhenInputParametersAreInvalid(GraphBase<int> graph, int initialVertex, Action<int> action)
        {
            var dfsAlgorithm = new DepthFirstSearch<GraphBase<int>, int>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                dfsAlgorithm.Execute(graph, initialVertex, action);
            });
        }
    }
}
