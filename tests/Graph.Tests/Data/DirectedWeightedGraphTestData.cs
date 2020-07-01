using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class DirectedWeightedGraphTestData
    {
        public static IEnumerable<object[]> MemberData_AddDirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var edges = new List<KeyValuePair<int, int>>
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 0),
                    new KeyValuePair<int, int>(2, 1)
                };

                var graph = new DirectedWeightedGraph<int, int>(vertices, edges);

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
                var edges = new List<KeyValuePair<int, int>>
                {
                    new KeyValuePair<int, int>(0, 1),
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 0),
                    new KeyValuePair<int, int>(2, 1)
                };

                var graph = new DirectedWeightedGraph<int, int>(vertices, edges);

                yield return new object[] { graph, 0, 0 };
                yield return new object[] { graph, 1, 2 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge_KeyNotFoundException
        {
            get
            {
                var vertices = new List<int> { 0, 1, 2 };
                var edge1 = new KeyValuePair<int, int>(0, 1);
                var edge2 = new KeyValuePair<int, int>(1, 2);
                var edge3 = new KeyValuePair<int, int>(2, 0);
                var edge4 = new KeyValuePair<int, int>(2, 1);
                var edges = new List<KeyValuePair<int, int>> { edge1, edge2, edge3, edge4 };
                var weights = new Dictionary<KeyValuePair<int, int>, int>()
                {
                    [edge1] = 1,
                    [edge2] = 2,
                    [edge3] = 3,
                };

                var graph = new DirectedWeightedGraph<int, int>(vertices, edges, weights);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 2, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }
    }
}
