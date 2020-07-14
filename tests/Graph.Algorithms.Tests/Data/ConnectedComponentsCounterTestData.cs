using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class ConnectedComponentsCounterTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                yield return new object[] { TestGraphs.EmptyUndirectedWeightedGraph, 0 };
                yield return new object[] { TestGraphs.Graph2, 1 };
                yield return new object[] { TestGraphs.Graph3, 2 };
            }
        }
    }
}
