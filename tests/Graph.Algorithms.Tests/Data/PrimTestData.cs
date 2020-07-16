using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class PrimTestData
    {
        public static Prim<UndirectedWeightedGraph<int, int>, int> PrimAlgorithm
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<UndirectedWeightedGraph<int, int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<UndirectedWeightedGraph<int, int>, int>(dfsAlgorithm);

                return new Prim<UndirectedWeightedGraph<int, int>, int>(connectedComponentsCounter);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedSpanningTreeEdges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(2, 5),
                    new Edge<int>(5, 4),
                    new Edge<int>(4, 3),
                };

                yield return new object[] { TestGraphs.Graph2, expectedSpanningTreeEdges };
            }
        }

        public static IEnumerable<object[]> MemberData_InvalidConnectedComponentsCount
        {
            get
            {
                yield return new object[] { TestGraphs.Graph3 };
            }
        }

        public static IEnumerable<object[]> MemberData_EmptyVerticesOrEdges
        {
            get
            {
                yield return new object[] { new UndirectedWeightedGraph<int, int>() };
            }
        }
    }
}
