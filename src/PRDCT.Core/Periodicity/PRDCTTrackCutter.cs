﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDCT.Core.PRDCTPeriodicity
{
    public class PRDCTTrackCutter
    {
        private const double PRDCT_CLASS_EPS_METHOD_DICHOTOMY = 0.0003;

        public PRDCTTrackCutter(Track track)
        {
            this.track = track;
        }

        public Tuple<double?, double> Calculation(double latCutter, int node, double tBegin, double tEnd, double tPastAN, int quart)
        {
            double tTemp, latTemp;
            double tBeginTemp = tBegin,
                   tEndTemp = tEnd;

            double? lonCut;

            double latBegin = track.ContinuousTrack(node, tBegin, tPastAN, quart).Lat;
            double latEnd = track.ContinuousTrack(node, tEnd, tPastAN, quart).Lat;

            if (MyMath.InRange(latCutter, latBegin, latEnd))
            {
                //------------------Новый метод (необходимо доработать)-------------------------
                if (latCutter == 0.0)  // для полярной и близко полярной орбиты
                {
                    if (MyMath.DoubleEquals(latBegin, 0.0, 1.0E-10) == true)
                    {
                        lonCut = track.ContinuousTrack(node, tBegin, tPastAN, quart).Lon;
                        return Tuple.Create(lonCut, tBegin);
                    }
                    if (MyMath.DoubleEquals(latEnd, 0.0, 1.0E-10) == true)
                    {
                        return Tuple.Create<double?, double>(null, tEnd);
                    }
                }
                //------------------------------------------------------------------------------
                do
                {
                    tTemp = (tBeginTemp + tEndTemp) / 2.0;
                    latTemp = track.ContinuousTrack(node, tTemp, tPastAN, quart).Lat;

                    if (MyMath.InRange(latCutter, latTemp, latBegin))
                        tEndTemp = tTemp;
                    else
                    {
                        tBeginTemp = tTemp;
                        latBegin = latTemp;
                    }
                }
                while (Math.Abs(latCutter - latTemp) > PRDCT_CLASS_EPS_METHOD_DICHOTOMY);
                lonCut = track.ContinuousTrack(node, tTemp, tPastAN, quart).Lon;
                return Tuple.Create(lonCut, tTemp);
            }
            else
            {
                if (Math.Abs(latCutter - latBegin) > Math.Abs(latCutter - latEnd))
                    tTemp = tEnd;
                else
                    tTemp = tBegin;
            }

            return Tuple.Create<double?, double>(null, tTemp);
        }

        private Track track;

        //private double currectLatCutter;
        //private int currentNode;
        //private double currentTBegin;
        //private double currentTEnd;
        //private int currentQuart;
        //private double currentTPastAN;
    }
}
