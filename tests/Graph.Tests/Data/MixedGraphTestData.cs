using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class MixedGraphTestData
    {
        public static MixedGraph<int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var directedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 1),
            };
            var undirectedEdges = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(0, 2)
            };

            return new MixedGraph<int>(vertices, directedEdges, undirectedEdges);
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

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge
        {
            get
            {
                var expectedAdjacencyLists1 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 2 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 1, 0 },
                    [3] = new List<int>() { },
                };
                var expectedAdjacencyLists2 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1, 2 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 1, 0 },
                    [3] = new List<int>() { },
                };

                yield return new object[] { GenerateTestGraph(), 0, 1, expectedAdjacencyLists1 };
                yield return new object[] { GenerateTestGraph(), 1, 3, expectedAdjacencyLists2 };
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

        public static IEnumerable<object[]> MemberData_RemoveUndirectedEdge
        {
            get
            {
                var expectedAdjacencyLists1 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 1 },
                    [3] = new List<int>() { },
                };

                var expectedAdjacencyLists2 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>() { 1, 2 },
                    [1] = new List<int>() { 2 },
                    [2] = new List<int>() { 1, 0 },
                    [3] = new List<int>() { },
                };

                yield return new object[] { GenerateTestGraph(), 0, 2, expectedAdjacencyLists1 };
                yield return new object[] { GenerateTestGraph(), 1, 3, expectedAdjacencyLists2 };
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
    }
}
