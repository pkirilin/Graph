using System.Collections.Generic;

namespace Graph.Algorithms.Tests.Data
{
    static class TestGraphs
    {
        public static UndirectedGraph<int> TwoConnectedVertices
        {
            get
            {
                var vertices = new List<int>() { 0, 1 };

                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1)
                };

                return new UndirectedGraph<int>(vertices, edges);
            }
        }

        public static DirectedGraph<int> DirectedTwoVertexLoop
        {
            get
            {
                var vertices = new List<int>() { 0, 1 };

                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(1, 0),
                };

                return new DirectedGraph<int>(vertices, edges);
            }
        }

        public static UndirectedGraph<int> Graph1
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 3),
                    new Edge<int>(1, 2),
                    new Edge<int>(1, 3),
                    new Edge<int>(1, 4),
                    new Edge<int>(2, 4),
                    new Edge<int>(3, 4),
                    new Edge<int>(3, 5),
                    new Edge<int>(4, 5),
                    new Edge<int>(4, 6),
                    new Edge<int>(5, 6),
                };

                return new UndirectedGraph<int>(vertices, edges);
            }
        }

        public static UndirectedWeightedGraph<int, int> Graph2
        {
            get
            {
                var adjacencyLists = new Dictionary<int, IReadOnlyList<int>>()
                {
                    [0] = new List<int> { 1, 2, 5 },
                    [1] = new List<int> { 0, 2, 3 },
                    [2] = new List<int> { 0, 1, 3, 5 },
                    [3] = new List<int> { 1, 2, 4 },
                    [4] = new List<int> { 3, 5 },
                    [5] = new List<int> { 0, 2, 4 },
                };

                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(0, 5),
                    new Edge<int>(1, 2),
                    new Edge<int>(1, 3),
                    new Edge<int>(2, 3),
                    new Edge<int>(2, 5),
                    new Edge<int>(3, 4),
                    new Edge<int>(4, 5),
                };

                var weights = new Dictionary<Edge<int>, int>()
                {
                    [new Edge<int>(0, 1)] = 7,
                    [new Edge<int>(0, 2)] = 9,
                    [new Edge<int>(0, 5)] = 14,

                    [new Edge<int>(1, 0)] = 7,
                    [new Edge<int>(1, 2)] = 10,
                    [new Edge<int>(1, 3)] = 15,

                    [new Edge<int>(2, 0)] = 9,
                    [new Edge<int>(2, 1)] = 10,
                    [new Edge<int>(2, 3)] = 11,
                    [new Edge<int>(2, 5)] = 2,

                    [new Edge<int>(3, 1)] = 15,
                    [new Edge<int>(3, 2)] = 11,
                    [new Edge<int>(3, 4)] = 6,

                    [new Edge<int>(4, 3)] = 6,
                    [new Edge<int>(4, 5)] = 9,

                    [new Edge<int>(5, 0)] = 14,
                    [new Edge<int>(5, 2)] = 2,
                    [new Edge<int>(5, 4)] = 9,
                };

                return new UndirectedWeightedGraph<int, int>(adjacencyLists.Keys, edges, weights);
            }
        }

        public static UndirectedWeightedGraph<int, int> Graph3
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5 };

                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 2),
                    new Edge<int>(3, 4),
                    new Edge<int>(4, 5),
                };

                var weights = new Dictionary<Edge<int>, int>()
                {
                    [new Edge<int>(0, 1)] = 4,
                    [new Edge<int>(0, 2)] = 9,
                    [new Edge<int>(1, 0)] = 4,
                    [new Edge<int>(1, 2)] = 5,
                    [new Edge<int>(2, 0)] = 9,
                    [new Edge<int>(2, 1)] = 5,
                    [new Edge<int>(3, 4)] = 2,
                    [new Edge<int>(4, 3)] = 2,
                    [new Edge<int>(4, 5)] = 3,
                    [new Edge<int>(5, 4)] = 3,
                };

                return new UndirectedWeightedGraph<int, int>(vertices, edges, weights);
            }
        }

        public static DirectedGraph<int> Graph4
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 3),
                    new Edge<int>(2, 1),
                    new Edge<int>(2, 3),
                    new Edge<int>(2, 6),
                    new Edge<int>(3, 4),
                    new Edge<int>(4, 5),
                    new Edge<int>(6, 5),
                };

                return new DirectedGraph<int>(vertices, edges);
            }
        }

        public static UndirectedGraph<int> Graph5
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 2),
                    new Edge<int>(2, 3),
                    new Edge<int>(2, 4),
                    new Edge<int>(3, 5),
                    new Edge<int>(4, 5),
                    new Edge<int>(5, 6),
                    new Edge<int>(5, 7),
                    new Edge<int>(6, 7),
                };

                return new UndirectedGraph<int>(vertices, edges);
            }
        }

        public static DirectedGraph<int> Graph6
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 5),
                    new Edge<int>(3, 5),
                    new Edge<int>(4, 3),
                    new Edge<int>(4, 5),
                    new Edge<int>(6, 3),
                };

                return new DirectedGraph<int>(vertices, edges);
            }
        }

        public static UndirectedGraph<int> Graph7
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(0, 3),
                    new Edge<int>(1, 2),
                    new Edge<int>(1, 4),
                    new Edge<int>(4, 5),
                    new Edge<int>(4, 7),
                    new Edge<int>(5, 6),
                    new Edge<int>(6, 7),
                };

                return new UndirectedGraph<int>(vertices, edges);
            }
        }

        public static UndirectedGraph<int> Graph8
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(0, 3),
                    new Edge<int>(1, 2),
                    new Edge<int>(4, 5),
                    new Edge<int>(4, 7),
                    new Edge<int>(5, 6),
                    new Edge<int>(6, 7),
                };

                return new UndirectedGraph<int>(vertices, edges);
            }
        }
    }
}
