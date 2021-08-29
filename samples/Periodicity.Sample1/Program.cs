using System;
using Periodicity.Core;

namespace Sample1
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
            var prdct = new Periodicity.Core.Periodicity();

            double days = 1.0;

            Orbit orbit = new Orbit();

            var sensor1 = new Sensor("Sensor1", 5.0, -25.0);
            var sensor2 = new Sensor("Sensor2", 5.0, 25.0);

            var region = Region.DefaultZone;

            var satellite1 = new Satellite("Satellite1")
            {
                Orbit = orbit,
                StartTime = orbit.Epoch,
                StopTime = orbit.Epoch.AddDays(days),
                TrueAnomaly = orbit.TrueAnomaly
            };

            var satellite2 = new Satellite("Satellite2")
            {
                Orbit = orbit,
                StartTime = orbit.Epoch,
                StopTime = orbit.Epoch.AddDays(days),
                TrueAnomaly = orbit.TrueAnomaly
            };

            satellite1.Sensors.Add(sensor1);
            satellite1.Sensors.Add(sensor2);

            satellite2.Sensors.Add(sensor1);
            satellite2.Sensors.Add(sensor2);

            prdct.Satellites.Add(satellite1);
            prdct.Satellites.Add(satellite2);

            prdct.Regions.Add(region);
        }
    }
}
