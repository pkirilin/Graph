using System.Collections.Generic;
using System.Linq;

namespace Graph.Tests.Data
{
    static class GraphBaseTestData
    {
        public static IEnumerable<object[]> MemberData_AvailableGraphs
        {
            get
            {
                var directedGraph = new DirectedGraph<int>();
                var undirectedGraph = new UndirectedGraph<int>();
                var mixedGraph = new MixedGraph<int>();

                yield return new object[] { directedGraph };
                yield return new object[] { undirectedGraph };
                yield return new object[] { mixedGraph };
            }
        }

        public static IEnumerable<object[]> MemberData_Edges
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 2),
                };
                var allEdges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 0),
                    new Edge<int>(1, 2),
                    new Edge<int>(2, 0),
                    new Edge<int>(2, 1),
                };

                var directedGraph = new DirectedGraph<int>(vertices, edges);
                var directedWeightedGraph = new DirectedWeightedGraph<int, int>(vertices, edges);
                var undirectedGraph = new UndirectedGraph<int>(vertices, edges);
                var undirectedWeightedGraph = new UndirectedWeightedGraph<int, int>(vertices, edges);
                var mixedGraph = new MixedGraph<int>(vertices, Enumerable.Empty<Edge<int>>(), edges);
                var mixedWeightedGraph = new MixedWeightedGraph<int, int>(vertices, Enumerable.Empty<Edge<int>>(), edges);

                yield return new object[] { directedGraph, edges };
                yield return new object[] { directedWeightedGraph, edges };
                yield return new object[] { undirectedGraph, allEdges };
                yield return new object[] { undirectedWeightedGraph, allEdges };
                yield return new object[] { mixedGraph, allEdges };
                yield return new object[] { mixedWeightedGraph, allEdges };
            }
        }

        public static IEnumerable<object[]> MemberData_ReducedEdges
        {
            get
            {
                var vertices = new List<int>() { 0, 1, 2 };
                var edges = new List<Edge<int>>()
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                    new Edge<int>(1, 2),
                };

                var undirectedGraph = new UndirectedGraph<int>(vertices, edges);
                var undirectedWeightedGraph = new UndirectedWeightedGraph<int, int>(vertices, edges);
                var mixedGraph = new MixedGraph<int>(vertices, Enumerable.Empty<Edge<int>>(), edges);
                var mixedWeightedGraph = new MixedWeightedGraph<int, int>(vertices, Enumerable.Empty<Edge<int>>(), edges);

                yield return new object[] { undirectedGraph, edges };
                yield return new object[] { undirectedWeightedGraph, edges };
                yield return new object[] { mixedGraph, edges };
                yield return new object[] { mixedWeightedGraph, edges };
            }
        }

        public static IEnumerable<object[]> MemberData_GetVertexDeg
        {
            get
            {
                var graph = new UndirectedGraph<int>(new int[] { 0, 1, 2, 3 }, new Edge<int>[]
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                });

                yield return new object[] { graph, 0, 2 };
                yield return new object[] { graph, 1, 1 };
                yield return new object[] { graph, 3, 0 };
            }
        }

        public static IEnumerable<object[]> MemberData_GetVertexDeg_WrongVertex
        {
            get
            {
                var graph = new UndirectedGraph<int>(new int[] { 0, 1, 2, 3 }, new Edge<int>[]
                {
                    new Edge<int>(0, 1),
                    new Edge<int>(0, 2),
                });

                yield return new object[] { graph, -1 };
                yield return new object[] { graph, 100 };
            }
        }
    }
}
