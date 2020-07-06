﻿using Moq;
using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class TestGraphs
    {
        public static UndirectedGraph<int> Graph1
        {
            get
            {
                var graphMock = new Mock<UndirectedGraph<int>>();

                graphMock.SetupGet(g => g.AdjacencyLists)
                    .Returns(new Dictionary<int, IReadOnlyList<int>>()
                    {
                        [0] = new List<int> { 1, 3 },
                        [1] = new List<int> { 0, 2, 3, 4 },
                        [2] = new List<int> { 1, 4 },
                        [3] = new List<int> { 0, 1, 4, 5 },
                        [4] = new List<int> { 1, 2, 3, 5, 6 },
                        [5] = new List<int> { 3, 4, 6 },
                        [6] = new List<int> { 4, 5 },
                    });

                return graphMock.Object;
            }
        }
    }
}
