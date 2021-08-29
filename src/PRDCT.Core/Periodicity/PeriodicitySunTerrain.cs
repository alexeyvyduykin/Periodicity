using System;
using System.Collections.Generic;
using System.Linq;
using GlmSharp;

namespace Periodicity.Core
{
    public class PeriodicitySunTerrain : Periodicity
    {
        public PeriodicitySunTerrain(Periodicity core) : base(core)
        {
            DataIvals = new List<Ivals>();
        }

        private readonly double h_sun_min = 0.0 * MyMath.DegreesToRadians;

        private double tempJD0, tempBeginSecs, tempS0;

        private bool PRDCT_HSUN(double t, double lon, double lat)
        {
            double S = tempS0 + Globals.Omega * (t + tempBeginSecs);
            double La = S + lon;
            double xp = Globals.Re * Math.Cos(lat) * Math.Cos(La);
            double yp = Globals.Re * Math.Cos(lat) * Math.Sin(La);
            double zp = Globals.Re * Math.Sin(lat);
            double JD = tempJD0 + (t + tempBeginSecs) / 86400.0;
            dvec4 rs = Sun.Position(JD);
            double xr = rs[0] - xp;
            double yr = rs[1] - yp;
            double zr = rs[2] - zp;
            double dal = Math.Sqrt(xr * xr + yr * yr + zr * zr);
            double rsun = Math.Sqrt(rs[0] * rs[0] + rs[1] * rs[1] + rs[2] * rs[2]);
            double alpha = Math.Acos((dal * dal + Globals.Re * Globals.Re - rsun * rsun) / (2 * Globals.Re * dal));
            double ahsun = alpha - Math.PI / 2.0;
            if (ahsun < h_sun_min)
            {
                return false;
            }

            return true;
        }

        public void Create()
        {
            string curID = "";

            foreach (var ival in base.DataIvals)
            {
                if (curID != ival.SatelliteID)
                {
                    var sat = base.Satellites.Where(s => s.Name == ival.SatelliteID).FirstOrDefault();
                    tempJD0 = sat.StartTime.Date.ToOADate() + 2415018.5;
                    tempBeginSecs = sat.StartTime.TimeOfDay.TotalSeconds;
                    //tempS0 = MyFunction.uds1900(tempJD0);
                    Julian jd = new Julian(sat.StartTime.Date);
                    tempS0 = jd.ToGmst();
                }

                bool isLeft = PRDCT_HSUN(ival.TimeLeft, ival.LonLeft, ival.LatRAD);
                bool isRight = PRDCT_HSUN(ival.TimeRight, ival.LonRight, ival.LatRAD);

                // интервал полностью освещён
                if (isLeft == true && isRight == true)
                {
                    DataIvals.Add(ival);
                }
                // концы интервала не освещены, необходима проверка внутренней части
                else if (isLeft == false && isRight == false)
                {
                    //// ищем любую точку которая освещенна, если такой нет, то выходим
                    double step = 1.0 * MyMath.DegreesToRadians;
                    bool isCur = false;
                    double lonCur, tCur = 0.0;
                    for (lonCur = ival.LonLeft; lonCur <= ival.LonRight; lonCur += step)
                    {
                        tCur = ((ival.LonRight - lonCur) * ival.TimeLeft + (lonCur - ival.LonLeft) * ival.TimeRight) / (ival.LonRight - ival.LonLeft);
                        isCur = PRDCT_HSUN(tCur, lonCur, ival.LatRAD);
                        if (isCur == true)
                        {
                            break;
                        }
                    }

                    if (isCur == false)
                    {
                        continue;
                    }

                    DataIvals.Add(FuncSun2(ival, lonCur, tCur));
                }
                // интервал освещён частично
                //if( isLighting1 == false || isLighting2 == false )
                else
                {
                    DataIvals.Add(FuncSun1(ival, isLeft, isRight));
                }

            }

        }

        private Ivals FuncSun1(Ivals ival, bool is1, bool is2)
        {
            double lon1 = ival.LonLeft;
            double lon2 = ival.LonRight;

            double t1 = ival.TimeLeft;
            double t2 = ival.TimeRight;

            double lon_cur = lon1;
            double lon_prev, t_cur;

            do
            {
                lon_prev = lon_cur;
                lon_cur = 0.5 * (lon1 + lon2);
                t_cur = 0.5 * (t1 + t2);

                bool is_cur = PRDCT_HSUN(t_cur, lon_cur, ival.LatRAD);

                if (is_cur == is1)
                {
                    lon1 = lon_cur;
                    t1 = t_cur;
                }
                else
                {
                    lon2 = lon_cur;
                    t2 = t_cur;
                }
            }
            while (Math.Abs(lon_cur - lon_prev) > 0.01);


            var result = ival;

            if (is1 == true)
            {
                lon2 = lon_cur;
                t2 = t_cur;

                result.LonRight = lon2;
                result.TimeRight = t2;
            }
            else
            {
                lon1 = lon_cur;
                t1 = t_cur;

                result.LonLeft = lon1;
                result.TimeLeft = t1;
            }


            return result;
        }

        private Ivals FuncSun2(Ivals ival, double lonCur, double tCur)
        {
            Ivals ival1 = ival;
            ival1.LonRight = lonCur;
            ival1.TimeRight = tCur;
            Ivals ival2 = ival;
            ival2.LonLeft = lonCur;
            ival2.TimeLeft = tCur;

            var left = FuncSun1(ival1, false, true);
            var right = FuncSun1(ival2, true, false);

            var result = ival;
            result.LonLeft = left.LonLeft;
            result.LonRight = right.LonRight;
            result.TimeLeft = left.TimeLeft;
            result.TimeRight = right.TimeRight;
            return result;
        }

        public override List<Ivals> DataIvals { get; }
    }

    internal abstract class Core
    {
        public Core() { }
        public Core(Core other) { }

        public List<Satellite> Satellite { get; set; }
    }

    internal class Prdct : Core
    {
        public Prdct() : base() { }

        public List<TimeIvals> TimeIvals { get; set; }
        public virtual List<Ivals> Ivals { get; set; }
    }

    internal class SunTerrain : Prdct
    {
        //public SunTerrain(Prdct prdct) : base(prdct) { }

        public override List<Ivals> Ivals { get; set; }
    }

    internal class fdfd
    {
        private void fddfd()
        {
            Prdct p = new Prdct();

            //SunTerrain st = new SunTerrain(p);
            //
            // Graph gr1 = new Graph(st as Prdct);
            // Graph gr2 = new Graph(p);
        }
    }

    internal class Graph
    {
        public Graph(Prdct p) { }
    }

    // public abstract class PRDCTSun : PRDCTCore
    // {
    //     protected PRDCTSun(PRDCTCore core) : base(core) { }

    //     protected Tuple<dvec3, double> SunState(double ud)
    //     {
    //         double d, t, r, e, v, h;
    //         d = ud - 2415020.0; t = d / 36525.0;
    //         e = 0.1675104e-1 - (0.418e-4 + 0.126e-6 * t) * t;
    //         r = 6.25658378411 + 1.72019697677e-2 * d - 2.61799387799e-6 * t * t;
    //         v = 4.90822940869 + 8.21498553644e-7 * d + 7.90634151156e-6 * t * t;
    //         r = r + 2.0 * e * Math.Sin(r) + 1.25 * e * e * Math.Sin(2.0 * r);
    //         h = 4.09319747446e-1 - (2.27110968916e-4 + (2.86233997327e-8 -
    //           8.77900613756e-9 * t) * t) * t + 4.46513400244e-5 * Math.Cos(4.52360151485 -
    //           (3.37571462465e+1 - (3.62640633471e-5 + 3.87850944887e-8 * t) * t) * t);
    //         double rs3 = 149600034.408 * (1.0 - e * e) / (1.0 + e * Math.Cos(r));
    //         v = v + r; r = Math.Sin(v);
    //         double rs0 = rs3 * Math.Cos(v);
    //         double rs1 = rs3 * r * Math.Cos(h);
    //         double rs2 = rs3 * r * Math.Sin(h);
    //         return Tuple.Create(new dvec3(rs0, rs1, rs2), rs3);
    //     }

    //     protected bool isLighting(double jd0h, double s0, double tcur, double r, Geo2D satellitePosition)
    //     {
    //         double jd = jd0h + tcur / 86400.0;
    //         var rs = SunState(jd);

    //         dvec3 sunNorm = rs.Item1 / rs.Item2;

    //         double S = s0 + MyConst.w3 * tcur;
    //         double LA = S + satellitePosition.Lon;
    //         LA = MyMath.WrapAngle(LA);

    //         //while (LA > TWOPI) LA -= TWOPI;

    //         dvec3 satPos = MyConversion.SphericalToCartesian(r, LA, satellitePosition.Lat);

    //         double rSat = satPos.Length; // Math.Sqrt(xSat * xSat + ySat * ySat + zSat * zSat);
    //         double rSun = sunNorm.Length; // Math.Sqrt(xSunNorm * xSunNorm + ySunNorm * ySunNorm + zSunNorm * zSunNorm);

    //         double r_S = dvec3.DistanceSqr(satPos, sunNorm); // xSat * xSunNorm + ySat * ySunNorm + zSat * zSunNorm;

    //         double angle = Math.Acos(r_S / (rSat * rSun));

    //         r_S = Math.Abs(r_S);

    //         if (angle > Math.PI / 2.0)
    //             r_S = -r_S;

    //         double G = r_S + Math.Sqrt(rSat * rSat - MyConst.RE * MyConst.RE);

    //         if (G < 0.0)
    //             return false;
    //         return true;
    //     }

    //     new protected List<PRDCTDataIvalsRecord> DataIvals { get { return base.DataIvals; } }
    //     new protected List<PRDCTDataTimeIvalsRecord> DataTimeIvals { get { return base.DataTimeIvals; } }
    //     new protected List<PRDCTDataRegionCutsRecord> DataRegionCuts { get { return base.DataRegionCuts; } }
    //     new protected List<PRDCTDataPeriodicitiesRecord> DataPeriodicities { get { return base.DataPeriodicities; } }
    ////     new protected List<double> LatitudesDEG { get { return base.LatitudesDEG; } }
    // }

    // public class PRDCTSatelliteLighting : PRDCTSun
    // {
    //     public PRDCTSatelliteLighting(PRDCTCore core, int curIdSatellite) : base(core) { }

    //     public void Create()
    //     {
    //         var dataSunIvals = new List<PRDCTDataIvalsRecord>();

    //         foreach (var ival in base.DataIvals)
    //         {
    //             var band = base.Bands.Where(m => m.SatelliteID.ToString()/* IdSatellite*/ == ival.SatelliteID).Single();
    //             var orbit = (Orbit)band;

    //             double lonLeft = ival.left;
    //             double lonRight = ival.right;
    //             double tLeft = ival.tLeft;
    //             double tRight = ival.tRight;

    //             double rLeft = orbit.Radius(ival.tLeft);
    //             double rRight = orbit.Radius(ival.tRight);

    //             //               timeCorrection = band.TimeBegin_;

    //             Geo2D PSatLeft = (new Track(orbit/*, 0.0, TrackPointDirection.None*/)).Position(tLeft - timeCorrection);
    //             Geo2D PSatRight = (new Track(orbit/*, 0.0, TrackPointDirection.None*/)).Position(tRight - timeCorrection);
    //             //   jd0h= 2453249.5  s0= 5.93418 t= 43760.7497  r= 13319  lon= 3.34811   lat= 0.60506
    //             bool isLighting1 = isLighting(jd0h, s0, tLeft, rLeft, PSatLeft);
    //             bool isLighting2 = isLighting(jd0h, s0, tRight, rRight, PSatRight);
    //             /////////////////////////////////////////////////
    //             //////// интервал полностью освещён
    //             if (isLighting1 == true && isLighting2 == true)
    //             {
    //                 dataSunIvals.Add(ival);
    //                 continue;
    //             }
    //             ////////////////////////////////////////////////////////////////////////////////
    //             ///// концы интервала не освещены, необходима проверка внутренней части
    //             if (isLighting1 == false && isLighting2 == false)
    //             {
    //                 bool is_cur = false;
    //                 double lon_res = 0.0, t_res = 0.0, lmn = lonLeft, lmk = lonRight, tn = tLeft, tk = tRight;
    //                 // ищем любую точку которая освещенна, если такой нет, то выходим
    //                 if (RekursiaSun(orbit, ival.latRAD, ref lmn, ref lmk, ref tn, ref tk, out lon_res, out t_res, out is_cur) == false)
    //                     continue;

    //                 var left = FuncSun1(orbit, ival.latRAD, lonLeft, lon_res, tLeft, t_res, isLighting1, true);
    //                 var right = FuncSun1(orbit, ival.latRAD, lon_res, lonRight, t_res, tRight, true, isLighting2);

    //                 dataSunIvals.Add(ival);

    //                 dataSunIvals.Last().left = left.Item1;
    //                 dataSunIvals.Last().right = right.Item2;
    //                 dataSunIvals.Last().tLeft = left.Item3;
    //                 dataSunIvals.Last().tRight = right.Item4;
    //                 continue;
    //             }
    //             /////////////////////////////////////////////////
    //             //////// интервал освещён частично
    //             //if( isLighting1 == false || isLighting2 == false )

    //             var ivals = FuncSun1(orbit, ival.latRAD, lonLeft, lonRight, tLeft, tRight, isLighting1, isLighting2);

    //             dataSunIvals.Add(ival);

    //             dataSunIvals.Last().left = ivals.Item1;
    //             dataSunIvals.Last().right = ivals.Item2;
    //             dataSunIvals.Last().tLeft = ivals.Item3;
    //             dataSunIvals.Last().tRight = ivals.Item4;
    //             continue;
    //         }
    //     }

    //     private double timeCorrection;

    //     private bool RekursiaSun(Orbit orbit, double Lat, ref double LonN, ref double LonK, ref double TN, ref double TK, out double LonRes, out double tRes, out bool Is)
    //     {
    //         double lmn = LonN, lmk = LonK, tn = TN, tk = TK, lmprev = LonN;
    //         double lm = 0.5 * (lmn + lmk);
    //         double t = 0.5 * (tn + tk);

    //         double r = orbit.Radius(t);

    //         Geo2D PSat = (new Track(orbit/*, 0.0, TrackPointDirection.None*/)).Position(t - timeCorrection);
    //         Is = isLighting(jd0h, s0, t, r, PSat);

    //         if (Is == true)
    //         {
    //             LonRes = lm;
    //             tRes = t;
    //             return Is;
    //         }

    //         if (Math.Abs(lm - lmprev) > 0.1 && Is == false)
    //         {
    //             lmn = lm; tn = t;
    //             lmk = LonK;
    //             RekursiaSun(orbit, Lat, ref lmn, ref lmk, ref tn, ref tk, out LonRes, out tRes, out Is);
    //         }

    //         if (Math.Abs(lm - lmprev) > 0.1 && Is == false)
    //         {
    //             lmk = lm; tk = t;
    //             lmn = LonN;
    //             RekursiaSun(orbit, Lat, ref lmn, ref lmk, ref tn, ref tk, out LonRes, out tRes, out Is);
    //         }

    //         LonRes = 0.0;
    //         tRes = 0.0;
    //         return false;
    //     }

    //     private Tuple<double, double, double, double> FuncSun1(Orbit orbit, double lat, double lon1, double lon2, double t1, double t2, bool is1, bool is2)
    //     {
    //         double lon_cur = lon1;
    //         double lon_prev, t_cur;

    //         do
    //         {
    //             lon_prev = lon_cur;
    //             lon_cur = 0.5 * (lon1 + lon2);
    //             t_cur = 0.5 * (t1 + t2);

    //             double r = orbit.Radius(t_cur);

    //             Geo2D PSat = (new Track(orbit/*, 0.0, TrackPointDirection.None*/)).Position(t_cur - timeCorrection);

    //             bool is_cur = isLighting(jd0h, s0, t_cur, r, PSat);

    //             if (is_cur == is1)
    //             {
    //                 lon1 = lon_cur;
    //                 t1 = t_cur;
    //             }
    //             else
    //             {
    //                 lon2 = lon_cur;
    //                 t2 = t_cur;
    //             }
    //         }
    //         while (Math.Abs(lon_cur - lon_prev) > 0.01);

    //         if (is1 == true)
    //         {
    //             lon2 = lon_cur;
    //             t2 = t_cur;
    //         }
    //         else
    //         {
    //             lon1 = lon_cur;
    //             t1 = t_cur;
    //         }
    //         //return new Interval<PRDCTPoint>(new PRDCTPoint(new Geo2D(lon1, lat), t1), true, new PRDCTPoint(new Geo2D(lon2, lat), t2), true);
    //         return Tuple.Create(lon1, lon2, t1, t2);
    //     }

    // }


}
