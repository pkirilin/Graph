using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class UndirectedWeightedGraphTestData
    {
        public static UndirectedWeightedGraph<int, int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var edges = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(1, 2)
            };
            var weights = new Dictionary<KeyValuePair<int, int>, int>()
            {
                [new KeyValuePair<int, int>(0, 1)] = 1,
                [new KeyValuePair<int, int>(0, 2)] = 3,
                [new KeyValuePair<int, int>(1, 2)] = 2,
                [new KeyValuePair<int, int>(1, 0)] = 1,
                [new KeyValuePair<int, int>(2, 0)] = 3,
                [new KeyValuePair<int, int>(2, 1)] = 2,
            };

            return new UndirectedWeightedGraph<int, int>(vertices, edges, weights);
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
                    [0] = new List<int>(new int[] { 2 }),
                    [1] = new List<int>(new int[] { 2 }),
                    [2] = new List<int>(new int[] { 0, 1 }),
                    [3] = new List<int>(),
                };
                var expectedWeights1 = new Dictionary<KeyValuePair<int, int>, int>()
                {
                    [new KeyValuePair<int, int>(0, 2)] = 3,
                    [new KeyValuePair<int, int>(2, 0)] = 3,
                    [new KeyValuePair<int, int>(1, 2)] = 2,
                    [new KeyValuePair<int, int>(2, 1)] = 2,
                };

                var expectedAdjacencyLists2 = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int>(new int[] { 1, 2 }),
                    [1] = new List<int>(new int[] { 0, 2 }),
                    [2] = new List<int>(new int[] { 0, 1 }),
                    [3] = new List<int>(),
                };
                var expectedWeights2 = new Dictionary<KeyValuePair<int, int>, int>()
                {
                    [new KeyValuePair<int, int>(0, 1)] = 1,
                    [new KeyValuePair<int, int>(1, 0)] = 1,
                    [new KeyValuePair<int, int>(0, 2)] = 3,
                    [new KeyValuePair<int, int>(2, 0)] = 3,
                    [new KeyValuePair<int, int>(1, 2)] = 2,
                    [new KeyValuePair<int, int>(2, 1)] = 2,
                };

                yield return new object[] { GenerateTestGraph(), 0, 1, expectedAdjacencyLists1, expectedWeights1 };
                yield return new object[] { GenerateTestGraph(), 1, 3, expectedAdjacencyLists2, expectedWeights2 };
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
