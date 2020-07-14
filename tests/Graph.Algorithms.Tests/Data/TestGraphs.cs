using Moq;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class TestGraphs
    {
        public static UndirectedWeightedGraph<int, int> EmptyUndirectedWeightedGraph
        {
            get
            {
                var graphMock = new Mock<UndirectedWeightedGraph<int, int>>();

                graphMock.SetupGet(g => g.AdjacencyLists).Returns(new Dictionary<int, IReadOnlyList<int>>());
                graphMock.SetupGet(g => g.Vertices).Returns(new List<int>());
                graphMock.SetupGet(g => g.Weights).Returns(new Dictionary<Edge<int>, int>());

                return graphMock.Object;
            }
        }

        public static UndirectedGraph<int> Graph1
        {
            get
            {
                var graphMock = new Mock<UndirectedGraph<int>>();
                var adjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int> { 1, 3 },
                    [1] = new List<int> { 0, 2, 3, 4 },
                    [2] = new List<int> { 1, 4 },
                    [3] = new List<int> { 0, 1, 4, 5 },
                    [4] = new List<int> { 1, 2, 3, 5, 6 },
                    [5] = new List<int> { 3, 4, 6 },
                    [6] = new List<int> { 4, 5 },
                };

                graphMock.SetupGet(g => g.AdjacencyLists).Returns(adjacencyLists);
                graphMock.SetupGet(g => g.Vertices).Returns(adjacencyLists.Keys);

                return graphMock.Object;
            }
        }

        public static UndirectedWeightedGraph<int, int> Graph2
        {
            get
            {
                var graphMock = new Mock<UndirectedWeightedGraph<int, int>>();
                var adjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int> { 1, 2, 5 },
                    [1] = new List<int> { 0, 2, 3 },
                    [2] = new List<int> { 0, 1, 3, 5 },
                    [3] = new List<int> { 1, 2, 4 },
                    [4] = new List<int> { 3, 5 },
                    [5] = new List<int> { 0, 2, 4 },
                };
                var weights = new Dictionary<Edge<int>, int>()
                {
                    [new Edge<int>(0, 1)] = 7,
                    [new Edge<int>(0, 2)] = 9,
                    [new Edge<int>(0, 5)] = 14,

                    [new Edge<int>(1, 0)] = 7,
                    [new Edge<int>(1, 2)] = 10,
                    [new Edge<int>(1, 3)] = 15,

                    [new Edge<int>(2, 0)] = 9,
                    [new Edge<int>(2, 1)] = 10,
                    [new Edge<int>(2, 3)] = 11,
                    [new Edge<int>(2, 5)] = 2,

                    [new Edge<int>(3, 1)] = 15,
                    [new Edge<int>(3, 2)] = 11,
                    [new Edge<int>(3, 4)] = 6,

                    [new Edge<int>(4, 3)] = 6,
                    [new Edge<int>(4, 5)] = 9,

                    [new Edge<int>(5, 0)] = 14,
                    [new Edge<int>(5, 2)] = 2,
                    [new Edge<int>(5, 4)] = 9,
                };

                graphMock.SetupGet(g => g.AdjacencyLists).Returns(adjacencyLists);
                graphMock.SetupGet(g => g.Vertices).Returns(adjacencyLists.Keys);
                graphMock.SetupGet(g => g.Weights).Returns(weights);

                return graphMock.Object;
            }
        }

        public static UndirectedWeightedGraph<int, int> Graph3
        {
            get
            {
                var graphMock = new Mock<UndirectedWeightedGraph<int, int>>();
                var adjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int> { 1, 2 },
                    [1] = new List<int> { 0, 2 },
                    [2] = new List<int> { 0, 1 },
                    [3] = new List<int> { 4 },
                    [4] = new List<int> { 3, 5 },
                    [5] = new List<int> { 4 },
                };
                var weights = new Dictionary<Edge<int>, int>()
                {
                    [new Edge<int>(0, 1)] = 4,
                    [new Edge<int>(0, 2)] = 9,

                    [new Edge<int>(1, 0)] = 4,
                    [new Edge<int>(1, 2)] = 5,

                    [new Edge<int>(2, 0)] = 9,
                    [new Edge<int>(2, 1)] = 5,

                    [new Edge<int>(3, 4)] = 2,

                    [new Edge<int>(4, 3)] = 2,
                    [new Edge<int>(4, 5)] = 3,

                    [new Edge<int>(5, 4)] = 3,
                };

                graphMock.SetupGet(g => g.AdjacencyLists).Returns(adjacencyLists);
                graphMock.SetupGet(g => g.Vertices).Returns(adjacencyLists.Keys);
                graphMock.SetupGet(g => g.Weights).Returns(weights);

                return graphMock.Object;
            }
        }
    }
}
