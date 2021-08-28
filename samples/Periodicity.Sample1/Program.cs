using System;
using Periodicity.Core;

namespace Periodicity.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample1 = new Sample1();

            sample1.Run();
        }
    }

    class Sample1
    {
        public Sample1()
        {

        }

        public void Run()
        {
            OrbitState orbitState = new OrbitState();
            double SemimajorAxis = orbitState.SemimajorAxis;

            double Period = 2.0 * Math.PI / (Math.Sqrt(Globals.GM / SemimajorAxis) / SemimajorAxis);
            double days = 1.0;

            Orbit orbit = new Orbit(
                orbitState.SemimajorAxis,
                orbitState.Eccentricity,
                orbitState.Inclination * MyMath.DegreesToRadians,
                orbitState.ArgumentOfPerigee * MyMath.DegreesToRadians,
                orbitState.LonAscnNode * MyMath.DegreesToRadians,
                orbitState.RAAN * MyMath.DegreesToRadians,
                Period,
                orbitState.OrbitEpoch);

            var satellite = new Satellite()
            {
                Orbit = orbit,
                StartTime = orbit.Epoch,
                StopTime = orbit.Epoch.AddDays(days),
                TrueAnomaly = orbitState.TrueAnomaly * MyMath.DegreesToRadians
            };

        }
    }
}
