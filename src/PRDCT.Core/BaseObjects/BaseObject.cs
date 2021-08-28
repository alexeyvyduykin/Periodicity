using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PRDCT.Core
{
    public interface IHeaderBaseObject
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        BaseObjectType Type { get; }
    }


    public enum BaseObjectType
    {
        Satellite = 1,
        Region = 2,
        Sensor = 3
    }

    public abstract class BaseObject : IXmlSerializable, IHeaderBaseObject
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BaseObjectType Type { get; protected set; }

        //public Property AccessConstraints Get the constraints imposed on the object.  
        //   public List<BaseObject> Children;
        //public Property ClassName Returns a class name of the object (i.e.Aircraft, Facility.)  
        //public Property ClassType Returns a class type of the object (i.e.eAircraft, eFacility etc.)
        //public Property DataProviders Returns the object representing a list of available data providers for the object.  
        //   public bool HasChildren;

        //public Property ObjectCoverage Returns an IAgStkObjectCoverage object.  

        //public Property Path Returns the object path.  
        //       public STKObjectRoot Root;

        public XmlSchema GetSchema() { return null; }
        public abstract void ReadXml(XmlReader reader);
        public abstract void WriteXml(XmlWriter writer);
    }

    public abstract class ParentBaseObject : BaseObject
    {
        //  public BaseObject Parent { get; protected set; }
        public Guid ParentId { get; protected set; }
    }

}
