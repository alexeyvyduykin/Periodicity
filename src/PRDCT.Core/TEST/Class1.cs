using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;

namespace PRDCT.Core
{
    //public static class SerializeXML
    //{
    //    public static XElement FromObjectToXML(Type type, object obj)
    //    {
    //        string xmlString = null;
    //        XmlSerializer xmlSerializer = new XmlSerializer(type);
    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            xmlSerializer.Serialize(memoryStream, obj);
    //            memoryStream.Position = 0;
    //            xmlString = new StreamReader(memoryStream).ReadToEnd();
    //        }
    //        return XElement.Parse(xmlString);
    //    }
    //}


    //public interface IObjectXML
    //{

    //    XElement ToXML();
    //}

    //public class STKObjectRoot
    //{

    //}

        
    public abstract class STKObject : IXmlSerializable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public Property AccessConstraints Get the constraints imposed on the object.  
        public List<STKObject> Children;
        //public Property ClassName Returns a class name of the object (i.e.Aircraft, Facility.)  
        //public Property ClassType Returns a class type of the object (i.e.eAircraft, eFacility etc.)
        //public Property DataProviders Returns the object representing a list of available data providers for the object.  
        public bool HasChildren;
        public string Description;
        //public Property ObjectCoverage Returns an IAgStkObjectCoverage object.  
        public STKObject Parent;
        //public Property Path Returns the object path.  
 //       public STKObjectRoot Root;

        public XmlSchema GetSchema() { return null; }
        public abstract void ReadXml(XmlReader reader);
        public abstract void WriteXml(XmlWriter writer);
    }


    public class ScenarioCs : IXmlSerializable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #region Serializable 
       
        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "ScenarioCs")
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];

                reader.Read(); // Skip ahead to next node
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Objects")
                {
                    reader.Read(); // Skip ahead to next node
                    while (reader.MoveToContent() == XmlNodeType.Element && Type.GetType(reader.LocalName).IsSubclassOf(typeof(STKObject)))           
                    {
                        Type objectType = Type.GetType(reader.LocalName);
                        //STKObject obj = AnimalType.Assembly.CreateInstance(reader.LocalName);
                        STKObject obj = (STKObject)Activator.CreateInstance(objectType);

                        obj.ReadXml(reader);
                        Objects.Add(obj);
                        reader.Read(); // Skip to next animal (if there is one)
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);
            writer.WriteStartElement("Objects");
            foreach (STKObject obj in Objects)
            {
                obj.WriteXml(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion

        public List<STKObject> Objects { get; set; }
    }


    public class SatelliteCs : STKObject
    {

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "ScenarioCs")
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];
            }
        }
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);
            writer.WriteEndElement();
        }
    }

    public class RegionCs : STKObject
    {

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "ScenarioCs")
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];
            }
        }
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);
            writer.WriteEndElement();
        }
    }
}
