using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{

    public class PRDCTEngineIvals : Periodicity
    {
        public PRDCTEngineIvals(Periodicity core) : base(core) { }

        public void Create()
        {
            //base.initConditions();
            DataIvals.Clear();

            double prevLatDEG = double.MinValue;

            for (int i = 0; i < DataRegionCuts.Count; i++)
            {
                if (DataRegionCuts[i].LatDEG == prevLatDEG)
                {
                    continue;
                }

                prevLatDEG = curLatDEG = DataRegionCuts[i].LatDEG;
                curLat = DataRegionCuts[i].LatRAD;

                foreach (var item in DataRegionCuts.Where(c => c.LatDEG.Equals(curLatDEG)))
                {
                    vectRegionCutters.Add(new RegionCutter
                    {
                        idRegion = item.RegionID,
                        left = item.LonLeft,
                        right = item.LonRight
                    });
                }

                searchIvals();
                vectRegionCutters.Clear();
            }

            return;
        }

        private void searchIvals()
        {
            //if( funcPolis( vectBands[i], dataIvals ) == true)
            //  continue;

            //iteratorBand curBand;
            //std::pair<iteratorBand, iteratorBand> curPair;
            /*for(curBand = mapBands.begin(); curBand != mapBands.end(); curBand++)
            {
              (*curBand).second->isCoverPolis(curLat,);
            }
            */

            foreach (var item in DataTimeIvals)
            {
                curIdSatellite = item.SatelliteID;
                curNode = item.Node;
                curTimeBegin = item.TimeBegin;
                curTimeEnd = item.TimeEnd;
                curQuart = MyData.ToQuart(item.Quart);

                vectBandIvals.Clear();

                foreach (var satellite in Satellites.Where(i => i.Name == curIdSatellite))
                {
                    foreach (var sensor in satellite.Sensors)
                    {                  
                        Band band = new Band(satellite.Orbit, sensor.VerticalHalfAngleDEG, sensor.RollAngleDEG);

                        var result = Utilities.BandCutterNew.Cut(band, curLat, curNode, curTimeBegin, curTimeEnd, satellite.TrueTimePastAN, curQuart);
                        vectBandIvals.AddRange(
                            result.Select(s => new PRDCTInterval(s.left, s.right, s.tLeft, s.tRight)));
                    }
                }

                insertDataIvals();
            }
        }

        private void insertDataIvals()
        {
            for (int i = 0; i < vectRegionCutters.Count; i++)
            {
                double leftCutter = vectRegionCutters[i].left;
                double rightCutter = vectRegionCutters[i].right;

                for (int j = 0; j < vectBandIvals.Count; j++)
                {
                    //double left = vectBandIvals[j].left;
                    //double right = vectBandIvals[j].right;
                    //double tLeft = vectBandIvals[j].tLeft;
                    //double tRight = vectBandIvals[j].tRight;

                    PRDCTInterval ival = vectBandIvals[j];

                    double leftTemp = ival.Left;
                    double rightTemp = ival.Right;

                    if (MyFunction.CutSegments(leftCutter, rightCutter, ref leftTemp, ref rightTemp) == true)
                    {
                        double lon1 = ival.Left,
                               lon2 = ival.Right,
                               t1 = ival.TimeBegin,
                               t2 = ival.TimeEnd;

                        if (leftTemp != ival.Left)
                        {
                            t1 = ((ival.Right - leftTemp) * ival.TimeBegin + (leftTemp - ival.Left) * ival.TimeEnd) / (ival.Right - ival.Left);
                            lon1 = leftTemp;
                        }
                        if (rightTemp != ival.Right)
                        {
                            t2 = ((ival.Right - rightTemp) * ival.TimeBegin + (rightTemp - ival.Left) * ival.TimeEnd) / (ival.Right - ival.Left);
                            lon2 = rightTemp;
                        }

                        DataIvals.Add(
                            new Ivals
                            {
                                SatelliteID = curIdSatellite,
                                LatDEG = curLatDEG,
                                LatRAD = curLat,
                                LonLeft = lon1,
                                LonRight = lon2,
                                Node = curNode,
                                TimeLeft = t1,
                                TimeRight = t2,
                                RegionID = vectRegionCutters[i].idRegion
                            });
                    }
                }
            }
            return;
        }

        private class RegionCutter
        {
            public string idRegion;
            public double left, right;
        }

        //private class BandIval
        //{
        //    public double left, right, tLeft, tRight;
        //}

        private readonly List<PRDCTInterval> vectBandIvals = new List<PRDCTInterval>();
        private readonly List<RegionCutter> vectRegionCutters = new List<RegionCutter>();

        private double curTimeBegin;
        private double curTimeEnd;
        private int curNode;
        private int curQuart;
        private string curIdSatellite;

        private double curLat;
        private double curLatDEG;
    }

    //public class PRDCTEngineIvals_TempMethod : PRDCTEngineIvals
    //{

    //    public PRDCTEngineIvals_TempMethod(PRDCTCore core) : base(core)
    //    {
    //        std::multimap<int, PRDCTBand*>::iterator itBands;
    //        for (itBands = mapBands.begin(); itBands != mapBands.end(); itBands++)
    //            vectBands.push_back((*itBands).second);

    //        std::multimap<int, PRDCTRegion*>::iterator itRegions;
    //        for (itRegions = mapRegions.begin(); itRegions != mapRegions.end(); itRegions++)
    //            vectRegions.push_back((*itRegions).second);

    //        numBands = mapBands.size();
    //        numRegions = mapRegions.size();
    //    }

    //    public override void Create()
    //    {
    //        PRDCTEngineIvals::initConditions();

    //        int i, index_lat, curId, curType, curI, numRec;
    //        AnsiString sql;
    //        double curLatDEG, curLon, curTime, left, right, lonLeft, lonRight, timeLeft, timeRight;

    //        TQuery* queryCuts = new TQuery(NULL);
    //        queryCuts->DatabaseName = "db_PRDCT";

    //        TQuery* queryTimeIvals = new TQuery(NULL);
    //        queryTimeIvals->DatabaseName = "db_PRDCT";

    //        dataIvals->open();
    //        dataIvals->clear();

    //        PRDCTData dataIvalsTemp = new PRDCTDataDnmcVctr(new PRDCTDataRecordIvals);
    //        PRDCTData dataCutsRegionsTemp = new PRDCTDataDnmcVctr(new PRDCTDataRecordRegionCuts);

    //        for (curRegion = 0; curRegion < numRegions; curRegion++)
    //        {
    //            prdctTypeRegion = vectRegions[curRegion]->type;

    //            sql = "SELECT* FROM DB_PRDCT_REGION_CUTS WHERE idRegion=" + IntToStr(vectRegions[curRegion]->id);
    //            addSQL(queryCuts, sql);

    //            dataCutsRegionsTemp->clear();
    //            queryCuts->Last();
    //            numRec = queryCuts->RecordCount;
    //            queryCuts->First();
    //            for (i = 0; i < numRec; ++i)
    //            {
    //                dataCutsRegionsTemp.Add(new PRDCTDataRecord(
    //                    new List<PRDCTDataCell>()
    //                    {
    //                        new PRDCTDataCell { Value = queryCuts->FieldByName("idRegion")->AsInteger, Name = "idRegion" },
    //                        new PRDCTDataCell { Value =  queryCuts->FieldByName("latRAD")->AsFloat, Name = "latRAD"},
    //                        new PRDCTDataCell { Value = queryCuts->FieldByName("latDEG")->AsFloat, Name = "latDEG" },
    //                        new PRDCTDataCell { Value = queryCuts->FieldByName("left")->AsFloat, Name = "left" },
    //                        new PRDCTDataCell { Value = queryCuts->FieldByName("right")->AsFloat, Name = "right" }
    //                    }
    //                    )); 

    //                queryCuts->Next();
    //    }
    //    dataCutsRegionsTemp->first();
    //            queryCuts->First();
    //            while (!queryCuts->Eof)
    //            {
    //                curLatRAD = queryCuts->FieldByName("latRAD")->AsFloat;
    //                curLatDEG = queryCuts->FieldByName("latDEG")->AsFloat;
    //                left = queryCuts->FieldByName("left")->AsFloat;
    //                right = queryCuts->FieldByName("right")->AsFloat;
    //                queryCuts->Next();

    //                prdctRegionCuts.clear();
    //                prdctRegionCuts.push_back(PRDCTRegionCut(left, right));

    //                for (i = 0; i < numBands; ++i)
    //                {
    //                    if (funcPolis(vectBands[i], dataIvals) == true)
    //                        continue;
    //                    curIdSatellite = vectBands[i]->idSatellite;
    //                    sql = "SELECT* FROM DB_PRDCT_TIME_IVALS WHERE idSatellite=" + IntToStr(vectBands[i]->idSatellite);
    //                    addSQL(queryTimeIvals, sql);

    //                    queryTimeIvals->First();
    //                    while (!queryTimeIvals->Eof)
    //                    {
    //                        curNode = queryTimeIvals->FieldByName("node")->AsInteger;
    //                        curTimeBegin = queryTimeIvals->FieldByName("timeBegin")->AsFloat;
    //                        curTimeEnd = queryTimeIvals->FieldByName("timeEnd")->AsFloat;
    //                        curQuart = queryTimeIvals->FieldByName("quart")->AsInteger;

    //                        vectBands[i]->initTimeIval(curNode, curTimeBegin, curTimeEnd, curQuart);

    //                        dataIvalsTemp->clear();
    //                        if (vectBands[i]->searchIval(curLatDEG, vectRegions[curRegion]->id, dataIvalsTemp) == true)
    //                            isRajonTest(dataCutsRegionsTemp, dataIvalsTemp);

    //                        queryTimeIvals->Next();
    //                    }
    //                }
    //                dataCutsRegionsTemp->next();
    //            } // num_lat
    //        }  // num_regions

    //        delete queryCuts;
    //        delete queryTimeIvals;

    //        delete dataIvalsTemp;
    //        delete dataCutsRegionsTemp;

    //        dataIvals->close();
    //        return;
    //    }

    //    private struct PRDCTRegionCut
    //    {
    //        PRDCTRegionCut(double left, double right) { this->left = left; this->right = right; }
    //        double left, right;
    //    }

    //    private int funcPolis(PRDCTBand band, PRDCTData dataIvals)
    //    {
    //        double time_an_to_polis, timeBeginPolis;
    //        int i, i_ = 0, numNodes = band->numNodes;

    //        if (band->isCoverPolis(curLatRAD, &time_an_to_polis) != true)
    //            return false;

    //        // !!!!!!!!!11 тут ничего не работает

    //        double period = band->nearLine->period;
    //        for (i = 0; i < numNodes; i++)
    //        {
    //            timeBeginPolis = time_an_to_polis + period * i + fabs(band->timeAN);  ////// !!!!!!!!!!!!!!!!1

    //            if (band->timeAN < 0)   ////// !!!!!!!!!!!!!!!!!!111
    //            {
    //                if (i != numNodes - 1)
    //                    i_ = 1;
    //                else
    //                    continue;
    //            }
    //            dataIvals->add(new PRDCTDataRecordIvals(
    //              band->idSatellite, curLatRAD * RAD_TO_DEG, curLatRAD, 0.0, FloatToStr(TWOPI).ToDouble(),
    //              i + 1 + i_, timeBeginPolis, timeBeginPolis, vectRegions[curRegion]->id));
    //        }
    //        return true;
    //    }
    //    private void isRajonTest(PRDCTData dataCutsRegionsTemp, PRDCTData dataIvalsTemp)
    //    {
    //        int idCutter = (int)dataCutsRegionsTemp[0]; // id
    //        double leftCutter = (double)dataCutsRegionsTemp[3]; // lon1
    //        double rightCutter = (double)dataCutsRegionsTemp[4]; // lon2

    //        dataIvalsTemp.First();
    //        while (!dataIvalsTemp.IsEnd())
    //        {
    //            int idSatellite = (int)dataIvalsTemp[0];
    //            double latDEG = (double)dataIvalsTemp[1];
    //            double latRAD = (double)dataIvalsTemp[2];
    //            double left = (double)dataIvalsTemp[3]; // lon1
    //            double right = (double)dataIvalsTemp[4]; // lon2
    //            int node = (int)dataIvalsTemp[5];
    //            double tLeft = (double)dataIvalsTemp[6]; // t1
    //            double tRight = (double)dataIvalsTemp[7]; // t2
    //            dataIvalsTemp.Next();

    //            double leftTemp = left;
    //            double rightTemp = right;

    //            if (prdctCutSegments(leftCutter, rightCutter, leftTemp, rightTemp) == 1)
    //            {
    //                double lon1 = left,
    //                       lon2 = right,
    //                       t1 = tLeft,
    //                       t2 = tRight;

    //                if (leftTemp != left)
    //                {
    //                    t1 = ((right - leftTemp) * tLeft + (leftTemp - left) * tRight) / (right - left);
    //                    lon1 = leftTemp;
    //                }
    //                if (rightTemp != right)
    //                {
    //                    t2 = ((right - rightTemp) * tLeft + (rightTemp - left) * tRight) / (right - left);
    //                    lon2 = rightTemp;
    //                }

    //                if (PRDCTEngineIvals::checkIval(latRAD, lon1, t1, lon2, t2) == true)
    //                    //dataIvals.Add(new PRDCTDataRecordIvals(
    //                    //  idSatellite, latDEG, latRAD, FloatToStr(lon1).ToDouble(), FloatToStr(lon2).ToDouble(),
    //                    //  node, t1, t2, idCutter));

    //                    dataIvals.Add(
    //                        new PRDCTDataRecord(new List<PRDCTDataCell>()
    //                        {
    //                            new PRDCTDataCell { Value = idSatellite, Name = "idSatellite" },
    //                            new PRDCTDataCell { Value = latDEG, Name = "latDEG" },
    //                            new PRDCTDataCell { Value = latRAD, Name = "latRAD" },
    //                            new PRDCTDataCell { Value = lon1, Name = "left" },
    //                            new PRDCTDataCell { Value = lon2, Name = "right" },
    //                            new PRDCTDataCell { Value = node, Name = "node" },
    //                            new PRDCTDataCell { Value = t1, Name = "tLeft" },
    //                            new PRDCTDataCell { Value = t2, Name = "tRight" },
    //                            new PRDCTDataCell { Value = idCutter, Name = "idRegion" }
    //                        }                                                            
    //                        ));

    //            }
    //        }
    //        return;
    //    }

    //    private List<PRDCTRegion> vectRegions;
    //    private List<PRDCTBand> vectBands;
    //    private int numBands, numRegions;

    //    private List<PRDCTRegionCut> prdctRegionCuts;

    //    private double curLatRAD;
    //    private double curLatDEG;

    //    private double curTimeBegin;
    //    private double curTimeEnd;
    //    private int curNode;
    //    private int curQuart;

    //    private int prdctTypeRegion;
    //    private int curRegion;

    //}

}
