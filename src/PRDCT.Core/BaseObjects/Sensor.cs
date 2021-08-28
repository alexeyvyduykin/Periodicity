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
            //      base.Parent = parent;
            base.ParentId = parent.Id;
        }

        public BaseSensor(BaseObject parent, double verticalHalfAngle, double rollAngle) : this(parent)
        {
            VerticalHalfAngle = verticalHalfAngle;
            RollAngle = rollAngle;
        }

        public BaseSensor Clone()
        {
            var newSensor = new BaseSensor()
            {
                Id = base.Id,
                Name = base.Name,
                Description = base.Description,
                Type = base.Type,
                //         Parent = base.Parent,
                ParentId = base.ParentId
            };

            newSensor.VerticalHalfAngle = VerticalHalfAngle;
            newSensor.RollAngle = RollAngle;

            return newSensor;
        }

        public double VerticalHalfAngle { get; set; }
        public double RollAngle { get; set; }

    }
}
