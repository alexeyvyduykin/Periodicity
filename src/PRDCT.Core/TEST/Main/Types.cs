using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDCT.Core.TEST.Main
{
    public static class Types   // some types with constants and variables
    {


        public static int MaxStarInHeap = 23777;
        public static int MaxObjectInHeap = 23777;
        public static int MaxSatElemInHeap = 23777;


        public struct RecStarCatType
        {
            int id;
            int ra, sd; //{ alpha southdelta in mas }
            int cea, ced;//  : Word ;    //{ difference in epoch 0.01JY }
            int sea, sed;// : Word ;    //{ units 0.1 mas error }
            int pma, pmd; //{ 0.1 mas/Year }
            int sema, semd;// : Word ;    //{ 0.1 mas/Year error }
            int bt; //{ B photometry 0.001 m }
            int sbt;//     : Word ;    //{ SE B photometry 0.001 m }
            int vt; //{ V photometry 0.001 m }
            int svt;//  : Word ;    //{ SE V photometry 0.001 m }
            int flags;
        }

        //{ for only bright stars catalogue }
        public struct RecStarAddType
        {
            int id;
            int FK5num;// : Word ;
            int ra, sd; //{ alpha southdelta in mas }
            int pma, pmd; //{ proper motion 0.01 mas/Year }
            int bt, vt; //{ B V photometry 0.001 m }
        }

        // for the stars catalogue positions
        public struct TStarOneRec
        {
            int TycNumber;
            int TrcNumber;
            int HipNumber;
            int FK5Number;//  : Word ;
            double Alpha;
            double Delta;
            double AlphaMotion;
            double DeltaMotion;
            double Bt, Vt;
        }

        // for all objects positions in the screen
        public struct TObjectTopRec
        {
            int nall;
            char chps; // * P or S
            string name;
            double Azt, Alt, Rot; // degree degree km
            double Mag, phase;
            byte IndexShadow;
            bool boovis;
            bool booscreen;
            int xpos, ypos;
        }

        // for the Earth satellites indentification and middle elements
        public struct TElemRec
        {
            public int satnord;  // number from NORAD
            public int satnuml;  // number launch date
            public string satname;
            public double stdmag;   // standard magnitude
            public double t, ao, au, ai, ae, am, an, dn;
        }


        public static TStarOneRec[] TStarAllHeap = new TStarOneRec[MaxStarInHeap];
//TStarAllHeapPtr   = ^TStarAllHeap ;
//   TObjectTopHeap    = Array[1..MaxObjectInHeap] of TObjectTopRec ;
//   TObjectTopHeapPtr = ^TObjectTopHeap ;
//   TSatElemHeap      = Array[1..MaxSatElemInHeap] of TElemRec ;
//   TSatElemHeapPtr   = ^TSatElemHeap ;
//   TObjectSort       = Array[1..MaxObjectInHeap] of LongInt ;
//   TObjectSortPtr    = ^TObjectSort ;

  //{ heap for number , position , proper motion and magnutude }
 public static TStarOneRec StarOneCur        ; // current record
 public static TStarOneRec StarOneFix      ; // record for star that chosen to fix
 //public static StarAllHeap      : TStarAllHeapPtr ;
 public static TObjectTopRec ObjectTopCur    ;
// public static ObjectTopHeap    : TObjectTopHeapPtr ;
//public static ObjectSort       : TObjectSortPtr ;   // to UnitSort and UnSelSat
 //public static IntDimForOrder   : TObjectSortPtr ;   // to UnitSort and UnSelSat
 public static SolarSystemPlanets PlanetNumFix      ;   // number of planet that chosen to fix
  public static TElemRec SatElemCur       ; // current record for satellite
  public static TElemRec SatElemFix       ; // record for satellite that chosen to fix
  public static TElemRec SatElemLook      ; // record for satellite for looking 'L'
 // public static SatElemHeap      : TSatElemHeapPtr ; // heap for satellites elements
  public static int NumObjectInHeap  ;
  public static int NumFirstSatHeap  ;
  public static int NumSatElemInHeap ;
  public static int NumBrStarsInHeap  ;
  public static string StrNameTychoCat   ;

    }
}
