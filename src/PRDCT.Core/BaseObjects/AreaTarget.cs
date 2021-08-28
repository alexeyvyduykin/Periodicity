using System;
using System.Collections.Generic;
using System.Xml;
using GlmSharp;

namespace Periodicity.Core
{
    public enum AreaTargetType
    {
        Rectangle = 0,
        Polygon = 1
    }

    public class BaseAreaTarget : BaseObject
    {
        public BaseAreaTarget()
        {
            base.Id = Guid.NewGuid();
            base.Name = "Region1";
            base.Type = BaseObjectType.Region;
            Data = new List<dvec2>();
        }

        public BaseAreaTarget(IEnumerable<dvec2> verts) : this()
        {
            DataType = AreaTargetType.Polygon;
            Data.AddRange(verts);
        }

        public BaseAreaTarget(double left, double right, double bottom, double top) : this()
        {
            DataType = AreaTargetType.Rectangle;
            Data.AddRange(new List<dvec2>
            {
                new dvec2(left, bottom),
                new dvec2(left, top),
                new dvec2(right, top),
                new dvec2(right, bottom)
            });
        }

        public BaseAreaTarget Clone()
        {
            var newAreaTarget = new BaseAreaTarget()
            {
                Id = base.Id,
                Name = base.Name,
                Description = base.Description,
                Type = base.Type,
                DataType = DataType
            };
            newAreaTarget.Data = new List<dvec2>();
            newAreaTarget.Data.AddRange(Data);
            return newAreaTarget;
        }

        public AreaTargetType DataType { get; protected set; }
        public List<dvec2> Data { get; protected set; }

        #region Serializable

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetType().ToString())
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];

                DataType = (AreaTargetType)Enum.Parse(typeof(AreaTargetType), reader["DataType"]);

                reader.Read(); // AreatTarget -> Data
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Data")
                {
                    Data.Clear();
                    reader.Read(); // Data -> Vertex
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Vertex")
                    {
                        double x = Double.Parse(reader["x"]);
                        double y = Double.Parse(reader["y"]);
                        Data.Add(new dvec2(x, y));
                        reader.Read(); // next element
                    }
                    reader.Read(); // Data <- Vertex
                }
                reader.Read(); // AreatTarget <- Data
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);

            writer.WriteAttributeString("DataType", DataType.ToString());

            writer.WriteStartElement("Data");
            foreach (var vert in Data)
            {
                writer.WriteStartElement("Vertex");
                writer.WriteAttributeString("x", vert.x.ToString());
                writer.WriteAttributeString("y", vert.y.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        #endregion
    }
}
