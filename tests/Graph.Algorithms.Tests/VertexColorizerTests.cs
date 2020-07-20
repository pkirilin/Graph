using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class VertexColorizerTests
    {
        [Theory]
        [MemberData(nameof(VertexColorizerTestData.MemberData_Execute), MemberType = typeof(VertexColorizerTestData))]
        public void VertexColoringAlgorithm_ShouldColorizeGraphVerticesWithMinimalNumberOfColors(UndirectedGraph<int> graph, IDictionary<int, int> expectedResult)
        {
            var vertexColorizer = VertexColorizerTestData.VertexColorizer;

            var result = vertexColorizer.Execute(graph);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void VertexColoringAlgorithm_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var vertexColorizer = VertexColorizerTestData.VertexColorizer;

            Assert.Throws<ArgumentNullException>(() =>
            {
                vertexColorizer.Execute(null);
            });
        }
    }
}
