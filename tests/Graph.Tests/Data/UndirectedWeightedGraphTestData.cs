using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class UndirectedWeightedGraphTestData
    {
        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var edges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 1, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_InvalidOperationException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var edges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

                yield return new object[] { graph, 0, 0 };
                yield return new object[] { graph, 1, 2 };
                yield return new object[] { graph, 2, 1 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveUndirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var edges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 1, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }
    }
}
