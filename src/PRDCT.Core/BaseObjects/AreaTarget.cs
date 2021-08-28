using System;
using System.Collections.Generic;
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
    }
}
