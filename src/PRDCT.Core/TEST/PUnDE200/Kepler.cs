using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRDCT.Core.TEST.Main;
using GlmSharp;

namespace PRDCT.Core.TEST.PUnDE200
{
    public static class Kepler //  { for the procedures of the simple kepler problem }
    {
        public static void AnomVM(double AE, double AV, out double AM)
        {
            double V = MyMath.DegreesToRadians * AV;
            double CV = Math.Cos(V);
            double SV = Math.Sin(V);
            double R = 1.0e0 / (1.0e0 + AE * CV);
            double SE = Math.Sqrt(1.0e0 - AE * AE) * SV * R;
            double CE = (CV + AE) * R;
            double RS = SE * CV - CE * SV;
            double RC = CE * CV + SE * SV;
            double E = V + SimpleProcedures.DATAN2(RS, RC);
            double U = E - AE * SE;
            AM = MyMath.RadiansToDegrees * U;
            if (AM < 0)
                AM = 360 + AM;
        }

        // { Kepler equation   M=E-e* sin(E)
        //   to solve with the use Newton iteration method
        //   from the russian excellent book
        //   M.B.Balk.Elements of the space flight dinamics.
        //   M., Nauka, 1965.
        //   Algorithm from page  113:
        //   М.Б.Балк.Элементы динамики космического полёта.
        //   М., Наука, 1965.
        //   Алгоритм со страницы 113:
        //   If fi(x)=0  then iteration may be possible
        //  x_(n+1)=f(x_n) where f(x)=x-fi(x)/(d/dx(fi(x))) }

        public static double KeplerEquation(double ElemE, double MeanAnom)
        {
            double EccAnom = MeanAnom + ElemE * Math.Sin(MeanAnom); // { in radian }
            double difa = 1.0;
            int iter = 0;
            while ((difa > 1.0e-15) && (iter < 241))
            {
                double cura = EccAnom;
                EccAnom = cura - (cura - ElemE * Math.Sin(cura) - MeanAnom) / (1 - ElemE * Math.Cos(cura));
                difa = Math.Abs(cura - EccAnom);
                iter = iter + 1;
            }
            return EccAnom; //{ eccentric anomaly in radian }
        }

        public static void KeplXV(double AB, double AE, double AI, double AO, double AU, double AM, ref double[] X)
        {
            double FM = 3.9860047E5;
            double S = Math.Sin(MyMath.DegreesToRadians * AI);
            double C = Math.Cos(MyMath.DegreesToRadians * AI);
            double O = MyMath.DegreesToRadians * AO;
            double U = MyMath.DegreesToRadians * AU;
            double W = MyMath.DegreesToRadians * AM;           // { mean anomaly in radian }
            double E = KeplerEquation(AE, W);                  // { eccentric anomaly in radian }
            double CE = Math.Cos(E);
            double SE = Math.Sin(E);
            double RR = AB * (1.0e0 - AE * CE);
            double R = 1.0e0 / (1.0e0 - AE * CE);
            double SV = Math.Sqrt(1.0e0 - AE * AE) * SE * R;
            double CV = (CE - AE) * R;
            double RS = SV * CE - CV * SE;
            double RC = CV * CE + SV * SE;
            double V = E + SimpleProcedures.DATAN2(RS, RC);
            double B = O + V;
            double SB = Math.Sin(B);
            double CB = Math.Cos(B);
            double SU = Math.Sin(U);
            double CU = Math.Cos(U);
            X[0] = RR * (CB * CU - SB * SU * C);
            X[1] = RR * (CB * SU + SB * CU * C);
            X[2] = RR * SB * S;
            double P = AB * (1.0e0 - AE * AE);
            double Q = Math.Sqrt(FM / P);
            double VR = Q * AE * SV / RR;
            double VN = Q * (1.0e0 + AE * CV);
            X[3] = VR * X[1] + VN * (-SB * CU - CB * SU * C);
            X[4] = VR * X[2] + VN * (-SB * SU + CB * CU * C);
            X[5] = VR * X[3] + VN * CB * S;
        }

        public static void ToObtainOrbitalMatr(dvec3 r, dvec3 v, ref dmat3 OrbMatr)
        {
            double cu;
            //  { integrals of the angular momentum }
            dvec3 c = new dvec3
            {
                x = r.y * v.z - v.y * r.z, // +sin(Omega)*sin(i)*c
                y = r.z * v.x - v.z * r.x, // -cos(Omega)*sin(i)*c
                z = r.x * v.y - v.x * r.y // +cos(i)*c
            };

            double q = Math.Sqrt(r.x * r.x + r.y * r.y + r.z * r.z); //{ the modul }
            double p = Math.Sqrt(c.x * c.x + c.y * c.y + c.z * c.z); //{ the modul }
            dvec3 d = r / q;
            c = c / p;

            double ci = c.z; //{ Cos(Inclination) }
            double si = Math.Sqrt(c.x * c.x + c.y * c.y); //{ Sin(Inclination) }
            double sn = +c.x / si; //{ Sin(the Ascending Node) }
            double cn = -c.y / si; //{ Cos(the Ascending Node) }
            double su = +d.z / si; //{ Sin(u) or Sin(argument of the latitude) }
            double cucn = d.x + su * sn * ci; //{ Cos(u)* Cos(Omega) }
            double cusn = d.y - su * cn * ci; //{ Cos(u)* Sin(Omega) }
            if (Math.Abs(cn) > 0.1)
                //{ Cos(u) }
                cu = cucn / cn;
            else
                cu = cusn / sn;

            OrbMatr.Column0 = d;
            OrbMatr.Column2 = c;

            //     For i:= 1 To 3 Do
            //        Begin
            //OrbMatr[1, i]:= d[i];
            //     OrbMatr[3, i]:= c[i];
            //     End;

            OrbMatr[1, 0] = -su * cn - cusn * ci;
            OrbMatr[1, 1] = -su * sn + cucn * ci;
            OrbMatr[1, 2] = +cu * si;
        }

        // { to change from
        //   Descart position and velosity to Kepler osculating elements
        //   position Pos x, y, z in km
        //     velosity Vel vx, vy, vz in km/s
        //       GeoFM from UnConTyp in km**3/s**2
        //   is geocentric gravitationl const for the current model
        //   RadGra from UnConTyp for angle from radian to degree
        //   PosVar 1,2,3 : a in km, e,  Inclination in degree
        //   AngVar 1,2,3 : the ascending node in degree
        //                : the argument of perigei in degree
        //                : the mean anomaly in degree }

        public static void FromCartToKepler(dvec3 Pos, dvec3 Vel, ref dvec3 PosVar, ref dvec3 AngVar)
        {
            double rincl;
            double r = Math.Sqrt(Pos.x * Pos.x + Pos.y * Pos.y + Pos.z * Pos.z); //{ in km }
            double v2 = Vel.x * Vel.x + Vel.y * Vel.y + Vel.z * Vel.z; //{ in (km/s)**2 }
            double q = Consts.GeoFM / r; //{ fm in (km**3/s**2)/km from UnL01010 for the Earth }
            double h = v2 - 2 * q; //{ (km/s)^2 energy integral }
            double c1 = Pos.y * Vel.z - Pos.z * Vel.y; //{ three momentum integrals }
            double c2 = Pos.z * Vel.x - Pos.x * Vel.z;
            double c3 = Pos.x * Vel.y - Pos.y * Vel.x;
            double c = Math.Sqrt(c1 * c1 + c2 * c2 + c3 * c3);
            double l1 = -q * Pos.x + Vel.y * c3 - Vel.z * c2; //{ three integrals }
            double l2 = -q * Pos.y + Vel.z * c1 - Vel.x * c3;
            double l3 = -q * Pos.z + Vel.x * c2 - Vel.y * c1;
            double l = Math.Sqrt(l1 * l1 + l2 * l2 + l3 * l3);
            double a = -Consts.GeoFM / h; //{ in km semi-major axis }
            double e = l / Consts.GeoFM; //{ eccentricity }
            double p = c * c / Consts.GeoFM;
            double ci = c3 / c; double si = Math.Sqrt(Math.Abs(1 - ci * ci));
            if (si < 1.0e-11)
            {
                si = 1.0e-11; rincl = 0; //{ singularity inclination = 0 }
            }
            else rincl = SimpleProcedures.DATAN2(si, ci); //{ inclination in radian }
            double sn = c1 / (c * si); double cn = -c2 / (c * si);
            double node = SimpleProcedures.DATAN2(sn, cn); //{ node in radian }
            double sp = l3 / (l * si); double cp = (l1 / l) * cn + (l2 / l) * sn;
            double argp = SimpleProcedures.DATAN2(sp, cp); //{ argument of perigei in radian }
            q = 1 / r; double su = q * Pos[3] / si; double cu = q * Pos[1] * cn + q * Pos[2] * sn;
            double sv = su * cp - cu * sp; double cv = cu * cp + su * sp; //{ argument of latitude u = v + omega }
            double TrueAnom = SimpleProcedures.DATAN2(sv, cv); //{ true anomaly in radian }
            q = 1 / (1 + e * cv); double se = Math.Sqrt(1 - e * e) * sv * q; double ce = (cv + e) * q;
            double EccAnom = TrueAnom + Math.Atan((se * cv - ce * sv) / (ce * cv + se * sv));
            double MeanAnom = EccAnom - e * se; //{ M = E - e* Sin(E) in radian }
            PosVar.x = a; //{ in km }
            PosVar.y = e;
            PosVar.z = MyMath.RadiansToDegrees * rincl; //{ in degree }
            AngVar.x = MyMath.RadiansToDegrees * node; //{ in degree }
            AngVar.y = MyMath.RadiansToDegrees * argp; //{ in degree }
            AngVar.z = MyMath.RadiansToDegrees * MeanAnom; //{ in degree }
            for (int i = 0; i < 3; i++)
            {
                while (AngVar[i] < -180) AngVar[i] = AngVar[i] + 360;
                while (AngVar[i] > +180) AngVar[i] = AngVar[i] - 360;
            }
        }

        // { to change from
        //   Kepler osculating elements to Descart position and velosity
        //   GeoFM from UnConTyp in km**3/s**2
        //   is geocentric gravitationl const for the current model
        //   RadGra from UnConTyp for angle from radian to degree
        //   PosVar 1,2,3 : a in km, e,  Inclination in degree
        //   AngVar 1,2,3 : the ascending node in degree
        //                : the argument of perigei in degree
        //                : the mean anomaly un degree
        //   position Pos 1,2,3  : x,y,z in km
        //   velosity Vel 1,2,3  : vx,vy,vz in km/s }

        public static void FromKeplerToCart(dvec3 PosVar, dvec3 AngVar, out dvec3 pos, out dvec3 vel)
        {
            double ElemA = PosVar.x;
            double ElemE = PosVar.y;
            double CosInc = Math.Cos(MyMath.DegreesToRadians * PosVar.z);
            double SinInc = Math.Sin(MyMath.DegreesToRadians * PosVar.z);
            double MeanAnom = MyMath.DegreesToRadians * AngVar.z; //{ to radian }
            double EccAnom = KeplerEquation(ElemE, MeanAnom);
            double SinE = Math.Sin(EccAnom);
            double CosE = Math.Cos(EccAnom);
            double SinV = Math.Sqrt(1 - ElemE * ElemE) * SinE / (1 - ElemE * CosE);
            double CosV = (CosE - ElemE) / (1 - ElemE * CosE);
            double TrueAnom = EccAnom + Math.Atan((SinV * CosE - CosV * SinE) / (CosV * CosE + SinV * SinE));
            double ArgLatitude = TrueAnom + MyMath.DegreesToRadians * AngVar.y; //{ in radian }
            double SinU = Math.Sin(ArgLatitude);
            double CosU = Math.Cos(ArgLatitude);
            double AscendingNode = MyMath.DegreesToRadians * AngVar.x;
            double SinNode = Math.Sin(AscendingNode);
            double CosNode = Math.Cos(AscendingNode);
            double r = ElemA * (1 - ElemE * CosE); //{ in km }
            double xr = +CosU * CosNode - SinU * SinNode * CosInc;
            double yr = +CosU * SinNode + SinU * CosNode * CosInc;
            double zr = +SinU * SinInc;
            double xv = -SinU * CosNode - CosU * SinNode * CosInc;
            double yv = -SinU * SinNode + CosU * CosNode * CosInc;
            double zv = +CosU * SinInc;
            double q = Math.Sqrt(Consts.GeoFM / (ElemA * (1 - ElemE * ElemE)));
            double vr = q * ElemE * SinV;
            double vn = q * (1 + ElemE * CosV);
            pos.x = r * xr;
            pos.y = r * yr;
            pos.z = r * zr;
            vel.x = vr * xr + vn * xv;
            vel.y = vr * yr + vn * yv;
            vel.z = vr * zr + vn * zv;
        }

    }
}
