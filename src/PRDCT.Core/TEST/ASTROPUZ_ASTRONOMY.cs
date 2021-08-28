using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace PRDCT.Core.TEST
{
//    public class UnSatPas
//    {
//        TfmSatEph fmSatEph;
//        TfmSelEph fmSelEph;
  
//        public struct TRisCul {   // for moment rise culmination set of satellite
//           public double tr, tc, ts;
//        }     // size 3 * 10 = 30 byte
//        public struct TEphAll {   // for any satellite that are visible from point
//            double tc; // moment in julian day
//            double az, al, ro; // azimuth altitude in degree
//            int shadow;     // range in km shadow case 2 1 0
//        }    // size 4 * 10 + 1 = 41 byte


//        TRisCul RisCul; // for moment rise culmination set of satellite
//        TEphAll EphAll; // for any satellite that are visible from point


//        public void SaTopSpher(double tc, // moment in julian date
//       UnCoTVar.TElemRec el, // record for elements UnCoTVar
//                     Global.TPlaceCooRec pc, // record for site UnGloVar
//                       out int shadow, // shadow case
//                       out double az, out double al, out double ro)  // topocentric
//        {
//            // azimut altitude in degree range in kilimeter with refraction
//            vec3 xe; //  geocentric satellite position true equator
//            vec3 xs; // topocentric satellite position type from UnConTyp
//                     // in PosTopSat ClcThreeRotMatr is called with value StationPos
//            ASTROPUZ_ANALIZ.GeoTopSat(tc, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, out xe, out xs); // UnForSat
//            shadow= ToGetShadowCase(tc, xe);  // UnitShad for geocentric pos
//            CoordConverter.ClcTopCoorInDegree(xs,out az, out al, out ro); // UnForCoo with refraction correction
//        }// byte 2 shadow 1 semishadow 0 lightning

//        public void AboutSatellite(TElemRec el)
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//     Add('  ' + IntToStr(el.satnuml) + '  ' + el.satname + '  спутник');
//            Add('  '); // functions from SysUtils UnForStr UnForDate
//            Add('  ' + FloatToFixStr(el.t - DifEpoch, 14, 8) + '  эпоха' + StringFromDate(el.t));
//            Add('     ' + FloatToFixStr(el.an, 11, 8) + '  среднее движение');
//            Add('      ' + FloatToFixStr(el.ae, 9, 7) + '   эксцентриситет');
//            Add('    ' + FloatToFixStr(el.ai, 9, 5) + '     угол наклонения');  // UnForStr
//            Add('  ');
//            end;
//        }

//        public string strDayNight(char ch)
//        {
//case  ch of
//          'D' : strDayNight:= '  день';
//            'T' : strDayNight:= '  сумерки';
//            'N' : strDayNight:= '  ночь';
//            end; // case character
//        }

//        public string strShadowCase(int b)
//        {
//case  b of
//           2  :  strShadowCase:= '  тень';
//            1  :  strShadowCase:= '  полутень';
//            0  :  strShadowCase:= '  ';
//            end; // case byte
//        }

//        public void PasCulMemo(double t, string s)
//        {
//            char c;         // character for day or night
//            int b;         // 2 shadow 1 semishadow 0 lightning
//            double z, h, r; // topocentric satellite position
//            SaTopSpher(t, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, z, h, r);
//            c:= chDayOrNight(t); // this may be called after ClcThreeRotMatr proc
//            with fmSatEph.MemoEph.Lines  do
//                Add(ToGetSimpleDate(t)           // date
//                   + '    ' + FloatToFixStr(z, 7, 3)   // azimuth degree
//                   + '    ' + FloatToFixStr(h, 6, 3)   // altitude degree
//                   + strDayNight(c) + s  // day or night rise culmination or set
//                   + strShadowCase(b));    // shadow case
//}

//        public void AboutPasCulm(double tr, double ts, double tc)
//        {
//            Inc(fmSatEph.NumPoint);
//            fmSatEph.MemoEph.Lines.Add('  ');
//            fmSatEph.MemoEph.Lines.Add
//            (' прохождение  '
//                    + IntToStr(fmSatEph.NumPoint)  // SysUtils
//                    + '   продолжительность  '
//                    + FloatToFixStr(1440.0 * (ts - tr), 8, 3) + ' минуты');
//            PasCulMemo(tr, ' восход');
//            PasCulMemo(tc, ' кульминация');
//            PasCulMemo(ts, ' заход');
//        }

//        public void AboutPassage(double tt, int bh, double az, double al, double ro)
//        {
//            string sr; // string for range
//            char ch:= chDayOrNight(tt);
//            if (not fmSelEph.BooDayEph ) and(ch = 'D')  then Exit; // this is day
//            Str(ro: 10:3, sr); // range with simple format to string
//            with fmSatEph.MemoEph.Lines  do
//                Add(ToGetSimpleDate(tt)            // UnForDat
//                    + '     ' + FloatToFixStr(az, 8, 4) // UnForStr
//                    + '    ' + FloatToFixStr(al, 7, 4)  // azimuth altitude
//                    + '   ' + sr                      // range in km
//                    + strDayNight(ch)               // day night twilight
//                    + strShadowCase(bh));           // may be shadow UnForPos

//      Inc(fmSatEph.NumPoint);              // UnSatEph
//        }

//        public void StepForStart(out double tc,// start to find passage
//                out double az, out double al, out double ro)
//        {
//            int b;
//            SaTopSpher(tc, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, az, al, ro); // here
//            if  al > 0.0 // altitude in degree
//    then // initial moment but satellite is above horizon
//      begin
//        tc:= tc - 0.4 * fmSelEph.SatPeriod; // one step backward
//            SaTopSpher(tc, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, az, al, ro);
//            end;
//        }

//        public void ToCorrectMoment(double dt, out double tt)
//        { // for correction moment with stepprint
//            int mc:= Round(86400.0 * dt); // stepprint to integer second
//            int md:= mc * Round(Frac(tt) / dt); // in second with stepprint
//            tt:= Int(tt) + md / 86400.0;   // new moment
//        }

//        public double ToFindCulm(double tr, double ts)
//        {
//            int b;        // for shadow case 2 shadow 1 0
//            int kt, kc;        // counts for try
//            double tc, dt;    // date and half of interval for interpolation
//            double az, ro;    // current azimuth range
//            double t0, t1, t2; // three date for interpolation
//            double h0, h1, h2; // three value of altitude for interpolation
//            tc:= 0.5 * (tr + ts); // approximately moment of culmination
//            dt:= 0.2 * (ts - tr); // initial half of interval around approx value
//            t0:= tc;
//            kt:= 1; // a first try with approximately value as half of interval
//            kc:= 0;
//            repeat
//              t1:= t0 - dt;
//            t2:= t0 + dt;
//            if  t1 < tr  then t1:= tr;
//            if  t2 > ts then t2:= ts;
//            SaTopSpher(t1, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, az, h1, ro);
//            SaTopSpher(t0, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, az, h0, ro);
//            SaTopSpher(t2, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, b, az, h2, ro);
//            if (h1 > h0) or(h2 > h0)
//    then // to prevent strange case in interpolation
//      begin
//        if (h1 > h0)
//                then
//                  begin
//              t0:= t1;
//            if  kc < 2  then kc:= kc + 1  else  kc:= 0;
//            end;
//            if  h2 > h0
//              then
//            begin
//              t0:= t2;
//            if  kc < 2  then kc:= kc + 1  else  kc:= 0;
//            end
//      end
//    else
//      kc:= 0;
//            if  kc = 0
//              then
//      begin
//        tc:= SquareInterPol(t1, t0, t2, h1, h0, h2); // simple square polinom
//            t0:= tc; // current value for culmination moment
//            kt:= kt + 1;   // the next try to find culmination
//            dt:= 0.1 * dt; // to compress interval
//            end;
//            until kt = 4;
//            ToFindCulm:= tc;
//        }

//        public void ForEphCapl()
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//       Add(' топоцентрические сферические координаты');
//            Add('  ');
//            Add(' год      I4');
//            Add(' месяц    I3');
//            Add(' день     I3');
//            Add(' часы     I4');
//            Add(' минуты   I3');
//            Add(' секунды  F7.3');
//            Add(' азимут (градусы)  F11.3');
//            Add(' высота (градусы)  F10.3');
//            end;
//        }

//        public void ForPasCulm() // to obtain parameters of culmination
//        {
//            ForEphCapl;
//            while  fmSatEph.BinStream.Position < fmSatEph.BinStream.Size  do
//                    begin


//                  fmSatEph.BinStream.Read(RisCul, 30); // rise in passage
//            with RisCul  do AboutPasCulm(tr, ts, tc); // information to MemoEph

//              end;
//        }

//        public void ForEphCapa()
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//       Add(' топоцентрические сферические координаты');
//            Add('  ');
//            Add(' год      I4');
//            Add(' месяц    I3');
//            Add(' день     I3');
//            Add(' часы     I4');
//            Add(' минуты   I3');
//            Add(' секунды  F7.3');
//            Add(' азимут (градусы)  F13.4');
//            Add(' высота (градусы)  F11.4');
//            Add(' расстояние (км)   F13.3');
//            Add('  ');
//            end;
//        }

//        public void ForEphSatl()   // general calculations
//        {
//            // topocentric spheric positions
//            int bs;     // for shadow case
//            double tt; // current moment
//            double az, al, ro; // topocentric satellite position
//            ForEphCapa;
//            while  fmSatEph.BinStream.Position < fmSatEph.BinStream.Size  do
//                    begin


//                  fmSatEph.BinStream.Read(RisCul, 30); // rise culm set in passage
//            tt:= RisCul.tr; // from rise
//            ToCorrectMoment(fmSelEph.StepPrint, tt); // to equal with step
//            repeat
//          SaTopSpher(tt, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, bs, az, al, ro);
//            if  al > fmSelEph.LatMinVal
//              then
//            AboutPassage(tt, bs, az, al, ro); // information to MemoEph
//            tt:= tt + fmSelEph.StepPrint;
//            until tt > RisCul.ts; // terminal moment in julian day set moment
//            end;
//        }

//        public void ForEphCapr() // format for rectangular positions
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//       Add('  ');
//            Add(' топоцентрические прямоугольные координаты');
//            Add('  ');
//            Add(' год      I4');
//            Add(' месяц    I3');
//            Add(' день     I3');
//            Add(' часы     I4');
//            Add(' минуты   I3');
//            Add(' секунды  F7.3');
//            Add(' x (км)   F13.3');
//            Add(' y (км)   F13.3');
//            Add(' z (км)   F13.3');
//            Add('  ');
//            end;
//        }

//        public void AboutTopRect(double tt, int bh, vec3 xs)
//        {
//            string sx, st;
//            char ch:= chDayOrNight(tt);  // char function from UnForPos
//            Str(xs[1]:11:3, sx);
//            st:= '  ' + sx;
//            Str(xs[2]:11:3, sx);
//            st:= st + '  ' + sx;
//            Str(xs[3]:11:3, sx);
//            st:= st + '  ' + sx;
//            if (not fmSelEph.BooDayEph ) and(ch = 'D')  then Exit; // this is day
//            with fmSatEph.MemoEph.Lines  do  // string functions UnForPos
//                Add(ToGetSimpleDate(tt) + st + strDayNight(ch) + strShadowCase(bh));

//      Inc(fmSatEph.NumPoint);  // date to string UnForDat   UnSatEph
//        }

//        public void ForEphSatr()   // rectangular positions
//        {
//            int bs;     // for shadow case
//            double tt; // current moment
//            vec3 xe; // geocentric  satellite positions
//            vec3 xs; // topocentric satellite positions
//            ForEphCapr;
//            fmSatEph.BinStream.Seek(0, soFromBeginning); // Classes
//            fmSatEph.NumPoint:= 0; // number of ephemeris points
//            while  fmSatEph.BinStream.Position < fmSatEph.BinStream.Size  do
//                    begin


//                  fmSatEph.BinStream.Read(RisCul, 30); // rise culm set in passage
//            tt:= RisCul.tr; // from rise
//            ToCorrectMoment(fmSelEph.StepPrint, tt); // to equal with step
//            repeat
//          GeoTopSat(tt, fmSelEph.SatElemEph,
//                       fmSelEph.SiteCooEph, xe, xs); // UnForSat
//            bs:= ToGetShadowCase(tt, xe);  // UnitShad for geocentric pos
//            AboutTopRect(tt, bs, xs); // information to MemoEph
//            tt:= tt + fmSelEph.StepPrint;
//            until tt > RisCul.ts; // terminal moment in julian day set moment
//            end;
//        }

//        public double rsCase(double a0, double t1, double a1, double t2, double a2)
//        { // to evaluate rise or set moment of the satellite
//            int bh; // shadow case
//            double d1, d2, b1, b2, db, dd, az, ro;

//            d1 = t1; // it is possible to find
//            b1 = a1; // a solution between t1 and t2 in julian day
//            d2 = t2; // a0 is minimum altitude in degree to find rise and set
//            b2 = a2;
//            dd = d2;
//            db = b2 - b1;
//            while (Math.Abs(db) > 1.0e-3)
//            {
//                dd = d1 + ((a0 - b1) / db) * (d2 - d1); // linear interpolation for moment
//                d1 = d2;
//                b1 = b2;
//                d2 = dd;
//                SaTopSpher(d2, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, out bh, out az, out b2, out ro);
//                db = b2 - b1;
//            }
//            return dd;
//        }

//        public void ForStacSat() // special case for geosynchronous satellite
//        { // tr rise tc culm ts set simulation

//            double tt, dt; // current moment and step in day
//            tt:= fmSelEph.StartDate;  // initial moment in julian day
//            dt:= 0.05; // step in day for geosynchronous and strange satellites
//            repeat
//  RisCul.tr:= tt;
//            RisCul.tc:= RisCul.tr + dt;
//            RisCul.ts:= RisCul.tc + dt;
//            fmSatEph.BinStream.Write(RisCul, 30); // to simulate rise culm set event
//            tt:= RisCul.ts;
//            until tt > fmSelEph.FinishDate;
//        }

//        public void ForRiseSet()
//        {
//            double tt, dt; // current moment and step in day
//            double az, ro; // topocentric satellite position
//            double t1, t2;
//            double a1, a2, a0; // current altitude and minimum altitude
//            int bh; // 2 shadow 1 semishadow 0
//            bool bo;

//            a0 = fmSelEph.LatMinVal; // minimum altitude in degree
//            dt = 0.01 * fmSelEph.SatPeriod; // period of revolituon in day step in day
//            tt = fmSelEph.StartDate;  // initial moment in julian day
//            StepForStart(out tt, out az, out a2, out ro); // now altitude a2 < 0 at the moment t2
//            t2 = tt; // initial altitude a2 for t2
//            bo = true; // to find rise
//            do
//            {
//                tt = tt + dt;
//                t1 = t2;
//                a1 = a2;
//                t2 = tt;
//                SaTopSpher(t2, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, out bh, out az, out a2, out ro);
//                if (bo)
//                {
//                    // to find rise event
//                    if ((a1 < a0) && (a0 < a2))
//                    // case of rise
//                    {
//                        RisCul.tr = rsCase(a0, t1, a1, t2, a2);  // rise moment
//                        bo = !bo; // from rise to set
//                    }
//                }
//                else // to find set event
//                    if ((a1 > a0) && (a0 > a2))
//                {
//                    RisCul.ts = rsCase(a0, t1, a1, t2, a2);  // set moment
//                    RisCul.tc = ToFindCulm(RisCul.tr, RisCul.ts); // culm
//                    fmSatEph.BinStream.Write(RisCul, 30); // rise culm set event
//                    bo = !bo; // from set to rise
//                    tt = tt + 0.5 * fmSelEph.SatPeriod; // big step forward
//                    t2 = tt;
//                    SaTopSpher(t2, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, bh, az, a2, ro);
//                }
//            }
//            while( bo && tt > fmSelEph.FinishDate); // terminal moment in julian day
//        }

//        public void ForDetailEph()
//        {
//            fmSatEph.BooPoint =
//    (fmSelEph.SatPeriod > 0.7)  // period of revolition in day
//    || ((fmSelEph.SatPeriod > 0.3)
//           && (fmSelEph.SatElemEph.ae > 0.2)); // eccentricity
//            if (fmSatEph.BooPoint)
//                // to fill BinStream of TMemoryStream
//                ForStacSat();   // for geosynchronous or high ellipticity orbit
//            else
//                ForRiseSet(); // to write moment of rise and set to BinStream
//        }

//        public void ForPointsEph()
//        {
//            fmSatEph.BinStream.Seek(0, soFromBeginning); // Classes
//            fmSatEph.NumPoint = 0; // number of ephemeris points
//            if (fmSatEph.BooPoint || (!fmSelEph.BooVisEph))
//            {
//                ForEphSatl(); // topocentric spherical positions
//                if (!fmSatEph.BooPoint)
//                    ForEphSatr(); // rectangular positions
//            }
//            else
//                ForPasCulm(); // may be only ForPasCulm three point to MemoEph
//        }

//        // CharForEphem = 'P'  sat from stat
//        public void ToFindPassage()   // called by fmSatEph.FormActivate from UnSatEph
//        {                       // information to fmSatEph.MemoEph
//            fmSatEph.BinStream.Clear(); // Classes
//            ForDetailEph();
//            ForPointsEph();
//        }
//    }

//    public class UnSatVaz
//    {
//        TfmSatEph fmSatEph;
//        //{ to put some information to MemoEph }
//        public void AboutSatMom(double t) // moment julian date
//        {
//            fmSatEph.MemoEph.Lines.Add('  ');       // UnEphSat
//            fmSatEph.MemoEph.Lines.Add('  взгляд со спутника');
//            fmSatEph.MemoEph.Lines.Add('  ' + HintButt[21]);  // UnButIni
//            fmSatEph.MemoEph.Lines.Add(ToGetStrForDate(t)); // UnForDat
//        }
//        //{ to put some information to MemoEph }
//        public void AboutSatPos(string s, vec3 x, vec3 v)
//        {
//            with fmSatEph.MemoEph.Lines  do  // MemoEph for UnSatEph
//                begin    // function FloatToFixStr from UnForStr

//               Add(s);
//            Add('  ' + FloatToFixStr(x[1], 11, 3) +
//                '  ' + FloatToFixStr(x[2], 11, 3) +
//                '  ' + FloatToFixStr(x[3], 11, 3) + '  км');
//            Add('    ' + FloatToFixStr(v[1], 9, 6) +
//                '    ' + FloatToFixStr(v[2], 9, 6) +
//                '    ' + FloatToFixStr(v[3], 9, 6) + '  км/с');
//            end;
//        }

//        //{ to put some information to MemoEph }
//        public void AboutSatGes(double a, double b, double r)
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//               Add('  географические сферические координаты');
//            Add('  ' + FloatToFixStr(a, 10, 6) + '  долгота градусы');
//            Add('  ' + FloatToFixStr(b, 10, 6) + '  широта  градусы');
//            Add('  ' + FloatToFixStr(r, 10, 3) + '  расстояние км');
//            end;
//        }

//        //{ in ObjectTopRec^ there is information about the Sun and planets }
//        public void AboutPlaOrb(int nump)
//        {
//            int n;     //  { simple count of the object   
//            bool b;     //  { to find the planet in array
//            OurPlaFromHea(nump, b, n); // UnParamO to find planet in list
//            if (b)   // for the planet information
//                with ObjectTopHeap ^[n]  do
//                begin



//                     fmSatEph.MemoEph.Lines.Add('  ');
//            fmSatEph.MemoEph.Lines.Add('  орбитальная система , ' + name);
//            fmSatEph.MemoEph.Lines.Add
//              ('  ' + FloatToFixStr(azt, 7, 3) + '  азимут');
//            fmSatEph.MemoEph.Lines.Add
//              ('  ' + FloatToFixStr((90.0 - alt), 7, 3) + '  зенитное расстояние');
//            end;
//        }

//        //{ geographical position
//        //  of the point from the Earth's centre
//        //  that perpendicular to line from satellite to the planet
//        //  angle of dissipation is angle between two direction
//        //    linked with special point P on the Earth's surface
//        //    line from P to the Sun and line from P to the satellite }
//        public void AboutPointP(int np) // to UnParamO
//        { // in PointP... longitude latitude range angle of dissipation

//            bool b;
//            double am, ap, ar, ud; // geo longitude latitude range angle d         
//            PointParametr(np, b, am, ap, ar, ud); // UnParamO for planet
//            if (!b) return;
//            fmSatEph.MemoEph.Lines.Add('  ');
//            fmSatEph.MemoEph.Lines.Add
//            ('  вектор параметр для объекта ' + PlanetNameR[np]);
//            fmSatEph.MemoEph.Lines.Add
//            ('  земная (вращающаяся) система координат');
//            fmSatEph.MemoEph.Lines.Add
//            ('     ' + FloatToFixStr(am, 7, 3) + '  географическая долгота градусы');
//            fmSatEph.MemoEph.Lines.Add
//            ('     ' + FloatToFixStr(ap, 7, 3) + '  географическая широта  градусы');
//            fmSatEph.MemoEph.Lines.Add
//            ('  ' + FloatToFixStr(ar, 10, 3)
//                 + '  расстояние от поверхности Земли км');
//            if ((np = NSun) && (ud > -90.0))// for this condition
//                                            // angle of dissipation exists
//                fmSatEph.MemoEph.Lines.Add
//                ('     ' + FloatToFixStr(ud, 7, 3) + '  угол рассеяния  градусы');
//        }

//        //{ fmSatEph from UnSatEph
//        //  position of the limb point only for shadow case }
//        public void AboutLimbPoint()   // position of the limb point
//                                       // orbital and geospheric coordinates of the surface point
//        {
//            double az, al, am, ap;
//            fmSatEph.MemoEph.Lines.Add('  ');
//            fmSatEph.MemoEph.Lines.Add
//              ('  орбитальная система , точка лимба по курсу');
//            NulloLimbPoint(az, al, am, ap); // UnForZem
//            fmSatEph.MemoEph.Lines.Add
//      ('  ' + FloatToFixStr(az, 7, 3) + '  азимут градусы');
//            fmSatEph.MemoEph.Lines.Add
//            ('  ' + FloatToFixStr((90.0 - al), 7, 3)
//                 + '  зенитное расстояние градусы');
//            fmSatEph.MemoEph.Lines.Add('  ');
//            fmSatEph.MemoEph.Lines.Add
//            ('  земная система , точка лимба по курсу');
//            fmSatEph.MemoEph.Lines.Add
//            ('  ' + FloatToFixStr(am, 7, 3) + '  географическая долгота градусы');
//            fmSatEph.MemoEph.Lines.Add
//            ('  ' + FloatToFixStr(ap, 7, 3) + '  географическая широта градусы');
//        }

//        //{ called from fmSatEph
//        //  te current date  el current record elements
//        //  matrix to change between coordinates system
//        //  RotMatr[..][..] RosMatr[..][..]
//        //TopMatr[..][..]
//        //  are declared in UnGloVar as global variable arrays
//        //  are assigned by procedure ClcThreeRotMatr FiguViewPlac UnPlaceA }
//        public void MoreAboutSat(double te, TElemRec el)
//        {
//            vec3 qx, qv;   // type UnConTyp  true eqiunox
//            vec3 rx, rv;   // type UnConTyp  greenwich meridian
//            double a, b, r; // geographic position of the satellite
//            AboutSatMom(te);
//            //{ position velosity of the satellite
//            // refer to the centre of the Earth in true equator system }
//            EilXV(te, el, qx, qv); // UnitEilP
//            AboutSatPos('  истинный экватор , положения и скорости', qx, qv);
//            // { position to Earth's rotating system
//            //   RosMatr UnGloVar to change
//            //   from true equinox to greenwich meridian rotating system }
//            MatrMultVector(RosMatr, qx, rx); // UnForFun
//                                             // { part of velosity in Earth's rotating system
//                                             //   without velosity of the rotation of the Earth }
//            MatrMultVector(RosMatr, qv, rv); // UnForFun only part for velosity
//                                             // { the next part for velosity in rotating system only for x y }
//            rv[1]:= rv[1] + VelOfRot * rx[2]; // to take rotation into account
//            rv[2]:= rv[2] - VelOfRot * rx[1]; // to take rotation into account
//            AboutSatPos('  гринвичский меридиан , положения и скорости', rx, rv);
//            ClcSpherCoorInDegree(rx, a, b, r); // UnForCoo geographic spheric
//            AboutSatGes(a, b, r); // satellite geographic coordinates
//                                  //  { in ObjectTopRec^ there is information about th Sun  PUnDE200 }
//            AboutPlaOrb(NSun); // position of the Sun in orbital system
//            AboutPointP(NSun); // to UnParamO
//            AboutLimbPoint;   // position of the limb point
//        }
//    }

//    public class UnSatVas
//    {

//        public struct TSerBop {
//            double ta, tb; // two moment set rise
//            double fa, fb; // latitude of perpend
//        }

//        public TSerBop SerBop; // to BinStream as result

//        //{ a view from the satellite
//        //    to find events
//        //    when a body may be eclipsed by the Earth}

//        public bool boevent;  // body yes or no beyond the Earth
//        public double planetd; // for the Sun the Moon projection range
//        public double sinphip; // sinus of latitude for perpenficular
//        public vec3 perpvec;   // vector of perpendicular true equa
//        public vec3 posbods;   // body position refer to satellite
//        public mat3 rotmatu;  // to change from fixed to true equator
//        public mat3 rosmatu;  // to change from true to rotating equa

//        public void ToClcBodyP(double t, ref vec3 r)
//        {
//            int np; vec3 re;
//            switch (CharBodEph)  // CharBodEph UnGloVar 'P' or 'S'
//            {
//                case 'P':
//                    np:= NumberBody; // number of selected planet
//                    PosGeoPlanet(np, t, re); // UnForPos fixed equator
//                    MatrMultVector(rotmatu, re, r); // from fixed to true equa

//                   case 'S' : ElemX(t, fmSelEph.SaoElemEph, r); // UnitEilP UnSelEph
//            } // 'P' or 'S'
//        }

//        //{ the elements of the satellite
//        //   in fmSelEph.SatElemEph of TElemRec
//        //}
//        public void ToClcSatlP(double t, ref vec3 r)
//        {
//            ElemX(t, fmSelEph.SatElemEph, r); // from UnitEilP true equator
//        }

//        //{ for the perpendicular
//        //  from the centre of the Earth
//        //    to the satellite - body direction
//        //    to obtain height from the surface
//        //    cos(beta) is angle between two directions
//        //    from the satellite to the centre of the Earth
//        //    from the satellite to the body(planet or another satellite)
//        //    GeoR0 radius of the Earth ParmA oblateness const from UnConTyp }

//        public double ToGetPerpH(double tt)// height above
//        {
//            int i;         // count
//            vec3 rs, rb;   // satellite body refer to the Earth's centre
//            vec3 ps, pe;   // position refer to satellite
//            double ze, zp; // range from satellite to the Earth body
//            double sc, cb; // scalar multiplication cos(beta)
//            double zr, zq; // range from object to perpendicular ratio
//            double sp, sf; // range from sat to perpendicular sin(phi)
//            ClcTrueRotMatr(JD2000, tt, rotmatu, rosmatu); // UnForCoo  equator
//            ToClcBodyP(tt, rb); // body position       refer to the centre
//            ToClcSatlP(tt, rs); // satellite position  of the Earth
//            for i:= 1 to 3 do // to the satellite as a centre
//                    begin                 // two vectors \vec B \vec E
//                      ps[i]:= rb[i] - rs[i]; // the selected body or satellite
//            pe[i]:= -rs[i];      // the centre of the Earth \vec E
//            posbods[i]:= ps[i];  // body position refer to satellite
//            end;
//            sc:= ScalarMult(pe, ps); // UnForFun scalar multiplication
//            ze:= VectorModul(pe);   // UnForFun range satellite - Earth
//            zp:= VectorModul(ps);   // UnForFun range satellite - body
//            cb:= sc / (ze * zp); // cos(beta) is angle between two directions
//            ToGetPerpH:= ze; // for the next case when cos(beta) < 0
//            if ((cb < 0.0) || (zp < ze)) // refer to the satellite

//                // a body may be satellite and may be very closed

//                return; // the Earth and our body are in different directions
//                        //{ length of perpendicular in kilometer from the surface }
//            sp:= ze * cb; { range from satellite to perpendicular }
//            zr:= zp - sp; { range from object to perpendicular }
//            zq:= sp / zr; { the ratio }
//            zr:= 1.0 / (1.0 + zq); // for the new vector
//            for i:= 1 to 3 do // vector perpendicular  true equator system
//                    perpvec[i]:= zr * (rs[i] + zq * rb[i]); // refer to the centre
//            if  CharBodEph = 'P' // NumberBody for the planet
//    then // to obtain projection of diameter to perpendicular
//      planetd:= 2 * RadiusOfPlanet[NumberBody] * sp / zp; // projection
//            sc:= ze * Sqrt(1 - Sqr(cb)); // range of perpendicular from centre
//            sf:= perpvec[3] / sc;     // sin(latitude) refer to true equator
//            sinphip:= sf;           // sinus of latitude for perpenficular
//            ToGetPerpH:= sc - GeoR0 * (1 - ParmA * Sqr(sf)); // UnConTyp
//        }

//        //{ to find any moment when the body is visible }

//        public double FirstMoment()
//        { // fmSelEph.StartDate is initial moment in julian day
//            int nc; // count satellite body true equator
//            double tc, dt; // current moment
//            tc:= fmSelEph.StartDate;      // the same moment UnSelEph
//            dt:= 0.01 * fmSelEph.SatPeriod; // UnSelEph period in day
//            nc:= 0;
//            while (ToGetPerpH(tc) < 10.0) and(nc < 50)  do
//                begin // body may be beyond the Earth
//                  nc:= nc + 1;  // simple count to prevent
//            tc:= tc - dt; // one step backward
//            end;
//            FirstMoment:= tc; // close to fmSelEph.StartDate
//        }

//        //{ set event exists
//        //  it is possible to find accurate set moment
//        //  in interval tn<ta < tk }

//        public double ToGetSetM(double tk, double dt)
//        {
//            double tc, st, ha, hb;
//            tc:= tk - dt;  // for start to find
//            st:= 0.01 * dt; // new step in day more small
//            hb:= ToGetPerpH(tc); // current height
//            ha:= hb;             // the same
//            while (hb > 0.0) do // it is condition for set event
//                    begin
//                      ha:= hb;    // previous height for interpolation
//            tc:= tc + st; // one step forward
//            hb:= ToGetPerpH(tc); // new value of height
//            end;
//            if (Abs(hb - ha) > 0.0)
//                then
//                  ToGetSetM:= tc - hb * st / (hb - ha) // linear intepolation
//    else
//      ToGetSetM:= tc;
//        }

//        //{ rise event exists
//        //  it is possible to find accurate set moment
//        //  in interval tn<tb < tk }

//        public double ToGetRisM(double tk, double dt)
//        {
//            double tc:= tk - dt;   // for start to find
//            double st:= 0.01 * dt; // new step in day more small
//            double hb:= ToGetPerpH(tc); // current height
//            double ha:= hb;             // the same
//            while (hb < 0.0) do  // it is condition for rise event
//                    begin
//                      ha:= hb;    // previous height for interpolation
//            tc:= tc + st; // one step forward
//            hb:= ToGetPerpH(tc); // new value of height
//            end;
//            if (Abs(hb - ha) > 0.0)
//                then
//                  ToGetRisM:= tc - hb * st / (hb - ha) // linear intepolation
//    else
//      ToGetRisM:= tc;
//        }

//        //{ to find two moments if it is possible
//        //  ta or set tb for rise when body beyond the Earth
//        //  RadGra const from UnConTyp  DATan2 function from UnForFun }

//        public void CurEvent(double tt) // result to SerBop
//        {
//            bool bs, br;  // set and rise event
//            boevent:= False; // default
//            double tc:= tt;    // current moment the first to find set event
//            double dt:= 0.01 * fmSelEph.SatPeriod; // UnSelEph period in day
//            bs:= (ToGetPerpH(tc) < 0.0); // case for set
//            while (not bs ) and(tc < fmSelEph.FinishDate)  do
//                begin
//                  tc:= tc + dt;
//            bs:= (ToGetPerpH(tc) < 0.0); // case for set
//            end;
//            if (not bs )  then Exit; // no events in this case
//                                     //  { to find moment for set event between two moments }
//            SerBop.ta:= ToGetSetM(tc, dt); // there is set event ta moment
//                                           //  { for pair satellite satellite may be strange case }
//            if (Abs(ToGetPerpH(SerBop.ta)) > 1.0) then Exit; // this is
//            SerBop.fa:= RadGra * DATan2(sinphip, Sqrt(1 - Sqr(sinphip))); // latit.
//            tc:= SerBop.ta + dt;              // to continue to find rise
//            br:= (ToGetPerpH(tc) > 0.0); // case for rise
//            while (not br ) and(tc < fmSelEph.FinishDate)  do
//                begin
//                  tc:= tc + dt;
//            br:= (ToGetPerpH(tc) > 0.0); // case for rise
//            end;
//            if (not br )  then Exit; // no events in this case
//                                     // { to find moment for rise event between two moments }
//            SerBop.tb:= ToGetRisM(tc, dt); // there is rise event tb moment
//                                           // { for pair satellite satellite may be strange case }
//            if (Abs(ToGetPerpH(SerBop.tb)) > 1.0) then Exit; // this is
//            SerBop.fb:= RadGra * DATan2(sinphip, Sqrt(1 - Sqr(sinphip))); // latit.
//            boevent:= True;  // there are set and rise
//        }

//        //{ a try to find some events
//        //  when selected body is beyond the Earth }

//        public void PosEvent()
//        {
//            double tt:= FirstMoment; // any moment close to fmSelEph.StartDate
//            repeat                              // set and rise in day
//  CurEvent(tt); // to SerBop two moments ta tb two latitude fa fb
//            if (boevent)
//                // result is in record SerBop
//                fmSatEph.BinStream.Write(SerBop, 40); //
//            switch (CharBodEph)  // 'P' or 'S'    the next moment to find
//            {
//                case 'P': tt:= SerBop.ta + 0.90 * fmSelEph.SatPeriod; // planet case
//                case 'S': tt:= SerBop.tb + 0.01 * fmSelEph.SatPeriod; // satellite case
//            } // case CharBodEph 'P' or 'S'
//            until(not boevent) or(tt > fmSelEph.FinishDate);
//        }

//        public void CapEphBody() // some usefull words about strings
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin


//               Add(' из центра Земли опущен перпендикуляр');
//            Add(' на линию, соединяющую спутник и небесное тело');
//            Add(' точка P - точка пересечения перпендикуляра и этой линии');
//            Add('  ');
//            Add(' год      I4');
//            Add(' месяц    I3');
//            Add(' день     I3');
//            Add(' часть дня , секунды  F8.1');
//            Add(' положение точки P  в земной системе отсчёта :');
//            Add(' расстояние точки   P   от поверхности Земли (метры) F12.1');
//            Add(' широта  проекции точки на поверхность Земли (град.)  F9.3');
//            Add(' долгота проекции точки на поверхность Земли (град.)  F9.3');
//            Add(' положение спутника в земной системе отсчёта :');
//            Add(' расстояние спутника    от поверхности Земли  (км)   F11.3');
//            Add(' широта  проекции спут. на поверхность Земли (град.)  F9.3');
//            Add(' долгота проекции спут. на поверхность Земли (град.)  F9.3');
//            Add(' небесное тело в орбитальной системе отсчёта :');
//            Add(' зенитное расстояние объекта                 (град.)  F9.3');
//            Add(' азимут объекта в орбитальной системе        (град.)  F9.3');
//            Add(' для Солнца, Луны и планет дана величина :');
//            Add(' проекция диаметра объекта на перпендикуляр   (км)    F8.3');
//            Add(' для Луны, планет и спутников дана величина :');
//            Add(' фаза, как мера освещённости объекта Солнцем (б.р.)   F7.3');
//            Add(' для планет Солнечной системы');
//            Add(' приводится звёздная величина объекта по формату      F5.1');
//            Add('  ');
//            end;
//        }

//        //{ long of event from ta set to tb rise in second of time }

//        public void SetRisL(double ta, double tb)
//        {
//            double dt;    // difference in second of time
//            dtring st; // string for difference
//            dt:= 86400.0 * (tb - ta); // difference in second of time
//            Str(dt: 18:1, st);
//            with fmSatEph.MemoEph.Lines  do
//                begin


//                Add('  ');
//            Add(st + '  продолжительность события в секундах');
//            end;
//        }

//        //{ phase magnitude information if it is possible
//        //  to use procedure ClcPlanetMagnitude from UnForPos
//        //     and procedure ToGetSatPhase      from UnForSat }

//        public string StrPhaM(double tc, vec3 zp)
//        { // posbods body position refer to the satellite
//            int i;
//            int np;        // number of the planet
//            vec3 rs;      // the Sun position fixed equator
//            vec3 ps;      // the Sun position true  equator
//            int vm;      // magnitude
//            int pv;      // phase
//            double ph;    // phase
//            string sp; // our string for phase information
//            string sv; // our string for magnitude
//            StrPhaM:= '';                   // default no information
//            PosGeoPlanet(NSun, tc, rs);      // the Sun UnForPos fixed equator
//            MatrMultVector(rotmatu, rs, ps); // from fixed to true equator
//            for i:= 1 to 3 do               // the Sun position true equa
//                    ps[i]:= ps[i] - zp[i];          // refer to the satellite
//            pv:= 1.;// default
//            vm:= 99; // default
//            sv:= ''; // default
//            switch (CharBodEph) // 'P' or 'S'
//            {
//                case 'P':
//                    np:= NumberBody; // planet number
//                    if (np /*in*/ [NEarth, NSun]) return; // no
//                    ClcPlanetMagnitude(np, posbods, ps, vm, ph); // UnForPos
//                    pv:= ph; // phase
//                             // function ToGetSatPhase from UnForSat
//                case 'S': pv:= ToGetSatPhase(posbods, ps); // body Sun refer to satellite
//            } // case CharBodEph 'P' or 'S'
//            Str(pv: 7:3, sp); // phase to string
//            if (vm < 25.0)
//                Str(vm: 5:1, sv);
//            StrPhaM:= sp + sv;
//        }

//        //{ to add information to fmSatEph.MemoEph
//        //  procedure ToObtainOritalMatr from UnitElmK }

//        public void SetRisO(int nc, double tc, double hp)
//        {      // to prevent strange result for satellite - satellite
//            vec3 rr;      // vector  rectangular
//            double al;    // longitude in degree
//            double ap;    // latitude  in degree
//            double zr;    // range  in kilometre
//            vec3 zp;      // satellite position
//            vec3 zv;      // satellite velosity
//            mat3 om;     // satellite orbital matrix
//            string sh; // string for height
//            string st; // string for print
//            string sp; // string for phase

//            if (Abs(hp) > fmSelEph.LatMinVal) then Exit; // no inform
//            st:= ToGetDateSecP(tc); // UnForDat date with part of day in sec
//            Str(1.0e3 * hp:12:1, sh);  // height to string in metr
//            st:= st + sh;             // our string date and height
//            MatrMultVector(rosmatu, perpvec, rr); // to rotating system
//            ClcSpherCoorInDegree(rr, al, ap, zr);  // perpendicular spheric
//            Str(ap: 9:3, sh); // latitude of perpendicular in degree
//            st:= st + sh; // latitude of perpendicular in degree to string
//            Str(al: 9:3, sh); // longitude of perpendicular in degree
//            st:= st + sh; // longitude of perpendicular in degree to string
//            EilXV(tc, fmSelEph.SatElemEph, zp, zv); // satellite to centre
//            MatrMultVector(rosmatu, zp, rr); // to rotating system
//            ClcSpherCoorInDegree(rr, al, ap, zr);  // satellite spheric
//            Str(zr: 11:3, sh); // satellite range from the Earth's centre
//            st:= st + sh; // satellite range from the Earth's centre to string
//            Str(ap: 9:3, sh); // latitude of satellite in degree
//            st:= st + sh; // latitude of sateliite in degree to string
//            Str(al: 9:3, sh); // longitude of satellite in degree
//            st:= st + sh; // longitude of satellite in degree to string
//            ToObtainOrbitalMatr(zp, zv, om); // for sat orbital matrix UnitEilP
//            MatrMultVector(om, posbods, rr); // body to orbital system UnForFun
//            ClcOrbCoorInDegree(rr, al, ap, zr); // azimuth zenith distance range
//            Str(ap: 9:3, sh); // body zenith distance in degree orbital system
//            st:= st + sh; // body zenith distance in degree to string
//            Str(al: 9:3, sh); // body azimuth in degree orbital system
//            st:= st + sh; // body azimuth in degree to string
//            if  CharBodEph = 'P' // NumberBody for the planet
//    then // to print projection of diameter to perpendicular
//      begin                  // only for planet
//        Str(planetd: 8:3, sh); // projection in km
//            st:= st + sh;           // to our common string
//            end;
//            sp:= StrPhaM(tc, zp); // phase magnitude information to string
//            st:= st + sp; // plus phase magnitude information
//            if  nc > 0
//              then
//      fmSatEph.MemoEph.Lines.Insert(nc, st) // result to memo
//    else
//      fmSatEph.MemoEph.Lines.Add(st); // result to memo
//        }

//        //{ information about some points around set  event }

//        public void SetInfo(double ta)// set  event
//        {
//            int ic;
//            int nc;     // count for MemoEph.Lines
//            double tc;    // current moment
//            double hp;    // current height in km above surface

//            fmSatEph.MemoEph.Lines.Add('  ');
//            nc:= fmSatEph.MemoEph.Lines.Count; // number of lines
//            ic:= 0;                     // simple count for print
//            tc:= ta;                    // start moment set moment
//            repeat                       // backward from start moment
//              hp:= ToGetPerpH(tc);        // height of perpendicular in km
//            SetRisO(nc, tc, hp); // to add information to fmSatEph.MemoEph
//            if (fmSelEph.BooVisEph) return; // one moment only
//            ic:= ic + 1;
//            tc:= tc - fmSelEph.StepPrint; // the next step backward
//            until(ic = 20) or(hp > fmSelEph.LatMinVal);
//            // { some points for heght less than nullo }
//            ic:= 0;                     // simple count for print
//            tc:= ta + fmSelEph.StepPrint; // the next step forward
//            repeat                       // backward from start moment
//              hp:= ToGetPerpH(tc);        // height of perpendicular in km
//            SetRisO(0, tc, hp); // to add information to fmSatEph.MemoEph
//            ic:= ic + 1;
//            tc:= tc + fmSelEph.StepPrint; // the next step backward
//            until(ic = 10);
//        }

//        //{ information about some points around rise event }

//        public void RisInfo(double tb)// rise event
//{
// int ic  ;
// int nc  ;     // count for MemoEph.Lines
// double tc;    // current moment
//            double hp ;    // current height in km above surface

//  nc:=fmSatEph.MemoEph.Lines.Count; // number of lines
//  ic:=0;                     // simple count for print
//  tc:=tb;                    // start moment set moment
//repeat                       // backward from start moment
//  hp:=ToGetPerpH(tc);        // height of perpendicular in km
//  SetRisO(nc, tc, hp); // to add information to fmSatEph.MemoEph
//  if ( fmSelEph.BooVisEph )  return ; // one moment only
//  ic:=ic+1;
//  tc:=tc-fmSelEph.StepPrint; // the next step backward
//until(ic = 10);
// // { some points for heght less than nullo }
//    ic:=0;                     // simple count for print
//  tc:=tb+fmSelEph.StepPrint; // the next step forward            
//repeat                       // backward from start moment
//  hp:=ToGetPerpH(tc);        // height of perpendicular in km
//  SetRisO(0, tc, hp); // to add information to fmSatEph.MemoEph
//    ic:=ic+1;
//  tc:=tc+fmSelEph.StepPrint; // the next step backward
//until(ic = 20) or(hp > fmSelEph.LatMinVal );
//}

//        public void PrtEvent() // result ro memo
//        {
//            fmSatEph.BinStream.Seek(0, soFromBeginning);
//            fmSatEph.NumPoint:= 0; // number of ephemeris points
//            CapEphBody; // some usefull words about strings
//            while  fmSatEph.BinStream.Position < fmSatEph.BinStream.Size  do
//                    begin
            
//                  fmSatEph.BinStream.Read(SerBop, 40); // set and rise event
//            Inc(fmSatEph.NumPoint);
//            with SerBop  do
//                begin
            
//                      SetRisL(ta, tb); // long of eclipse in second
//            SetInfo(ta); // some points around set  event
//            RisInfo(tb); // some points around rise event
//            end;
//            end;
//        }

//        public void CapSunCase() // some usefull words about strings
//        {
//            with fmSatEph.MemoEph.Lines  do
//                begin
           
//               Add('  ');
//            Add(' несколько точек для наблюдений лимба');
//            Add(' от момента захода Солнца до момета восхода');
//            Add('  ');
//            Add(' год      I4');
//            Add(' месяц    I3');
//            Add(' день     I3');
//            Add(' часть дня , секунды  F8.1');
//            Add(' положение точки P  в земной системе отсчёта :');
//            Add(' расстояние точки   P   от поверхности Земли (метры) F12.1');
//            Add(' широта  проекции точки на поверхность Земли (град.)  F9.3');
//            Add(' долгота проекции точки на поверхность Земли (град.)  F9.3');
//            Add(' положение спутника в земной системе отсчёта :');
//            Add(' расстояние спутника    от поверхности Земли  (км)   F11.3');
//            Add(' широта  проекции спут. на поверхность Земли (град.)  F9.3');
//            Add(' долгота проекции спут. на поверхность Земли (град.)  F9.3');
//            Add(' точка лимба в орбитальной системе отсчёта :');
//            Add(' зенитное расстояние точки лимба             (град.)  F9.3');
//            Add(' азимут точки лимба в орбитальной системе    (град.)  F9.3');
//            Add('  ');
//            end;
//        }

//        //{ CharForEphem = 'O' body from satellite
//        //  proc is called from UnSatEph proc PassToMemo }

//        public void ToFindEvents()    // called by fmSatEph.FormActivate
//        {                     // information to fmSatEph.MemoEph
//            fmSatEph.BinStream.Clear; // Classes UnSatEph
//            PosEvent(); // when selected body is beyond the Earth
//            PrtEvent(); // to print and graph result
//        }

//    }
}
