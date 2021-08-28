using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using PRDCT.Core.TEST.Main;

namespace PRDCT.Core.TEST.PUnDE200
{
//    public static class TPUnDE200 //  { for popular ephemeris Jet Propulsion Laboratory}
//    {
//        public static double MassRatio = 0.012300034;
//        public static double CoefMassRatio = MassRatio / (1 + MassRatio);

//        public static int NumberOfPlanets = 11;
//        public static double[] RadiusOfPlanet = new double[]
//        {     2440.0 , 6052.0 , 6378.0 , 3394.0 , 71400.0 , 60000.0 , 25900.0 , 24800.0 , 2000.0 , 1737.0 , 690990.0 };

//        public static int[] NumCoefOrd = new int[] { 2, 146, 182, 272, 302, 329, 353, 377, 395, 413, 701 };
//        public static int[] NumbofCoef = new int[] { 12, 12, 15, 10, 9, 8, 8, 6, 6, 12, 15 };
//        public static int[] NumbofDay = new int[] { 8, 32, 16, 32, 32, 32, 32, 32, 32, 4, 32 };
//        public static int[] NumbofCoor = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
//      //  public static int NMercury = 1;
//      //  public static int NVenus = 2;
//      //  public static int NEarth = 3;
//      //  public static int NMars = 4;
//     //   public static int NJupiter = 5;
//     //   public static int NSaturn = 6;
//     //   public static int NUranus = 7;
//      //  public static int NNeptune = 8;
//      //  public static int NPluto = 9;
//      //  public static int NMoon = 10;
//     //   public static int NSun = 11;

//        public static string[] PlanetNameL =
//                        { "Mercury" , "Venus" , "Earth" , "Mars" ,
//                     "Jupiter" , "Saturn" , "Uranus" , "Neptune" ,
//                                 "Pluto" , " Moon" , "Sun" };

//        public static string[] PlanetNameR =
//                   { "Меркурий" , "Венера" , "Земля" , "Марс" ,
//                       "Юпитер" , "Сатурн" ,  "Уран" , "Нептун" ,
//                                  "Плутон" ,  "Луна" , "Солнце" };

//        // { to read ephemeris DE200 }

//        public static double[] CoefEph; // double[745]
//        public static string CoeFileName;
//        public static double TinitEph, TlastEph;
//        public static int NofRec, irecmax;
//        public static bool BooOpenCoeFile, BooExistEph, BooExistPos;

//        public static dvec3 PosTopSun;
//        public static List<dvec3> PosDim;
//        public static double MagVOneP;
//        public static double PhasOneP;
//   //     public static List<TAllSystemRec> PosSystem;

//        // { ClcPosVel is a procedure
//        //   to obtain the position of the Planet
//        //      reffered to the barycentre from DE200/LE200 Ephemeris }

//        public static void ClcPosVel(SolarSystemPlanets NPlanet, double TinTDB, ref dvec3 Pos, ref dvec3 Vel)
//        //Var
//        //  PolQuanT, PolQuanU : array[0..15] of Extended;
//        //  xr,xv : Extended;
//        //  i,i1,i2,j : integer;
//        {
//            double[,] CoefQuant = new double[3, 15];
//            double[] PolQuanT = new double[15];
//            double[] PolQuanU = new double[15];
//    //        ToReadEphemeris(TinTDB);             !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//            if( ! BooExistPos)             
//       return;
//            int k = NumCoefOrd[(int)NPlanet];
//            int l = NumbofCoef[(int)NPlanet];
//            int l0 = l - 1;
//            int m = NumbofDay[(int)NPlanet];
//            double w = m;
//            double rw = 2 / w;
//            double t = TinTDB;
//            double Tn = CoefEph[1];
//            double Tk = Tn + 32;
//            double r = (t - Tn) / m;
//            int n = (int)Math.Truncate(r - 1.0e-10);
//            int np = k + 3 * l * n - 1;
//            double Tnn = Tn + m * n;
//            double Tkk = Tnn + m;
//            double x = -1 + (t - Tnn) * rw;
//            double xw = 2 * x;
//            for (int i = 0; i < 3; i++)
//                for (int j = 0; j < l0; j++)
//                {
//                    np = np + 1;
//                    CoefQuant[i, j] = CoefEph[np];
//                }
//            PolQuanT[0] = 1;
//            PolQuanT[1] = x;
//            PolQuanU[0] = 1;
//            PolQuanU[1] = xw;
//            for (int i = 2; i < l; i++)
//            {
//                int i1 = i - 1;
//                int i2 = i - 2;
//                PolQuanT[i] = xw * PolQuanT[i1] - PolQuanT[i2];
//                PolQuanU[i] = xw * PolQuanU[i1] - PolQuanU[i2];
//            }
//            for (int i = 0; i < 3; i++)
//            {
//                double xr = CoefQuant[i, 0];
//                double xv = 0.0;
//                for (int j = 0; j < l0; j++)
//                {
//                    xr = xr + CoefQuant[i, j] * PolQuanT[j];
//                    xv = xv + j * CoefQuant[i, j] * PolQuanU[j - 1];
//                }
//                Pos[i] = xr;
//                Vel[i] = rw * xv;
//            }
//        }

// //       { ToGetCoeFileName is a procedure
// //         to define name of the file for DE200/LE200 ephemeris }

//    public static void ToGetCoeFileName()// { all variables are external }
//    {
//        int numfile = 3;
//        string[] namefile = { "EphTheoL.dat", "EphemAll.jpl", "Ephem11.jpl" };
//        int pos, ior;
//        BooOpenCoeFile = false;
//        BooExistEph = false;
//        BooExistPos = false;
//        int num = 0;
//        do
//        {
//            num = num + 1;
//            string stn = namefile[num];
//            CoeFileName = stn;
//            Assign(CoeFile, CoeFileName);
//            //  {$I -}
//            ReSet(CoeFile);
//            //  {$I +}
//            ior = IOResult;
//            if (Enumerable.Range(0, 5).Contains(ior))

//                BooExistEph = true;
//            else
//            {
//                ToUseSomePath(stn, CoeFileName, boo);
//                if (boo)

//                {
//                    Assign(CoeFile, CoeFileName);
//                    // {$I -}
//                    ReSet(CoeFile);
//                    //  {$I +}
//                    ior = IOResult;
//                    if (Enumerable.Range(0, 5).Contains(ior))

//                        BooExistEph = true;
//                }
//            }
//            if (ior == 5)
//            //{ file read only }
//            {
//                FileMode = 0;
//                ReSet(CoeFile);// { file is opened for reading }
//                FileMode = 2;
//            }
//        }
//        while ((BooExistEph) || (num == numfile));
//        if (!BooExistEph)

//            //{ no ephemeris file DE200/LE200 }
//            return;
//        // { file is opened DE200/LE200 }
//        Pos = 0;
//        Seek(CoeFile, Pos);
//        Read(CoeFile, CoefEph);
//        TinitEph = CoefEph[1];
//        Pos = FileSize(CoeFile) - 1;
//        Seek(CoeFile, Pos);
//        Read(CoeFile, CoefEph);
//        TlastEph = CoefEph[1] + 32;
//        BooOpenCoeFile = true;
//        NofRec = -1;
//        irecmax = FileSize(CoeFile) - 1;
//    }

//   //       { to read file with the DE200 data
//}

//public static void ToReadEphemeris(double TinTDB)
//{
//    BooExistPos = false; //{ previous }
//    if (!BooOpenCoeFile)

//                  ToGetCoeFileName();  
//        if (!BooExistEph)

//            //{ no ephemeris file DE200/LE200 }
//            return;
//    if ((TinitEph <= TinTDB) && (TinTDB <= TlastEph))
//    {
//        double r = (TinTDB - TinitEph) / 32.0e0;
//        int irec = (int)Math.Truncate(r);
//        if (irec > irecmax)
//            irec = irecmax;
//        if (irec != NofRec)
//        {
//            Seek(CoeFile, irec);
//            Read(CoeFile, CoefEph);
//            NofRec = irec;
//        }
//        BooExistPos = true; //{ there are ephemeris data }
//    }
//    // { DE200/LE200 Ephemeris has been read in massive CoefEph(745) }
//}

//   //      { ClcEphEarth is a procedure
//   //        to obtain the position of the Earth reffered to the barycenter
//   //           of the Solar system from DE200/LE200 Ephemeris }

//public static void ClcEphEarth(double TinTDB, ref dvec3 PosEarth, ref dvec3 VelEarth)
//        {
//            dvec3 PosMoon = new dvec3(), VelMoon = new dvec3(),
//          PosBarEM = new dvec3(), VelBarEM = new dvec3();
//            ClcPosVel( SolarSystemPlanets.Moon, TinTDB, ref PosMoon, ref VelMoon);
//            if (!BooExistPos)
//                //{ no ephemeris for this date TinTDB }
//                return;
//            ClcPosVel(SolarSystemPlanets.Earth, TinTDB, ref PosBarEM, ref VelBarEM);

//            PosEarth = PosBarEM - CoefMassRatio * PosMoon;
//            VelEarth = VelBarEM - CoefMassRatio * VelMoon;

//        }

//    }

}
