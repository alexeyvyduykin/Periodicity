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
            var orbit = new Orbit()
            { 
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
            
                SemimajorAxis = 6955.14,
                Eccentricity = 0.0,

                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0,

                TrueAnomaly = 30.0
            };

            // SizeShape
            Assert.Equal(6955.14, orbit.SemimajorAxis);
            Assert.Equal(0.0, orbit.Eccentricity);
            Assert.Equal(6955.14, orbit.ApogeeRadius);
            Assert.Equal(6955.14, orbit.PerigeeRadius);
            Assert.Equal(6955.14 - Globals.Re, orbit.ApogeeAltitude);
            Assert.Equal(6955.14 - Globals.Re, orbit.PerigeeAltitude);
            Assert.Equal(5772.5778019809095, orbit.Period);           
            Assert.Equal(14.967316676156551, orbit.MeanMotion);

            // Orientation
            Assert.Equal(97.65, orbit.Inclination);
            Assert.Equal(269.663, orbit.RAAN);
            Assert.Equal(0.0, orbit.ArgumentOfPerigee);
            Assert.Equal(-15192.237415179006, orbit.LonAscnNode);

            // Location
            Assert.Equal(30.0, orbit.TrueAnomaly);
            Assert.Equal(29.999999999999996, orbit.MeanAnomaly);
            Assert.Equal(29.999999999999996, orbit.EccentricAnomaly);
            Assert.Equal(30, orbit.ArgumentOfLatitude);
            Assert.Equal(27562.0287470343, orbit.TimePastAN);
            Assert.Equal(27562.0287470343, orbit.TimePastPerigee);
        }
    }
}
