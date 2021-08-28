using System;
using Xunit;
using Periodicity.Core;

namespace Periodicity.Core.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var orbitState = new OrbitState()
            {
                SemimajorAxis = 6955.14,
                Eccentricity = 0.0
            };

            Assert.Equal(6955.14, orbitState.ApogeeRadius);
            Assert.Equal(6955.14, orbitState.PerigeeRadius);
            Assert.Equal(6955.14 - Globals.Re, orbitState.ApogeeAltitude);
            Assert.Equal(6955.14 - Globals.Re, orbitState.PerigeeAltitude);
            Assert.Equal(5772.5778019809095, orbitState.Period);           
            Assert.Equal(14.967316676156551, orbitState.MeanMotion);
        }
    }
}
