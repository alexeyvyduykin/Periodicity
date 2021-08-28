using System;

namespace PRDCT.Core
{
    public class TMyDate
    {
        public enum DAY_OF_WEEK
        {
            SUNDAY = 0,
            MONDAY = 1,
            TUESDAY = 2,
            WEDNESDAY = 3,
            THURSDAY = 4,
            FRIDAY = 5,
            SATURDAY = 6
        }

        public TMyDate()
        {
            dblJulian = 0.0;
        }

        public TMyDate(int Year, int Month, double Day)
        {
            Set(Year, Month, Day, 0.0, 0.0, 0.0);
        }
        public TMyDate(int Year, int Month, double Day, double Hour, double Minute, double Second)
        {
            Set(Year, Month, Day, Hour, Minute, Second);
        }
        public TMyDate(double JD)
        {
            dblJulian = JD;
        }

        public TMyDate(DateTime dateTime)
        {
            Set(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Millisecond);
        }

        public enum DateTimeFlag
        {
            Date = 0,
            Time = 1,
            DateTime = 2
        }

        //public TMyDate(string data, DateTimeFlag flag = DateTimeFlag.DateTime)
        //{
        //    string ASMonth = "   ";
        //    int Day, Year, Hour, Minute, Second;

        //    switch (flag)
        //    {
        //        case DateTimeFlag.Date:
        //            sscanf(data, DateFormat, &Day, ASMonth, &Year);
        //            Set(Year, StrToIntMonth(ASMonth), Day, 0.0, 0.0, 0.0);
        //            break;
        //        case DateTimeFlag.Time:
        //            sscanf(data, TimeFormat, &Hour, &Minute, &Second);
        //            dblJulian = (Hour / 24.0) + (Minute / 1440.0) + (Second / 86400.0);
        //            break;
        //        case DateTimeFlag.DateTime:
        //            sscanf(data, DateTimeFormat, &Day, ASMonth, &Year, &Hour, &Minute, &Second);
        //            Set(Year, StrToIntMonth(ASMonth), Day, Hour, Minute, Second);
        //            break;
        //    }
        //}

        //Non Static methods
        public void Set(int Year, int Month, double Day, double Hour, double Minute, double Second)
        {
            double dblDay = Day + (Hour / 24.0) + (Minute / 1440.0) + (Second / 86400.0);
            dblJulian = DateToJD(Year, Month, dblDay);
        }

        public void Get(out int Year, out int Month, out int Day, out int Hour, out int Minute, out double Second)
        {
            double JD = dblJulian + 0.5;
            double tempZ;

            double F = MyMath.Modf(JD, out tempZ);
            int Z = (int)tempZ;

            int alpha = INT((Z - 1867216.25) / 36524.25);
            int A = Z + 1 + alpha - INT(INT(alpha) / 4.0);

            int B = A + 1524;
            int C = INT((B - 122.1) / 365.25);
            int D = INT(365.25 * C);
            int E = INT((B - D) / 30.6001);

            double dblDay = B - D - INT(30.6001 * E) + F;
            Day = (int)dblDay;

            if (E < 14)
            {
                Month = E - 1;
            }
            else
            {
                Month = E - 13;
            }

            if (Month > 2)
            {
                Year = C - 4716;
            }
            else
            {
                Year = C - 4715;
            }

            F = MyMath.Modf(dblDay, out tempZ);
            Hour = INT(F * 24);
            Minute = INT((F - (Hour) / 24.0) * 1440.0);
            Second = (F - (Hour / 24.0) - (Minute / 1440.0)) * 86400.0;
        }

        private DateTime getDateTime()
        {
            int year, month, day, hour, min;
            double sec;
            Get(out year, out month, out day, out hour, out min, out sec);
            //DateTime tempDateTime = new DateTime(year, month, day);
            //tempDateTime += (86400.0 / (hour * 3600.0 + min * 60.0 + sec));
            return new DateTime(year, month, day, hour, min, (int)sec);
            //return tempDateTime;
        }

        public DateTime DateTime
        {
            get
            {
                int year, month, day, hour, min;
                double sec;
                Get(out year, out month, out day, out hour, out min, out sec);
                return new DateTime(year, month, day, hour, min, (int)sec);
            }
        }

        public DAY_OF_WEEK DayOfWeek()
        {
            return (DAY_OF_WEEK)((dblJulian + 1.5) % 7);

        }
        public long DaysInMonth()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return DaysInMonth(Month, IsLeap(Year));
        }
        public long DaysInYear()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            if (IsLeap(Year))
            {
                return 366;
            }
            else
            {
                return 365;
            }
        }
        public double DayOfYear()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return DayOfYear(dblJulian, Year);
        }
        public double FractionalYear()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            long DaysInYear;
            if (IsLeap(Year))
            {
                DaysInYear = 366;
            }
            else
            {
                DaysInYear = 365;
            }

            return Year + ((dblJulian - DateToJD(Year, 1, 1)) / DaysInYear);
        }
        public bool Leap()
        {
            return IsLeap(Year());
        }
        public double Julian()
        {
            return dblJulian;
        }
        public long Year()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return Year;
        }
        public long Hour()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return Hour;
        }
        public long Minute()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return Minute;
        }
        public double Second()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return Second;
        }

        //My Functions from GlobalTime
        //enum MonthFlagEng {Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec};

        //public string AnsiStringFormat(DateTimeFlag flag = DateTimeFlag.DateTime)
        //{
        //    int Year, Month, Day, Hour, Minute;
        //    double Second;
        //    Get(out Year, out Month, out Day, out Hour, out Minute, out Second);

        //    string[] StrMonths = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    string StrDate = "";
        //    if (Day < 10)
        //        StrDate += "0";
        //    StrDate += Day.ToString() + " " + StrMonths[Month - 1] + " " + Year.ToString();
        //    string StrTime = "";
        //    if (Hour < 10)
        //        StrTime += "0";
        //    StrTime += Hour.ToString() + ":";
        //    if (Minute < 10)
        //        StrTime += "0";
        //    StrTime += Minute.ToString() + ":";
        //    if (Second < 10)
        //        StrTime += "0";
        //    StrTime += Second.ToString();

        //    switch (flag)
        //    {
        //        case DateTimeFlag.Date:
        //            return StrDate;
        //        case DateTimeFlag.Time:
        //            return StrTime;
        //        case DateTimeFlag.DateTime:
        //            return StrDate + " " + StrTime;
        //    }
        //    return "";
        //}
        //public double s0_Nut(int nut)    // nut = 1 - нутация учитывается
        //{
        //    double jd2000 = 2451545.0; // 12h UTC 1 января
        //    double jd = JulianDate0h();//floor( dblJulian );   // на 0h         // ????????
        //    double d = jd - jd2000 - 0.5;                     // ????????
        //    double t = d / 36525.0; // 36525 - юлианский период 100 лет
        //    double t2 = t * t;
        //    double h1 = 24110.54841;
        //    double h2 = 8640184.812866 * t;
        //    double h3 = 0.093104 * t2;
        //    double h4 = t2 * t * 6.2E-6;
        //    double na;
        //    if (nut == 0) na = 0;
        //    else na = MyFunction. utc_nut(t);
        //    double s0_m = h1 + h2 + h3 - h4 + na;

        //    double s0_m_mod = Math.IEEERemainder(s0_m, 86400);
        //    //double s0_day = floor(s0_m / 86400);
        //    //double s0_m_hour = s0_m_mod / 3600.0;
        //    //s0_m_hour = floor(s0_m_mod / 3600);
        //    //double sec_min = s0_m_mod - s0_m_hour * 3600;
        //    //double s0_m_min = floor(sec_min / 60);
        //    //double s0_m_sec = sec_min - s0_m_min * 60;
        //    return s0_m_mod;
        //}
        public double JulianDate0h()
        {
            int Year, Month, Day, Hour, Minute;
            double Second;
            Get(out Year, out Month, out Day, out Hour, out Minute, out Second);
            return DateToJD(Year, Month, Day);
        }
        //---------------------------------------
        //public double S0Apparent_SEC()    // nut = 1  true
        //{
        //    return s0_Nut(1);
        //}
        //public double S0Mean_SEC()         // nut = 0  false
        //{
        //    return s0_Nut(0);
        //}
        //public double S0Apparent_RAD()
        //{
        //    return s0_Nut(1) * MyMath.SEC_IN_RAD;
        //}
        //public double S0Mean_RAD()
        //{
        //    return s0_Nut(0) * MyMath.SEC_IN_RAD;
        //}
        public double SecondOf0h()
        {
            double ttt = Hour() * 3600.0 + Minute() * 60.0 + Second();
            return ttt;
        }

        //public TMyDate operator +(double tsec)
        //{
        //    return new TMyDate(dblJulian + tsec / 86400.0);
        //}
        //TMyDate& operator=(const TMyDate& date);


        //Static Methods
        private static double DateToJD(int Year, int Month, double Day)
        {
            long Y = Year;
            long M = Month;
            if (M < 3)
            {
                Y = Y - 1;
                M = M + 12;
            }
            long A = INT(Y / 100.0);
            long B = 2 - A + INT(A / 4.0);
            return INT(365.25 * (Y + 4716)) + INT(30.6001 * (M + 1)) + Day + B - 1524.5;
        }
        private static void DayOfYearToDayAndMonth(int DayOfYear, bool bLeap, out int DayOfMonth, out int Month)
        {
            int K = bLeap ? 1 : 2;
            Month = INT(9 * (K + DayOfYear) / 275.0 + 0.98);
            if (DayOfYear < 32)
            {
                Month = 1;
            }

            DayOfMonth = DayOfYear - INT((275 * Month) / 9.0) + (K * INT((Month + 9) / 12.0)) + 30;

        }
        private static double DayOfYear(double JD, int Year)
        {
            return JD - DateToJD(Year, 1, 1) + 1;


        }
        private static long DaysInMonth(int Month, bool bLeap)
        {
            int[] MonthLength = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (bLeap)
            {
                MonthLength[1]++;
            }

            return MonthLength[Month - 1];

        }
        private static int INT(double value)
        {
            if (value >= 0)
            {
                return (int)(value);
            }
            else
            {
                return (int)(value - 1);
            }
        }
        private static bool IsLeap(long Year)
        {
            if ((Year % 100) == 0)
            {
                return ((Year % 400) == 0) ? true : false;
            }
            else
            {
                return ((Year % 4) == 0) ? true : false;
            }
        }

        protected double dblJulian;
    }


}
