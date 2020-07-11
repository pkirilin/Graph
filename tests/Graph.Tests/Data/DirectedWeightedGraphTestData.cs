using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class DirectedWeightedGraphTestData
    {
        public static DirectedWeightedGraph<int, int> GenerateTestGraph()
        {
            var vertices = new List<int> { 0, 1, 2 };
            var edge1 = new Edge<int>(0, 1);
            var edge2 = new Edge<int>(1, 2);
            var edge3 = new Edge<int>(2, 0);
            var edge4 = new Edge<int>(2, 1);
            var edges = new List<Edge<int>>() { edge1, edge2, edge3, edge4 };
            var weights = new Dictionary<Edge<int>, int>()
            {
                [edge1] = 1,
                [edge2] = 3,
                [edge3] = 4,
                [edge4] = 2,
            };

            return new DirectedWeightedGraph<int, int>(vertices, edges, weights);
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
    }
}
