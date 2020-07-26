using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class LeeTests
    {
        [Theory]
        [MemberData(nameof(LeeTestData.MemberData_Execute), MemberType = typeof(LeeTestData))]
        public void LeeAlgorithm_ShouldFindPathFromStartToEndVertex(
            UndirectedGraph<int> graph,
            int startVertex,
            int endVertex,
            IEnumerable<int> expectedResult)
        {
            var leeAlgorithm = LeeTestData.LeeAlgorithm;

            var result = leeAlgorithm.Execute(graph, startVertex, endVertex);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void LeeAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var leeAlgorithm = LeeTestData.LeeAlgorithm;

            Assert.Throws<ArgumentNullException>(() =>
            {
                leeAlgorithm.Execute(null, default, default);
            });
        }

        [Theory]
        [MemberData(nameof(LeeTestData.MemberData_NotExistingVertex), MemberType = typeof(LeeTestData))]
        public void LeeAlgorithm_ShouldThrowArgumentException_WhenTargetGraphDoesNotContainStartOrEndVertex(
            UndirectedGraph<int> graph,
            int startVertex,
            int endVertex)
        {
            var leeAlgorithm = LeeTestData.LeeAlgorithm;

            Assert.Throws<ArgumentException>(() =>
            {
                leeAlgorithm.Execute(graph, startVertex, endVertex);
            });
        }

        [Theory]
        [MemberData(nameof(LeeTestData.MemberData_WrongConnectedComponentsCount), MemberType = typeof(LeeTestData))]
        public void LeeAlgorithm_ShouldThrowInvalidOperationException_WhenTargetGraphHasMoreThanOneConnectedComponent(
            UndirectedGraph<int> graph,
            int startVertex,
            int endVertex)
        {
            var leeAlgorithm = LeeTestData.LeeAlgorithm;

            Assert.Throws<InvalidOperationException>(() =>
            {
                leeAlgorithm.Execute(graph, startVertex, endVertex);
            });
        }
    }
}
