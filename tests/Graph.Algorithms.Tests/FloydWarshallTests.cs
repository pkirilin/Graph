using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class FloydWarshallTests
    {
        [Theory]
        [MemberData(nameof(FloydWarshallTestData.MemberData_Execute), MemberType = typeof(FloydWarshallTestData))]
        public void FloydWarshallAlgorithm_ShouldCalculateShortestDistancesBetweenAllGraphVertices(
            WeightedGraph<int, int> graph,
            IDictionary<int, IDictionary<int, int>> expectedDistances)
        {
            var floydWarshall = FloydWarshallTestData.FloydWarshallAlgorithm;

            var distances = floydWarshall.Execute(graph);

            Assert.Equal(expectedDistances, distances);
        }

        [Fact]
        public void FloydWarshallAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var floydWarshall = FloydWarshallTestData.FloydWarshallAlgorithm;

            Assert.Throws<ArgumentNullException>(() =>
            {
                floydWarshall.Execute(null);
            });
        }
    }
}
