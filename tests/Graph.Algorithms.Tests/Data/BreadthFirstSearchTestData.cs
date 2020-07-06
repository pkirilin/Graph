using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class BreadthFirstSearchTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedVisitedVertices1 = new List<int> { 0, 1, 3, 2, 4, 5, 6 };

                yield return new object[] { TestGraphs.Graph1, 0, expectedVisitedVertices1 };
            }
        }

        public static IEnumerable<object[]> MemberData_Execute_ArgumentException
        {
            get
            {
                yield return new object[] { TestGraphs.Graph1, -1 };
                yield return new object[] { TestGraphs.Graph1, 100 };
            }
        }
    }
}
