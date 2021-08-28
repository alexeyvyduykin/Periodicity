using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace PRDCT.Core.TEST
{
//    public static class Consts
//    {
//   public static double JD2000   = 2451545.0 ;
//        public static double JulianC =   36525.0 ;
//        public static double DifEpoch = 2400000.5 ;

//        public static double GaussConst = 0.0172020989500000e0;
//        // { in AstrUnit**3/((Ephemer.Day**2)*MassOfSun if in Sqr}
//        public static double CavendishConst   = 6.672e-20 ;   //  { in km**3/(kg* s**2) }
//        public static double AstrUnit = 1.49597870691e8 ;  //{ in km }
//        public static double VelOfLight       = 299792.4580 ;  // { in km/s }

//        public static double GeoFM = 3.98600448e5;// { km**3/s**2 }
//        public static double GeoR0    = 6.37814000e3 ;// { km }
//        public static double ParmA    = 1.0/298.257 ; // oblateness of the Earth
//        public static double VelOfRot = 0.7292115e-4 ; // radian per second Earth rotation velosity

//        public static double PiTwo = Math.PI / 2;
//        public static double HalfPi = PiTwo ;
//        public static double Pi22     = 2 * Math.PI;
//        public static double TwoPi = Pi22 ;
//        public static double GraRad   = Math.PI / 180 ;
//        public static double RadGra   = 180 / Math.PI;
//        public static double SecRad   = GraRad / 3600 ;
//        public static double RadSec   = 3600 / GraRad ;
// Type
//   TVect3 = Array[1..3] of Extended;
//TMatr33   = Array[1..3] of TVect3 ;
//   TFundArg  = Array[1..14] of Extended ;
//   TypeDim3  = TVect3 ;
//   TypeDim33 = TMatr33 ;

//  public static string[] StrDayWeek =  new string[]        // to UnForTim
//               { 'понедельник' , 'вторник' , 'среда' ,
//                 'четверг' , 'пятница' , 'суббота' ,
//                 'воскресенье' } ;
//        public static string[]  StrMonth = new string[] {
//                "январь"  ,  "февраль" ,  "март" ,
//                 'апрель'  ,  'май'     ,  'июнь' ,
//                 'июль'    ,  'август'  ,  'сентябрь' ,
//                 'октябрь' ,  'ноябрь'  ,  'декабрь' };

//    }

//    public class UnCoTVar
//    {

//       int MaxStarInHeap     = 23777 ;
//   int MaxObjectInHeap   = 23777 ;
//   int MaxSatElemInHeap  = 23777 ;
            
//            public struct RecStarCatType
//        {
//            int id;
//            int ra, sd; //{ alpha southdelta in mas }
//            int cea, ced;    //{ difference in epoch 0.01JY }
//            int sea, sed;    //{ units 0.1 mas error }
//            int pma, pmd; //{ 0.1 mas/Year }
//            int sema, semd;    //{ 0.1 mas/Year error }
//            int bt; //{ B photometry 0.001 m }
//            int sbt;    //{ SE B photometry 0.001 m }
//            int vt; //{ V photometry 0.001 m }
//            int svt;    //{ SE V photometry 0.001 m }
//            int flags;
//        }

//        //{ for only bright stars catalogue }
//        public struct RecStarAddType
//        {
//            int id;
//            int FK5num;
//            int ra, sd; //{ alpha southdelta in mas }
//            int pma, pmd; //{ proper motion 0.01 mas/Year }
//            int bt, vt; //{ B V photometry 0.001 m }

//        }

//  // for the stars catalogue positions
//  public struct TStarOneRec
//        {
//            int TycNumber;
//            int TrcNumber;
//            int HipNumber;
//            int FK5Number;
//            double Alpha;
//            double Delta;
//            double AlphaMotion;
//            double DeltaMotion;
//            double Bt, Vt;
//        }
//        // for all objects positions in the screen
//        public struct TObjectTopRec
//        {
//            int nall;
//            char chps; // * P or S
//            string name;
//            double Azt, Alt, Rot; // degree degree km
//            double Mag, phase;
//            byte IndexShadow;
//            bool boovis;
//            bool booscreen;
//            int xpos, ypos;
//        }

//        // for the Earth satellites indentification and middle elements
//        public struct TElemRec
//        {
//            public int satnord;  // number from NORAD
//            public int satnuml;  // number launch date
//            public string satname;
//            public double stdmag;   // standard magnitude
//            public double t, ao, au, ai, ae, am, an, dn;
//        }

   
//   TStarAllHeap = Array[1..MaxStarInHeap] of TStarOneRec;
//TStarAllHeapPtr   = ^TStarAllHeap ;
//   TObjectTopHeap    = Array[1..MaxObjectInHeap] of TObjectTopRec ;
//   TObjectTopHeapPtr = ^TObjectTopHeap ;
//   TSatElemHeap      = Array[1..MaxSatElemInHeap] of TElemRec ;
//   TSatElemHeapPtr   = ^TSatElemHeap ;
//   TObjectSort       = Array[1..MaxObjectInHeap] of LongInt ;
//   TObjectSortPtr    = ^TObjectSort ;

// //Var { heap for number , position , proper motion and magnutude }
//   StarOneCur       : TStarOneRec ; // current record
//   StarOneFix       : TStarOneRec ; // record for star that chosen to fix
//   StarAllHeap      : TStarAllHeapPtr ;
//   ObjectTopCur     : TObjectTopRec ;
//   ObjectTopHeap    : TObjectTopHeapPtr ;
//   ObjectSort       : TObjectSortPtr ;   // to UnitSort and UnSelSat
//   IntDimForOrder   : TObjectSortPtr ;   // to UnitSort and UnSelSat
//   PlanetNumFix     : LongInt ;   // number of planet that chosen to fix
//   SatElemCur       : TElemRec ; // current record for satellite
//   SatElemFix       : TElemRec ; // record for satellite that chosen to fix
//   SatElemLook      : TElemRec ; // record for satellite for looking 'L'
//   SatElemHeap      : TSatElemHeapPtr ; // heap for satellites elements
//   NumObjectInHeap  : LongInt ;
//   NumFirstSatHeap  : LongInt ;
//   NumSatElemInHeap : LongInt ;
//   NumBrStarsInHeap : LongInt ;
//   StrNameTychoCat  : ShortString ;

//    }

//    public static class Global
//    {


//        //{ for the different coordinates system }
//        public struct TAllSystemRec
//        {
//            double ro;
//            double ah, hh; //{ horizontal}
//            double aa, da; //{ equator }
//            double le, be; //{ ecliptic }
//            double lg, bg;
//        }

//        public static dmat3 MatrUnit = new dmat3(
//    new dvec3(1.0, 0.0, 0.0),
//    new dvec3(0.0, 1.0, 0.0),
//    new dvec3(0.0, 0.0, 1.0));
//        // { to change from galactic to equatorial system from HIP v.1,p.92 }
//        public static dmat3 MatrGal = new dmat3
//                  (new dvec3(-0.0548755604, +0.4941094279, -0.8676661490),
//                    new dvec3(-0.8734370902, -0.4448296300, -0.1980763734),
//                    new dvec3(-0.4838350155, +0.7469822445, +0.4559837762));

//        public static double[] TypeFundArg = new double[5];

//        public struct
//          TPlaceCooRec
//        {
//            public int num;
//            public string name;
//            public double x, y, z; // position in metr
//            public double f, l, h; // lat long degree h metr
//            public double c, d, s; // hour : zone decret summer
//        }


//        public struct TEclipseRec
//        {
//            vec3 centreshad;
//            double fullshadow;
//            double semishadow;
//        }


//        int NumberOfStars;
//        int NumStarsInRect;
//        int SmallStarSize;


//        public bool BooAddInform;
//        public bool BooNegative;
//        public bool BooReticulum;
//        public static bool BooFixObject;
//        public bool BooPlanetLook;
//        public static bool BooApp,
//   BooApprox,
//   BooRiSetP,
//   BooSatElem,
//   BooExistSat,
//   BooDayNight,
//   BooHourImage,
//   BooRefraction,
//   BooAutoControl;


//        public bool BooRusLat,
//   BooAzNorthS,
//   BooAlphaHourA;
//        public char CharDayNight,
//   CharDayNightMem;// { D - day , T - twilight , N - night }
//        public string StrDayNight,
//   StrDayNightMem;
//        public string StrRefSystem;
//        public double TheSunHight; //{ the hight of the sun under horizon }
//        public string StrTheSunHight;
//        public char CharReferSystem,
//   CharRefSysteMem,
//   CharTimeSystem;
//        public double DecreTMoscow;
//        public double SummerHour; //{ for one hour jump of time if exists }
//        public double StepWithTime;
//        public double SizeForArea;


//        public static double JulianDate; // the main variable for time
//        public static double LocalSTime; // local sidereal time
//        public double DHourImage;
//        public static dvec3 StationPos; //{ in the true equatorial system }
//        public static dmat3 MatrEcl; // from fixed equator to ecliptic
//        public static dmat3 PrecMatr; // precession matrix
//        public static dmat3 MatrNut; // nutation matrix
//        public static dmat3 RotMatr; // from fixed to true equator
//        public static mat3 TopMatr; // from true to horizon or orbital
//        public static mat3 RosMatr; // true sidereal time matrix
//        public static TPlaceCooRec PlaceCoor; // information about station 
//        public static double RinDegC; // general radius of field of view
//        public double XinDegC, YinDegC; // current scale for X Y axis
//        public static double AzimutC; // horizon degree centre image
//        public static double AltitudeC; // horizon degree centre image
//        public vec3 PosHorizoC;   // centre horizon  but descart
//        public double AlphaC, DeltaC; // equator degree centre image
//        public vec3 PosZemelaC;   // orbital centre of the Earth
//        public double AngleZemeL; // 'L' the Earth from satellite
//        public double SurfaRange; // special range from satellite
//        public double DecretTime;
//        public double DCivilTime;
//        public bool BooSummerJ;  // summer hour yes or no

//        // is called PUnCRead UnitMain 
//        public Single VMagMin, VMagMax;   // to limit stars magnitude
//        public Single VAllMin, VAllMax;   // stars magnitude

//        public TEclipseRec VEclipse;
//        public bool BooCircle;


//        public static char CharForView; // 'S' for sky 'W' for world map and 'L'
//        public static char CharForEphem; // 'P' for point 'O' for object
//        public char CharBodEph; // 'S' satellite 'P' planet as object
//        public int NumberBody; // number of choosed planet in case 'O'
//        public char chElemFormat; // 'N' NORAD 'I' IRVS 'K' KDr 'M' no
//        public string stElementFile; // name of file with elements
//        public string stCatalogFile; // name of catalogue
//        public string stPathCat; // for path to folder with catalogue
//        public string stPathElem; // for path to folder with elements
//        public string stPathResult; // for path to folder for result
//        public string stPathDefault; // for path to folder with program
//        public string stPathSite; // for path to folder with site position

//        Const
//          MaxNameValue = 97; // to select step , area size and others

//        public struct TNameValue
//        {
//            string strname;
//            double valname;
//        }

//        public int NumNameValue; // current maximum number
//        public int NumStatValue; // current number in list station
//        public int NumStepValue; // current number in list of time step
//        public int NumSizeValue; // current number in list of area size
//        public List<TNameValue> NameValue = new List<TNameValue>();

//        // variable for map world
//        public static byte MapSatNumber;  // less equal 8 and 2 for the Sun the Moon
//        public vec3[] MapSatGeoPos = new vec3[10];   // for position
//        public double[] MapSatAltitude = new double[10]; // for altitude in degree
//        public byte[] MapSatShadow = new byte[10];     // o 1 2
//        public int[] MapSatX, MapSatY = new int[10];  // for position of imagesat
//        public double[] RangeSatMap = new double[10]; // UnGraAll for range 'W'
//        public TElemRec[] SatElemMap = new TElemRec[10]; // elements
//        public static bool BooMapPoint;

//        public int xpMouse;
//        public int ypMouse;


//        public bool boomaif; //{ false if fmPuz not active }
//    }



//    public  class UnitStat
//    {

//        public int MaxStationL = 255;

//        public int NumStationL;
//        public List<TPlaceCooRec> StationList; // type from UnGloVar
//        public void ToAddStationList(string sf) // from new file
//        {
//            string ft;// : TextFile ;
//            int no, nt;
//            double rf, rl, rh; // altitude longitude height
//            double rx, ry, rz; // geocentric position in km
//            int nz, nd, ns;  // hour zone , decret hour , summer hour
//            string st; // name
//            char ch; // index for input parameters
//            no:= 0; // site number
//            nz:= 0; // hour zone
//            nd:= 0; // decret hour
//            ns:= 0; // summer hour
//            AssignFile(ft, sf); // new file
//                                // {$I -}
//                                //  ReSet(ft);
//                                // {$I +}
//            if (IOResult == 0)
//            // file exists
//            {
//                Read(ft, ch); // the first string with cap information
//                if (ch != 'i')
//                // error in file may be other file
//                {
//                    CloseFile(ft);
//                    ShowMessage('ошибка формата'); // Dialogs
//                    return;
//                }
//                ReadLn(ft); // the first string with cap information
//                nt:= NumStationL; // there is some stations in list
//                while (!EOF(ft))
//                {

//                    Read(ft, ch);
//                    switch (ch)
//                    {
//                        case 'r':  // x y z position in metre
//                            ReadLn(ft, st, no, rx, ry, rz, nz, nd, ns); // no site number
//                            GeoSpherFromCart(rx, ry, rz, rf, rl, rh); // UnforCoo degree
//                            nt:= nt + 1; // the next point

//                        case 'z':  // decimal point phi lambda degree h in metre
//                            ReadLn(ft, st, no, rf, rl, rh, nz, nd, ns); // no site number
//                            GeoCartFromSpher(rf, rl, rh, rx, ry, rz); // UnForCoo metre
//                            nt:= nt + 1; // the next point             
//                        default: ReadLn(ft);
//                    } // case ch - character that input may be r z d h
//                    if (nt > MaxStationL) nt = MaxStationL;
//                    if (ch/* in*/ ['r', 'z'])

//                        while (StationList[nt])
//                        {
//                            num:= no;
//                            name:= st;
//                            x:= rx; // x position in metre
//                            y:= ry; // y position in metre
//                            z:= rz; // z position in metre
//                            f:= rf; // geodetic latitude in degree
//                            l:= rl; // geodetic longitude in degree
//                            h:= rh; // geodetic height in meter
//                            c:= nz; // hour zone for civil time
//                            d:= nd; // decret hour
//                            s:= ns; // summer hour
//                        }


//                }
//                CloseFile(ft);
//                NumStationL:= nt;

//            }

//        }
//        public
//            void StationForView()
//        {
//            TimeVarInit(1);  // from UnGetIni
//            CentreCoorH;  // from UnGetIni
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//        }

//        public void ShowModalStationList()
//        {
//            int n;
//            fmSelHol.ListBox.Clear; // station list to ListBox1
//            for n:= 1 to NumStationL do // var from UnitStat
//                    fmSelHol.ListBox.Items.Add('   ' + StationList[n].name);

//             fmSelHol.ListBox.ItemIndex:= NumStatValue - 1; // station number in list
//            fmSelHol.ListBox.Columns:= 3;
//            fmSelHol.ShowModal; // to select one station from the list
//        }

//        public void ToSelectOneStation() // to choose station from the list
//        { // but list from the file statfira.txt
//            fmSelHol.CapSelHol:= 'Пункт наблюдений.'; // form from UnSelHol
//            fmSelHol.ButtonAdd.Enabled:= True;
//            fmSelHol.ButtonAdd.Visible:= True;
//            fmSelFel.BooSelFel:= False;
//            fmSelHol.AddSelHol:= 1;
//            ShowModalStationList; // NumStationL greater or equal 1 default
//            fmSelHol.ButtonAdd.Enabled:= False;
//            fmSelHol.ButtonAdd.Visible:= False;
//            if  fmSelHol.BoaSelHol // ButtonAdd is clicked
//    then
//      begin
//        fmSelFel.FileElemBox.Mask:= '*.txt';    // form UnSelFel
//            fmSelFel.Caption:= ' Выбор. Наборы пунктов.';
//            fmSelFel.stPathFel:= stPathSite; // folder with stations files
//            fmSelFel.BinSelFel:= False; // to select text but not binary file
//            fmSelFel.ButtonFol.Enabled:= True;
//            fmSelFel.ButtonFol.Visible:= True;
//            fmSelFel.FileElemBox.ApplyFilePath(fmSelFel.stPathFel);
//            if  fmSelFel.FileElemBox.Items.Count > 0
//                  then // files exist
//            fmSelFel.ShowModal  // form UnSelFel to select file
//          else // to select other folder
//            fmSelFel.BofSelFel:= True;
//            fmSelFel.ButtonFol.Enabled:= False;
//            fmSelFel.ButtonFol.Visible:= False;
//            if  fmSelFel.BofSelFel
//                  then
//            begin
//              fmPuz.MenuEphCooClick(fmSelFel.SenderFel);
//            if  not fmSelFol.BooSelectFolder then Exit; //  fmSelFol
//            fmSelFel.BooSelFel:= False;
//            fmSelFel.stPathFel:= stPathSite;
//            fmSelFel.FileElemBox.ApplyFilePath(fmSelFel.stPathFel);
//            if  fmSelFel.FileElemBox.Items.Count > 0
//                        then // files exist
//                  fmSelFel.ShowModal
//                else
//                  ShowMessage('в папке ' + stPathSite + ' нет искомых файлов');
//            end;
//            if  fmSelFel.BooSelFel
//                  then
//            begin
//              ToAddStationList(fmSelFel.StrSelFel); // from UnitStat
//            ShowModalStationList;
//            end;
//            end;
//        }

//        public void StationFromMap(char cn, double rf, double rl, TPlaceCooRec pc)  // from UnClaMap
//        {  // information to pc of TPlaceCooRec var from UnGloVar
//            double q;
//            fmSatMap.SatMapC:= cn;
//            if  cn = 'N'
//              then
//      fmSatMap.ShowModal;
//            with pc  do
//                begin
//                  num:= 0;     // default
//            name:= fmSatMap.SatMapN;
//            f:= rf;      // latitude in degree
//            l:= rl;      // longitude in degree
//            h:= 0.0;     // height in meter
//            GeoCartFromSpher(f, l, h, x, y, z); // UnForCoo descart position in meter
//            q:= (rl + 15.0) / 15.0; // longitude in hour
//            if  q > 12.0  then q:= q - 24.0;
//            c:= Trunc(q);       // hour zone default
//            d:= 0;       // hour decret default
//            s:= 0;       // hour summer default
//            end;
//            end;

//            function ToGetStrForSito(pc  : TPlaceCooRec ) : ShortString;
//            var
//              st  : ShortString;
//            begin
//              st:= '  ' + IntegerToFixStr(pc.num, 4)
//                 + '    ' + FloatToFixStr(pc.x, 11, 3)
//                 + '    ' + FloatToFixStr(pc.y, 11, 3)
//                 + '    ' + FloatToFixStr(pc.z, 11, 3);
//            st:= st + '   ' + pc.name;
//            ToGetStrForSito:= st;
//        }

//        public string ToGetStrForSitu(TPlaceCooRec pc)
//        {
//            int nh, nm, kd, km;
//            double aa, ab;
//            string sa, st;
//            PrHourAngle(GraRad * pc.l, sa, nh, nm, aa); // geodetic longitude
//            PrDegrAngle(GraRad * pc.f, sa, kd, km, ab); // geodetic latitude
//            st:= '  ' + IntegerToFixStr(pc.num, 4)
//                 + '  ' + IntegerToFixStr(nh, 2)
//                 + ' ' + IntegerToFixStr(nm, 2)
//                 + ' ' + FloatToFixStr(aa, 7, 4);
//            st:= st + '  '
//                 + sa + IntegerToFixStr(kd, 2)
//                 + ' ' + IntegerToFixStr(km, 2)
//                 + ' ' + FloatToFixStr(ab, 6, 3)
//                 + '      ' + FloatToFixStr(pc.h, 9, 3);
//            st:= st + '   ' + pc.name;
//            ToGetStrForSitu:= st;
//        }

//    }

//    public class UnGetIni // initial for figure
//    {
//        bool CatalogueExist()
//        {
//            VAllMin = +25; VAllMax = -25;
//            VMagMin = -5; VMagMax = 25;
//            if (ExistBrFile) // boolean variable from unit PUnCRead

//            {
//                RinDegC = 15; // in degree for full field of view UnGloVar
//                BrightStarsOnly = true; //  bright stars catalogue only for demo
//                return true;
//            }
//            else if (ExistTRCFile)
//            {
//                RinDegC = 1.5; // in degree for small field of view  UnGloVar
//                BrightStarsOnly = false; // boolean variable from unit PUnCRead
//                return true;
//            }
//            else
//                // { there is no catalogues }
//                return false;
//        }

//        void SomeInitVar()
//        {
//            WhiteColorOnly = false;
//            BooNegative = false;
//            BooReticulum = false;
//            BooHourImage = false;
//            BooRefraction = true; //{ to refraction take into account }
//            SimpleDataForRefraction; //{ from UnRefrOl }
//            BooAddInform = true; //{ to add information on the screen }
//            BooDayNight = false;
//            BooAzNorthS = true; //{ geodetic azimuth from the north point }
//            BooAlphaHourA = true; //{ right ascension for the equator system }
//            CharDayNight = ' ';
//            CharDayNightMem = ' ';
//            StrDayNight = '';
//            BooPlanetLook = false; //{ no real unit if objects is figured }
//            BooAutoControl = false;
//            CharReferSystem = 'H'; //{ H - horizontal , A - equator }
//            CharRefSysteMem = ' ';
//            StrRefSystem = ' horizon';
//            BooRusLat = true; //{ FALSE for latinian TRUE for russian }
//            CharForView = 'S'; //{ to view sky but 'W' for world map }
//            VMagMax = 25; //{ to limit stars with magnitude }
//            boomoud = false; //{ UnMuDraw to draw by mouse if true and Ctrl }
//            boomaif = true; //{ UnGloVar main form fmPuz is active }
//        }

//        void ForStatInit() // initial data for station default
//        {
//            double rf, rl, rh;
//            double rx = 2886365.206; // cartesian
//            double ry = 2155941.870; // position
//            double rz = 5245817.642; // in metre for station
//            GeoSpherFromCart(rx, ry, rz, rf, rl, rh); // from unit UnForCoo
//            With PlaceCoor  Do
//             Begin
//       num = 1872;
//            name = 'Звенигород';  // default
//            x = rx; // metre
//            y = ry; // metre
//            z = rz; // metre
//            f = rf; // geodetic latitude in degree
//            l = rl; // geodetic longitude in degree
//            h = rh; // geodetic height in metre
//            c = 2;  // hour zone civil time
//            d = 1;  // decret hour
//            s = 1;  // summer hour default for Zvenigorod
//            End;
//            NumStatValue = 1; // number of station in list Zvenigorod default
//            NumSizeValue = 1; // initial number of area size in list
//            NumStationL = 1; // var from UnitStat
//            StationList[NumStationL] = PlaceCoor;
//        }

//        void TimeVarInit(byte istep) // simple initial values
//        {                                     // of some time variables
//            CharTimeSystem = 'U'; //{ U - UTC,M - MDT,C - civil time C.T,D - CDT }
//            DCivilTime = PlaceCoor.c / 24.0;
//            //  { for Moscow civil zone time }
//            DecreTMoscow = DCivilTime + PlaceCoor.d / 24.0; //{ for Moscow decret time }
//            DHourImage = 1.0 / 24.0; //{ one hour earlier }
//            DecretTime = DecreTMoscow;
//            if (BooFixObject or(istep <> 1) ) // BooFixObject from UnGloVar
//     {         // current number of this value in step list
//               // NumStepValue from unit UnitSelH
//                StepWithTime = 1.0 / 24.0; // one hour step default UnGloVar
//                NumStepValue = 18; // current number of this value in step list
//            }
//            SummerHour = PlaceCoor.s; // all these global variables from unit UnGloVar

//            if (PlaceCoor.s == 1)
//                BooSummerJ = true; //{ hour jump for summer time for Russia our Country }
//            else
//                BooSummerJ = false;

//            BooRiSetP = false;
//            InitForFixCase; // chFixObject:=' ' BooFixObject:=False UnitFixO
//        }

//        void CurDateTime() // to get current date and time in julian day
//        {
//            char Day, Month, Year;
//            char Hour, Min, Sec, SecT;
//            DeCodeDate(Date, Year, Month, Day); // from SysUtils for current date
//            int iday = Day;
//            int imonth = Month;
//            int iyear = Year;
//            TransDateToJD(iday, imonth, iyear, JulianDate); // unit UnForDat
//            DeCodeTime(Time, Hour, Min, Sec, SecT); // from SysUtils for current time
//            JulianDate = JulianDate + (Hour + Min / 60.0) / 24.0 - DecretTime; // to London
//            if (BooSummerJ)// one hour jump is possible for this station        
//                if (BooHourJump(JulianDate)) // from unit UnForTim         
//                    JulianDate = JulianDate - SummerHour / 24.0; // one hour jump

//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//        }

//        void CentreCoorH() // for initial azimut altitude
//        {
//            AzimutC = 180; // topocentric horizontal azimut in degree
//            if (PlaceCoor.f > 0)
//                // north hemisphere
//                AltitudeC = 90 - PlaceCoor.f
//             else // south hemisphere
//            {
//                AltitudeC = 90 + PlaceCoor.f;
//                AzimutC = 0.0;
//            }
//            if (AltitudeC < RinDegC - 1)
//                // initial altitude topocentric horizontal in degree
//                AltitudeC = RinDegC - 1;
//        }

//        void EphemInit() // for DE200/LE200 ephemeris
//        { // if  NOT BooExistPos then there is no ephemeris
//            double DeltaTA, TinTDT, TinTDB;
//            BooOpenCoeFile = false;
//            BooExistEph = false;
//            BooExistPos = false;
//            BooCircle = true; // two circles for the Moon eclipse case
//            FromUTCtoTT(JulianDate, TinTDT, DeltaTA); // unit UnForTim in julian day
//            TinTDB = ToGetTEph(TinTDT); // unit UnForTim in julian day
//            ToGetCoeFileName; // from unit PUnDE200
//            ToReadEphemeris(TinTDB);  // from unit PUnDE200
//        }

//        void EphemCurrent() // for DE200/LE200 ephemeris
//        { // if  NOT BooExistPos then there is no ephemeris
//            double DeltaTA, TinTDT, TinTDB;
//            BooCircle = true; // two circles for the Moon eclipse case
//            FromUTCtoTT(JulianDate, TinTDT, DeltaTA); // unit UnForTim in julian day
//            TinTDB = ToGetTEph(TinTDT); // unit UnForTim in julian day
//            ToReadEphemeris(TinTDB);  // from unit PUnDE200
//        }

//        void ForIniC(byte ic) // ic =0 before Form1 is opened
//        {
//            if (!CatalogueExist)
//                return; // no stars catalogue
//            SomeInitVar; // initial values for boolean char and string variables
//            if (ic == 0)
//                ForStatInit; // initial for station position and identification
//            TimeVarInit(ic); // initial values for some time boolean char variables
//            CurDateTime; // to get current date and time in julian day
//            CentreCoorH; // for initial azimut altitude
//            if (ic > 0)
//                // file with ephemeris is opened
//                EphemCurrent
//             else
//            {
//                EphemInit; // to define and open file with the planet ephemeris
//                SatNoradFileFind; // to find file with *.tle in UnitSatF
//            }
//        }
//    }

//    public class TfmSelHol
//    {
//        private int AllSelHol;
//        private string StrSelHol;

//        public string CapSelHol;
//        public bool BooSelHol;
//        public bool BoaSelHol; // true if ButtonAdd is clicked
//        public int NumSelHol;
//        public byte AddSelHol;
//        object SenderHol;

//        TfmSelHol fmSelHol;

//        public void BitSelClick(object sender)
//        {
//            fmSelHol.BooSelHol:= True;
//            Close;
//        }

//        public void BitEscClick(object sender)
//        {
//            fmSelHol.BooSelHol:= False;
//            Close;
//        }

//        private void ToAddStrings()  // to add string to memolist
//        {
//            switch (fmSelHol.AddSelHol)
//            {
//                case 0:
//                    fmSelHol.Memo.Lines.Insert(0, '    ');
//                    fmSelHol.Memo.Lines.Add(fmSelHol.CapSelHol);
//                    fmSelHol.Memo.Lines.Add(fmSelHol.StrSelHol);

//                case 1:  // for station position
//                    string s;
//                    fmSelHol.Memo.Lines.Add(fmSelHol.StrSelHol); // simple name
//                    int n:= fmSelHol.NumSelHol; // number of string in list
//                    Str(StationList[n].f:10:3, s); // latitude in degree
//                    fmSelHol.Memo.Lines.Add('широта  ' + s + ' градусы');
//                    Str(StationList[n].l:10:3, s); // longitude in degree
//                    fmSelHol.Memo.Lines.Add('долгота ' + s + ' градусы');
//                    Str(StationList[n].h:10:1, s); // height in metre
//                    fmSelHol.Memo.Lines.Add('высота  ' + s + ' метр');

//            } // case to add string to memolist
//        }

//        public void FormPaint(object sender)
//        {
//            fmSelHol.Color = clGreen;
//            fmSelHol.Caption = ' Выбор. ' + fmSelHol.CapSelHol;
//            fmSelHol.ListBox.Color = clYellow;
//            fmSelHol.BooSelHol = False;
//            fmSelHol.BoaSelHol = False;
//            int n = fmSelHol.ListBox.ItemIndex + 1;
//            if (n < 1)
//                n:= 1;
//            fmSelHol.NumSelHol = n;
//            fmSelHol.StrSelHol = fmSelHol.ListBox.Items[n - 1];
//            fmSelHol.Memo.Visible = True;
//            fmSelHol.Memo.Color = clLime;
//            fmSelHol.Memo.Clear;
//            fmSelHol.Memo.Lines.Add('Строка номер ' + IntToStr(NumSelHol) +
//                                    '.  Всего ' + IntToStr(AllSelHol) + ' строк');
//            fmSelHol.ToAddStrings;
//        }

//        public void FormActivate(object sender)
//        {
//            fmSelHol.ListBox.Visible:= True;
//            fmSelHol.ListBox.Visible:= True;
//            fmSelHol.AllSelHol:= fmSelHol.ListBox.Items.Count;
//            fmSelHol.BitSel.Visible:= True;
//            fmSelHol.BitEsc.Visible:= True;
//        }

//        public void ListBoxClick(object sender)
//        {
//            fmSelHol.NumSelHol:= fmSelHol.ListBox.ItemIndex + 1;
//            fmSelHol.StrSelHol:= fmSelHol.ListBox.Items[fmSelHol.ListBox.ItemIndex];
//            fmSelHol.Memo.Clear;
//            fmSelHol.Memo.Lines.Add('Строка номер ' + IntToStr(NumSelHol) +
//                                    '.  Всего ' + IntToStr(AllSelHol) + ' строк');
//            fmSelHol.ToAddStrings;
//        }

//        public void ListBoxKeyPress(object sender, char Key)
//        {
//            char ch = UpCase(Key);
//            if (ch /*in*/ [ #27 , 'Q' ])  
//            fmSelHol.BitEscClick(Sender); // to exit
//            if (ch = '13')
//                fmSelHol.BitSelClick(Sender); // to select
//        }

//        public void ListBoxDblClick(object sender)
//        {
//            fmSelHol.BitSelClick(sender);
//        }

//        public void ButtonAddClick(object sender)
//        {
//            fmSelHol.BoaSelHol:= True;
//            fmSelHol.SenderHol:= Sender;
//            Close;
//        }

//        public void ListBoxKeyDown(object sender, int Key, TShiftState Shift)
//        {
//            if (Key = Ord('S')) and(Shift = [ssCtrl])
//             // to save
//      with fmSelHol  do  // procedure from UnGraBmp
//                PuzSaveBmp(ClientWidth, ClientHeight,
//                           Width, Height, 3, 3, Canvas);
//}
//    }


////    public class TfmSatEph
////    {

////        private bool BooGraph;
////        private bool BooPaint;
////        private TCursor mmCursor;  // Controls
////        private TBitMap BmpPaint;  // to keep part of the form fmPuz

////        public bool BooOther;
////        public TMemoryStream BinStream; // Classes
////        public bool BooPoint;
////        public int NumPoint; // number of point

////        //{ part of form fmPuz to BmpPaint of TBitMap and to keep }
////        public void ToGetBmp()  //{ proc is called from UnMuDraw }
////        {
////            dst,sr: TRect;   // from Windows simple rectangles
////            BmpPaint:= TBitMap.Create; // only for this case
////            int xw:= PaintBox.Width;       // to place to PaintBox width
////            int yh:= PaintBox.Height;      // to place to PaintBox height
////            int x1:= xcdraw - xw div 2;      // rectangle in form fmPuz
////            int x2:= xcdraw + xw div 2;      // this rectangle
////            int y1:= ycdraw - yh div 2;      // for copying
////            int y2:= ycdraw + yh div 2;      // to our BitMap
////            BmpPaint.Width:= xw;   // size of our BitMap
////            BmpPaint.Height:= yh;  // it is limited by PaintBox component
////            sr.Left:= x1;          // rectangle
////            sr.Top:= y1;           // of the source
////            sr.Right:= x2;         // for por BitMap
////            sr.Bottom:= y2;        // at the form fmPuz
////            dst.Left:= 0;                   // rectangle
////            dst.Top:= 0;                    // for the Canvas
////            dst.Right:= BmpPaint.Width;     // on our BitMap
////            dst.Bottom:= BmpPaint.Height;   // selectad rectangle to BitMap
////            BmpPaint.Canvas.CopyRect(dst, fmPuz.Canvas, sr); { Graphic method }
////        }

////        //{ simple image to fmSatEph.PaintBox }
////        public void AfterDrawBmp()
////        { //{ this rectangle was in the form fmPuz }
////            fmSatEph.PaintBox.Canvas.Draw(0, 0, BmpPaint);
////        } //{ try with bitmap it is selected by proc ToGetBmp }

////        public void ForDiagram() // graphical presentation
////        { // 'P' satellite from station 'O' body from satellite
////  case  CharForEphem of // UnGloVar
////    'P' : GraS.CulmFigure; // class from UnSatRaf
////            'O' : Gras.EventFigur; // class from UnSatRaf
////            'R' , 'Q' : AfterDrawBmp;    // image to PaintBox
////            end; // case CharForEphem kind of ephemeris
////        }

////        public void ForLabelCap()
////        { // 'P' satellite from station 'O' body from satellite
////  case  CharForEphem of // UnGloVar
////    'P' : begin { a look from the station to the satellite }
////            fmSatEph.Caption:= ' Прогноз положений ИСЗ';
////            fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
////                                    + fmSelEph.SiteCooEph.name + ' ';
////            fmSatEph.lbSatl.Caption:= ' объект наблюдений - '
////                                    + fmSelEph.SatElemEph.satname + ' ';
////            end;
////            'O' : begin { a look from the satellite to the body }
////            fmSatEph.Caption:= ' Прогноз событий';
////            fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
////                                    + fmSelEph.SatElemEph.satname;
////            fmSatEph.lbSatl.Caption:= ' объект наблюдений - '
////                                    + fmSelEph.SiteCooEph.name + ' ';
////            end;
////            'R' : begin { some information about objects in rectangle }
////            fmSatEph.Caption:= ' Объекты в поле зрения';
////            fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
////                                    + PlaceCoor.name + ' '; { UnCoTVar }
////            fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
////                                    + 'звёзды, планеты, ИСЗ ';
////            end;
////            'Q' : begin { some information about objects in rectangle }
////            fmSatEph.Caption:= ' Объекты в поле зрения';
////            fmSatEph.lbSite.Caption:= ' взгляд со спутника - '
////                                    + SatElemLook.satname + ' '; { UnCoTVar }
////            fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
////                                    + 'звёзды, планеты, ИСЗ ';
////            end;
////            'L' : begin { about objects in rectangle around limb }
////            fmSatEph.Caption:= ' Лимб Земли по курсу';
////            fmSatEph.lbSite.Caption:= ' взгляд со спутника - '
////                                    + SatElemLook.satname + ' '; { UnCoTVar }
////            fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
////                                    + 'звёзды, планеты, ИСЗ ';
////            end;
////            end; // case CharForEphem 'P' or 'O' or 'R' or 'Q' or 'L'
////        }

////        public void TitleToMemo()
////        {
////case  CharForEphem of
////          'P' , 'O' : AboutSatellite(fmSelEph.SatElemEph); // UnSatPas
////            'Q' , 'L' : AboutSatellite(SatElemLook); // from UnSatPas
////            end; // CharForEPhem
////case  CharForEphem of
////          'P' , 'R' :
////        with fmSatEph.MemoEph.Lines  do
////                begin
////                    if  CharForEphem = 'R'
////              then
////                        Add(ToGetStrForDate(JulianDate)); // UnForDat
////            Add(ToGetStrForSito(PlaceCoor)); { global var }
////            Add(ToGetStrForSitu(PlaceCoor)); { UnCoTVar }
////            end;
////            'O' : with fmSatEph.MemoEph.Lines  do
////                begin // for 'O' may be 'S' satellite 'P' planet

////                      Add('  взгляд со спутника на объект');
////            Add('  поиск событий захода объекта'
////            + ' за край Земли и событий восхода');
////            case  CharBodEph of // UnGloVar 'S' or 'P'
////              'P' : Add('        светило   ' + fmSelEph.SiteCooEph.name);
////            'S' : Add('        спутник   ' + fmSelEph.SiteCooEph.name);
////            end; // CharBodEph
////            end;
////            'Q' : with fmSatEph.MemoEph.Lines  do
////                begin


////                      Add(ToGetStrForDate(JulianDate)); // UnForDat
////            MoreAboutSat(JulianDate, SatElemLook);  // UnSatVaz
////            end;
////            end; // case 'P' 'O' 'R' 'Q'
////            fmSatEph.MemoEph.Lines.Add('  ');    // ToGetStr... from UnitStat
////        }

////        public void PassToMemo()
////        {
////            fmSatEph.BinStream:= TMemoryStream.Create; // Classes
////            mmCursor:= fmPuz.Cursor;
////            fmPuz.Cursor:= crHourGlass;
////            fmSatEph.MemoEph.Visible:= False;
////            fmSatEph.MemoEph.Clear;
////            fmSatEph.TitleToMemo;
////  case  CharForEphem of  // UnGloVar
////    'P' : ToFindPassage; // UnSatPas
////            'O' : ToFindEvents; // UnSatVas
////            'R' : AfterDrawSky; // UnMuDraw
////            'Q' : AfterDrawLook; // UnMuDraw
////            'L' : ToLimbCourse; // UnSatVau
////            end; // case 'P' 'O' 'R' 'Q' 'L'
////            if  fmSatEph.NumPoint = 0
////              then
////      begin
////                fmSatEph.MemoEph.Lines.Add('  ');
////        case  CharForEphem of  // UnGloVar
////          'P' : fmSatEph.MemoEph.Lines.Add('  нет видимости');
////            'O' : fmSatEph.MemoEph.Lines.Add('  нет событий');
////            end; // case 'P' or 'O'
////            end;
////            fmSatEph.MemoEph.Lines.Insert(0, '  ');
////            fmPuz.Cursor:= mmCursor;
////        }

////        public void FormActivate(object sender)
////        {
////            fmSatEph.ForLabelCap;
////            fmSatEph.BooGraph:= False; // no diagram but table
////            fmSatEph.BooOther:= False;
////            fmSatEph.ButGraph.Caption:= 'диаграмма';
////            if  CharForEphem = 'L' // course to point of limb
////    then
////      begin
////                fmSatEph.ButGraph.Visible:= False;
////            fmSatEph.ButOther.Visible:= False;
////            end
////            else
////      begin
////                fmSatEph.ButGraph.Visible:= True;
////            fmSatEph.ButOther.Visible:= True;
////            end;
////            fmSatEph.MemoEph.Visible:= True;
////            fmSatEph.MemoEph.SetFocus;
////        }

////        public void ButGraphClick(object sender)
////        {
////            fmSatEph.BooGraph:= not fmSatEph.BooGraph;
////            if  fmSatEph.BooGraph
////              then
////      begin
////                fmSatEph.MemoEph.Visible:= False;
////            fmSatEph.MemoEph.Enabled:= False;
////            fmSatEph.ButGraph.Caption:= 'таблица';
////            fmSatEph.ButGraph.Hint:= 'результаты расчётов';
////            fmSatEph.ForDiagram;
////            fmSatEph.BooPaint:= False;
////            end
////            else
////      begin
////                fmSatEph.MemoEph.Enabled:= True;
////            fmSatEph.MemoEph.Visible:= True;
////            fmSatEph.ButGraph.Caption:= 'диаграмма';
////            fmSatEph.ButGraph.Hint:= 'графическая иллюстрация';
////            end;
////        }

////        public void ButEscClick(object sender)
////        { //{ to close form }
////            fmSatEph.Close;
////        }

////        public void ButOtherClick(object sender)
////        { //{ to close form }
////            fmSatEph.BooOther:= True;
////            fmSatEph.Close;
////        }

////        //{ if new file to write or append
////        //  the contents of MemoEph.Lines sf name of file }
////        public void WriteAppEnd(string sf)
////        {
////            ft: TextFile;   // our file with text
////            ns: Integer;
////            AssignFile(ft, sf); // our text file
////            if  FileExists(sf) // function SysUtils
////    then             // file exists already
////      AppEnd(ft)     // to write to the end of file
////    else
////      ReWrite(ft);   // to open new file
////            for ns:= 0 to fmSatEph.MemoEph.Lines.Count - 1 do
////                    WriteLn(ft, fmSatEph.MemoEph.Lines[ns]);

////               CloseFile(ft);
////        }

////        public void ToSaveGraph()
////        {
////            dst,sr: TRect;   // from Windows
////            TBitMap BitMap:= TBitMap.Create;
////            BitMap.Width:= fmSatEph.ClientWidth - 4;
////            BitMap.Height:= fmSatEph.ButGraph.Top - 5;
////            sr.Left:= 2;
////            sr.Top:= 2;
////            sr.Right:= fmSatEph.ClientWidth - 2;
////            sr.Bottom:= fmSatEph.ButGraph.Top - 3;
////            dst.Left:= 0;
////            dst.Top:= 0;
////            dst.Right:= BitMap.Width;
////            dst.Bottom:= BitMap.Height;
////            BitMap.Canvas.CopyRect(dst, fmSatEph.Canvas, sr);
////            if  fmPuz.SavePictureDialog.Execute
////              then
////        { standard dialog }
////            BitMap.SaveToFile(fmPuz.SavePictureDialog.FileName);
////            BitMap.Free;
////        }

////        public void ButSaveClick(object sender)
////        {
////            if  not BooGraph
////              then // for table
////      if  fmPuz.SaveDialog.Execute
////        then // fmPuz UnitMain
////          fmSatEph.WriteAppEnd(fmPuz.SaveDialog.FileName)
////                else
////    else // for diagram
////      fmSatEph.ToSaveGraph;
////        }

////        public void MemoEphKeyPress(object sender, out char Key)
////        {
////            char ch:= UpCase(Key);
////            if  ch /*in*/ ['Q', #27 ]  then  fmSatEph.ButEscClick(Sender);  // exit Esc
////  if  ch /*in*/ ['S', #13 ]  then  fmSatEph.ButSaveClick(Sender); // save Enter
////  if (ch = 'D') and(CharForEphem <> 'L')
////    then fmSatEph.ButGraphClick(Sender);
////            Key:=#0;
////}

////        public void FormPaint(object sender)
////        {
////            if  BooGraph and BooPaint  then fmSatEph.ForDiagram;
////            BooPaint:= True;
////        }

////        public void FormKeyPress(object sender, out char Key)
////        {
////            char ch:= UpCase(Key);
////            if  ch /*in*/ ['Q', #27 ]  then  fmSatEph.ButEscClick(Sender);  // exit Esc
////  if  ch/* in */['S', #13 ]  then  fmSatEph.ButSaveClick(Sender); // save Enter
////  if (ch = 'D') and(CharForEphem <> 'L')
////    then fmSatEph.ButGraphClick(Sender);
////        }

////        public void FormClose(object sender, out TCloseAction Action)
////        {
////            fmSatEph.BinStream.Free;
////            if  not fmSatEph.MemoEph.Enabled
////              then { this situation may be in diagram case }
////            fmSatEph.MemoEph.Enabled:= True; { always }
////            if  CharForEphem /*in*/ ['R', 'Q']
////        then
////                BmpPaint.Free; { BitMap is created only for select rectangle }
////        }

////        public void FormClick(object sender)
////        {
////            with fmSatEph  do  // procedure from UnGraBmp
////                PuzSaveBmp(ClientWidth, ClientHeight,
////                           Width, Height, 3, 3, Canvas);
////}

////        public void MemoEphKeyDown(object sender, out int Key, TShiftState Shift )
////{
////  if  fmSatEph.BooGraph or(Key = Ord('D'))
////    then Exit; // only for MemoEph component
////  if  ( Key = Ord('A') ) and(Shift = [ssCtrl]) // to select
////    then                                            // all in Memo
////      fmSatEph.MemoEph.SelectAll ;
////  if  ( Key = Ord('C') ) and(Shift = [ssCtrl]) // to
////    then                                            // ClipBoard
////      fmSatEph.MemoEph.CopyToClipboard ;
////  if  ( Key = Ord('X') ) and(Shift = [ssCtrl]) // to
////    then                                            // Clipboard
////      fmSatEph.MemoEph.CutToClipboard ;
////  if  ( Key = Ord('V') ) and(Shift = [ssCtrl]) // from
////    then                                            // ClipBoard
////      fmSatEph.MemoEph.PasteFromClipboard ;
////    }
////    }

//    public class TfmSelEph
//    {
//        public bool BooSelEph;  // boolean true if ephemeris is selected
//        public double SatPeriod; // period of satellite revolution in day
//        public double StartDate; // julian date to start
//        public double FinishDate; // julian date to finish
//        public double StepPrint; // step to output information
//        public double LatMinVal; // minimum value for latitude in degree
//        public bool BooDayEph;  // true if day and night
//        public bool BooVisEph;  // true if visibility false if detail
//        public UnCoTVar.TElemRec SatElemEph;   // type from UnCoTVar satellite elements
//        public UnCoTVar.TElemRec SaoElemEph;   // type from UnCoTVar satellite elements
//        public TPlaceCooRec SiteCooEph; // type from UnGloVar station position

//        public TfmSelEph fmSelEph;

//// ---> 4 // ---> 6
//        public void BeforeShow() // 'P' or 'E'
//        {                            // CharForEphem from UnGloVar
//            fmSelEph.BooSelEph:= False; // boolean to select initial condition
//            fmSelEph.BooVisEph:= False; // boolean false for visibility
//            switch (CharForEphem)
//            {
//                case 'P':
//                    fmSelEph.Caption:= 'Эфемериды: начальные условия';
//                    fmSelEph.lbSite.Caption:= 'пункт наблюдений';
//                    fmSelEph.lbSatl.Caption:= 'спутник';
//                    fmSelEph.ButSite.Enabled:= True;
//                    fmSelEph.ButSatl.Enabled:= True;
//                    fmSelEph.ButSite.Hint:= 'выбрать другой пункт наблюдений';
//                    fmSelEph.SiteCooEph:= PlaceCoor; // station UnGloVar
//                    fmSelEph.edSite.Text:= fmSelEph.SiteCooEph.name; // station indentification
//                    fmSelEph.lbLatm.Caption:= 'высота';
//                    fmSelEph.rbDetail.Enabled:= True;
//                    fmSelEph.rbDetail.Visible:= True;
//                    fmSelEph.rbVisibl.Enabled:= True;
//                    fmSelEph.rbVisibl.Visible:= True;
//                    fmSelEph.lbStart.Enabled:= True;
//                    fmSelEph.lbStart.Visible:= True;
//                    fmSelEph.lbFinish.Enabled:= True;
//                    fmSelEph.lbFinish.Visible:= True;
//                    fmSelEph.meStart.Enabled:= True;
//                    fmSelEph.meStart.Visible:= True;
//                    fmSelEph.meFinish.Enabled:= True;
//                    fmSelEph.meFinish.Visible:= True;
//                    fmSelEph.rbNight.Enabled:= True;
//                    fmSelEph.rbDayn.Enabled:= True;
//                    fmSelEph.rbNight.Visible:= True;
//                    fmSelEph.rbDayn.Visible:= True;

//                case 'O':
//                    fmSelEph.Caption:= 'Эфемериды: взгляд со спутника';
//                    fmSelEph.lbSite.Caption:= 'объект наблюдений';
//                    fmSelEph.lbSatl.Caption:= 'спутник';
//                    fmSelEph.ButSite.Enabled:= True;
//                    fmSelEph.ButSatl.Enabled:= True;
//                    fmSelEph.ButSite.Hint:= 'выбрать другой объект наблюдений';
//                    CharBodEph:= 'P';  // a view to the Sun UnGloVar
//                    NumberBody:= NSun; // the Sun default   UnGloVar
//                    fmSelEph.edSite.Text:= PlanetNameR[NumberBody];
//                    fmSelEph.SiteCooEph.name:= PlanetNameR[NumberBody];
//                    fmSelEph.lbLatm.Caption:= 'высота';
//                    fmSelEph.rbDetail.Enabled:= True;
//                    fmSelEph.rbDetail.Visible:= True;
//                    fmSelEph.rbVisibl.Enabled:= True;
//                    fmSelEph.rbVisibl.Visible:= True;
//                    fmSelEph.lbStart.Enabled:= True;
//                    fmSelEph.lbStart.Visible:= True;
//                    fmSelEph.lbFinish.Enabled:= True;
//                    fmSelEph.lbFinish.Visible:= True;
//                    fmSelEph.meStart.Enabled:= True;
//                    fmSelEph.meStart.Visible:= True;
//                    fmSelEph.meFinish.Enabled:= True;
//                    fmSelEph.meFinish.Visible:= True;
//                    fmSelEph.rbNight.Enabled:= False;
//                    fmSelEph.rbDayn.Enabled:= False;
//                    fmSelEph.rbNight.Visible:= False;
//                    fmSelEph.rbDayn.Visible:= False;

//                case 'L':
//                    fmSelEph.Caption:= 'Эфемериды: лимб со спутника';
//                    fmSelEph.lbSite.Caption:= 'объект наблюдений';
//                    fmSelEph.lbSatl.Caption:= 'спутник';
//                    fmSelEph.ButSite.Enabled:= False;
//                    fmSelEph.ButSatl.Enabled:= False;
//                    CharBodEph:= 'L';  // a view to the limb UnGloVar
//                    NumberBody:= 0;    // a limb default   UnGloVar
//                    fmSelEph.edSite.Text:= 'лимб';
//                    fmSelEph.SiteCooEph.name:= 'лимб';
//                    fmSelEph.lbLatm.Caption:= 'размер поля';
//                    fmSelEph.rbDetail.Enabled:= False;
//                    fmSelEph.rbDetail.Visible:= False;
//                    fmSelEph.rbVisibl.Enabled:= False;
//                    fmSelEph.rbVisibl.Visible:= False;
//                    fmSelEph.lbStart.Enabled:= False;
//                    fmSelEph.lbStart.Visible:= False;
//                    fmSelEph.lbFinish.Enabled:= False;
//                    fmSelEph.lbFinish.Visible:= False;
//                    fmSelEph.meStart.Enabled:= False;
//                    fmSelEph.meStart.Visible:= False;
//                    fmSelEph.meFinish.Enabled:= False;
//                    fmSelEph.meFinish.Visible:= False;
//                    fmSelEph.rbNight.Enabled:= False;
//                    fmSelEph.rbDayn.Enabled:= False;
//                    fmSelEph.rbNight.Visible:= False;
//                    fmSelEph.rbDayn.Visible:= False;

//            } // case ch character 'P' or 'O' or 'L'
//        }

//        public string TextForDate(double tt)
//        {
//            int nd, nm, ny;
//            double dt;
//            string st;
//            TransJDtoDate(tt, nd, nm, ny, dt); // UnForDat day month year part of day
//            st:= IntegerToFixStr(nd, 2) + IntegerToFixStr(nm, 2) + IntegerToFixStr(ny, 4);
//            TextForDate:= st;
//        }

//        public double ForDifDay()
//        {
//            double dt;
//            if (CharForEphem = 'L')  // only for limb
//                dt = 1.0;         // only for limb one day
//            else
//                dt = 5.0;        // other case five days
//            if (fmSelEph.rbVisibl.Checked)
//                dt = 35.0;
//            ForDifDay = dt;
//        }

//        public void StartFinishDate()
//        { // StartDate must be known
//            fmSelEph.meStart.Text = TextForDate(fmSelEph.StartDate);
//            fmSelEph.FinishDate = fmSelEph.StartDate + ForDifDay;
//            fmSelEph.meFinish.Text = TextForDate(fmSelEph.FinishDate);
//        }

//        public double ToGetDate(string st)
//        {
//            int nd, nm, ny, er;
//            double da;
//            string sa:= Copy(st, 1, 2); // day
//            Val(sa, nd, er);    // day
//            sa:= Copy(st, 3, 2); // month
//            Val(sa, nm, er);    // month
//            sa:= Copy(st, 5, 4); // year
//            Val(sa, ny, er);    // year
//            TransDateToJD(nd, nm, ny, da); // UnForDat
//            ToGetDate:= da;
//        }

//        public int ToGetStepPrint(int n)
//        {
//            int[] p = new int[] { 1, 2, 5, 10, 15, 30, 60, 120, 180, 300, 600, 900, 1200, 1800, 2400, 3600 };
//            ToGetStepPrint = p[n];
//        }

//        public int ToGetStepPosition()
//        {                             // SatElemEph.an in revolution per day
//            double b:= 1440.0 / fmSelEph.SatElemEph.an; // period of revolution in minute
//            b:= 0.01 * b;                        // 0.01 part of period in minute
//            int s:= Round(60 * b); // integer value for step to print in second
//            int n:= 6; // 30 second default
//            for i:= 1 to 16 do
//                    if  s > fmSelEph.ToGetStepPrint(i)  then n:= i;
//  case  CharForEphem of
//    'P' : if (fmSelEph.SatElemEph.an > 14.1)
//                then n:= 5; // 15 second small step for printing
//            'O' : n:= 1;         // only one second step for printing
//            'L' : n:= 6;         // 30 seconds step for printing
//            end; // CharForEphem 'P' or 'O'
//            ToGetStepPosition:= n;
//        }

//        public int ToGetLatmVal(int n)
//        {
//            int[] p = new int[] { 0, 5, 10, 15, 20, 25, 30 };
//            int[] o = new int[] { 2, 5, 10, 20, 50, 100, 200 };
//            ToGetLatmVal:= 10;
//            if ((n < 1) || (n > 7))
//                return; // no action
//            switch (CharForEphem)
//            {
//                case 'P': ToGetLatmVal:= p[n]; break; // to view satellite from point
//                case 'O': ToGetLatmVal:= o[n]; break; // to look object from satellite
//                case 'L': ToGetLatmVal:= Round(RinDegC / 3.0) + 1; break; // to look limb sat
//            } // CharForEphem 'P' or 'O'
//        }

//        public void FormActivate(object sender)
//        {
//            fmSelEph.rbDetail.Checked = true; // default for detail ephemeris
//            if (CharForView = 'L') // for look form the satellite option    
//                fmSelEph.SatElemEph = SatElemLook
//            else
//      if (BooFixObject && (chFixObject = 'S'))
//                // satellite is selected on the screen
//                fmSelEph.SatElemEph = SatElemFix;
//            else
//                ToGetRandomSat(fmSelEph.SatElemEph); // proc from UnitFixO
//            fmSelEph.edSatl.Text = fmSelEph.SatElemEph.satname; // satl. identification
//            fmSelEph.SatPeriod = 1.0 / fmSelEph.SatElemEph.an; // satellite period in day
//            fmSelEph.StartDate = JulianDate;
//            fmSelEph.StartFinishDate; // JulianDate from UnGloVar
//            fmSelEph.udStep.Position = fmSelEph.ToGetStepPosition; // to find step
//            fmSelEph.edStep.Text = IntToStr(ToGetStepPrint(fmSelEph.udStep.Position));
//            switch (CharForEphem)
//            {
//                case 'P':  // satellite from the station 'S' option

//                    fmSelEph.lbLatm.Hint:= 'минимальная высота над горизонтом';
//                    fmSelEph.lbDegr.Caption:= 'градусы';
//                    fmSelEph.udLatm.Enabled:= True;
//                    fmSelEph.udLatm.Visible:= True;
//                    fmSelEph.udLatm.Position:= 3; // minimum altitude 10 degree

//                case 'O':  // object from the satellite  'S' 'L' options

//                    fmSelEph.lbLatm.Hint:= 'максимальная высота над поверхностью';
//                    fmSelEph.lbDegr.Caption:= 'км';
//                    fmSelEph.udLatm.Enabled:= True; // for the altitide
//                    fmSelEph.udLatm.Visible:= True;
//                    fmSelEph.udLatm.Position:= 7; // minimum height 200 km

//                case 'L':  // limb from the satellite  CharForView = 'L' option

//                    fmSelEph.lbLatm.Hint:= 'размер малого поля зрения';
//                    fmSelEph.lbDegr.Caption:= 'градусы';
//                    fmSelEph.udLatm.Enabled:= False;  // only one value
//                    fmSelEph.udLatm.Visible:= False;  // only one value
//                    fmSelEph.udLatm.Position:= 1; //

//            } // CharForEphem 'P' 'O' 'L'
//            fmSelEph.edLatm.Text:= IntToStr(ToGetLatmVal(fmSelEph.udLatm.Position));
//            fmSelEph.rbNight.Checked:= True;
//            fmSelEph.BooDayEph:= not fmSelEph.rbNight.Checked; // the night only
//        }

//        public void rbDetailClick(object sender)
//        {
//            fmSelEph.BooVisEph = false; // boolean false for visibility true for detail
//            fmSelEph.StartFinishDate(); // here above
//        }

//        public void rbVisiblClick(object sender)
//        {
//            fmSelEph.BooVisEph:= True; // boolean true for visibility false for detail
//            fmSelEph.StartFinishDate; // here above
//        }

//        public void ButSelClick(object sender) // to select ephemeris
//        {
//            double tn:= fmSelEph.ToGetDate(fmSelEph.meStart.Text);
//            double tk:= fmSelEph.ToGetDate(fmSelEph.meFinish.Text);
//            if (tk < tn)
//            // strange interval
//            {
//                ShowMessage('странный интервал');
//                fmSelEph.meStart.Text:= TextForDate(fmSelEph.StartDate);
//                fmSelEph.meFinish.Text:= TextForDate(fmSelEph.FinishDate);
//                Exit;
//            }
//            else
//            {
//                fmSelEph.StartDate= tn;
//                fmSelEph.FinishDate= tk;
//                fmSelEph.BooSelEph= true; // ephemeris StepPrint from second to day
//                fmSelEph.StepPrint= StrToInt(fmSelEph.edStep.Text) / 86400.0;
//                fmSelEph.LatMinVal= StrToInt(fmSelEph.edLatm.Text);
//                fmSelEph.BooDayEph= ! fmSelEph.rbNight.Checked;
//                Close();
//            }
//        }

//        public void ButEscClick(object sender) // to quit without ephem.
//        {
//            BooSelEph:= False;
//            Close;
//        }

//        public void ButSiteClick(object sender) // to new station
//        {
//            switch (CharForEphem)         // UnGloVar
//            {
//                case 'P':
//                    // satellite from station
//                    ToSelectOneStation; // proc from UnitStat
//                    if  fmSelHol.BooSelHol // boolean var from UnSelHol fmSelHol
//         then // the new station is selected  StationList from UnitStat
//           fmSelEph.SiteCooEph:= StationList[fmSelHol.NumSelHol]; // to new station
//                    fmSelEph.edSite.Text:= fmSelEph.SiteCooEph.name; // station indentification
//                                                                     // 'P'
//                case 'O':

//                    fmSelSat.CapSelSat:= 'Небесные тела.'; // UnSelSat
//                    fmSelSat.ShowModal; // UnSelSat
//                    if (fmSelSat.BooSelSat)// boolean var from UnSelSat
//                                           // object is selected from the list
//                    { // but may be planet or satellite
//                        int nt:= fmSelSat.NumSelTop;  // number in ObjectTopHeap^[]
//                        CharBodEph:= ObjectTopHeap ^[nt].chps; // body
//                        switch (CharBodEph) // UnGloVar
//                        {
//                            case 'S':  // 'S' for the satellite

//                                int ns:= ObjectTopHeap ^[nt].nall; // number of satellite in SatElemHeap^[]
//                                if  SatElemHeap ^[ns].satname
//                                       = fmSelEph.SatElemEph.satname
//                                  then Exit; // the same satellite
//                                fmSelEph.SaoElemEph:= SatElemHeap ^[ns]; // satellite elements
//                                fmSelEph.edSite.Text:= SaoElemEph.satname; // satellite identification
//                                fmSelEph.SiteCooEph.name:= fmSelEph.edSite.Text;

//                            case 'P':  // 'P' for the planet

//                                ns:= ObjectTopHeap ^[nt].nall; // planet number
//                                if  ns = NEarth then Exit; // without Earth
//                                NumberBody:= ns;              // planet name
//                                fmSelEph.edSite.Text:= ObjectTopHeap ^[nt].name;
//                                fmSelEph.SiteCooEph.name:= fmSelEph.edSite.Text;

//                        } // case CharBodEph
//                    }
//                    // 'O'

//            } //{ case CharForEphem }

//        }

//        public void ButSatlClick(object sender) // to new satellite
//        { // to change satellite
//            fmSelSat.CapSelSat:= 'Спутник Земли.'; // UnSelSat
//            fmSelSat.ShowModal; // UnSelSat
//            if (fmSelSat.BooSelSat) // boolean var from UnSelSat
//                                    // object is selected from the list
//            { // but may be planet
//                int nt:= fmSelSat.NumSelTop;  // number in ObjectTopHeap^[]
//                if (ObjectTopHeap ^[nt].chps <> 'S') return; // no satellite
//                int ns:= ObjectTopHeap ^[nt].nall; // number of satellite in SatElemHeap^[]
//                if (SatElemHeap ^[ns].satname
//                    == fmSelEph.SaoElemEph.satname
//                   ) return; // the same satellite
//                fmSelEph.SatElemEph:= SatElemHeap ^[ns]; // satellite elements
//                fmSelEph.edSatl.Text:= SatElemEph.satname; // satellite identification
//            }
//        }

//        public void udStepClick(object sender, TUDBtnType Button)
//        { // control to select value for step to output information
//            if (fmSelEph.udStep.Position > 16)

//                fmSelEph.udStep.Position:= 1
//            else if (fmSelEph.udStep.Position < 1)
//                fmSelEph.udStep.Position:= 16;
//            fmSelEph.edStep.Text:= IntToStr(ToGetStepPrint(fmSelEph.udStep.Position));
//        }

//        public void udLatmClick(object Sender, TUDBtnType Button)
//        { // control to select value for minimum latitude in degree
//            if (fmSelEph.udLatm.Position > 7)
//                fmSelEph.udLatm.Position:= 1
//    else
//      if (fmSelEph.udLatm.Position < 1)
//                fmSelEph.udLatm.Position:= 7;
//            fmSelEph.edLatm.Text:= IntToStr(ToGetLatmVal(fmSelEph.udLatm.Position));
//        }

//        public void GroupBoxClick(object Sender)
//        {
//            with fmSelEph  do  // procedure from UnGraBmp
//                PuzSaveBmp(ClientWidth, ClientHeight,
//                           Width, Height, 3, 3, Canvas);
//}

//    }

//    public class TfmSatEph
//    {
//        UnSatPas UnSatPas;
//        UnSatVas UnSatVas;
//        TfmSatEph fmSatEph;
//        private bool BooGraph;
//        private bool BooPaint;
//        private TCursor mmCursor;  // Controls
//        private TBitMap BmpPaint;  // to keep part of the form fmPuz

//        public bool BooOther;
//        public TMemoryStream BinStream; // Classes
//        public bool BooPoint;
//        public int NumPoint; // number of point

//        //{ part of form fmPuz to BmpPaint of TBitMap and to keep }
//        public void ToGetBmp() // { proc is called from UnMuDraw }
//        {
//            TRect dst, sr;
//            BmpPaint:= TBitMap.Create; // only for this case
//            int xw:= PaintBox.Width;       // to place to PaintBox width
//            int yh:= PaintBox.Height;      // to place to PaintBox height
//            int x1:= xcdraw - xw div 2;      // rectangle in form fmPuz
//            int x2:= xcdraw + xw div 2;      // this rectangle
//            int y1:= ycdraw - yh div 2;      // for copying
//            int y2:= ycdraw + yh div 2;      // to our BitMap
//            BmpPaint.Width:= xw;   // size of our BitMap
//            BmpPaint.Height:= yh;  // it is limited by PaintBox component
//            sr.Left:= x1;          // rectangle
//            sr.Top:= y1;           // of the source
//            sr.Right:= x2;         // for por BitMap
//            sr.Bottom:= y2;        // at the form fmPuz
//            dst.Left:= 0;                   // rectangle
//            dst.Top:= 0;                    // for the Canvas
//            dst.Right:= BmpPaint.Width;     // on our BitMap
//            dst.Bottom:= BmpPaint.Height;   // selectad rectangle to BitMap
//            BmpPaint.Canvas.CopyRect(dst, fmPuz.Canvas, sr);
//            { Graphic method }
//        }

//        //{ simple image to fmSatEph.PaintBox }
//        public void AfterDrawBmp()
//        { //{ this rectangle was in the form fmPuz }
//            fmSatEph.PaintBox.Canvas.Draw(0, 0, BmpPaint);
//        } //{ try with bitmap it is selected by proc ToGetBmp }

//        public void ForDiagram() // graphical presentation
//        { // 'P' satellite from station 'O' body from satellite
//            switch (CharForEphem) // UnGloVar
//            {
//                case 'P': GraS.CulmFigure; // class from UnSatRaf
//                case 'O': Gras.EventFigur; // class from UnSatRaf
//                case 'R':
//                case 'Q': AfterDrawBmp;    // image to PaintBox
//            } // case CharForEphem kind of ephemeris
//        }

//        public void ForLabelCap()
//        { // 'P' satellite from station 'O' body from satellite
//            switch (CharForEphem) // UnGloVar
//            {
//                case 'P': // { a look from the station to the satellite }
//                    fmSatEph.Caption:= ' Прогноз положений ИСЗ';
//                    fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
//                                            + fmSelEph.SiteCooEph.name + ' ';
//                    fmSatEph.lbSatl.Caption:= ' объект наблюдений - '
//                                            + fmSelEph.SatElemEph.satname + ' ';

//                case 'O': // { a look from the satellite to the body }
//                    fmSatEph.Caption:= ' Прогноз событий';
//                    fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
//                                            + fmSelEph.SatElemEph.satname;
//                    fmSatEph.lbSatl.Caption:= ' объект наблюдений - '
//                                            + fmSelEph.SiteCooEph.name + ' ';

//                case 'R': // { some information about objects in rectangle }
//                    fmSatEph.Caption:= ' Объекты в поле зрения';
//                    fmSatEph.lbSite.Caption:= ' пункт наблюдений - '
//                                            + PlaceCoor.name + ' ';
//                 //   { UnCoTVar }
//                    fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
//                                            + 'звёзды, планеты, ИСЗ ';

//                case 'Q': // { some information about objects in rectangle }
//                    fmSatEph.Caption:= ' Объекты в поле зрения';
//                    fmSatEph.lbSite.Caption:= ' взгляд со спутника - '
//                                            + SatElemLook.satname + ' ';
//                  //  { UnCoTVar }
//                    fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
//                                            + 'звёзды, планеты, ИСЗ ';

//                case 'L': // { about objects in rectangle around limb }
//                    fmSatEph.Caption:= ' Лимб Земли по курсу';
//                    fmSatEph.lbSite.Caption:= ' взгляд со спутника - '
//                                            + SatElemLook.satname + ' ';
//                  //  { UnCoTVar }
//                    fmSatEph.lbSatl.Caption:= ' объекты наблюдений - '
//                                            + 'звёзды, планеты, ИСЗ ';

//            } // case CharForEphem 'P' or 'O' or 'R' or 'Q' or 'L'
//        }

//        public void TitleToMemo()
//        {
//            switch (CharForEphem)
//            {
//                case 'P':
//                case 'O': AboutSatellite(fmSelEph.SatElemEph); // UnSatPas
//                case 'Q':
//                case 'L': AboutSatellite(SatElemLook); // from UnSatPas
//            } // CharForEPhem
//            switch (CharForEphem)
//            {
//                case 'P':
//                case 'R':
//                    with fmSatEph.MemoEph.Lines  do
//                        begin
//                    if  CharForEphem = 'R'
//              then
//                        Add(ToGetStrForDate(JulianDate)); // UnForDat
//                    Add(ToGetStrForSito(PlaceCoor)); //{ global var }
//                    Add(ToGetStrForSitu(PlaceCoor));// { UnCoTVar }

//                case 'O':
//                    with fmSatEph.MemoEph.Lines  do
//                        // for 'O' may be 'S' satellite 'P' planet

//                        Add('  взгляд со спутника на объект');


//          Add('  поиск событий захода объекта'
//          + ' за край Земли и событий восхода');
//                    switch (CharBodEph) // UnGloVar 'S' or 'P'
//                    {
//                        case 'P': Add('        светило   ' + fmSelEph.SiteCooEph.name);
//                        case 'S': Add('        спутник   ' + fmSelEph.SiteCooEph.name);
//                    } // CharBodEph

//                    'Q' : with fmSatEph.MemoEph.Lines  do
//                        begin




//                              Add(ToGetStrForDate(JulianDate)); // UnForDat
//                    MoreAboutSat(JulianDate, SatElemLook);  // UnSatVaz
//                    end;
//            } // case 'P' 'O' 'R' 'Q'
//            fmSatEph.MemoEph.Lines.Add('  ');    // ToGetStr... from UnitStat
//        }

//        public void PassToMemo()
//        {
//            fmSatEph.BinStream:= TMemoryStream.Create; // Classes
//            mmCursor:= fmPuz.Cursor;
//            fmPuz.Cursor:= crHourGlass;
//            fmSatEph.MemoEph.Visible:= False;
//            fmSatEph.MemoEph.Clear;
//            fmSatEph.TitleToMemo();
//            switch (CharForEphem)  // UnGloVar
//            {
//                case 'P': UnSatPas.ToFindPassage(); // UnSatPas
//                case 'O': UnSatVas.ToFindEvents(); // UnSatVas
//                case 'R': AfterDrawSky(); // UnMuDraw
//                case 'Q': AfterDrawLook(); // UnMuDraw
//                case 'L': ToLimbCourse(); // UnSatVau
//            } // case 'P' 'O' 'R' 'Q' 'L'
//            if (fmSatEph.NumPoint == 0)
//            {
//                fmSatEph.MemoEph.Lines.Add('  ');
//                switch (CharForEphem)  // UnGloVar
//                {
//                    case 'P': fmSatEph.MemoEph.Lines.Add('  нет видимости');
//                    case 'O': fmSatEph.MemoEph.Lines.Add('  нет событий');
//                } // case 'P' or 'O'
//            }
//            fmSatEph.MemoEph.Lines.Insert(0, '  ');
//            fmPuz.Cursor:= mmCursor;
//        }

//        public void FormActivate(object sender)
//        {
//            fmSatEph.ForLabelCap();
//            fmSatEph.BooGraph:= False; // no diagram but table
//            fmSatEph.BooOther:= False;
//            fmSatEph.ButGraph.Caption:= 'диаграмма';
//            if (CharForEphem == 'L') // course to point of limb
//            {
//                fmSatEph.ButGraph.Visible:= False;
//                fmSatEph.ButOther.Visible:= False;
//            }
//            else
//            {
//                fmSatEph.ButGraph.Visible:= True;
//                fmSatEph.ButOther.Visible:= True;
//            }
//            fmSatEph.MemoEph.Visible:= True;
//            fmSatEph.MemoEph.SetFocus;
//        }

//        public void ButGraphClick(object sender)
//        {
//            fmSatEph.BooGraph = !fmSatEph.BooGraph;
//            if (fmSatEph.BooGraph)
//            {
//                fmSatEph.MemoEph.Visible = false;
//                fmSatEph.MemoEph.Enabled = false;
//                fmSatEph.ButGraph.Caption = 'таблица';
//                fmSatEph.ButGraph.Hint = 'результаты расчётов';
//                fmSatEph.ForDiagram();
//                fmSatEph.BooPaint = false;
//            }
//            else
//            {
//                fmSatEph.MemoEph.Enabled = true;
//                fmSatEph.MemoEph.Visible = true;
//                fmSatEph.ButGraph.Caption = 'диаграмма';
//                fmSatEph.ButGraph.Hint = 'графическая иллюстрация';
//            }
//        }

//        public void ButEscClick(object sender)
//        { //{ to close form }
//            fmSatEph.Close;
//        }

//        public void ButOtherClick(object sender)
//        { //{ to close form }
//            fmSatEph.BooOther:= True;
//            fmSatEph.Close;
//        }

//        //{ if new file to write or append
//        //  the contents of MemoEph.Lines sf name of file }
//        public void WriteAppEnd(string sf)
//        {
//            FILE ft;   // our file with text
//            int ns;
//            AssignFile(ft, sf); // our text file
//            if  FileExists(sf) // function SysUtils
//    then             // file exists already
//      AppEnd(ft)     // to write to the end of file
//    else
//      ReWrite(ft);   // to open new file
//            for ns:= 0 to fmSatEph.MemoEph.Lines.Count - 1 do
//                    WriteLn(ft, fmSatEph.MemoEph.Lines[ns]);

//               CloseFile(ft);
//        }

//        public void ToSaveGraph()
//        {
//            TBitMap BitMap:= TBitMap.Create;
//            BitMap.Width:= fmSatEph.ClientWidth - 4;
//            BitMap.Height:= fmSatEph.ButGraph.Top - 5;
//            TRect dst, sr;   // from Windows
//            sr.Left:= 2;
//            sr.Top:= 2;
//            sr.Right:= fmSatEph.ClientWidth - 2;
//            sr.Bottom:= fmSatEph.ButGraph.Top - 3;
//            dst.Left:= 0;
//            dst.Top:= 0;
//            dst.Right:= BitMap.Width;
//            dst.Bottom:= BitMap.Height;
//            BitMap.Canvas.CopyRect(dst, fmSatEph.Canvas, sr);
//            if  fmPuz.SavePictureDialog.Execute
//              then
//        { standard dialog }
//            BitMap.SaveToFile(fmPuz.SavePictureDialog.FileName);
//            BitMap.Free;
//        }

//        public void ButSaveClick(object sender)
//        {
//            if  not BooGraph
//              then // for table
//      if  fmPuz.SaveDialog.Execute
//        then // fmPuz UnitMain
//          fmSatEph.WriteAppEnd(fmPuz.SaveDialog.FileName)
//                else
//    else // for diagram
//      fmSatEph.ToSaveGraph;
//        }

//        public void MemoEphKeyPress(object sender, char Key)
//        {
//            char ch = UpCase(Key);
//            if (ch /*in*/ ['Q', #27 ] )   fmSatEph.ButEscClick(Sender);  // exit Esc
//  if (ch /*in*/ ['S', #13 ]  )  fmSatEph.ButSaveClick(Sender); // save Enter
//  if ((ch == 'D') && (CharForEphem != 'L'))
//                        fmSatEph.ButGraphClick(Sender);
//            Key = '0';
//        }

//        public void FormPaint(object sender)
//        {
//            if (BooGraph && BooPaint)
//                fmSatEph.ForDiagram();
//            BooPaint = true;
//        }

//        public void FormKeyPress(object sender, out char Key)
//        {
//            char ch = UpCase(Key);
//            if (ch /*in*/ ['Q', #27 ] )   fmSatEph.ButEscClick(Sender);  // exit Esc
//  if (ch /*in*/ ['S', #13 ])    fmSatEph.ButSaveClick(Sender); // save Enter
//  if ((ch == 'D') && (CharForEphem != 'L'))
//                        fmSatEph.ButGraphClick(Sender);
//        }

//        public void FormClose(object sender, TCloseAction Action)
//        {
//            fmSatEph.BinStream.Free;
//            if (!fmSatEph.MemoEph.Enabled)
//                // { this situation may be in diagram case }
//                fmSatEph.MemoEph.Enabled = true;// { always }
//            if (CharForEphem /*in*/ ['R', 'Q'])
//                BmpPaint.Free; //{ BitMap is created only for select rectangle }
//        }

//        public void FormClick(object sender)
//        {
//            with fmSatEph  do  // procedure from UnGraBmp
//                PuzSaveBmp(ClientWidth, ClientHeight,
//                           Width, Height, 3, 3, Canvas);
//}

//        public void MemoEphKeyDown(object sender, TShiftState Shift, out int var Key)
//        {
//            if (fmSatEph.BooGraph || (Key = Ord('D')))
//                return; // only for MemoEph component
//            if ((Key == Ord('A')) && (Shift == [ssCtrl]))// to select
//                                                         // all in Memo
//                fmSatEph.MemoEph.SelectAll;
//            if ((Key == Ord('C')) && (Shift == [ssCtrl])) // to
//                                                          // ClipBoard
//                fmSatEph.MemoEph.CopyToClipboard;
//            if ((Key == Ord('X')) && (Shift == [ssCtrl]))// to
//                                                         // Clipboard
//                fmSatEph.MemoEph.CutToClipboard;
//            if ((Key == Ord('V')) && (Shift == [ssCtrl])) // from
//                                                          // ClipBoard
//                fmSatEph.MemoEph.PasteFromClipboard();
//        }

//    }

//    public class UnPlaceA
//    {
//        // option 'S' sky to look from the station
//        public vec3 ToGetSkyColor()
//        { // ClcThree.. in UnForCoo where StationPos is in true equator system
//            char ch:= DayOrNightNow; // from unit UnForPos
//            ToGetSkyColor:= clBlack; // default
//   case  ch of
//     'D' : ToGetSkyColor:= clBlue;
//            'T' : ToGetSkyColor:= clNavy;
//            'N' : ToGetSkyColor:= clBlack; // color from Graphics
//            end; // case ch
//        }

//        //{ option 'L' for look from the satellite
//        //  our satellite may be in shadow with index LooShad = 2 }
//        public vec3 ToGetSkyColoo()
//        { // ClcThree.. in UnForCoo where StationPos is in true equator system
//            ToGetSkyColoo:= clBlack; // default
//            switch (ShadLookSat)  // byte function from UnForSat
//            {
//                case 0:
//                    ToGetSkyColoo:= clBlue;
//                    SpecButt[21]:= '  свет  ';
//                    HintButt[21]:= 'спутник освещён';

//                case 1:
//                    ToGetSkyColoo:= clNavy;
//                    SpecButt[21]:= 'полутень';
//                    HintButt[21]:= 'Солнце частично закрыто ';

//                case 2:
//                    ToGetSkyColoo:= clBlack; // color from Graphics
//                    SpecButt[21]:= '  тень  ';
//                    HintButt[21]:= 'Солнце закрыто Землёй';

//            } // case ch
//        }

//        public void RectCoordinates(double a, double b, out int x, out int y)
//        {
//            x = -10; // non real current position in SkyRect
//            y = -10; // non real value  RinDegC radius in degree from UnGloVar
//            if ((Math.Abs(a) < XinDegC) && (Abs(b) < YinDegC))
//            {
//                // half      reversal                    max range
//                x = SkyParm.xh - SkyParm.rx * Math.Round(a / XinDegC * SkyParm.dx);
//                y = SkyParm.yh - SkyParm.ry * Math.Round(b / YinDegC * SkyParm.dy);
//            }
//        }

//        public bool BooPosInRect(int x, int y)
//        {
//            With SkyParm Do // SkyParm from unit UnButIni
//     if (((xa < x) and(x < xb)) && ((ya < y) and(y < yb)) )
//        // our point in our rectangle
//         BooPosInRect:= True
//              else
//         BooPosInRect:= False;
//        }

//        public void ScreenStarsToHeap(int n, int x, int y, double azt, double alt)
//        {
//            if (NumObjectInHeap < MaxObjectInHeap)
//            // all variables from unit UnCotVar
//            {
//                Inc(NumObjectInHeap);
//                ObjectTopHeap ^[NumObjectInHeap].nall:= n; // number StarHeap
//                ObjectTopHeap ^[NumObjectInHeap].chps:= '*'; // there is star
//                ObjectTopHeap ^[NumObjectInHeap].name:= ''; // default
//                ObjectTopHeap ^[NumObjectInHeap].azt:= azt; // current azimuth
//                ObjectTopHeap ^[NumObjectInHeap].alt:= alt; // current alitude
//                ObjectTopHeap ^[NumObjectInHeap].rot:= 1.0; // range default for star
//                ObjectTopHeap ^[NumObjectInHeap].Mag:= StarOneCur.Vt; // magnitude
//                ObjectTopHeap ^[NumObjectInHeap].phase:= 1.0; // default for star
//                ObjectTopHeap ^[NumObjectInHeap].IndexShadow:= 0; // default
//                ObjectTopHeap ^[NumObjectInHeap].boovis:= True; // on screen
//                ObjectTopHeap ^[NumObjectInHeap].booscreen:= True; // on screen
//                ObjectTopHeap ^[NumObjectInHeap].xpos:= x; // x on screen
//                ObjectTopHeap ^[NumObjectInHeap].ypos:= y; // y on screen
//            }
//        }

//        //{ values for AzimutC and AltitudeC must be known
//        //  to use procedure StarHorizonCoor form UnForSta
//        //  to obtain azimuth and altitude of a star
//        //  to find for star being near field of view or no }

//        public void StarToScreen() // to put stars to screen
//        {
//            n: Word;
//            azt,alt: Extended; // azimuth altitude in degree
//            ksi,eta: Extended; // ideal coordinates
//            xc,yc: Integer; // position in the field of view
//            bos: Boolean; // star may be out of our hemisphere
//            Begin                          // all stars from the list
//              For n:= 1 To NumberOfStars Do // NumberOfStars from StarToMemory
//                 Begin // StarHorizontCoor with the aberration corr. UnForSta
//       StarHorizontCoor(n, azt, alt, bos); // azimuth altitude
//            if (bos)    // this star may be in our hemisphere
//                        // this star mey be in our field of view
//            {  // about this star in record StarOneCur
//                IdealCoordinates(azt, alt, ksi, eta); // unit UnPlateC
//                RectCoordinates(ksi, eta, xc, yc); // field as plate
//            } // position projection to the plate
//            else     // this star is out our hemisphere
//            {  // this star is out our field if view
//                xc:= -30000; // default large minus position
//                yc:= -30000; // default large minus position
//            }   // to exlude this star
//            if (BooPosInRect(xc, yc)  // to compare current position

//                 && (StarOneCur.vt < VMagMax))// with our rectangle

//            // PUnCRead to limit with magnitude

//            {                // as field of view

//                FigureStar(xc, yc); // UnGraAll star in our rectangle
//                Inc(NumStarsInRect); // to add one star for screen
//                ScreenStarsToHeap(n, xc, yc, azt, alt); // to heap now
//            } // but other stars are excluded
//            End; // after ScreenStarsToHeap always boovis booscreen
//        }

//        public void ViewPlac() // to view area of the sky
//        {
//            ClcThreeRotMatr(JD2000, JulianDate,  // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed VelBarEarth of TVect3
//            NumStarsInRect:= 0; // unit UnGloVar
//            NumObjectInHeap:= 0; // unit UnGloVar
//            ToGetCentrePos; // UnitFixO for AzimutC AltitudeC values
//            StarToScreen; // to put stars to screen
//            PlanetsToScreen; // from unit UnForPos
//            SatellitesAmongStars; // from unit UnForSat
//            if (CharForView = 'L')  // in the case look option
//                                    // the Earth may be in the field of view
//                EarthFiguScreen;  // UnForZem  the Earth the field of view
//            HintForStarOnScreen;  // from UnButIni
//            CursorFigu(xpMouse, ypMouse); // proc UnMouSky var UnGloVar
//        }

//        //{ proc StarToMemory from UnForSta
//        //  to obtain fixed equator position
//        //  of the centre of the field of view
//        //  right ascension declination pos[1, 2, 3]
//        //  from topocentric horizon azimuth altitude }
//        public void FiguViewPlac() // after changing some parameters
//        {                    // 'S'
//            if (SkyBooX)// boolean variable from UnButIni
//                ToClearBoxInform; // procedure from UnPlaceO
//            ClcThreeRotMatr(JD2000, JulianDate,  // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed VelBarEarth of TVect3
//            HintForButtons; // UnButIni
//            SpecForButtons; // from unit UnButIni
//            FiguPlac; // unit UnFigIni
//            ToGetCentrePos; // UnitFixO for AzimutC AltitudeC values
//            StarToMemory; // UnForSta  all stars in selected rectangle
//            if (CharForView = 'L')  // UnForZem in the case look option
//                                    // the Earth in the field of view
//                EarthFiguScrini;  // for values PosZemelaC AngleZemeL
//            ViewPlac; // from Unit UnPlaceA
//        }

//        public void FiguViewInfo() // after changing some parameters
//        {
//            if (SkyBooX) // boolean variable from UnButIni
//                ToClearBoxInform; // procedure from UnPlaceO
//            ClcThreeRotMatr(JD2000, JulianDate,  // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed VelBarEarth of TVect3
//            ToGetCentrePos; // UnitFixO for AzimutC AltitudeC values
//            HintForButtons; // from unit UnButIni
//            SpecForButtons; // from unit UnButIni
//            FiguInfo; // unit UnFigIni
//            StarToMemory; // unit UnForSta all stars in selected rectangle
//            if (CharForView = 'L') // UnForZem in the case look option
//                                   // the Earth in the field of view
//                EarthFiguScrini;  // for values PosZemelaC AngleZemeL
//            ViewPlac; // from Unit UnPlaceA
//        }

//        public void FiguViewMap() // to view map of the world
//        {  // the Sun the Moon refer to rotating Earth
//            double fs, vs, fm, vm;
//            int xs, ys, xm, ym;

//            if (CharForView <> 'W')
//                return; // no action only for WorldMap
//            HintForButtons();
//            SpecForButtons(); // from unit UnButIni
//            FiguInfo(); // unit UnFigIni
//            with SkyRect  do // rectangular SkyRect from unit UnButIni
//                MapW.SimpleMapWorld(x1, y1, x2, y2); // method of class TMapW UnClaMap

//            RotatingGeoPos(NSun, MapSatGeoPos[MapSatNumber + 1], fs, vs);  // UnForPos
//            RotatingGeoPos(NMoon, MapSatGeoPos[MapSatNumber + 2], fm, vm); // UnForPos
//            MapW.DayNightLine(fs, vs, fm, vm, xs, ys, xm, ym); // UnClaMap with Sun Moon screen
//            SatelliteToMapWorld(xs, ys, xm, ym); // procedure from UnForSat
//        }
//    }

//    public class UnForBut // what to do when one button is choiced
//    {
//        UnGloVar UnGloVar;
//        UnitStat UnitStat;
//        TfmSelHol fmSelHol;
//        UnPlaceA UnPlaceA;
//        TfmSelEph fmSelEph;
//        TfmSatEph fmSatEph;
//    UnForCoo UnForCoo;

//        public void ForButtonF()// to select from two catalogues TRC1 or TYC2
//        {
//            if (!(CharForView /*in*/ ['S', 'L']))
//                return;
//            ToSelectOneCatalogue(); // procedure from UnFilesO

//            if (fmSelFel.BooSelFel)
//            { // stCatalogFile is declared in UnGloVar
//                stCatalogFile = fmSelFel.StrSelFel; // name of catalogue file
//                if (!ToOpenOtherFile) // boolean function from UnFilIni
//                                      // by using stCatalogFile
//                    return; // a try is unsuccessful
//                if (BrightStarsOnly) // global variables are declared in unit PUnCRead

//                {
//                    BrightStarsOnly = !BrightStarsOnly;
//                    RinDegC = 1.5;


//                }
//                FiguViewPlac(); // unit UnPlaceA

//            }

//        }
//        public void ForButtonDate() // to change date with time
//        {
//            fmSelDat = TfmSelDat.Create(Application);
//            try
//            {
//                fmSelDat.ShowModal;
//                if (fmSelDat.BooSelDat)
//                    // but may be error
//                    JulianDate = fmSelDat.JulSelDat;
//            }
//            finally
//            {
//                fmSelDat.Free;
//            }
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed
//            switch (CharForView)
//            {
//                case 'S':
//                case 'L': FiguViewInfo; // from UnPlaceA
//                case 'W': FiguViewMap;  // from UnPlaceA
//            }

//        }
//        public void ForButtonP()
//        {
//            if (UnGloVar.CharForView != 'S') return;
//            UnitStat.ToSelectOneStation(); // UnitStat
//            if (fmSelHol.BooSelHol)
//            // the new station is selected
//            { // word NumStatValue from UnGloVar
//                UnGloVar.NumStatValue = fmSelHol.NumSelHol; // current station number in list
//                UnGloVar.PlaceCoor = UnitStat.StationList[UnGloVar.NumStatValue]; // to change station to view
//                UnitStat.StationForView(); // UnitStat  PlaceCoor from UnGloVar
//                UnPlaceA.FiguViewInfo();    // UnPlaceA
//            }
//        }
//        public void NoVisibilityMessage()
//        {  // ShowMessage from unit Dialogs
//            ShowMessage(fmSelSat.StrSelSat + '  нет видимости');
//            InitForFixCase; // UnitFixO no visibility no fix object
//        }
//        public void ForButtonC() // to choose object from the list
//        {                  // for sky and look options not for map
//            if (not(CharForView in ['S', 'L'])) 
//                    return;
//            fmSelSat.CapSelSat = 'Небесные тела.';
//            fmSelSat.ShowModal;
//            if (fmSelSat.BooSelSat)
//            // object is selected from the list
//            {
//                BooFixObject = true; // var from UnGloVar  OBoxInform from UnPlaceO
//                OBoxInform = fmSelSat.NumSelTop;  // number in ObjectTopHeap^[]
//                ToDefineFixObject; // UnitFixO for ObjectTopHeap^[OBoxInform]
//                if (not ToGetCentrePos ) // UnitFixO for AzimutC AltitudeC          
//            NoVisibilityMessage
//          else
//            if (chFixObject = 'S') // character is assigned in UnitFixO
//                    StepValueForFixSat; // UnitSelH small step
//                FiguViewPlac; // UnPlaceA sky look options
//            }
//            else
//                BooFixObject:= False;
//        }
//        public void ForButtonE() // to choice set of elements for the satellite
//        {
//            if (!(CharForView /*in*/ ['S', 'L']))
//                return;
//            ToSelectOneSetOfElem(); // procedure from UnFilesO
//            if (fmSelFel.BooSelFel) // from UnSelFel
//            { // stElementFile is declared in UnGloVar
//                stElementFile = fmSelFel.StrSelFel; // name of file with elements

//                switch (ToGetElemFormat(stElementFile))

//                {// function from UnFilElm
//                    case 'N': ToReadNoradFormat(stElementFile); // UnrfNord
//                    case 'I': ToWorkWithIRVSfmt(stElementFile); // UnrfIrvs
//                    case 'K': ToWorkWithKodrfmt(stElementFile); // UnrfKodr
//                    case 'R': ToReadPosVelForma(stElementFile); // UnrfIrvs
//                    case 'Z': ShowMessage(stElementFile + '  нет элементов');
//                }
//                FiguViewPlac(); // unit UnPlaceA
//            }
//        }
//        public void ForButtonT() // to change catalogue ;
//        { // for star '*'  chFixObject:=' ';  BooFixObject:=False;
//            if (not(CharForView in ['S', 'L']) ) return;
//            if (chFixObject = '*') InitForFixCase; // UnitFixO only for star
//            if (BrightStarsOnly)// global variables are declared in unit PUnCRead    
//                if (ExistTRCFile)
//                {
//                    BrightStarsOnly = !BrightStarsOnly;
//                    RinDegC = 1.5;
//                    FiguViewPlac; // unit UnPlaceA
//                }
//                else
//                    ForButtonF;
//            else
//  if (ExistBRFile)
//            {
//                BrightStarsOnly = !BrightStarsOnly;
//                RinDegC = 15;
//                FiguViewPlac; // unit UnPlaceA
//            }
//            NumSizeValue = 1; // for number in size area list
//        }
//        public void ForPlusT()   // forward with time
//        {
//            JulianDate = JulianDate + StepWithTime; // global var from unit UnGloVar
//            if (JulianDate > (TlastEph - 1.0))  // too big moment
//                                                // to compare with the ephemeris
//                JulianDate:= TlastEph - 1.0; // ephemeris maximum moment
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed
//            switch (CharForView)
//            {
//                case 'S':
//                case 'L': FiguViewInfo; // from UnPlaceA
//                case 'W': FiguViewMap;  // from UnPlaceA
//            }
//        }
//        public void ForMinusT()  // backward with time
//        {
//            JulianDate = JulianDate - StepWithTime; // global var from unit UnGloVar
//            if (JulianDate < (TinitEph + 1.0))  // too small moment
//                                                // to compare with the ephemeris
//                JulianDate:= TinitEph + 1.0; // ephemeris minimum moment
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//            ToObtainAberrationParm(JulianDate); // UnForRed
//            switch (CharForView)
//            {
//                case 'S': FiguViewInfo; // from UnPlaceA
//                case 'L':
//                    // look from the satelite
//                    if (JulianDate < 2436117.0)
//                        // 5 october 1957 year
//                        JulianDate:= 2436117.0; // to limit date
//                    FiguViewInfo; // form UnPlaceA

//                case 'W': FiguViewMap;  // from UnPlaceA
//            }
//        }
//        public void ForButtonS() // to choose step to change time
//        {
//            int n;
//            ToGetValueAllStepInDay; // from UnitSelH
//            fmSelHol.CapSelHol:= 'Шаг по времени.'; // form from UnSelHol
//            fmSelHol.AddSelHol:= 0;
//            fmSelHol.ListBox.Clear; // pure list now
//            for n:= 1 to NumNameValue do // var from UnGloVar is assigned in UnitSelH
//                    fmSelHol.ListBox.Items.Add('   ' + NameValue[n].strname); // UnSelHol


//             fmSelHol.ListBox.ItemIndex:= NumStepValue - 1; // to select value in ListBox
//            fmSelHol.ListBox.Columns:= 3; // to select from list fmSelHol.LisBox
//            fmSelHol.ShowModal; // to select one things from the list
//            if (fmSelHol.BooSelHol)
//            // a new value is selected
//            { // var StepWithTime from UnGloVar
//                NumStepValue:= fmSelHol.NumSelHol; // from UnSelHol  StepWithTime
//                StepWithTime:= NameValue[NumStepValue].valname; // to change step
//            }
//        }
//        public void ForButtonMap() // to figure world map
//        {
//            if (!BooExistEph)
//                return; // no planets information PUnDE200
//            switch (CharForView)
//            {
//                case 'S':
//                    // from "sky" to "map"
//                    fmHelPuz.HelPuzI:= 21; // initial number for help map
//                    fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI;
//                    BooMapPoint:= False;  // UnGloVar
//                    CharForView:= 'W';    // from UnGloVar for world map
//                    ToGetFixSatellites;  // procedure from UnitFixO
//                    FiguViewMap;        // UnPlaceA
//                    BooFixObject:= False; // UnGloVar
//                    fmSatEph.BinStream:= TMemoryStream.Create; // UnSatEph Classes
//                    fmSatMas.NulloNumbers;  // count for number of ephemeris case

//                case 'W':
//                    // from "map" to "sky"
//                    fmHelPuz.HelPuzI:= 11;  // initial number for help sky
//                    fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI; // to use help files
//                    BooMapPoint:= False;  // UnGloVar
//                    CharForView:= 'S';  // from UnGloVar for sky
//                    VMagMax:= 25;       // to limit stars with magnitude
//                    InitForFixCase;    // procedure from UnitFixO
//                    FiguViewPlac;  // after changing some parameters UnPlaceA
//                    fmSatMas.Close;
//                    fmSatEph.BinStream.Free; // UnSatEph

//                case 'L':
//                    // from "look" to "sky"
//                    ForIniC(2);    // UnGetIni CharForView 'S' sky option
//                    fmHelPuz.HelPuzI:= 11;  // initial number for help sky
//                    fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI; // to use help files
//                    BooMapPoint:= False;  // UnGloVar
//                    CharForView:= 'S';  // from UnGloVar for sky
//                    VMagMax:= 25;       // to limit stars with magnitude
//                    InitForFixCase;    // procedure from UnitFixO
//                    FiguViewPlac;  // after changing some parameters UnPlaceA

//            } // case
//            ToChangePopupMenuS; // with information in CharForView UnPopCap
//        }
//        public void ForButtonY() // centre objects position small rectangle
//        {                  // to print objects positions
//case  CharForView of
//  'S' : AfterSkyKey; // UnMuDraw 'Y' key  "sky" case information
//            'L' : AfterLooKey; // UnMuDraw 'Y' key "look" case information
//            'W' : Exit; // no operation for "map" case
//            end; // case CharForView
//        }
//        public void ForButtonI() // I ICRS information objects position
//        { // to print objects positions all screen 'S' option only
//            if (CharForView <> 'S') return; // "sky" case information
//            fmPrtPos = TfmPrtPos.Create(Application); // form UnPrtPos

//            try
//            {                   // equator ecliptic galactic positions
//                fmPrtPos.charact:= 'I'; // objects positions ICRS
//                fmPrtPos.ShowModal; // form from UnPrtPos
//            }
//            finally
//            {
//                fmPrtPos.Free;
//            }

//        }
//        public void ForButEphSat() // satellite ephemeris
//        {
//            do
//            {
//                fmSelEph.ShowModal(); // UnSelEph
//                if (fmSelEph.BooSelEph)

//                {
//                    fmSatEph.PassToMemo();  // UnSatEph
//                    fmSatEph.ShowModal(); // UnSatEph fmSatEph form for ephemeris
//                }
//                else
//                    fmSatEph.BooOther= false;
//            }
//            while(!fmSatEph.BooOther);
//            UnForCoo.ClcThreeRotMatr(UnConTyp.JD2000, UnGloVar.JulianDate, // UnConTyp UnGloVar
//                            UnGloVar.PlaceCoor, out UnGloVar.RotMatr, out UnGloVar.RosMatr, out UnGloVar.TopMatr); // UnForCoo
//        }
//        public void ForMapEphSat()
//        {
//            if  not BooMapPoint // BooMapPoint UnGloVar assigned by MouseLeftClickSky
//              then Exit;      // from UnMouSky no fixed point at the map
//            if  fmSatMas.NumEph < 4      // UnSatMas NumEph count
//    then                       // for ephemeris diagrams
//      begin
//        fmSatMas.PassForPoint;    // results to fmSatEph.BinStream
//            if  fmSatMas.NumPnt < 1
//                      then    // no visibility in this case
//            ShowMessage('совсем нет видимости');
//            end
//    else
//      ShowMessage('Количество диаграмм ограничено числом '
//                  + IntToStr(fmSatMas.NumEph)); // SysUtils
//            if  fmSatMas.NumEph > 0  // there are some ephemeris
//    then                   // for diagram
//      fmSatMas.Show;       // form from UnSatMas
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//        }

//// ---> 5
//        public void ForButEphes() // ephemeris button "
//        {                   // object from the satellite
//            if (UnGloVar.CharForView == 'W')
//                return; // no action for map
//            UnGloVar.CharForEphem = 'O'; // 'P' for point 'O' for object
//            fmSelEph.BeforeShow(); // UnSelEph
//            do
//            {
//                fmSelEph.ShowModal(); // UnSelEph
//                if (fmSelEph.BooSelEph)
//                {
//                    fmSatEph.PassToMemo();  // UnSatEph
//                    fmSatEph.ShowModal(); // UnSatEph fmSatEph form for ephemeris
//                }
//                else
//                    fmSatEph.BooOther = false;
//            }
//            while(!fmSatEph.BooOther);
//        UnForCoo.ClcThreeRotMatr(UnConTyp.JD2000, UnGloVar.JulianDate, // UnConTyp UnGloVar
//                            UnGloVar.PlaceCoor, out UnGloVar.RotMatr, out UnGloVar.RosMatr, out UnGloVar.TopMatr); // UnForCoo
//        }

//// ---> 3
//        public void ForButEphem() // ephemeris button '
//        {                // CharForEphem from UnGloVar
//            UnGloVar.CharForEphem = 'P'; // 'P' for point 'O' for object
//            fmSelEph.BeforeShow(); // with CharForEphem char UnSelEph
//            switch (UnGloVar.CharForView)
//            {
//                case 'S': ForButEphSat(); break;  // satellite ephemeris
//                case 'W': ForMapEphSat(); break; // ephemeris among the map
//                case 'L': ForButEphes(); break;  // object from the satellite

//            }                   // satellite from the station
//        }
//        public void ForSatLook() // to view from the satellite
//        {
//            if  not(BooFixObject and(chFixObject = 'S'))
//                      then  // no satellite is fixed record in SatElemFix
//      if  fmSatMap.BooChangeSatl
//        then  // a satellite may be choosen from the list
//          SatElemFix:= fmSatMap.SatMel // record TElemRec UnCotVar
//        else    // Exit for no action no satellite to look
//          Exit; // it is satellite to look SatElemFix of TElemRec
//            if  CharForView = 'W'          // to go from world map option
//    then                         // to 'L' look from sat option
//      begin                      // it may be among world map
//        fmSatMas.Close;          // this form was opened
//            fmSatEph.BinStream.Free; // UnSatEph was create
//            end;                       // to come away from 'W'
//            CharForView:= 'L'; // to look from the satellite
//            fmHelPuz.HelPuzI:= 31;    // initial number for help look option
//            fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI; // to use help files
//            SatElemLook:= SatElemFix; // UnCoTVar a look from this satellite
//            VMagMax:= 25;      // to limit stars with magnitude
//            CentreToLimbuP(JulianDate, // 'L' option centre to limb point
//                           SatElemLook, AzimutC, AltitudeC); // UnForZem
//            InitForFixCase;  // UnitFixO
//            StepValueForFixSat; // UnitSelH small step 10 second
//            FiguViewPlac;    // UnPlaceA
//            ToChangePopupMenuS; // with information in CharForView UnPopCap
//        }
//        public void ForAreaSize() // button % to change size of area
//        {
//            char n;
//            if (not(CharForView in ['S', 'L']) ) return;
//            ToGetValueAreaSize; // from UnitSelH to record array NameValue
//            fmSelHol.CapSelHol:= 'Размер поля зрения.'; // form from UnSelHol
//            fmSelHol.AddSelHol:= 0;
//            fmSelHol.ListBox.Clear; // pure list now
//            for n:= 1 to NumNameValue do // var from UnGloVar is assigned in UnitSelH
//                    fmSelHol.ListBox.Items.Add('   ' + NameValue[n].strname); // UnSelHol


//             fmSelHol.ListBox.ItemIndex:= NumSizeValue - 1; // to select value in ListBox
//            fmSelHol.ListBox.Columns:= 3; // to select from list fmSelHol.LisBox
//            fmSelHol.ShowModal; // to select one things from the list

//            if (fmSelHol.BooSelHol)
//            // the new station is selected
//            { // var SizeForArea  NumSizeValue from UnGloVar
//                NumSizeValue:= fmSelHol.NumSelHol; // form from UnSelHol
//                SizeForArea:= NameValue[NumSizeValue].valname; // to change size
//                RinDegC:= SizeForArea; // half of size on y axes in degree
//                FiguViewPlac; // unit UnPlaceA
//            }
//        }
//        public void ForMapButA()
//        {
//            if (CharForView <> 'W') return; // only for map world
//            fmSatMap.SatMapC = 'A'; // form class from UnSatMap
//            fmSatMap.ShowModal;
//        }
//        public void ForSkyButV() // to obtain initial parameters cond. vector
//        { // the first action to find satellite for parameters
//            if (!(BooFixObject && (chFixObject = 'S'))
//                // no satellite is fixed record in SatElemFix
//      if (fmSatMap.BooChangeSatl)// to use fmSatMel modal form
//                                 // a satellite may be choosen from the list
//                    SatElemFix = fmSatMap.SatMel // record TElemRec UnCoTVar
//        else    // Exit for no action no satellite to look
//                    return; // it is satellite for parameters SatElemFix
//            fmPrtPos = TfmPrtPos.Create(Application); // form UnPrtPos
//            try // different format for vector condition
//            {
//                fmPrtPos.charact = 'P'; // for initial parameters
//                fmPrtPos.starpom = JulianDate; // moment to start
//                fmPrtPos.elmSatl = SatElemFix; // record with elements
//                fmPrtPos.ShowModal; // form from UnPrtPos
//            }
//            finally
//            {

//                fmPrtPos.Free;
//            }

//        }
//        public void ForMapButV()
//        {
//            if  CharForView <> 'W'  then Exit; // only for map world
//            fmSatMap.SatMapC:= 'V'; // form class from UnSatMap
//            fmSatMap.ShowModal;
//            FiguViewMap;          // UnPlaceA
//        }
//        public void ToLookLimb() // 'L' look option for limb point
//        {
//            CharForEphem:= 'L';    // 'L' limb for look option
//            fmSelEph.BeforeShow; // UnSelEph
//            repeat
//  fmSelEph.ShowModal; // UnSelEph
//            if  fmSelEph.BooSelEph
//              then
//      begin
//        fmSatEph.PassToMemo;  // UnSatEph
//            fmSatEph.ShowModal; // UnSatEph fmSatEph form for ephemeris
//            end
//    else
//      fmSatEph.BooOther:= False;
//            until not fmSatEph.BooOther;
//            ClcThreeRotMatr(JD2000, JulianDate, // UnConTyp UnGloVar
//                            PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo
//        }
//        public void ForButtonV()
//        {
//  case  CharForView of
//    'S' : ForSkyButV; // to print condition vector init parameter
//            'W' : ForMapButV; // to change objects among map
//            'L' : ToLookLimb; // look from the sat option for limb ephem
//            end; // case
//        }
//        public void ForButtonH()
//        {
//            fmHelPuz.Show;
//        }
//        public void ForButtonQ()
//        {
//            ForIniC(2); // from unit UnGetIni CharForView 'S' sky option
//            fmHelPuz.HelPuzI:= 11;  // initial number for help sky
//            fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI; // to use help files
//            FiguViewPlac;  // after changing some parameters unit UnPlaceA
//            ToChangePopupMenuS; // UnPopCap
//        }
//        public void ForButtonX()
//        {
//            fmPuz.Close;
//        }

//// ---> 2
//        public void ForButtonChoice(char cb) // called in UnMouSky
//        {
//            char ch = UpCase(cb);
//            if (ch == '27')
//                ch = 'Q'; // Esc
//            switch (ch)
//            {
//                case 'F': ForButtonF(); break; // to select from two catalogues TRC1 or TYC2
//                case 'P': ForButtonP(); break;  // to choose station ToSelectOneStation from UnitStat
//                case 'C': ForButtonC(); break;  // to choose object
//                case 'E': ForButtonE(); break;  // to choose set of elements for the satellite
//                case 'T': ForButtonT(); break;  // to change catalogue: Bright or Tycho
//                case '+': ForPlusT(); break;   // forward with time
//                case '-': ForMinusT(); break; // backward with time
//                case 'S': ForButtonS(); break;  // to choose step to change time
//                case 'M': ForButtonMap(); break;  // satellite projection : map or sky
//                case '1': ForButtonDate(); break;  // to change date with time
//                case '2': return;
//                case 'U': return;
//                case 'Y': ForButtonY(); break;   // centre objects position small rectangle
//                case ' ': ForButEphem(); break;  // for ephemeris satellite from station
//                case ' ': ForButEphes(); break;  // for ephemeris object from satellite
//                case '&':
//                case 'L': ForSatLook(); break;   // to view from satellite option 'L'
//                case '%': ForAreaSize(); break; // to change size of area
//                case '#': return;
//                case 'I': ForButtonI(); break;  // ICRS objects position all screen
//                case 'A': ForMapButA(); break; // information about object that are above horizon
//                case 'V': ForButtonV(); break;  // 'S' vector condition 'W' objects among map 'L' limb
//                case 'H': ForButtonH(); break;  // for simple help
//                case 'Q': ForButtonQ(); break; // to return to the initial point
//                case 'X': ForButtonX(); break; // to exit program
//            }// { case char ch }
//        }

//    }

//    public class TfmPuz
//    {
//        TfmPuz fmPuz;

//        UnForBut UnForBut;

//        UnGloVar UnGloVar;

//        bool BooFormOpen;
//        bool BooFormClose;

//        void ToCreateSomeClasses()
//        {
//            Application.HintPause = 10;
//            fmSelEph = TfmSelEph.Create(Application); // form for ephemeris data
//            fmSatEph = TfmSatEph.Create(Application); // form for ephemeris
//            fmSatMas = TfmSatMas.Create(Application); // form for ephemeris map
//            fmSelFel = TfmSelFel.Create(Application); // form to select some files
//            fmSelHol = TfmSelHol.Create(Application); // form to select some points
//            fmSelSat = TfmSelSat.Create(Application); // form to choose satellite
//            fmHelPuz = TfmHelPuz.Create(Application); // form for help information
//            fmSatMap = TfmSatMap.Create(Application); // form for set of satellites
//            fmSatMel = TfmSatMel.Create(Application); // form to choose one satellite
//            MapW = TMapW.Create; // to create class TMapW UnClaMap around the world
//            MapW.LineToMem; // procedure from class TMapW UnClaMap
//            GraS = TGraS.Create; // to create class TGraS UnSatRaf passage diagram
//        }

//        void FormCreate(object sender)
//        {
//            Randomize;
//            if  not ForOpenFiles  // from UnFilIni to open files for stars
//              then
//      begin
//        ShowMessage('no catalogues');
//            Halt; // no action
//            end;
//            fmPuz.ToCreateSomeClasses;
//            BooFormOpen:= True;
//            BooFormClose:= False;
//            ForCreateMem;   // from UnMemIni
//            AllBrightStars; // from UnFilIni all bright stars just to memory
//            ForIniC(0); // from UnGetIni  variable JulianDate
//            xpMouse:= -100;   // x default for mouse UnGloVar
//            ypMouse:= -100;   // y default for mouse
//            HintForButtons; // from UnButIni for buttons
//            SpecForButtons; // from UnButIni for buttons
//            StarToMemory; // NumberOfStars around AlphaC DeltaC to memory UnForSta
//            FiguSize; // from UnFigIni for current size
//            stPathDefault:= GetCurrentDir;  // UnGloVar SysUtils folder with program
//            stPathElem:= stPathDefault; // stPathDefault string from UngloVar
//            stPathResult:= stPathDefault; // stPathDefault string from UngloVar
//            stPathSite:= stPathDefault; // UnGloVar path to folder with station file
//            ToAddStationList(stPathSite + '\statfirs.txt'); // UnitStat sites position
//            fmHelPuz.HelPuzI:= 11; // initial number for help in sky
//            fmHelPuz.HelPuzN:= fmHelPuz.HelPuzI;
//            // PrtFileOpen ; // UnForPrt
//        }

//        void ppEphClcClick(object sender)
//        {
//            CharForEphem = 'P'; // 'P' for point 'O' for object
//            ForButtonChoice(''''); // UnForBut for ephemeris
//        }

//        void ppMapViewClick(object sender) // for PopupMenus
//        {
//            UnForBut.ForButtonChoice('M'); // from unit UnForBut
//        }

//        void ppDateNewClick(object sender)
//        {
//            UnForBut.ForButtonChoice('1'); // from unit UnForBut
//        }

//        void ppAreaViewClick(object sender)
//        {
//            switch (CharForView)  // global character from UnGloVar
//            {
//                case 'S':
//                case 'L': UnForBut.ForButtonChoice('%'); // for sky from unit UnForBut
//                case 'W': UnForBut.ForButtonChoice('A'); // for map from unit UnForBut
//            } // case CharForView
//        }

//        void ppPrtPosClick(object sender)
//        { // for 'S' ICRS information about object
//            switch (CharForView) // global character from UnGloVar
//            {
//                case 'S': UnForBut.ForButtonChoice('I'); // for sky from unit UnForBut
//                case 'W': UnForBut.ForButtonChoice('V'); // for map from unit UnForBut
//            }  // case CharForView  'W' to choice satellites
//        }

//        void ppSatLookClick(object sender)
//        {
//            UnForBut.ForButtonChoice('&'); // to look from the satellite UnForBut
//        }


////---> 1
//        void MenuEphSapClick(object sender)
//        { // to view one satellite from the station
//            UnGloVar.CharForEphem = 'P'; // 'P' for point 'O' for object
//            UnForBut.ForButtonChoice(' '); // UnForBut for ephemeris
//        }

//        void MenuEphSaoClick(object sender)
//        { // to view one object from the satellite
//            UnGloVar.CharForEphem = 'O'; // 'P' for point 'O' for object
//            UnForBut.ForButtonChoice(' '); // UnForBut for ephemeris
//        }

//        void MenuEphSavClick(object sender)
//        {
//            UnGloVar.CharForEphem = 'V'; // 'P' 'O' 'V' for vector condition
//            UnForBut.ForButtonChoice('V'); // UnForBut for ephemeris
//        }

//    }

}
