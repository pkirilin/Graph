using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// An algorithm for getting graph connected components count
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class ConnectedComponentsCounter<TGraph, TVertex> : IConnectedComponentsCounter<TGraph, TVertex>
        where TGraph : GraphBase<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IGraphSearcher<TGraph, TVertex> _dfsAlgorithm;

        public ConnectedComponentsCounter(IGraphSearcher<TGraph, TVertex> dfsAlgorithm)
        {
            _dfsAlgorithm = dfsAlgorithm ?? throw new ArgumentNullException(nameof(dfsAlgorithm));
        }

        public int Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if (!graph.Vertices.Any())
                return 0;

            var verticesComparer = new GraphVertexEqualityComparer<TVertex>();
            var notVisitedVertices = new HashSet<TVertex>(graph.Vertices, verticesComparer);
            var connectedComponentsCount = 0;

            while (notVisitedVertices.Any())
            {
                var initialVertex = notVisitedVertices.First();

                _dfsAlgorithm.Execute(graph, initialVertex, vertex =>
                {
                    notVisitedVertices.Remove(vertex);
                });

                connectedComponentsCount++;
            }

            return connectedComponentsCount;
        }
    }
}
