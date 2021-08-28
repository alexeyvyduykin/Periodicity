namespace Periodicity.Core
{
    public interface ITrace
    {

    }

    public interface IDynamicObject
    {
        ITrace Get();
    }


    public class Sensor
    {
        public Sensor() { }

        //public Band CreateBand()
        //{
        //    BandMode mode;

        //    if (RollAngleDEG == 0)
        //        mode = BandMode.Middle;
        //    else if (RollAngleDEG > 0.0)
        //        mode = BandMode.Left;
        //    else
        //        mode = BandMode.Right;

        //    double gam1DEG = Math.Abs(RollAngleDEG) - VerticalHalfAngleDEG;
        //    double gam2DEG = Math.Abs(RollAngleDEG) + VerticalHalfAngleDEG;

        //    return new Band(((ObjectHeader<Satellite>)Parent).Object.Orbit, gam1DEG, gam2DEG, mode);
        //}

        //  public ObjectHeader Parent { get; set; }

        public double VerticalHalfAngleDEG { get; set; }
        public double RollAngleDEG { get; set; }


        public static Sensor From(BaseSensor sensor)
        {
            return new Sensor()
            {
                VerticalHalfAngleDEG = sensor.VerticalHalfAngle,
                RollAngleDEG = sensor.RollAngle,
                Type = SensorType.Rectancle
            };
        }

        public enum SensorType
        {
            Rectancle,
            Cone
        }

        public SensorType Type;
    }
}
