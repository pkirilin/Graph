using Graph.Structures;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class DirectedWeightedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new DirectedWeightedGraph<int, int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new DirectedWeightedGraph<int, int>(vertices);

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
                new Edge<int>(0, 2),
                new Edge<int>(2, 0)
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { },
                [2] = new List<int>() { 0 },
            };

            var graph = new DirectedWeightedGraph<int, int>(vertices, edges);

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
            var edge2 = new Edge<int>(0, 2);
            var edge3 = new Edge<int>(2, 0);
            var edges = new List<Edge<int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { },
                [2] = new List<int>() { 0 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge2] = default,
                [edge3] = 3,
            };

            var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Fact]
        public void Ctor_ShouldThrowInvalidOperationException_WhenWeightsEdgeDoesNotExist()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(0, 2);
            var edge3 = new Edge<int>(2, 0);
            var edges = new List<Edge<int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
                [new Edge<int>(1, 2)] = 100
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);
            });
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndTheirWeights_WhenTargetVertexExists()
        {
            var graph = DirectedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(1, 2)] = 3,
                [new Edge<int>(2, 1)] = 2,
            };

            graph.RemoveVertex(0);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var graph = DirectedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2, 0 },
                [2] = new List<int>() { 0, 1 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(1, 0)] = default,
                [new Edge<int>(0, 2)] = 5,
                [new Edge<int>(2, 0)] = 4,
                [new Edge<int>(1, 2)] = 3,
                [new Edge<int>(2, 1)] = 2,
            };

            graph.AddDirectedEdge(1, 0);
            graph.AddDirectedEdge(0, 2, 5);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(DirectedWeightedGraphTestData.MemberData_AddDirectedEdge_KeyNotFoundException), MemberType = typeof(DirectedWeightedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(DirectedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
                graph.AddDirectedEdge(source, destination, 1);
            });
        }

        [Theory]
        [MemberData(nameof(DirectedWeightedGraphTestData.MemberData_AddDirectedEdge_InvalidOperationException), MemberType = typeof(DirectedWeightedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(DirectedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
                graph.AddDirectedEdge(source, destination, 1);
            });
        }

        [Fact]
        public void RemoveDirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var graph = DirectedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1 },
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 0 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(2, 0)] = 4,
                [new Edge<int>(1, 2)] = 3,
            };

            graph.RemoveDirectedEdge(2, 1);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(DirectedWeightedGraphTestData.MemberData_RemoveDirectedEdge_KeyNotFoundException), MemberType = typeof(DirectedWeightedGraphTestData))]
        public void RemoveDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(DirectedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveDirectedEdge(source, destination);
            });
        }
    }
}
