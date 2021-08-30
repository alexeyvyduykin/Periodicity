using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private readonly List<(double left, double right, double tBegin, double tEnd)> _vectBandIvals = new();
        private readonly List<(string regName, double left, double right)> _vectRegionCutters = new();

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
                if (DataRegionCuts[i].latDeg == prevLatDEG)
                {
                    continue;
                }

                prevLatDEG = _curLatDEG = DataRegionCuts[i].latDeg;
                _curLat = DataRegionCuts[i].latRad;

                foreach (var item in DataRegionCuts.Where(c => c.latDeg.Equals(_curLatDEG)))
                {
                    _vectRegionCutters.Add((item.regName, item.lonLeft, item.lonRight));
                }

                searchIvals();
                _vectRegionCutters.Clear();
            }

            return;
        }

        private void searchIvals()
        {
            foreach ((string name, int node, double tBegin, double tEnd, TrackNodeQuarter quart) in DataTimeIvals)
            {
                _curIdSatellite = name;
                _curNode = node;
                _curTimeBegin = tBegin;
                _curTimeEnd = tEnd;
                _curQuart = quart;

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

                        DataIvals.Add((_curIdSatellite, _curLatDEG, _curLat, lon1, lon2, _curNode, t1, t2, _vectRegionCutters[i].regName));
                    }
                }
            }
            return;
        }
    }
}
