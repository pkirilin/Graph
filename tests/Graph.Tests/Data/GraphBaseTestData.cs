using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class GraphBaseTestData
    {
        public static IEnumerable<object[]> MemberData_AvailableGraphs
        {
            get
            {
                var directedGraph = new DirectedGraph<int>();
                var undirectedGraph = new UndirectedGraph<int>();
                var mixedGraph = new MixedGraph<int>();

                yield return new object[] { directedGraph };
                yield return new object[] { undirectedGraph };
                yield return new object[] { mixedGraph };
            }
        }
    }
}
