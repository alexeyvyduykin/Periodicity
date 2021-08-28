using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;

namespace PRDCT.Core
{
    public class Ellipsoid
    {
        public static readonly Ellipsoid WGS84 = new Ellipsoid(6378137.0, 6378137.0, 6356752.3142451793);   // f = 298.257223563
        public static readonly Ellipsoid PZ90 = new Ellipsoid(6378136.0, 6378136.0, 6356751.361745713);   // f = 298.257839303

        public Ellipsoid(double x, double y, double z)
        {
            _radii = new dvec3(x, y, z);

            _radiiSquared = new dvec3(
                _radii.x * _radii.x, 
                _radii.y * _radii.y, 
                _radii.z * _radii.z);

            _radiiToTheFourth = new dvec3(
                _radiiSquared.x * _radiiSquared.x, 
                _radiiSquared.y * _radiiSquared.y, 
                _radiiSquared.z * _radiiSquared.z);

            _oneOverRadiiSquared = new dvec3(
                1.0 / (_radii.x * _radii.x), 
                1.0 / (_radii.y * _radii.y), 
                1.0 / (_radii.z * _radii.z));
        }

        public dvec3 GeodeticSurfaceNormal(Geo3D geodetic)
        {
            double cosLatitude = Math.Cos(geodetic.Lat);

            return new dvec3(
                cosLatitude * Math.Cos(geodetic.Lon),
                cosLatitude * Math.Sin(geodetic.Lon),
                Math.Sin(geodetic.Lat));
        }

        public dvec3 GeodeticSurfaceNormal(dvec3 positionOnEllipsoid)
        {
            return (positionOnEllipsoid * _oneOverRadiiSquared).Normalized;
        }

        public dvec3 ToCartesian(Geo3D geodetic)
        {
            dvec3 n = GeodeticSurfaceNormal(geodetic);
            dvec3 k = n * _radiiSquared;

            double gamma = Math.Sqrt(n.x * k.x + n.y * k.y + n.z * k.z);

            dvec3 rSurface = k / gamma;

            return rSurface + n * geodetic.W;
        }

        public Geo2D ToGeodetic2D(dvec3 position)
        {
            dvec3 n = GeodeticSurfaceNormal(position);
            return new Geo2D(Math.Atan2(n.y, n.x), Math.Asin(n.z / n.Length));
        }

        public Geo3D ToGeodetic3D(dvec3 position)
        {
            dvec3 p = ScaleToGeodeticSurface(position);
            dvec3 h = position - p;
            double height = Math.Sign(dvec3.Dot(h, position)) * h.Length;
            var point = ToGeodetic2D(p);
            return new Geo3D(point.Lon, point.Lat, height);
        }

        public dvec3 ScaleToGeodeticSurface(dvec3 position)
        {
            double x2 = position.x * position.x * _oneOverRadiiSquared.x;
            double y2 = position.y * position.y * _oneOverRadiiSquared.y;
            double z2 = position.z * position.z * _oneOverRadiiSquared.z;

            double beta = 1.0 / Math.Sqrt(x2 + y2 + z2);

            double n = new dvec3(
                position.x * beta * _oneOverRadiiSquared.x,
                position.y * beta * _oneOverRadiiSquared.y,
                position.z * beta * _oneOverRadiiSquared.z).Length;

            double alpha = (1.0 - beta) * (position.Length / n);

            double da = 0.0, db = 0.0, dc = 0.0;
            double s = 0.0, dSdA = 1.0;

            do
            {
                alpha -= (s / dSdA);

                da = 1.0 + (alpha * _oneOverRadiiSquared.x);
                db = 1.0 + (alpha * _oneOverRadiiSquared.y);
                dc = 1.0 + (alpha * _oneOverRadiiSquared.z);

                double da2 = da * da;
                double db2 = db * db;
                double dc2 = dc * dc;

                double da3 = da2 * da;
                double db3 = db2 * db;
                double dc3 = dc2 * dc;

                s = x2 / (_radiiSquared.x * da2) + y2 / (_radiiSquared.y * db2) + z2 / (_radiiSquared.z * dc2) - 1.0;
                dSdA = -2.0 * (x2 / (_radiiToTheFourth.x * da3) + y2 / (_radiiToTheFourth.y * db3) + z2 / (_radiiToTheFourth.z * dc3));

            } while (Math.Abs(s) > 1e-10);

            return new dvec3(position.x * da, position.y * db, position.z * dc);
        }

        public dvec3 ScaleToGeocentricSurface(dvec3 position)
        {
            double beta = 1.0 / Math.Sqrt(
                (position.x * position.x) * _oneOverRadiiSquared.x +
                (position.y * position.y) * _oneOverRadiiSquared.y +
                (position.z * position.z) * _oneOverRadiiSquared.z);

            return position * beta;
        }

        public double MinimumRadius
        {
            get
            {
                return Math.Min(_radii.x, Math.Min(_radii.y, _radii.z));
            }
        }

        public double MaximumRadius
        {
            get
            {
                return Math.Max(_radii.x, Math.Max(_radii.y, _radii.z));
            }
        }

        private dvec3 _radii;
        private dvec3 _radiiSquared;
        private dvec3 _radiiToTheFourth;
        private dvec3 _oneOverRadiiSquared;
    }
}
