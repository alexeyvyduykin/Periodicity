using System;
using System.Collections.Generic;
using PRDCT.Core.TEST.Main;

namespace PRDCT.Core.TEST.PUnDE200
{
    public static class Nutations
    {

        // { ClcFundArg is a procedure
        //   to calculate Fundamental Arguments in radians
        //      on moment TCur in julian day.
        //      FundArg[1..5] is massive of the values of arguments.
        //      global const - GraRad - for change from degrees to radians. }

        public static void ClcFundArg(double TCur, ref double[] FundArg)
        {
            double dt = (TCur - Consts.JD2000) / Consts.JulianC;
            double dt2 = dt * dt;
            double dt3 = dt * dt2;

            FundArg[0] = 218.31643250e0 + 481267.8812772222e0 * dt
                                         - 0.00161167e0 * dt2 + 0.00000528e0 * dt3;
            FundArg[1] = 134.96298139e0 + 477198.8673980556e0 * dt
                                         + 0.00869722e0 * dt2 + 0.00001778e0 * dt3;
            FundArg[2] = 357.52772333e0 + 35999.05034e0 * dt
                                         - 0.00016028e0 * dt2 - 0.00000333e0 * dt3;
            FundArg[3] = 93.27191028e0 + 483202.0175380555e0 * dt
                                         - 0.00368250e0 * dt2 + 0.00000306e0 * dt3;
            FundArg[4] = 297.85036306e0 + 445267.11148e0 * dt
                                         - 0.00191417e0 * dt2 + 0.00000528e0 * dt3;
            for (int i = 0; i < 5; i++)
            {
                double r = FundArg[i];
                int l = (int)Math.Truncate(r / 360.0e0);
                FundArg[i] = MyMath.DegreesToRadians * (r - 360.0e0 * l);
            }
        }

        // { ClcNut is a procedure
        //   to calculate the parameters of nutation in longitude and obliquity
        //      and value of the mean obliquity in radians on the moment TCur.
        //      the arguments and coefficients
        //          of nutation theory are contained in the NutArg and CoefNut.
        //      global parameter - SecRad - for change from arcsecs to radians. }

        public static void ClcNut(double TCur, out double DeltaPsi, out double DeltaEps, out double EpsMean)
        {
            double[] FundArg = new double[5];

            #region NutArg

            //{ 1980 IAU Theory of Nutation(J.M.Wahr) }
            List<int[]> NutArg = new List<int[]>//[106, 5]() //{ arguments }
            {
               new int[] { 1, 0, 0, 0, 0 },
              new int[] { 2, 0, 0, 0, 0 },
              new int[] {1 ,   -2 ,    0 ,    2 ,    0 },
              new int[] {0 ,    2 ,    0 ,   -2 ,    0 } ,
              new int[] {2 ,   -2 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    1 ,   -1 ,    0 ,   -1 } ,
              new int[] {1 ,    0 ,   -2 ,    2 ,   -2 } ,
              new int[] {1 ,    2 ,    0 ,   -2 ,    0 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,   -2 } ,
              new int[] {0 ,    0 ,    1 ,    0 ,    0 } ,
              new int[] {2 ,    0 ,    1 ,    2 ,   -2 } ,
              new int[] {2 ,    0 ,   -1 ,    2 ,   -2 } ,
              new int[] {1 ,    0 ,    0 ,    2 ,   -2 } ,
              new int[] {0 ,    2 ,    0 ,    0 ,   -2 } ,
              new int[] {0 ,    0 ,    0 ,    2 ,   -2 } ,
              new int[] {0 ,    0 ,    2 ,    0 ,    0 } ,
              new int[] {1 ,    0 ,    1 ,    0 ,    0 } ,
              new int[] {2 ,    0 ,    2 ,    2 ,   -2 } ,
              new int[] {1 ,    0 ,   -1 ,    0 ,    0 } ,
              new int[] {1 ,   -2 ,    0 ,    0 ,    2 } ,
              new int[] {1 ,    0 ,   -1 ,    2 ,   -2 } ,
              new int[] {1 ,    2 ,    0 ,    0 ,   -2 } ,
              new int[] {1 ,    0 ,    1 ,    2 ,   -2 } ,
              new int[] {0 ,    1 ,    0 ,    0 ,   -1 } ,
              new int[] {0 ,    2 ,    1 ,    0 ,   -2 } ,
              new int[] {1 ,    0 ,    0 ,   -2 ,    2 } ,
              new int[] {0 ,    0 ,    1 ,   -2 ,    2 } ,
              new int[] {2 ,    0 ,    1 ,    0 ,    0 } ,
              new int[] {1 ,   -1 ,    0 ,    0 ,    1 } ,
              new int[] {0 ,    0 ,    1 ,    2 ,   -2 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    1 ,    0 ,    0 ,    0 } ,
              new int[] {1 ,    0 ,    0 ,    2 ,    0 } ,
              new int[] {2 ,    1 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    1 ,    0 ,    0 ,   -2 } ,
              new int[] {2 ,   -1 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    0 ,    0 ,    0 ,    2 } ,
              new int[] {1 ,    1 ,    0 ,    0 ,    0 } ,
              new int[] {1 ,   -1 ,    0 ,    0 ,    0 } ,
              new int[] {2 ,   -1 ,    0 ,    2 ,    2 } ,
              new int[] {1 ,    1 ,    0 ,    2 ,    0 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,    2 } ,
              new int[] {0 ,    2 ,    0 ,    0 ,    0 } ,
              new int[] {2 ,    1 ,    0 ,    2 ,   -2 } ,
              new int[] {2 ,    2 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    0 ,    0 ,    2 ,    0 } ,
              new int[] {1 ,   -1 ,    0 ,    2 ,    0 } ,
              new int[] {1 ,   -1 ,    0 ,    0 ,    2 } ,
              new int[] {1 ,    1 ,    0 ,    0 ,   -2 } ,
              new int[] {1 ,   -1 ,    0 ,    2 ,    2 } ,
              new int[] {0 ,    1 ,    1 ,    0 ,   -2 } ,
              new int[] {2 ,    0 ,    1 ,    2 ,    0 } ,
              new int[] {2 ,    0 ,   -1 ,    2 ,    0 } ,
              new int[] {2 ,    1 ,    0 ,    2 ,    2 } ,
              new int[] {0 ,    1 ,    0 ,    0 ,    2 } ,
              new int[] {2 ,    2 ,    0 ,    2 ,   -2 } ,
              new int[] {1 ,    0 ,    0 ,    0 ,    2 } ,
              new int[] {1 ,    0 ,    0 ,    2 ,    2 } ,
              new int[] {1 ,    1 ,    0 ,    2 ,   -2 } ,
              new int[] {1 ,    0 ,    0 ,    0 ,   -2 } ,
              new int[] {0 ,    1 ,   -1 ,    0 ,    0 } ,
              new int[] {1 ,    2 ,    0 ,    2 ,    0 } ,
              new int[] {0 ,    0 ,    1 ,    0 ,   -2 } ,
              new int[] {0 ,    1 ,    0 ,   -2 ,    0 } ,
              new int[] {0 ,    0 ,    0 ,    0 ,    1 } ,
              new int[] {0 ,    1 ,    1 ,    0 ,    0 } ,
              new int[] {0 ,    1 ,    0 ,    2 ,    0 } ,
              new int[] {2 ,    1 ,   -1 ,    2 ,    0 } ,
              new int[] {2 ,   -1 ,   -1 ,    2 ,    2 } ,
              new int[] {1 ,   -2 ,    0 ,    0 ,    0 } ,
              new int[] {2 ,    3 ,    0 ,    2 ,    0 } ,
              new int[] {2 ,    0 ,   -1 ,    2 ,    2 } ,
              new int[] {2 ,    1 ,    1 ,    2 ,    0 } ,
              new int[] {1 ,   -1 ,    0 ,    2 ,   -2 } ,
              new int[] {1 ,    2 ,    0 ,    0 ,    0 } ,
              new int[] {2 ,    1 ,    0 ,    0 ,    0 } ,
              new int[] {0 ,    3 ,    0 ,    0 ,    0 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,    1 } ,
              new int[] {2 ,   -1 ,    0 ,    0 ,    0 } ,
              new int[] {0 ,    1 ,    0 ,    0 ,   -4 } ,
              new int[] {2 ,   -2 ,    0 ,    2 ,    2 } ,
              new int[] {2 ,   -1 ,    0 ,    2 ,    4 } ,
              new int[] {0 ,    2 ,    0 ,    0 ,   -4 } ,
              new int[] {2 ,    1 ,    1 ,    2 ,   -2 } ,
              new int[] {1 ,    1 ,    0 ,    2 ,    2 } ,
              new int[] {2 ,   -2 ,    0 ,    2 ,    4 } ,
              new int[] {2 ,   -1 ,    0 ,    4 ,    0 } ,
              new int[] {0 ,    1 ,   -1 ,    0 ,   -2 } ,
              new int[] {1 ,    2 ,    0 ,    2 ,   -2 } ,
              new int[] {2 ,    2 ,    0 ,    2 ,    2 } ,
              new int[] {1 ,    1 ,    0 ,    0 ,    2 } ,
              new int[] {2 ,    0 ,    0 ,    4 ,   -2 } ,
              new int[] {2 ,    3 ,    0 ,    2 ,   -2 } ,
              new int[] {0 ,    1 ,    0 ,    2 ,   -2 } ,
              new int[] {1 ,    0 ,    1 ,    2 ,    0 } ,
              new int[] {1 ,   -1 ,   -1 ,    0 ,    2 } ,
              new int[] {1 ,    0 ,    0 ,   -2 ,    0 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,   -1 } ,
              new int[] {0 ,    0 ,    1 ,    0 ,    2 } ,
              new int[] {0 ,    1 ,    0 ,   -2 ,   -2 } ,
              new int[] {1 ,    0 ,   -1 ,    2 ,    0 } ,
              new int[] {1 ,    1 ,    1 ,    0 ,   -2 } ,
              new int[] {0 ,    1 ,    0 ,   -2 ,    2 } ,
              new int[] {0 ,    2 ,    0 ,    0 ,    2 } ,
              new int[] {2 ,    0 ,    0 ,    2 ,    4 } ,
              new int[] {0 ,    0 ,    1 ,    0 ,    1 } };

            #endregion

            #region CoefNut

            //{ 1980 IAU Theory of Nutation(J.M.Wahr) }
            List<double[]> CoefNut = new List<double[]>//: Array[1..106, 1..4] of Single = { amplitudes }
            {
                new double[] { -17.199600 ,     -0.017420 ,      9.202500 ,      0.000890 } ,
      new double[] { 0.206200 ,      0.000020 ,     -0.089500 ,      0.000050 } ,
      new double[] { 0.004600 ,      0.000000 ,     -0.002400 ,      0.000000 } ,
      new double[] { 0.001100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] { -0.000300 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] { -0.000300 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] { -0.000200 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] { 0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] { -1.318700 ,     -0.000160 ,      0.573600 ,     -0.000310 } ,
      new double[] { 0.142600 ,     -0.000340 ,      0.005400 ,     -0.000010 } ,
      new double[] { -0.051700 ,      0.000120 ,      0.022400 ,     -0.000060 } ,
      new double[] { 0.021700 ,     -0.000050 ,     -0.009500 ,      0.000030 } ,
      new double[] { 0.012900 ,      0.000010 ,     -0.007000 ,      0.000000 } ,
      new double[] { 0.004800 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] { -0.002200 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] { 0.001700 ,     -0.000010 ,      0.000000 ,      0.000000 } ,
      new double[] { -0.001500 ,      0.000000 ,      0.000900 ,      0.000000 } ,
      new double[] { -0.001600 ,      0.000010 ,      0.000700 ,      0.000000 } ,
      new double[] {-0.001200 ,      0.000000 ,      0.000600 ,      0.000000 } ,
      new double[] {-0.000600 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {-0.000500 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {0.000400 ,      0.000000 ,     -0.000200 ,      0.000000 } ,
      new double[] {0.000400 ,      0.000000 ,     -0.000200 ,      0.000000 } ,
      new double[] {-0.000400 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.227400 ,     -0.000020 ,      0.097700 ,     -0.000050 } ,
      new double[] {0.071200 ,      0.000010 ,     -0.000700 ,      0.000000 } ,
      new double[] {-0.038600 ,     -0.000040 ,      0.020000 ,      0.000000 } ,
      new double[] {-0.030100 ,      0.000000 ,      0.012900 ,     -0.000010 } ,
      new double[] {-0.015800 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {0.012300 ,      0.000000 ,     -0.005300 ,      0.000000 } ,
      new double[] {0.006300 ,      0.000000 ,     -0.000200 ,      0.000000 } ,
      new double[] {0.006300 ,      0.000010 ,     -0.003300 ,      0.000000 } ,
      new double[] {-0.005800 ,     -0.000010 ,      0.003200 ,      0.000000 } ,
      new double[] {-0.005900 ,      0.000000 ,      0.002600 ,      0.000000 } ,
      new double[] {-0.005100 ,      0.000000 ,      0.002700 ,      0.000000 } ,
      new double[] {-0.003800 ,      0.000000 ,      0.001600 ,      0.000000 } ,
      new double[] {0.002900 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {0.002900 ,      0.000000 ,     -0.001200 ,      0.000000 } ,
      new double[] {-0.003100 ,      0.000000 ,      0.001300 ,      0.000000 } ,
      new double[] {0.002600 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {0.002100 ,      0.000000 ,     -0.001000 ,      0.000000 } ,
      new double[] {0.001600 ,      0.000000 ,     -0.000800 ,      0.000000 } ,
      new double[] {-0.001300 ,      0.000000 ,      0.000700 ,      0.000000 } ,
      new double[] {-0.001000 ,      0.000000 ,      0.000500 ,      0.000000 } ,
      new double[] {-0.000700 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000700 ,      0.000000 ,     -0.000300 ,      0.000000 } ,
      new double[] {-0.000700 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {-0.000800 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {0.000600 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000600 ,      0.000000 ,     -0.000300 ,      0.000000 } ,
      new double[] {-0.000600 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {-0.000700 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {0.000600 ,      0.000000 ,     -0.000300 ,      0.000000 } ,
      new double[] {-0.000500 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {0.000500 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000500 ,      0.000000 ,      0.000300 ,      0.000000 } ,
      new double[] {-0.000400 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000400 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000400 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000300 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000300 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000300 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000300 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000200 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000300 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000300 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {0.000200 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000200 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {0.000200 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000200 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {0.000200 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000200 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000200 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000100 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,     -0.000100 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {-0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } ,
      new double[] {0.000100 ,      0.000000 ,      0.000000 ,      0.000000 } };

            #endregion


            double dt = (TCur - Consts.JD2000) / Consts.JulianC; // from UnConTyp
            double dt2 = dt * dt;
            double dt3 = dt * dt2;
            EpsMean = MyMath.SecondsToRadians * (84381.448 - 46.8150 * dt - 0.00059 * dt2 + 0.001813 * dt3);
            ClcFundArg(TCur, ref FundArg);
            DeltaPsi = 0.0e0;
            DeltaEps = 0.0e0;
            for (int k = 0; k < 106; k++)
            {
                double r = NutArg[k][0] * (FundArg[0] - FundArg[3]);
                for (int i = 1; i < 5; i++)
                {
                    r = r + NutArg[k][i] * FundArg[i];
                }

                DeltaPsi = DeltaPsi + (CoefNut[k][0] + dt * CoefNut[k][1]) * Math.Sin(r);
                DeltaEps = DeltaEps + (CoefNut[k][2] + dt * CoefNut[k][3]) * Math.Cos(r);
            }
            DeltaPsi = MyMath.SecondsToRadians * DeltaPsi; //{ in radian }
            DeltaEps = MyMath.SecondsToRadians * DeltaEps;
        }

    }
}
