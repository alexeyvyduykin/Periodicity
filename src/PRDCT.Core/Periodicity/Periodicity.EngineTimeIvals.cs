using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        public void CreateDataTimeIvals()
        {
            DataTimeIvals.Clear();

            foreach (var satellite in Satellites)
            {
                var nodes = satellite.Nodes();

                DataTimeIvals.AddRange(
                    nodes.SelectMany(n => n.Quarts.Select(m => new TimeIvals
                    {
                        SatelliteID = satellite.Name,
                        Node = n.Value,
                        TimeBegin = m.TimeBegin,
                        TimeEnd = m.TimeEnd,
                        Quart = m.Quart
                    })).ToList());
            }
        }

    }
}
