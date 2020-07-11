using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class DijkstraTests
    {
        [Theory]
        [MemberData(nameof(DijkstraTestData.MemberData_Execute), MemberType = typeof(DijkstraTestData))]
        public void DijkstraAlgorithm_ShouldFindShortestDistances_FromInitialVertex_ToAllRemaining(WeightedGraph<int, int> graph, int initialVertex, Dictionary<int, int> expectedShortestDistances)
        {
            var dijkstraAlgorithm = new Dijkstra<WeightedGraph<int, int>, int>();

            var result = dijkstraAlgorithm.Execute(graph, initialVertex);

            Assert.Equal(expectedShortestDistances, result);
        }

        [Theory]
        [MemberData(nameof(DijkstraTestData.MemberData_WrongInitialVertex), MemberType = typeof(DijkstraTestData))]
        public void DijkstraAlgorithm_ShouldThrowArgumentException_WhenInitialVertexDoesNotExistInGraph(WeightedGraph<int, int> graph, int initialVertex)
        {
            var dijkstraAlgorithm = new Dijkstra<WeightedGraph<int, int>, int>();

            Assert.Throws<ArgumentException>(() =>
            {
                dijkstraAlgorithm.Execute(graph, initialVertex);
            });
        }

        [Theory]
        [MemberData(nameof(DijkstraTestData.MemberData_NegativeWeights), MemberType = typeof(DijkstraTestData))]
        public void DijkstraAlgorithm_ShouldThrowInvalidOperationException_WhenGraphContainsNegativeWeights(WeightedGraph<int, int> graph, int initialVertex)
        {
            var dijkstraAlgorithm = new Dijkstra<WeightedGraph<int, int>, int>();

            Assert.Throws<InvalidOperationException>(() =>
            {
                dijkstraAlgorithm.Execute(graph, initialVertex);
            });
        }
    }
}
