using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using Microsoft.SqlServer.Types;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using PRDCT.Data;

namespace PRDCT.Core
{
    //public static class Converter
    //{
    //    //public static List<Band> Converting(PRDCT.Data.Satellites sat, DateTime begin, DateTime end)
    //    //{
    //    //    int numBands = 0;
    //    //    BandMode[] typeLine = new BandMode[2];

    //    //    switch (sat.Sensors.SARMode)
    //    //    {
    //    //        case "ONE":
    //    //            numBands = 1;
    //    //            typeLine[0] = BandMode.Middle;
    //    //            break;
    //    //        case "LEFT":
    //    //            numBands = 1;
    //    //            typeLine[0] = BandMode.Left;
    //    //            break;
    //    //        case "RIGHT":
    //    //            numBands = 1;
    //    //            typeLine[0] = BandMode.Right;
    //    //            break;
    //    //        case "TWO":
    //    //            numBands = 2;
    //    //            typeLine[0] = BandMode.Right;
    //    //            typeLine[1] = BandMode.Left;
    //    //            break;
    //    //    }

    //    //    List<Band> bands = new List<Band>();

    //    //    Orbit orbit =
    //    //        new Orbit(
    //    //            sat.Orbits.SemimajorAxis,
    //    //            sat.Orbits.Eccentricity,
    //    //            sat.Orbits.Inclination * MyMath.DegreesToRadians,
    //    //            sat.Orbits.ArgumentOfPerigee * MyMath.DegreesToRadians,
    //    //            sat.Orbits.LonAscnNode * MyMath.DegreesToRadians,
    //    //            sat.Orbits.RAAN * MyMath.DegreesToRadians,
    //    //            sat.Orbits.Period,
    //    //            sat.Orbits.Epoch);

    //    //    for (int i = 0; i < numBands; i++)
    //    //    {
    //    //        Band band = new Band(orbit, sat.Sensors.SARMinAngle, sat.Sensors.SARMaxAngle, typeLine[i]);
    //    //        //              band.SatelliteID = Guid.Parse(sat.SatelliteID);
    //    //        //          band.SetCalculationTime(begin, end);
    //    //        //band.InitBands(sat.Sensors.SARMinAngle, sat.Sensors.SARMaxAngle, typeLine[i]);

    //    //        bands.Add(band);
    //    //    }

    //    //    return bands;
    //    //}
        
    //    public static BandMode[] ToBandMode(string sesorMode)
    //    {
    //        switch (sesorMode)
    //        {
    //            case "ONE":
    //                return new BandMode[] { BandMode.Middle };
    //            case "LEFT":
    //                return new BandMode[] { BandMode.Left };
    //            case "RIGHT":
    //                return new BandMode[] { BandMode.Right };
    //            case "TWO":
    //                return new BandMode[] { BandMode.Left, BandMode.Right };
    //            default:
    //                return new BandMode[] { };
    //        }
    //    }

    //    public static SensorMode ToSensorMode(string mode)
    //    {
    //        switch (mode)
    //        {
    //            case "ONE":
    //                return SensorMode.One;
    //            case "LEFT":
    //                return SensorMode.Left;
    //            case "RIGHT":
    //                return SensorMode.Right;
    //            case "TWO":
    //                return SensorMode.Two;
    //            default:
    //                return SensorMode.Error;
    //        }
    //    }

    //    public static string ToSensorMode(SensorMode mode)
    //    {
    //        switch (mode)
    //        {
    //            case SensorMode.One:
    //                return "One";
    //            case SensorMode.Left:
    //                return "Left";
    //            case SensorMode.Right:
    //                return "Right";
    //            case SensorMode.Two:
    //                return "Two";
    //            default:
    //                return "Error";
    //        }
    //    }



    ////    public static Satellite ToSatellite(Satellites sat, DateTime begin, DateTime end)
    ////    {
    ////        BandMode[] typeLine = ToBandMode(sat.Sensors.SARMode);
    ////        List<Band> bands = new List<Band>();
    ////        Orbit orbit =
    ////            new Orbit(
    ////                sat.Orbits.SemimajorAxis,
    ////                sat.Orbits.Eccentricity,
    ////                sat.Orbits.Inclination * MyMath.DegreesToRadians,
    ////                sat.Orbits.ArgumentOfPerigee * MyMath.DegreesToRadians,
    ////                sat.Orbits.LonAscnNode * MyMath.DegreesToRadians,
    ////                sat.Orbits.RAAN * MyMath.DegreesToRadians,
    ////                sat.Orbits.Period,
    ////                sat.Orbits.Epoch);

    ////        Satellite satellite = new Satellite(orbit, begin, end);

    //////        foreach (var item in typeLine)            
    //////            satellite.Bands.Add(new Band(orbit, sat.Sensors.SARMinAngle, sat.Sensors.SARMaxAngle, item));

    ////        return satellite;
    ////    }

    //    //public static ObjectHeader ToSatelliteHeader(Satellites satellite, DateTime begin, DateTime end)
    //    //{
    //    //    return new ObjectHeader<Satellite>(Guid.Parse(satellite.SatelliteID), satellite.SatelliteName, satellite.Description)
    //    //    {
    //    //        Object = ToSatellite(satellite, begin, end)
    //    //    };
    //    //}

    //    public static RegionType ToRegionType(string type)
    //    {
    //        switch (type)
    //        {
    //            case "ZONE":
    //                return RegionType.Zone;
    //            case "RECTANGLE":
    //                return RegionType.Rectangle;
    //            case "POLYGON":
    //                return RegionType.Polygon;
    //            default:
    //                return RegionType.Error;
    //        }
    //    }

    //    public static ObjectHeader ToRegionHeader(this Regions value)
    //    {
    //        switch (ToRegionType(value.RegionType))
    //        {
    //            case RegionType.Zone:
    //                return new ObjectHeader<Region>//(Guid.Parse(region.RegionID), region.RegionName)
    //                {
    //                    Id = Guid.Parse(value.RegionID),
    //                    Name = value.RegionName,
    //                    Tag = ToRegion(value)
    //                };

    //            case RegionType.Rectangle:
    //                return new ObjectHeader<Region>//(Guid.Parse(region.RegionID), region.RegionName)
    //                {
    //                    Id = Guid.Parse(value.RegionID),
    //                    Name = value.RegionName,
    //                    Tag = ToRegion(value)
    //                };

    //            case RegionType.Polygon:
    //                return null;

    //            default:
    //                return null;
    //        }
    //    }

    //    public static Region ToRegion(this Regions value)
    //    {
    //        switch (ToRegionType(value.RegionType))
    //        {
    //            case RegionType.Zone:

    //                string[] data = value.RegionData.Split(' ');

    //                return new Region(
    //                    new List<dvec2>
    //                    {
    //                        new dvec2(0.0, Double.Parse(data[0])),
    //                        new dvec2(360.0, Double.Parse(data[0])),
    //                        new dvec2(360.0, Double.Parse(data[1])),
    //                        new dvec2(0.0, Double.Parse(data[1]))
    //                    },
    //                    RegionType.Zone);

    //            case RegionType.Rectangle:

    //                SqlGeography sqlRegion = SqlGeography.Parse(value.RegionData);

    //                List<SqlGeography> points = new List<SqlGeography> { sqlRegion.STPointN(1), sqlRegion.STPointN(2), sqlRegion.STPointN(3), sqlRegion.STPointN(4) };

    //                return new Region(
    //                    new List<dvec2>
    //                    {
    //                        new dvec2((double)points.Min(c => c.Long), (double)points.Max(c => c.Lat)),
    //                        new dvec2((double)points.Max(c => c.Long), (double)points.Max(c => c.Lat)),
    //                        new dvec2((double)points.Max(c => c.Long), (double)points.Min(c => c.Lat)),
    //                        new dvec2((double)points.Min(c => c.Long), (double)points.Min(c => c.Lat))
    //                    },
    //                    RegionType.Rectangle);

    //            case RegionType.Polygon:
    //                return null;

    //            default:
    //                return null;
    //        }
    //    }

    //}
}
