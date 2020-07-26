using Graph.Abstractions.Algorithms;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class LeeTestData
    {
        public static IMazeNavigator<UndirectedGraph<int>, int> LeeAlgorithm
        {
            get
            {
                var dfsAlgorithm = new DepthFirstSearch<UndirectedGraph<int>, int>();
                var connectedComponentsCounter = new ConnectedComponentsCounter<UndirectedGraph<int>, int>(dfsAlgorithm);

                return new Lee<UndirectedGraph<int>, int>(connectedComponentsCounter);
            }
        }

        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedResult = new List<int>()
                {
                    30, 29, 28, 27, 22, 14, 11, 12, 2, 3, 4, 5, 6, 7, 13, 19, 24, 32
                };

                yield return new object[] { TestGraphs.Graph9, 30, 32, expectedResult };
            }
        }

        public static IEnumerable<object[]> MemberData_NotExistingVertex
        {
            get
            {
                yield return new object[] { TestGraphs.Graph9, -1, 32 };
                yield return new object[] { TestGraphs.Graph9, 30, 100 };
                yield return new object[] { TestGraphs.Graph9, -1, 100 };
            }
        }

        public static IEnumerable<object[]> MemberData_WrongConnectedComponentsCount
        {
            get
            {
                yield return new object[] { TestGraphs.Graph8, 0, 2 };
            }
        }
    }
}
