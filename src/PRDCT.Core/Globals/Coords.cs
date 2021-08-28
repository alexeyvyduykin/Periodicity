using System;

namespace PRDCT.Core
{
    public enum GeoCoordTypes
    {
        Radians,
        Degrees
    }

    //public class Geo
    //{
    //    public static geo3 Empty = new geo3();

    //    private geo3()
    //    {
    //        this.Lon = Double.NaN;
    //        this.Lat = Double.NaN;
    //        this.Height = Double.NaN;
    //        this.Type = GeoCoordType.Radians;
    //    }

    //    public geo3(double lon, double lat, GeoCoordType type = GeoCoordType.Radians) : this(lon, lat, 0.0, type) { }

    //    public geo3(double lon, double lat, double height, GeoCoordType type = GeoCoordType.Radians)
    //    {
    //        this.Lon = lon;
    //        this.Lat = lat;
    //        this.Height = height;
    //        this.Type = type;
    //    }

    //    public void Radians()
    //    {
    //        if(Type == GeoCoordType.Degrees)
    //        {
    //            Lon *= MyMath.DegreesToRadians;
    //            Lat *= MyMath.DegreesToRadians;
    //            Type = GeoCoordType.Radians;
    //        }
    //    }

    //    public void Degrees()
    //    {
    //        if(Type == GeoCoordType.Radians)
    //        {
    //            Lon *= MyMath.RadiansToDegrees;
    //            Lat *= MyMath.RadiansToDegrees;
    //            Type = GeoCoordType.Degrees;
    //        }
    //    }

    //    public geo3 ToRadians()
    //    {
    //        Geo point = new Geo(Lon, Lat, Height, Type);
    //        point.Radians();
    //        return point;
    //    }

    //    public geo3 ToDegrees()
    //    {
    //        Geo point = new Geo(Lon, Lat, Height, Type);
    //        point.Degrees();
    //        return point;
    //    }

    //    private GeoCoordType Type { get; set; }
    //    public double Lon { get; set; }
    //    public double Lat { get; set; }
    //    public double Height { get; set; }
    //}


    public class Geo2D
    {
        public static Geo2D Empty = new Geo2D();

        private Geo2D()
        {
            Lon = Double.NaN;
            Lat = Double.NaN;
            Type = GeoCoordTypes.Radians;
        }

        public Geo2D(double lon, double lat, GeoCoordTypes type = GeoCoordTypes.Radians)
        {
            Lon = lon;
            Lat = lat;
            Type = type;
        }

        //public Geo2D LongitudeNormalize()
        //{
        //    Geo2D p = new Geo2D(Lon, Lat);
        //    while (p.Lon < 0.0)
        //        p.Lon += 2.0 * Math.PI;
        //    while (p.Lon > 2.0 * Math.PI)
        //        p.Lon -= 2.0 * Math.PI;

        //    return p;
        //}

        //public void Normalize()
        //{
        //    if (Type == GeoCoordTypes.Radians)
        //    {
        //        Lon = MyMath.WrapAngle(Lon);
        //    }
        //    else
        //    {
        //        Lon = MyMath.WrapAngle360(Lon);
        //    }
        //}

        //public static Geo2D Normalized(Geo2D position)
        //{
        //    Geo2D p = new Geo2D(position.Lon, position.Lat, position.Type);
        //    p.Normalize();
        //    return p;
        //}

        //public Geo2D Normalized()
        //{
        //    Geo2D p = new Geo2D(Lon, Lat, Type);
        //    p.Normalize();
        //    return p;
        //}

        public Geo2D ToRadians()
        {
            Geo2D point = new Geo2D(Lon, Lat, Type);
            point.Radians();
            return point;
        }

        public Geo2D ToDegrees()
        {
            Geo2D point = new Geo2D(Lon, Lat, Type);
            point.Degrees();
            return point;
        }

        private void Radians()
        {
            if (Type == GeoCoordTypes.Degrees)
            {
                Lon *= MyMath.DegreesToRadians;
                Lat *= MyMath.DegreesToRadians;
                Type = GeoCoordTypes.Radians;
            }
        }

        private void Degrees()
        {
            if (Type == GeoCoordTypes.Radians)
            {
                Lon *= MyMath.RadiansToDegrees;
                Lat *= MyMath.RadiansToDegrees;
                Type = GeoCoordTypes.Degrees;
            }
        }

        public GeoCoordTypes Type { get; protected set; }

        public double Lon { get; protected set; }
        public double Lat { get; protected set; }
    }

    public class Geo3D
    {
        public static Geo3D Empty = new Geo3D();

        private Geo3D()
        {
            Lon = Double.NaN;
            Lat = Double.NaN;
            W = Double.NaN;
            Type = GeoCoordTypes.Radians;
        }

        public Geo3D(double lon, double lat, double height, GeoCoordTypes type = GeoCoordTypes.Radians)
        {
            Lon = lon;
            Lat = lat;
            W = height;
            Type = type;
        }

        //public void Normalize()
        //{
        //    if (Type == GeoCoordTypes.Radians)
        //    {
        //        Lon = MyMath.WrapAngle(Lon);
        //    }
        //    else
        //    {
        //        while (Lon < 0.0)
        //            Lon += 360.0;
        //        while (Lon > 360.0)
        //            Lon -= 360.0;
        //    }
        //}

        //public static Geo3D Normalized(Geo3D position)
        //{
        //    Geo3D p = new Geo3D(position.Lon, position.Lat, position.Height, position.Type);
        //    p.Normalize();
        //    return p;
        //}

        //public Geo3D Normalized()
        //{
        //    Geo3D p = new Geo3D(Lon, Lat, Height, Type);
        //    p.Normalize();
        //    return p;
        //}

        public Geo3D ToRadians()
        {
            Geo3D point = new Geo3D(Lon, Lat, W, Type);
            point.Radians();
            return point;
        }

        public Geo3D ToDegrees()
        {
            Geo3D point = new Geo3D(Lon, Lat, W, Type);
            point.Degrees();
            return point;
        }

        private void Radians()
        {
            if (Type == GeoCoordTypes.Degrees)
            {
                Lon *= MyMath.DegreesToRadians;
                Lat *= MyMath.DegreesToRadians;
                Type = GeoCoordTypes.Radians;
            }
        }

        private void Degrees()
        {
            if (Type == GeoCoordTypes.Radians)
            {
                Lon *= MyMath.RadiansToDegrees;
                Lat *= MyMath.RadiansToDegrees;
                Type = GeoCoordTypes.Degrees;
            }
        }

        public GeoCoordTypes Type { get; protected set; }

        public double Lon { get; protected set; }
        public double Lat { get; protected set; }
        public double W { get; protected set; }
    }

}
