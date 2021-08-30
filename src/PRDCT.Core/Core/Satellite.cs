using System;
using System.Collections.Generic;

namespace Periodicity.Core
{
    public class Satellite : BaseEntity
    {
        public double TrueAnomaly { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public Orbit Orbit { get; set; }

        public IList<Sensor> Sensors { get; }

        private Satellite() { }

        public Satellite(string name)
        {
            Name = name;
            Sensors = new List<Sensor>();
        }
 
        public double TrueTimePastAN
        {
            get
            {
                double u = TrueAnomaly;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(Orbit.SemimajorAxis, -3.0 / 2.0);
                double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Orbit.Eccentricity) / (1.0 + Orbit.Eccentricity)) * Math.Sin(u / 2.0), Math.Cos(u / 2.0));
                if (e1 < 0)
                {
                    e1 += 2.0 * Math.PI;
                }

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

                List<(double, double, TrackNodeQuarter)> tq = new List<(double, double, TrackNodeQuarter)>();
            
                foreach (var item in Enum.GetValues(typeof(TrackNodeQuarter)))
                {
                    double q = Orbit.Quarts[(int)item] + i * Orbit.Period;
                    if (timePastAN < q)
                    {
                        if (TimeEnd < q)
                        {
                            tq.Add((timePastAN, TimeEnd, (TrackNodeQuarter)item));
                            break;
                        }

                        tq.Add((timePastAN, q, (TrackNodeQuarter)item));
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

            return nodes;
        }

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
                    {
                        lon -= 2.0 * Math.PI;
                    }

                    while (lon < 0.0)
                    {
                        lon += 2.0 * Math.PI;
                    }

                    points.Add(new Geo2D(lon, point.Lat));
                }
            }

            return points;
        }
    }

    public enum SensorMode
    {
        One = 0,
        Left = 1,
        Right = 2,
        Two = 3,
        Error
    }
}
