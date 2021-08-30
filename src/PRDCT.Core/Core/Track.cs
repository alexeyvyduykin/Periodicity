﻿using System;

namespace Periodicity.Core
{
    public class FactorShiftTrack
    {
        public FactorShiftTrack(Orbit orbit, double gam1DEG, double gam2DEG, BandMode direction)
        {
            double inclination = orbit.Inclination * MyMath.DegreesToRadians;
            int mdf = 0, ch23, ch4, pls1 = 0, pls2 = 0;

            double gam1 = gam1DEG * MyMath.DegreesToRadians;
            double gam2 = gam2DEG * MyMath.DegreesToRadians;

            switch (direction)
            {
                case BandMode.Middle:
                    pls1 = -1;
                    pls2 = 1;
                    break;
                case BandMode.Left:
                    pls1 = pls2 = -1;
                    break;
                case BandMode.Right:
                    pls1 = pls2 = 1;
                    break;
            }

            double semi_axis = orbit.Semiaxis(Math.PI / 2.0);

            double fi1 = Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(gam1) / Globals.Re)) - gam1;
            double fi2 = Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(gam2) / Globals.Re)) - gam2;

            double i1_90 = inclination - fi1 * pls1;
            double i2_90 = inclination - fi2 * pls2;
            double di1_90 = Math.Abs(Math.PI / 2.0 - i1_90);
            double di2_90 = Math.Abs(Math.PI / 2.0 - i2_90);

            double i1_270 = Math.PI + inclination + fi1 * pls1;
            double i2_270 = Math.PI + inclination + fi2 * pls2;
            double di1_270 = Math.Abs(3.0 * Math.PI / 2.0 - i1_270);
            double di2_270 = Math.Abs(3.0 * Math.PI / 2.0 - i2_270);

            ////////////////////////////////////////////////////////////////////
            double uTr1 = Math.Acos(Math.Cos(fi1) * Math.Cos(Math.PI / 2.0));
            double iTr1 = inclination - Math.Atan2(Math.Tan(fi1), Math.Sin(Math.PI / 2.0)) * pls1;
            double lat1 = Math.Asin(Math.Sin(uTr1) * Math.Sin(iTr1));
            double asinlon1 = Math.Tan(lat1) / Math.Tan(iTr1);
            if (Math.Abs(asinlon1) > 1.0)
            {
                asinlon1 = MyMath.Sign(asinlon1);
            }

            double lon1 = Math.Asin(asinlon1) - Globals.Omega * orbit.Quart1;// Period / 4.0;

            double uTr2 = Math.Acos(Math.Cos(fi2) * Math.Cos(Math.PI / 2.0));
            double iTr2 = inclination - Math.Atan2(Math.Tan(fi2), Math.Sin(Math.PI / 2.0)) * pls2;
            double lat2 = Math.Asin(Math.Sin(uTr2) * Math.Sin(iTr2));
            double asinlon2 = Math.Tan(lat2) / Math.Tan(iTr2);
            if (Math.Abs(asinlon2) > 1.0)
            {
                asinlon2 = MyMath.Sign(asinlon2);
            }

            double lon2 = Math.Asin(asinlon2) - Globals.Omega * orbit.Quart1;// Period / 4.0;
            ////////////////////////////////////////////////////////////////////
            ch23 = 0;
            if (lon1 < 0.0 && lon2 < 0.0)
            {
                ch23++;
            }

            if (lon1 > 0.0 && lon2 < 0.0)
            {
                if (di1_90 < di2_90)
                {
                    ch23++;
                }
            }

            if (lon1 < 0.0 && lon2 > 0.0)
            {
                if (di1_90 > di2_90)
                {
                    ch23++;
                }
            }
            /////////////////////////////////////////////////////////////////////

            uTr1 = Math.Acos(Math.Cos(fi1) * Math.Cos(3.0 * Math.PI / 2.0));
            iTr1 = inclination - Math.Atan2(Math.Tan(fi1), Math.Sin(3.0 * Math.PI / 2.0)) * pls1;
            lat1 = Math.Asin(Math.Sin(uTr1) * Math.Sin(iTr1));
            asinlon1 = Math.Tan(lat1) / Math.Tan(iTr1);
            if (Math.Abs(asinlon1) > 1.0)
            {
                asinlon1 = MyMath.Sign(asinlon1);
            }

            lon1 = 2.0 * Math.PI + Math.Asin(asinlon1) - Globals.Omega * orbit.Quart3;// 3.0 * Period / 4.0;

            uTr2 = Math.Acos(Math.Cos(fi2) * Math.Cos(3.0 * Math.PI / 2.0));
            iTr2 = inclination - Math.Atan2(Math.Tan(fi2), Math.Sin(3.0 * Math.PI / 2.0)) * pls2;
            lat2 = Math.Asin(Math.Sin(uTr2) * Math.Sin(iTr2));
            asinlon2 = Math.Tan(lat2) / Math.Tan(iTr2);
            if (Math.Abs(asinlon2) > 1.0)
            {
                asinlon2 = MyMath.Sign(asinlon2);
            }

            lon2 = 2.0 * Math.PI + Math.Asin(asinlon2) - Globals.Omega * orbit.Quart3;// 3.0 * Period / 4.0;
            ///////////////////////////////////////////////////////////////////////////////
            ch4 = ch23;

            if (lon1 > 2.0 * Math.PI && lon2 > 2.0 * Math.PI)
            {
                ch4++;
            }

            if (lon1 > 2.0 * Math.PI && lon2 < 2.0 * Math.PI)
            {
                if (di1_270 > di2_270)
                {
                    ch4++;
                }
            }

            if (lon1 < 2.0 * Math.PI && lon2 > 2.0 * Math.PI)
            {
                if (di1_270 < di2_270)
                {
                    ch4++;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////

            if (ch4 == 2)
            {
                mdf = -1;
            }

            if (ch4 == 1)
            {
                mdf = 0;
            }

            if (ch4 == 0)
            {
                mdf = 1;
            }

            //--------------------------------------------------------------------------------------
            int pmdf;
            double modnakl = orbit.InclinationNormal();
            if ((modnakl + fi1 < Math.PI / 2.0) && (modnakl + fi2 < Math.PI / 2.0))
            {
                pmdf = 1;
            }
            else
            {
                pmdf = 0;
            }
            //---------------------------------
            Offset = mdf;
            Quart23 = ch23;
            Quart4 = ch4;
            Polis = pmdf;
        }

        public int Offset { get; }  // смещение
        public int Quart23 { get; }
        public int Quart4 { get; }
        public int Polis { get; }
    }

    public enum TrackPointDirection
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    public class Track
    {
        public Track(Orbit orbit)
        {
            Orbit = orbit;
        }

        public Geo2D Position(double tnorm)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double lonAscnNode = Orbit.LonAscnNode * MyMath.DegreesToRadians;

            double u = MyMath.WrapAngle(Orbit.Anomalia(tnorm) + Orbit.ArgumentOfPerigee);
            double lat = Math.Asin(Math.Sin(u) * Math.Sin(inclination));
            double asinlon = Math.Tan(lat) / Math.Tan(inclination);
            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = MyMath.Sign(asinlon);
            }

            double lon = Math.Asin(asinlon);
            if ((u <= Math.PI / 2.0) && (u >= 0))
            {
                lon = Math.Asin(asinlon);
            }

            if ((u > Math.PI / 2.0) && (u <= 3 * Math.PI / 2))
            {
                lon = Math.PI - lon;
            }

            if (u > 3 * Math.PI / 2 && u <= 2.0 * Math.PI)
            {
                lon = 2.0 * Math.PI + lon;
            }

            lon = lonAscnNode + lon - Globals.Omega * (tnorm);
            while (lon > 2.0 * Math.PI)
            {
                lon -= 2.0 * Math.PI;
            }

            while (lon < 0.0)
            {
                lon += 2.0 * Math.PI;
            }

            return new Geo2D(lon, lat, GeoCoordTypes.Radians);
        }

        public virtual Geo2D ContinuousTrack(double node, double t, double tPastAN, TrackNodeQuarter quart)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double lonAscnNode = Orbit.LonAscnNode * MyMath.DegreesToRadians;

            double v = Orbit.Anomalia(t, tPastAN);
            double u = v + Orbit.ArgumentOfPerigee;

            double lat = Math.Asin(Math.Sin(u) * Math.Sin(inclination));
            double asinlon = Math.Tan(lat) / Math.Tan(inclination);

            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = MyMath.Sign(asinlon);
            }

            double lon = Math.Asin(asinlon);

            switch (quart)
            {
                case TrackNodeQuarter.First:
                    //lon = lon;
                    break;
                case TrackNodeQuarter.Second:         
                case TrackNodeQuarter.Third:
                    lon = (Math.PI - lon);// - factor.ch23 * 2.0 * Math.PI;
                    break;
                case TrackNodeQuarter.Fourth:
                    lon = 2.0 * Math.PI + lon;// - factor.ch4 * 2.0 * Math.PI;
                    break;
                default:
                    break;
            }

            lon = lonAscnNode + lon - Globals.Omega * (t + tPastAN) + node * 2.0 * Math.PI;// * factor.mdf;
            return new Geo2D(lon, lat);
        }

        public virtual Geo2D TrackPoint(double u)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double lat = Math.Asin(Math.Sin(u) * Math.Sin(inclination));
            double asinlon = Math.Tan(lat) / Math.Tan(inclination);
            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = Math.Sign(asinlon);
            }

            double lon = 0.0;
            if (u >= 0.0 && u < Math.PI / 2.0)
            {
                lon = Math.Asin(asinlon);
            }
            else if (u >= Math.PI / 2.0 && u < 3.0 * Math.PI / 2.0)
            {
                lon = Math.PI - Math.Asin(asinlon);
            }
            else if (u >= 3.0 * Math.PI / 2.0 && u < 2.0 * Math.PI)
            {
                lon = 2.0 * Math.PI + Math.Asin(asinlon);
            }

            return new Geo2D(lon, lat);
        }

        public Orbit Orbit { get; }
    }
   
    public class CustomTrack : Track
    {
        public CustomTrack(Orbit orbit, double alpha1DEG, TrackPointDirection direction) : base(orbit)
        {
            Alpha1 = alpha1DEG * MyMath.DegreesToRadians;
            Direction = direction;
            switch (direction)
            {
                case TrackPointDirection.None:
                    dir = 0;
                    break;
                case TrackPointDirection.Left:
                    dir = -1;
                    break;
                case TrackPointDirection.Right:
                    dir = 1;
                    break;
                default:
                    dir = 0;
                    break;
            }
        }

        public bool polisMod(double lat, ref double polis_mod)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double t_polis;
            if (lat >= 0.0)
            {
                t_polis = Orbit.TimeHalfPi();
            }
            else
            {
                t_polis = 3.0 * Orbit.TimeHalfPi();
            }

            double fi = centralAngle(t_polis + Orbit.ArgumentOfPerigee * Orbit.Period / (2.0 * Math.PI));
            double i = inclination - fi * dir;
            if (i > Math.PI / 2.0)
            {
                i = Math.PI - i;
            }

            if (i <= Math.Abs(lat))
            {
                polis_mod = Orbit.InclinationNormal() + fi * dir;
                return true;
            }
            return false;
        }

        public double centralAngle(double t)
        {
            double u = Orbit.Anomalia(t) + Orbit.ArgumentOfPerigee;
            double semi_axis = Orbit.Semiaxis(u);
            return Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(Alpha1) / Globals.Re)) - Alpha1;
        }

        public override Geo2D ContinuousTrack(double node, double t, double tPastAN, TrackNodeQuarter quart)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double lonAscnNode = Orbit.LonAscnNode * MyMath.DegreesToRadians;

            double v = Orbit.Anomalia(t, tPastAN);
            double u = v + Orbit.ArgumentOfPerigee;

            double semi_axis = Orbit.Semiaxis(u);
            double angle = Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(Alpha1) / Globals.Re)) - Alpha1;
            double uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
            double iTr = inclination - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * dir;
            double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
            double asinlon = Math.Tan(lat) / Math.Tan(iTr);

            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = MyMath.Sign(asinlon);
            }

            double lon = Math.Asin(asinlon);

            switch (quart)
            {
                case TrackNodeQuarter.First:
                    //lon = lon;
                    break;
                case TrackNodeQuarter.Second:           
                case TrackNodeQuarter.Third:
                    lon = (Math.PI - lon);// - factor.ch23 * 2.0 * Math.PI;
                    break;
                case TrackNodeQuarter.Fourth:
                    lon = 2.0 * Math.PI + lon;// - factor.ch4 * 2.0 * Math.PI;
                    break;
                default:
                    break;
            }

            lon = lonAscnNode + lon - Globals.Omega * (t + tPastAN) + node * 2.0 * Math.PI;// * factor.mdf;
            return new Geo2D(lon, lat, GeoCoordTypes.Radians);
        }

        public override Geo2D TrackPoint(double u)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double uTr, iTr, angle;
            if (Alpha1 == 0.0)
            {
                angle = 0.0;
                uTr = u;
                iTr = inclination;
            }
            else
            {
                angle = CentralAngle(u);
                uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
                iTr = inclination - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * dir;
            }
            double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
            double asinlon = Math.Tan(lat) / Math.Tan(iTr);
            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = Math.Sign(asinlon);
            }

            double lon = 0.0;
            if (u >= 0.0 && u < Math.PI / 2.0)
            {
                lon = Math.Asin(asinlon);
            }
            else if (u >= Math.PI / 2.0 && u < 3.0 * Math.PI / 2.0)
            {
                lon = Math.PI - Math.Asin(asinlon);
            }
            else if (u >= 3.0 * Math.PI / 2.0 && u < 2.0 * Math.PI)
            {
                lon = 2.0 * Math.PI + Math.Asin(asinlon);
            }

            return new Geo2D(lon, lat);
        }

        private double CentralAngle(double u)
        {
            double semiAxis, alphaGround, angle;
            semiAxis = Orbit.Semiaxis(u);
            angle = Math.PI / 2.0 - (Math.Acos(semiAxis * Math.Sin(Alpha1) / Globals.Re)) - Alpha1;
            if (Alpha2 != 0.0)
            {
                alphaGround = Math.Atan(Math.Tan(Alpha2) * Math.Sin(Alpha1));
                angle = Math.Asin(Math.Sin(angle) / Math.Cos(alphaGround));
            }
            return angle;
        }

        public double Alpha1 { get; }
        public double Alpha2 { get; } = 0.0;
        public TrackPointDirection Direction { get; }

        protected int dir;
    }

    public class FactorTrack : CustomTrack
    {
        public FactorTrack(CustomTrack track, FactorShiftTrack factor) : base(track.Orbit, track.Alpha1 * MyMath.RadiansToDegrees, track.Direction)
        {
            this.factor = factor;
        }

        public override Geo2D ContinuousTrack(double node, double t, double tPastAN, TrackNodeQuarter quart)
        {
            double inclination = Orbit.Inclination * MyMath.DegreesToRadians;
            double lonAscnNode = Orbit.LonAscnNode * MyMath.DegreesToRadians;

            double v = Orbit.Anomalia(t, tPastAN);
            double u = v + Orbit.ArgumentOfPerigee;

            double semi_axis = Orbit.Semiaxis(u);
            double angle = Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(Alpha1) / Globals.Re)) - Alpha1;
            double uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
            double iTr = inclination - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * dir;
            double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
            double asinlon = Math.Tan(lat) / Math.Tan(iTr);

            if (Math.Abs(asinlon) > 1.0)
            {
                asinlon = MyMath.Sign(asinlon);
            }

            double lon = Math.Asin(asinlon);

            switch (quart)
            {
                case TrackNodeQuarter.First:
                    //lon = lon;
                    break;
                case TrackNodeQuarter.Second:                    
                case TrackNodeQuarter.Third:
                    lon = (Math.PI - lon) - factor.Quart23 * 2.0 * Math.PI;
                    break;
                case TrackNodeQuarter.Fourth:
                    lon = 2.0 * Math.PI + lon - factor.Quart4 * 2.0 * Math.PI;
                    break;
                default:
                    break;
            }

            lon = lonAscnNode + lon - Globals.Omega * (t + tPastAN) + node * 2.0 * Math.PI * factor.Offset;
            return new Geo2D(lon, lat, GeoCoordTypes.Radians);
        }

        private readonly FactorShiftTrack factor;
    }
}
