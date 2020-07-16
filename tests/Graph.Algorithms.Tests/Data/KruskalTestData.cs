using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class KruskalTestData
    {
        public static Kruskal<UndirectedWeightedGraph<int, int>, int> KruskalAlgorithm
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<UndirectedWeightedGraph<int, int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<UndirectedWeightedGraph<int, int>, int>(dfsAlgorithm);
                var cyclesDetector = new CyclesDetector<UndirectedWeightedGraph<int, int>, int>();
                var kruskalAlgorithm = new Kruskal<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounter, cyclesDetector);

                return kruskalAlgorithm;
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult = new List<Edge<int>>()
                {
                    new Edge<int>(2, 5),
                    new Edge<int>(3, 4),
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(4, 5),
                };

                yield return new object[] { TestGraphs.Graph2, expectedResult };
            }
        }

        public static IEnumerable<object[]> MemberData_InvalidConnectedComponentsCount
        {
            get
            {
                yield return new object[] { TestGraphs.Graph3 };
            }
        }
    }
}
