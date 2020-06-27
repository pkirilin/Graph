using System;
using System.Collections.Generic;

namespace Graph.Internal
{
    class GraphVertexEqualityComparer<TVertex> : IEqualityComparer<TVertex> where TVertex : IComparable<TVertex>
    {
        public bool Equals(TVertex x, TVertex y)
        {
            return x.CompareTo(y) == 0;
        }

        public int GetHashCode(TVertex obj)
        {
            return obj.GetHashCode();
        }
    }
}
