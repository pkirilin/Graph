using Graph.Structures;
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
            var edges = new List<Edge<int>>()
            {
                new Edge<int>(0, 1),
                new Edge<int>(1, 2),
                new Edge<int>(0, 2)
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 0, 2 },
                [2] = new List<int>() { 1, 0 },
            };

            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.All(edges, edge =>
            {
                Assert.Equal(default, graph.Weights[edge]);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithWeights()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(1, 2);
            var edge3 = new Edge<int>(0, 2);
            var edges = new List<Edge<int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 0, 2 },
                [2] = new List<int>() { 1, 0 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(1, 2)] = default,
                [new Edge<int>(1, 0)] = 1,
                [new Edge<int>(2, 0)] = 3,
                [new Edge<int>(2, 1)] = default,
            };

            var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Fact]
        public void Ctor_ShouldThrowInvalidOperationException_WhenWeightsEdgeDoesNotExist()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(1, 2);
            var edges = new List<Edge<int>>() { edge1, edge2 };
            var weights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [new Edge<int>(0, 2)] = 100,
                [new Edge<int>(2, 0)] = 101
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);
            });
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndWeights_WhenTargetVertexExists()
        {
            var graph = UndirectedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1 },
                [3] = new List<int>(),
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
            };

            graph.RemoveVertex(0);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var graph = UndirectedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 0, 2, 3 },
                [2] = new List<int>() { 0, 1, 3 },
                [3] = new List<int>() { 2, 1 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(1, 0)] = 1,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
                [new Edge<int>(1, 3)] = 4,
                [new Edge<int>(3, 1)] = 4,
                [new Edge<int>(2, 3)] = default,
                [new Edge<int>(3, 2)] = default,
            };

            graph.AddUndirectedEdge(2, 3);
            graph.AddUndirectedEdge(1, 3, 4);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
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

        [Theory]
        [MemberData(nameof(UndirectedWeightedGraphTestData.MemberData_RemoveUndirectedEdge), MemberType = typeof(UndirectedWeightedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified(
            UndirectedWeightedGraph<int, int> graph,
            int sourceToRemove,
            int destinationToRemove,
            Dictionary<int, IReadOnlyList<int>> expectedAdjacencyLists,
            Dictionary<Edge<int>, int> expectedWeights)
        {
            graph.RemoveUndirectedEdge(sourceToRemove, destinationToRemove);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
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
