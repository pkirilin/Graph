using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class CyclesDetectorTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                yield return new object[] { TestGraphs.Graph3, 0, true };
                yield return new object[] { TestGraphs.Graph3, 5, false };
                yield return new object[] { TestGraphs.TwoConnectedVertices, 0, false };
                yield return new object[] { TestGraphs.Graph4, 0, false };
                yield return new object[] { TestGraphs.Graph1, 0, true };
                yield return new object[] { TestGraphs.Graph6, 4, false };
                yield return new object[] { TestGraphs.Graph6, 0, false };
                yield return new object[] { TestGraphs.DirectedTwoVertexLoop, 0, true };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongInitialVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph3, 100 };
                yield return new object[] { TestGraphs.TwoConnectedVertices, 3 };
                yield return new object[] { TestGraphs.Graph1, 200 };
            }
        }
    }
}
