using Graph.Abstractions;
using Graph.Structures;
using Moq;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class DijkstraTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult = new Dictionary<int, int>()
                {
                    [0] = 0,
                    [1] = 7,
                    [2] = 9,
                    [3] = 20,
                    [4] = 20,
                    [5] = 11,
                };

                yield return new object[] { TestGraphs.Graph2, 0, expectedResult };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongInitialVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph2, 100 };
            }
        }

        public static IEnumerable<object[]> MemberData_NegativeWeights
        {
            get
            {
                var graphMock = new Mock<WeightedGraph<int, int>>();

                graphMock.SetupGet(g => g.AdjacencyLists)
                    .Returns(new Dictionary<int, IReadOnlyList<int>>()
                    {
                        [0] = new List<int>() { 1 },
                        [1] = new List<int>() { 0 },
                    });

                graphMock.SetupGet(g => g.Weights)
                    .Returns(new Dictionary<Edge<int>, int>()
                    {
                        [new Edge<int>(0, 1)] = -1,
                        [new Edge<int>(1, 0)] = 1,
                    });

                yield return new object[] { graphMock.Object, 0 };
            }
        }
    }
}
