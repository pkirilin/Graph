using Graph.Abstractions;
using Graph.Abstractions.Algorithms;
using Graph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    /// Graph vertex coloring greedy algorithm implementation.
    /// Vertex coloring is a way of coloring the vertices of a graph, in which any two adjacent vertices correspond to different colors.
    /// The number of colors should be minimal.
    /// Algorithm returns a dictionary where keys are vertices and values are colors of corresponding key vertex.
    /// </summary>
    /// <typeparam name="TGraph">Graph type</typeparam>
    /// <typeparam name="TVertex">Graph vertex type</typeparam>
    public class VertexColorizer<TGraph, TVertex> : IVertexColorizer<TGraph, TVertex>
        where TGraph : Graph<TVertex>, IUndirectedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        private readonly IEqualityComparer<TVertex> _verticesComparer;

        public VertexColorizer()
        {
            _verticesComparer = new GraphVertexEqualityComparer<TVertex>();
        }

        public IDictionary<TVertex, int> Execute(TGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            // Vertices that are going to be colored (sorted by vertex degs descending)
            var notColoredVertices = graph.Vertices
                .OrderByDescending(v => graph.GetVertexDeg(v))
                .ToList();
            // A dictionary for mapping vertices to their colors
            var verticesAndColors = new Dictionary<TVertex, int>(_verticesComparer);
            // For simplicity, an integer value is used to represent a color
            var curColorId = 0;

            while (notColoredVertices.Any())
            {
                // Set current color to first uncolored vertex (it's a vertex with max deg)
                SetColorToVertex(notColoredVertices.First(), curColorId, verticesAndColors, notColoredVertices);
                
                var verticesToPaintInTheSameColor = notColoredVertices.ToList();

                // View all other vertices and "paint" with current color those of them that are not adjacent to any vertex painted with it
                while (verticesToPaintInTheSameColor.Any())
                {
                    var curVertex = verticesToPaintInTheSameColor.First();

                    if (CanVertexBeColored(graph, curVertex, curColorId, verticesAndColors))
                        SetColorToVertex(curVertex, curColorId, verticesAndColors, notColoredVertices);
                    // Excluding already viewed vertices
                    verticesToPaintInTheSameColor.Remove(curVertex);
                }

                // Move to next color
                curColorId++;
            }

            return verticesAndColors;
        }

        /// <summary>
        /// Determines whether a vertex can be "painted" in specified color
        /// </summary>
        /// <param name="graph">Target graph</param>
        /// <param name="vertex">Target vertex</param>
        /// <param name="curColorId">Target color</param>
        /// <param name="verticesAndColors">A dictionary for mapping vertices to their colors</param>
        /// <returns>True if vertex can be "painted" in specified color, false if not</returns>
        private bool CanVertexBeColored(TGraph graph, TVertex vertex, int curColorId, IDictionary<TVertex, int> verticesAndColors)
        {
            return !graph.AdjacencyLists[vertex].Any(v =>
                verticesAndColors.ContainsKey(v) && verticesAndColors[v] == curColorId);
        }

        /// <summary>
        /// "Paints" a vertex in specified color
        /// </summary>
        /// <param name="vertex">Target vertex</param>
        /// <param name="curColorId">Target color</param>
        /// <param name="verticesAndColors">A dictionary for mapping vertices to their colors</param>
        /// <param name="notColoredVertices">Vertices that are going to be colored</param>
        private void SetColorToVertex(TVertex vertex, int curColorId, IDictionary<TVertex, int> verticesAndColors, ICollection<TVertex> notColoredVertices)
        {
            verticesAndColors.Add(vertex, curColorId);
            notColoredVertices.Remove(vertex);
        }
    }
}
