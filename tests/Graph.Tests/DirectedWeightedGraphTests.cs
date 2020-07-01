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
            var edges = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(2, 0)
            };

            var graph = new DirectedWeightedGraph<int, int>(vertices, edges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Empty(graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[2]);
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
            var edge2 = new KeyValuePair<int, int>(0, 2);
            var edge3 = new KeyValuePair<int, int>(2, 0);
            var edges = new List<KeyValuePair<int, int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
            };

            var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Empty(graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(1, graph.Weights[edge1]);
            Assert.Equal(default, graph.Weights[edge2]);
            Assert.Equal(3, graph.Weights[edge3]);
        }

        [Fact]
        public void Ctor_ShouldThrowInvalidOperationException_WhenWeightsEdgeDoesNotExist()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(0, 2);
            var edge3 = new KeyValuePair<int, int>(2, 0);
            var edges = new List<KeyValuePair<int, int>>() { edge1, edge2, edge3 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge3] = 3,
                [new KeyValuePair<int, int>(1, 2)] = 100
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);
            });
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndTheirWeights_WhenTargetVertexExists()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(0, 2);
            var edge3 = new KeyValuePair<int, int>(2, 0);
            var edge4 = new KeyValuePair<int, int>(1, 2);
            var edges = new List<KeyValuePair<int, int>> { edge1, edge2, edge3, edge4 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [edge3] = 3,
                [edge4] = 4,
            };
            var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);

            graph.RemoveVertex(0);

            Assert.DoesNotContain(0, graph.AdjacencyLists);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Empty(graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>() { [edge4] = 4 }, graph.Weights);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(2, 0);
            var edges = new List<KeyValuePair<int, int>> { edge1 };
            var graph = new DirectedWeightedGraph<int, int>(vertices, edges);

            graph.AddDirectedEdge(edge2.Key, edge2.Value);
            graph.AddDirectedEdge(edge3.Key, edge3.Value, 1);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(default, graph.Weights[edge1]);
            Assert.Equal(default, graph.Weights[edge2]);
            Assert.Equal(1, graph.Weights[edge3]);
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
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(2, 0);
            var edges = new List<KeyValuePair<int, int>> { edge1, edge2, edge3 };
            var weights = new Dictionary<KeyValuePair<int, int>, int>
            {
                [edge1] = 1,
                [edge2] = 2,
                [edge3] = 3
            };
            var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);

            graph.RemoveDirectedEdge(edge2.Key, edge2.Value);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Empty(graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(1, graph.Weights[edge1]);
            Assert.Equal(3, graph.Weights[edge3]);
            Assert.DoesNotContain(edge2, graph.Weights);
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
