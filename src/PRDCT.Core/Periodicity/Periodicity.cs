using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        public List<(string satName, int node, double tBegin, double tEnd, TrackNodeQuarter quart)> DataTimeIvals { get; } = new();

        public List<(string regName, double latDeg, double latRad, double lonLeft, double lonRight)> DataRegionCuts { get; } = new();

        public List<(string satName, double latDeg, double latRad, double lonLeft, double lonRight, int node, double tLeft, double tRight, string regName)> DataIvals { get; } = new();

        public List<(double latDeg, int prdct, double percent, double widthRad, double widthKm)> DataPeriodicities { get; } = new();

        public List<Satellite> Satellites { get; } = new();

        public List<Region> Regions { get; } = new();

        public Periodicity() { }

        public IEnumerable<double> UniqueLatitudesDEG => DataRegionCuts.Select(m => m.latDeg).Distinct();

        public void Calculate(double pitchLatDEG = 0.5)
        {
            CreateDataTimeIvals();
            CreateDataRegionCuts(pitchLatDEG);
            CreateDataIvals();
            CreateData();
        }

        private IEnumerable<dynamic> ExecSQL(double latDEG)
        {
            //            string sql =
            // @"SELECT left, '1' \
            //FROM DB_PRDCT_REGION_CUTS \
            //WHERE latDEG = :p1 \
            //UNION ALL \
            //SELECT left, '2' \
            //FROM DB_PRDCT_IVALS \
            //WHERE latDEG = :p1 \
            //UNION ALL \
            //SELECT right, '3'\
            //FROM DB_PRDCT_IVALS \
            //WHERE latDEG = :p1 \
            //UNION ALL \
            //SELECT right, '4' \
            //FROM DB_PRDCT_REGION_CUTS \
            //WHERE latDEG = :p1 \
            //ORDER BY 1, 2;";

            return (from m in DataRegionCuts
                    where m.latDeg == latDEG
                    select new { Longitude = m.lonLeft, Type = 1 }).Concat(
                      from m in DataIvals
                      where m.latDeg == latDEG
                      select new { Longitude = m.lonLeft, Type = 2 }).Concat(
                      from m in DataIvals
                      where m.latDeg == latDEG
                      select new { Longitude = m.lonRight, Type = 3 }).Concat(
                      from m in DataRegionCuts
                      where m.latDeg == latDEG
                      select new { Longitude = m.lonRight, Type = 4 }).OrderBy(c => c.Longitude).ThenBy(n => n.Type);
        }

        private void CreateData()
        {
            DataPeriodicities.Clear();

            foreach (var regionCut in DataRegionCuts)
            {
                double width = regionCut.lonRight - regionCut.lonLeft;

                double lonPrev = regionCut.lonLeft;  // самое левое значение lon, которое должно быть PRDCT_LON_TYPE_LEFT

                int prdct = 0;

                List<double> widthIvals = new List<double>();

                var queryVerts = ExecSQL(regionCut.latDeg);
                foreach (var vertex in queryVerts)
                {
                    if (prdct >= (int)widthIvals.Count)
                    {
                        widthIvals.Add(0.0);
                    }

                    if (vertex.Type == 1)
                    {
                        lonPrev = vertex.Longitude;
                    }

                    widthIvals[prdct] += vertex.Longitude - lonPrev;            // ширина отрезка этого шага

                    if (vertex.Type == 2)
                    {
                        prdct++;
                    }
                    else if (vertex.Type == 3)   // end
                    {
                        prdct--;
                    }

                    lonPrev = vertex.Longitude;
                }

                widthIvals[prdct] += (regionCut.lonRight - lonPrev);
                double radToProc = 100.0 / width;
                double r = Math.Cos(regionCut.latRad) * Globals.Re;

                double correctSumma = 0.0;
                int numWidthIvals = widthIvals.Count;
                for (int i = 0; i < numWidthIvals; ++i)
                {
                    widthIvals[i] *= radToProc;
                    //strProcent = strProcent.FormatFloat("0.##;0.##", widthIvals[i]);                   
                    widthIvals[i] = double.Parse(widthIvals[i].ToString("0.00"));
                    correctSumma += widthIvals[i];
                }
                //correctSumma += 0.01;    // для устранения ошибки округления

                if ((correctSumma + 0.02) >= 100.0)
                {
                    widthIvals[numWidthIvals - 1] -= (correctSumma - 100.0);
                    //strProcent = strProcent.FormatFloat("0.##;0.##", widthIvals[numWidthIvals - 1]);
                    widthIvals[numWidthIvals - 1] = double.Parse(widthIvals[numWidthIvals - 1].ToString("0.00"));
                }

                for (int i = 0; i < numWidthIvals; ++i)
                {
                    double lonKM = r * widthIvals[i] / radToProc;
                    //strProcent.sprintf("%,2f", procent);
                    if (widthIvals[i] != 0.0)
                    {
                        DataPeriodicities.Add((regionCut.latDeg, i, widthIvals[i], widthIvals[i] / radToProc, lonKM));
                    }
                }

            }
            return;
        }
    }
}
