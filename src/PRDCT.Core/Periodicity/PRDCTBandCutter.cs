using System;
using System.Collections.Generic;

namespace Periodicity.Core
{
    public class PRDCTBandCutter
    {
        public PRDCTBandCutter(Band band)
        {
            this.band = band;
        }

        public List<Tuple<double, double, double, double>> Calculation(double latCutter, int node, double tBegin, double tEnd, double tPastAN, int quart)
        {
            var result = new List<Tuple<double, double, double, double>>();

            currectLatCutter = latCutter;
            currentNode = node;
            currentTBegin = tBegin;
            currentTEnd = tEnd;
            currentTPastAN = tPastAN;
            currentQuart = quart;

            //     band.NearLine.initTimeIval(node, tBegin, tEnd, tPastAN, quart);
            //     band.FarLine.initTimeIval(node, tBegin, tEnd, tPastAN, quart);

            double left = 0.0, right = 0.0, tLeft = 0.0, tRight = 0.0, tTemp;

            if (SearchIval(ref left, ref right, ref tLeft, ref tRight) == true)
            {
                if (left > right)   // 0 --- lm2 ............... lm1 --- TWOPI
                {
                    tTemp = (right * tLeft + (2.0 * Math.PI - left) * tRight) / ((2.0 * Math.PI - left) + right);

                    result.Add(Tuple.Create(0.0, right, tTemp, tRight));
                    result.Add(Tuple.Create(left, 2.0 * Math.PI, tLeft, tTemp));
                }
                else
                {
                    result.Add(Tuple.Create(left, right, tLeft, tRight));
                }
            }

            return result;
        }

        public bool SearchIval(ref double leftTemp, ref double rightTemp, ref double tLeftTemp, ref double tRightTemp)
        {
          
            double temp;

            var (left, tLeft, isLeft) = PeriodicityUtility.TrackCutter(band.NearLine, currectLatCutter, currentNode, currentTBegin, currentTEnd, currentTPastAN, currentQuart);
            var (right, tRight, isRight) = PeriodicityUtility.TrackCutter(band.FarLine, currectLatCutter, currentNode, currentTBegin, currentTEnd, currentTPastAN, currentQuart);

            // пересечение одной трассы
            if (isLeft == false)
            {
                if (SearchBorderIval(tLeft, ref left) == false)
                {
                    return false;
                }
            }

            if (isRight == false)
            {
                if (SearchBorderIval(tRight, ref right) == false)
                {
                    return false;
                }
            }

            if (left > right)
            {
                temp = right;
                right = left;
                left = temp;

                temp = tRight;
                tRight = tLeft;
                tLeft = temp;
            }

            left = MyMath.WrapAngle(left);
            right = MyMath.WrapAngle(right);

            leftTemp = left;
            tLeftTemp = tLeft;// - band.Orbit.TimeCorrection_;

            rightTemp = right;
            tRightTemp = tRight;// - band.Orbit.TimeCorrection_;

            return true;
        }

        private bool SearchBorderIval(double tBorder, ref double lonCut)
        {
            double tNorm, latCut = 0.0, angleDistance;
            Geo2D pLeft, pRight;

            pLeft = band.NearLine.ContinuousTrack(currentNode, tBorder, currentTPastAN, currentQuart);
            pRight = band.FarLine.ContinuousTrack(currentNode, tBorder, currentTPastAN, currentQuart);

            if (!MyMath.InRange(currectLatCutter, pLeft.Lat, pRight.Lat))
            {
                return false;
            }

            tNorm = tBorder;// - band->time_ascend_null_node;

            //  tNorm = tBorder + (period - timeHalfPi); // !!!!!!!!!!!!!!!!!!!!!!! 28.10.2014

            // 02.02.2015 при рассчёте интервалов для КС СМОТР-Р с более широкой полосой 18 - 50
            // возникает ошибка из-за строки tNorm = tBorder + (period - timeHalfPi);
            // роблему удалось решить добавлением  tNorm = tBorder + (period + timeAN);
            tNorm = tBorder + (band.Orbit.Period + 0.0/* band.TimePastAN*/);       // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            while (tNorm > band.Orbit.Period)
            {
                tNorm -= band.Orbit.Period;
            }
            // является ли место среза в точках

            if (MyMath.DoubleEquals(tNorm, band.Orbit.Quart1/* timeHalfPi*/, 0.00001) ||     //                          *
                MyMath.DoubleEquals(tNorm, band.Orbit.Quart3/* Period - timeHalfPi*/, 0.00001))      //                       .     .
            {                                                            //                    .           .
                lonCut = pLeft.Lon; // pLeft.lon == pRight.lon;          //    .            .
            }                                                            //       .      .
                                                                         //          *
            else if (MyMath.DoubleEquals(tNorm, band.Orbit.Quart0/* 0.0*/, 0.00001) ||          // является ли место среза в точках
                     MyMath.DoubleEquals(tNorm, band.Orbit.Quart2/* Period / 2.0*/, 0.00001) ||          //                          .
                     MyMath.DoubleEquals(tNorm, band.Orbit.Quart4/* Period*/, 0.00001))           //                       .     .
            {                                                            //                    .           .
                                                                         //    *            *                *
                CutLines(pLeft.Lon, pLeft.Lat, pRight.Lon, pRight.Lat,    //       .      .
                          pLeft.Lon, currectLatCutter, pRight.Lon, currectLatCutter,    //          .
                          ref lonCut, ref latCut);

                if (MyMath.DoubleEquals(lonCut, 0.0, 1.0E-8) == true)
                {
                    if (lonCut < 0.0)
                    {
                        lonCut = 2.0 * Math.PI;
                    }
                    else if (lonCut > 0.0)
                    {
                        lonCut = 0.0;
                    }
                }
            }
            else
            {
                angleDistance = Distance(tBorder);
                SearchPOTest(pLeft, pRight, angleDistance, ref lonCut);
            }
            return true;
            //double coef = (cut - left) / (right - left);
            //double tCut = tLeft + (tRight - tLeft)*coef;
        }

        private double Distance(double t)
        {
            //double distance = distantLine->CentralAngle(t) * distantLine->pls - nearLine->CentralAngle(t) * nearLine->pls;
            //return fabs( distance );
            double distance = band.FarLine.centralAngle(t) - band.NearLine.centralAngle(t);
            return Math.Abs(distance);
        }

        private void SearchPOTest(Geo2D p1, Geo2D p2, double angleDistance, ref double lonCutter)
        {
            double c, A = 0.0, lat = 0.0, lon = 0.0, lonTemp = 0.0, eps, ai, di;
            c = angleDistance;

            eps = 0.001;
            for (ai = 0.0; ai <= 2.0 * Math.PI; ai += 0.0001)
            {
                FuncSphere(c, ai, p1.Lat, p1.Lon, ref lat, ref lon);
                if (ai < (2.0 * Math.PI) && ai > Math.PI)
                {
                    lon -= (2.0 * Math.PI);
                }

                if (MyMath.DoubleEquals(p2.Lat, lat, eps) && MyMath.DoubleEquals(p2.Lon, lon, eps) == true)
                {
                    A = ai;
                    eps /= 10.0;
                }
            }
            eps = 0.001;
            for (di = 0.0; di <= c; di = di + 0.0001)
            {
                FuncSphere(di, A, p1.Lat, p1.Lon, ref lat, ref lon);
                if (MyMath.DoubleEquals(lat, currectLatCutter, eps) == true)
                {
                    if (A < (2.0 * Math.PI) && A > Math.PI)      // !!!!!!!!!!!!!!!!!!!!!!!1111
                    {
                        lon -= (2.0 * Math.PI);                // !!!!!!!!!!!!!!!!!!!!!!!!111
                    }

                    lonTemp = lon;
                    eps /= 10.0;
                }
            }
            lonCutter = lonTemp;
            return;
        }

        private void FuncSphere(double di, double ai, double fs, double ls, ref double fb, ref double lb)
        {
            // di - длина дуги
            // ai - азимут

            double cfs, sfs, sdi, cdi, sfi, cfi, sdl, cdl;
            cfs = Math.Cos(fs);
            if (Math.Abs(cfs) <= 0.1e-6)
            {
                fb = fs - di * MyMath.Sign(fs);
                lb = ai;
            }
            else
            {
                sfs = Math.Sin(fs);
                sdi = Math.Sin(di);
                cdi = Math.Cos(di);
                sfi = Math.Cos(ai) * cfs * sdi + sfs * cdi;
                if (Math.Abs(sfi) > 1.0)
                {
                    sfi = MyMath.Sign(sfi);
                }

                cfi = Math.Sqrt(1.0 - sfi * sfi);
                if (Math.Abs(cfi) <= 0.1E-6)
                {
                    lb = ls;
                }
                else
                {
                    sdl = sdi * Math.Sin(ai) / cfi;
                    if (Math.Abs(sdl) > 1.0)
                    {
                        sdl = MyMath.Sign(sdl);
                    }

                    cdl = (cdi - sfs * sfi) / cfs / cfi;
                    if (Math.Abs(cdl) > 1.0)
                    {
                        cdl = MyMath.Sign(cdl);
                    }

                    lb = ls + MyMath.ArcCos2(sdl, cdl);
                }
                fb = Math.Asin(sfi);
            }
        }

        private bool CutLines(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4,
      ref double x, ref double y)
        {
            double numer, denom, tIn = 0.0, tOut = 1.0;
            double dxLine1 = x2 - x1,
                   dyLine1 = y2 - y1,
                   dxLine2 = x4 - x3,
                   dyLine2 = y4 - y3;

            numer = -dyLine1 * (x1 - x3) + dxLine1 * (y1 - y3);
            denom = -dyLine1 * dxLine2 + dxLine1 * dyLine2;

            if (!ChopCI(ref tIn, ref tOut, numer, denom))
            {
                return false; // досрочный выход
            }

            if (tOut < 1.0)
            {
                x = x1 + dxLine2 * tOut;
                y = y1 + dyLine2 * tOut;
            }
            if (tIn > 0.0)
            {
                x = x1 + dxLine2 * tIn;
                y = y1 + dyLine2 * tIn;
            }
            return true;
        }

        private bool ChopCI(ref double tIn, ref double tOut, double numer, double denom)
        {
            double tHit;
            if (denom < 0)                           // луч входит
            {
                tHit = numer / denom;
                if (tHit > tOut)
                {
                    return false;    // досрочный выход
                }
                else
                { if (tHit > tIn) { tIn = tHit; } }  // берём больше t
            }
            else if (denom > 0)                   // луч выходит
            {
                tHit = numer / denom;
                if (tHit < tIn)
                {
                    return false;        // досрочный выход
                }
                else
                { if (tHit < tOut) { tOut = tHit; } } // берём меньшее t
            }
            else
            {                 // denom(знаменатель) равен нулю: луч параллелен
                if (numer <= 0)
                {
                    return false;
                }
            }                     // прошёл мимо прямой
            return true;             // возможный интервал по-прежнему пуст
        }

        private double currectLatCutter;
        private int currentNode;
        private double currentTBegin;
        private double currentTEnd;
        private int currentQuart;
        private double currentTPastAN;

        private readonly Band band;
    }



    public class PRDCTBandCutterNew
    {
        public PRDCTBandCutterNew(Band band)
        {
            //this.sensor = sensor;

            //  var satellite = sensor.Parent.Object as Satellite;
            //  var sensorSAR = sensor as SensorSAR;

            this.band = band;// new Band(satellite.Orbit, sensorSAR.VerticalHalfAngleDEG, sensorSAR.RollAngleDEG);
        }

        public List<Tuple<double, double, double, double>> Calculation(double latCutter, int node, double tBegin, double tEnd, double tPastAN, int quart)
        {
            var result = new List<Tuple<double, double, double, double>>();

            currectLatCutter = latCutter;
            currentNode = node;
            currentTBegin = tBegin;
            currentTEnd = tEnd;
            currentTPastAN = tPastAN;
            currentQuart = quart;

            //     band.NearLine.initTimeIval(node, tBegin, tEnd, tPastAN, quart);
            //     band.FarLine.initTimeIval(node, tBegin, tEnd, tPastAN, quart);

            double left = 0.0, right = 0.0, tLeft = 0.0, tRight = 0.0, tTemp;

            if (SearchIval(ref left, ref right, ref tLeft, ref tRight) == true)
            {
                if (left > right)   // 0 --- lm2 ............... lm1 --- TWOPI
                {
                    tTemp = (right * tLeft + (2.0 * Math.PI - left) * tRight) / ((2.0 * Math.PI - left) + right);

                    result.Add(Tuple.Create(0.0, right, tTemp, tRight));
                    result.Add(Tuple.Create(left, 2.0 * Math.PI, tLeft, tTemp));
                }
                else
                {
                    result.Add(Tuple.Create(left, right, tLeft, tRight));
                }
            }

            return result;
        }

        public bool SearchIval(ref double leftTemp, ref double rightTemp, ref double tLeftTemp, ref double tRightTemp)
        {
         
            double temp;

            var (left, tLeft, isLeft) = PeriodicityUtility.TrackCutter(band.NearLine, currectLatCutter, currentNode, currentTBegin, currentTEnd, currentTPastAN, currentQuart);
            var (right, tRight, isRight) = PeriodicityUtility.TrackCutter(band.FarLine, currectLatCutter, currentNode, currentTBegin, currentTEnd, currentTPastAN, currentQuart);

            // пересечение одной трассы
            if (isLeft == false)
            {
                if (SearchBorderIval(tLeft, ref left) == false)
                {
                    return false;
                }
            }

            if (isRight == false)
            {
                if (SearchBorderIval(tRight, ref right) == false)
                {
                    return false;
                }
            }

            if (left > right)
            {
                temp = right;
                right = left;
                left = temp;

                temp = tRight;
                tRight = tLeft;
                tLeft = temp;
            }

            left = MyMath.WrapAngle(left);
            right = MyMath.WrapAngle(right);

            leftTemp = left;
            tLeftTemp = tLeft;// - band.Orbit.TimeCorrection_;

            rightTemp = right;
            tRightTemp = tRight;// - band.Orbit.TimeCorrection_;

            return true;
        }

        private bool SearchBorderIval(double tBorder, ref double lonCut)
        {
            double tNorm, latCut = 0.0, angleDistance;
            Geo2D pLeft, pRight;

            pLeft = band.NearLine.ContinuousTrack(currentNode, tBorder, currentTPastAN, currentQuart);
            pRight = band.FarLine.ContinuousTrack(currentNode, tBorder, currentTPastAN, currentQuart);

            if (!MyMath.InRange(currectLatCutter, pLeft.Lat, pRight.Lat))
            {
                return false;
            }

            tNorm = tBorder;// - band->time_ascend_null_node;

            //  tNorm = tBorder + (period - timeHalfPi); // !!!!!!!!!!!!!!!!!!!!!!! 28.10.2014

            // 02.02.2015 при рассчёте интервалов для КС СМОТР-Р с более широкой полосой 18 - 50
            // возникает ошибка из-за строки tNorm = tBorder + (period - timeHalfPi);
            // роблему удалось решить добавлением  tNorm = tBorder + (period + timeAN);
            tNorm = tBorder + (band.Orbit.Period + 0.0/* band.TimePastAN*/);       // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            while (tNorm > band.Orbit.Period)
            {
                tNorm -= band.Orbit.Period;
            }
            // является ли место среза в точках

            if (MyMath.DoubleEquals(tNorm, band.Orbit.Quart1/* timeHalfPi*/, 0.00001) ||     //                          *
                MyMath.DoubleEquals(tNorm, band.Orbit.Quart3/* Period - timeHalfPi*/, 0.00001))      //                       .     .
            {                                                            //                    .           .
                lonCut = pLeft.Lon; // pLeft.lon == pRight.lon;          //    .            .
            }                                                            //       .      .
                                                                         //          *
            else if (MyMath.DoubleEquals(tNorm, band.Orbit.Quart0/* 0.0*/, 0.00001) ||          // является ли место среза в точках
                     MyMath.DoubleEquals(tNorm, band.Orbit.Quart2/* Period / 2.0*/, 0.00001) ||          //                          .
                     MyMath.DoubleEquals(tNorm, band.Orbit.Quart4/* Period*/, 0.00001))           //                       .     .
            {                                                            //                    .           .
                                                                         //    *            *                *
                CutLines(pLeft.Lon, pLeft.Lat, pRight.Lon, pRight.Lat,    //       .      .
                          pLeft.Lon, currectLatCutter, pRight.Lon, currectLatCutter,    //          .
                          ref lonCut, ref latCut);

                if (MyMath.DoubleEquals(lonCut, 0.0, 1.0E-8) == true)
                {
                    if (lonCut < 0.0)
                    {
                        lonCut = 2.0 * Math.PI;
                    }
                    else if (lonCut > 0.0)
                    {
                        lonCut = 0.0;
                    }
                }
            }
            else
            {
                angleDistance = Distance(tBorder);
                SearchPOTest(pLeft, pRight, angleDistance, ref lonCut);
            }
            return true;
            //double coef = (cut - left) / (right - left);
            //double tCut = tLeft + (tRight - tLeft)*coef;
        }

        private double Distance(double t)
        {
            //double distance = distantLine->CentralAngle(t) * distantLine->pls - nearLine->CentralAngle(t) * nearLine->pls;
            //return fabs( distance );
            double distance = band.FarLine.centralAngle(t) - band.NearLine.centralAngle(t);
            return Math.Abs(distance);
        }

        private void SearchPOTest(Geo2D p1, Geo2D p2, double angleDistance, ref double lonCutter)
        {
            double c, A = 0.0, lat = 0.0, lon = 0.0, lonTemp = 0.0, eps, ai, di;
            c = angleDistance;

            eps = 0.001;
            for (ai = 0.0; ai <= 2.0 * Math.PI; ai += 0.0001)
            {
                FuncSphere(c, ai, p1.Lat, p1.Lon, ref lat, ref lon);
                if (ai < (2.0 * Math.PI) && ai > Math.PI)
                {
                    lon -= (2.0 * Math.PI);
                }

                if (MyMath.DoubleEquals(p2.Lat, lat, eps) && MyMath.DoubleEquals(p2.Lon, lon, eps) == true)
                {
                    A = ai;
                    eps /= 10.0;
                }
            }
            eps = 0.001;
            for (di = 0.0; di <= c; di = di + 0.0001)
            {
                FuncSphere(di, A, p1.Lat, p1.Lon, ref lat, ref lon);
                if (MyMath.DoubleEquals(lat, currectLatCutter, eps) == true)
                {
                    if (A < (2.0 * Math.PI) && A > Math.PI)      // !!!!!!!!!!!!!!!!!!!!!!!1111
                    {
                        lon -= (2.0 * Math.PI);                // !!!!!!!!!!!!!!!!!!!!!!!!111
                    }

                    lonTemp = lon;
                    eps /= 10.0;
                }
            }
            lonCutter = lonTemp;
            return;
        }

        private void FuncSphere(double di, double ai, double fs, double ls, ref double fb, ref double lb)
        {
            // di - длина дуги
            // ai - азимут

            double cfs, sfs, sdi, cdi, sfi, cfi, sdl, cdl;
            cfs = Math.Cos(fs);
            if (Math.Abs(cfs) <= 0.1e-6)
            {
                fb = fs - di * MyMath.Sign(fs);
                lb = ai;
            }
            else
            {
                sfs = Math.Sin(fs);
                sdi = Math.Sin(di);
                cdi = Math.Cos(di);
                sfi = Math.Cos(ai) * cfs * sdi + sfs * cdi;
                if (Math.Abs(sfi) > 1.0)
                {
                    sfi = MyMath.Sign(sfi);
                }

                cfi = Math.Sqrt(1.0 - sfi * sfi);
                if (Math.Abs(cfi) <= 0.1E-6)
                {
                    lb = ls;
                }
                else
                {
                    sdl = sdi * Math.Sin(ai) / cfi;
                    if (Math.Abs(sdl) > 1.0)
                    {
                        sdl = MyMath.Sign(sdl);
                    }

                    cdl = (cdi - sfs * sfi) / cfs / cfi;
                    if (Math.Abs(cdl) > 1.0)
                    {
                        cdl = MyMath.Sign(cdl);
                    }

                    lb = ls + MyMath.ArcCos2(sdl, cdl);
                }
                fb = Math.Asin(sfi);
            }
        }

        private bool CutLines(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4,
      ref double x, ref double y)
        {
            double numer, denom, tIn = 0.0, tOut = 1.0;
            double dxLine1 = x2 - x1,
                   dyLine1 = y2 - y1,
                   dxLine2 = x4 - x3,
                   dyLine2 = y4 - y3;

            numer = -dyLine1 * (x1 - x3) + dxLine1 * (y1 - y3);
            denom = -dyLine1 * dxLine2 + dxLine1 * dyLine2;

            if (!ChopCI(ref tIn, ref tOut, numer, denom))
            {
                return false; // досрочный выход
            }

            if (tOut < 1.0)
            {
                x = x1 + dxLine2 * tOut;
                y = y1 + dyLine2 * tOut;
            }
            if (tIn > 0.0)
            {
                x = x1 + dxLine2 * tIn;
                y = y1 + dyLine2 * tIn;
            }
            return true;
        }

        private bool ChopCI(ref double tIn, ref double tOut, double numer, double denom)
        {
            double tHit;
            if (denom < 0)                           // луч входит
            {
                tHit = numer / denom;
                if (tHit > tOut)
                {
                    return false;    // досрочный выход
                }
                else
                { if (tHit > tIn) { tIn = tHit; } }  // берём больше t
            }
            else if (denom > 0)                   // луч выходит
            {
                tHit = numer / denom;
                if (tHit < tIn)
                {
                    return false;        // досрочный выход
                }
                else
                { if (tHit < tOut) { tOut = tHit; } } // берём меньшее t
            }
            else
            {                 // denom(знаменатель) равен нулю: луч параллелен
                if (numer <= 0)
                {
                    return false;
                }
            }                     // прошёл мимо прямой
            return true;             // возможный интервал по-прежнему пуст
        }

        private double currectLatCutter;
        private int currentNode;
        private double currentTBegin;
        private double currentTEnd;
        private int currentQuart;
        private double currentTPastAN;

        //private readonly Sensor sensor;
        private readonly Band band;
    }

}
