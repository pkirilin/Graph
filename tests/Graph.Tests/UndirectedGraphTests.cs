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
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(1, 2)
            };

            var graph = new UndirectedGraph<int>(vertices, edges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0, 1 }, graph.AdjacencyLists[2]);
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(2, 1),
                new KeyValuePair<int, int>(0, 3)
            };
            var graph = new UndirectedGraph<int>(vertices, edges);

            Assert.Equal(new List<int> { 1, 3 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[3]);
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

        [Fact]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2)
            };
            var graph = new UndirectedGraph<int>(vertices, edges);

            graph.RemoveUndirectedEdge(1, 2);
            // Removing not existing connection should not affect adjacencyLists
            graph.RemoveUndirectedEdge(0, 2);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { }, graph.AdjacencyLists[2]);
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
