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

            var graph = new DirectedGraph<int>(vertices, edges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Empty(graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[2]);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 0),
                new KeyValuePair<int, int>(2, 1)
            };
            var graph = new DirectedGraph<int>(vertices, edges);

            graph.AddDirectedEdge(3, 0);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0, 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[3]);
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

        [Fact]
        public void RemoveDirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 0),
                new KeyValuePair<int, int>(2, 1)
            };
            var graph = new DirectedGraph<int>(vertices, edges);

            graph.RemoveDirectedEdge(0, 1);
            graph.RemoveDirectedEdge(2, 0);
            // Removing not existing connection should not affect adjacencyLists
            graph.RemoveDirectedEdge(1, 0);

            Assert.Equal(new List<int> { }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
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
