using System;
using System.Collections.Generic;
using System.Linq;
using GlmSharp;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private readonly double _h_sun_min = 0.0 * MyMath.DegreesToRadians;
        private double _tempJD0;
        private double _tempBeginSecs;
        private double _tempS0;

        private bool PRDCT_HSUN(double t, double lon, double lat)
        {
            double S = _tempS0 + Globals.Omega * (t + _tempBeginSecs);
            double La = S + lon;
            double xp = Globals.Re * Math.Cos(lat) * Math.Cos(La);
            double yp = Globals.Re * Math.Cos(lat) * Math.Sin(La);
            double zp = Globals.Re * Math.Sin(lat);
            double JD = _tempJD0 + (t + _tempBeginSecs) / 86400.0;
            dvec4 rs = Sun.Position(JD);
            double xr = rs[0] - xp;
            double yr = rs[1] - yp;
            double zr = rs[2] - zp;
            double dal = Math.Sqrt(xr * xr + yr * yr + zr * zr);
            double rsun = Math.Sqrt(rs[0] * rs[0] + rs[1] * rs[1] + rs[2] * rs[2]);
            double alpha = Math.Acos((dal * dal + Globals.Re * Globals.Re - rsun * rsun) / (2 * Globals.Re * dal));
            double ahsun = alpha - Math.PI / 2.0;
            if (ahsun < _h_sun_min)
            {
                return false;
            }

            return true;
        }

        private void CreateSunIvals()
        {
            string curID = "";

            foreach ((string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) in DataIvals)
            {
                if (curID != satName)
                {
                    var sat = Satellites.Where(s => s.Name == satName).FirstOrDefault();
                    _tempJD0 = sat.StartTime.Date.ToOADate() + 2415018.5;
                    _tempBeginSecs = sat.StartTime.TimeOfDay.TotalSeconds;
                    //tempS0 = MyFunction.uds1900(tempJD0);
                    Julian jd = new Julian(sat.StartTime.Date);
                    _tempS0 = jd.ToGmst();
                }

                bool isLeft = PRDCT_HSUN(tLeft, lonLeft, latRad);
                bool isRight = PRDCT_HSUN(tRight, lonRight, latRad);

                // интервал полностью освещён
                if (isLeft == true && isRight == true)
                {
                    DataIvals.Add((satName, latDeg, latRad, lonLeft, lonRight, node, tLeft, tRight, regName));
                }
                // концы интервала не освещены, необходима проверка внутренней части
                else if (isLeft == false && isRight == false)
                {
                    //// ищем любую точку которая освещенна, если такой нет, то выходим
                    double step = 1.0 * MyMath.DegreesToRadians;
                    bool isCur = false;
                    double lonCur, tCur = 0.0;
                    for (lonCur = lonLeft; lonCur <= lonRight; lonCur += step)
                    {
                        tCur = ((lonRight - lonCur) * tLeft + (lonCur - lonLeft) * tRight) / (lonRight - lonLeft);
                        isCur = PRDCT_HSUN(tCur, lonCur, latRad);
                        if (isCur == true)
                        {
                            break;
                        }
                    }

                    if (isCur == false)
                    {
                        continue;
                    }

                    DataIvals.Add(FuncSun2((satName, latDeg, latRad, lonLeft, lonRight, node, tLeft, tRight, regName), lonCur, tCur));
                }
                // интервал освещён частично
                //if( isLighting1 == false || isLighting2 == false )
                else
                {
                    DataIvals.Add(FuncSun1((satName, latDeg, latRad, lonLeft, lonRight, node, tLeft, tRight, regName), isLeft, isRight));
                }

            }

        }

        private (string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) FuncSun1((string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) ival, bool is1, bool is2)
        {
            double lon1 = ival.lonLeft;
            double lon2 = ival.lonRight;

            double t1 = ival.tLeft;
            double t2 = ival.tRight;

            double lon_cur = lon1;
            double lon_prev, t_cur;

            do
            {
                lon_prev = lon_cur;
                lon_cur = 0.5 * (lon1 + lon2);
                t_cur = 0.5 * (t1 + t2);

                bool is_cur = PRDCT_HSUN(t_cur, lon_cur, ival.latRad);

                if (is_cur == is1)
                {
                    lon1 = lon_cur;
                    t1 = t_cur;
                }
                else
                {
                    lon2 = lon_cur;
                    t2 = t_cur;
                }
            }
            while (Math.Abs(lon_cur - lon_prev) > 0.01);


            var result = ival;

            if (is1 == true)
            {
                lon2 = lon_cur;
                t2 = t_cur;

                result.lonRight = lon2;
                result.tRight = t2;
            }
            else
            {
                lon1 = lon_cur;
                t1 = t_cur;

                result.lonLeft = lon1;
                result.tLeft = t1;
            }


            return result;
        }

        private (string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) FuncSun2((string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) ival, double lonCur, double tCur)
        {
            (string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) ival1 = ival;
            ival1.lonRight = lonCur;
            ival1.tRight = tCur;
            (string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName) ival2 = ival;
            ival2.lonLeft = lonCur;
            ival2.tLeft = tCur;

            var left = FuncSun1(ival1, false, true);
            var right = FuncSun1(ival2, true, false);

            var result = ival;
            result.lonLeft = left.lonLeft;
            result.lonRight = right.lonRight;
            result.tLeft = left.tLeft;
            result.tRight = right.tRight;
            return result;
        }   
    }
}
