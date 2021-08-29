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

            var sensor1 = new Sensor(5.0, -25.0);
            var sensor2 = new Sensor(5.0, 25.0);

            var satellite = new Satellite()
            {
                Orbit = orbit,
                StartTime = orbit.Epoch,
                StopTime = orbit.Epoch.AddDays(days),
                TrueAnomaly = orbit.TrueAnomaly
            };

            satellite.Sensors.Add(sensor1);
            satellite.Sensors.Add(sensor2);
        }
    }
}
