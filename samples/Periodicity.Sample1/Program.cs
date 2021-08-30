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
            var periodicity = new Periodicity.Core.Periodicity();

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

            satellite1.Sensors.Add(sensor1);
            satellite1.Sensors.Add(sensor2);

            periodicity.Satellites.Add(satellite1);

            periodicity.Regions.Add(region);

            periodicity.Calculate();

            foreach ((double latDeg, int prdct, double percent, double widthRad, double widthKm) in periodicity.DataPeriodicities)
            {
                Console.WriteLine($"Lat={latDeg:0.0} deg, Prdct={prdct:##0}, is {percent:0.00} %, Width={widthRad:0.00} rad");
            }

            Console.ReadKey();
        }
    }
}
