using Graph.Abstractions.Algorithms;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class ArticulationPointSearcherTestData
    {
        public static IArticulationPointSearcher<UndirectedGraph<int>, int> ArticulationPointSearcher
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<UndirectedGraph<int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<UndirectedGraph<int>, int>(dfsAlgorithm);

                return new ArticulationPointSearcher<UndirectedGraph<int>, int>(connectedComponentsCounter);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                yield return new object[]
                {
                    TestGraphs.Graph7,
                    new List<int>() { 0, 1, 4 }
                };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongConnectedComponentsCount
        {
            get
            {
                yield return new object[] { TestGraphs.Graph8 };
            }
        }
    }
}
