using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class UndirectedWeightedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new UndirectedWeightedGraph<int, int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new UndirectedWeightedGraph<int, int>(vertices);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.All(graph.AdjacencyLists.Values, list =>
            {
                Assert.Empty(list);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithDefaultWeights()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edges = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(0, 2)
            };

            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1, 0 }, graph.AdjacencyLists[2]);
            Assert.All(edges, edge =>
            {
                Assert.Equal(default, graph.Weights[edge]);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithWeights()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var edges = new List<KeyValuePair<int, int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
            };

            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1, 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(1, graph.Weights[edge1]);
            Assert.Equal(default, graph.Weights[edge2]);
            Assert.Equal(3, graph.Weights[edge3]);
        }

        [Fact]
        public void Ctor_ShouldThrowInvalidOperationException_WhenWeightsEdgeDoesNotExist()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edges = new List<KeyValuePair<int, int>>() { edge1, edge2 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [new KeyValuePair<int, int>(0, 2)] = 100,
                [new KeyValuePair<int, int>(2, 0)] = 101
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);
            });
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndWeights_WhenTargetVertexExists()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(0, 2);
            var edge3 = new KeyValuePair<int, int>(1, 2);
            var edges = new List<KeyValuePair<int, int>> { edge1, edge2, edge3 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [edge3] = 3
            };
            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);

            graph.RemoveVertex(0);

            Assert.DoesNotContain(0, graph.AdjacencyLists);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(1, 2)] = 3,
                [new KeyValuePair<int, int>(2, 1)] = 3
            }, graph.Weights);
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edges = new List<KeyValuePair<int, int>> { edge1 };
            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

            graph.AddUndirectedEdge(2, 1);
            graph.AddUndirectedEdge(0, 3, 10);

            Assert.Equal(new List<int> { 1, 3 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[3]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = default,
                [new KeyValuePair<int, int>(1, 0)] = default,
                [new KeyValuePair<int, int>(2, 1)] = default,
                [new KeyValuePair<int, int>(1, 2)] = default,
                [new KeyValuePair<int, int>(0, 3)] = 10,
                [new KeyValuePair<int, int>(3, 0)] = 10,
            }, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(UndirectedWeightedGraphTestData.MemberData_AddUndirectedEdge_KeyNotFoundException), MemberType = typeof(UndirectedWeightedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(UndirectedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(UndirectedWeightedGraphTestData.MemberData_AddUndirectedEdge_InvalidOperationException), MemberType = typeof(UndirectedWeightedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(UndirectedWeightedGraph<int, int> graph, int source, int destination)
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
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edges = new List<KeyValuePair<int, int>> { edge1, edge2 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
            };
            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);

            graph.RemoveUndirectedEdge(1, 2);
            // Removing not existing connection should not affect adjacencyLists
            graph.RemoveUndirectedEdge(0, 2);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[1]);
            Assert.Empty(graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = 1,
                [new KeyValuePair<int, int>(1, 0)] = 1,
            }, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(UndirectedWeightedGraphTestData.MemberData_RemoveUndirectedEdge_KeyNotFoundException), MemberType = typeof(UndirectedWeightedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(UndirectedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveUndirectedEdge(source, destination);
            });
        }
    }
}
