using System.Collections.Generic;

namespace Periodicity.Core
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
                    var ivals = Utilities.RegionCutter.Cut(region, latRADCutter);

                    foreach (var (left, right) in ivals)
                    {
                        core.DataRegionCuts.Add(new RegionCuts
                        {
                            RegionID = region.Name,
                            LatDEG = latDEGCutter,
                            LatRAD = latRADCutter,
                            LonLeft = left,
                            LonRight = right
                        });
                    }
                }
            }
        }
    }

}
