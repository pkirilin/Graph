using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class MixedGraphTestData
    {
        public static IEnumerable<object[]> MemberData_AddDirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var directedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 0),
                    new KeyValuePair<int, int>(2, 1)
                };

                var graph = new MixedGraph<int>(vertices, directedEdges, new List<KeyValuePair<int, int>>());

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 2, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_AddDirectedEdge_InvalidOperationException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var directedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 0),
                    new KeyValuePair<int, int>(2, 1)
                };

                var graph = new MixedGraph<int>(vertices, directedEdges, new List<KeyValuePair<int, int>>());

                yield return new object[] { graph, 0, 0 };
                yield return new object[] { graph, 1, 2 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var directedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 0),
                    new KeyValuePair<int, int>(2, 1)
                };

                var graph = new MixedGraph<int>(vertices, directedEdges, new List<KeyValuePair<int, int>>());

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 2, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var undirectedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new MixedGraph<int>(vertices, new List<KeyValuePair<int, int>>(), undirectedEdges);

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
                var undirectedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new MixedGraph<int>(vertices, new List<KeyValuePair<int, int>>(), undirectedEdges);

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
                var undirectedEdges = new List<KeyValuePair<int, int>>()
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(0, 2),
                    new KeyValuePair<int, int>(1, 2)
                };

                var graph = new MixedGraph<int>(vertices, new List<KeyValuePair<int, int>>(), undirectedEdges);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 1, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }
    }
}
