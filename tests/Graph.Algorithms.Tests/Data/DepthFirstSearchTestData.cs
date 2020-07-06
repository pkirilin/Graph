﻿using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class DepthFirstSearchTestData
    {
        public static IEnumerable<object[]> MemberData_Execute
        {
            get
            {
                var expectedVisitedVertices1 = new List<int> { 0, 3, 5, 6, 4, 2, 1 };

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
