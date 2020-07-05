using Graph.Structures;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Graph.Tests
{
    public class MixedWeightedGraphTests
    {
        [Fact]
        public void Ctor_ShouldInitializeAdjacencyLists()
        {
            var graph = new MixedWeightedGraph<int, int>();

            Assert.NotNull(graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists);
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVertices()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };

            var graph = new MixedWeightedGraph<int, int>(vertices);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.All(graph.AdjacencyLists.Values, list =>
            {
                Assert.Empty(list);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithDefaultWeights()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var directedEdges = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
                new Edge<int>(0, 2)
            };
            var undirectedEdges = new List<Edge<int>>
            {
                new Edge<int>(1, 3),
                new Edge<int>(2, 1)
            };
            var edges = directedEdges.Concat(undirectedEdges);
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 3, 2 },
                [2] = new List<int>() { 1 },
                [3] = new List<int>() { 1 },
            };

            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.All(edges, edge =>
            {
                Assert.Equal(default, graph.Weights[edge]);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithWeights()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(0, 2);
            var edge3 = new Edge<int>(1, 3);
            var edge4 = new Edge<int>(2, 1);
            var directedEdges = new List<Edge<int>> { edge1, edge2 };
            var undirectedEdges = new List<Edge<int>> { edge3, edge4 };
            var directedWeights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
            };
            var undirectedWeights = new Dictionary<Edge<int>, int>()
            {
                [edge3] = 3,
                [edge4] = 4,
            };
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 3, 2 },
                [2] = new List<int>() { 1 },
                [3] = new List<int>() { 1 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [edge3] = 3,
                [edge4] = 4,
                [new Edge<int>(3, 1)] = 3,
                [new Edge<int>(1, 2)] = 4,
            };

            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndTheirWeights_WhenTargetVertexExists()
        {
            var graph = MixedWeightedGraphTestData.GenerateTestGraph();
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
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var graph = MixedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2, 0 },
                [2] = new List<int>() { 1, 0 },
                [3] = new List<int>() { 1 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(1, 0)] = default,
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
                [new Edge<int>(3, 1)] = 4,
            };

            graph.AddDirectedEdge(1, 0);
            graph.AddDirectedEdge(3, 1, 4);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_AddDirectedEdge_KeyNotFoundException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddDirectedEdge(source, destination);
                graph.AddDirectedEdge(source, destination, 1);
            });
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_AddDirectedEdge_InvalidOperationException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void AddDirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(MixedWeightedGraph<int, int> graph, int source, int destination)
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
            var graph = MixedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 2 },
                [1] = new List<int>() { 2 },
                [2] = new List<int>() { 1, 0 },
                [3] = new List<int>() { },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
            };

            graph.RemoveDirectedEdge(0, 1);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_RemoveDirectedEdge_KeyNotFoundException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void RemoveDirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveDirectedEdge(source, destination);
            });
        }

        [Fact]
        public void AddUndirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var graph = MixedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { 2, 3 },
                [2] = new List<int>() { 1, 0, 3 },
                [3] = new List<int>() { 1, 2 },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
                [new Edge<int>(3, 1)] = 4,
                [new Edge<int>(1, 3)] = default,
                [new Edge<int>(3, 1)] = default,
                [new Edge<int>(2, 3)] = 4,
                [new Edge<int>(3, 2)] = 4,
            };

            graph.AddUndirectedEdge(1, 3);
            graph.AddUndirectedEdge(3, 2, 4);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_AddUndirectedEdge_KeyNotFoundException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_AddUndirectedEdge_InvalidOperationException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void AddUndirectedEdge_ShouldThrowInvalidOperationException_WhenEdgeCreatesLoopOrMultipleEdge(MixedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.AddUndirectedEdge(source, destination);
            });
        }

        [Fact]
        public void RemoveUndirectedEdge_ShouldRemoveConnectedVertexFromAdjacencyLists_WhenExistingKeysSpecified()
        {
            var graph = MixedWeightedGraphTestData.GenerateTestGraph();
            var expectedAdjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
            {
                [0] = new List<int>() { 1, 2 },
                [1] = new List<int>() { },
                [2] = new List<int>() { 0 },
                [3] = new List<int>() { },
            };
            var expectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
            };

            graph.RemoveUndirectedEdge(1, 2);

            Assert.Equal(expectedAdjacencyLists, graph.AdjacencyLists);
            Assert.Equal(expectedWeights, graph.Weights);
        }

        [Theory]
        [MemberData(nameof(MixedWeightedGraphTestData.MemberData_RemoveUndirectedEdge_KeyNotFoundException), MemberType = typeof(MixedWeightedGraphTestData))]
        public void RemoveUndirectedEdge_ShouldThrowKeyNotFoundException_WhenNotExistingKeySpecified(MixedWeightedGraph<int, int> graph, int source, int destination)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveUndirectedEdge(source, destination);
            });
        }
    }
}
