using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using PRDCT.Core.TEST.PUnDE200;

namespace PRDCT.Core.TEST.Main
{
    public enum SolarSystemPlanets
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4,
        Saturn = 5,
        Uranus = 6,
        Neptune = 7,
        Pluto = 8,
        Moon = 9,
        Sun = 10
    }

    public static class Consts
    {
        public static double JD2000 = 2451545.0;
        public static double JulianC = 36525.0;
        public static double DifEpoch = 2400000.5;

        public static double GaussConst = 0.0172020989500000e0;
        // { in AstrUnit**3/((Ephemer.Day**2)*MassOfSun if in Sqr}
        public static double CavendishConst = 6.672e-20;   //  { in km**3/(kg* s**2) }
        public static double AstrUnit = 1.49597870691e8;  //{ in km }
        public static double VelOfLight = 299792.4580;  // { in km/s }

        public static double GeoFM = 3.98600448e5;// { km**3/s**2 }
        public static double GeoR0 = 6.37814000e3;// { km }
        public static double ParmA = 1.0 / 298.257; // oblateness of the Earth
        public static double VelOfRot = 0.7292115e-4; // radian per second Earth rotation velosity

        public static double PiTwo = Math.PI / 2;
        public static double HalfPi = PiTwo;
        public static double Pi22 = 2 * Math.PI;
        public static double TwoPi = Pi22;
        public static double GraRad = Math.PI / 180;
        public static double RadGra = 180 / Math.PI;
        public static double SecRad = GraRad / 3600;
        public static double RadSec = 3600 / GraRad;

        public static string[] StrDayWeek = new string[]
{ "Monday" , "Tuesday" , "Wednesday" , "Thursday" , "Friday" , "Saturday" , "Sunday" };


        //public static string[] StrDayWeek = new string[]        // to UnForTim
        //       { 'понедельник' , 'вторник' , 'среда' ,
        //         'четверг' , 'пятница' , 'суббота' ,
        //         'воскресенье' };
        //public static string[] StrMonth = new string[] {
        //        "январь"  ,  "февраль" ,  "март" ,
        //         'апрель'  ,  'май'     ,  'июнь' ,
        //         'июль'    ,  'август'  ,  'сентябрь' ,
        //         'октябрь' ,  'ноябрь'  ,  'декабрь' };

    }

    public static class CoordConverter
    {
        //  { ClcPrecMatr is a procedure
        //    to calculate the Matrix of precession
        //       for change from   Epoch to  TCur or another words
        //       from the fixed equator and ecliptic to the instantaneous ones.
        //      Fundamental  EPOCH is J2000.0
        //         but  Epoch as parameter
        //              may be any fixed epoch in  JD
        //                 and  TCur is epoch of the Date in  JD.
        //       PrecMatr[0..2, 0..2] is the resulting massive. }

        public static void ClcPrecMatr(double Epoch, double TCur, out dmat3 PrecMatr)
        {
            double de = (Epoch - Consts.JD2000) / Consts.JulianC; double de2 = de * de; // constants from UnConTyp
            double dt = (TCur - Epoch) / Consts.JulianC; double dt2 = dt * dt; double dt3 = dt2 * dt;
            double r = (2306.2181 + 1.39656 * de - 0.000139 * de2) * dt;
            double Dzita0 = r + (0.30188 - 0.000344 * de) * dt2 + 0.017998 * dt3;
            double Teta = (2004.3109 - 0.85330 * de - 0.000217 * de2) * dt - (0.42665 + 0.000217 * de) * dt2 - 0.041833 * dt3;
            double Zet = r + (1.09468 + 0.000066 * de) * dt2 + 0.018203 * dt3;
            double u1 = MyMath.SecondsToRadians * Dzita0;
            double u2 = MyMath.SecondsToRadians * Teta;
            double u3 = MyMath.SecondsToRadians * Zet;
            double s1 = Math.Sin(u1); double s2 = Math.Sin(u2); double s3 = Math.Sin(u3);
            double c1 = Math.Cos(u1); double c2 = Math.Cos(u2); double c3 = Math.Cos(u3);
            PrecMatr.m00 = +c3 * c2 * c1 - s3 * s1; PrecMatr.m01 = -c3 * c2 * s1 - s3 * c1; PrecMatr.m02 = -c3 * s2;
            PrecMatr.m10 = +s3 * c2 * c1 + c3 * s1; PrecMatr.m11 = -s3 * c2 * s1 + c3 * c1; PrecMatr.m12 = -s3 * s2;
            PrecMatr.m20 = +s2 * c1; PrecMatr.m21 = -s2 * s1; PrecMatr.m22 = +c2;
        }

        //  { ClcTrueRotMatr is a procedure
        //       to calculate the Matrix to change
        //       from the fixed equator and ecliptic
        //       to the system of the true equator and instantaneous ecliptic.
        //       Fundamental  EPOCH is J2000.0
        //       TCur is epoch of the Date in  JD.
        //       PrecMatr and MatrNut is declared in UnGloVar
        //       RotMatr[1..3, 1..3] is the resulting massive.
        //       RosMatr[1..3, 1..3] is the matrix for the sidereal time }

        public static void ClcTrueRotMatr(double Epoch, double TCur, out dmat3 RotMatr, out dmat3 RosMatr)
        {
            double DeltaEps, DeltaPsi, EpsMean;
            Nutations.ClcNut(TCur, out DeltaPsi, out DeltaEps, out EpsMean); //{ in radian }
            double se = Math.Sin(EpsMean);
            double ce = Math.Cos(EpsMean);

            dmat3 MatrEps = new dmat3
            {
                m00 = 1.0,
                m01 = 0.0,
                m02 = 0.0,
                m10 = 0.0,
                m11 = ce,
                m12 = se,
                m20 = 0.0,
                m21 = -se,
                m22 = ce
            };

            Global.MatrEcl = MatrEps;

            double s = Math.Sin(DeltaPsi);
            double c = Math.Cos(DeltaPsi);

            dmat3 MatrPsi = new dmat3
            {
                m00 = +c,
                m01 = -s,
                m02 = 0.0,
                m10 = +s,
                m11 = +c,
                m12 = 0.0,
                m20 = 0.0,
                m21 = 0.0,
                m22 = +1
            };

            dmat3 MatrCur = MatrPsi * MatrEps;

            double e = EpsMean + DeltaEps;
            s = Math.Sin(e);
            c = Math.Cos(e);

            MatrEps = new dmat3
            {
                m00 = +1,
                m01 = 0.0,
                m02 = 0.0,
                m10 = 0.0,
                m11 = +c,
                m12 = -s,
                m20 = 0.0,
                m21 = +s,
                m22 = +c
            };

            Global.MatrNut = MatrEps * MatrCur;

            ClcPrecMatr(Epoch, TCur, out Global.PrecMatr);

            RotMatr = Global.MatrNut * Global.PrecMatr;

            double SidTime = JulianDateTime.ToGetGrMSTime(TCur);
            //{ mean sidereal time unit UnForTim }
            double ts = SidTime + DeltaPsi * ce;
            //{ true sidereal time }
            s = Math.Sin(ts); // RosMatr matrix of rotation R_z(teta)
            c = Math.Cos(ts); // from true equinox to greenwich point

            RosMatr = new dmat3
            {
                m00 = +c,
                m01 = +s,
                m02 = 0.0,
                m10 = -s,
                m11 = +c,
                m12 = 0.0,
                m20 = 0.0,
                m21 = 0.0,
                m22 = +1
            };
        }

        // for option 'S' 'W' topocentric matrix
        public static void ClcTopMatr(Global.TPlaceCooRec pc, // for station
        dmat3 rm,     // rotation matrix
                          out dvec3 ps,  // station position
                               out dmat3 rt)  // topo matrix
        {
            dvec3 PosGeodet = new dvec3();

            // pc with information about station
            double uf = MyMath.DegreesToRadians * pc.f;
            //{ latitude to radian }
            double ul = MyMath.DegreesToRadians * pc.l;
            //{ longitude to radian }
            PosGeodet.x = 1.0e-3 * pc.x;
            //{ in km }
            PosGeodet.y = 1.0e-3 * pc.y;
            //{ geo rotating system }
            PosGeodet.z = 1.0e-3 * pc.z;
            //{ in km }

            //{ station position from greenwich to true equator }
            ps = rm * PosGeodet;

            //{ teta greenwich true sidereal time }
            double c = rm[1, 1];
            //{ cos(teta) }
            double s = rm[1, 2];
            //{ sin(teta) }
            double sf = Math.Sin(uf);
            double cf = Math.Cos(uf);
            double sl = Math.Sin(ul);
            double cl = Math.Cos(ul);
            double st = s * cl + c * sl;
            //{ sin(teta + lambda) }
            double ct = c * cl - s * sl;
            //{ cos(teta + lambda) }
            SimpleProcedures.ArcTanTwoArg(st, ct, out Global.LocalSTime);
            //{ local sidereal time in radians }
            //      {
            //          rt matrix to change
            ///            from true equator system
            //              to topocentric horizontal system
            //              R_z(180) * R_y(90 - phi) * R_z(teta + lambda)
            //               where phi latitude lambda longitude of the point

            //     after this transformation

            //     axis OX direct to the north

            //      axis OZ direct to the zenith }
            rt = new dmat3
            {
                m00 = -sf * ct,
                m01 = -sf * st,
                m02 = +cf,
                m10 = +st,
                m11 = -ct,
                m12 = 0.0,
                m20 = +cf * ct,
                m21 = +cf * st,
                m22 = +sf
            };
        }

        //{ om orbital matrix to change
        //     from true equator system
        //     to orbital system refer to the satellite
        //     elements of the satellite are in record SatElemLook UnCoTVar
        //     axis OX direct as transversal from the satellite
        //     axis OZ direct along radius-vector
        //     R_x(90)* R_z(90)* R_z(u)* R_x(i)* R_z(Omega)
        //        where simple satellite orbit parameters

        //        u is argument of latitude

        //        i is angle of inclination

        //        Omega is longitude of ascending node }

        public static void ClcOrbMatr(double tc, // moment julian date
        out dvec3 po, // object position
                               ref dmat3 om) // orbital matr
        {  // record SatElemLook UnCoTVar satellite in true equator
            dvec3 x, v; // position velosity in km satellite
            EilP.EilXV(tc, Types.SatElemLook, out x, out v); // UnitEilP  refer to the geo centre
            po = x; // satellite position the same as StationPos[..]
            Kepler.ToObtainOrbitalMatr(x, v, ref om); // our matrix to change to orbital
            x = om.Column0; // to replace elements of the orbital matrix UnitElmK
            om.Column0 = om.Column1; // x' = y
            om.Column1 = om.Column2; // y' = z
            om.Column2 = x;     // z' = x
        }                          // from UnitEilP instead TopMatr

        //  { ClcThreeRotMatr is a procedure
        //       to calculate the Matrix to change
        //       from the fixed equator and ecliptic
        //       to the system of the true equator and instantaneous ecliptic.
        //       Fundamental  EPOCH is J2000.0
        //       TCur is epoch of the Date in  JD.
        //       PrecMatr and MatrNut is declared in UnGloVar
        //       RotMatr[1..3, 1..3] is the resulting massive.
        //       RosMatr[1..3, 1..3] is the matrix for the sidereal time
        //       TopMatr[1..3, 1..3] is the matrix
        //              to change to topocentric system for 'S' sky
        //              to change to orbital system for 'L' look option }

        public static void ClcThreeRotMatr(double Epoch, double TCur,
       Global.TPlaceCooRec PlaceCoor, ref dmat3 RotMatr, ref dmat3 RosMatr, ref dmat3 TopMatr)
        {
            ClcTrueRotMatr(Epoch, TCur, out RotMatr, out RosMatr);
            //           { 'S' option
            //             TopMatr[...] to change from true equator to horizont
            //             'L' option
            //    TopMatr[...] is simple orbital matrix
            //                          to change from true equator to object system }
            switch (Global.CharForView) // for 'S' 'W' topocentric 'L' orbital
            {
                case 'S':
                case 'W': ClcTopMatr(PlaceCoor, RosMatr, out Global.StationPos, out TopMatr); break;
                case 'L': ClcOrbMatr(TCur, out Global.StationPos, ref TopMatr); break; // for satellite
            }
        }

        //{ ClcSpherCoor is a procedure
        //   to obtain the spherical coordinates of the planet
        //     from cartesian ones }

        public static void ClcSpherCoor(vec3 Pos, out double Alfa, out double Delta, out double Ro)
        {
            vec3 xr = Pos;
            double a, b;
            double qq = Math.Pow(xr.x, 2) + Math.Pow(xr.y, 2);
            double rr = qq + Math.Pow(xr.z, 2);
            double r = Math.Sqrt(rr);
            double q = Math.Sqrt(qq);
            double p = 1 / q;
            double f = 1 / r;
            double c = p * xr.x;
            double s = p * xr.y;
            SimpleProcedures.ArcTanTwoArg(s, c, out a); // unit UnForFun
            c = q * f;
            s = xr.z * f;
            SimpleProcedures.ArcTanTwoArg(s, c, out b);
            if (a < 0)
                a = a + 2.0 * Math.PI;
            Alfa = a;
            Delta = b;
            Ro = r;
        }

        // { ClcSpherCoorInDegree is a procedure
        //   to obtain the spherical coordinates of the planet from cartesian ones }

        public static void ClcSpherCoorInDegree(dvec3 Pos, out double Alfa, out double Delta, out double Ro)
        {
            double a, b;
            double qq = Pos.x * Pos.x + Pos.y * Pos.y;
            double rr = qq + Pos.z * Pos.z;
            double r = Math.Sqrt(rr);
            double q = Math.Sqrt(qq);
            double p = 1 / q;
            double f = 1 / r;
            double c = p * Pos.x;
            double s = p * Pos.y;
            SimpleProcedures.ArcTanTwoArg(s, c, out a);
            c = q * f;
            s = Pos.z * f;
            SimpleProcedures.ArcTanTwoArg(s, c, out b);
            if (a < 0)
                a = a + 2.0 * Math.PI;
            Alfa = MyMath.RadiansToDegrees * a;
            Delta = MyMath.RadiansToDegrees * b;
            Ro = r;
        }

        // { ClcOrbCoorInDegree is a procedure
        //   to obtain the orbital coordinates in spherical form }

        public static void ClcOrbCoorInDegree(vec3 Pos, out double Azt, out double Zan, out double Ro)
        {
            vec3 xr = new vec3(Pos.y, Pos.z, Pos.x);
            double s, c, a, b;
            double qq = xr.x * xr.x + xr.y * xr.y;
            double rr = qq + xr.z * xr.z;
            double r = Math.Sqrt(rr);
            double q = Math.Sqrt(qq);
            if (r < 0.01 || q < 0.01) // to compare distance in km
            // distance in kilometer is too small
            {
                Azt = 0.0;
                Zan = -100.0;
                Ro = 0.0;
                return; // it is necessary to exclude this point
            }
            double p = 1 / q;
            double f = 1 / r;
            //{ special rotation }
            c = +p * xr.x;
            s = -p * xr.y;
            SimpleProcedures.ArcTanTwoArg(s, c, out a); // UnForFun
            c = +q * f;
            s = +xr.z * f;
            SimpleProcedures.ArcTanTwoArg(s, c, out b); // UnForFun
            if (a < 0)
                a = a + 2.0 * Math.PI; // const UnConTyp
            Azt = MyMath.RadiansToDegrees * a;
            //{ azimut in degree UnConTyp }
            Zan = 90.0 - MyMath.RadiansToDegrees * b;
            //{ zenith distance in degree }
            Ro = r;
        }

        // { ClcTopCoorInDegree is a procedure
        //   to obtain the topocentric coordinates in spherical form }

        public static void ClcTopCoorInDegree(dvec3 Pos, out double Azt, out double Alt, out double Ro)
        {
            double a, b, zr;
            dvec3 xr = Pos;
            double qq = xr.x * xr.x + xr.y * xr.y;
            double rr = qq + xr.z * xr.z;
            double r = Math.Sqrt(rr); // distance in km
            double q = Math.Sqrt(qq);
            if (r < 0.01 || q < 0.01) // to compare distance in km
            // distance in kilometer is too small
            {
                Azt = 0.0;
                Alt = -100.0;
                Ro = 0.0;
                return; // it is necessary to exclude this point
            }
            double p = 1 / q;
            double f = 1 / r;
            double c = +p * xr.x;
            double s = -p * xr.y;
            SimpleProcedures.ArcTanTwoArg(s, c, out a);
            c = q * f;
            s = xr.z * f;
            SimpleProcedures.ArcTanTwoArg(s, c, out b);
            if (a < 0)
                a = a + 2.0 * Math.PI;
            Azt = MyMath.RadiansToDegrees * a; // azimuth  in degree
            Alt = MyMath.RadiansToDegrees * b; // altitude in degree
            Ro = r;
            if (Global.CharForView == 'L' || !Global.BooRefraction)
                return; // without refraction correction
            //{ to add refraction }
            zr = Refraction.ToGetRefraction(Alt);
            Alt = Alt + zr;
        }

        // { to obtain spherical coordinates(h in metr )
        //      of the station from the cartesian ones in metr }

        public static void GeoSpherFromCart(double x, double y, double z, out double f, out double v, out double h)
        {
            double sf = 0.0, cf = 0.0, hg = 0.0, gg = 0.0, ff, vv;
            double r = 6378144.11;// { in metr }
            double a = 1.0 / 298.257;
            double e = 2.0 * a - a * a;
            double pp = Math.Sqrt(x * x + y * y);
            if (pp < 1.0e4)
                r = 1.0e-3 * r;
            //{ in km }
            double cv = x / pp;
            double sv = y / pp;
            for (int irf = 0; irf < 4; irf++)
            {
                double sff = z * (1 + hg);
                double cff = pp * (1 - e + hg);
                double cc = Math.Sqrt(sff * sff + cff * cff);
                sf = sff / cc;
                cf = cff / cc;
                gg = r / Math.Sqrt(1 - e * sf * sf);
                hg = pp / (gg * cf) - 1.0;
            }
            SimpleProcedures.ArcTanTwoArg(sf, cf, out ff);
            f = MyMath.RadiansToDegrees * ff;
            SimpleProcedures.ArcTanTwoArg(sv, cv, out vv);
            v = MyMath.RadiansToDegrees * vv;
            if (v < 0.0)
                v = 360.0 + v;
            h = hg * gg;
        }

        // { to obtain cartesian coordinates in metr
        //      of the station from the spherical ones }

        public static void GeoCartFromSpher(double f, double v, double h, out double x, out double y, out double z)
        {
            double r = 6378144.11;
            double a = 1.0 / 298.257;
            double e = 2 * a - a * a;
            double q = 1 - e;
            double ff = MyMath.DegreesToRadians * f;
            double vv = MyMath.DegreesToRadians * v;
            double sf = Math.Sin(ff);
            double cf = Math.Cos(ff);
            double gg = r / Math.Sqrt(1 - e * sf * sf);
            x = (gg + h) * cf * Math.Cos(vv);
            y = (gg + h) * cf * Math.Sin(vv);
            z = (gg * q + h) * sf;
        }

        // { to obtain DesCart
        //   or rectangular position
        //   when sheric coordinates in right system are known
        //   for example longitude latitude right ascension declination }
        public static void DescFromSpherCoor(double Aln, double Alt, double Ro, out dvec3 PosDescart)
        { // by usual formula is called in UnForPos UnPhysOp
            double a = MyMath.DegreesToRadians * Aln;  //{ longitude in radian  0 <= azimuth <= 360 }
            double b = MyMath.DegreesToRadians * Alt;  //{ latitude  in radian -90 <= altit <= +90 }
            double cb = Math.Cos(b);     //{ cosinus altitude }
            PosDescart = new dvec3
            {
                x = Ro * Math.Cos(a) * cb, //{ x coordinate }
                y = Ro * Math.Sin(a) * cb, //{ y coordinate }
                z = Ro * Math.Sin(b)       //{ z coordinate }
            };
        }

        // { to obtain DesCart
        //   or rectangular horizontal position
        //   when sheric coordinates in left system are known
        //   for example horizontal azimuth from the north altitude }
        public static void DescFromHorizCoor(double Az, double Alt, double Ro, ref dvec3 PosDescart)
        { // proc is called in UnForSys by FromTopToTrueEqu
            DescFromSpherCoor(Az, Alt, Ro, out PosDescart); //{ but left azimuth }
            PosDescart.y = -PosDescart.y; //{ from left to right angle }
        }
    }

    public static class SimpleProcedures
    {

        // { DATAN2 is a function
        //   to obtain Arctan for the Angle in interval
        //      from  -(1/2)* Pi  to  +(3/2)* Pi  in rad}

        public static double DATAN2(double SinAngle, double CosAngle)
        {
            double a;
            double c = CosAngle;
            double s = SinAngle;
            if (c != 0)
            {
                double p = s / c;
                a = Math.Atan(p);
                if (c < 0)
                    a = a + Math.PI;
            }
            else if (s > 0)
                a = 2.0 * Math.PI;
            else
                a = -2.0 * Math.PI;
            return a;
        }

        //{ ATanDegree is a function  to obtain
        //  Arctan for the Angle in interval from  0  to  360  in degree }

        public static double ATanDegree(double s, double c)
        {
            double a;
            if (c != 0.0)

            {
                double p = s / c;
                a = MyMath.RadiansToDegrees * Math.Atan(p);
                if( c< 0  )
                    a = a + 180;
            }
            else
       if (s > 0) a = 90;
            else a = 270;
            if( a< 0  )
                a = 360 + a;
            return a;
        }

        //{ ArcTanTwoArg is procedure
        //  to obtain Arctan for the Angle in interval
        //     from  -(1/2)* Pi  to  +(3/2)* Pi  in rad }

        public static void ArcTanTwoArg(double SinAngle, double CosAngle, out double Angle)
        {
            double a;
            double c = CosAngle; double s = SinAngle;
            if (c != 0)
            {
                double p = s / c; a = Math.Atan(p);
                if (c < 0) a = a + Math.PI;
            }
            else if (s > 0) a = 2.0 * Math.PI;
            else a = -2.0 * Math.PI;
            Angle = a;
        }

        public static double Frac(double value)
        {
            return value - Math.Truncate(value);
        }

        // { PrDegrAngle is quite simple procedure
        //   to change a value for  Angle  in radians
        //      to the civil form : Sign Degree Minute Second. }

        public static void PrDegrAngle(double Angle, out string AngleSign, out int IntDegree, out int IntMin, out double AngleSec)
        {
            if (Angle < 0)
                AngleSign = "-";
            else
                AngleSign = "+";
            double r = MyMath.RadiansToDegrees * Math.Abs(Angle);
            IntDegree = (int)Math.Truncate(r);
            r = 60 * Frac(r);
            IntMin = (int)Math.Truncate(r);
            AngleSec = 60 * Frac(r);
            if (AngleSec > 59.999)
            {
                AngleSec = 0;
                IntMin = IntMin + 1;
            }
            if (IntMin > 59)
            {
                IntMin = IntMin - 60;
                IntDegree = IntDegree + 1;
            }
        }

        // { PrHourAngle is quite simple procedure
        //   to change a value for  Angle  in radians
        //      to the civil form : Sign Hour Minute Second. }

        public static void PrHourAngle(double Angle, out string AngleSign, out int IntHour, out int IntMin, out double AngleSec)
        {
            if (Angle < 0)
                AngleSign = "-";
            else
                AngleSign = "+";
            double r = MyMath.RadiansToDegrees * Math.Abs(Angle) / 15;
            IntHour = (int)Math.Truncate(r);
            r = 60 * Frac(r);
            IntMin = (int)Math.Truncate(r);
            AngleSec = 60 * Frac(r);
            if (AngleSec > 59.999)
            {
                AngleSec = 0;
                IntMin = IntMin + 1;
            }
            if (IntMin > 59)
            {
                IntMin = IntMin - 60;
                IntHour = IntHour + 1;
            }
        }

        // { from the interesting book }

        public static double Log10(double x)
        {
            return Math.Log(x) / Math.Log(10);
        }

        public static double VectorModul(dvec3 a)
        {
            return Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static double ScalarMult(dvec3 a, dvec3 b)
        { // scalar multiplication of two vectors
            return (+a[1] * b[1] + a[2] * b[2] + a[3] * b[3]);
        }

        public static double ScalarMunt(dvec3 a, dvec3 b)
        { // for cos between two vectors unit scalar multiplication
            return ScalarMult(a, b) / (VectorModul(a) * VectorModul(b));
        }

        public static void MatrMultVector(dmat3 p, dvec3 a, ref dvec3 b)
        {
            for (int i = 0; i < 3; i++)
            {
                b.Values[i] = 0;
                for (int j = 0; j < 3; j++)
                    b[i] = b[i] + p[i, j] * a[j];
            }
        }

        public static void UMatrMultVect(dmat3 p, // to transform matrix
        dvec3 a, ref dvec3 b)
        {
            for (int i = 0; i < 3; i++)
            {
                b[i] = 0;
                for (int j = 0; j < 3; j++)
                    b[i] = b[i] + p[j, i] * a[j]; // to transform matrix
            }
        }

        // { angle a in degree, m = 1 , 2 , 3 for X Y Z axis }

        public static void SimpleRotMatr(byte m, double a, ref dmat3 r)
        {
            for (int i = 0; i < 3; i++ )
                for (int j = 0; j < 3; j++ )
                    if (i == j)
                        r.Values[i, j] = 1.0;
                    else
                        r.Values[i, j] = 0.0; // unit matrix

            double b = MyMath.DegreesToRadians * a;
            double s = Math.Sin(b);
            double c = Math.Cos(b);
            if (m == 1)
            {
                r.m00 = +c;
                r.m01 = +s;
                r.m10 = -s;
                r.m11 = +c;
                return;
            }
            if (m == 2)

            {
                r.m00 = +c;
                r.m02 = -s;
                r.m20 = +s;
                r.m22 = +c;
                return;
            }
            if (m == 3)
            {
                r.m00 = +c;
                r.m01 = +s;
                r.m10 = -s;
                r.m11 = +c;
                return;
            }
        }

        //{ alpha delta of the north pole of an object in degree }

        public static void RotMatrFromPole(double a, double d, out dmat3 r)
        {
            dmat3 p = new dmat3(), q = new dmat3();
            double z = 90 + a;
            SimpleRotMatr(3, z, ref p);
            double x = 90 - d;
            SimpleRotMatr(1, x, ref q);
            r = q * p; // MatrMultMatr(q, p, r);
            //{ Rx(90 - F) * Rz(90 + L) }
        }

        // { alpha delta of the north pole of an object in degree
        //    w angle of the first meridian orientation in degree
        //    matrix  R_z(w)* R_x(90-d)* R_z(90+a)
        //    matrix to change from the fixed equator to the body system }

        public static void RotoBodyMatr(double a, double d, double w, out dmat3 v)
        {
            dmat3 p = new dmat3(), q = new dmat3();
            double z = 90 + a;
           // { 90 degree plus right ascension }
            SimpleRotMatr(3, z, ref p);
          //  { around Z axis }
            double x = 90 - d;
          //  { 90 degree minus declinaton }
            SimpleRotMatr(1, x, ref q);
            // { around X axis }
            dmat3 r = q * p;//  MatrMultMatr(q, p, r);
          //  { Rx(90 - F) * Rz(90 + L) }
            SimpleRotMatr(3, w, ref p);
            //  { around Z axis by angle w R_z(w) }
            v = p * r; // MatrMultMatr(p, r, ref v);
           // { R_z(w) * R_x(90 - d) * R_z(90 + a) }
        }

        public static double ToGetDet(dmat3 a)
        {
            return +a[1, 1] * (a[2, 2] * a[3, 3] - a[3, 2] * a[2, 3])
                      - a[1, 2] * (a[2, 1] * a[3, 3] - a[3, 1] * a[2, 3])
                      + a[1, 3] * (a[2, 1] * a[3, 2] - a[3, 1] * a[2, 2]);
        }

        public static void ToFillMatrix(dvec3 a, dvec3 b, dvec3 c, ref dmat3 d)
        {
            for (int i = 0; i < 3; i++)
            { // main matrix 3*3
                d.Values[i, 1] = a[i];
                d.Values[i, 2] = b[i];
                d.Values[i, 3] = c[i];
            }
        }

        public static double SquareInterPol(double x1, double x0, double x2, double y1, double y0, double y2)
        { // to interpolate three points by square polinom
            double x, d, a, b; // a try to find point of maximum
            dmat3 ad = new dmat3(), aa = new dmat3(), ab = new dmat3();  // values a b c are unknown
                        // y ( 1 0 2 ) = a * x^2 + b * x + c ; x ( 1 0 2 )
            dvec3 yy = new dvec3
            {
                x = y1, // y1 = a * (x1-x0)^2 + b * (x1-x0) + c
                y = y0, // y0 = a * (x0-x0)^2 + b * (x0-x0) + c  only c  x0-x0=0
                z = y2  // y2 = a * (x2-x0)^2 + b * (x2-x0) + c
            };
            dvec3 xc = dvec3.Ones;  // coefficient with c equal 1
            dvec3 xb = new dvec3
            {
                x = x1 - x0, // coefficients with b (x1-x0)
                y = 0.0,   // the first derivation equal nullo
                z = x2 - x0  // for the maximum of square polinom if it exists
            };
            dvec3 xa = new dvec3
            {
                x = xb.x * xb.x,
                y = xb.y * xb.y,
                z = xb.z * xb.z
        };
            ToFillMatrix(xa, xb, xc, ref ad); // to solve three linear equation
            ToFillMatrix(yy, xb, xc, ref aa); // with respect to a b c
            ToFillMatrix(xa, yy, xc, ref ab); // the square polinom   a*(x-x0)^2+b*(x-x0)+c
            d = ToGetDet(ad);   // determinant of the main 3*3 matrix
            if ( Math.Abs(d) < 1.0e-6  )
                d = 1.0e16;
            a = ToGetDet(aa) / d; // a value
            b = ToGetDet(ab) / d; // b value
            x = x0 - b / (2 * a); // to solve equation 2*a*(x-x0)+b=0
            if (x < x1)  x = x1; // out of range
            if (x > x2)  x = x2; // out of range
            return x;
        }

    }

}
