using System;
using System.Collections.Generic;
using System.Linq;

namespace Periodicity.Core
{
    public class BaseScenario
    {
        public event EventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public BaseScenario() : this(Guid.NewGuid(), "DefaultScenario", "Created Default Constructor") { }

        public BaseScenario(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

            Objects = new List<BaseObject>();
        }

        public static BaseScenario Clone(BaseScenario scenario)
        {
            var sc = new BaseScenario(Guid.NewGuid(), scenario.Name, scenario.Description)
            {
                Objects = scenario.Objects

            };

            sc.Changed += scenario.Changed;

            return sc;
        }

        public void Add(BaseScenario scenario)
        {
            foreach (var item in scenario.Objects)
            {
                if (Objects.TrueForAll(obj => obj.Id != item.Id))
                {
                    Objects.Add(item);
                }
            }
            OnChanged(EventArgs.Empty);
        }

        public void Clear()
        {
            Objects.Clear();
            OnChanged(EventArgs.Empty);
        }

        public void Add(BaseObject obj)
        {
            Objects.Add(obj);
            OnChanged(EventArgs.Empty);
        }

        public void Delete(BaseObject obj)
        {
            if (Objects.Contains(obj) == true)
            {
                if (obj is ParentBaseObject)
                {
                    foreach (var item in Objects.Where(b => (b as ParentBaseObject).ParentId == obj.Id).Reverse())  // delete all child objects                
                    {
                        Objects.Remove(item);
                    }
                }

                Objects.Remove(obj);
                OnChanged(EventArgs.Empty);
            }
        }

        public IEnumerable<T> ToObjects<T>() where T : BaseObject
        {
            return Objects.Where(obj => obj is T).Select(obj => (T)obj);
        }

        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private List<BaseObject> Objects { get; set; }
    }

}
