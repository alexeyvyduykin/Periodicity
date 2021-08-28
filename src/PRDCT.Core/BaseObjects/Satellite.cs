using System;

namespace Periodicity.Core
{
    public class OrbitState
    {
        // SizeShape
        private double _semimajorAxis;
        private double _eccentricity;
        private double _apogeeRadius;
        private double _perigeeRadius;
        private double _apogeeAltitude;
        private double _perigeeAltitude;
        private double _period;
        private double _meanMotion;

        public OrbitState()       
        {
            SemimajorAxis = 6955.14;
            Eccentricity = 0.0;
            

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

        private void SynchronizeShapeProperties(double value, string name)
        {
            switch (name)
            {
                case nameof(OrbitState.SemimajorAxis):

                    _semimajorAxis = value;

                    if(_eccentricity == default)
                    {
                        break;
                    }

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));
                  
                    break;

                case nameof(OrbitState.Eccentricity):

                    _eccentricity = value;

                    if (_semimajorAxis == default)
                    {
                        break;
                    }

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;

                case nameof(OrbitState.ApogeeRadius):

                    _apogeeRadius = value;
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);

                    _semimajorAxis = (_apogeeRadius + _perigeeRadius) / 2.0;
                    _eccentricity = (_perigeeRadius - _apogeeRadius) / (_perigeeRadius + _apogeeRadius);
               
                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;


                case nameof(OrbitState.PerigeeRadius):

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = value;

                    _semimajorAxis = (_apogeeRadius + _perigeeRadius) / 2.0;
                    _eccentricity = (_perigeeRadius - _apogeeRadius) / (_perigeeRadius + _apogeeRadius);

                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;


                case nameof(OrbitState.ApogeeAltitude):

                    _apogeeRadius = value + Globals.Re;
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);

                    _semimajorAxis = (_apogeeRadius + _perigeeRadius) / 2.0;
                    _eccentricity = (_perigeeRadius - _apogeeRadius) / (_perigeeRadius + _apogeeRadius);

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;


                case nameof(OrbitState.PerigeeAltitude):

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);                    
                    _perigeeRadius = value + Globals.Re;

                    _semimajorAxis = (_apogeeRadius + _perigeeRadius) / 2.0;
                    _eccentricity = (_perigeeRadius - _apogeeRadius) / (_perigeeRadius + _apogeeRadius);

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;


                case nameof(OrbitState.Period):

                    _period = value;
                                                         
                    _semimajorAxis = Math.Pow(Math.Pow(_period / (2.0 * Math.PI), 2) * Globals.GM, 1.0 / 3.0);

                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;            
                    _meanMotion = 86400.0 / (2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM));

                    break;


                case nameof(OrbitState.MeanMotion):

                    _meanMotion = value;

                    _semimajorAxis = Math.Pow(Math.Pow((86400.0 / _meanMotion) / (2.0 * Math.PI), 2) * Globals.GM, 1.0 / 3.0);
                   
                    _apogeeRadius = _semimajorAxis * (1.0 - _eccentricity);
                    _perigeeRadius = _semimajorAxis * (1.0 + _eccentricity);
                    _apogeeAltitude = _semimajorAxis * (1.0 - _eccentricity) - Globals.Re;
                    _perigeeAltitude = _semimajorAxis * (1.0 + _eccentricity) - Globals.Re;
                    _period = 2.0 * Math.PI * Math.Sqrt(Math.Pow(_semimajorAxis, 3) / Globals.GM);               

                    break;

                default:
                    break;
            }
        }

        public double SemimajorAxis
        {
            get => _semimajorAxis;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.SemimajorAxis));
        }

        public double Eccentricity
        {
            get => _eccentricity;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.Eccentricity));
        }

        public double ApogeeRadius
        {
            get => _apogeeRadius;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.ApogeeRadius));
        }

        public double PerigeeRadius
        {
            get => _perigeeRadius;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.PerigeeRadius));
        }
       
        public double ApogeeAltitude
        {
            get => _apogeeAltitude;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.ApogeeAltitude));
        }
      
        public double PerigeeAltitude
        {
            get => _perigeeAltitude;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.PerigeeAltitude));
        }
    
        public double Period
        {
            get => _period;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.Period));
        }
       
        public double MeanMotion
        {
            get => _meanMotion;
            set => SynchronizeShapeProperties(value, nameof(OrbitState.MeanMotion));
        }
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
                double meanAnomaly = EccentricAnomaly - (orbitState.Eccentricity * Math.Sin(EccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                return MyMath.WrapAngle360(meanAnomaly);
            }
            set
            {
                double ecc = orbitState.Eccentricity;
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
                double ecc = orbitState.Eccentricity;
                double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - ecc) / (1.0 + ecc)) * Math.Tan(0.5 * TrueAnomaly * MyMath.DegreesToRadians));
                eccentricAnomaly *= MyMath.RadiansToDegrees;
                return MyMath.WrapAngle360(eccentricAnomaly);
            }
            set
            {
                double ecc = orbitState.Eccentricity;
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
                double ecc = orbitState.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SemimajorAxis, -3.0 / 2.0);
                double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - ecc) / (1.0 + ecc)) * Math.Sin(0.5 * orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * orbitState.Orientation.ArgumentOfPerigee * MyMath.DegreesToRadians));
                e1 = MyMath.WrapAngle(e1);
                double e2 = e1 - ecc * Math.Sin(e1);
                return TimePastPerigee + e2 / n;
            }
            set
            {
                double ecc = orbitState.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SemimajorAxis, -3.0 / 2.0);
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
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SemimajorAxis, -3.0 / 2.0);
                return MeanAnomaly / n;
            }
            set
            {
                double ecc = orbitState.Eccentricity;
                double n = Math.Sqrt(Globals.GM) * Math.Pow(orbitState.SemimajorAxis, -3.0 / 2.0);
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


