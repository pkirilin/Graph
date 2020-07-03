using Graph.Structures;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class MixedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new MixedGraph<int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new MixedGraph<int>(vertices);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.All(graph.AdjacencyLists.Values, list =>
            {
                Assert.Empty(list);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdges()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var directedEdges = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
            };
            var undirectedEdges = new List<Edge<int>>
            {
                new Edge<int>(1, 2),
                new Edge<int>(0, 2)
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1, 0 },
                [3] = new List<int>(),
            };

            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdges_WhenTargetVertexExists()
        {
            var graph = MixedGraphTestData.GenerateTestGraph();
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
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var graph = MixedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1, 0 },
                [3] = new List<int>() { 1 },
            };

            graph.AddDirectedEdge(3, 1);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_AddDirectedEdge_KeyNotFoundException), MemberType = typeof(MixedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_AddDirectedEdge_InvalidOperationException), MemberType = typeof(MixedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowInvalidOperationException_WhenEdgesCreatesLoopOrMultipleEdge(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_RemoveDirectedEdge), MemberType = typeof(MixedGraphTestData))]
        public void RemoveDirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified(
            MixedGraph<int> graph,
            int sourceToRemove,
            int destinationToRemove,
            Dictionary<int, IReadOnlyList<int>> expectedAdjacencyLists)
        {
            graph.RemoveDirectedEdge(sourceToRemove, destinationToRemove);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_RemoveDirectedEdge_KeyNotFoundException), MemberType = typeof(MixedGraphTestData))]
        public void RemoveDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveDirectedEdge(source, destination);
            });
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var graph = MixedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2, 3 },
                [2] = new List<int>() { 1, 0 },
                [3] = new List<int>() { 1 },
            };

            graph.AddUndirectedEdge(3, 1);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_AddUndirectedEdge_KeyNotFoundException), MemberType = typeof(MixedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_AddUndirectedEdge_InvalidOperationException), MemberType = typeof(MixedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_RemoveUndirectedEdge), MemberType = typeof(MixedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified(
            MixedGraph<int> graph,
            int sourceToRemove,
            int destinationToRemove,
            Dictionary<int, IReadOnlyList<int>> expectedAdjacencyLists)
        {
            graph.RemoveUndirectedEdge(sourceToRemove, destinationToRemove);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(MixedGraphTestData.MemberData_RemoveUndirectedEdge_KeyNotFoundException), MemberType = typeof(MixedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedGraph<int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveUndirectedEdge(source, destination);
            });
        }
    }
}
