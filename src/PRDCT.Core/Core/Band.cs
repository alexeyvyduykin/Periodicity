using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRDCT.Core.PRDCTPeriodicity;

namespace PRDCT.Core
{
    public enum BandMode
    {
        Middle,
        Left,
        Right
    }

    public class Band
    {
        public Band(Orbit orbit, double gam1DEG, double gam2DEG, BandMode mode)
        {
            this.Orbit = orbit;

            TrackPointDirection[] dir = new TrackPointDirection[2];
            switch (mode)
            {
                case BandMode.Middle:
                    dir[0] = TrackPointDirection.Left;
                    dir[1] = TrackPointDirection.Right;
                    break;
                case BandMode.Left:
                    dir[0] = dir[1] = TrackPointDirection.Left;
                    break;
                case BandMode.Right:
                    dir[0] = dir[1] = TrackPointDirection.Right;
                    break;
            }

            FactorShiftTrack factor = new FactorShiftTrack(orbit, gam1DEG, gam2DEG, mode);

            NearLine = new FactorTrack(new CustomTrack(orbit, gam1DEG, dir[0]), factor);
            FarLine = new FactorTrack(new CustomTrack(orbit, gam2DEG, dir[1]), factor);
        }

        public Band(Orbit orbit, double verticalHalfAngleDEG, double rollAngleDEG)
        {
            this.Orbit = orbit;

            BandMode mode;

            if (rollAngleDEG == 0)
                mode = BandMode.Middle;
            else if (rollAngleDEG > 0.0)
                mode = BandMode.Left;
            else
                mode = BandMode.Right;

            TrackPointDirection[] dir = new TrackPointDirection[2];
            switch (mode)
            {
                case BandMode.Middle:
                    dir[0] = TrackPointDirection.Left;
                    dir[1] = TrackPointDirection.Right;
                    break;
                case BandMode.Left:
                    dir[0] = dir[1] = TrackPointDirection.Left;
                    break;
                case BandMode.Right:
                    dir[0] = dir[1] = TrackPointDirection.Right;
                    break;
            }

            double gam1DEG = Math.Abs(rollAngleDEG) - verticalHalfAngleDEG;
            double gam2DEG = Math.Abs(rollAngleDEG) + verticalHalfAngleDEG;

            FactorShiftTrack factor = new FactorShiftTrack(orbit, gam1DEG, gam2DEG, mode);

            NearLine = new FactorTrack(new CustomTrack(orbit, gam1DEG, dir[0]), factor);
            FarLine = new FactorTrack(new CustomTrack(orbit, gam2DEG, dir[1]), factor);            
        }

        public bool IsCoverPolis(double latRAD, ref double timeFromANToPolis)
        {
            double angleToPolis1 = 0.0, angleToPolis2 = 0.0;
            if (NearLine.polisMod(latRAD, ref angleToPolis1) == true &&
                FarLine.polisMod(latRAD, ref angleToPolis2) == true)
            {
                if (MyMath.InRange(Math.PI / 2.0, angleToPolis1, angleToPolis2))
                {
                    if (latRAD >= 0.0) timeFromANToPolis = Orbit.Quart1;
                    else timeFromANToPolis = Orbit.Quart3;
                    return true;
                }
            }
            return false;
        }

        public bool IsCoverPolis(double latRAD)
        {
            double angleToPolis1 = 0.0, angleToPolis2 = 0.0;
            if (NearLine.polisMod(latRAD, ref angleToPolis1) == true &&
                FarLine.polisMod(latRAD, ref angleToPolis2) == true)
            {
                if (MyMath.InRange(Math.PI / 2.0, angleToPolis1, angleToPolis2))
                    return true;
            }
            return false;
        }

        public Orbit Orbit { get; }

        public IList<Geo2D> GetNearGroundTrack(PRDCTSatellite satellite, int node)
        {
            CustomTrack track1 = new CustomTrack(Orbit, NearLine.Alpha1 * MyMath.RadiansToDegrees, NearLine.Direction);
            return GetGroundTrack(track1, satellite, node);
        }

        public IList<Geo2D> GetFarGroundTrack(PRDCTSatellite satellite, int node)
        {
            CustomTrack track2 = new CustomTrack(Orbit, FarLine.Alpha1 * MyMath.RadiansToDegrees, FarLine.Direction);
            return GetGroundTrack(track2, satellite, node);
        }

        private IList<Geo2D> GetGroundTrack(CustomTrack track, PRDCTSatellite satellite, int node)
        {
            var points = new List<Geo2D>();

            var nodes = satellite.Nodes();
            for (int q = 0; q < nodes[node].Quarts.Count; q++)
            {
                for (double t = nodes[node].Quarts[q].TimeBegin; t <= nodes[node].Quarts[q].TimeEnd; t += 5.0)
                {
                    var point = track.ContinuousTrack(node, t, satellite.TrueTimePastAN, nodes[node].Quarts[q].Quart);

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

        public FactorTrack NearLine { get; private set; }
        public FactorTrack FarLine { get; private set; }
    }



}
