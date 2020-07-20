using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class TopologicalSorterTestData
    {
        public static TopologicalSorter<DirectedGraph<int>, int> TopologicalSorter
        {
            get
            {
                var cyclesDetector = new CyclesDetector<DirectedGraph<int>, int>();

                return new TopologicalSorter<DirectedGraph<int>, int>(cyclesDetector);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult1 = new List<int>() { 6, 4, 3, 0, 1, 5, 2 };
                var expectedResult2 = new List<int>() { 4, 0, 1, 2, 6, 3, 5 };
                var expectedResult3 = new List<int>() { 6, 4, 3, 0, 1, 2, 5 };

                yield return new object[] { TestGraphs.Graph6, 0, expectedResult1 };
                yield return new object[] { TestGraphs.Graph6, 6, expectedResult2 };
                yield return new object[] { TestGraphs.Graph6, 5, expectedResult3 };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongInitialVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph6, -1 };
                yield return new object[] { TestGraphs.Graph6, 100 };
            }
        }

        public static IEnumerable<object[]> MemberData_GraphWithCycles
        {
            get
            {
                var graph = TestGraphs.Graph6;
                graph.AddDirectedEdge(5, 4);

                yield return new object[] { graph, 0 };
            }
        }
    }
}
