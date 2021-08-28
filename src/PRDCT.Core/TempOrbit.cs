namespace PRDCT.Core
{
    //public class Orbit
    //{
    //    public Orbit(double a, double ecc, double incl)
    //    {
    //        this.a = a;
    //        this.ecc = ecc;
    //        this.Inclination = incl;
    //        p = a * (1 - ecc * ecc);
    //    }

    //    public double Semiaxis(double u)
    //    {
    //        return p / (1 + ecc * Math.Cos(u));
    //    }

    //    public double Anomalia(double t, double t0 = 0.0)
    //    {
    //        double M, v, e1, e2;
    //        double n = Math.Sqrt(MyConst.MU / a) / a;
    //        M = n * (t - t0);

    //        e1 = M;
    //        e2 = M + ecc * Math.Sin(e1);
    //        while (Math.Abs(e1 - e2) > 0.001)
    //        {
    //            e1 = e2;
    //            e2 = M + ecc * Math.Sin(e1);
    //        }
    //        v = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - ecc * ecc), Math.Cos(e2) - ecc);
    //        v = MyMath.WrapAngle(v);
    //        return v;
    //    }

    //    public double Period()
    //    {
    //        return (2.0 * Math.PI) / (Math.Sqrt(MyConst.MU / a) / a);
    //    }

    //    public double Inclination { get; private set; }

    //    private double a, ecc, p;
    //}

    //public class SubSatellitePoint
    //{
    //    private enum Quarters
    //    {
    //        None,
    //        First,
    //        Second,
    //        Third,
    //        Fourth
    //    }

    //    private Quarters Quarter(double angle)
    //    {
    //        if (angle >= 0.0 && angle < Math.PI / 2.0)
    //            return Quarters.First;
    //        else if (angle >= Math.PI / 2.0 && angle < Math.PI)
    //            return Quarters.Second;
    //        else if (angle >= Math.PI && angle < 3.0 * Math.PI / 2.0)
    //            return Quarters.Third;
    //        else if (angle >= 3.0 * Math.PI / 2.0 && angle < 2.0 * Math.PI)
    //            return Quarters.Fourth;
    //        return Quarters.None;
    //    }

    //    public SubSatellitePoint(Orbit orb, double alpha1, int pls, double alpha2 = 0.0)
    //    {
    //        this.orb = orb;
    //        this.alpha1 = alpha1;
    //        this.pls = pls;
    //        this.alpha2 = alpha2;
    //    }

    //    public Geo2D SubPoint(double u)
    //    {
    //        return (alpha2 == 0.0) ? SubPointGamma(u) : SubPointAlpha(u);
    //    }

    //    public Geo2D SubPointTemp(double u, double fi)
    //    {
    //        double angle = fi;//CentralAngle(u);
    //        double uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
    //        double iTr = orb.Inclination - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * pls;
    //        double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
    //        double asinlon = Math.Tan(lat) / Math.Tan(iTr);
    //        if (Math.Abs(asinlon) > 1.0)
    //            asinlon = Math.Sign(asinlon);

    //        switch(Quarter(u))
    //        {
    //            case Quarters.First:
    //                return new Geo2D(Math.Asin(asinlon), lat);
    //            case Quarters.Second:
    //            case Quarters.Third:
    //                return new Geo2D(Math.PI - Math.Asin(asinlon), lat);
    //            case Quarters.Fourth:
    //                return new Geo2D(2.0 * Math.PI + Math.Asin(asinlon), lat);
    //            default:
    //                return null;
    //        }
    //    }

    //    public Geo2D SubPointGamma(double u)
    //    {
    //        double uTr, iTr, angle;
    //        if (alpha1 == 0.0)
    //        {
    //            angle = 0.0;
    //            uTr = u;
    //            iTr = orb.Inclination;
    //        }
    //        else
    //        {
    //            angle = CentralAngle(u);
    //            uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
    //            iTr = orb.Inclination - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * pls;
    //        }
    //        double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
    //        double asinlon = Math.Tan(lat) / Math.Tan(iTr);
    //        if (Math.Abs(asinlon) > 1.0) asinlon = Math.Sign(asinlon);
    //        double lon = 0.0;
    //        if (u >= 0.0 && u < Math.PI / 2.0)
    //            lon = Math.Asin(asinlon);
    //        else if (u >= Math.PI / 2.0 && u < 3.0 * Math.PI / 2.0)
    //            lon = Math.PI - Math.Asin(asinlon);
    //        else if (u >= 3.0 * Math.PI / 2.0 && u < 2.0 * Math.PI)

    //            lon = 2.0 * Math.PI + Math.Asin(asinlon);
    //        return new Geo2D(lon, lat);
    //    }

    //    public Geo2D SubPointAlpha(double u)
    //    {
    //        double alphaGround = Math.Atan(Math.Tan(alpha2) * Math.Sin(alpha1));
    //        double semiAxis = orb.Semiaxis(u);
    //        double angleGam = Math.PI / 2.0 - (Math.Acos(semiAxis * Math.Sin(alpha1) / MyConst.RE)) - alpha1;
    //        //double angleGam    = CentralAngle(u);
    //        double angleAlpha = Math.Asin(Math.Sin(angleGam) / Math.Cos(alphaGround));
    //        double uTr = Math.Acos(Math.Cos(Math.PI / 2.0 + alphaGround) * Math.Sin(u) * Math.Sin(angleAlpha) + Math.Cos(u) * Math.Cos(angleAlpha));
    //        double iTr;
    //        if (u == 0.0 || u == Math.PI || u == 2.0 * Math.PI)
    //            iTr = orb.Inclination;
    //        else
    //            iTr = orb.Inclination - Math.Acos((Math.Cos(angleAlpha) - Math.Cos(u) * Math.Cos(uTr)) / (Math.Sin(u) * Math.Sin(uTr))) * pls;
    //        double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
    //        double sinLon = Math.Tan(lat) / Math.Tan(iTr);
    //        if (Math.Abs(sinLon) > 1.0) sinLon = Math.Sign(sinLon);
    //        double lon = 0.0;
    //        if (u >= 0.0 && u < Math.PI / 2.0)
    //            lon = Math.Asin(sinLon);
    //        else if (u >= Math.PI / 2.0 && u < 3.0 * Math.PI / 2.0)
    //            lon = Math.PI - Math.Asin(sinLon);
    //        else if (u >= 3.0 * Math.PI / 2.0 && u < 2.0 * Math.PI)

    //            lon = 2.0 * Math.PI + Math.Asin(sinLon);
    //        return new Geo2D(lon, lat);
    //    }

    //    public double CentralAngle(double u)
    //    {
    //        double semiAxis, alphaGround, angle;
    //        semiAxis = orb.Semiaxis(u);
    //        angle = Math.PI / 2.0 - (Math.Acos(semiAxis * Math.Sin(alpha1) / MyConst.RE)) - alpha1;
    //        if (alpha2 != 0.0)
    //        {
    //            alphaGround = Math.Atan(Math.Tan(alpha2) * Math.Sin(alpha1));
    //            angle = Math.Asin(Math.Sin(angle) / Math.Cos(alphaGround));
    //        }
    //        return angle;
    //    }

    //    private Orbit orb;
    //    private double alpha1, alpha2;
    //    private int pls;
    //}


    //public class PRDCTTrackPoint
    //{
    //    public Geo2D Point { get; set; }
    //    public double Time { get; set; }

    //    public int Node { get; set; }

    // //   public PRDCTTrack_ Track { get; set; }
    //}

    //public class PRDCTOrbit_
    //{
    //    public double anom(double tnorm, double t0)
    //    {
    //        double M, v, e1, e2;
    //        M = n * (tnorm - t0);

    //        e1 = M;
    //        e2 = M + ecc * Math.Sin(e1);
    //        while (Math.Abs(e1 - e2) > 0.001)
    //        {
    //            e1 = e2;
    //            e2 = M + ecc * Math.Sin(e1);
    //        }
    //        v = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - ecc * ecc), Math.Cos(e2) - ecc);
    //        v = MyMath.WrapAngle(v);
    //        return v;
    //    }

    //    public double ecc, incl, w, lonAN, t0;
    //    public double p, n;
    //}

    //public class PRDCTTrack_ : PRDCTOrbit_
    //{
    //    public PRDCTTrackPoint continuousTrassa(double node, double t, int quart)
    //    {
    //        double v = anom(t, t0);
    //        double u = v + w;
    //        u = MyMath.WrapAngle(u);

    //        double semi_axis = p / (1 + ecc * Math.Cos(u));
    //        double angle = Math.PI / 2.0 - (Math.Acos(semi_axis * Math.Sin(gam) / MyConst.RE)) - gam;
    //        double uTr = Math.Acos(Math.Cos(angle) * Math.Cos(u));
    //        double iTr = incl - Math.Atan2(Math.Tan(angle), Math.Sin(u)) * pls;
    //        double lat = Math.Asin(Math.Sin(uTr) * Math.Sin(iTr));
    //        double asinlon = Math.Tan(lat) / Math.Tan(iTr);

    //        if (Math.Abs(asinlon) > 1.0) asinlon = MyMath.Sign(asinlon);

    //        double lon = Math.Asin(asinlon);

    //        //if( quart == 1 )lon = lon;
    //        if (quart == 2 || quart == 3)
    //            lon = (Math.PI - lon) - factor.ch23 * 2.0 * Math.PI;
    //        else if (quart == 4)
    //            lon = 2.0 * Math.PI + lon - factor.ch4 * 2.0 * Math.PI;

    //        lon = lonAN + lon - MyConst.w3 * (t - t0) + node * 2.0 * Math.PI * factor.mdf;

    //        return new PRDCTTrackPoint {
    //            Point = new Geo2D(lon, lat, Geo2DTypes.Radians),
    //            Time = t,
    //          //  Track = this
    //        };
    //    }

    //    private double gam;
    //    private int pls;

    //    private FactorShiftTrack factor;
    //}

}
