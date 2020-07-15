using Graph.Algorithms.Tests.Data;
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
        public void PrimAlgorithm_ShouldFindMinimalSpanningTreeForGraph(UndirectedWeightedGraph<int, int> graph, IEnumerable<Edge<int>> expectedSpanningTreeEdges)
        {
            var primAlgorithm = PrimTestData.PrimAlgorithm;

            var result = primAlgorithm.Execute(graph);

            Assert.Equal(expectedSpanningTreeEdges, result);
        }

        [Fact]
        public void PrimAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var primAlgorithm = PrimTestData.PrimAlgorithm;

            Assert.Throws<ArgumentNullException>(() =>
            {
                primAlgorithm.Execute(null);
            });
        }

        [Theory]
        [MemberData(nameof(PrimTestData.MemberData_InvalidConnectedComponentsCount), MemberType = typeof(PrimTestData))]
        public void PrimAlgorithm_ShouldThrowInvalidOperationException_WhenTargetGraphHasMoreThanOneConnectedComponents(UndirectedWeightedGraph<int, int> graph)
        {
            var primAlgorithm = PrimTestData.PrimAlgorithm;

            Assert.Throws<InvalidOperationException>(() =>
            {
                primAlgorithm.Execute(graph);
            });
        }

        [Theory]
        [MemberData(nameof(PrimTestData.MemberData_EmptyVerticesOrEdges), MemberType = typeof(PrimTestData))]
        public void PrimAlgorithm_ShouldReturnEmptySpanningTreeEdges_WhenTargetGraphHasNoVerticesOrEdges(UndirectedWeightedGraph<int, int> graph)
        {
            var primAlgorithm = PrimTestData.PrimAlgorithm;

            var result = primAlgorithm.Execute(graph);

            Assert.Equal(Enumerable.Empty<Edge<int>>(), result);
        }
    }
}
