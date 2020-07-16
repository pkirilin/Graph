using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class DirectedGraphTestData
    {
        public static DirectedGraph<int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edges = new List<Edge<int>>()
            {
                new Edge<int>(0, 1),
                new Edge<int>(1, 2),
                new Edge<int>(2, 0),
                new Edge<int>(2, 1)
            };

            return new DirectedGraph<int>(vertices, edges);
        }

        public static IEnumerable<object[]> MemberData_AddDirectedEdge_KeyNotFoundException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 100, 0 };
                yield return new object[] { GenerateTestGraph(), 2, 100 };
                yield return new object[] { GenerateTestGraph(), 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_AddDirectedEdge_InvalidOperationException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 0, 0 };
                yield return new object[] { GenerateTestGraph(), 1, 2 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge_KeyNotFoundException
        {
            get
            {
                yield return new object[] { GenerateTestGraph(), 100, 0 };
                yield return new object[] { GenerateTestGraph(), 2, 100 };
                yield return new object[] { GenerateTestGraph(), 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge
        {
            get
            {
                var expectedAdjacencyLists1 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 0, 1 },
                };
                var expectedAdjacencyLists2 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 0 },
                };
                var expectedAdjacencyLists3 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 0, 1 },
                };

                yield return new object[] { GenerateTestGraph(), 0, 1, expectedAdjacencyLists1 };
                yield return new object[] { GenerateTestGraph(), 2, 1, expectedAdjacencyLists2 };
                yield return new object[] { GenerateTestGraph(), 1, 0, expectedAdjacencyLists3 };
            }
        }
    }
}
