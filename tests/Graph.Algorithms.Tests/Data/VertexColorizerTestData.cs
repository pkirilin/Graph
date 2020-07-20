using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class VertexColorizerTestData
    {
        public static VertexColorizer<UndirectedGraph<int>, int> VertexColorizer
            => new VertexColorizer<UndirectedGraph<int>, int>();

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult1 = new Dictionary<int, int>()
                {
                    [0] = 0,
                    [1] = 1,
                    [2] = 2,
                    [3] = 2,
                    [4] = 0,
                    [5] = 1,
                    [6] = 2,
                };

                var expectedResult2 = new Dictionary<int, int>()
                {
                    [0] = 1,
                    [1] = 2,
                    [2] = 0,
                    [3] = 1,
                    [4] = 1,
                    [5] = 0,
                    [6] = 1,
                    [7] = 2,
                };

                yield return new object[] { TestGraphs.Graph1, expectedResult1 };
                yield return new object[] { TestGraphs.Graph5, expectedResult2 };
            }
        }
    }
}
