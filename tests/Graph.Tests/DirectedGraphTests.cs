using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class DirectedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new DirectedGraph<int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new DirectedGraph<int>(vertices);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.All(graph.AdjacencyLists.Values, list =>
            {
                Assert.Empty(list);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdges()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(2, 0)
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { },
                [2] = new List<int>() { 0 },
            };

            var graph = new DirectedGraph<int>(vertices, edges);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdges_WhenTargetVertexExists()
        {
            var graph = DirectedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1 },
            };

            graph.RemoveVertex(0);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var graph = DirectedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1 },
                [1] = new List<int>() { 2, 0 },
                [2] = new List<int>() { 0, 1 },
            };

            graph.AddDirectedEdge(1, 0);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(DirectedGraphTestData.MemberData_AddDirectedEdge_KeyNotFoundException), MemberType = typeof(DirectedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(DirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(DirectedGraphTestData.MemberData_AddDirectedEdge_InvalidOperationException), MemberType = typeof(DirectedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(DirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(DirectedGraphTestData.MemberData_RemoveDirectedEdge), MemberType = typeof(DirectedGraphTestData))]
        public void RemoveDirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified(
            DirectedGraph<int> graph,
            int sourceToRemove,
            int destinationToRemove,
            Dictionary<int, IReadOnlyList<int>> expectedAdjacencyLists)
        {
            graph.RemoveDirectedEdge(sourceToRemove, destinationToRemove);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(DirectedGraphTestData.MemberData_RemoveDirectedEdge_KeyNotFoundException), MemberType = typeof(DirectedGraphTestData))]
        public void RemoveDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(DirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveDirectedEdge(source, destination);
            });
        }
    }
}
