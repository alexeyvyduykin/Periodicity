using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace PRDCT.Core.PRDCTPeriodicity
{
    public class Interval<T> where T : IComparable<T>
    {
        public Interval(T start, bool hasStart, T end, bool hasEnd)
        {
            Start = start;
            HasStart = hasStart;
            End = end;
            HasEnd = hasEnd;
        }
    
        public static Interval<T> Create(Interval<T> obj)
        {
            return new Interval<T>(obj.Start, obj.HasStart, obj.End, obj.HasEnd);
        }

        public void SwapTest()
        {
            if(Start.CompareTo(End) < 0)
            {
                T temp = End;
                End = Start;
                Start = temp;
            }
        }

        public T Start { get; private set; }
        public T End { get; private set; }

        public bool HasStart { get; private set; }
        public bool HasEnd { get; private set; }
    }

    //public abstract class PRDCTConditionSun : PRDCTCore
    //{
    //    public abstract bool isLightingIval(double lat, ref double lon1, ref double t1, ref double lon2, ref double t2);

    //    protected PRDCTConditionSun(PRDCTCore core) : base(core) { }

    //    protected double[] SunState(double ud)
    //    {
    //        double d, t, r, e, v, h;
    //        d = ud - 2415020.0; t = d / 36525.0;
    //        e = 0.1675104e-1 - (0.418e-4 + 0.126e-6 * t) * t;
    //        r = 6.25658378411 + 1.72019697677e-2 * d - 2.61799387799e-6 * t * t;
    //        v = 4.90822940869 + 8.21498553644e-7 * d + 7.90634151156e-6 * t * t;
    //        r = r + 2.0 * e * Math.Sin(r) + 1.25 * e * e * Math.Sin(2.0 * r);
    //        h = 4.09319747446e-1 - (2.27110968916e-4 + (2.86233997327e-8 -
    //          8.77900613756e-9 * t) * t) * t + 4.46513400244e-5 * Math.Cos(4.52360151485 -
    //          (3.37571462465e+1 - (3.62640633471e-5 + 3.87850944887e-8 * t) * t) * t);
    //        double rs3 = 149600034.408 * (1.0 - e * e) / (1.0 + e * Math.Cos(r));
    //        v = v + r; r = Math.Sin(v);
    //        double rs0 = rs3 * Math.Cos(v);
    //        double rs1 = rs3 * r * Math.Cos(h);
    //        double rs2 = rs3 * r * Math.Sin(h);
    //        return new double[] { rs0, rs1, rs2, rs3 };
    //    }

    //    protected bool isLighting(double jd0h, double s0, double tcur, double r, Geo2D satellitePosition)
    //    {
    //        double jd = jd0h + tcur / 86400.0;
    //        double[] rs = SunState(jd);

    //        double xSunNorm = rs[0] / rs[3];
    //        double ySunNorm = rs[1] / rs[3];
    //        double zSunNorm = rs[2] / rs[3];

    //        double S = s0 + MyConst.w3 * tcur;
    //        double LA = S + satellitePosition.Lon;
    //        LA = MyMath.WrapAngle(LA);

    //        //while (LA > TWOPI) LA -= TWOPI;

    //        double xSat = r * Math.Cos(satellitePosition.Lat) * Math.Cos(LA);
    //        double ySat = r * Math.Cos(satellitePosition.Lat) * Math.Sin(LA);
    //        double zSat = r * Math.Sin(satellitePosition.Lat);

    //        double rSat = Math.Sqrt(xSat * xSat + ySat * ySat + zSat * zSat);
    //        double rSun = Math.Sqrt(xSunNorm * xSunNorm + ySunNorm * ySunNorm + zSunNorm * zSunNorm);
    //        double r_S = xSat * xSunNorm + ySat * ySunNorm + zSat * zSunNorm;


    //        double angle = Math.Acos(r_S / (rSat * rSun));

    //        r_S = Math.Abs(r_S);

    //        if (angle > Math.PI / 2.0)
    //            r_S = -r_S;

    //        double G = r_S + Math.Sqrt(rSat * rSat - MyConst.RE * MyConst.RE);

    //        if (G < 0.0)
    //            return false;
    //        return true;
    //    }

    //}


    //public class PRDCTConditionSatelliteLighting : PRDCTConditionSun
    //{
    //    public PRDCTConditionSatelliteLighting(PRDCTCore core, Guid curIdSatellite) : base(core)
    //    {
    //        this.curIdSatellite = curIdSatellite;
    //    }


    //    //       public Interval<PRDCTPoint> CreateLightingIval(Interval<PRDCTPoint> sourceIval)
    //    //       {
    //    //           var ival = Interval<PRDCTPoint>.Create(sourceIval);
    //    //           //         var band = base.FindBand(curIdSatellite);
    //    //           Band band = base.Bands.Where(m => m.SatelliteID/* IdSatellite*/ == curIdSatellite).Single();

    //    //           double rLeft = band.Radius(ival.Start.Time);
    //    //           double rRight = band.Radius(ival.End.Time);

    //    ////           timeCorrection = band.TimeBegin_;

    //    //           Geo2D PSatLeft = band.Position(ival.Start.Time - timeCorrection);
    //    //           Geo2D PSatRight = band.Position(ival.End.Time - timeCorrection);
    //    //           //   jd0h= 2453249.5  s0= 5.93418 t= 43760.7497  r= 13319  lon= 3.34811   lat= 0.60506
    //    //           bool isLighting1 = isLighting(jd0h, s0, ival.Start.Time, rLeft, PSatLeft);
    //    //           bool isLighting2 = isLighting(jd0h, s0, ival.End.Time, rRight, PSatRight);



    //    //           return ival;
    //    //       }


    //    //       public override bool isLightingIval(double lat, ref double lon1, ref double t1, ref double lon2, ref double t2)
    //    //       {
    //    //           //std::multimap<int, PRDCTBand*>::const_iterator it;
    //    //           //it = mapBands.find(*curIdSatellite);

    //    //           //      var band = base.FindBand(curIdSatellite);// bands.ElementAt(curIdSatellite);
    //    //           Band band = base.Bands.Where(m => m.SatelliteID/* IdSatellite*/ == curIdSatellite).Single();

    //    //           double lonLeft = lon1;
    //    //           double lonRight = lon2;
    //    //           double tLeft = t1;
    //    //           double tRight = t2;

    //    //           double rLeft = band.Radius(tLeft);
    //    //           double rRight = band.Radius(tRight);

    //    ////           timeCorrection = band.TimeBegin_;

    //    //           Geo2D PSatLeft = band.Position(tLeft - timeCorrection);
    //    //           Geo2D PSatRight = band.Position(tRight - timeCorrection);
    //    //           //   jd0h= 2453249.5  s0= 5.93418 t= 43760.7497  r= 13319  lon= 3.34811   lat= 0.60506
    //    //           bool isLighting1 = isLighting(jd0h, s0, tLeft, rLeft, PSatLeft);
    //    //           bool isLighting2 = isLighting(jd0h, s0, tRight, rRight, PSatRight);
    //    //           /////////////////////////////////////////////////
    //    //           //////// интервал полностью освещён
    //    //           if (isLighting1 == true && isLighting2 == true)
    //    //               return true;
    //    //           ////////////////////////////////////////////////////////////////////////////////
    //    //           ///// концы интервала не освещены, необходима проверка внутренней части
    //    //           if (isLighting1 == false && isLighting2 == false)
    //    //           {
    //    //               bool is_cur = false;
    //    //               double lon_res = 0.0, t_res = 0.0, lmn = lonLeft, lmk = lonRight, tn = tLeft, tk = tRight;
    //    //               // ищем любую точку которая освещенна, если такой нет, то выходим
    //    //               if (RekursiaSun(lat, ref lmn, ref lmk, ref tn, ref tk, out lon_res, out t_res, out is_cur) == false)
    //    //                   return false;

    //    //               //PRDCTInterval left = new PRDCTInterval(lat, lonLeft, lon_res, tLeft, t_res, isLighting1, true);
    //    //               //PRDCTInterval right = new PRDCTInterval(lat, lon_res, lonRight, t_res, tRight, true, isLighting2);
    //    //               //FuncSun1(left);
    //    //               //FuncSun1(right);

    //    //               Interval<PRDCTPoint> left = FuncSun1(lat, lonLeft, lon_res, tLeft, t_res, isLighting1, true);
    //    //               Interval<PRDCTPoint> right = FuncSun1(lat, lon_res, lonRight, t_res, tRight, true, isLighting2);

    //    //               lon1 = left.Start.Position.Lon;
    //    //               lon2 = right.End.Position.Lon;
    //    //               t1 = left.Start.Time;
    //    //               t2 = right.End.Time;
    //    //               return true;
    //    //           }
    //    //           /////////////////////////////////////////////////
    //    //           //////// интервал освещён частично
    //    //           //if( isLighting1 == false || isLighting2 == false )

    //    //           //PRDCTInterval ival = new PRDCTInterval(lat, lonLeft, lonRight, tLeft, tRight, isLighting1, isLighting2);
    //    //           //FuncSun1(ival);
    //    //           Interval<PRDCTPoint> ival = FuncSun1(lat, lonLeft, lonRight, tLeft, tRight, isLighting1, isLighting2);

    //    //           lon1 = ival.Start.Position.Lon;
    //    //           lon2 = ival.End.Position.Lon;
    //    //           t1 = ival.Start.Time;
    //    //           t2 = ival.End.Time;
    //    //           return true;
    //    //       }

    //    private Guid curIdSatellite;

    //    private double timeCorrection;

    //    private bool RekursiaSun(double Lat, ref double LonN, ref double LonK, ref double TN, ref double TK, out double LonRes, out double tRes, out bool Is)
    //    {
    //        double lmn = LonN, lmk = LonK, tn = TN, tk = TK, lmprev = LonN;
    //        double lm = 0.5 * (lmn + lmk);
    //        double t = 0.5 * (tn + tk);

    //        //*Is = PRDCT_HSUN(t,lm);
    //        //std::multimap<int, PRDCTBand*>::const_iterator it;
    //        //it = mapBands.find(*curIdSatellite);

    //        //          var band = base.FindBand(curIdSatellite);// Bands.ElementAt(curIdSatellite);
    //        Band band = base.Bands.Where(m => m.SatelliteID/* IdSatellite*/ == curIdSatellite).Single();
    //        double r = band.Radius(t);

    //        Geo2D PSat = band.Position(t - timeCorrection);
    //        Is = isLighting(jd0h, s0, t, r, PSat);

    //        if (Is == true)
    //        {
    //            LonRes = lm;
    //            tRes = t;
    //            return Is;
    //        }

    //        if (Math.Abs(lm - lmprev) > 0.1 && Is == false)
    //        {
    //            lmn = lm; tn = t;
    //            lmk = LonK;
    //            RekursiaSun(Lat, ref lmn, ref lmk, ref tn, ref tk, out LonRes, out tRes, out Is);
    //        }

    //        if (Math.Abs(lm - lmprev) > 0.1 && Is == false)
    //        {
    //            lmk = lm; tk = t;
    //            lmn = LonN;
    //            RekursiaSun(Lat, ref lmn, ref lmk, ref tn, ref tk, out LonRes, out tRes, out Is);
    //        }

    //        LonRes = 0.0;
    //        tRes = 0.0;
    //        return false;
    //    }

    //    private Interval<PRDCTPoint> FuncSun1(double lat, double lon1, double lon2, double t1, double t2, bool is1, bool is2)
    //    {
    //        double lon_cur = lon1;
    //        double lon_prev, t_cur;

    //        Band band = base.Bands.Where(m => m.SatelliteID/* IdSatellite*/ == curIdSatellite).Single();
    //        //      var band = base.FindBand(curIdSatellite);// Bands.ElementAt(curIdSatellite);
    //        do
    //        {
    //            lon_prev = lon_cur;
    //            lon_cur = 0.5 * (lon1 + lon2);
    //            t_cur = 0.5 * (t1 + t2);

    //            double r = band.Radius(t_cur);

    //            Geo2D PSat = band.Position(t_cur - timeCorrection);

    //            bool is_cur = isLighting(jd0h, s0, t_cur, r, PSat);
    //            //is_cur   = PRDCT_HSUN(t_cur, lon_cur);

    //            if (is_cur == is1)
    //            {
    //                lon1 = lon_cur;
    //                t1 = t_cur;
    //            }
    //            else
    //            {
    //                lon2 = lon_cur;
    //                t2 = t_cur;
    //            }
    //        }
    //        while (Math.Abs(lon_cur - lon_prev) > 0.01);

    //        if (is1 == true)
    //        {
    //            lon2 = lon_cur;
    //            t2 = t_cur;
    //        }
    //        else
    //        {
    //            lon1 = lon_cur;
    //            t1 = t_cur;
    //        }
    //        return new Interval<PRDCTPoint>(new PRDCTPoint(new Geo2D(lon1, lat), t1), true, new PRDCTPoint(new Geo2D(lon2, lat), t2), true);
    //    }

    //}

}
