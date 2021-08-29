namespace Periodicity.Core
{
    public enum SensorType
    {
        Rectancle,
        Cone
    }

    public class Sensor : BaseEntity
    {
        private Sensor() { }

        public Sensor(string name) 
        {
            Name = name;
            Type = SensorType.Rectancle;
        }

        public Sensor(string name, double verticalHalfAngleDEG, double rollAngleDEG) : this(name)
        {
            VerticalHalfAngleDEG = verticalHalfAngleDEG;
            RollAngleDEG = rollAngleDEG;
        }

        public double VerticalHalfAngleDEG { get; set; }

        public double RollAngleDEG { get; set; }

        public SensorType Type { get; }
    }
}
