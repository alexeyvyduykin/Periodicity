﻿using System.Collections.Generic;
using Periodicity.Core.Data;

namespace PRDCT.Core.PRDCTPeriodicity
{
    public class PRDCTEngineRegionCuts// : PRDCTPeriodicity
    {
        public PRDCTEngineRegionCuts(Periodicity core) { }// : base(core) { }

        public static void Initialize(Periodicity core)
        {
            core.DataRegionCuts.Clear();

            List<double> vectCutIvals = new List<double>();

            for (double latDEGCutter = -90.0; latDEGCutter <= 90.0; latDEGCutter += core.PitchLatDEG)
            {
                double latRADCutter = latDEGCutter * MyMath.DegreesToRadians;

                foreach (var region in core.Regions)
                {
                    var cutter = new PRDCTRegionCutter(region.Value);

                    foreach (var item in cutter.Calculation(latRADCutter))
                    {
                        core.DataRegionCuts.Add(new RegionCuts
                        {
                            RegionID = region.Key.ToString(),
                            LatDEG = latDEGCutter,
                            LatRAD = latRADCutter,
                            LonLeft = item.Item1,
                            LonRight = item.Item2
                        });
                    }
                }
            }
        }
    }

}
