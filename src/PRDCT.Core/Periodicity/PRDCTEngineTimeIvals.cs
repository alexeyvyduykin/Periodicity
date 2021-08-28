using System.Linq;
using Periodicity.Core.Data;

namespace PRDCT.Core.PRDCTPeriodicity
{
    public class PRDCTEngineTimeIvals
    {
        //public PRDCTEngineTimeIvals(/*PRDCTPeriodicity core*/) { }// : base(core) { }

        //public void Initialize(PRDCTData dataTimeIvals)
        //{
        //    dataTimeIvals.open();
        //    dataTimeIvals.clear();

        //    int prevId = -1;
        //    List<PRDCT_NODE> nodes = new List<PRDCT_NODE>();

        //    foreach (var band in Bands)
        //    {
        //        int id = band.Value.idSatellite;

        //        band.Value.changeCalculationTime(base.DateTimeBegin.ToString(), base.DateTimeEnd.ToString());


        //        if (prevId == id)
        //        {
        //            nodes = band.Value.Nodes();   // для инициализации timeCorrection !!!!!!!
        //            continue;
        //        }

        //        nodes = band.Value.Nodes();

        //        for (int i = 0; i < nodes.Count; ++i)
        //        {
        //            for (int j = 0; j < nodes[i].num_quarts; ++j)
        //            {
        //                dataTimeIvals.add(new PRDCTDataRecordTimeIvals(id,
        //                                                                 i + 1,
        //                                                                 nodes[i].quarts[j].time_begin,
        //                                                                 nodes[i].quarts[j].time_end,
        //                                                                 nodes[i].quarts[j].quart));
        //            }
        //        }
        //        prevId = id;   
        //    }
        //    dataTimeIvals.close();
        //}

        public static void Initialize(Periodicity core)
        {
            core.DataTimeIvals.Clear();

            foreach (var satellite in core.Satellites)
            {
                var nodes = satellite.Value.Nodes();

                //var directory = people.SelectMany(p => p.PhoneNumbers, (parent, child) => new { parent.Name, child.Number });
                //var rd = nodes.SelectMany(n => n.Quarts, (parent, child) => new { parent.Value, child.Quart, child.TimeBegin, child.TimeEnd });

                core.DataTimeIvals.AddRange(
                    nodes.SelectMany(n => n.Quarts.Select(m => new TimeIvals
                    {
                        SatelliteID = satellite.Key.ToString(),
                        Node = n.Value,
                        TimeBegin = m.TimeBegin,
                        TimeEnd = m.TimeEnd,
                        Quart = MyData.FromQuart(m.Quart)
                    })).ToList());
            }
        }

    }

}
