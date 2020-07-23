using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class ArticulationPointSearcherTests
    {
        [Theory]
        [MemberData(nameof(ArticulationPointSearcherTestData.MemberData_Execute), MemberType = typeof(ArticulationPointSearcherTestData))]
        public void ArticulationPointSearcher_ShouldFindAllArticulationPointVerticesInGraph(UndirectedGraph<int> graph, IEnumerable<int> expectedResult)
        {
            var searcher = ArticulationPointSearcherTestData.ArticulationPointSearcher;

            var result = searcher.Execute(graph);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ArticulationPointSearcher_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var searcher = ArticulationPointSearcherTestData.ArticulationPointSearcher;

            Assert.Throws<ArgumentNullException>(() =>
            {
                searcher.Execute(null);
            });
        }

        [Theory]
        [MemberData(nameof(ArticulationPointSearcherTestData.MemberData_WrongConnectedComponentsCount), MemberType = typeof(ArticulationPointSearcherTestData))]
        public void ArticulationPointSearcher_ShouldThrowInvalidOperationException_WhenGraphHasMoreThanOneConnectedComponent(UndirectedGraph<int> graph)
        {
            var searcher = ArticulationPointSearcherTestData.ArticulationPointSearcher;

            Assert.Throws<InvalidOperationException>(() =>
            {
                searcher.Execute(graph);
            });
        }
    }
}
