using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using PRDCT.Core.TEST.PUnDE200;
using PRDCT.Core.TEST.Main;

namespace PRDCT.Core.TEST.SolarSystemPlanet
{

   // public static class SSPPosition // solar system planets
   // {
   //     public static double JulianDate;
   //     public static dmat3 RotMatr, RosMatr, TopMatr, PrecMatr, MatrNut;

   //     public static char CharForView;

   //     //  UnCotVar , // for types
   //     //  UnForCoo , // for procedure ClcTopCoorInDegree ClcThreeRotMatr
   //     //  UnitSort , // for procedure ShellMethodForOrdering
   //     //  UnPlaceA , // for RectCoordinates
   //     //  UnPlateC , // for IdealCoordinates
   //     //  UnGraAll , // for procedure FigurePlanet
   //     //  UnButIni ; // for SpecButt[21] and HintButt[21] day twilight night

   //     //var // PUnD200 UnCoTVar
   //     //  TopArray  : Array[1..NumberOfPlanets] of TObjectTopRec;
   //     //  IntOrder  : TIntegerNumber ; // UnitSort
   //     //  RealSort  : TRealNumber ;    // UnitSort

   //     // after ClcThreeRotMatr from unit UnForCoo
   //     // there are matrix to transform to true equator for current date

   //     public static double MagnitudeValue(SolarSystemPlanets n, double ro, double rop, double phase)
   //     {
   //         double g = 99.0;
   //         double f = MyMath.RadiansToDegrees * phase; //{ in degree UnConTyp}
   //         switch (n)
   //         {
   //             case SolarSystemPlanets.Mercury:
   //                 g = -0.38 + 5 * Math.Log10(ro * rop) + 0.0380 * f - 0.000273 * f * f + 0.000002 * f * f * f;
   //                 break;
   //             case SolarSystemPlanets.Venus:
   //                 g = -4.41 + 5 * Math.Log10(ro * rop) + 0.01314 * f + 0.0000004351 * f * f * f;
   //                 break;
   //             case SolarSystemPlanets.Mars:
   //                 g = -1.51 + 5 * Math.Log10(ro * rop) + 0.01486 * f;
   //                 break;
   //             case SolarSystemPlanets.Jupiter:
   //                 g = -9.40 + 5 * Math.Log10(ro * rop);
   //                 break;
   //             case SolarSystemPlanets.Saturn:
   //                 g = -8.88 + 5 * Math.Log10(ro * rop);
   //                 break;
   //             case SolarSystemPlanets.Uranus:
   //                 g = -7.19 + 5 * Math.Log10(ro * rop);
   //                 break;
   //             case SolarSystemPlanets.Neptune:
   //                 g = -6.87 + 5 * Math.Log10(ro * rop);
   //                 break;
   //             case SolarSystemPlanets.Pluto:
   //                 g = -1.00 + 5 * Math.Log10(ro * rop);
   //                 break;
   //         } //{ case n }
   //         return g;
   //     }

   //     private static double MagSaturnRing()
   //     //Var
   //     //  i         : Byte ;
   //     { // PrecMatr and MatrNut from UnGloVar are assigned by ToClcThreeRot...
   //         double ae, de, re;
   //         double az, ds, rs;
   //         dvec3 pq, ps = new dvec3(), pt = new dvec3(), pe = new dvec3();
   //         dmat3 rotm;
   //         double dt = (JulianDate - Consts.JD2000) / 36525.00; // difference in century
   //         double ap = 40.58 - 0.036 * dt;  // north pole of the Saturn in J2000.0 equator
   //         double dp = 83.54 - 0.004 * dt;  // in degree right ascension declination
   //         double rp = 1.0; // unit distance  value for PrecMatr exists
   //         CoordConverter.DescFromSpherCoor(ap, dp, rp, out pq); // UnForCoo pole descart position
   //         dvec3 pp = PrecMatr * pq; //  Saturn north pole to equator of date
   //                                   //MatrMultVector(PrecMatr, pq, pp); 
   //         CoordConverter.ClcSpherCoorInDegree(pp, out ap, out dp, out rp); // descart position in equator of date
   //         SimpleProcedures.RotMatrFromPole(ap, dp, out rotm); // UnForFun rotation matrix to Saturn equator
   //         pq = TPUnDE200.PosDim[(int)SolarSystemPlanets.Saturn]; // topocentric descart position of Saturn
   //         SimpleProcedures.UMatrMultVect(TopMatr, pq, ref pt); // topocentric but true equator
   //         SimpleProcedures.UMatrMultVect(MatrNut, pt, ref pe); // the same but equator of date for Saturn
   //         pq = TPUnDE200.PosDim[(int)SolarSystemPlanets.Sun]; // topocentric descart position of the Sun
   //         SimpleProcedures.UMatrMultVect(TopMatr, pq, ref pt); // topocentric but true equator
   //         SimpleProcedures.UMatrMultVect(MatrNut, pt, ref ps); // the same but equator of date for the Sun               
   //         // change from station to the Saturn in equator of date
   //         ps = -pe + ps;
   //         pe = -pe;

   //         pq = rotm * pe; // to the Saturn equator rotation matrix
   //                         //MatrMultVector(rotm, pe, pq); 
   //         CoordConverter.ClcSpherCoorInDegree(pq, out ae, out de, out re); // the Earth on the Saturn equator

   //         pq = rotm * ps; // to the Saturn equator rotation matrix
   //                         // MatrMultVector(rotm, ps, pq); 
   //         CoordConverter.ClcSpherCoorInDegree(pq, out az, out ds, out rs); // the Sun on the Saturn equator
   //         double sde = Math.Sin(MyMath.DegreesToRadians * de);
   //         return +0.044 * Math.Abs(az - ae) - 2.60 * Math.Abs(sde) + 1.25 * sde * sde;
   //     }

   //     // { np number of planet
   //     //   PosP position planet refer to our point
   //     // PosS  position the Sun refer to our point }

   //     public static void ClcPlanetMagnitude(SolarSystemPlanets np, dvec3 PosP, dvec3 PosS, out double vmag, out double phas)
   //     {
   //         if ((np == SolarSystemPlanets.Earth) || (np == SolarSystemPlanets.Sun))
   //         {
   //             vmag = 99;
   //             phas = 1.0;
   //             return;
   //         }

   //         dvec3 ra = -PosP; //  { the point from the planet }
   //         dvec3 rb = +PosS - PosP; // { the Sun in topocentric }

   //         double a = Math.Sqrt(ra.x * ra.x + ra.y * ra.y + ra.z * ra.z);
   //         double b = Math.Sqrt(rb.x * rb.x + rb.y * rb.y + rb.z * rb.z);
   //         double c = (ra.x * rb.x + ra.y * rb.y + ra.z * rb.z) / (a * b);
   //         //{ cos(Phase) }
   //         phas = 0.5 * (1 + c);
   //         if (np != SolarSystemPlanets.Moon)
   //         {
   //             double s = Math.Sqrt(1 - c * c);
   //             double f = SimpleProcedures.DATAN2(s, c); 
   //             a = a / Consts.AstrUnit;
   //             b = b / Consts.AstrUnit;
   //             vmag = MagnitudeValue(np, a, b, f);
   //             if (np == SolarSystemPlanets.Saturn)
   //                 vmag = vmag + MagSaturnRing();
   //         }
   //         else
   //             vmag = 99; // for the Moon
   //     }

   //     public static void ForSimplePos(SolarSystemPlanets np,    // number of planet
   //        double tb,// moment in TDB scale
   //                                 dvec3 pf, dvec3 pb, // point pf to the Earth
   //                                  ref dvec3 pp) // pb to barycenter
   //                                                //var // in pp[..] position refer to station or other object with pf[.]
   //     {
   //         dvec3 v = new dvec3();
   //         TPUnDE200.ClcPosVel(np, tb, ref pp, ref v); // the planet to Solar system barycenter
   //         // position of the planet in fixed equator
   //         if (np != SolarSystemPlanets.Moon) // NMoon ClcPosVel from PUnDE200
   //                                            // for all planets and the Sun
   //             pp = pp - pb; // refer to the current point
   //         else // only for the Moon refer to the centre of the Earth
   //             pp = pp - pf; // refer to the current point
   //     }

   //     public static void PosReferToPoint(SolarSystemPlanets np,// number of planet
   //    double tb, // moment in TDB scale
   //                                dvec3 pf, dvec3 pb, // point pf to the Earth
   //                                 ref dvec3 pp) // pb to barycenter
   //                                               //Var // pp[1..3] position of the planet refer to the point in fixed equator

   //     {
   //         double tcur = 0.0;
   //         ForSimplePos(np, tb, pf, pb, ref pp); // here above
   //         for (int iter = 0; iter < 3; iter++ ) // to get position of the planet
   //         { // with light delay by simple iteration
   //             double rs = SimpleProcedures.VectorModul(pp); // range from point to the planet in km
   //             double dt = (rs / Consts.VelOfLight) / 86400; // light delay in part of day UnConTyp
   //             tcur = tb - dt; // moment with light delay
   //             ForSimplePos(np, tcur, pf, pb, ref pp); // here above
   //         } // in fixed equator
   //         ForSimplePos(np, tcur, pf, pb, ref pp); // here above
   //     }

   //     public static char DayOrNightNow() // current date in JulianDate
   //         { // current station position in km StationPos in true equator system
   //         char ch;
   //         double TinTDT, DeltaTA, ro;
   //         double azt, alt;
   //         dvec3 PosS = new dvec3(), Vel = new dvec3(), PosE = new dvec3(), pof = new dvec3();
     
   //         JulianDateTime.FromUTCtoTT(JulianDate, out TinTDT, out DeltaTA);  // unit UnForTim
   //            double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //         TPUnDE200.ClcEphEarth(TinTDB, ref PosE, ref Vel); // the Earth to Solar system barycentre
   //             if( !TPUnDE200.BooExistPos )
   //             return 'N'; // boolean var from unit PUnDE200 // may be no ephemeris file
   //         CoordConverter.ClcThreeRotMatr(Consts.JD2000, JulianDate, Global.PlaceCoor, ref RotMatr, ref RosMatr, ref TopMatr);
   //         SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos, ref pof); // station to fix equator
   //             // vector for station position in fixed equator in km
   //              dvec3 pob = PosE + pof; // refer to Solar system barycentre
   //             PosReferToPoint(SolarSystemPlanets.Sun, TinTDB, pof, pob, ref PosS);
   //             SimpleProcedures.MatrMultVector(RotMatr, PosS, ref PosE); // from fixed to true equator UnForFun
   //         SimpleProcedures.MatrMultVector(TopMatr, PosE, ref PosS); // to topocentric horizontal system
   //         CoordConverter.ClcTopCoorInDegree(PosS,out azt, out alt, out ro); // UnForCoo azimut altitude range
   //         double rs = MyMath.RadiansToDegrees * TPUnDE200.RadiusOfPlanet[(int)SolarSystemPlanets.Sun] / ro; // half Sun from station in degree
   //         if (alt > -rs) // the Sun height above horizon with the refraction
   //             ch = 'D'; // correction and with the Solar radius
   //         else         // now is the day the Sun above horizon
   //if (alt > -6.0)  // in degree
   //             ch = 'T';  // astronomical twilight
   //         else
   //             ch = 'N'; // night for faint stars
   //            ButtonsIni.ForSunHightButton(ch, alt); // procedure from unit UnButIni
   //             return ch;
   //         }

   //     public static char chDayOrNight(double tt)
   //     //Var // it is possible to call this function after proc ClcThreeRotMatr
   //     { // current station position in km StationPos in true equator system
   //         dvec3 PosE = new dvec3(), Vel = new dvec3(), pof = new dvec3(), PosS = new dvec3();
   //         double alt, azt, ro, TinTDT, DeltaTA;
   //         char ch;
   //         JulianDateTime.FromUTCtoTT(tt, out TinTDT, out DeltaTA);  // unit UnForTim
   //         double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //         TPUnDE200.ClcEphEarth(TinTDB, ref PosE, ref Vel); // the Earth to Solar system barycentre
   //         if (!TPUnDE200.BooExistPos) return 'N'; // boolean var from unit PUnDE200 // may be no ephemeris file
   //         SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos, ref pof); // station to fix equator
   //                                                                   // vector for station position in fixed equator in km
   //         dvec3 pob = PosE + pof; // refer to Solar system barycentre
   //         PosReferToPoint(SolarSystemPlanets.Sun, TinTDB, pof, pob, ref PosS);
   //         SimpleProcedures.MatrMultVector(RotMatr, PosS, ref PosE); // from fixed to true equator UnForFun
   //         SimpleProcedures.MatrMultVector(TopMatr, PosE, ref PosS); // to topocentric horizontal system
   //         CoordConverter.ClcTopCoorInDegree(PosS, out azt, out alt, out ro); // UnForCoo azimut altitude range
   //         double rs = MyMath.RadiansToDegrees * TPUnDE200.RadiusOfPlanet[(int)SolarSystemPlanets.Sun] / ro; // half Sun from station in degree
   //         if (alt > -rs) // the Sun height above horizon with the refraction
   //             ch = 'D'; // correction and with the Solar radius
   //         else         // now is the day the Sun above horizon  
   //         if (alt > -6.0)  // in degree
   //             ch = 'T'; // astronomical twilight
   //         else
   //             ch = 'N'; // night for faint stars
   //         return ch;
   //     }

   //     public static string stDayOrNight(double tt, double aa, double dd)
   //         //Var // tt current moment aa longitude dd latitude of the point on the Earth
   //         //  i  : Byte ;
   //         //  azt,alt,ro  : Extended ;
  
   //         //  ,Vel,pof  : TVect3 ;
   //         //  pl  : TPlaceCooRec ;
   //         { // current station position in km StationPos in true equator system
   //           // stDayOrNight = "  ночь  "; // may be no ephemeris file
   //         Global.TPlaceCooRec pl = new Global.TPlaceCooRec();
   //             pl.num = 0;
   //             pl.name = "";
   //             pl.f = dd;  // latitude in degree
   //             pl.l = aa;  // logitude in degree
   //             pl.h = 0.0; // height in meter
            
   //             CoordConverter.GeoCartFromSpher(pl.f, pl.l, pl.h, out pl.x, out pl.y, out pl.z); // proc from UnForCoo
              
   //             dvec3 Vel = new dvec3(), PosE = new dvec3(), PosS = new dvec3(), pof = new dvec3();
   //             double TinTDT, alt, azt, ro, DeltaTA;
   //         string st;
   //         CoordConverter.ClcThreeRotMatr(Consts.JD2000, tt, pl, ref RotMatr, ref RosMatr, ref TopMatr);
   //         JulianDateTime.FromUTCtoTT(tt, out TinTDT, out DeltaTA);  // unit UnForTim
   //            double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //         TPUnDE200.ClcEphEarth(TinTDB, ref PosE, ref Vel); // the Earth to Solar system barycentre
   //             if( !TPUnDE200.BooExistPos)   return "  ночь  "; // boolean var from unit PUnDE200
   //             SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos, ref pof); // station to fix equator
   //              // vector for station position in fixed equator in km
   //              dvec3 pob = PosE + pof; // refer to Solar system barycentre
   //             PosReferToPoint(SolarSystemPlanets.Sun, TinTDB, pof, pob, ref PosS);
   //         SimpleProcedures.MatrMultVector(RotMatr, PosS, ref PosE); // from fixed to true equator UnForFun
   //         SimpleProcedures.MatrMultVector(TopMatr, PosE, ref PosS); // to topocentric horizontal system
   //         CoordConverter.ClcTopCoorInDegree(PosS, out azt, out alt, out ro); // UnForCoo azimut altitude range
   //         double rs = MyMath.RadiansToDegrees * TPUnDE200.RadiusOfPlanet[(int)SolarSystemPlanets.Sun] / ro; // half Sun from station in degree
   //         if (alt > -rs) // the Sun height above horizon with the refraction
   //             st = "  день  "; // correction and with the Solar radius
   //         else         // now is the day the Sun above horizon
   //             if (alt > -6.0)   // in degree
   //             st = "сумерки ";  // astronomical twilight
   //      else st = "  ночь  "; // night for faint stars
   //             return st;
   //         }

   //     //public static void PlanetsToHeapWithFigure(int numc)
   //     //{
   //     //    int nc = 0;
   //     //    do
   //     //    {
   //     //        nc = nc + 1;
   //     //        int np = IntOrder[nc];
   //     //        ObjectTopCur = TopArray[np];
   //     //        Inc(NumObjectInHeap);
   //     //        ObjectTopHeap ^[NumObjectInHeap] = ObjectTopCur;
   //     //        With ObjectTopCur  do
   //     //            if (booscreen && BooPosInRect(xpos, ypos))
   //     //    FigurePlanet(); // unit UnGraAll
   //     //    }
   //     //    while (nc == numc);
   //     //}

   //     //            public static void PlanetForOrder(SolarSystemPlanets np, double az, double al, double ro, double vmag, double phasep, byte ishad, out int numc)      
   //     //            //  ksi,eta  : Extended ;
   //     //            {
   //     //            int x, y;
   //     //                dvec3 top = TPUnDE200.PosDim[(int)np]; // topocentric position of current planet

   //     ////                {  PosHorizoC UnGloVar topo horizon vector of the centre
   //     ////                  our planet may be out of our hemisphere
   //     ////     ScalarMunt simple cosinus between two vectors
   //     ////                  if cosinus > 0.5 then planet is in our hemishere
   //     ////                  if cosinus > 0.5 then planet may be in our field of view }
   //     //                bool bos = (ScalarMunt(top, PosHorizoC) > 0.5); // our planet may be in the other hemisphere
   //     //            if (bos)      // planet is in our hemishere
   //     //                          // this planet is in our hemisphere
   //     //            {      // this planet may be in our field of view
   //     //                IdealCoordinates(az, al, ksi, eta); // from UnPlateC
   //     //                RectCoordinates(ksi, eta, x, y);    // from UnPlaceA
   //     //            } // position projection to the plate
   //     //            else     // this planet is out our hemisphere
   //     //            {  // this palnet is out our field if view
   //     //                x = -30000; // default large minus position
   //     //                y = -30000; // default large minus position
   //     //            }   // to exlude this planet
   //     //                ObjectTopCur.nall = np; // number
   //     //                ObjectTopCur.chps = 'P'; // there is planet
   //     //                ObjectTopCur.name = TPUnDE200.PlanetNameR[(int)np]; // PUnDE200
   //     //                ObjectTopCur.azt = az; // current azimuth
   //     //                ObjectTopCur.alt = al; // current altitude
   //     //                ObjectTopCur.rot = ro; // range default for star
   //     //                ObjectTopCur.Mag = vmag; // magnitude
   //     //                ObjectTopCur.phase = phasep; // for planet
   //     //                ObjectTopCur.IndexShadow = ishad; // default
   //     //            if (al > 0.0)                  // altitude above horizon

   //     //                ObjectTopCur.boovis = true;  // visibility
   //     //            else
   //     //                ObjectTopCur.boovis = false; // no visibility
   //     //                ObjectTopCur.booscreen = bos;    // screen condition
   //     //                ObjectTopCur.xpos = x; // x on screen
   //     //                ObjectTopCur.ypos = y; // y on screen
   //     //                Inc(numc);
   //     //                TopArray[numc] = ObjectTopCur;
   //     //                IntOrder[numc] = numc; // initial order in array
   //     //                RealSort[numc] = ObjectTopCur.rot; // to order with range in km
   //     //            }

   //     //public static void PlanetsToScreen() // current date in JulianDate
   //     //                                                          // proc is called by ViewPlac                                                                                                                                                                                                  
   //     //{ // current station position in km StationPos in true equator system
   //     //dvec3 PosP = new dvec3(), PosS = new dvec3(), PosE = new dvec3(), Vel = new dvec3(), pof = new dvec3();
   //     //double TinTDT, DeltaTA;
   //     //double azt,alt,ro,phase, vmag;
   //     //byte ishad;
   //     //JulianDateTime.FromUTCtoTT(JulianDate, out TinTDT, out DeltaTA);  // unit UnForTim
   //     //   double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //     //TPUnDE200.ClcEphEarth(TinTDB,ref  PosE,ref  Vel); // the Earth to Solar system barycentre
   //     //    if (!TPUnDE200.BooExistPos) return; // boolean var from unit PUnDE200
   //     //SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos, ref pof); // station to fix equator
   //     //    // vector for station position in fixed equator in km
   //     //       dvec3 pob = PosE + pof; // refer to Solar system barycentre
   //     //    PosReferToPoint(SolarSystemPlanets.Sun, TinTDB, pof, pob, ref PosS); // the Sun refer to point
   //     //SimpleProcedures.MatrMultVector(RotMatr, PosS, ref Vel); // from fixed to true equator
   //     //SimpleProcedures.MatrMultVector(TopMatr, Vel, ref PosS); // to topocentric horizont the Sun
   //     //    int numc = 0; // inital nullo before order all planets with range
   //     //    for(SolarSystemPlanets np = SolarSystemPlanets.Mercury; np < SolarSystemPlanets.Sun; np++)
   //     //       if (np != SolarSystemPlanets.Earth)  // pof point refer to the Earth
   //     //    {  // pob point refer to the Solar system barycenter
   //     //        PosReferToPoint(np, TinTDB, pof, pob, ref PosP); // PosP refer to point
   //     //    Reductions.ForAberrationCorrection(PosP); // UnForRed correction in fix equator
   //     //    SimpleProcedures.MatrMultVector(RotMatr, PosP,ref Vel); // from fixed to true equator
   //     //    SimpleProcedures.MatrMultVector(TopMatr, Vel,ref PosP); // to topocentric horizont
   //     //    TPUnDE200.PosDim[(int)np] = PosP; // topocentric position of the planet to array
   //     //        ClcPlanetMagnitude(np, PosP, PosS, out vmag, out phase);
   //     //    CoordConverter.ClcTopCoorInDegree(PosP, out azt, out alt, out ro); // UnForCoo
   //     //        ishad = ToGetShadow(np); // UnitShad
   //     //        PlanetForOrder(np, azt, alt, ro, vmag, phase, ishad, out numc);
   //     //    }

   //     //TPUnDE200.PosDim[(int)SolarSystemPlanets.Earth] = dvec3.Zero;// array PosDim from PUnDE200
   //     //ShellMethodForOrdering(numc, RealSort, IntOrder); // UnitSort with range
   //     //    PlanetsToHeapWithFigure(numc);
   //     //}

   //     public static void PosTopPlanet(SolarSystemPlanets np, double tcur, ref double azt, ref double alt)
   //         {
   //         double TinTDT, dtmp, ro;
   //         dvec3 PosP = new dvec3(), PosE = new dvec3(), tmp = new dvec3(), pof = new dvec3();
   //         CoordConverter.ClcThreeRotMatr(Consts.JD2000, tcur, // UnConTyp UnGloVar for StationPos true
   //                             Global.PlaceCoor, ref RotMatr, ref RosMatr, ref TopMatr); // UnForCoo  equator
   //         Reductions.ToObtainAberrationParm(tcur); // UnForRed
   //             JulianDateTime.FromUTCtoTT(tcur, out TinTDT, out dtmp);  // unit UnForTim
   //             double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //         TPUnDE200.ClcEphEarth(TinTDB, ref PosE, ref tmp); // the Earth to Solar system barycentre
   //             if( !TPUnDE200.BooExistPos)  return; // boolean var from unit PUnDE200
   //         SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos, ref pof); // station to the Earth fix equator
   //             // vector for station position in fixed equator in km
   //               dvec3 pob = PosE + pof; // pob refer to Solar system barycentre
   //             PosReferToPoint(np, TinTDB, pof, pob, ref PosP); // PosP refer to station
   //         Reductions.ForAberrationCorrection(PosP); // UnForRed correction in fix equator
   //         SimpleProcedures.MatrMultVector(RotMatr, PosP, ref tmp); // from fixed to true equator UnForFun
   //         SimpleProcedures.MatrMultVector(TopMatr, tmp, ref PosP); // to topocentric horizont UnForFun
   //         CoordConverter.ClcTopCoorInDegree(PosP, out azt, out alt, out ro); // UnForCoo
   //         }

   //         public static void PosTopSop(SolarSystemPlanets np,// number of Solar system object
   //        double tcur,// current moment
   //                              Global.TPlaceCooRec posc, // station geodetic position
   //                               ref dvec3 ptop)// in topocentric
   //                                              //Var
   //                                              //  i  : Byte ;
   //                                              //  TinTDT,dtmp  : Extended ;
                                    
   //         { // StationPos[..] from ClcThreeTorMatr with posc
   //         dvec3 posp = new dvec3(), pose = new dvec3(), tmp = new dvec3(), pof = new dvec3();
   //         double TinTDT, dtmp;
   //         CoordConverter.ClcThreeRotMatr(Consts.JD2000, tcur, // UnConTyp UnGloVar for StationPos true
   //                             posc, ref RotMatr, ref RosMatr, ref TopMatr); // UnForCoo  equator
   //         Reductions.ToObtainAberrationParm(tcur); // UnForRed
   //         JulianDateTime.FromUTCtoTT(tcur, out TinTDT, out dtmp);  // unit UnForTim
   //            double TinTDB = JulianDateTime.ToGetTEph(TinTDT); // function from unit UnForTim
   //         TPUnDE200.ClcEphEarth(TinTDB,ref pose, ref tmp); // the Earth to Solar system barycentre
   //             if( !TPUnDE200.BooExistPos )  return; // boolean var from unit PUnDE200
   //         SimpleProcedures.UMatrMultVect(RotMatr, Global.StationPos,ref  pof); // station to the Earth fix equator
   //             // vector for station position in fixed equator in km
   //               dvec3 pob = pose + pof; // pob refer to Solar system barycentre
   //             PosReferToPoint(np, TinTDB, pof, pob, ref posp); // PosP refer to station
   //         Reductions.ForAberrationCorrection(posp); // UnForRed correction in fix equator
   //         SimpleProcedures.MatrMultVector(RotMatr, posp,ref  tmp); // from fixed to true equator UnForFun
   //         SimpleProcedures.MatrMultVector(TopMatr, tmp, ref ptop); // to topocentric horizont UnForFun
   //         }

   //         public static void PosGeoPlanet(SolarSystemPlanets np, double tc, ref dvec3 xp)
   //         //var // position of the planet refer to the centre of the Earth with delay     
   //         {
   //         dvec3 xg, xs = new dvec3(), xe = new dvec3(), vs = new dvec3();
   //         double tt, tb;
   //         JulianDateTime.FromUTCtoTT(tc, out tt, out tb);  // unit UnForTim tt scale TDT
   //         tb = JulianDateTime.ToGetTEph(tt); // function from unit UnForTim  tb scale TDB
   //         if (np != SolarSystemPlanets.Moon)
   //             TPUnDE200.ClcEphEarth(tb, ref xe, ref vs); // PUnDE200 the Earth refer to Solar barycentre
   //         else
   //             xe = dvec3.Zero; // for the Moon refer to the Earth
   //         if( !TPUnDE200.BooExistPos )
   //             return; // global boolean var from PUnDE200
   //         double dc = tb; // for iteration to obtain light delay
   //         TPUnDE200.ClcPosVel(np, dc, ref xs, ref vs); // for the planet or the Sun from PUnDE200
   //         for (int it = 0; it < 3; it++ ) // xs[..] the planet refer to Solar system barycentre
   //         { // the Sun refer to the centre of the Earth
   //             xg = xs - xe; // geocentric for the planet
   //             double rr = SimpleProcedures.VectorModul(xg); // function from UnForFun
   //             dc = tb - (rr / Consts.VelOfLight) / 86400; // time delay const UnConTyp
   //             TPUnDE200.ClcPosVel(np, dc, ref xs, ref vs); // the planet or the Sun position with delay
   //         }
   //         xp = xs - xe; // fix equator geocentric the planet
   //     }

   //     private static bool CentreWithFixPlanet() // called from UnitFixO
   //                                              // to obtain AzimutC AltitudeC centre of the screen in horizon

   //     {
   //         //   CentreWithFixPlanet = false; // default
   //         if ((Types.PlanetNumFix < SolarSystemPlanets.Mercury) || (Types.PlanetNumFix > SolarSystemPlanets.Sun))
   //             // it is strange number
   //             return false; // number of planet may be within NMercuty..NSun
   //         if (CharForView == 'L')// 'L' look option without visibility
   //                                // to  look around from the satellite
   //         {             // full sky above anfd lower horizon
   //                       //  CentreWithFixPlanet = true; // well on current date
   //             PosTopPlanet(Types.PlanetNumFix, // selected planet
   //                          JulianDate,   // current moment
   //                          ref Global.AzimutC, ref Global.AltitudeC); // new centre
   //             return true; // only for look option full sky lower horizon
   //         }
   //         double azt = 0.0, alt = 0.0;
   //         double step = 0.5 / 24.0; // for step in day to find moment of visibility
   //         double tmin = JulianDate;       // start  moment to find
   //         double tmax = tmin + 60.0 * step;   // finish moment to find
   //         double tcur = tmin - step;
   //         do
   //         { // step by step to find
   //             tcur = tcur + step; // visibility from a ststion
   //             PosTopPlanet(Types.PlanetNumFix, tcur, ref azt, ref alt); // selected planet
   //             if (!TPUnDE200.BooExistPos) return false; // boolean var from unit PUnDE200
   //         }
   //         while ((alt > 0.0) || (tcur > tmax));  // may be visibility
   //         if (alt > 0.0)// altitude more than 0.0 degree for visibility

   //         {     // new centre for field of view
   //               //   CentreWithFixPlanet = true; // well
   //             JulianDate = tcur; // for new date
   //             Global.AzimutC = azt;     // new centre azimuth
   //             Global.AltitudeC = alt;   // new centre altitude
   //             if (Global.AltitudeC < Global.RinDegC - 1) // but above horizon
   //                                                        // all these variables from UnGloVar
   //                 Global.AltitudeC = Global.RinDegC - 1; // above horizon
   //             return true;
   //         }
   //         return false;
   //     }

   //     public static void RotatingGeoPos(SolarSystemPlanets np,  // number of planet
   //             ref dvec3 pp, ref double fs, ref double vs)// lat longitude degree                                                                                                                                                                                                                                        
   //     {
   //         dvec3 pe = new dvec3(), vp = new dvec3();
   //         double tdt, dt, rs;
   //         CoordConverter.ClcTrueRotMatr(Consts.JD2000, JulianDate, out RotMatr, out RosMatr);  // UnForCoo  equator
   //         JulianDateTime.FromUTCtoTT(JulianDate, out tdt, out dt);  // unit UnForTim
   //         double tdb = JulianDateTime.ToGetTEph(tdt); // function from unit UnForTim
   //         switch (np)
   //         {
   //             case SolarSystemPlanets.Moon: TPUnDE200.ClcPosVel(np, tdb, ref pp, ref vp); break;// the Moon refer to the Earth now
   //             default:
   //                 TPUnDE200.ClcEphEarth(tdb, ref pe, ref vp); // the Earth to Solar system barycentre
   //                 if (!TPUnDE200.BooExistPos) return; // boolean var from unit PUnDE200
   //                 TPUnDE200.ClcPosVel(np, tdb, ref pp, ref vp); // the Sun to Solar system barycenter
   //                                                               // position of the Sun in fixed equator
   //                 pp = pp - pe; // refer to the centre of the Earth
   //                 for (int iter = 0; iter < 3; iter++) // to get position of the Sun
   //                 { // with light delay by simple iteration VectorModul UnForFun
   //                     rs = SimpleProcedures.VectorModul(pp); // range from point to the planet in km
   //                     dt = (rs / Consts.VelOfLight) / 86400; // light delay in part of day UnConTyp
   //                     double tb = tdb - dt; // moment with light delay  PUnDE200
   //                     TPUnDE200.ClcPosVel(np, tb, ref pp, ref vp); // the Sun position with light delay
   //                     pp = pp - pe; // refer to Earth centre
   //                 } // in fixed equator
   //                 break;
   //         } // case  np
   //         vp = RotMatr * pp;  // from fixed to true equator
   //         pp = RosMatr * vp;     // to rotating Earth equator UnForFun
   //         CoordConverter.ClcSpherCoorInDegree(pp, out vs, out fs, out rs); // long latitude of the Sun UnForCoo
   //     }

   // }

    
}
