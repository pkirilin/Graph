using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class FloydWarshallTestData
    {
        public static IFloydWarshall<WeightedGraph<int, int>, int> FloydWarshallAlgorithm
            => new FloydWarshall<WeightedGraph<int, int>, int>();

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedDistances = new Dictionary<int, IDictionary<int, int>>()
                {
                    [0] = new Dictionary<int, int>()
                    {
                        [0] = 0,
                        [1] = 7,
                        [2] = 9,
                        [3] = 20,
                        [4] = 20,
                        [5] = 11,
                    },
                    [1] = new Dictionary<int, int>()
                    {
                        [0] = 7,
                        [1] = 0,
                        [2] = 10,
                        [3] = 15,
                        [4] = 21,
                        [5] = 12,
                    },
                    [2] = new Dictionary<int, int>()
                    {
                        [0] = 9,
                        [1] = 10,
                        [2] = 0,
                        [3] = 11,
                        [4] = 11,
                        [5] = 2,
                    },
                    [3] = new Dictionary<int, int>()
                    {
                        [0] = 20,
                        [1] = 15,
                        [2] = 11,
                        [3] = 0,
                        [4] = 6,
                        [5] = 13,
                    },
                    [4] = new Dictionary<int, int>()
                    {
                        [0] = 20,
                        [1] = 21,
                        [2] = 11,
                        [3] = 6,
                        [4] = 0,
                        [5] = 9,
                    },
                    [5] = new Dictionary<int, int>()
                    {
                        [0] = 11,
                        [1] = 12,
                        [2] = 2,
                        [3] = 13,
                        [4] = 9,
                        [5] = 0,
                    },
                };

                yield return new object[] { TestGraphs.Graph2, expectedDistances };
            }
        }
    }
}
