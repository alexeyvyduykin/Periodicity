using System;
using System.Xml;

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

        #region Serializable

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetType().ToString())
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];
                ParentId = Guid.Parse(reader["ParentId"]);

                VerticalHalfAngle = Double.Parse(reader["VerticalHalfAngle"]);
                RollAngle = Double.Parse(reader["RollAngle"]);
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("ParentId", ParentId.ToString());

            writer.WriteAttributeString("VerticalHalfAngle", VerticalHalfAngle.ToString());
            writer.WriteAttributeString("RollAngle", RollAngle.ToString());
            writer.WriteEndElement();
        }

        #endregion
    }
}
