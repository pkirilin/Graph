using Graph.Abstractions.Algorithms;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms.Tests.Data
{
    static class BridgeSearcherTestData
    {
        public static IBridgeSearcher<UndirectedGraph<int>, int> BridgeSearcher
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<UndirectedGraph<int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<UndirectedGraph<int>, int>(dfsAlgorithm);

                return new BridgeSearcher<UndirectedGraph<int>, int>(connectedComponentsCounter);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                yield return new object[]
                {
                    TestGraphs.Graph7,
                    new List<Edge<int>>()
                    {
                        new Edge<int>(3, 0),
                        new Edge<int>(4, 1),
                    }
                };

                yield return new object[]
                {
                    new UndirectedGraph<int>(),
                    Enumerable.Empty<Edge<int>>()
                };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongConnectedComponentsCount
        {
            get
            {
                yield return new object[] { TestGraphs.Graph8 };
            }
        }
    }
}
