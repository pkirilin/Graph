﻿using System.Collections.Generic;

namespace Graph.Tests.Data
{
    static class DirectedGraphTestData
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

                var graph = new DirectedGraph<int>(vertices, edges);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 2, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }

        public static IEnumerable<object[]> MemberData_RemoveDirectedEdge_KeyNotFoundException
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

                var graph = new DirectedGraph<int>(vertices, edges);

                yield return new object[] { graph, 100, 0 };
                yield return new object[] { graph, 2, 100 };
                yield return new object[] { graph, 101, 102 };
            }
        }
    }
}
