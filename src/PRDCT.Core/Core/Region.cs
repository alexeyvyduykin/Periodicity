using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using GlmSharp;

namespace Periodicity.Core
{
    public enum RegionType
    {
        Zone = 0,
        Rectangle = 1,
        Polygon = 2,
        Error = 3
    }

    public class Region : BaseEntity
    {
        public static Region DefaultZone = new Region(0, 360, 0, 90);

        private Region() { }

        private Region(string name)
        {
            Name = name;
        }

        public Region(string name, List<dvec2> verts2D, RegionType type) : this(name)
        {
            Type = type;
            Verts = new List<dvec2>();
            Lines = new List<dvec2>();
            Normals = new List<dvec2>();

            for (int i = 0; i < verts2D.Count; i++)
            {
                int k = i + 1;

                if (i == verts2D.Count - 1)
                {
                    k = 0;
                }

                Verts.Add(verts2D[i]);
                Lines.Add(verts2D[k] - verts2D[i]);
                Normals.Add(Lines[i].Rotated(Math.PI / 2.0));
            }

            float xMin = (float)verts2D.Min(s => s.x);
            float xMax = (float)verts2D.Max(s => s.x);
            float yMin = (float)verts2D.Min(s => s.y);
            float yMax = (float)verts2D.Max(s => s.y);

            BoundingRectangle = new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        private Region(double left, double right, double bottom, double top) : this("DefaultZone")
        {
            Type = RegionType.Zone;

            Verts = new List<dvec2>
            {
                new dvec2(left, bottom),
                new dvec2(left, top),
                new dvec2(right, top),
                new dvec2(right, bottom)
            };
            
            BoundingRectangle = new RectangleF((float)left, (float)bottom, (float)(right - left), (float)(top - bottom));
        }

        private bool IsInside(dvec2 point)
        {
            double alf, x1, y1, x2, y2, pz;
            double eps = 0.05;
            double summa_alf = 0.0;

            for (int i = 0; i < Verts.Count - 1; ++i)
            {
                x1 = Verts[i].x - point.x;
                y1 = Verts[i].y - point.y;

                x2 = Verts[i + 1].x - point.x;
                y2 = Verts[i + 1].y - point.y;

                alf = Math.Acos((x1 * x2 + y1 * y2) / (Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2)));
                pz = x1 * y2 - y1 * x2;
                if (pz > 0)
                {
                    alf = -alf;
                }

                summa_alf = summa_alf + alf;
            }

            x1 = Verts.Last().x - point.x;
            y1 = Verts.Last().y - point.y;
            x2 = Verts[0].x - point.x;
            y2 = Verts[0].y - point.y;
            alf = Math.Acos((x1 * x2 + y1 * y2) / (Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2)));
            pz = x1 * y2 - y1 * x2;
            if (pz > 0)
            {
                alf = -alf;
            }

            summa_alf = summa_alf + alf;

            if (Math.Abs(2.0 * Math.PI - Math.Abs(summa_alf)) < eps)
            {
                return true;
            }

            return false;
        }

        public List<dvec2> Verts { get; private set; }    // вершина полигона

        public List<dvec2> Lines { get; private set; }    // ограничивающая прямая полигона

        public List<dvec2> Normals { get; private set; }  //

        public double Left => BoundingRectangle.Left;

        public double Right => BoundingRectangle.Right;

        public double Bottom => BoundingRectangle.Top;

        public double Top => BoundingRectangle.Bottom;

        public RectangleF BoundingRectangle { get; }

        public RegionType Type { get; }
    }
}
