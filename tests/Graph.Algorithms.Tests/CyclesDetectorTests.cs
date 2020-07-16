using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class CyclesDetectorTests
    {
        [Theory]
        [MemberData(nameof(CyclesDetectorTestData.MemberData_Execute), MemberType = typeof(CyclesDetectorTestData))]
        public void CyclesDetector_ShouldDetermineWhetherGraphContainsAnyCycle(GraphBase<int> graph, int initialVertex, bool expectedResult)
        {
            var cyclesDetector = new CyclesDetector<GraphBase<int>, int>();

            var result = cyclesDetector.Execute(graph, initialVertex);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CyclesDetector_ShouldThrowArgumentNullException_WhenTargetGraphIsNull()
        {
            var cyclesDetector = new CyclesDetector<GraphBase<int>, int>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                cyclesDetector.Execute(null, default);
            });
        }

        [Theory]
        [MemberData(nameof(CyclesDetectorTestData.MemberData_WrongInitialVertex), MemberType = typeof(CyclesDetectorTestData))]
        public void CyclesDetector_ShouldThrowArgumentException_WhenNotExistingInitialVertexSpecified(GraphBase<int> graph, int initialVertex)
        {
            var cyclesDetector = new CyclesDetector<GraphBase<int>, int>();

            Assert.Throws<ArgumentException>(() =>
            {
                cyclesDetector.Execute(graph, initialVertex);
            });
        }
    }
}
