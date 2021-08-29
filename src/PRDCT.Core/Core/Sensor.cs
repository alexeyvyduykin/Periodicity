namespace Periodicity.Core
{
    public enum SensorType
    {
        Rectancle,
        Cone
    }

    public class Sensor
    {
        public Sensor() 
        {
            Type = SensorType.Rectancle;
        }

        public Sensor(double verticalHalfAngleDEG, double rollAngleDEG) : this()
        {
            VerticalHalfAngleDEG = verticalHalfAngleDEG;
            RollAngleDEG = rollAngleDEG;
        }

        public double VerticalHalfAngleDEG { get; set; }

        public double RollAngleDEG { get; set; }

        public SensorType Type { get; }
    }
}
