using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class UndirectedGraphTestData
    {
        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_KeyNotFoundException
        {
            get
            {
                var graph = new UndirectedGraph<int>();

                graph.AddVertex(0);
                graph.AddVertex(1);
                graph.AddVertex(2);

                graph.AddUndirectedEdge(0, 1);
                graph.AddUndirectedEdge(0, 2);
                graph.AddUndirectedEdge(1, 2);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 1, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveUndirectedEdge_KeyNotFoundException
        {
            get
            {
                var graph = new UndirectedGraph<int>();

                graph.AddVertex(0);
                graph.AddVertex(1);
                graph.AddVertex(2);

                graph.AddUndirectedEdge(0, 1);
                graph.AddUndirectedEdge(0, 2);
                graph.AddUndirectedEdge(1, 2);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 1, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }
    }
}
