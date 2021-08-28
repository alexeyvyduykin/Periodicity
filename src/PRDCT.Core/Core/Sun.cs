using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace PRDCT.Core
{



    //public static class DarinKoblick
    //{
    //    public static double JD2GMST(double jd)
    //    {

    //        // Convert a specified Julian Date Vector to Greenwhich Mean Sidereal Time.
    //        //
    //        // Inputs:
    //        // JD[N x M x L]                         Julian Date Vector

    //        // Outputs:
    //        // GMST[N x M x L]                         Greenwich Mean Sidereal
    //        // Time in degrees from
    //        // 0 - 360

    //        // Find the Julian Date of the previous midnight, JD0

    //        double JDmin = Math.Floor(jd) - 0.5;
    //        double JDmax = Math.Floor(jd) + 0.5;

    //        double JD0 = (jd > JDmax) ? JDmax : JDmin;

    //        double H = (jd - JD0) * 24;       // Time in hours past previous midnight
    //        double D = jd - 2451545.0;     // Compute the number of days since J2000
    //        double D0 = JD0 - 2451545.0;   // Compute the number of days since J2000
    //        double T = D / 36525;           // Compute the number of centuries since J2000
    //                                        // Calculate GMST in hours(0h to 24h)...then convert to degrees
    //        double GMST = (6.697374558 + 0.06570982441908 * D0 + 1.00273790935 * H + 0.000026 * (T * T) % 24) * 15;

    //        return GMST;

    //    }

    //    public static double JD2GAST(double jd)
    //    {
    //        // Convert a specified Julian Date Vector to Greenwhich Apparent Sidereal Time.
    //        //
    //        // Inputs:
    //        // JD[N x M x L] Julian Date Vector

    //        // Outputs:
    //        // GAST[N x M x L] Greenwich Apparent Sidereal
    //        // Time in degrees from
    //        // 0 - 360
    //        //
    //        // References:
    //        // Approximate Sidereal Time,
    //        // http://www.usno.navy.mil/USNO/astronomical-applications/...
    //        // astronomical - information - center / approx - sider - time
    //        //
    //        // Universal Sidereal Times, The Astronomical Almanac For The Year 2004
    //        //
    //        // THETAm is the mean siderial time in degrees
    //        double THETAm = JD2GMST(jd);

    //        // Compute the number of centuries since J2000
    //        double T = (jd - 2451545.0) / 36525;

    //        // Mean obliquity of the ecliptic(EPSILONm)
    //        // see http://www.cdeagle.com/ccnum/pdf/demogast.pdf equation 3
    //        // also see Vallado, Fundamentals of Astrodynamics and Applications, second edition.
    //        // pg. 214 EQ 3 - 53
    //        double EPSILONm = 23.439291 - 0.0130111 * T - 1.64E-07 * (T * T) + 5.04E-07 * (T * T * T);

    //        // Nutations in obliquity and longitude(degrees)
    //        // see http://www.cdeagle.com/ccnum/pdf/demogast.pdf equation 4
    //        double L = 280.4665 + 36000.7698 * T;
    //        double dL = 218.3165 + 481267.8813 * T;
    //        double OMEGA = 125.04452 - 1934.136261 * T;

    //        // Calculate nutations using the following two equations:
    //        // see http://www.cdeagle.com/ccnum/pdf/demogast.pdf equation 5

    //        double dPSI = -17.20 * Math.Sin(OMEGA * MyMath.DegreesToRadians) - 1.32 * Math.Sin(2.0 * L * MyMath.DegreesToRadians) - 0.23 * Math.Sin(2.0 * dL * MyMath.DegreesToRadians) + 0.21 * Math.Sin(2.0 * OMEGA * MyMath.DegreesToRadians);
    //        double dEPSILON = 9.20 * Math.Cos(OMEGA * MyMath.DegreesToRadians) + 0.57 * Math.Cos(2.0 * L * MyMath.DegreesToRadians) + 0.10 * Math.Cos(2.0 * dL * MyMath.DegreesToRadians) - 0.09 * Math.Cos(2.0 * OMEGA * MyMath.DegreesToRadians);

    //        // Convert the units from arc-seconds to degrees

    //        dPSI = dPSI * (1.0 / 3600.0);

    //        dEPSILON = dEPSILON * (1.0 / 3600.0);

    //        // (GAST)Greenwhich apparent sidereal time expression in degrees
    //        // see http://www.cdeagle.com/ccnum/pdf/demogast.pdf equation 1
    //        double GAST = (THETAm + dPSI * Math.Cos((EPSILONm + dEPSILON) * MyMath.DegreesToRadians)) % 360;
    //        return GAST;
    //    }
    //}

    //public static class FFF
    //{
    //    // Compute sidereal time at Greenwich (according to: Jean Meeus: Astronomical Algorithms) 
    //    public static double SiderealTime(double JD)
    //    {
    //        double T = (JD - 2451545.0) / 36525.0;
    //        double theta0 = 280.46061837 + 360.98564736629 * (JD - 2451545.0) + 0.000387933 * T * T - T * T * T / 38710000.0; // degrees 
    //        theta0 = MyMath.WrapAngle360(theta0);
    //        return theta0;
    //    }

    //    public static double GMST(double jd)
    //    {
    //        double d = jd - 2451545.0;
    //        double T = d / 36525.0;
    //        double Tsquad = T * T;
    //        double Tcube = Tsquad * T;

    //        double GMST = 24110.54841 + 8640184.812866 * T + 0.093104 * Tsquad - 0.0000062 * Tcube;

    //        return GMST;

    //    }

    //    public static double ASTROPUZ_GMST(double TinUTC)
    //    {
    //        // ClcGrMSTime is a function to obtain Greenwich Mean Sidereal Time in radians.      
    //        // IAU 1976  for the Greenwich Mean Sidereal Time. TinUTC in UTC and close to UT1

    //        double t, aj, ajj, ajs, s, s0, r, freq;
    //        double SidTime;
    //        int IntPart;

    //        t = TinUTC;
    //        aj = Math.Truncate(t) + 0.5;
    //        s = t - aj;
    //        ajj = aj - 2451545.0;
    //        ajs = ajj / 36525.0;
    //        s0 = 1.753368559233266e0 + (628.3319706888409e0
    //         + (6.770713944903336e-06 - 4.508767234318685e-10 * ajs) * ajs) * ajs;
    //        freq = 1.002737909350795e0
    //        + (5.900575455674703e-11 - 5.893984333409384e-15 * ajs) * ajs;
    //        s0 = s0 + freq * s * 2 * Math.PI;
    //        r = s0 / (2 * Math.PI);
    //        IntPart = (int)Math.Truncate(r);
    //        SidTime = s0 - 2 * Math.PI * IntPart;
    //        if (SidTime < 0)
    //            SidTime = SidTime + 2 * Math.PI;
    //        return SidTime;
    //    }

        



    //    // Julian day: 86400 s, Julian year: 365.25 d, Julian Century: 36525 d
    //    // UT - sec.
    //    public static double JulianDay(int date, int month, int year, double UT)
    //    {
    //        if (month <= 2)
    //        {
    //            month = month + 12;
    //            year = year - 1;
    //        }

    //        return (int)(365.25 * year) + (int)(30.6001 * (month + 1)) - 15 + 1720996.5 + date + UT / 86400.0;
    //    }
    //}


    public class Sun
    {
        public Sun() { }


        public dvec4 Position(DateTime date, double secs = 0.0)
        {
            double jd = date.AddSeconds(secs).ToOADate() + 2415018.5;
            return Position(jd);
        }

        public static dvec4 Position(double jd)
        {
            double d = jd - 2415020.0;
            double t = d / 36525.0;
            double e = 0.1675104e-1 - (0.418e-4 + 0.126e-6 * t) * t;
            double r = 6.25658378411 + 1.72019697677e-2 * d - 2.61799387799e-6 * t * t;
            double v = 4.90822940869 + 8.21498553644e-7 * d + 7.90634151156e-6 * t * t;
            r = r + 2.0 * e * Math.Sin(r) + 1.25 * e * e * Math.Sin(2.0 * r);
            double h = 4.09319747446e-1 - (2.27110968916e-4 + (2.86233997327e-8 -
              8.77900613756e-9 * t) * t) * t + 4.46513400244e-5 * Math.Cos(4.52360151485 -
              (3.37571462465e+1 - (3.62640633471e-5 + 3.87850944887e-8 * t) * t) * t);
            double rSun = 149600034.408 * (1.0 - e * e) / (1.0 + e * Math.Cos(r));
            v = v + r;
            r = Math.Sin(v);

            return new dvec4() {
                x = rSun * Math.Cos(v),
                y = rSun * r * Math.Cos(h),
                z = rSun * r * Math.Sin(h),
                w = rSun
            };
        }

        protected bool IsLighting(double jd, dvec4 satPosition)
        {
            //double xSunNorm = Position(jd).x / Position(jd).w;
            //double ySunNorm = Position(jd).y / Position(jd).w;
            //double zSunNorm = Position(jd).z / Position(jd).w;

            dvec4 sunPosition = Position(jd).Normalized;



            double rSat = satPosition.Length;// Math.Sqrt(satPosition.x * satPosition.x + satPosition.y * satPosition.y + satPosition.z * satPosition.z);
            double rSun = sunPosition.Length;// Math.Sqrt(xSunNorm * xSunNorm + ySunNorm * ySunNorm + zSunNorm * zSunNorm);
            //double r_S = satPosition.x * sunPosition.x + satPosition.y * sunPosition.y + satPosition.z * sunPosition.z;

            double r_S = dvec4.Dot(satPosition, sunPosition);

            double angle = Math.Acos(r_S / (rSat * rSun));

            r_S = Math.Abs(r_S);

            if (angle > Math.PI / 2.0)
                r_S = -r_S;

            double G = r_S + Math.Sqrt(rSat * rSat - Globals.Re * Globals.Re);

            if (G < 0.0)
                return false;
            return true;
        }
        
        public Geo2D SubSunPosition(DateTime date)
        {
            var JDE = Meeus.julian(date.DayOfYear + date.TimeOfDay.TotalDays, date.Year);
            //var JD = FFF.JulianDay(date.Day, date.Month, date.Year, 0.0);
            //  var JDE = FFF.JulianDay(date.Day, date.Month, date.Year, date.TimeOfDay.TotalSeconds);

            //    var S0_rad = FFF.SiderealTime(JD) * MyMath.DegreesToRadians;
            //     var S0_deg = FFF.SiderealTime(JD);

            //    var S_deg = FFF.SiderealTime(JDE);
            //    var S_rad = FFF.SiderealTime(JDE) * MyMath.DegreesToRadians;  // true
            //    double S_deg_ = MyFunction.MGST(date);
            //    double S_rad_ = MyFunction.MGST(date) * MyMath.DegreesToRadians;  // true

            var S_rad = Meeus.gast2(JDE, 0.0, 0) * 2.0 * Math.PI / 24.0;

            // double S = S0 + MyConst.w3 * date.TimeOfDay.TotalSeconds;
            // S = MyMath.WrapAngle(S);

            dvec4 rs = Position(JDE);
            Geo2D sunPosition = MyConversion.CartesianToSpherical(new dvec3(rs));

            return new Geo2D(MyMath.WrapAngle(sunPosition.Lon - S_rad), sunPosition.Lat);
        }
    
        public Geo2D SubSunPosition1(DateTime date)
        {
            //1.Вычисление модифицированной  юлианской даты на начало суток

            // MJD - Модифицированная Юлианская дата
            double MJD = MyFunction.DateToMJD((int)date.Year, (int)date.Month, (int)date.Day);

            // var date = new DateTime((int)Year, (int)Mon, (int)Day, (int)hous, (int)min, (int)sec);
            // var JD = MyFunction.DateToJD((int)Year, (int)Mon, (int)Day);

            // Вычисление Гринвеческого звездного времени

            double T0 = (MJD - 51544.5) / 36525; // мод.юл.дата на начало суток в юлианских столетиях
            double a1 = 24110.54841;
            double a2 = 8640184.812;
            double a3 = 0.093104;
            double a4 = 0.0000062;
            double S0 = a1 + a2 * T0 + a3 * T0 * T0 - a4 * T0 * T0 * T0;// звездное время в Гринвиче на начало суток в секундах
                                                                        //UT - всемирное время в часах, момент расчета
            double UT = date.Hour + date.Minute / 60 + date.Second / 3600;
            if (UT > 24) UT = UT - 24;
            if (UT < 0) UT = UT + 24;
            double Nsec = UT * 3600; // количество секунд, прошедших  от начала суток до момента наблюдения

            double NsecS = Nsec * 366.2422 / 365.2422; //количество  звездных секунд
            double GMT = (S0 + NsecS) / 3600 * 15;//гринвическое среднее звездное время в градусах SG
            while (GMT > 360) GMT = GMT - 360;
            double GST = GMT;// местное звездное время ST
                             //Lon – долгота наблюдателя

           // double S__ = MyFunction.uds1900(new DateTime(2007, 7, 1, 12, 0, 0).ToOADate() + 2415018.5) * MyMath.RadiansToDegrees;

            //  Вычисление эклиптических координат Солнца

            T0 = (MJD - 51544.5) / 36525; // мод.юл.дата на начало суток в юлианских столетиях
            double M = 357.528 + 35999.05 * T0 + 0.04107 * UT;// средняя аномалия
            while (M > 360) M = M - 360;
            double L0 = 280.46 + 36000.772 * T0 + 0.04107 * UT;
            double L = L0 + (1.915 - 0.0048 * T0) * Math.Sin(M * MyMath.DegreesToRadians) + 0.02 * Math.Sin(2 * M * MyMath.DegreesToRadians);//долгота Солнца
            while (L > 360) L = L - 360;

            double X = Math.Cos(L * MyMath.DegreesToRadians); // вектор
            double Y = Math.Sin(L * MyMath.DegreesToRadians); //  в эклиптической
            double Z = 0; //  системе координат



            // Координаты Cолнца в прямоугольной экваториальной системе координат

            double Eps = 23.439281; //наклон эклиптики к экватору
            double X_ = X;                          // вектор
            double Y_ = Y * Math.Cos(Eps * MyMath.DegreesToRadians) - Z * Math.Sin(Eps * MyMath.DegreesToRadians); //   в экваториальной
            double Z_ = Y * Math.Sin(Eps * MyMath.DegreesToRadians) + Z * Math.Cos(Eps * MyMath.DegreesToRadians);//    системе координат


            // Экваториальные геоцентрические координаты Солнца
            // RA - прямое восхождение Солнца на нужный момент времени
            //DEC - склонение Солнца на нужный момент времени

            double Ra = Math.Atan2(Y_, X_) * MyMath.RadiansToDegrees;
            double Dec = Math.Atan2(Z_, Math.Sqrt(X_ * X_ + Y_ * Y_)) * MyMath.RadiansToDegrees;

            // получаем подсолнечную точку
            // Долгота Солнца
            double LonSan = Ra - GST;
            // Широта Солнца
            double LatSan = Dec;

            return new Geo2D(LonSan, LatSan, GeoCoordTypes.Degrees);
        }

        public List<Geo2D> Sunlight(DateTime date, double FHSunMinDEG = 0.0)
        {
            double FHSunMinRAD = FHSunMinDEG * MyMath.DegreesToRadians;
           
            double JD = date.Date.ToOADate() + 2415018.5;
            double jd = date.ToOADate() + 2415018.5;
            //   double S0 = MyFunction.uds1900(JD);
            //   double S = MyFunction.uds1900(jd);

            //  var cgfgddf = MyConst.w3 * date.TimeOfDay.TotalSeconds;

            //  var JDE = MyFunction.ToJDE(JD, date.Hour * 3600.0 + date.Minute * 60.0 + date.Second);
            //  var JC = MyFunction.ToJC(JD);
            //  var JCE = MyFunction.ToJCE(JDE);
            //  var JME = MyFunction.ToJME(JCE);

            //  var meanJD = MyFunction.DateToMJD(date.Year, date.Month, date.Day);

            //S = S0 + Globals.Omega * date.TimeOfDay.TotalSeconds;
            Julian jdate = new Julian(date);

            double S = jdate.ToGmst();

            dvec4 rs = Position(jd);
            Geo2D sunPosition = MyConversion.CartesianToSpherical(new dvec3(rs));// new Geo2D(Math.Atan2(rs.y, rs.x), Math.Asin(rs.z / rs.w));

            double SunLat = sunPosition.Lat;// Math.Asin(rs.z / rs.w);
            double SunLon = sunPosition.Lon - S;// Math.Atan2(rs.y, rs.x);

            double ro = Math.Acos(Globals.Re * Math.Cos(FHSunMinRAD) / rs.w) - FHSunMinRAD;
            double xs = Globals.Re * Math.Cos(ro);
            double zs = Globals.Re * Math.Sin(ro);

            var sunlight = new List<Geo2D>();

            int step = 1;
            for (int i = 0; i <= 360; i += step)
            {
                double g = i * MyMath.DegreesToRadians;
                double xg = xs * Math.Cos(SunLat) * Math.Cos(-SunLon) + zs * (-Math.Sin(SunLat) * Math.Cos(g) * Math.Cos(-SunLon) + Math.Sin(g) * Math.Sin(-SunLon));
                double yg = xs * (-Math.Sin(-SunLon) * Math.Cos(SunLat)) + zs * (Math.Sin(-SunLon) * Math.Sin(SunLat) * Math.Cos(g) + Math.Sin(g) * Math.Cos(-SunLon));
                double zg = xs * Math.Sin(SunLat) + zs * Math.Cos(SunLat) * Math.Cos(g);
                double TermLon = Math.Atan2(yg / Globals.Re, xg / Globals.Re);
                double TermLat = Math.Asin(zg / Globals.Re);
                TermLon = MyMath.WrapAngle(TermLon);
                sunlight.Add( new Geo2D(TermLon, TermLat) );
            }

            return sunlight;
        }

        public Geo2D SunPos(
                    double Year,//год
                    double Mon,// месяцы
                    double Day,// дни
                    double hous,//часы
                    double min,//минуты
                    double sec,// секунды
                    double zona,// Часовой пояс
                  ref double RA_,
                  ref double DEC_
            )

        {
            //1.Вычисление модифицированной  юлианской даты на начало суток

            // MJD - Модифицированная Юлианская дата
            double MJD = MyFunction.DateToMJD((int)Year, (int)Mon, (int)Day);

           // var date = new DateTime((int)Year, (int)Mon, (int)Day, (int)hous, (int)min, (int)sec);
           // var JD = MyFunction.DateToJD((int)Year, (int)Mon, (int)Day);
 
            // Вычисление Гринвеческого звездного времени

            double T0 = (MJD - 51544.5) / 36525; // мод.юл.дата на начало суток в юлианских столетиях
            double a1 = 24110.54841;
            double a2 = 8640184.812;
            double a3 = 0.093104;
            double a4 = 0.0000062;
            double S0 = a1 + a2 * T0 + a3 * T0 * T0 - a4 * T0 * T0 * T0;// звездное время в Гринвиче на начало суток в секундах
                                                                        //UT - всемирное время в часах, момент расчета
            double UT = hous - zona + min / 60 + sec / 3600;
            if (UT > 24) UT = UT - 24;
            if (UT < 0) UT = UT + 24;
            double Nsec = UT * 3600; // количество секунд, прошедших  от начала суток до момента наблюдения

            double NsecS = Nsec * 366.2422 / 365.2422; //количество  звездных секунд
            double GMT = (S0 + NsecS) / 3600 * 15;//гринвическое среднее звездное время в градусах SG
            while (GMT > 360) GMT = GMT - 360;
            double GST = GMT;// местное звездное время ST
                                   //Lon – долгота наблюдателя


            //    var dfdfd = MyFunction.uds(fdfddf);


       //     double S__ = MyFunction.uds1900(new DateTime(2007, 7, 1, 12, 0, 0).ToOADate() + 2415018.5) * MyMath.RadiansToDegrees;

            //  Вычисление эклиптических координат Солнца

            T0 = (MJD - 51544.5) / 36525; // мод.юл.дата на начало суток в юлианских столетиях
            double M = 357.528 + 35999.05 * T0 + 0.04107 * UT;// средняя аномалия
            while (M > 360) M = M - 360;
            double L0 = 280.46 + 36000.772 * T0 + 0.04107 * UT;
            double L = L0 + (1.915 - 0.0048 * T0) * Math.Sin(M * MyMath.DegreesToRadians) + 0.02 * Math.Sin(2 * M * MyMath.DegreesToRadians);//долгота Солнца
            while (L > 360) L = L - 360;

            double X = Math.Cos(L * MyMath.DegreesToRadians); // вектор
            double Y = Math.Sin(L * MyMath.DegreesToRadians); //  в эклиптической
            double Z = 0; //  системе координат



            // Координаты Cолнца в прямоугольной экваториальной системе координат

            double Eps = 23.439281; //наклон эклиптики к экватору
            double X_ = X;                          // вектор
            double Y_ = Y * Math.Cos(Eps * MyMath.DegreesToRadians) - Z * Math.Sin(Eps * MyMath.DegreesToRadians); //   в экваториальной
            double Z_ = Y * Math.Sin(Eps * MyMath.DegreesToRadians) + Z * Math.Cos(Eps * MyMath.DegreesToRadians);//    системе координат


            // Экваториальные геоцентрические координаты Солнца
            // RA - прямое восхождение Солнца на нужный момент времени
            //DEC - склонение Солнца на нужный момент времени

            double Ra = Math.Atan2(Y_, X_) * MyMath.RadiansToDegrees;
            double Dec = Math.Atan2(Z_, Math.Sqrt(X_ * X_ + Y_* Y_)) * MyMath.RadiansToDegrees;

            RA_ = Ra;
            DEC_ = Dec;

            // получаем подсолнечную точку
            // Долгота Солнца
            double LonSan = Ra - GST;
            // Широта Солнца
            double LatSan = Dec;

            return new Geo2D(LonSan, LatSan, GeoCoordTypes.Degrees);
        }

    }

//    public class SolarElements
//    {
//        private double julianUnixEpoch = 2440587.5; // julian days to start of unix epoch

//        DateTime JulianToDateTime(double julianDate)
//        {
//            double unixTime = (julianDate - 2440587.5) * 86400;

//            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
//            dtDateTime = dtDateTime.AddSeconds(unixTime);//.ToLocalTime();

//            return dtDateTime;
//        }

//        DateTime Create(double seconds)
//        {
//            var datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

//            return datetime.AddSeconds(seconds);
//        }

//        // Main function to calculate solar values. Requires a time value (seconds since
//        // 1970-1-1) as input. 
//        //public void calcSolar(/*atime_t*/ DateTime t, ref SolarElements SE)
//        //{
            
//        //    // Calculate the time past midnight, as a fractional day value
//        //    // e.g. if it's noon, the result should be 0.5.

//        //    SE.timeFracDay = ((((double)(t.Second / 60) + t.Minute) / 60) + t.Hour) / 24;

//        //    // unixDays is the number of whole days since the start

//        //    // of the Unix epoch. The division sign will truncate any remainder

//        //    // since this will be done as integer division.

//        //    SE.unixDays = (long)(t.ToOADate() - julianUnixEpoch + 2415018.5);// (long)((t.ToOADate()) /*t*/ / 86400);

//        //    // calculate Julian Day Number

//        //    SE.JDN = julianUnixEpoch + SE.unixDays;

//        //    // Add the fractional day value to the Julian Day number. If the

//        //    // input value was in the GMT time zone, we could proceed directly

//        //    // with this value. 

//        //    SE.JDN = SE.JDN + SE.timeFracDay;

//        //    // Adjust JDN to GMT time zone

//        //    SE.JDN = SE.JDN - ((double)SE.tzOffset / 24);

//        //    // Calculate Julian Century Number

//        //    SE.JCN = (SE.JDN - 2451545) / 36525;

//        //    // Geometric Mean Longitude of Sun (degrees)

//        //    SE.GMLS = (280.46646 + SE.JCN * (36000.76983 + SE.JCN * 0.0003032));

//        //    // Finish GMLS calculation by calculating modolu(GMLS,360) as

//        //    // it's done in R or Excel. C's fmod doesn't work in the same

//        //    // way. The floor() function is from the math.h library.

//        //    SE.GMLS = SE.GMLS - (360 * (Math.Floor(SE.GMLS / 360)));

//        //    // Geometric Mean Anomaly of Sun (degrees)

//        //    SE.GMAS = 357.52911 + (SE.JCN * (35999.05029 - 0.0001537 * SE.JCN));



//        //    // Eccentricity of Earth Orbit

//        //    SE.EEO = 0.016708634 - (SE.JCN * (0.000042037 + 0.0000001267 * SE.JCN));

//        //    // Sun Equation of Center

//        //    SE.SEC = Math.Sin(SE.GMAS * MyMath.DegreesToRadians) * (1.914602 -

//        //                                    (SE.JCN * (0.004817 + 0.000014 * SE.JCN))) +

//        //    Math.Sin((2 * SE.GMAS) * MyMath.DegreesToRadians) * (0.019993 - 0.000101 * SE.JCN) +

//        //    Math.Sin((3 * SE.GMAS) * MyMath.DegreesToRadians) * 0.000289;

//        //    // Sun True Longitude (degrees)

//        //    SE.STL = SE.GMLS + SE.SEC;

//        //    // Sun True Anomaly (degrees)

//        //    SE.STA = SE.GMAS + SE.SEC;

//        //    // Sun Radian Vector (Astronomical Units)

//        //    SE.SRV = (1.000001018 * (1 - SE.EEO * SE.EEO)) / (1 + SE.EEO *

//        //                                          Math.Cos(SE.STA * MyMath.DegreesToRadians));

//        //    // Sun Apparent Longitude (degrees)

//        //    SE.SAL = SE.STL - 0.00569 - (0.00478 *

//        //                           Math.Sin((125.04 - 1934.136 * SE.JCN) * MyMath.DegreesToRadians));

//        //    // Mean Oblique Ecliptic (degrees)

//        //    SE.MOE = 23 + (26 + (21.448 - SE.JCN * (46.815 + SE.JCN *

//        //                                    (0.00059 - SE.JCN * 0.001813))) / 60) / 60;

//        //    // Oblique correction (degrees)

//        //    SE.OC = SE.MOE + 0.00256 * Math.Cos((125.04 - 1934.136 * SE.JCN) * MyMath.DegreesToRadians);

//        //    // Sun Right Ascension (degrees)

//        //    SE.SRA = (Math.Atan2(Math.Cos(SE.OC * MyMath.DegreesToRadians) * Math.Sin(SE.SAL * MyMath.DegreesToRadians),

//        //                 Math.Cos(SE.SAL * MyMath.DegreesToRadians))) * MyMath.RadiansToDegrees;

//        //    // Sun Declination (degrees)

//        //    SE.SDec = (Math.Asin(Math.Sin(SE.OC * MyMath.DegreesToRadians) *

//        //                 Math.Sin(SE.SAL * MyMath.DegreesToRadians))) * MyMath.RadiansToDegrees;

//        //    // var y

//        //    SE.vy = Math.Tan((SE.OC / 2) * MyMath.DegreesToRadians) * Math.Tan((SE.OC / 2) * MyMath.DegreesToRadians);



//        //    // Equation of Time (minutes)

//        //    SE.EOT = 4 * ((SE.vy * Math.Sin(2 * (SE.GMLS * MyMath.DegreesToRadians)) -

//        //                2 * SE.EEO * Math.Sin(SE.GMAS * MyMath.DegreesToRadians) +

//        //                4 * SE.EEO * SE.vy * Math.Sin(SE.GMAS * MyMath.DegreesToRadians) *

//        //                Math.Cos(2 * (SE.GMLS * MyMath.DegreesToRadians)) -

//        //                0.5 * SE.vy * SE.vy * Math.Sin(4 * (SE.GMLS * MyMath.DegreesToRadians)) -

//        //                1.25 * SE.EEO * SE.EEO * Math.Sin(2 * (SE.GMAS * MyMath.DegreesToRadians))) *

//        //                MyMath.RadiansToDegrees);

//        //    // Hour Angle Sunrise (degrees)

//        //    SE.HAS = Math.Acos((Math.Cos(90.833 * MyMath.DegreesToRadians) /

//        //                (Math.Cos(SE.lat * MyMath.DegreesToRadians) * Math.Cos(SE.SDec * MyMath.DegreesToRadians))) -

//        //               Math.Tan(SE.lat * MyMath.DegreesToRadians) * Math.Tan(SE.SDec * MyMath.DegreesToRadians)) *

//        //               MyMath.RadiansToDegrees;

//        //    // Solar Noon - result is given as fraction of a day

//        //    // Time value is in GMT time zone

//        //    SE.SolarNoonfrac = (720 - 4 * SE.lon - SE.EOT) / 1440;

//        //    // SolarNoon is given as a fraction of a day. Add this

//        //    // to the unixDays value, which currently holds the

//        //    // whole days since 1970-1-1 00:00

//        //    SE.SolarNoonDays = SE.unixDays + SE.SolarNoonfrac;

//        //    // SolarNoonDays is in GMT time zone, correct it to

//        //    // the input time zone

//        //    SE.SolarNoonDays = SE.SolarNoonDays + ((double)SE.tzOffset / 24);

//        //    // Then convert SolarNoonDays to seconds

//        //    SE.SolarNoonTime = Create(SE.SolarNoonDays * 86400);// SE.SolarNoonDays * 86400;

//        //    // Sunrise Time, given as fraction of a day

//        //    SE.Sunrise = SE.SolarNoonfrac - SE.HAS * 4 / 1440;

//        //    // Convert Sunrise to days since 1970-1-1

//        //    SE.Sunrise = SE.unixDays + SE.Sunrise;

//        //    // Correct Sunrise to local time zone from GMT

//        //    SE.Sunrise = SE.Sunrise + ((double)SE.tzOffset / 24);

//        //    // Convert Sunrise to seconds since 1970-1-1

//        //    SE.Sunrise = SE.Sunrise * 86400;

//        //    // Convert Sunrise to a time_t object (Time library)

//        //    SE.SunriseTime = Create(SE.Sunrise);// (DateTime)/* (atime_t)*/SE.Sunrise;

//        //    // Sunset Time

//        //    SE.Sunset = SE.SolarNoonfrac + SE.HAS * 4 / 1440;

//        //    // Convert Sunset to days since 1970-1-1

//        //    SE.Sunset = SE.unixDays + SE.Sunset;

//        //    // Correct Sunset to local time zone from GMT

//        //    SE.Sunset = SE.Sunset + ((double)SE.tzOffset / 24);

//        //    // Convert Sunset to seconds since 1970-1-1

//        //    SE.Sunset = SE.Sunset * 86400;

//        //    // Convert Sunset to a time_t object (Time library)

//        //    SE.SunsetTime = Create(SE.Sunset);// (DateTime)/* (atime_t)*/SE.Sunset;

//        //    // Sunlight Duration (day length, minutes)

//        //    SE.SunDuration = 8 * SE.HAS;

//        //    // True Solar Time (minutes)

//        //    SE.TST = (SE.timeFracDay * 1440 +

//        //           SE.EOT + 4 * SE.lon - 60 * SE.tzOffset);

//        //    // Finish TST calculation by calculating modolu(TST,360) as

//        //    // it's done in R or Excel. C's fmod doesn't work in the same

//        //    // way. The floor() function is from the math.h library.

//        //    SE.TST = SE.TST - (1440 * (Math.Floor(SE.TST / 1440)));

//        //    // Hour Angle (degrees)

//        //    if (SE.TST / 4 < 0)
//        //    {

//        //        SE.HA = SE.TST / 4 + 180;

//        //    }
//        //    else if (SE.TST / 4 >= 0)
//        //    {

//        //        SE.HA = SE.TST / 4 - 180;

//        //    }

//        //    // Solar Zenith Angle (degrees)

//        //    SE.SZA = (Math.Acos(Math.Sin(SE.lat * MyMath.DegreesToRadians) *

//        //                Math.Sin(SE.SDec * MyMath.DegreesToRadians) +

//        //                Math.Cos(SE.lat * MyMath.DegreesToRadians) *

//        //                Math.Cos(SE.SDec * MyMath.DegreesToRadians) *

//        //                Math.Cos(SE.HA * MyMath.DegreesToRadians))) * MyMath.RadiansToDegrees;

//        //    // Solar Elevation Angle (degrees above horizontal)

//        //    SE.SEA = 90 - SE.SZA;

//        //    // Approximate Atmospheric Refraction (degrees)

//        //    if (SE.SEA > 85)
//        //    {

//        //        SE.AAR = 0;

//        //    }
//        //    else if (SE.SEA > 5)
//        //    {

//        //        SE.AAR = (58.1 / Math.Tan(SE.SEA * MyMath.DegreesToRadians)) -

//        //        0.07 / (Math.Pow(Math.Tan(SE.SEA * MyMath.DegreesToRadians), 3)) +

//        //        0.000086 / (Math.Pow(Math.Tan(SE.SEA * MyMath.DegreesToRadians), 5));

//        //    }
//        //    else if (SE.SEA > -0.575)
//        //    {

//        //        SE.AAR = 1735 + SE.SEA * (-581.2 * SE.SEA *

//        //                            (103.4 + SE.SEA * (-12.79 + SE.SEA * 0.711)));

//        //    }
//        //    else
//        //    {

//        //        SE.AAR = -20.772 / Math.Tan(SE.SEA * MyMath.DegreesToRadians);

//        //    }

//        //    SE.AAR = SE.AAR / 3600.0;

//        //    // Solar Elevation Corrected for Atmospheric

//        //    // refraction (degrees)

//        //    SE.SEC_Corr = SE.SEA + SE.AAR;

//        //    // Solar Azimuth Angle (degrees clockwise from North)

//        //    if (SE.HA > 0)
//        //    {

//        //        SE.SAA = (((Math.Acos((Math.Sin(SE.lat * MyMath.DegreesToRadians) *

//        //                       Math.Cos(SE.SZA * MyMath.DegreesToRadians) -

//        //                       Math.Sin(SE.SDec * MyMath.DegreesToRadians)) /

//        //                      (Math.Cos(SE.lat * MyMath.DegreesToRadians) *

//        //                       Math.Sin(SE.SZA * MyMath.DegreesToRadians)))) *

//        //                MyMath.RadiansToDegrees) + 180);

//        //        SE.SAA = SE.SAA - (360 * (Math.Floor(SE.SAA / 360)));

//        //    }
//        //    else
//        //    {

//        //        SE.SAA = (540 - (Math.Acos((((Math.Sin(SE.lat * MyMath.DegreesToRadians) *

//        //                              Math.Cos(SE.SZA * MyMath.DegreesToRadians))) -

//        //                            Math.Sin(SE.SDec * MyMath.DegreesToRadians)) /

//        //                           (Math.Cos(SE.lat * MyMath.DegreesToRadians) *

//        //                            Math.Sin(SE.SZA * MyMath.DegreesToRadians)))) *

//        //               MyMath.RadiansToDegrees);

//        //        SE.SAA = SE.SAA - (360 * (Math.Floor(SE.SAA / 360)));

//        //    }

//        //}

//        public static Geo2D calcSolar1(DateTime t)
//        {
//            SolarElements SE = new SolarElements();

//            double JD = t.ToOADate() + 2415018.5;
//            JD = JD + ((((double)(t.Second / 60) + t.Minute) / 60) + t.Hour) / 24;
//            double T = (JD - 2451545) / 36525;

//            double GMLS = 280.46646 + T * (36000.76983 + T * 0.0003032);

//            GMLS = GMLS - (360 * (Math.Floor(GMLS / 360)));

//            double GMAS = 357.52911 + (T * (35999.05029 - 0.0001537 * T));


//            double SEC = Math.Sin(GMAS * MyMath.DegreesToRadians) * (1.914602 - (T * (0.004817 + 0.000014 * T))) +

//            Math.Sin((2 * GMAS) * MyMath.DegreesToRadians) * (0.019993 - 0.000101 * T) +

//            Math.Sin((3 * GMAS) * MyMath.DegreesToRadians) * 0.000289;


//            double STL = GMLS + SEC;

//            double SAL = STL - 0.00569 - (0.00478 * Math.Sin((125.04 - 1934.136 * T) * MyMath.DegreesToRadians));

//            double MOE = 23 + (26 + (21.448 - T * (46.815 + T * (0.00059 - T * 0.001813))) / 60) / 60;

//            double OC = MOE + 0.00256 * Math.Cos((125.04 - 1934.136 * T) * MyMath.DegreesToRadians);

//            double X = Math.Cos(SAL * MyMath.DegreesToRadians);
//            double Y = Math.Cos(OC * MyMath.DegreesToRadians) * Math.Sin(SAL * MyMath.DegreesToRadians);
//            double Z = Math.Sin(OC * MyMath.DegreesToRadians) * Math.Sin(SAL * MyMath.DegreesToRadians);


//            SE.SRA = Math.Atan2(Y, X) * MyMath.RadiansToDegrees;
//            SE.SDec = Math.Asin(Z) * MyMath.RadiansToDegrees;

//            if (X < 0.0)
//                SE.SRA = SE.SRA + 180.0;
//            else if (Y < 0.0 && X > 0.0)
//                SE.SRA = SE.SRA + 360.0;

//            double S0 = FFF.SiderealTime(FFF.JulianDay(t.Day, t.Month, t.Year, 0.0));
//            double Nsec = t.TimeOfDay.TotalSeconds; // количество секунд, прошедших  от начала суток до момента наблюдения


//            var testt = MyFunction.uds1900(FFF.JulianDay(t.Day, t.Month, t.Year, 0.0)) * MyMath.RadiansToDegrees;

//            double NsecS = Nsec * 366.2422 / 365.2422; //количество  звездных секунд
//            double GMT = (S0 + NsecS) / 3600 * 15;//гринвическое среднее звездное время в градусах SG
//            while (GMT > 360.0) GMT = GMT - 360.0;

//            // получаем подсолнечную точку
//            return new Geo2D(SE.SRA - GMT, SE.SDec, Geo2DTypes.Degrees);
//        }


//        public static Geo2D NotYet(DateTime t)
//        {
//            SolarElements result = new SolarElements();
//            double JD = FFF.JulianDay(t.Day, t.Month, t.Year, 0.0);
//            double JDE = FFF.JulianDay(t.Day, t.Month, t.Year, t.TimeOfDay.TotalSeconds);

//            //find T the fraction of a julian century from this formula:
//            double T = (JDE - 2451545) / 36525;

//            // mean longitude of the sun, degree
//            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T * T;
//            L0 = MyMath.WrapAngle360(L0);

//            //mean anomaly of the sun, degree
//            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T * T - 0.00000048 * T * T * T;
//            M = MyMath.WrapAngle360(M);
         
//            // obliquity eps of ecliptic
//            double eps = 23.0 + 26.0 / 60.0 + 21.448 / 3600.0 - (46.8150 * T + 0.00059 * T * T - 0.001813 * T * T * T) / 3600.0;

//            double k = 2 * Math.PI / 360.0;
//            double DL = (1.914600 - 0.004817 * T - 0.000014 * T * T) * Math.Sin(k * M) + (0.019993 - 0.000101 * T) * Math.Sin(k * 2 * M) + 0.000290 * Math.Sin(k * 3 * M);
//            // true ecliptic longitude of the sun, degree
//            double L = L0 + DL;

//            // convert ecliptic longitude L to right ascension RA and declination delta
//            // (the ecliptic latitude of the Sun is assumed to be zero): 

//            double X = Math.Cos(L * MyMath.DegreesToRadians);
//            double Y = Math.Cos(eps * MyMath.DegreesToRadians) * Math.Sin(L * MyMath.DegreesToRadians);
//            double Z = Math.Sin(eps * MyMath.DegreesToRadians) * Math.Sin(L * MyMath.DegreesToRadians);
//            double R = Math.Sqrt(1.0 - Z * Z);

//            double Dec = (180.0 / Math.PI) * Math.Atan(Z / R); // in degrees            
//            //delta = (180.0 / Math.PI) * Math.Asin(Math.Sin(eps) * Math.Sin(L)); // in degrees

//            double RA = (180.0 / Math.PI)/*(24.0 / Math.PI)*/ * Math.Atan(Y / (X + R)); // in hours                                                                                                                                                                                 
//            //RA = (180.0 / Math.PI) * Math.Atan((Math.Sin(L) * Math.Cos(eps) - Math.Tan(B) * Math.Sin(eps)) / Math.Cos(L)); // in degrees

//            if (X < 0.0)
//                RA = RA + 180.0;
//            else if (Y < 0.0 && X > 0.0)
//                RA = RA + 360.0;

//            double S0 = FFF.SiderealTime(JD);
//            double Nsec = t.TimeOfDay.TotalSeconds; // количество секунд, прошедших  от начала суток до момента наблюдения

//            double NsecS = Nsec * 366.2422 / 365.2422; //количество  звездных секунд
//            double GMT = (S0 + NsecS) / 3600 * 15;//гринвическое среднее звездное время в градусах SG
//            while (GMT > 360.0) GMT = GMT - 360.0;

//            // получаем подсолнечную точку
//            return new Geo2D(RA - GMT, Dec, Geo2DTypes.Degrees);
//        }


//   //     public int tzOffset;   // Time zone Offset, zones west of GMT are negative

//     //   public double lat = 0.0;     // Latitude of site, values north of equator are positive

//     //   public double lon = 0.0;      // Longitude of site, values west of GMT are negative

//    //    public double timeFracDay;  // Fraction of day past midnight for current time

//   //     public long unixDays;  // Days since 1970-1-1

//  //      public double JDN;     // Julian Day Number

//  //      public double JCN;     // Julian Century Number

//  //      public double GMLS;    // Geometric Mean Longitude of Sun

//  //      public double GMAS;    // Geometric Mean Anomaly of Sun

//  //      public double EEO;     // Eccentricity of Earth Orbit (degrees)

//  //      public double SEC;     // Sun Equation of Center 

// //       public double STL;     // Sun True Longitude (degrees)

//  //      public double STA;     // Sun True Anomaly (degrees)

//  //      public double SRV;     // Sun Radian Vector (degrees)

//  //      public double SAL;     // Sun Apparent Longitude 

// //       public double MOE;     // Mean Oblique Ecliptic (degrees)

////        public double OC;      // Oblique correction 

//        public double SRA;     // Sun Right Ascension (degrees)

//        public double SDec;    // Sun Declination (degrees)

// //       public double vy;      // var y

//  //      public double EOT;     // Equation of Time (minutes)

//     //   public double HAS;     // Hour Angle Sunrise (degrees)

//     //   public double SolarNoonfrac;       // Solar noon (fractional day)

//    //    public double SolarNoonDays;   // Solar Noon (days since 1970-1-1)

//  //      public DateTime/* atime_t*/ SolarNoonTime;   // Solar Noon time (Time Object)

//    //    public double Sunrise;     // Sunrise time (unix time, seconds)

//   //     public DateTime/* atime_t*/ SunriseTime; // Sunrise time (Time object)

//  //      public double Sunset;      // Sunset times (unix time, seconds)

//  //      public DateTime/* atime_t*/ SunsetTime;  // Sunset time (Time object)

//    //    public double SunDuration; // Sunlight Duration (minutes)

//  //      public double TST;     // True Solar Time (minutes)

//   //     public double HA;      // Hour Angle (degrees)

// //       public double SZA;  // Solar Zenith Angle (degrees)

// //       public double SEA;     // Solar Elevation Angle (degrees)

//  //      public double AAR;  // Approximate Atmospheric Refraction 

//  //      public double SEC_Corr; // Solar Elevation, Corrected (degrees)

// //       public double SAA; // Solar Azimuth Angle (degrees)

//}

}
