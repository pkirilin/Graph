using Graph.Algorithms.Tests.Data;
using System;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class ConnectedComponentsCounterTests
    {
        [Theory]
        [MemberData(nameof(ConnectedComponentsCounterTestData.MemberData_Execute), MemberType = typeof(ConnectedComponentsCounterTestData))]
        public void ConnectedComponentsCounter_ShouldReturnConnectedComponentsCountForGraph(UndirectedWeightedGraph<int, int> graph, int expectedConnectedComponentsCount)
        {
            var dfsAlgorithm = new DepthFirstSearch<UndirectedWeightedGraph<int, int>, int>();
            var counter = new ConnectedComponentsCounter<UndirectedWeightedGraph<int, int>, int>(dfsAlgorithm);

            var result = counter.Execute(graph);

            Assert.Equal(expectedConnectedComponentsCount, result);
        }

        [Fact]
        public void ConnectedComponentsCounter_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var dfsAlgorithm = new DepthFirstSearch<UndirectedWeightedGraph<int, int>, int>();
            var counter = new ConnectedComponentsCounter<UndirectedWeightedGraph<int, int>, int>(dfsAlgorithm);

            Assert.Throws<ArgumentNullException>(() =>
            {
                counter.Execute(null);
            });
        }
    }
}
