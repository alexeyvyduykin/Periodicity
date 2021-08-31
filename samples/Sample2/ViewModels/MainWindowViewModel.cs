using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OxyPlot.Series;
using Periodicity.Core;

namespace Sample2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var periodicity = new Periodicity.Core.Periodicity();

            double days = 1.0;

            Orbit orbit1 = new Orbit()
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
                SemimajorAxis = 577 + Globals.Re,
                Eccentricity = 0.0,
                Inclination = 97.65,
                LonAscnNode = 0.0,
                ArgumentOfPerigee = 0.0,
                TrueAnomaly = 0.0
            };

            Orbit orbit2 = new Orbit()
            {
                Epoch = new DateTime(2015, 6, 22, 0, 0, 0),
                SemimajorAxis = 577 + Globals.Re,
                Eccentricity = 0.0,
                Inclination = 97.65,
                LonAscnNode = 6.0,
                ArgumentOfPerigee = 0.0,
                TrueAnomaly = 0.0
            };

            var sensor1 = new Sensor("Sensor1", 14.8, -34.0);
            var sensor2 = new Sensor("Sensor2", 14.8, 34.0);

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

            satellite1.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });
            satellite2.Sensors.AddRange(new List<Sensor>() { sensor1, sensor2 });

            periodicity.Satellites.AddRange(new List<Satellite>() { satellite1, satellite2 });
            periodicity.Regions.Add(region);

            periodicity.Calculate();

            CreateReport(periodicity);
        }

        private void CreateReport(Periodicity.Core.Periodicity periodicity)
        {
            double tempLatDeg = periodicity.DataPeriodicities.First().latDeg;
            double summaryPercent = 0.0;
            double averagePercent = 0.0;
            double reservePercent = 5.0;
            int minPrdct = int.MaxValue;
            int maxPrdct = int.MinValue;
            int minReservePrdct = int.MaxValue;
            int maxReservePrdct = int.MinValue;


            PeriodicityReport = new ObservableCollection<PeriodicityRecord>();
            PeriodicityGraph1 = new ObservableCollection<PeriodicityGraph1>();
            PeriodicityGraph2 = new ObservableCollection<BoxPlotItem>();

            int count = periodicity.DataPeriodicities.Count;
            for (int i = 0; i < count; i++)
            {
                var(latDeg, prdct, percent, _, _) = periodicity.DataPeriodicities[i];

                if (tempLatDeg != latDeg)
                {
                    if (tempLatDeg % 5.0 == 0.0)
                    {
                        PeriodicityReport.Add(new PeriodicityRecord()
                        {
                            Latitude = tempLatDeg,
                            MinPeriodicity = minPrdct,
                            MaxPeriodicity = maxPrdct,
                            Coverage = summaryPercent
                        });
                    }

                    PeriodicityGraph1.Add(new PeriodicityGraph1()
                    {
                        Latitude = tempLatDeg,
                        Coverage = summaryPercent
                    });

                    PeriodicityGraph2.Add(new BoxPlotItem(tempLatDeg, minPrdct, minReservePrdct, averagePercent / 100.0, maxReservePrdct, maxPrdct));

                    summaryPercent = 0.0;
                    averagePercent = 0.0;
                    minPrdct = int.MaxValue;
                    maxPrdct = int.MinValue;
                    minReservePrdct = int.MaxValue;
                    maxReservePrdct = int.MinValue;
                }

                minPrdct = Math.Min(minPrdct, prdct);
                maxPrdct = Math.Max(maxPrdct, prdct);

                if (percent >= reservePercent)
                {
                    minReservePrdct = Math.Min(minReservePrdct, prdct);
                    maxReservePrdct = Math.Max(maxReservePrdct, prdct);
                }
                
                averagePercent += prdct * percent;

                if (prdct != 0)
                {
                    summaryPercent += percent;
                }

                tempLatDeg = latDeg;

                if(i == count - 1)
                {
                    if (tempLatDeg % 5.0 == 0.0)
                    {
                        PeriodicityReport.Add(new PeriodicityRecord()
                        {
                            Latitude = tempLatDeg,
                            MinPeriodicity = minPrdct,
                            MaxPeriodicity = maxPrdct,
                            Coverage = summaryPercent
                        });
                    }

                    PeriodicityGraph1.Add(new PeriodicityGraph1()
                    {
                        Latitude = tempLatDeg,
                        Coverage = summaryPercent
                    });

                    PeriodicityGraph2.Add(new BoxPlotItem(tempLatDeg, minPrdct, minReservePrdct, averagePercent / 100.0, maxReservePrdct, maxPrdct));
                }
            }
        }

        public ObservableCollection<PeriodicityRecord> PeriodicityReport { get; set; }

        public ObservableCollection<PeriodicityGraph1> PeriodicityGraph1 { get; set; }

        public ObservableCollection<BoxPlotItem> PeriodicityGraph2 { get; set; }
    }

    public class PeriodicityRecord
    {
        public double Latitude { get; set; }

        public int MinPeriodicity { get; set; }

        public int MaxPeriodicity { get; set; }

        public double Coverage { get; set; }
    }

    public class PeriodicityGraph1
    {
        public double Latitude { get; set; }

        public double Coverage { get; set; }
    }

//    public class PeriodicityGraph2 : BoxPlotItem
//{
//        public double Latitude { get; set; }

//        public double AveragePeriodicity { get; set; }

//        public double MinPeriodicity { get; set; }

//        public double MaxPeriodicity { get; set; }
//    }
}
