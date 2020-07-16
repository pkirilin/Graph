using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class UndirectedGraphTestData
    {
        public static UndirectedGraph<int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edges = new List<Edge<int>>()
            {
                new Edge<int>(0, 1),
                new Edge<int>(0, 2),
                new Edge<int>(1, 2)
            };

            return new UndirectedGraph<int>(vertices, edges);
        }

        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_KeyNotFoundException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 100, 0 };
                yield return new object[] { GenerateTestGraph(), 1, 100 };
                yield return new object[] { GenerateTestGraph(), 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_AddUndirectedEdge_InvalidOperationException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 0, 0 };
                yield return new object[] { GenerateTestGraph(), 1, 2 };
                yield return new object[] { GenerateTestGraph(), 2, 1 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveUndirectedEdge_KeyNotFoundException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 100, 0 };
                yield return new object[] { GenerateTestGraph(), 1, 100 };
                yield return new object[] { GenerateTestGraph(), 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveUndirectedEdge
        {
            get
            {
                var expectedAdjacencyLists1 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 2 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 0, 1 },
                    [3] = new List<int>() { },
                };

                var expectedAdjacencyLists2 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1, 2 },
                    [1] = new List<int>() { 0, 2 },
                    [2] = new List<int>() { 0, 1 },
                    [3] = new List<int>() { },
                };

                yield return new object[] { GenerateTestGraph(), 0, 1, expectedAdjacencyLists1 };
                yield return new object[] { GenerateTestGraph(), 1, 3, expectedAdjacencyLists2 };
            }
        }
    }
}
