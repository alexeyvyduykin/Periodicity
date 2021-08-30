using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public class PRDCTDataPeriodicitiesRecord
    {
        public double latDEG { get; set; }
        public int periodicity { get; set; }
        public double percent { get; set; }
        public double widthRAD { get; set; }
        public double widthKM { get; set; }
    }

    public partial class Periodicity
    {
        public double PitchLatDEG { get; protected set; }

        public List<(string name, int node, double tBegin, double tEnd, TrackNodeQuarter quart)> DataTimeIvals { get; }

        public List<(string name, double latDeg, double latRad, double lonLeft, double lonRight)> DataRegionCuts { get; }

        public List<Ivals> DataIvals { get; }

        public List<PRDCTDataPeriodicitiesRecord> DataPeriodicities { get; }

        public IList<Satellite> Satellites { get; }

        public IList<Region> Regions { get; }

        public Periodicity() 
        {
            PitchLatDEG = 0.5;

            Satellites = new List<Satellite>();
            Regions = new List<Region>();

            DataTimeIvals = new List<(string, int, double, double, TrackNodeQuarter)>();
            DataRegionCuts = new List<(string, double, double, double, double)>();
            DataIvals = new List<Ivals>();

            DataPeriodicities = new List<PRDCTDataPeriodicitiesRecord>();
        }

        public Periodicity(Periodicity prdct)
        {
            Satellites = prdct.Satellites;
            Regions = prdct.Regions;

            PitchLatDEG = prdct.PitchLatDEG;

            DataIvals = prdct.DataIvals;
            DataPeriodicities = prdct.DataPeriodicities;
            DataRegionCuts = prdct.DataRegionCuts;
            DataTimeIvals = prdct.DataTimeIvals;
        }

        public IEnumerable<double> UniqueLatitudesDEG => DataRegionCuts.Select(m => m.latDeg).Distinct();

        public void Func1()
        {
            CreateDataTimeIvals();
            CreateDataRegionCuts();
            CreateDataIvals();
        }

        public void Func2()
        {
            CreateData();
        }

        //        public void CalculationTimeModeling(DateTime dateTimeBegin, DateTime dateTimeEnd)
        //        {
        //     //       TMyDate timeBeginDate = new TMyDate(dateTimeBegin);
        //     //       TMyDate timeEndDate = new TMyDate(dateTimeEnd);

        //     //       base.DateTimeBegin = dateTimeBegin;
        //     //       base.DateTimeEnd = dateTimeEnd;

        //    //        base.jd0h = timeBeginDate.JulianDate0h();
        //    //        base.s0 = timeBeginDate.S0Mean_RAD();
        //    //        base.timeBegin = timeBeginDate.SecondOf0h();
        //    //        base.timeEnd = (timeEndDate.Julian() - timeBeginDate.Julian()) * 86400.0 + timeBegin;


        ////            engineTimeIvals = new PRDCTEngineTimeIvals(this);
        ////            engineRegionCuts = new PRDCTEngineRegionCuts(this);
        // //           engineIvals = new PRDCTEngineIvals(this);
        //        }

        public void CreateIvals()
        {
            CreateDataTimeIvals();
            CreateDataRegionCuts();
            CreateDataIvals();

            //           engineTimeIvals.Initialize();
            //engineRegionCuts.Initialize();
            //engineIvals.Create();
            CreateData();
        }


        //     private PRDCTEngineTimeIvals engineTimeIvals;
        //     private PRDCTEngineRegionCuts engineRegionCuts;
        //     private PRDCTEngineIvals engineIvals;

        // public TimeIvalsMethod TimeIvalsMethod { get; set; } = TimeIvalsMethod.Default;
        // public RegionCutsMethod RegionCutsMethod { get; set; } = RegionCutsMethod.Default;
        // public CreateIvalsMethod CreateIvalsMethod { get; set; } = CreateIvalsMethod.Temp;

        //public SunCondition SunCondition { get; set; } = SunCondition.Disable;


        //public class PRDCT_DATA
        //{
        //    public class PRDCT_LAT_DATA
        //    {
        //        public double lat;
        //        public List<PRDCT_LON_INFO> lons = new List<PRDCT_LON_INFO>();
        //        public int num_lon;
        //        public double width;
        //    }

        //    public PRDCT_DATA()
        //    {
        //        data = new List<PRDCT_LAT_DATA>();
        //        num_lat = 0;
        //    }

        //    public void Clear()
        //    {
        //        data.Clear();
        //        num_lat = 0;
        //    }

        //    public void Add_Lat(double lat_)
        //    {
        //        data.Add(new PRDCT_LAT_DATA());
        //        data[num_lat++].lat = lat_;
        //    }

        //    public void Add_Lons(List<PRDCT_LON_INFO> lons_, double width_)
        //    {
        //        int size = lons_.Count,
        //            j = num_lat - 1;
        //        data[j].num_lon = size;
        //        data[j].width = width_;

        //        data[j].lons.Clear();
        //        //   data[j].lons = lons_;           //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //        //for(i = 0; i < size; i++)
        //        //  data[j].lons.push_back(lons_[i]);

        //        for (int i = 0; i < lons_.Count; i++)
        //            data[j].lons.Add(lons_[i]);
        //    }

        //    public List<PRDCT_LAT_DATA> data;
        //    private int num_lat;
        //}

        //public PRDCT_DATA CreateData()
        //{
        //    PRDCT_DATA data = new PRDCT_DATA();

        //    base.dataIvals[0]

        //    data.Add_Lat();

        //}

        //dataIvals.Add(//new PRDCTDataRecord(
        //                    new List<PRDCTDataCell>()
        //                    {
        //                        new PRDCTDataCell { Value = idSatellite, Name = "idSatellite" },
        //                        new PRDCTDataCell { Value = latDEG, Name = "latDEG" },
        //                        new PRDCTDataCell { Value = latRAD, Name = "latRAD" },
        //                        new PRDCTDataCell { Value = lon1, Name = "left" },
        //                        new PRDCTDataCell { Value = lon2, Name = "right" },
        //                        new PRDCTDataCell { Value = node, Name = "node" },
        //                        new PRDCTDataCell { Value = t1, Name = "tLeft" },
        //                        new PRDCTDataCell { Value = t2, Name = "tRight" },
        //                        new PRDCTDataCell { Value = idCutter, Name = "idRegion" }
        //                    });

        //[Table(Name = "Customers")]
        //private class QueryRecord
        //{
        //    public double Longitude { get; set; }
        //    public int Type { get; set; }
        //}

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
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonLeft, Type = 2 }).Concat(
                      from m in DataIvals
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonRight, Type = 3 }).Concat(
                      from m in DataRegionCuts
                      where m.latDeg == latDEG
                      select new { Longitude = m.lonRight, Type = 4 }).OrderBy(c => c.Longitude).ThenBy(n => n.Type);
        }

        public void CreateData()
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
                    widthIvals[i] = Double.Parse(widthIvals[i].ToString("0.00"));
                    correctSumma += widthIvals[i];
                }
                //correctSumma += 0.01;    // для устранения ошибки округления

                if ((correctSumma + 0.02) >= 100.0)
                {
                    widthIvals[numWidthIvals - 1] -= (correctSumma - 100.0);
                    //strProcent = strProcent.FormatFloat("0.##;0.##", widthIvals[numWidthIvals - 1]);
                    widthIvals[numWidthIvals - 1] = Double.Parse(widthIvals[numWidthIvals - 1].ToString("0.00"));
                }

                for (int i = 0; i < numWidthIvals; ++i)
                {
                    double lonKM = r * widthIvals[i] / radToProc;
                    //strProcent.sprintf("%,2f", procent);
                    if (widthIvals[i] != 0.0)
                    {
                        DataPeriodicities.Add(
                            new PRDCTDataPeriodicitiesRecord
                            {
                                latDEG = regionCut.latDeg,
                                periodicity = i,
                                percent = widthIvals[i],
                                widthRAD = widthIvals[i] / radToProc,
                                widthKM = lonKM
                            });
                    }
                }

            }
            return;
        }

              

    }
}
