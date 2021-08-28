using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using PRDCT.Core.TEST.Main;

namespace PRDCT.Core.TEST.PUnDE200
{
    //public static class Reductions
    //{
    //    public static dvec3 VelBarEarth;

    //    // { to calculate aberration it is necessary
    //    //   to calculate the position and velosity of the Earth }

    //    public static void ToObtainAberrationParm(double JulUTC)
    //    {
    //        double dt, tdt;
    //        dvec3 PosBar = new dvec3(), VelBar = new dvec3();
    //        JulianDateTime.FromUTCtoTT(JulUTC, out tdt, out dt); // UnForTim time terrestrial
    //        double TinTDB = JulianDateTime.ToGetTEph(tdt);     // UnForTim time ephemeris
    //        TPUnDE200.ClcEphEarth(TinTDB, ref PosBar, ref VelBar); //{ to the barycentre }
    //        if (!TPUnDE200.BooExistPos)
    //            return; //{ no ephemeris }
    //        VelBarEarth = VelBar / 86400.0; //{ to km/s }
    //    }

    //    // { to correct the geocentric vector for aberration }

    //    public static void ForAberrationCorrection(dvec3 PosFix)
    //    {

    //        if (!TPUnDE200.BooExistPos)
    //            return; // boolean from PUnDE200
 
    //        double u = Math.Sqrt(PosFix.x * PosFix.x + PosFix.y * PosFix.y + PosFix.z * PosFix.z);

    //        if (u < 1.0e-10)
    //            return;// { too small }

    //        dvec3 pos = PosFix / u; //{ to unit vector }
    //        dvec3 vos = VelBarEarth / Consts.VelOfLight; // VelOfLight from UnConTyp

    //        double v = vos.x * vos.x + vos.y * vos.y + vos.z * vos.z; //{ V* V }
    //        double b = Math.Sqrt(1 - v); //{ 1/beta = Sqrt(1-V* V) }
    //        double p = pos[1] * vos[1] + pos[2] * vos[2] + pos[3] * vos[3]; //{ scalar multiplication }
    //        double q = p / (1 + b);
    //        double r = 1 / (1 + p);
    //        for (int i = 0; i < 3; i++)
    //        {
    //            pos[i] = r * (b * pos[i] + vos[i] + q * vos[i]); //{ the unit vector }
    //            PosFix[i] = u * pos[i]; //{ to initial distance }
    //        }
    //    }

    //}

    public static class Refraction
    {
        //{ procedure to calculate the refraction correction
        //has been presented by doctor Konstantin V.Kuimov}

        static double wlmkm; //{ length of wave}
        static double temprc; //{ temperature in degree Celsius }
        static double prepas; //{ pressure in pascal 760 mm = 101325 Pa }
        static double humrun; //{ humidity in unit interval[0, 1] }

        public static void SimpleDataForRefraction()
        {
            // { initial value }
            wlmkm = 0.5931; //{ lehgtn of wave in mkm }
            temprc = 0.0; //{ in degree of Celsius }
            prepas = 99325.158; //{ in pascal 745 in mm }
            humrun = 0.9; //{ relative humidity is 90 % }
        }

        //public static void ToGetDataForRefraction()
        //{
        //    //{ initial value }
        //    wlmkm = 0.5931; //{ in mkm }
        //    double tempk = 273.15; //{ in Kelvin }
        //    double presh = 745.0; //{ in mm }
        //    double hum = 90.0; //{ in % }
        //    Assign(ft, 'RefrParm.Dat');
        //    // {$I-}
        //    ReSet(ft);
        //    // {$I+}
        //    if (IOResult == 0)

        //    {
        //        if (!EOF(ft)) ReadLn(ft, tempk);
        //        if (!EOF(ft)) ReadLn(ft, presh);
        //        if (!EOF(ft)) ReadLn(ft, hum);
        //        Close(ft);
        //    }
        //    temprc = tempk - 273.15; //{ to Celsius }
        //    prepas = 101325.0 * (presh / 760.0); //{ pressure in pascal }
        //    humrun = hum / 100.0; //{ to unit interval }
        //}

        public static void RefrOlik(double wlmkm, double temp, double ppas, double hum, double z, out double ref_)
        {
            double R = 8314.72e0;  //{ Ј §®ў п Ї®бв®п­­ п, ¤¦/(Љ* Є¬®«м) }
            double rw = 8314.72e0 / 18.0152e0; //{ Ј §®ў п Ї®бв®п­­ п/¬®«.ўҐб ў®¤л }
            double g0 = 9.80665e0; //{ гбЄ®аҐ­ЁҐ бЁ«л вп¦ҐбвЁ, ¬/б**2 }
            double Dm = 28.9644e0; //{ ¬®«па­ п ¬ бб бге®Ј® ў®§¤ге , ЄЈ/¬®«м }
            double r0 = 6371000.0e0;  //{ баҐ¤­Ё© а ¤Ёгб ‡Ґ¬«Ё, ¬ }
            double sqpi2 = 0.8862269254527580e0; //{ sqrt(pi)/2 }

            double Tabs, tabs2, tabs3, t, roln, pw, Ps,
             Ds, Dw, s2, s4, s6, alpha0, b0, a, x, y;

            Tabs = temp + 273.15e0; //{  Ўб®«ов­ п вҐ¬ЇҐа вга }
            t = temp;               //{ вҐ¬ЇҐа вга Ї® –Ґ«мбЁо }
            roln = -5.32917e0 + t * (0.0688825e0 + t * (-2.9815e-4
            + 1.39e-6 * t)); //{ ­ в. «®Ј аЁд¬ Ї«®в­®бвЁ ў®¤п­®Ј® Ї а(б¬. [2]) }
            pw = hum * rw * Tabs * Math.Exp(roln); //{ ¤ ў«Ґ­ЁҐ ў®¤п­®Ј® Ї а }
            Ps = (ppas - pw) / 100; //{ ўла §Ёвм ў ¬Ё««ЁЎ ае ¤ ў«Ґ­ЁҐ бге®Ј® ў®§¤ге  }
            pw = pw / 100;          //{ ўла §Ёвм ў ¬Ё««ЁЎ ае ¤ ў«Ґ­ЁҐ ў®¤п­®Ј® Ї а }
            tabs2 = Tabs * Tabs;
            tabs3 = tabs2 * Tabs;
            Ds = Ps / Tabs * (1 + Ps * (57.90e-8 - 9.3250e-4 / Tabs + 0.25844e0 / tabs2));
            Dw = pw / Tabs * (1 + pw * (1 + 3.7e-4 * pw)
                  * (-2.37321e-3 + 2.23366e0 / Tabs - 710.792e0 / tabs2
                  + 7.75141e4 / tabs3));
            s2 = 1 / (wlmkm * wlmkm);
            s4 = s2 * s2;
            s6 = s2 * s4;
            alpha0 = ((2371.34e0 + 683939.7e0 / (130 - s2) + 4547.3e0 / (38.9e0 - s2)) * Ds
                        + (6487.31e0 + 58.058e0 * s2 - 0.71150e0 * s4 + 0.08851e0 * s6) * Dw) / 1.0e8;
            b0 = R * Tabs / (g0 * Dm * r0);           //{ beta0 }
            a = b0 - alpha0 / 2;
            x = Math.Cos(z) / Math.Sqrt(2 * a);
            t = 1.0e0 - 7.5e0 / (Math.Abs(x) + 3.75e0);   //{ NAG13 s15adf.for }
            y = (((((((((((((((+3.328130055126039e-10
                 * t - 5.718639670776992e-10) * t - 4.066088879757269e-9)
                 * t + 7.532536116142436e-9) * t + 3.026547320064576e-8)
                 * t - 7.043998994397452e-8) * t - 1.822565715362025e-7)
                 * t + 6.575825478226343e-7) * t + 7.478317101785790e-7)
                 * t - 6.182369348098529e-6) * t + 3.584014089915968e-6)
                 * t + 4.789838226695987e-5) * t - 1.524627476123466e-4)
                 * t - 2.553523453642242e-5) * t + 1.802962431316418e-3)
                 * t - 8.220621168415435e-3) * t + 2.414322397093253e-2;
            y = (((((y * t - 5.480232669380236e-2) * t + 1.026043120322792e-1)
                 * t - 1.635718955239687e-1) * t + 2.260080669166197e-1)
                 * t - 2.734219314954260e-1) * t + 1.455897212750385e-1;
            ref_ = alpha0 * (1 - alpha0 / 2) * Math.Sqrt(2 / a) * Math.Sin(z) * y * sqpi2;
        }

        public static double ToGetRefraction(double alt)
        {
            double ref_ = 0;
            double z = 90 - alt;
            if (z > 90.0) z = 90.0;// { if altitude< 0 degree }
            if (z < 0.00) z = 0.00;
            if ((0.0 <= z) && (z <= 90.0))


            {
                double zr = MyMath.DegreesToRadians * z; //{ to radian }
                RefrOlik(wlmkm, temprc, prepas, humrun, zr, out ref_); //{ ref in radian }
            }
            return MyMath.RadiansToDegrees * ref_; //{ in degree }
        }

    }
    }
