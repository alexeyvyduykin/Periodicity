namespace PRDCT.Core
{



    //public abstract class ObjectHeader
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; } = "";

    //    public object Tag
    //    {
    //        get
    //        {
    //            return GetTag();
    //        }

    //        set
    //        {
    //            SetTag(value);
    //        }
    //    }

    //    public ObjectHeader Parent { get; set; } = null;

    //    protected abstract object GetTag();
    //    protected abstract void SetTag(object obj);
    //}

    //public class ObjectHeader<T> : ObjectHeader
    //{
    //    public ObjectHeader() { }

    //    public ObjectHeader(Guid id, string name, string description)
    //    {
    //        base.Id = id;
    //        base.Name = name;
    //        base.Description = description;
    //    }

    //    protected override object GetTag()
    //    {
    //        return Tag;
    //    }

    //    protected override void SetTag(object obj)
    //    {
    //        Tag = (T)obj;
    //    }

    //    public new T Tag { get; set; }
    //}

}
