using System;
using System.Collections.Generic;
using GlmSharp;


namespace Periodicity.Core
{

    public class CoverageRegion
    {
        public CoverageRegion(dvec2 p0, double w, double h, int numPoints)
        {
            P0 = p0;
            Width = w;
            Height = h;
            double dw = w / numPoints;
            double dh = h / numPoints;
            double w0 = p0.x + dw / 2.0;
            double h0 = p0.y + dh / 2.0;

            Points = new List<dvec2>();

            for (int j = 0; j < numPoints; j++)
            {
                for (int i = 0; i < numPoints; i++)
                {
                    Points.Add(new dvec2(w0 + i * dw, h0 + j * dh));
                }
            }
        }

        public dvec2 P0 { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }

        public List<dvec2> Points { get; protected set; }
    }

    public class CoverageDefinition
    {
        public CoverageDefinition(double minLat, double maxLat, double resolution)
        {
            MinLatitude = minLat;
            MaxLatitude = maxLat;
            ResolutionLonLat = resolution;
            Regions = new List<CoverageRegion>();
            Update();
        }

        private double LengthArcEarth(double latitudeDEG)
        {
            double r1 = 6378.137;
            double r2 = 6356.752;

            double B = latitudeDEG * MyMath.DegreesToRadians;
            double R = Math.Sqrt((Math.Pow((r1 * r1 * Math.Cos(B)), 2) + Math.Pow((r2 * r2 * Math.Sin(B)), 2)) /
                (Math.Pow((r1 * Math.Cos(B)), 2) + Math.Pow((r2 * Math.Sin(B)), 2)));
            return Math.Cos(B) * 2.0 * Math.PI * R;
        }

        private void CreateRegions(int numPoints)
        {
            double heightBoundsDEG = MaxLatitude - MinLatitude;
            int numColumnRegions = (int)Math.Round(heightBoundsDEG / (numPoints * ResolutionLonLat));
            double heightRegion = heightBoundsDEG / numColumnRegions;

            for (int j = 0; j < numColumnRegions; j++)
            {
                double latCenter = MinLatitude + heightRegion / 2.0 + j * heightRegion;
                double LCenter = LengthArcEarth(latCenter);

                int numRowRegions = (int)Math.Round(LCenter / (numPoints * ResolutionDistance));
                double widthRegion = LCenter / numRowRegions;
                widthRegion = (widthRegion / Globals.Re) * MyMath.RadiansToDegrees;

                widthRegion = 360.0 / numRowRegions;

                for (int i = 0; i < numRowRegions; i++)
                {
                    dvec2 p0 = new dvec2(0.0 + i * widthRegion, MinLatitude + 0.0 + j * heightRegion);
                    CoverageRegion reg = new CoverageRegion(p0, widthRegion, heightRegion, numPoints);
                    Regions.Add(reg);
                }
            }
        }

        private void Update()
        {
            double heightBounds = MaxLatitude - MinLatitude;

            if (ResolutionLonLat <= 10.0 / 27.0) // 0.373737       // 81 x 81
            {
                if (27 * ResolutionLonLat * 2.0 <= heightBounds)
                {
                    CreateRegions(81);
                }
                else if (27 * ResolutionLonLat * 2.0 > heightBounds)
                {
                    CreateRegions(27);
                }
                else if (9 * ResolutionLonLat * 2.0 > heightBounds)
                {
                    CreateRegions(9);
                }
                else
                {
                    CreateRegions(3);
                }
            }
            else if (ResolutionLonLat <= 10.0 / 9.0) // 1.111       // 27 x 27
            {
                if (9 * ResolutionLonLat * 2.0 <= heightBounds)
                {
                    CreateRegions(27);
                }
                else if (9 * ResolutionLonLat * 2.0 > heightBounds)
                {
                    CreateRegions(9);
                }
                else
                {
                    CreateRegions(3);
                }
            }
            else if (ResolutionLonLat <= 10.0 / 3.0) // 3.333       // 9 x 9
            {
                if (3 * ResolutionLonLat * 2.0 <= heightBounds)
                {
                    CreateRegions(9);
                }
                else
                {
                    CreateRegions(3);
                }

            }
            else // all        // 3 x 3
            {
                CreateRegions(3);
            }
        }

        public List<CoverageRegion> Regions { get; set; }

        public double MinLatitude { get; protected set; }
        public double MaxLatitude { get; protected set; }

        public double ResolutionDistance
        {
            get
            {
                return resolutionDistance;
            }
            set
            {
                resolutionDistance = value;
                resolutionLonLat = (resolutionDistance / Globals.Re) * MyMath.RadiansToDegrees;
            }
        }
        public double ResolutionLonLat
        {
            get
            {
                return resolutionLonLat;
            }
            set
            {
                resolutionLonLat = value;
                resolutionDistance = resolutionLonLat * MyMath.DegreesToRadians * Globals.Re;
            }
        }

        private double resolutionDistance, resolutionLonLat;
    }
}
