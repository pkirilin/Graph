﻿using Graph.Tests.Data;
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
            var edges = directedEdges.Concat(undirectedEdges);

            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 3, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[3]);
            Assert.All(edges, edge =>
            {
                Assert.Equal(default, graph.Weights[edge]);
            });
        }

        [Fact]
        public void Ctor_ShouldInitializeAdjacencyListsWithVerticesAndEdgesWithWeights()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(0, 2);
            var edge3 = new KeyValuePair<int, int>(1, 3);
            var edge4 = new KeyValuePair<int, int>(2, 1);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1, edge2 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge3, edge4 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge3] = 3,
                [edge4] = 4,
            };

            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            Assert.Equal(vertices, graph.AdjacencyLists.Keys);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 3, 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[3]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
                [edge2] = 2,
                [edge3] = 3,
                [edge4] = 4,
                [new KeyValuePair<int, int>(3, 1)] = 3,
                [new KeyValuePair<int, int>(1, 2)] = 4,
            }, graph.Weights);
        }

        [Fact]
        public void RemoveVertex_ShouldRemoveVertexWithAllRelatedEdgesAndTheirWeights_WhenTargetVertexExists()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge2, edge3 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge2] = 2,
                [edge3] = 3,
            };
            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            graph.RemoveVertex(0);

            Assert.DoesNotContain(0, graph.AdjacencyLists);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(2, 1)] = 2
            }, graph.Weights);
        }

        [Fact]
        public void AddDirectedEdge_ShouldAddConnectedVertexToAdjacencyListsWithWeight_WhenExistingKeysSpecified()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge2, edge3 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge2] = 2,
                [edge3] = 3,
            };
            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            graph.AddDirectedEdge(1, 0);
            graph.AddDirectedEdge(3, 2, 4);

            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2, 0 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1, 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = 1,
                [new KeyValuePair<int, int>(1, 0)] = default,
                [new KeyValuePair<int, int>(0, 2)] = 3,
                [new KeyValuePair<int, int>(2, 0)] = 3,
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(2, 1)] = 2,
                [new KeyValuePair<int, int>(3, 2)] = 4,
            }, graph.Weights);
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
            var vertices = new List<int> { 0, 1, 2};
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge2, edge3 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge2] = 2,
                [edge3] = 3,
            };
            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            graph.RemoveDirectedEdge(0, 1);
            graph.RemoveDirectedEdge(0, 2);

            Assert.Empty(graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1, 0 }, graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(2, 1)] = 2,
                [new KeyValuePair<int, int>(2, 0)] = 3,
            }, graph.Weights);
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
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge2, edge3 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge2] = 2,
                [edge3] = 3,
            };
            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            graph.AddUndirectedEdge(1, 3);
            graph.AddUndirectedEdge(2, 3, 4);

            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2, 3 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1, 0, 3 }, graph.AdjacencyLists[2]);
            Assert.Equal(new List<int> { 1, 2 }, graph.AdjacencyLists[3]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = 1,
                [new KeyValuePair<int, int>(0, 2)] = 3,
                [new KeyValuePair<int, int>(2, 0)] = 3,
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(2, 1)] = 2,
                [new KeyValuePair<int, int>(2, 3)] = 4,
                [new KeyValuePair<int, int>(3, 2)] = 4,
                [new KeyValuePair<int, int>(1, 3)] = default,
                [new KeyValuePair<int, int>(3, 1)] = default,
            }, graph.Weights);
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
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new KeyValuePair<int, int>(0, 1);
            var edge2 = new KeyValuePair<int, int>(1, 2);
            var edge3 = new KeyValuePair<int, int>(0, 2);
            var directedEdges = new List<KeyValuePair<int, int>> { edge1 };
            var undirectedEdges = new List<KeyValuePair<int, int>> { edge2, edge3 };
            var directedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge1] = 1,
            };
            var undirectedWeights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [edge2] = 2,
                [edge3] = 3,
            };
            var graph = new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);

            graph.RemoveUndirectedEdge(0, 2);

            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[0]);
            Assert.Equal(new List<int> { 2 }, graph.AdjacencyLists[1]);
            Assert.Equal(new List<int> { 1 }, graph.AdjacencyLists[2]);
            Assert.Equal(new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = 1,
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(2, 1)] = 2,
            }, graph.Weights);
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
