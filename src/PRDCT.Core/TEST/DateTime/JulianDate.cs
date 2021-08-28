using System;
using System.Collections.Generic;
using PRDCT.Core.TEST.DateTime;

namespace PRDCT.Core.TEST.Main
{
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


    public static class JulianDateTime
    {
        // {  FromUTCtoTT is a procedure for transition UTC  to TT. }

        #region Consts

        private static readonly List<double[]> TablOne = new List<double[]>
        {
            new double[] { 35473.00, 31.34 },             //{ 1956  1  1 }
            new double[] { 35747.00, 31.56 },             //{ 1956 10  1 }
            new double[] { 36112.00, 32.00 },             //{ 1957 10  1 }
            new double[] { 36477.00, 32.52 },             //{ 1958 10  1 }
            new double[] { 36842.00, 33.00 },             //{ 1959 10  1 }
            new double[] { 37208.00, 33.45 },             //{ 1960 10  1 }
            new double[] { 37665.00, 33.99 }             //{ 1962  1  1 }
        };
        private static readonly List<double[]> Tabl = new List<double[]>
        {
            new double[] { 37665.0, 34.0298580, 0.0011232, 37665.0 },   //{  1962 01 01  }
            new double[] { 38334.0, 34.1298580, 0.0011232, 37665.0 },   //{  1963 11 01  }
            new double[] { 38395.0, 35.4241300, 0.0012960, 38761.0 },   //{  1964 01 01  }
            new double[] { 38486.0, 35.5241300, 0.0012960, 38761.0 },   //{  1964 04 01  }
            new double[] { 38639.0, 35.6241300, 0.0012960, 38761.0 },   //{  1964 09 01  }
            new double[] { 38761.0, 35.7241300, 0.0012960, 38761.0 },   //{  1965 01 01  }
            new double[] { 38820.0, 35.8241300, 0.0012960, 38761.0 },   //{  1965 03 01  }
            new double[] { 38942.0, 35.9241300, 0.0012960, 38761.0 },   //{  1965 07 01  }
            new double[] { 39004.0, 36.0241300, 0.0012960, 38761.0 },   //{  1965 09 01  }
            new double[] { 39126.0, 36.4971700, 0.0025920, 39126.0 },   //{  1966 01 01  }
            new double[] { 39887.0, 36.5971700, 0.0025920, 39126.0 },   //{  1968 02 01  }
            new double[] { 41317.0, 42.184,     0.0,       0.0 },       //{  1972 01 01  }
            new double[] { 41499.0, 43.184,     0.0,       0.0 },       //{  1972 07 01  }
            new double[] { 41683.0, 44.184,     0.0,       0.0 },       //{  1973 01 01  }
            new double[] { 42048.0, 45.184,     0.0,       0.0 },       //{  1974 01 01  }
            new double[] { 42413.0, 46.184,     0.0,       0.0 },       //{  1975 01 01  }
            new double[] { 42778.0, 47.184,     0.0,       0.0 },       //{  1976 01 01  }
            new double[] { 43144.0, 48.184,     0.0,       0.0 },       //{  1977 01 01  }
            new double[] { 43509.0, 49.184,     0.0,       0.0 },       //{  1978 01 01  }
            new double[] { 43874.0, 50.184,     0.0,       0.0 },       //{  1979 01 01  }
            new double[] { 44239.0, 51.184,     0.0,       0.0 },       //{  1980 01 01  }
            new double[] { 44786.0, 52.184,     0.0,       0.0 },       //{  1981 07 01  }
            new double[] { 45151.0, 53.184,     0.0,       0.0 },       //{  1982 07 01  }
            new double[] { 45516.0, 54.184,     0.0,       0.0 },       //{  1983 07 01  }
            new double[] { 46247.0, 55.184,     0.0,       0.0 },       //{  1985 07 01  }
            new double[] { 47161.0, 56.184,     0.0,       0.0 },       //{  1988 01 01  }
            new double[] { 47892.0, 57.184,     0.0,       0.0 },       //{  1990 01 01  }
            new double[] { 48257.0, 58.184,     0.0,       0.0 },       //{  1991 01 01  }
            new double[] { 48804.0, 59.184,     0.0,       0.0 },       //{  1992 07 01  }
            new double[] { 49169.0, 60.184,     0.0,       0.0 },       //{  1993 07 01  }
            new double[] { 49534.0, 61.184,     0.0,       0.0 },       //{  1994 07 01  }
            new double[] { 50083.0, 62.184,     0.0,       0.0 },       //{  1996 01 01  }
            new double[] { 50630.0, 63.184,     0.0,       0.0 },       //{  1997 07 01  }
            new double[] { 51178.0, 64.184,     0.0,       0.0 },       //{  1999 01 01  }
            new double[] { 53736.0, 65.184,     0.0,       0.0 }        //{  2006 01 01  }
        };

        #endregion

        public static void FromUTCtoTT(double JulUTC, out double JulTT, out double DeltaTA)
        {
            JulTT = JulUTC;
            DeltaTA = 0.0;

            if (JulTT < 2435473.5)
            {
                return;
            }

            if (JulUTC < (Tabl[0][0] + Consts.DifEpoch))
            {
                if (JulUTC < (TablOne[0][0] + Consts.DifEpoch))
                {
                    DeltaTA = TablOne[0][1] * (JulUTC - 2415020.5) / (TablOne[0][0] - 15020.0);
                    DeltaTA = DeltaTA / 86400;
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if ((TablOne[i][0] <= (JulUTC - Consts.DifEpoch)) && ((JulUTC - Consts.DifEpoch) < TablOne[i + 1][0]))
                        {
                            double r = (JulUTC - Consts.DifEpoch - TablOne[i][0]) / (TablOne[i + 1][0] - TablOne[i][0]);
                            DeltaTA = TablOne[i][1] + r * (TablOne[i + 1][1] - TablOne[i][1]);
                            DeltaTA = DeltaTA / 86400;
                        }
                    }
                }
                JulTT = JulUTC + DeltaTA;
                return;
            }

            for (int i = 0; i < Tabl.Count - 1; i++)
            {
                if ((Tabl[i][0] <= (JulUTC - Consts.DifEpoch)) && ((JulUTC - Consts.DifEpoch) < Tabl[i + 1][0]))
                {
                    DeltaTA = (Tabl[i][1] + Tabl[i][2] * (JulUTC - Consts.DifEpoch - Tabl[i][3])) / 86400.0e0;
                }
            }

            if ((JulUTC - Consts.DifEpoch) > Tabl[Tabl.Count - 1][0])
            {
                DeltaTA = Tabl[Tabl.Count - 1][1] / 86400;
            }

            JulTT = JulUTC + DeltaTA;
        }

        // { ToGetTEph is a function
        //   to obtain the moment in  TE with the use a value for  TT }

        public static double ToGetTEph(double JulTT)
        {
            double d = (JulTT - Consts.JD2000) / Consts.JulianC;
            double g = MyMath.DegreesToRadians * (357.528 + 35999.050 * d);
            return JulTT + (0.001658 * Math.Sin(g + 0.0167 * Math.Sin(g))) / 86400.0e0;
        }

        //{ to get universal time coordinate when terrestrial time is known }

        public static double ToGetUTC(double JulTT)
        {
            double d, r;
            double t = JulTT; // { JulTT is Terrestrial Time }
            FromUTCtoTT(t, out d, out r);
            t = JulTT - r; //{ one more }
            FromUTCtoTT(t, out d, out r);
            return JulTT - r;
        }

        //{ to get Universal Time if UTC and DeltaUT1 are known }

        public static double ToGetUT1(double JulUTC, double DeltaUT1)
        {
            return JulUTC + DeltaUT1;
        }

        // { ClcGrMSTime is a function
        //   to obtain Greenwich Mean Sidereal Time in radians.
        //      IAU 1976  for the Greenwich Mean Sidereal Time.
        //      TinUTC in UTC and close to UT1 }

        public static double ToGetGrMSTime(double TinUTC)
        {
            double t = TinUTC;
            double aj = Math.Truncate(t) + 0.5;
            double s = t - aj;
            double ajj = aj - 2451545.0;
            double ajs = ajj / 36525.0;
            double s0 = 1.753368559233266e0 + (628.3319706888409e0
                + (6.770713944903336e-06 - 4.508767234318685e-10 * ajs) * ajs) * ajs;
            double freq = 1.002737909350795e0
                + (5.900575455674703e-11 - 5.893984333409384e-15 * ajs) * ajs;
            s0 = s0 + freq * s * 2 * Math.PI;
            double r = s0 / (2 * Math.PI);
            int IntPart = (int)Math.Truncate(r);
            double SidTime = s0 - 2 * Math.PI * IntPart;
            if (SidTime < 0)
            {
                SidTime = SidTime + 2 * Math.PI;
            }

            return SidTime;
        }

        public static string ToGetWeekJulianDay(double t)
        {
            // { null point is monday 30 december 1974 year or 2442411.5 in MJD }
            double d = t - 2442411.5 + 1.0e-8;
            if (d < 0.0)
            {
                d = d - 1;
            }

            int l = (int)Math.Truncate(d);
            int k = l % 7;
            if (k < 0)
            {
                k = k + 7;
            }

            int n = k + 1; // { from 1 to 7 or from monday to sunday }
            return Consts.StrDayWeek[n]; // const StrDayWeek from UnConTyp
        }

        // variable Day date in julian day

        public static bool BooHourJump(double Day)
        {
            double SunDay = 2442410.5; // 29 december 1974 year sunday
            double t, s, March;
            int d, m, y;

            Date.TransJDToDate(Day, out d, out m, out y, out s); // from unit UnForDat
            if (y > 1980)  // no jump if  y < 1981  year     
            {
                d = 31;
                m = 3; //{ March }
                Date.TransDateToJD(d, m, y, out t); // from unit UnForDat
                int l = (int)Math.Round(t - SunDay);
                int n = l % 7;
                March = t - n; //{ march date for jumping }
                d = 31;
                m = 10; //{ October }
                Date.TransDateToJD(d, m, y, out t);
                l = (int)Math.Round(t - SunDay);
                n = l % 7;
                double Octob = t - n; //{ october date for jumping }
                if ((March < Day) && (Day < Octob)) // { one hour jump for summer time }            
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
