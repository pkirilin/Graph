using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class BridgeSearcherTests
    {
        [Theory]
        [MemberData(nameof(BridgeSearcherTestData.MemberData_Execute), MemberType = typeof(BridgeSearcherTestData))]
        public void BridgeSearcher_ShouldFindAllBridgesInTargetGraph(UndirectedGraph<int> graph, IEnumerable<Edge<int>> expectedResult)
        {
            var bridgeSearcher = BridgeSearcherTestData.BridgeSearcher;

            var result = bridgeSearcher.Execute(graph);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void BridgeSearcher_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var bridgeSearcher = BridgeSearcherTestData.BridgeSearcher;

            Assert.Throws<ArgumentNullException>(() =>
            {
                bridgeSearcher.Execute(null);
            });
        }

        [Theory]
        [MemberData(nameof(BridgeSearcherTestData.MemberData_WrongConnectedComponentsCount), MemberType = typeof(BridgeSearcherTestData))]
        public void BridgeSearcher_ShouldThrowInvalidOperationException_WhenTargetGraphHasMoreThanOneConnectedComponents(UndirectedGraph<int> graph)
        {
            var bridgeSearcher = BridgeSearcherTestData.BridgeSearcher;

            Assert.Throws<InvalidOperationException>(() =>
            {
                bridgeSearcher.Execute(graph);
            });
        }
    }
}
