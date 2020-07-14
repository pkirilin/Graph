using Graph.Abstractions.Algorithms;
using Moq;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class PrimTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var graph = TestGraphs.Graph2;
                var connectedComponentsCounterMock = new Mock<IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int>>();
                var expectedSpanningTreeEdges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(2, 5),
                    new Edge<int>(5, 4),
                    new Edge<int>(4, 3),
                };

                connectedComponentsCounterMock.Setup(c => c.Execute(graph)).Returns(1);

                yield return new object[]
                {
                    graph,
                    connectedComponentsCounterMock.Object,
                    expectedSpanningTreeEdges
                };
            }
        }

        public static IEnumerable<object[]> MemberData_InvalidConnectedComponentsCount
        {
            get
            {
                var graph = TestGraphs.Graph2;
                var connectedComponentsCounterMock1 = new Mock<IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int>>();
                var connectedComponentsCounterMock2 = new Mock<IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int>>();

                connectedComponentsCounterMock1.Setup(c => c.Execute(graph)).Returns(2);
                connectedComponentsCounterMock2.Setup(c => c.Execute(graph)).Returns(3);

                yield return new object[] { graph, connectedComponentsCounterMock1.Object };
                yield return new object[] { graph, connectedComponentsCounterMock2.Object };
            }
        }

        public static IEnumerable<object[]> MemberData_EmptyVerticesOrEdges
        {
            get
            {
                var graphMock1 = new Mock<UndirectedWeightedGraph<int, int>>();
                var graphMock2 = new Mock<UndirectedWeightedGraph<int, int>>();
                var connectedComponentsCounterMock = new Mock<IFunctionAlgorithm<UndirectedWeightedGraph<int, int>, int, int>>();

                connectedComponentsCounterMock
                    .Setup(c => c.Execute(It.IsNotNull<UndirectedWeightedGraph<int, int>>()))
                    .Returns(0);

                yield return new object[] { graphMock1.Object, connectedComponentsCounterMock.Object };
                yield return new object[] { graphMock2.Object, connectedComponentsCounterMock.Object };
            }
        }
    }
}
