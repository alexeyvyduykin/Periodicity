using System;
using System.Collections.Generic;
using Periodicity.Core;
using System.Linq;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample1 = new Sample1();

            sample1.Run();
            
            Console.ReadKey();
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

            Orbit orbit1 = new Orbit() 
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),            
                SemimajorAxis = 6948.0,            
                Eccentricity = 0.0,          
                Inclination = 97.65,          
                RAAN = 269.663,          
                ArgumentOfPerigee = 0.0,           
                TrueAnomaly = 0.0     
            };

            Orbit orbit2 = new Orbit()
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
                SemimajorAxis = 6948.0,
                Eccentricity = 0.0,
                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0,
                TrueAnomaly = 90.0
            };

            Orbit orbit3 = new Orbit()
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
                SemimajorAxis = 6948.0,
                Eccentricity = 0.0,
                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0,
                TrueAnomaly = 180.0
            };

            Orbit orbit4 = new Orbit()
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
                SemimajorAxis = 6948.0,
                Eccentricity = 0.0,
                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0,
                TrueAnomaly = 270.0
            };

            var sensor1 = new Sensor("Sensor1", 8.814, -40.0);
            var sensor2 = new Sensor("Sensor2", 8.814, 40.0);

            var region = Region.DefaultZone;

            var satellite1 = new Satellite("Satellite1")
            {
                Orbit = orbit1,
                StartTime = orbit1.Epoch,
                StopTime = orbit1.Epoch.AddDays(days),
                TrueAnomaly = orbit1.TrueAnomaly
            };

            var satellite2 = new Satellite("Satellite2")
            {
                Orbit = orbit2,
                StartTime = orbit2.Epoch,
                StopTime = orbit2.Epoch.AddDays(days),
                TrueAnomaly = orbit2.TrueAnomaly
            };

            var satellite3 = new Satellite("Satellite3")
            {
                Orbit = orbit3,
                StartTime = orbit3.Epoch,
                StopTime = orbit3.Epoch.AddDays(days),
                TrueAnomaly = orbit3.TrueAnomaly
            };

            var satellite4 = new Satellite("Satellite4")
            {
                Orbit = orbit4,
                StartTime = orbit4.Epoch,
                StopTime = orbit4.Epoch.AddDays(days),
                TrueAnomaly = orbit4.TrueAnomaly
            };

            satellite1.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });
            satellite2.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });
            satellite3.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });
            satellite4.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });

            periodicity.Satellites.AddRange(new List<Satellite>() { satellite1, satellite2, satellite3, satellite4 });
            periodicity.Regions.Add(region);

            periodicity.Calculate();

            foreach ((double latDeg, int prdct, double percent, double widthRad, double widthKm) in periodicity.DataPeriodicities)
            {
                Console.WriteLine($"Lat={latDeg:0.0} deg, Prdct={prdct:##0}, is {percent:0.00} %, Width={widthRad:0.00} rad");
            }
        }
    }
}
