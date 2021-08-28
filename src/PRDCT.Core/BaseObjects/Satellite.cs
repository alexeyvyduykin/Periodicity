using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PRDCT.Core
{

    public class BaseSatellite : BaseObject
    {
        public BaseSatellite() : this(Guid.NewGuid()) { }

        private BaseSatellite(Guid id)
        {
            base.Id = id;
            base.Name = "Satellite1";
            base.Type = BaseObjectType.Satellite;

            OrbitState = new OrbitState();
        }

        public OrbitState OrbitState { get; set; }

        public BaseSatellite Clone()
        {
            BaseSatellite newSat = new BaseSatellite(Id)
            {
                Name = Name,
                Description = Description,
                Type = Type
            };

            newSat.OrbitState.SizeShape.SemimajorAxis = OrbitState.SizeShape.SemimajorAxis;
            newSat.OrbitState.SizeShape.Eccentricity = OrbitState.SizeShape.Eccentricity;

            newSat.OrbitState.Orientation.Inclination = OrbitState.Orientation.Inclination;
            newSat.OrbitState.Orientation.RAAN = OrbitState.Orientation.RAAN;
            newSat.OrbitState.Orientation.ArgumentOfPerigee = OrbitState.Orientation.ArgumentOfPerigee;

            newSat.OrbitState.Location.TrueAnomaly = OrbitState.Location.TrueAnomaly;

            newSat.OrbitState.OrbitEpoch = OrbitState.OrbitEpoch;

            return newSat;
        }

        #region Serializable

        public override void ReadXml(XmlReader reader)     // читать
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetType().ToString())
            {
                Id = Guid.Parse(reader["Id"]);
                Name = reader["Name"];
                Description = reader["Description"];

                reader.Read(); // Skip ahead to next node
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "OrbitState")
                {
                    //reader.Read(); // Skip ahead to next node
                    OrbitState.ReadXml(reader);
                }
                reader.Read();
            }
        }

        public override void WriteXml(XmlWriter writer)    // записывать
        {
            writer.WriteStartElement(GetType().ToString());
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Description", Description);
            writer.WriteStartElement("OrbitState");
            OrbitState.WriteXml(writer);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion
    }


    public class OrbitState : IXmlSerializable
    {
        public OrbitState()
        {
            SizeShape = new SizeShape()
            {
                SemimajorAxis = 6955.14,
                Eccentricity = 0.0
            };

            Orientation = new Orientation(this)
            {
                Inclination = 97.65,
                RAAN = 269.663,
                ArgumentOfPerigee = 0.0
            };

            Location = new Location(this)
            {
                TrueAnomaly = 0.0
            };
            //   22/06/2015 00:00:00
            OrbitEpoch = new DateTime(2015, 6, 22, 0, 0, 0);
        }

        public DateTime OrbitEpoch { get; set; }

        public SizeShape SizeShape { get; set; }
        public Orientation Orientation { get; set; }
        public Location Location { get; set; }

        public double SiderealTime()
        {
            //    double JD = OrbitEpoch.Date.ToOADate() + 2415018.5;
            //    double S0 = MyFunction.uds1900(JD);
            //    double S = S0 + Globals.Omega * OrbitEpoch.TimeOfDay.TotalSeconds;
            Julian jd = new Julian(OrbitEpoch);
            return jd.ToGmst();
        }

        #region Serializable

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)     // читать
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "OrbitState")
            {
                OrbitEpoch = DateTime.Parse(reader["OrbitEpoch"]);
                SizeShape.SemimajorAxis = Double.Parse(reader["SemimajorAxis"]);
                SizeShape.Eccentricity = Double.Parse(reader["Eccentricity"]);
                Orientation.Inclination = Double.Parse(reader["Inclination"]);
                Orientation.RAAN = Double.Parse(reader["RAAN"]);
                Orientation.ArgumentOfPerigee = Double.Parse(reader["ArgumentOfPerigee"]);
                Location.TrueAnomaly = Double.Parse(reader["TrueAnomaly"]);

                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)   // записывать
        {
            //  writer.WriteStartElement("OrbitState");

            writer.WriteAttributeString("OrbitEpoch", OrbitEpoch.ToString());

            writer.WriteAttributeString("SemimajorAxis", SizeShape.SemimajorAxis.ToString());
            writer.WriteAttributeString("Eccentricity", SizeShape.Eccentricity.ToString());

            writer.WriteAttributeString("Inclination", Orientation.Inclination.ToString());
            writer.WriteAttributeString("RAAN", Orientation.RAAN.ToString());
            writer.WriteAttributeString("ArgumentOfPerigee", Orientation.ArgumentOfPerigee.ToString());

            writer.WriteAttributeString("TrueAnomaly", Location.TrueAnomaly.ToString());

            //  writer.WriteEndElement();
        }

        #endregion
    }

    public class SizeShape
    {
        public double SemimajorAxis
        {
            get
            {
                return semimajorAxis;
            }
            set
            {
                semimajorAxis = value;
            }
        }
        public double Eccentricity
        {
            get
            {
                return eccentricity;
            }
            set
            {
                eccentricity = value;
            }
        }
        public double ApogeeRadius
        {
            get
            {
                return semimajorAxis * (1.0 - eccentricity);
            }
            set
            {
                double apogeeRadius = value;
                double perigeeRadius = PerigeeRadius;
                semimajorAxis = (apogeeRadius + perigeeRadius) / 2.0;
                eccentricity = (perigeeRadius - apogeeRadius) / (perigeeRadius + apogeeRadius);
            }
        }
        public double PerigeeRadius
        {
            get
            {
                return semimajorAxis * (1.0 + eccentricity);
            }
            set
            {
                double apogeeRadius = ApogeeRadius;
                double perigeeRadius = value;
                semimajorAxis = (apogeeRadius + perigeeRadius) / 2.0;
                eccentricity = (perigeeRadius - apogeeRadius) / (perigeeRadius + apogeeRadius);
            }
        }
        public double ApogeeAltitude
        {
            get
            {
                return semimajorAxis * (1.0 - eccentricity) - Globals.Re;
            }
            set
            {
                double apogeeRadius = value + Globals.Re;
                double perigeeRadius = PerigeeRadius;
                semimajorAxis = (apogeeRadius + perigeeRadius) / 2.0;
                eccentricity = (perigeeRadius - apogeeRadius) / (perigeeRadius + apogeeRadius);
            }
        }
        public double PerigeeAltitude
        {
            get
            {
                return semimajorAxis * (1.0 + eccentricity) - Globals.Re;
            }
            set
            {
                double apogeeRadius = ApogeeRadius;
                double perigeeRadius = value + Globals.Re;
                semimajorAxis = (apogeeRadius + perigeeRadius) / 2.0;
                eccentricity = (perigeeRadius - apogeeRadius) / (perigeeRadius + apogeeRadius);
            }
        }
        public double Period
        {
            get
            {
                return 2.0 * Math.PI * Math.Sqrt(Math.Pow(semimajorAxis, 3) / Globals.GM);
            }
            set
            {
                double period = value;
                semimajorAxis = Math.Pow(Math.Pow(Period / (2.0 * Math.PI), 2) * Globals.GM, 1.0 / 3.0);
            }
        }
        public double MeanMotion
        {
            get
            {
                return 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(semimajorAxis, 3) / Globals.GM));
            }
            set
            {
                double meanMotion = value;
                semimajorAxis = Math.Pow(Math.Pow((86400.0 / meanMotion) / (2.0 * Math.PI), 2) * Globals.GM, 1.0 / 3.0);
            }
        }

        private double semimajorAxis;
        private double eccentricity;
    }

    public class Orientation
    {
        public Orientation(OrbitState orbitState)
        {
            this.orbitState = orbitState;
        }

        public double Inclination { get; set; }

        public double RAAN
        {
            get
            {
                return raan;
            }
            set
            {
                raan = value;
            }
        }

        public double LonAscnNode
        {
            get
            {
                double S = orbitState.SiderealTime();
                double tAN = orbitState.Location.TimePastAN;
                return raan - (tAN * Globals.Omega + S) * MyMath.RadiansToDegrees;
            }
            set
            {
                double S = orbitState.SiderealTime();
                double tAN = orbitState.Location.TimePastAN;
                raan = (tAN * Globals.Omega + S) * MyMath.RadiansToDegrees + value;
            }
        }

        public double ArgumentOfPerigee { get; set; }

        private double raan;
        private readonly OrbitState orbitState;
    }

    //public enum OrientationAscNodeType
    //{
    //    AscNodeLAN = 0,
    //    AscNodeRAAN = 1
    //}

    //public class OrientationAscNode
    //{
    //    public OrientationAscNode(OrbitState orbitState)
    //    {
    //        this.OrbitState = orbitState;
    //    }

    //    public double As(OrientationAscNodeType type)
    //    {
    //        if (this.Type == type)
    //            return Value;

    //        double S = OrbitState.SiderealTime();
    //        double tAN = OrbitState.Location.TimePastAN;

    //        switch (type)
    //        {
    //            case OrientationAscNodeType.AscNodeLAN:
    //                return Value - (tAN * MyConst.w3 + S) * MyMath.RadiansToDegrees;
    //            case OrientationAscNodeType.AscNodeRAAN:
    //                return (tAN * MyConst.w3 + S) * MyMath.RadiansToDegrees + Value;
    //            default:
    //                return Double.NaN;
    //        }

    //    }

    //    public double Value { get; protected set; }

    //    private OrientationAscNodeType Type;
    //    private OrbitState OrbitState;
    //}



    //public enum LocationType
    //{
    //    TrueAnomaly = 0,
    //    MeanAnomaly = 1,
    //    EccentricAnomaly = 2,
    //    ArgumentOfLatitude = 3,
    //    TimePastAN = 4,
    //    TimePastPerigee = 5
    //}

    //public class Location
    //{
    //    public Location(OrbitState orbitState)
    //    {
    //        this.OrbitState = orbitState;
    //    }

    //    public double As(LocationType type)
    //    {
    //        if (type == LocationType.TrueAnomaly)
    //            return TrueAnomaly;

    //        if(type == LocationType.ArgumentOfLatitude)
    //        {
    //            double ArgumentOfLatitude = TrueAnomaly + OrbitState.ArgOfPerigee;
    //            return MyMath.WrapAngle360(ArgumentOfLatitude);
    //        }

    //        double EccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - OrbitState.Eccentricity) / (1.0 + OrbitState.Eccentricity)) * Math.Tan(0.5 * TrueAnomaly * MyMath.DegreesToRadians));
    //        EccentricAnomaly = MyMath.WrapAngle(EccentricAnomaly);

    //        if(type == LocationType.EccentricAnomaly)
    //            return EccentricAnomaly * MyMath.RadiansToDegrees;

    //        double MeanAnomaly = EccentricAnomaly - OrbitState.Eccentricity * Math.Sin(EccentricAnomaly);
    //        MeanAnomaly = MyMath.WrapAngle(MeanAnomaly);

    //        if (type == LocationType.MeanAnomaly)
    //            return MeanAnomaly * MyMath.RadiansToDegrees;

    //        double n = Math.Sqrt(MyConst.MU) * Math.Pow(OrbitState.SemimajorAxis, -3.0 / 2.0);
    //        double TimePastPerigee = MeanAnomaly / n;

    //        if(type == LocationType.TimePastPerigee)
    //            return TimePastPerigee;

    //        double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - OrbitState.Eccentricity) / (1.0 + OrbitState.Eccentricity)) * Math.Sin(0.5 * OrbitState.ArgOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * OrbitState.ArgOfPerigee * MyMath.DegreesToRadians));
    //        e1 = MyMath.WrapAngle(e1);
    //        double e2 = e1 - OrbitState.Eccentricity * Math.Sin(e1);

    //        return TimePastPerigee + e2 / n;
    //    }

    //    public double TrueAnomaly { get; protected set; }

    //    private OrbitState OrbitState;
    //}

    public class Location
    {
        public Location(OrbitState orbitState)
        {
            this.orbitState = orbitState;
        }

        public double TrueAnomaly
        {
            get
            {
                return trueAnomaly;
            }
            set
            {
                trueAnomaly = value;
            }
        }
        public double MeanAnomaly
        {
            get
            {
                double meanAnomaly = EccentricAnomaly - (orbitState.SizeShape.Eccentricity * Math.Sin(EccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                return MyMath.WrapAngle360(meanAnomaly);
            }
            set
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double M = value * MyMath.DegreesToRadians;
                double e1 = M;
                double e2 = M + ecc * Math.Sin(e1);
                while (Math.Abs(e1 - e2) > 0.000001)
                {
                    e1 = e2;
                    e2 = M + ecc * Math.Sin(e1);
                }
                double E = e2;
                trueAnomaly = Math.Atan2(Math.Sin(E) * Math.Sqrt(1 - ecc * ecc), Math.Cos(E) - ecc);
                trueAnomaly *= MyMath.RadiansToDegrees;
            }
        }
        public double EccentricAnomaly
        {
            get
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - ecc) / (1.0 + ecc)) * Math.Tan(0.5 * TrueAnomaly * MyMath.DegreesToRadians));
                eccentricAnomaly *= MyMath.RadiansToDegrees;
                return MyMath.WrapAngle360(eccentricAnomaly);
            }
            set
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double E = value * MyMath.DegreesToRadians;
                trueAnomaly = Math.Atan2(Math.Sin(E) * Math.Sqrt(1 - ecc * ecc), Math.Cos(E) - ecc);
                trueAnomaly *= MyMath.RadiansToDegrees;
            }
        }
        public double ArgumentOfLatitude
        {
            get
            {
                double argumentOfLatitude = trueAnomaly + orbitState.Orientation.ArgumentOfPerigee;
                return MyMath.WrapAngle360(argumentOfLatitude);
            }
            set
            {
                trueAnomaly = value - orbitState.Orientation.ArgumentOfPerigee;
                trueAnomaly = MyMath.WrapAngle360(trueAnomaly);
            }
        }
        public double TimePastAN
        {
            get
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SizeShape.SemimajorAxis, -3.0 / 2.0);
                double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - ecc) / (1.0 + ecc)) * Math.Sin(0.5 * orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians));
                e1 = MyMath.WrapAngle(e1);
                double e2 = e1 - ecc * Math.Sin(e1);
                return TimePastPerigee + e2 / n;
            }
            set
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SizeShape.SemimajorAxis, -3.0 / 2.0);
                double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - ecc) / (1.0 + ecc)) * Math.Sin(orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians / 2.0), Math.Cos(orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians / 2.0));
                e1 = MyMath.WrapAngle(e1);
                double e2 = e1 - ecc * Math.Sin(e1);

                double M = (value - e2 / n) * n;
                e1 = M;
                e2 = M + ecc * Math.Sin(e1);
                while (Math.Abs(e1 - e2) > 0.000001)
                {
                    e1 = e2;
                    e2 = M + ecc * Math.Sin(e1);
                }

                trueAnomaly = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - ecc * ecc), Math.Cos(e2) - ecc);
                trueAnomaly *= MyMath.RadiansToDegrees;
                return;
            }
        }
        public double TimePastPerigee
        {
            get
            {
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SizeShape.SemimajorAxis, -3.0 / 2.0);
                return MeanAnomaly / n;
            }
            set
            {
                double ecc = orbitState.SizeShape.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SizeShape.SemimajorAxis, -3.0 / 2.0);
                double M = value * n;
                double e1 = M;
                double e2 = M + ecc * Math.Sin(e1);
                while (Math.Abs(e1 - e2) > 0.000001)
                {
                    e1 = e2;
                    e2 = M + ecc * Math.Sin(e1);
                }
                TrueAnomaly = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - ecc * ecc), Math.Cos(e2) - ecc);
                TrueAnomaly *= MyMath.RadiansToDegrees;
            }
        }

        private double trueAnomaly;

        private readonly OrbitState orbitState;
    }

}

//public interface IOrbitStateCoordinateSystem
//    {
//        string Epoch { get; set; }
//        ECoordinateSystem Type { get; }
//    }

//    #region SizeShape


//    public interface IClassicalSizeShape
//    {

//    }

//    public interface IClassicalSizeShapeRadius : IClassicalSizeShape
//    {
//        double ApogeeRadius { get; set; }
//        double PerigeeRadius { get; set; }
//        void SetSizeShapeRadius(double ApogeeRadius, double PerigeeRadius);
//    }

//    public interface IClassicalSizeShapeSemimajorAxis : IClassicalSizeShape
//    {
//        double Eccentricity { get; set; }
//        double SemiMajorAxis { get; set; }
//    }

//    public interface IClassicalSizeShapeMeanMotion : IClassicalSizeShape
//    {
//        double Eccentricity { get; set; }
//        double MeanMotion { get; set; }
//    }

//    public interface IClassicalSizeShapeAltitude : IClassicalSizeShape
//    {
//        double ApogeeAltitude { get; set; }
//        double PerigeeAltitude { get; set; }
//    }

//    public interface IClassicalSizeShapePeriod : IClassicalSizeShape
//    {
//        double Eccentricity { get; set; }
//        double Period { get; set; }
//    }

//    #endregion

//    #region Orientation

//    public interface IOrientationAscNode
//    {
//    }

//    public interface IOrientationAscNodeRAAN : IOrientationAscNode
//    {
//        double Value { get; set; }
//    }

//    public interface IOrientationAscNodeLAN : IOrientationAscNode
//    {
//        double Value { get; set; }
//    }

//    public interface IClassicalOrientation
//    {
//        double ArgOfPerigee { get; set; }
//        IOrientationAscNode AscNode { get; }
//        EOrientationAscNode AscNodeType { get; set; }
//        double Inclination { get; set; }
//    }

//    #endregion

//    #region Location

//    public interface IClassicalLocation
//    {

//    }

//    public interface IClassicalLocationMeanAnomaly : IClassicalLocation { double Value { get; set; } }
//    public interface IClassicalLocationTimePastPerigee : IClassicalLocation { double Value { get; set; } }
//    public interface IClassicalLocationArgumentOfLatitude : IClassicalLocation { double Value { get; set; } }
//    public interface IClassicalLocationEccentricAnomaly : IClassicalLocation { double Value { get; set; } }
//    public interface IClassicalLocationTrueAnomaly : IClassicalLocation { double Value { get; set; } }
//    public interface IClassicalLocationTimePastAN : IClassicalLocation { double Value { get; set; } }

//    #endregion


//    public interface IOrbitState
//    {
//        IOrbitStateCoordinateSystem CoordinateSystem { get; }
//        ECoordinateSystem CoordinateSystemType { get; set; }

//        IClassicalSizeShape SizeShape { get; }
//        EClassicalSizeShape SizeShapeType { get; set; }

//        IClassicalOrientation Orientation { get; }

//        EClassicalLocation LocationType { get; set; }
//        IClassicalLocation Location { get; }

//        Array SupportedCoordinateSystemTypes { get; }
//    }

//    public enum ECoordinateSystem
//    {
//        eCoordinateSystemUnknown = -1,
//        eCoordinateSystemAlignmentAtEpoch = 0,
//        eCoordinateSystemB1950 = 1,
//        eCoordinateSystemFixed = 2,
//        eCoordinateSystemJ2000 = 3,
//        eCoordinateSystemMeanOfDate = 4,
//        eCoordinateSystemMeanOfEpoch = 5,
//        eCoordinateSystemTEMEOfDate = 6,
//        eCoordinateSystemTEMEOfEpoch = 7,
//        eCoordinateSystemTrueOfDate = 8,
//        eCoordinateSystemTrueOfEpoch = 9,
//        eCoordinateSystemTrueOfRefDate = 10
//    }

//    public enum EClassicalSizeShape
//    {
//        eSizeShapeUnknown = -1,
//        eSizeShapeAltitude = 0,
//        eSizeShapeMeanMotion = 1,
//        eSizeShapePeriod = 2,
//        eSizeShapeRadius = 3,
//        eSizeShapeSemimajorAxis = 4
//    }

//    public enum EOrientationAscNode
//    {
//        eAscNodeUnknown = -1,
//        eAscNodeLAN = 0,
//        eAscNodeRAAN = 1
//    }

//    public enum EClassicalLocation
//    {
//        eLocationUnknown = -1,
//        eLocationArgumentOfLatitude = 0,
//        eLocationEccentricAnomaly = 1,
//        eLocationMeanAnomaly = 2,
//        eLocationTimePastAN = 3,
//        eLocationTimePastPerigee = 4,
//        eLocationTrueAnomaly = 5
//    }

//    public enum EOrbitStateType
//    {
//        eOrbitStateCartesian = 0,
//        eOrbitStateClassical = 1,
//        eOrbitStateEquinoctial = 2,
//        eOrbitStateDelaunay = 3,
//        eOrbitStateSpherical = 4,
//        eOrbitStateMixedSpherical = 5,
//        eOrbitStateGeodetic = 6
//    }


