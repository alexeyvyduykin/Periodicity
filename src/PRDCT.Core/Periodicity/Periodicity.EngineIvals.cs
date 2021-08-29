using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private readonly List<PRDCTInterval> _vectBandIvals = new List<PRDCTInterval>();
        private readonly List<(string name, double left, double right)> _vectRegionCutters = new List<(string, double, double)>();

        private double _curTimeBegin;
        private double _curTimeEnd;
        private int _curNode;
        private int _curQuart;
        private string _curIdSatellite;
        private double _curLat;
        private double _curLatDEG;

        public List<Ivals> DataIvals { get; }

        public void CreateDataIvals()
        {         
            DataIvals.Clear();

            double prevLatDEG = double.MinValue;

            for (int i = 0; i < DataRegionCuts.Count; i++)
            {
                if (DataRegionCuts[i].LatDEG == prevLatDEG)
                {
                    continue;
                }

                prevLatDEG = _curLatDEG = DataRegionCuts[i].LatDEG;
                _curLat = DataRegionCuts[i].LatRAD;

                foreach (var item in DataRegionCuts.Where(c => c.LatDEG.Equals(_curLatDEG)))
                {
                    _vectRegionCutters.Add((item.RegionID, item.LonLeft, item.LonRight));
                }

                searchIvals();
                _vectRegionCutters.Clear();
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
                _curIdSatellite = item.SatelliteID;
                _curNode = item.Node;
                _curTimeBegin = item.TimeBegin;
                _curTimeEnd = item.TimeEnd;
                _curQuart = MyData.ToQuart(item.Quart);

                _vectBandIvals.Clear();

                foreach (var satellite in Satellites.Where(i => i.Name == _curIdSatellite))
                {
                    foreach (var sensor in satellite.Sensors)
                    {                  
                        Band band = new Band(satellite.Orbit, sensor.VerticalHalfAngleDEG, sensor.RollAngleDEG);

                        var result = Utilities.BandCutterNew.Cut(band, _curLat, _curNode, _curTimeBegin, _curTimeEnd, satellite.TrueTimePastAN, _curQuart);
                        _vectBandIvals.AddRange(
                            result.Select(s => new PRDCTInterval(s.left, s.right, s.tLeft, s.tRight)));
                    }
                }

                insertDataIvals();
            }
        }

        private void insertDataIvals()
        {
            for (int i = 0; i < _vectRegionCutters.Count; i++)
            {
                double leftCutter = _vectRegionCutters[i].left;
                double rightCutter = _vectRegionCutters[i].right;

                for (int j = 0; j < _vectBandIvals.Count; j++)
                {
                    PRDCTInterval ival = _vectBandIvals[j];

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
                                SatelliteID = _curIdSatellite,
                                LatDEG = _curLatDEG,
                                LatRAD = _curLat,
                                LonLeft = lon1,
                                LonRight = lon2,
                                Node = _curNode,
                                TimeLeft = t1,
                                TimeRight = t2,
                                RegionID = _vectRegionCutters[i].name
                            });
                    }
                }
            }
            return;
        }
    }
}
