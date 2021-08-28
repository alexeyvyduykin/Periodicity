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
                Eccentricity = 0.0,

                TrueAnomaly = 30.0
            };

            // SizeShape
            Assert.Equal(6955.14, orbitState.ApogeeRadius);
            Assert.Equal(6955.14, orbitState.PerigeeRadius);
            Assert.Equal(6955.14 - Globals.Re, orbitState.ApogeeAltitude);
            Assert.Equal(6955.14 - Globals.Re, orbitState.PerigeeAltitude);
            Assert.Equal(5772.5778019809095, orbitState.Period);           
            Assert.Equal(14.967316676156551, orbitState.MeanMotion);

            // Location
            Assert.Equal(30.0, orbitState.TrueAnomaly);
            Assert.Equal(29.999999999999996, orbitState.MeanAnomaly);
            Assert.Equal(29.999999999999996, orbitState.EccentricAnomaly);
            Assert.Equal(30, orbitState.ArgumentOfLatitude);
            Assert.Equal(27562.0287470343, orbitState.TimePastAN);
            Assert.Equal(27562.0287470343, orbitState.TimePastPerigee);
        }

        [Fact]
        public void Test2()
        {
            var orbitState = new OrbitState()
            {
                SemimajorAxis = 6955.14,
                Eccentricity = 0.0
            };

            var orientation = new Orientation(orbitState)
            {
                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0
            };

            Assert.Equal(97.65, orientation.Inclination);
            Assert.Equal(269.663, orientation.RAAN);
            Assert.Equal(0.0, orientation.ArgumentOfPerigee);
            Assert.Equal(-15192.237415179006, orientation.LonAscnNode);
        }
    }
}