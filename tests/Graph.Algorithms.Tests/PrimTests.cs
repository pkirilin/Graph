using Graph.Abstractions.Algorithms;
using Graph.Algorithms.Tests.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class PrimTests
    {
        [Theory]
        [MemberData(nameof(PrimTestData.MemberData_Execute), MemberType = typeof(PrimTestData))]
        public void PrimAlgorithm_ShouldFindMinimalSpanningTreeForGraph(
            UndirectedWeightedGraph<int, int> graph,
            IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int> connectedComponentsCounter,
            IEnumerable<Edge<int>> expectedSpanningTreeEdges)
        {
            var primAlgorithm = new Prim<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounter);

            var result = primAlgorithm.Execute(graph);

            Assert.Equal(expectedSpanningTreeEdges, result);
        }

        [Fact]
        public void PrimAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var connectedComponentsCounterMock = new Mock<IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int>>();
            var primAlgorithm = new Prim<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounterMock.Object);

            Assert.Throws<ArgumentNullException>(() =>
            {
                primAlgorithm.Execute(null);
            });
        }

        [Theory]
        [MemberData(nameof(PrimTestData.MemberData_InvalidConnectedComponentsCount), MemberType = typeof(PrimTestData))]
        public void PrimAlgorithm_ShouldThrowInvalidOperationException_WhenTargetGraphHasMoreThanOneConnectedComponents(
            UndirectedWeightedGraph<int, int> graph,
            IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int> connectedComponentsCounter)
        {
            var primAlgorithm = new Prim<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounter);

            Assert.Throws<InvalidOperationException>(() =>
            {
                primAlgorithm.Execute(graph);
            });
        }

        [Theory]
        [MemberData(nameof(PrimTestData.MemberData_EmptyVerticesOrEdges), MemberType = typeof(PrimTestData))]
        public void PrimAlgorithm_ShouldReturnEmptySpanningTreeEdges_WhenTargetGraphHasNoVerticesOrEdges(
            UndirectedWeightedGraph<int, int> graph,
            IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int> connectedComponentsCounter)
        {
            var primAlgorithm = new Prim<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounter);

            var result = primAlgorithm.Execute(graph);

            Assert.Equal(Enumerable.Empty<Edge<int>>(), result);
        }
    }
}
