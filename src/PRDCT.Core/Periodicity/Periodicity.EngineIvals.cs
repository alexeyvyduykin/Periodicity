﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private readonly List<(double left, double right, double tBegin, double tEnd)> _vectBandIvals = new List<(double, double, double, double)>();
        private readonly List<(string name, double left, double right)> _vectRegionCutters = new List<(string, double, double)>();

        private double _curTimeBegin;
        private double _curTimeEnd;
        private int _curNode;
        private TrackNodeQuarter _curQuart;
        private string _curIdSatellite;
        private double _curLat;
        private double _curLatDEG;

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
            foreach (var item in DataTimeIvals)
            {
                _curIdSatellite = item.SatelliteID;
                _curNode = item.Node;
                _curTimeBegin = item.TimeBegin;
                _curTimeEnd = item.TimeEnd;
                _curQuart = item.Quart;

                _vectBandIvals.Clear();

                foreach (var satellite in Satellites.Where(i => i.Name == _curIdSatellite))
                {
                    foreach (var sensor in satellite.Sensors)
                    {                  
                        Band band = new Band(satellite.Orbit, sensor.VerticalHalfAngleDEG, sensor.RollAngleDEG);

                        var result = Utilities.BandCutterNew.Cut(band, _curLat, _curNode, _curTimeBegin, _curTimeEnd, satellite.TrueTimePastAN, _curQuart);
                        _vectBandIvals.AddRange(
                            result.Select(s => (s.left, s.right, s.tLeft, s.tRight)));
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
                    var (left, right, tBegin, tEnd) = _vectBandIvals[j];

                    double leftTemp = left;
                    double rightTemp = right;

                    if (MyFunction.CutSegments(leftCutter, rightCutter, ref leftTemp, ref rightTemp) == true)
                    {
                        double lon1 = left,
                               lon2 = right,
                               t1 = tBegin,
                               t2 = tEnd;

                        if (leftTemp != left)
                        {
                            t1 = ((right - leftTemp) * tBegin + (leftTemp - left) * tEnd) / (right - left);
                            lon1 = leftTemp;
                        }
                        if (rightTemp != right)
                        {
                            t2 = ((right - rightTemp) * tBegin + (rightTemp - left) * tEnd) / (right - left);
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
