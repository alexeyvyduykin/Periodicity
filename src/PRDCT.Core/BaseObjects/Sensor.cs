using System;

namespace Periodicity.Core
{
    public class BaseSensor : ParentBaseObject
    {
        private BaseSensor()
        {
            base.Id = Guid.NewGuid();
            base.Name = "Sensor1";
            base.Type = BaseObjectType.Sensor;
        }

        public BaseSensor(BaseObject parent) : this()
        {          
            base.ParentId = parent.Id;
        }

        public BaseSensor(BaseObject parent, double verticalHalfAngle, double rollAngle) : this(parent)
        {
            VerticalHalfAngle = verticalHalfAngle;
            RollAngle = rollAngle;
        }

        public double VerticalHalfAngle { get; set; }
        public double RollAngle { get; set; }

    }
}
