using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class TopologicalSorterTests
    {
        [Theory]
        [MemberData(nameof(TopologicalSorterTestData.MemberData_Execute), MemberType = typeof(TopologicalSorterTestData))]
        public void TopologicalSortingAlgorithm_ShouldReturnSortedVertices(
            DirectedGraph<int> graph,
            int initialVertex,
            IEnumerable<int> expectedResult)
        {
            var topologicalSorter = TopologicalSorterTestData.TopologicalSorter;

            var result = topologicalSorter.Execute(graph, initialVertex);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TopologicalSortingAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var topologicalSorter = TopologicalSorterTestData.TopologicalSorter;

            Assert.Throws<ArgumentNullException>(() =>
            {
                topologicalSorter.Execute(null, default);
            });
        }

        [Theory]
        [MemberData(nameof(TopologicalSorterTestData.MemberData_WrongInitialVertex), MemberType = typeof(TopologicalSorterTestData))]
        public void TopologicalSortingAlgorithm_ShouldThrowArgumentException_WhenGraphDoesNotContainInitialVertex(DirectedGraph<int> graph, int initialVertex)
        {
            var topologicalSorter = TopologicalSorterTestData.TopologicalSorter;

            Assert.Throws<ArgumentException>(() =>
            {
                topologicalSorter.Execute(graph, initialVertex);
            });
        }

        [Theory]
        [MemberData(nameof(TopologicalSorterTestData.MemberData_GraphWithCycles), MemberType = typeof(TopologicalSorterTestData))]
        public void TopologicalSortingAlgorithm_ShouldThrowInvalidOperationException_WhenGraphDoesNotContainInitialVertex(DirectedGraph<int> graph, int initialVertex)
        {
            var topologicalSorter = TopologicalSorterTestData.TopologicalSorter;

            Assert.Throws<InvalidOperationException>(() =>
            {
                topologicalSorter.Execute(graph, initialVertex);
            });
        }
    }
}
