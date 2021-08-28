using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDCT.Core.TEST.DateTime
{
    public static class Date // { to change date between civil and julian forms }
    {

        // { TransDatetoJD is a procedure
        //   for transition
        //       from Date in form Day Month Year  to Julian Day
        // from the book  Astronomical Formulae for Calculators
        //                by Jean Meeus  Moscow Mir 1988 pages 27 , 28 , 29 }

        public static void TransDateToJD(int NDay, int NMonth, int NYear, out double TinJD)
        {
            int m, y;
            double a, b;
            if (NMonth > 2)
            {
                y = NYear;
                m = NMonth;
            }
            else
            {
                y = NYear - 1;
                m = NMonth + 12;
            }
            double q = NYear + 0.01 * NMonth + 0.0001 * NDay;
            if (q >= 1582.1015)
            {
                a = (int)(0.01 * y);
                b = 2 - a + (int)(0.25 * a);
            }
            else
                b = 0;
            TinJD = (int)(30.6001 * (m + 1)) + NDay + 1720994.5 + b;
            if (y > 0)
                TinJD = TinJD + (int)(365.25 * y);
            else
                TinJD = TinJD + (int)(365.25 * y - 0.75);
        }

        // { TransJDtoDate is a procedure
        //   for transition
        //       from Julian Day to Date in form Day Month Year
        //   from the book Astronomical Formulae for Calculators
        //                 by Jean Meeus  Moscow Mir 1988 pages 29 ,30 }


        public static void TransJDToDate(double TinJD, out int NDay, out int NMonth, out int NYear, out double SecPart)
        {
            int a, alfa, m, y;
            double t = TinJD + 0.5;
            int z = (int)Math.Truncate(t);
            double f = t - Math.Truncate(t); // Frac(t);
            if (z < 2299161)
                a = z;
            else
            {
                alfa = (int)Math.Truncate((z - 1867216.25) / 36524.25);
                a = z + 1 + alfa - (alfa / 4); // div
            }
            int b = a + 1524;
            int c = (int)Math.Truncate((b - 122.1) / 365.25);
            int d = (int)Math.Truncate(365.25 * c);
            int e = (int)Math.Truncate((b - d) / 30.6001);
            NDay = b - d - (int)Math.Truncate(30.6001 * e);
            if (e > 13)
                m = e - 13;
            else
                m = e - 1;
            NMonth = m;
            if (m > 2)
                y = c - 4716;
            else
                y = c - 4715;
            NYear = y;
            SecPart = 86400.0 * f;
        }

        // { this is quite simple procedure
        //   to change a value for the part of day in second
        //        to the civil form :     Hour Minute Second }


//        public static void FromSecPartToHourMinSec(SecPart             : Extended ;
//Var
//  IntHour, IntMin  : Integer ;
//                                     Var Second         : Extended ) ;
// Var
//   r     : Extended;
// Begin
//    r:=SecPart/3600; IntHour:=Trunc(r);
//r:=60* Frac(r); IntMin:=Trunc(r); Second:=60* Frac(r);
//    if  Second > 59.999 then begin Second:=0; IntMin:=IntMin+1; end;
//    if  IntMin > 59 then
//      begin IntMin:=IntMin-60; IntHour:=IntHour+1; end;
// End;

 
//public static string ToGetSimpleDate(dt  : Extended ) : ShortString ;
// var
//   nd, nm, ny  : Integer ;
//   kh,km  : Integer ;
//   ap,az  : Extended ;
//   st  : ShortString ;
// begin
//   TransJDToDate(dt, nd, nm, ny, ap);
//   FromSecPartToHourMinSec(ap, kh, km, az);
//st:=IntegerToFixStr(ny,4)+' '+IntegerToFixStr(nm,2)
//      +' '+IntegerToFixStr(nd,2)+'  ';
//   st:=st+IntegerToFixStr(kh,2)+' '+IntegerToFixStr(km,2)
//      +' '+FloatToFixStr(az,6,3);
//ToGetSimpleDate:=st;
// end;

 
//public static string ToGetOnlyDate(dt  : Extended ) : ShortString ;
// var
//   nd, nm, ny  : Integer ;
//   ap  : Extended ;
//   st  : ShortString ;
// begin
//   TransJDToDate(dt, nd, nm, ny, ap);
//st:=IntegerToFixStr(nd,2)+'  '+IntegerToFixStr(nm,2)
//      +'  '+IntegerToFixStr(ny,4);
//ToGetOnlyDate:=st;
// end;

 
//public static string ToGetOnlyTime(dt  : Extended ) : ShortString ;
// var
//   nd, nm, ny  : Integer ;
//   kh,km  : Integer ;
//   ap,az  : Extended ;
//   st  : ShortString ;
// begin
//   TransJDToDate(dt, nd, nm, ny, ap);
//   FromSecPartToHourMinSec(ap, kh, km, az);
//st:=IntegerToFixStr(kh,2)+'  '+IntegerToFixStr(km,2)
//      +'  '+FloatToFixStr(az,6,3);
//ToGetOnlyTime:=st;
// end;

 
//public static string ToGetDateSecP(dt  : Extended ) : ShortString ;
// var
//   nd, nm, ny  : Integer ; // day month year
//   ap  : Extended ;      // part of day in second
//   st  : ShortString ;   // our string
// begin
//   TransJDToDate(dt, nd, nm, ny, ap);
//st:=IntegerToFixStr(ny,4)
//      +' '+IntegerToFixStr(nm,2)
//      +' '+IntegerToFixStr(nd,2)
//      +' '+FloatToFixStr(ap,7,1);
//ToGetDateSecP:=st;   // with the part of day in second
// end;

 
//public static string ToGetStrForDate(dt  : Extended ) : ShortString ;
// begin
//   ToGetStrForDate:='  '+ToGetSimpleDate(dt)+'  date in UTC';
// end;

//// { string has a mask: 00.00.0000  00h00m00s }

 
//public static void DateFromString(st  : ShortString ;
//var bs  : Boolean ;
//                            var dt  : Extended ) ;
// var
//   sa  : ShortString ;
//   nd,nm,ny  : Integer ;
//   kh,km,ks,er  : Integer ;
// begin
//   bs:=False; // initial
//   dt:=0.0;   // initial
//   sa:=Copy(st,2,2); // day
//   Val(sa, nd, er); // day
//   if  er<> 0  then Exit; // may be error
//sa:=Copy(st,5,2); // month
//   Val(sa, nm, er); // month
//   if  er<> 0  then Exit; // may be error
//sa:=Copy(st,8,4); // year
//   Val(sa, ny, er); // year
//   if  er<> 0  then Exit; // may be error
//sa:=Copy(st,14,2); // hour
//   Val(sa, kh, er); // hour
//   if  er<> 0  then Exit; // may be error
//sa:=Copy(st,17,2); // minute
//   Val(sa, km, er); // minute
//   if  er<> 0  then Exit; // may be error
//sa:=Copy(st,20,2); // second
//   Val(sa, ks, er); // second
//   if  er<> 0  then Exit; // may be error
//   TransDateToJD(nd, nm, ny, dt); // julian date
//dt:=dt+(kh+(km+ks/60.0)/60.0)/24.0; // julian date with part of day
//   bs:=True; // may be julian date
// end;

//// { string has a mask: 00.00.0000  00h00m00.000s }

 
//public static string StringFromDate(dt  : Extended ) : ShortString ;
// var
//   st  : ShortString ;
//   nd,nm,ny  : Integer ;
//   kh,km  : Integer ;
//   secp,sec  : Extended ;
// begin
//   TransJDToDate(dt, nd, nm, ny, secp);
//   FromSecPartToHourMinSec(secp, kh, km, sec);
//st:=' '+IntegerToFixStr(nd,2)+'.'+IntegerToFixStr(nm,2)+'.';
//   st:=st+IntegerToFixStr(ny,4)+'  ';
//   st:=st+IntegerToFixStr(kh,2)+'h'+IntegerToFixStr(km,2)+'m';
//   st:=st+FloatToFixStr(sec,6,3)+'s';
//   StringFromDate:=st;
// end;

// END.

    }
}
