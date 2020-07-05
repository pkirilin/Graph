using Graph.Abstractions;
using Moq;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class DepthFirstSearchTestData
    {
        public static GraphBase<int> SetupGraph()
        {
            var graphMock = new Mock<GraphBase<int>>();

            graphMock.SetupGet(g => g.AdjacencyLists)
                .Returns(new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int> { 1, 3 },
                    [1] = new List<int> { 0, 2, 3, 4 },
                    [2] = new List<int> { 1, 4 },
                    [3] = new List<int> { 0, 1, 4, 5 },
                    [4] = new List<int> { 1, 2, 3, 5, 6 },
                    [5] = new List<int> { 3, 4, 6 },
                    [6] = new List<int> { 4, 5 },
                });

            return graphMock.Object;
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var graph1 = SetupGraph();
                var expectedVisitedVertices1 = new List<int> { 0, 3, 5, 6, 4, 2, 1 };

                yield return new object[] { graph1, 0, expectedVisitedVertices1 };
            }
        }

        public static IEnumerable<object[]> MemberData_Execute_ArgumentException
        {
            get
            {
                var graph1 = SetupGraph();

                yield return new object[] { graph1, -1 };
                yield return new object[] { graph1, 100 };
            }
        }
    }
}
