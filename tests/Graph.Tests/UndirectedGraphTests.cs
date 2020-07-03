using Graph.Structures;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class UndirectedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new UndirectedGraph<int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new UndirectedGraph<int>(vertices);

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
            var edges = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
                new Edge<int>(0, 2),
                new Edge<int>(1, 2)
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 0, 2 },
                [2] = new List<int>() { 0, 1 },
            };

            var graph = new UndirectedGraph<int>(vertices, edges);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdges_WhenTargetVertexExists()
        {
            var graph = UndirectedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1 },
                [3] = new List<int>(),
            };

            graph.RemoveVertex(0);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var graph = UndirectedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 0, 2, 3 },
                [2] = new List<int>() { 0, 1 },
                [3] = new List<int>() { 1 },
            };

            graph.AddUndirectedEdge(3, 1);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(UndirectedGraphTestData.MemberData_AddUndirectedEdge_KeyNotFoundException), MemberType = typeof(UndirectedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(UndirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(UndirectedGraphTestData.MemberData_AddUndirectedEdge_InvalidOperationException), MemberType = typeof(UndirectedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(UndirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(UndirectedGraphTestData.MemberData_RemoveUndirectedEdge), MemberType = typeof(UndirectedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified(
            UndirectedGraph<int> graph,
            int sourceToRemove,
            int destinationToRemove,
            Dictionary<int, IReadOnlyList<int>> expectedAdjacencyLists)
        {
            graph.RemoveUndirectedEdge(sourceToRemove, destinationToRemove);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(UndirectedGraphTestData.MemberData_RemoveUndirectedEdge_KeyNotFoundException), MemberType = typeof(UndirectedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(UndirectedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveUndirectedEdge(source, destination);
            });
        }
    }
}
