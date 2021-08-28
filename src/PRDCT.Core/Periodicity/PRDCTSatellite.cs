using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDCT.Core.PRDCTPeriodicity
{
    public class PRDCTSatellite
    {
        public static PRDCTSatellite Default = new PRDCTSatellite();
        private PRDCTSatellite() { }

        public PRDCTSatellite(Orbit orbit, DateTime startTime, DateTime stopTime, double trueAnomaly)
        {
            this.Orbit = orbit;
            this.StartTime = startTime;
            this.StopTime = stopTime;
            this.TrueAnomaly = trueAnomaly;
        }

        public PRDCTSatellite(Orbit orbit, DateTime startTime, DateTime stopTime) : this(orbit, startTime, stopTime, 0.0) { }

        public PRDCTSatellite(Orbit orbit, int days) : this(orbit, orbit.Epoch, orbit.Epoch.AddDays(days), 0.0) { }

        public PRDCTSatellite(Orbit orbit, int days, double trueAnomaly) : this(orbit, orbit.Epoch, orbit.Epoch.AddDays(days), trueAnomaly) { }

        public static PRDCTSatellite From(BaseSatellite satellite)
        {
            double SemimajorAxis = satellite.OrbitState.SizeShape.SemimajorAxis;
            double Period = 2.0 * Math.PI / (Math.Sqrt(Globals.GM / SemimajorAxis) / SemimajorAxis);
            Orbit orbit = new Orbit(
                satellite.OrbitState.SizeShape.SemimajorAxis,
                satellite.OrbitState.SizeShape.Eccentricity,
                satellite.OrbitState.Orientation.Inclination * MyMath.DegreesToRadians,
                satellite.OrbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians,
                satellite.OrbitState.Orientation.LonAscnNode * MyMath.DegreesToRadians,
                satellite.OrbitState.Orientation.RAAN * MyMath.DegreesToRadians,
                Period,
                satellite.OrbitState.OrbitEpoch);

            return new PRDCTSatellite(orbit, 1, satellite.OrbitState.Location.TrueAnomaly * MyMath.DegreesToRadians);    
        }

        public double TrueTimePastAN
        {
            get
            {
                double u = TrueAnomaly;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(Orbit.SemimajorAxis, -3.0 / 2.0);
                double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Orbit.Eccentricity) / (1.0 + Orbit.Eccentricity)) * Math.Sin(u / 2.0), Math.Cos(u / 2.0));
                if (e1 < 0) e1 += 2.0 * Math.PI;
                double e2 = e1 - Orbit.Eccentricity * Math.Sin(e1);
                return e2 / n;
            }
        }

        public IList<Node> Nodes()
        {
            var nodes = new List<Node>();
            //double julianDate_ = StartTime.ToOADate() + 2415018.5;
            //double TimeBegin = StartTime.TimeOfDay.TotalSeconds;
            double TimeDuration = (StopTime - StartTime).TotalSeconds;
            double TimeEnd = TimeDuration + TrueTimePastAN;

            int numNodes = (int)Math.Ceiling((TimeDuration + TrueTimePastAN) / Orbit.Period);

            double timePastAN = TrueTimePastAN;

            for (int i = 0; i < numNodes; i++)
            {
                Node node = new Node();

                List<Tuple<double, double, int>> tq = new List<Tuple<double, double, int>>();

                for (int j = 1; j <= 4; j++)
                {
                    double q = Orbit.Quarts[j] + i * Orbit.Period;
                    if (timePastAN < q)
                    {
                        if (TimeEnd < q)
                        {
                            tq.Add(Tuple.Create(timePastAN, TimeEnd, j));
                            break;
                        }

                        tq.Add(Tuple.Create(timePastAN, q, j));
                        timePastAN = q;
                    }
                }

                #region Full Realization

                //   double q1 = Orbit.Quarts[1] + i * Orbit.Period;
                //   double q2 = Orbit.Quarts[2] + i * Orbit.Period;
                //   double q3 = Orbit.Quarts[3] + i * Orbit.Period;
                //   double q4 = Orbit.Quarts[4] + i * Orbit.Period;

                //if (timePastAN < q1)
                //{
                //    if (TimeEnd < q1)
                //    {
                //        tq.Add(Tuple.Create(timePastAN, TimeEnd, 1));
                //        goto markl;
                //    }

                //    tq.Add(Tuple.Create(timePastAN, q1, 1));
                //    timePastAN = q1;
                //}

                //if (timePastAN < q2)
                //{
                //    if (TimeEnd < q2)
                //    {
                //        tq.Add(Tuple.Create(timePastAN, TimeEnd, 2));
                //        goto markl;
                //    }

                //    tq.Add(Tuple.Create(timePastAN, q2, 2));
                //    timePastAN = q2;
                //}

                //if (timePastAN < q3)
                //{
                //    if (TimeEnd < q3)
                //    {
                //        tq.Add(Tuple.Create(timePastAN, TimeEnd, 3));
                //        goto markl;
                //    }

                //    tq.Add(Tuple.Create(timePastAN, q3, 3));
                //    timePastAN = q3;
                //}

                //if (timePastAN < q4)
                //{
                //    if (TimeEnd < q4)
                //    {
                //        tq.Add(Tuple.Create(timePastAN, TimeEnd, 4));
                //        goto markl;
                //    }

                //    tq.Add(Tuple.Create(timePastAN, q4, 4));
                //    timePastAN = q4;
                //}

                //markl:

                #endregion

                foreach (var item in tq)
                {
                    node.Quarts.Add(new NodeQuarter
                    {
                        TimeBegin = item.Item1 - TrueTimePastAN,
                        TimeEnd = item.Item2 - TrueTimePastAN,
                        Quart = item.Item3
                    });
                }

                node.Value = i + 1;
                nodes.Add(node);
            }

            Console.WriteLine("Nodes: TrueTimePastAN = {0}, Period = {1}, numNodes = {2}", TrueTimePastAN, Orbit.Period, numNodes);
            return nodes;
        }

        public Orbit Orbit { get; }

        public IList<Geo2D> GetGroundTrack(int node)
        {
            List<Geo2D> points = new List<Geo2D>();

            Track track = new Track(Orbit);

            var nodes = Nodes();
            for (int q = 0; q < nodes[node].Quarts.Count; q++)
            {
                for (double t = nodes[node].Quarts[q].TimeBegin; t <= nodes[node].Quarts[q].TimeEnd; t += 5.0)
                {
                    var point = track.ContinuousTrack(node, t, TrueTimePastAN, nodes[node].Quarts[q].Quart);

                    double lon = point.Lon;
                    while (lon > 2.0 * Math.PI)
                        lon -= 2.0 * Math.PI;
                    while (lon < 0.0)
                        lon += 2.0 * Math.PI;
                    points.Add(new Geo2D(lon, point.Lat));
                }
            }

            return points;
        }

        public double TrueAnomaly { get; }

        public DateTime StartTime { get; }

        public DateTime StopTime { get; }
    }

    public enum SensorMode
    {
        One = 0,
        Left = 1,
        Right = 2,
        Two = 3,
        Error
    }

    public static class Extensions
    {
        public static string ToString(this SensorMode mode)
        {
            switch (mode)
            {
                case SensorMode.One:
                    return "One";
                case SensorMode.Left:
                    return "Left";
                case SensorMode.Right:
                    return "Right";
                case SensorMode.Two:
                    return "Two";
                default:
                    return "Error";
            }
        }
    }

}
