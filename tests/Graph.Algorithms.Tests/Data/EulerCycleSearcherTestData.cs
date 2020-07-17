using Graph.Abstractions;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class EulerCycleSearcherTestData
    {
        public static EulerCycleSearcher<GraphBase<int>, int> EulerCycleSearcher
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<GraphBase<int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<GraphBase<int>, int>(dfsAlgorithm);
                
                return new EulerCycleSearcher<GraphBase<int>, int>(connectedComponentsCounter);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult = new List<int>() { 0, 1, 2, 3, 5, 6, 7, 5, 4, 2, 0 };

                yield return new object[] { TestGraphs.Graph5, 0, expectedResult };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongInitialVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph5, -1 };
                yield return new object[] { TestGraphs.Graph5, 100 };
            }
        }

        public static IEnumerable<object[]> MemberData_NotEulerGraph
        {
            get
            {
                yield return new object[] { TestGraphs.Graph1, 0 };
                yield return new object[] { TestGraphs.Graph3, 0 };
            }
        }
    }
}
