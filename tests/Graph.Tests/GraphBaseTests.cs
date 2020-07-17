using Graph.Abstractions;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class GraphBaseTests
    {
        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void AddVertex_ShouldAddVertex_WhenUniqueKeySpecified(GraphBase<int> graph)
        {
            graph.AddVertex(0);

            Assert.Contains(0, graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists[0]);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void AddVertex_ShouldThrowArgumentException_WhenDuplicateKeySpecified(GraphBase<int> graph)
        {
            graph.AddVertex(0);
            graph.AddVertex(1);

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddVertex(1);
            });
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void RemoveVertex_ShouldThrowKeyNotFoundException_WhenKeyDoesNotExist(GraphBase<int> graph)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveVertex(1);
            });
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_Edges), MemberType = typeof(GraphBaseTestData))]
        public void Edges_ShouldReturnAllGraphEdges(GraphBase<int> graph, IEnumerable<Edge<int>> expectedReducedEdges)
        {
            var result = graph.Edges;

            Assert.Equal(expectedReducedEdges, result);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_ReducedEdges), MemberType = typeof(GraphBaseTestData))]
        public void ReducedEdges_ShouldReturnGraphEdgesWithoutUndirectedDuplicates(GraphBase<int> graph, IEnumerable<Edge<int>> expectedReducedEdges)
        {
            var result = graph.ReducedEdges;

            Assert.Equal(expectedReducedEdges, result);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_GetVertexDeg), MemberType = typeof(GraphBaseTestData))]
        public void GetVertexDeg_ReturnsDegreeOfSpecifiedVertex(GraphBase<int> graph, int vertex, int expectedDeg)
        {
            var result = graph.GetVertexDeg(vertex);

            Assert.Equal(expectedDeg, result);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_GetVertexDeg_WrongVertex), MemberType = typeof(GraphBaseTestData))]
        public void GetVertexDeg_ThrowsArgumentException_WhenNotExistingVertexSpecified(GraphBase<int> graph, int vertex)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                graph.GetVertexDeg(vertex);
            });
        }
    }
}
