using Graph.Abstractions;
using Graph.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public class GraphBaseTests
    {
        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void AddVertex_ShouldAddVertex_WhenUniqueKeySpecified(GraphBase<int> graph)
        {
            graph.AddVertex(0);

            Assert.Contains(0, graph.AdjacencyLists);
            Assert.Empty(graph.AdjacencyLists[0]);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void AddVertex_ShouldThrowArgumentException_WhenDuplicateKeySpecified(GraphBase<int> graph)
        {
            graph.AddVertex(0);
            graph.AddVertex(1);

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddVertex(1);
            });
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void RemoveVertex_ShouldDeleteVertex_WithExistingKey(GraphBase<int> graph)
        {
            graph.AddVertex(0);
            graph.AddVertex(1);

            graph.RemoveVertex(0);

            Assert.DoesNotContain(0, graph.AdjacencyLists);
        }

        [Theory]
        [MemberData(nameof(GraphBaseTestData.MemberData_AvailableGraphs), MemberType = typeof(GraphBaseTestData))]
        public void RemoveVertex_ShouldThrowKeyNotFoundException_WhenKeyDoesNotExist(GraphBase<int> graph)
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                graph.RemoveVertex(1);
            });
        }
    }
}
