using Graph.Structures;
using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class MixedWeightedGraphTestData
    {
        public static MixedWeightedGraph<int, int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2, 3 };
            var directedEdges = new List<Edge<int>>
            {
                new Edge<int>(0, 1),
            };
            var undirectedEdges = new List<Edge<int>>
            {
                new Edge<int>(1, 2),
                new Edge<int>(0, 2)
            };
            var directedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(0, 1)] = 1,
            };
            var undirectedWeights = new Dictionary<Edge<int>, int>()
            {
                [new Edge<int>(1, 2)] = 2,
                [new Edge<int>(2, 1)] = 2,
                [new Edge<int>(0, 2)] = 3,
                [new Edge<int>(2, 0)] = 3,
            };

            return new MixedWeightedGraph<int, int>(vertices, directedEdges, undirectedEdges, directedWeights, undirectedWeights);
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
    }
}
