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
            double days = 1.0;

            Orbit orbit = new Orbit();

            var satellite = new Satellite()
            {
                Orbit = orbit,
                StartTime = orbit.Epoch,
                StopTime = orbit.Epoch.AddDays(days),
                TrueAnomaly = orbit.TrueAnomaly
            };

        }
    }
}
