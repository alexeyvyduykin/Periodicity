using System;

namespace Periodicity.Core
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

    public abstract class BaseObject : IHeaderBaseObject
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BaseObjectType Type { get; protected set; }
    }

    public abstract class ParentBaseObject : BaseObject
    {
        public Guid ParentId { get; protected set; }
    }

}
