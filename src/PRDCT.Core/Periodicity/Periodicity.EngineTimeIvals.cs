using System.Linq;

namespace Periodicity.Core
{
    public partial class Periodicity
    {
        private void CreateDataTimeIvals()
        {
            DataTimeIvals.Clear();

            foreach (var satellite in Satellites)
            {
                var nodes = satellite.Nodes();

                DataTimeIvals.AddRange(
                    nodes.SelectMany(n => n.Quarts.Select(m => (satellite.Name, n.Value, m.TimeBegin, m.TimeEnd, m.Quart))).ToList());
            }
        }

    }
}
