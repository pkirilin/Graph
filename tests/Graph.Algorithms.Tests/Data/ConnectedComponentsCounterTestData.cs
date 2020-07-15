using Graph.Abstractions;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class ConnectedComponentsCounterTestData
    {
        public static ConnectedComponentsCounter<GraphBase<int>, int> ConnectedComponentsCounter
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<GraphBase<int>, int>();

                return new ConnectedComponentsCounter<GraphBase<int>, int>(dfsAlgorithm);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                yield return new object[] { new UndirectedWeightedGraph<int, int>(), 0 };
                yield return new object[] { TestGraphs.Graph2, 1 };
                yield return new object[] { TestGraphs.Graph3, 2 };
            }
        }
    }
}
