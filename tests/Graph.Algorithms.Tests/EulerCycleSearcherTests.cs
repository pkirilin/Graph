using Graph.Abstractions;
using Graph.Algorithms.Tests.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Algorithms.Tests
{
    public class EulerCycleSearcherTests
    {
        [Theory]
        [MemberData(nameof(EulerCycleSearcherTestData.MemberData_Execute), MemberType = typeof(EulerCycleSearcherTestData))]
        public void EulerCycleSearcher_ShouldListVerticesThroughWhichEulerCyclePasses_WhenGraphIsEulers(GraphBase<int> graph, int initialVertex, IEnumerable<int> expectedResult)
        {
            var eulerCycleSearcher = EulerCycleSearcherTestData.EulerCycleSearcher;

            var result = eulerCycleSearcher.Execute(graph, initialVertex);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EulerCycleSearcher_ShouldThrowArgumentNullException_WhenGraphIsNull()
        {
            var eulerCycleSearcher = EulerCycleSearcherTestData.EulerCycleSearcher;

            Assert.Throws<ArgumentNullException>(() =>
            {
                eulerCycleSearcher.Execute(null, default);
            });
        }

        [Theory]
        [MemberData(nameof(EulerCycleSearcherTestData.MemberData_WrongInitialVertex), MemberType = typeof(EulerCycleSearcherTestData))]
        public void EulerCycleSearcher_ShouldThrowArgumentException_WhenGraphDoesNotContainInitialVertex(GraphBase<int> graph, int initialVertex)
        {
            var eulerCycleSearcher = EulerCycleSearcherTestData.EulerCycleSearcher;

            Assert.Throws<ArgumentException>(() =>
            {
                eulerCycleSearcher.Execute(graph, initialVertex);
            });
        }

        [Theory]
        [MemberData(nameof(EulerCycleSearcherTestData.MemberData_NotEulerGraph), MemberType = typeof(EulerCycleSearcherTestData))]
        public void EulerCycleSearcher_ShouldThrowInvalidOperationException_WhenGraphIsNotEulers(GraphBase<int> graph, int initialVertex)
        {
            var eulerCycleSearcher = EulerCycleSearcherTestData.EulerCycleSearcher;

            Assert.Throws<InvalidOperationException>(() =>
            {
                eulerCycleSearcher.Execute(graph, initialVertex);
            });
        }
    }
}
