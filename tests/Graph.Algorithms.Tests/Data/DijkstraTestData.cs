using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class DijkstraTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult = new Dictionary<int, int>()
                {
                    [0] = 0,
                    [1] = 7,
                    [2] = 9,
                    [3] = 20,
                    [4] = 20,
                    [5] = 11,
                };

                yield return new object[] { TestGraphs.Graph2, 0, expectedResult };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongInitialVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph2, 100 };
            }
        }

        public static IEnumerable<object[]> MemberData_NegativeWeights
        {
            get
            {
                var vertices = new List<int>() { 0, 1 };
                var edges = new List<Edge<int>>() { new Edge<int>(0, 1) };
                var weights = new Dictionary<Edge<int>, int>()
                {
                    [new Edge<int>(0, 1)] = -1,
                };

                var graph = new UndirectedWeightedGraph<int, int>(vertices, edges, weights);

                yield return new object[] { graph, 0 };
            }
        }

        public static IEnumerable<object[]> MemberData_ArgumentNullException
        {
            get
            {
                yield return new object[] { null, 0 };
            }
        }
    }
}
