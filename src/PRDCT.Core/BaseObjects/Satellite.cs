using System;

namespace Periodicity.Core
{
    public class OrbitState 
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


