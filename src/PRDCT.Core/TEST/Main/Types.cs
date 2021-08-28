namespace PRDCT.Core.TEST.Main
{
    public static class Types   // some types with constants and variables
    {


        public static int MaxStarInHeap = 23777;
        public static int MaxObjectInHeap = 23777;
        public static int MaxSatElemInHeap = 23777;


        public struct RecStarCatType
        {
            private readonly int id;
            private readonly int ra, sd; //{ alpha southdelta in mas }
            private readonly int cea, ced;//  : Word ;    //{ difference in epoch 0.01JY }
            private readonly int sea, sed;// : Word ;    //{ units 0.1 mas error }
            private readonly int pma, pmd; //{ 0.1 mas/Year }
            private readonly int sema, semd;// : Word ;    //{ 0.1 mas/Year error }
            private readonly int bt; //{ B photometry 0.001 m }
            private readonly int sbt;//     : Word ;    //{ SE B photometry 0.001 m }
            private readonly int vt; //{ V photometry 0.001 m }
            private readonly int svt;//  : Word ;    //{ SE V photometry 0.001 m }
            private readonly int flags;
        }

        //{ for only bright stars catalogue }
        public struct RecStarAddType
        {
            private readonly int id;
            private readonly int FK5num;// : Word ;
            private readonly int ra, sd; //{ alpha southdelta in mas }
            private readonly int pma, pmd; //{ proper motion 0.01 mas/Year }
            private readonly int bt, vt; //{ B V photometry 0.001 m }
        }

        // for the stars catalogue positions
        public struct TStarOneRec
        {
            private readonly int TycNumber;
            private readonly int TrcNumber;
            private readonly int HipNumber;
            private readonly int FK5Number;//  : Word ;
            private readonly double Alpha;
            private readonly double Delta;
            private readonly double AlphaMotion;
            private readonly double DeltaMotion;
            private readonly double Bt, Vt;
        }

        // for all objects positions in the screen
        public struct TObjectTopRec
        {
            private readonly int nall;
            private readonly char chps; // * P or S
            private readonly string name;
            private readonly double Azt, Alt, Rot; // degree degree km
            private readonly double Mag, phase;
            private readonly byte IndexShadow;
            private readonly bool boovis;
            private readonly bool booscreen;
            private readonly int xpos, ypos;
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
        public static TStarOneRec StarOneCur; // current record
        public static TStarOneRec StarOneFix; // record for star that chosen to fix
                                              //public static StarAllHeap      : TStarAllHeapPtr ;
        public static TObjectTopRec ObjectTopCur;
        // public static ObjectTopHeap    : TObjectTopHeapPtr ;
        //public static ObjectSort       : TObjectSortPtr ;   // to UnitSort and UnSelSat
        //public static IntDimForOrder   : TObjectSortPtr ;   // to UnitSort and UnSelSat
        public static SolarSystemPlanets PlanetNumFix;   // number of planet that chosen to fix
        public static TElemRec SatElemCur; // current record for satellite
        public static TElemRec SatElemFix; // record for satellite that chosen to fix
        public static TElemRec SatElemLook; // record for satellite for looking 'L'
                                            // public static SatElemHeap      : TSatElemHeapPtr ; // heap for satellites elements
        public static int NumObjectInHeap;
        public static int NumFirstSatHeap;
        public static int NumSatElemInHeap;
        public static int NumBrStarsInHeap;
        public static string StrNameTychoCat;

    }
}
