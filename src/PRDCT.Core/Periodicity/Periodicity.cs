using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PRDCT.Data;
using Microsoft.SqlServer.Types;
using GlmSharp;
using System.Data.SqlClient;

namespace PRDCT.Core.PRDCTPeriodicity
{
    //public enum TimeIvalsMethod
    //{
    //    Default
    //}

    //public enum RegionCutsMethod
    //{
    //    Default
    //}

    //public enum CreateIvalsMethod
    //{
    //    Temp,
    //    Structure,
    //    Speed
    //}

    //public enum SunCondition
    //{
    //    Disable,
    //    SatelliteLighting,
    //    GroundLighting
    //}

    public class PRDCTDataPeriodicitiesRecord
    {
        public double latDEG { get; set; }
        public int periodicity { get; set; }
        public double percent { get; set; }
        public double widthRAD { get; set; }
        public double widthKM { get; set; }
    }

    public enum LonType
    {
        Begin = 0,
        End = 1,
        Left = 2,
        Right = 3
    }

    public class Periodicity //: Scenario
    {
        //public new static PRDCTPeriodicity Default = new PRDCTPeriodicity(Scenario.Default);

        public Periodicity(/*Scenario scenario*/) //: base(/*scenario*/)
        {
            PitchLatDEG = 0.5;

            Satellites = new Dictionary<Guid, PRDCTSatellite>();
            Regions = new Dictionary<Guid, PRDCTRegion>();
            Sensors = new Dictionary<Guid, List<PRDCTSensor>>();

            DataTimeIvals = new List<TimeIvals>();
            DataRegionCuts = new List<RegionCuts>();
            DataIvals = new List<Ivals>();

            DataPeriodicities = new List<PRDCTDataPeriodicitiesRecord>();
        }

        public Periodicity(Periodicity prdct) //: base(/*prdct*/)
        {
            this.Satellites = prdct.Satellites;
            this.Regions = prdct.Regions;
            this.Sensors = prdct.Sensors;

            this.PitchLatDEG = prdct.PitchLatDEG;

            this.DataIvals = prdct.DataIvals;
            this.DataPeriodicities = prdct.DataPeriodicities;
            this.DataRegionCuts = prdct.DataRegionCuts;
            this.DataTimeIvals = prdct.DataTimeIvals;
        }

        public void Func1()
        {
         //   var engineTimeIvals = new PRDCTEngineTimeIvals(this);
        //    engineTimeIvals.Initialize();

            PRDCTEngineTimeIvals.Initialize(this);
            PRDCTEngineRegionCuts.Initialize(this);
            //var engineRegionCuts = new PRDCTEngineRegionCuts(this);
            //engineRegionCuts.Initialize();
            var engineIvals = new PRDCTEngineIvals(this);
            engineIvals.Create();
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
            //var engineTimeIvals = new PRDCTEngineTimeIvals(this);
            //engineTimeIvals.Initialize();

            PRDCTEngineTimeIvals.Initialize(this);

            PRDCTEngineRegionCuts.Initialize(this);

            //var engineRegionCuts = new PRDCTEngineRegionCuts(this);
            //engineRegionCuts.Initialize();
            var engineIvals = new PRDCTEngineIvals(this);
            engineIvals.Create();

            //           engineTimeIvals.Initialize();
            //engineRegionCuts.Initialize();
            //engineIvals.Create();
            CreateData();
        }

        #region Save/Load Scenario
        
        private void SaveSatellites(string PrdctID, SqlCommand cmd)
        {
            //using (SqlConnection connection = new SqlConnection(MyData.ConnectionString))
            //{
            //    connection.Open();
            cmd.CommandText = "INSERT INTO TimeIvals(PeriodicityID, SatelliteID, Node, TimeBegin, TimeEnd, Quart) VALUES(@param1,@param2,@param3,@param4,@param5,@param6)";
            //SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@param1", "");
            cmd.Parameters.AddWithValue("@param2", "");
            cmd.Parameters.AddWithValue("@param3", "");
            cmd.Parameters.AddWithValue("@param4", "");
            cmd.Parameters.AddWithValue("@param5", "");
            cmd.Parameters.AddWithValue("@param6", "");

            foreach (var item in DataTimeIvals)
            {
                cmd.Parameters["@param1"].Value = PrdctID;
                cmd.Parameters["@param2"].Value = item.SatelliteID;
                cmd.Parameters["@param3"].Value = item.Node;
                cmd.Parameters["@param4"].Value = item.TimeBegin;
                cmd.Parameters["@param5"].Value = item.TimeEnd;
                cmd.Parameters["@param6"].Value = /*PRDCTDataTimeIvalsRecord.FromQuart(*/item.Quart/*)*/;// item.QuartConversion();
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "INSERT INTO Ivals(PeriodicityID, SatelliteID, LatDEG, LatRAD, LonLeft, LonRight, Node, TimeLeft, TimeRight, RegionID) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@param1", "");
            cmd.Parameters.AddWithValue("@param2", "");
            cmd.Parameters.AddWithValue("@param3", "");
            cmd.Parameters.AddWithValue("@param4", "");
            cmd.Parameters.AddWithValue("@param5", "");
            cmd.Parameters.AddWithValue("@param6", "");
            cmd.Parameters.AddWithValue("@param7", "");
            cmd.Parameters.AddWithValue("@param8", "");
            cmd.Parameters.AddWithValue("@param9", "");
            cmd.Parameters.AddWithValue("@param10", "");

            foreach (var item in DataIvals)
            {
                cmd.Parameters["@param1"].Value = PrdctID;
                cmd.Parameters["@param2"].Value = item.SatelliteID;
                cmd.Parameters["@param3"].Value = item.LatDEG;
                cmd.Parameters["@param4"].Value = item.LatRAD;
                cmd.Parameters["@param5"].Value = item.LonLeft;
                cmd.Parameters["@param6"].Value = item.LonRight;
                cmd.Parameters["@param7"].Value = item.Node;
                cmd.Parameters["@param8"].Value = item.TimeLeft;
                cmd.Parameters["@param9"].Value = item.TimeRight;
                cmd.Parameters["@param10"].Value = item.RegionID;
                cmd.ExecuteNonQuery();
            }
            // }
        }

        private void SaveRegions(string PrdctID, SqlCommand cmd)
        {
            //using (SqlConnection connection = new SqlConnection(MyData.ConnectionString))
            //{
            //    connection.Open();
            cmd.CommandText = "INSERT INTO RegionCuts(PeriodicityID, RegionID, LatDEG, LatRAD, LonLeft, LonRight) VALUES(@param1,@param2,@param3,@param4,@param5,@param6)";
            //    SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@param1", "");
            cmd.Parameters.AddWithValue("@param2", "");
            cmd.Parameters.AddWithValue("@param3", "");
            cmd.Parameters.AddWithValue("@param4", "");
            cmd.Parameters.AddWithValue("@param5", "");
            cmd.Parameters.AddWithValue("@param6", "");

            foreach (var item in DataRegionCuts)
            {
                cmd.Parameters["@param1"].Value = PrdctID;
                cmd.Parameters["@param2"].Value = item.RegionID;
                cmd.Parameters["@param3"].Value = item.LatDEG;
                cmd.Parameters["@param4"].Value = item.LatRAD;
                cmd.Parameters["@param5"].Value = item.LonLeft;
                cmd.Parameters["@param6"].Value = item.LonRight;
                cmd.ExecuteNonQuery();
            }

            // }
        }

        private void SaveScenario(string PrdctID)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
            db.Periodicities.InsertOnSubmit(new Periodicities()
            {
                PeriodicityID = PrdctID,
                PeriodicityName = "Prdct_" + (db.Periodicities.Count() + 1).ToString(),
                Epoch = DateTime.Now.ToString(),// TimeBegin.ToString(),
                TimeStart = 0.0,// timeBegin,
                TimeDuration = 0.0,// timeEnd - timeBegin,
                LatitudeStep = PitchLatDEG
            });
            db.SubmitChanges();
        }

        public void Save()
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
            db.ExecuteCommand("DELETE FROM Ivals");
            db.ExecuteCommand("DELETE FROM RegionCuts");
            db.ExecuteCommand("DELETE FROM TimeIvals");
            db.ExecuteCommand("DELETE FROM Periodicities");

            using (SqlConnection connection = new SqlConnection(MyData.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand() { Connection = connection };

                string PrdctID = Guid.NewGuid().ToString();
                SaveScenario(PrdctID);
                SaveRegions(PrdctID, cmd);
                SaveSatellites(PrdctID, cmd);
            }
        }
        
        //public static Periodicity Load(string periodicityID)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    var prdct = db.Periodicities.Where(p => p.PeriodicityID == periodicityID).Single();

        //    var newPeriodicity = new Periodicity(/*Scenario.Default*/);

        //    DateTime timeBegin = DateTime.Parse(prdct.Epoch).AddSeconds(prdct.TimeStart);
        //    DateTime timeEnd = DateTime.Parse(prdct.Epoch).AddSeconds(prdct.TimeStart + prdct.TimeDuration);

        //  //  newPeriodicity.CalculationTimeModeling(timeBegin, timeEnd);

        //    foreach (var item in db.SatelliteCollection(periodicityID))
        //    {
        //        //newPeriodicity.Bands.AddRange(Band.Converting(item, timeBegin, timeEnd));
        //        newPeriodicity.Satellites.Add(Guid.Parse(item.SatelliteID), Converter.ToSatellite(item, timeBegin, timeEnd));
        //    }
        //    //newPeriodicity.Bands.AddRange(db.SatelliteCollection(periodicityID).SelectMany(s => PRDCTBand.Converting(s, timeBegin, timeEnd)));

        //    foreach (var item in db.RegionCollection(periodicityID))
        //    {
        //        newPeriodicity.Regions.Add(Guid.Parse(item.RegionID), Converter.ToRegion(item));
        //    }
        //    //newPeriodicity.Regions.AddRange(db.RegionCollection(periodicityID).Select(p => PRDCTRegion.Converting(p)));

        //    foreach ( var item in db.TimeIvals.Where(t => t.PeriodicityID == periodicityID))
        //    {
        //        newPeriodicity.DataTimeIvals.Add(new TimeIvals {
        //            SatelliteID = item.SatelliteID,
        //            Node = item.Node,
        //            TimeBegin = item.TimeBegin,
        //            TimeEnd = item.TimeEnd,
        //            Quart = /*MyData.ToQuart(*/item.Quart//)
        //        });
        //    }

        //    foreach (var item in db.Ivals.Where(v => v.PeriodicityID == periodicityID).OrderBy(c => c.LatDEG))
        //    {
        //        newPeriodicity.DataIvals.Add(new Ivals {
        //            SatelliteID = item.SatelliteID,
        //            LatDEG = item.LatDEG,
        //            LatRAD = item.LatRAD,
        //            LonLeft = item.LonLeft,
        //            LonRight = item.LonRight,
        //            Node = item.Node,
        //            TimeLeft = item.TimeLeft,
        //            TimeRight = item.TimeRight,
        //            RegionID = item.RegionID
        //        });
        //    }

        //    foreach (var item in db.RegionCuts.Where(r => r.PeriodicityID == periodicityID).OrderBy(c => c.LatDEG))
        //    {
        //        newPeriodicity.DataRegionCuts.Add(new RegionCuts {
        //            RegionID = item.RegionID,
        //            LatDEG = item.LatDEG,
        //            LatRAD = item.LatRAD,
        //            LonLeft = item.LonLeft,
        //            LonRight = item.LonRight
        //        });
        //    }

        //    newPeriodicity.CreateData();

        //    return newPeriodicity;
        //}
        
        #endregion
        
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
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonLeft, Type = 1 }).Concat(         
                      from m in DataIvals
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonLeft, Type = 2 }).Concat(
                      from m in DataIvals
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonRight, Type = 3 }).Concat(
                      from m in DataRegionCuts
                      where m.LatDEG == latDEG
                      select new { Longitude = m.LonRight, Type = 4 }).OrderBy(c => c.Longitude).ThenBy(n => n.Type);
        } 

        public void CreateData()
        {
            DataPeriodicities.Clear();

            foreach (var regionCut in DataRegionCuts)
            {
                double width = regionCut.LonRight - regionCut.LonLeft;

                double lonPrev = regionCut.LonLeft;  // самое левое значение lon, которое должно быть PRDCT_LON_TYPE_LEFT
                        
                int prdct = 0;

                List<double> widthIvals = new List<double>();

                var queryVerts = ExecSQL(regionCut.LatDEG);
                foreach (var vertex in queryVerts)
                {
                    if (prdct >= (int)widthIvals.Count)
                        widthIvals.Add(0.0);

                    if (vertex.Type == 1)
                        lonPrev = vertex.Longitude;

                    widthIvals[prdct] += vertex.Longitude - lonPrev;            // ширина отрезка этого шага

                    if (vertex.Type == 2)
                        prdct++;
                    else if (vertex.Type == 3)   // end
                        prdct--;

                    lonPrev = vertex.Longitude;
                }

                widthIvals[prdct] += (regionCut.LonRight - lonPrev);
                double radToProc = 100.0 / width;
                double r = Math.Cos(regionCut.LatRAD) * Globals.Re;

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
                                latDEG = regionCut.LatDEG,
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

        public void Clear()
        {
            Satellites.Clear();
            Regions.Clear();
            Sensors.Clear();
        }

        public IEnumerable<double> UniqueLatitudesDEG
        {
            get
            {
                return DataRegionCuts.Select(m => m.LatDEG).Distinct();
            }
        }

        public double PitchLatDEG { get; protected set; }

        public virtual List<Ivals> DataIvals { get; }
        public List<TimeIvals> DataTimeIvals { get; }
        public List<RegionCuts> DataRegionCuts { get; }

        public List<PRDCTDataPeriodicitiesRecord> DataPeriodicities { get; }

        public Dictionary<Guid, PRDCTSatellite> Satellites { get; }
        public Dictionary<Guid, PRDCTRegion> Regions { get; }

        public Dictionary<Guid, List<PRDCTSensor>> Sensors { get; }
    }
}