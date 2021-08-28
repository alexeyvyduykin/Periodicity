using System;
using System.Collections.Generic;
using GlmSharp;

namespace PRDCT.Core.TEST.Main
{
    public static class Global
    {

        //{ for the different coordinates system }
        public struct TAllSystemRec
        {
            private readonly double ro;
            private readonly double ah, hh; //{ horizontal}
            private readonly double aa, da; //{ equator }
            private readonly double le, be; //{ ecliptic }
            private readonly double lg, bg;
        }

        public static dmat3 MatrUnit = new dmat3(
    new dvec3(1.0, 0.0, 0.0),
    new dvec3(0.0, 1.0, 0.0),
    new dvec3(0.0, 0.0, 1.0));
        // { to change from galactic to equatorial system from HIP v.1,p.92 }
        public static dmat3 MatrGal = new dmat3
                  (new dvec3(-0.0548755604, +0.4941094279, -0.8676661490),
                    new dvec3(-0.8734370902, -0.4448296300, -0.1980763734),
                    new dvec3(-0.4838350155, +0.7469822445, +0.4559837762));

        public static double[] TypeFundArg = new double[5];

        public struct
          TPlaceCooRec
        {
            public int num;
            public string name;
            public double x, y, z; // position in metr
            public double f, l, h; // lat long degree h metr
            public double c, d, s; // hour : zone decret summer
        }


        public struct TEclipseRec
        {
            private vec3 centreshad;
            private readonly double fullshadow;
            private readonly double semishadow;
        }

        private static readonly int NumberOfStars;
        private static readonly int NumStarsInRect;
        private static readonly int SmallStarSize;


        public static bool BooAddInform;
        public static bool BooNegative;
        public static bool BooReticulum;
        public static bool BooFixObject;
        public static bool BooPlanetLook;
        public static bool BooApp,
   BooApprox,
   BooRiSetP,
   BooSatElem,
   BooExistSat,
   BooDayNight,
   BooHourImage,
   BooRefraction,
   BooAutoControl;


        public static bool BooRusLat,
   BooAzNorthS,
   BooAlphaHourA;
        public static char CharDayNight,
   CharDayNightMem;// { D - day , T - twilight , N - night }
        public static string StrDayNight,
   StrDayNightMem;
        public static string StrRefSystem;
        public static double TheSunHight; //{ the hight of the sun under horizon }
        public static string StrTheSunHight;
        public static char CharReferSystem,
   CharRefSysteMem,
   CharTimeSystem;
        public static double DecreTMoscow;
        public static double SummerHour; //{ for one hour jump of time if exists }
        public static double StepWithTime;
        public static double SizeForArea;


        public static double JulianDate; // the main variable for time
        public static double LocalSTime; // local sidereal time
        public static double DHourImage;
        public static dvec3 StationPos; //{ in the true equatorial system }
        public static dmat3 MatrEcl; // from fixed equator to ecliptic
        public static dmat3 PrecMatr; // precession matrix
        public static dmat3 MatrNut; // nutation matrix
        public static dmat3 RotMatr; // from fixed to true equator
        public static mat3 TopMatr; // from true to horizon or orbital
        public static mat3 RosMatr; // true sidereal time matrix
        public static TPlaceCooRec PlaceCoor; // information about station 
        public static double RinDegC; // general radius of field of view
        public static double XinDegC, YinDegC; // current scale for X Y axis
        public static double AzimutC; // horizon degree centre image
        public static double AltitudeC; // horizon degree centre image
        public static vec3 PosHorizoC;   // centre horizon  but descart
        public static double AlphaC, DeltaC; // equator degree centre image
        public static vec3 PosZemelaC;   // orbital centre of the Earth
        public static double AngleZemeL; // 'L' the Earth from satellite
        public static double SurfaRange; // special range from satellite
        public static double DecretTime;
        public static double DCivilTime;
        public static bool BooSummerJ;  // summer hour yes or no

        // is called PUnCRead UnitMain 
        public static Single VMagMin, VMagMax;   // to limit stars magnitude
        public static Single VAllMin, VAllMax;   // stars magnitude

        public static TEclipseRec VEclipse;
        public static bool BooCircle;


        public static char CharForView; // 'S' for sky 'W' for world map and 'L'
        public static char CharForEphem; // 'P' for point 'O' for object
        public static char CharBodEph; // 'S' satellite 'P' planet as object
        public static int NumberBody; // number of choosed planet in case 'O'
        public static char chElemFormat; // 'N' NORAD 'I' IRVS 'K' KDr 'M' no
        public static string stElementFile; // name of file with elements
        public static string stCatalogFile; // name of catalogue
        public static string stPathCat; // for path to folder with catalogue
        public static string stPathElem; // for path to folder with elements
        public static string stPathResult; // for path to folder for result
        public static string stPathDefault; // for path to folder with program
        public static string stPathSite; // for path to folder with site position

        private static readonly int MaxNameValue = 97; // to select step , area size and others

        public struct TNameValue
        {
            private readonly string strname;
            private readonly double valname;
        }

        public static int NumNameValue; // current maximum number
        public static int NumStatValue; // current number in list station
        public static int NumStepValue; // current number in list of time step
        public static int NumSizeValue; // current number in list of area size
        public static List<TNameValue> NameValue = new List<TNameValue>();

        // variable for map world
        public static byte MapSatNumber;  // less equal 8 and 2 for the Sun the Moon
        public static vec3[] MapSatGeoPos = new vec3[10];   // for position
        public static double[] MapSatAltitude = new double[10]; // for altitude in degree
        public static byte[] MapSatShadow = new byte[10];     // o 1 2
        public static int[] MapSatX, MapSatY = new int[10];  // for position of imagesat
        public static double[] RangeSatMap = new double[10]; // UnGraAll for range 'W'
        public static Types.TElemRec[] SatElemMap = new Types.TElemRec[10]; // elements
        public static bool BooMapPoint;

        public static int xpMouse;
        public static int ypMouse;


        public static bool boomaif; //{ false if fmPuz not active }
    }

}
