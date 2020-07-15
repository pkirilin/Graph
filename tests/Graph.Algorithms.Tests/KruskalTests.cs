using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class KruskalTests
    {
        [Theory]
        [MemberData(nameof(KruskalTestData.MemberData_Execute), MemberType = typeof(KruskalTestData))]
        public void KruskalAlgorithm_ShouldFindMinimalSpanningTreeForGraph(UndirectedWeightedGraph<int, int> graph, IEnumerable<Edge<int>> expectedResult)
        {
            var kruskalAlgorithm = KruskalTestData.KruskalAlgorithm;

            var result = kruskalAlgorithm.Execute(graph);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void KruskalAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var kruskalAlgorithm = KruskalTestData.KruskalAlgorithm;

            Assert.Throws<ArgumentNullException>(() =>
            {
                kruskalAlgorithm.Execute(null);
            });
        }

        [Theory]
        [MemberData(nameof(KruskalTestData.MemberData_InvalidConnectedComponentsCount), MemberType = typeof(KruskalTestData))]
        public void KruskalAlgorithm_ShouldThrowInvalidOperationException_WhenTargetGraphHasMoreThanOneConnectedComponents(UndirectedWeightedGraph<int, int> graph)
        {
            var kruskalAlgorithm = KruskalTestData.KruskalAlgorithm;

            Assert.Throws<InvalidOperationException>(() =>
            {
                kruskalAlgorithm.Execute(graph);
            });
        }
    }
}
