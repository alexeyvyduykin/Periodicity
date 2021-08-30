using System.Collections.Generic;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private void CreateDataRegionCuts(double pitchLatDEG)
        {
            DataRegionCuts.Clear();

            for (double latDEGCutter = -90.0; latDEGCutter <= 90.0; latDEGCutter += pitchLatDEG)
            {
                double latRADCutter = latDEGCutter * MyMath.DegreesToRadians;

                foreach (var region in Regions)
                {  
                    var ivals = Utilities.RegionCutter.Cut(region, latRADCutter);

                    foreach (var (left, right) in ivals)
                    {
                        DataRegionCuts.Add((region.Name, latDEGCutter, latRADCutter, left, right));
                    }
                }
            }
        }
    }

}
