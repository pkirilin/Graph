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
            var directedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2)
            };
            var undirectedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(1, 3),
                new KeyValuePair<int, int>(2, 1)
            };

            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 3, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[3]);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdges_WhenTargetVertexExists()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var directedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 2)
            };
            var undirectedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2)
            };
            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            graph.RemoveVertex(0);

            Assert.DoesNotContain(0, graph.AdjacencyLists);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var directedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 0)
            };
            var undirectedEdges = new List<KeyValuePair<int, int>> { };
            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            graph.AddDirectedEdge(2, 1);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0, 1 }, graph.AdjacencyLists[2]);
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

        [Fact]
        public void RemoveDirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var directedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 0),
                new KeyValuePair<int, int>(2, 1)
            };
            var undirectedEdges = new List<KeyValuePair<int, int>> { };
            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            graph.RemoveDirectedEdge(0, 1);
            graph.RemoveDirectedEdge(2, 0);
            // Removing not existing connection should not affect adjacencyLists
            graph.RemoveDirectedEdge(1, 0);

            Assert.Equal(new List<int> { }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
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
            var vertices = new List<int> { 0, 1, 2 };
            var directedEdges = new List<KeyValuePair<int, int>> { };
            var undirectedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1)
            };
            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            graph.AddUndirectedEdge(2, 1);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
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

        [Fact]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var directedEdges = new List<KeyValuePair<int, int>> { };
            var undirectedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2)
            };
            var graph = new MixedGraph<int>(vertices, directedEdges, undirectedEdges);

            graph.RemoveUndirectedEdge(1, 2);
            // Removing not existing connection should not affect adjacencyLists
            graph.RemoveUndirectedEdge(0, 2);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { }, graph.AdjacencyLists[2]);
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
