using System;

namespace Periodicity.Core
{
    public class OrbitState
    {
        private DateTime _orbitEpoch;
        private double _siderealTime;

        // SizeShape
        private double _semimajorAxis;
        private double _eccentricity;
        private double _apogeeRadius;
        private double _perigeeRadius;
        private double _apogeeAltitude;
        private double _perigeeAltitude;
        private double _period;
        private double _meanMotion;

        // Location
        private double _trueAnomaly;
        private double _meanAnomaly;
        private double _eccentricAnomaly;
        private double _argumentOfLatitude;
        private double _timePastAN;
        private double _timePastPerigee;

        // Orientation
        private double _inclination;
        private double _raan;
        private double _lonAscnNode;
        private double _argumentOfPerigee;

        public OrbitState()
        {
            //   22/06/2015 00:00:00
            OrbitEpoch = new DateTime(2015, 6, 22, 0, 0, 0);

            SemimajorAxis = 6955.14;
            Eccentricity = 0.0;

            Inclination = 97.65;
            RAAN = 269.663;
            ArgumentOfPerigee = 0.0;

            TrueAnomaly = 0.0;
        }

        private void SynchronizeProperties(object value, string name)
        {
            switch (name)
            {
                case nameof(OrbitState.OrbitEpoch):

                    _orbitEpoch = (DateTime)value;

                    //    double JD = OrbitEpoch.Date.ToOADate() + 2415018.5;
                    //    double S0 = MyFunction.uds1900(JD);
                    //    double S = S0 + Globals.Omega * OrbitEpoch.TimeOfDay.TotalSeconds;
                    Julian jd = new Julian(OrbitEpoch);
                    _siderealTime = jd.ToGmst();

                    SynchronizeOrientationProperties(RAAN, nameof(OrbitState.RAAN));

                    break;
                default:
                    break;
            }
        }

        public DateTime OrbitEpoch
        {
            get => _orbitEpoch;
            set => SynchronizeProperties(value, nameof(OrbitState.OrbitEpoch));
        }

        public double SiderealTime => _siderealTime;

        private void SynchronizeShapeProperties(double value, string name)
        {
            switch (name)
            {
                case nameof(OrbitState.SemimajorAxis):

                    _semimajorAxis = value;

                    if (_eccentricity == default)
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

        private void SynchronizeLocationProperties(double value, string name)
        {
            switch (name)
            {
                case nameof(OrbitState.TrueAnomaly):
                    {
                        _trueAnomaly = value;

                        double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Tan(0.5 * _trueAnomaly * MyMath.DegreesToRadians));
                        _eccentricAnomaly = MyMath.WrapAngle360(eccentricAnomaly * MyMath.RadiansToDegrees);

                        double meanAnomaly = _eccentricAnomaly - (Eccentricity * Math.Sin(_eccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                        _meanAnomaly = MyMath.WrapAngle360(meanAnomaly);

                        double argumentOfLatitude = _trueAnomaly + ArgumentOfPerigee;
                        _argumentOfLatitude = MyMath.WrapAngle360(argumentOfLatitude);

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians));
                        e1 = MyMath.WrapAngle(e1);
                        double e2 = e1 - Eccentricity * Math.Sin(e1);

                        _timePastPerigee = _meanAnomaly / n;
                        _timePastAN = _timePastPerigee + e2 / n;
                    }
                    break;

                case nameof(OrbitState.MeanAnomaly):
                    {
                        _meanAnomaly = value;

                        double M = _meanAnomaly * MyMath.DegreesToRadians;
                        double e1 = M;
                        double e2 = M + Eccentricity * Math.Sin(e1);
                        while (Math.Abs(e1 - e2) > 0.000001)
                        {
                            e1 = e2;
                            e2 = M + Eccentricity * Math.Sin(e1);
                        }
                        double E = e2;

                        _trueAnomaly = Math.Atan2(Math.Sin(E) * Math.Sqrt(1 - Eccentricity * Eccentricity), Math.Cos(E) - Eccentricity) * MyMath.RadiansToDegrees;

                        double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Tan(0.5 * _trueAnomaly * MyMath.DegreesToRadians));
                        _eccentricAnomaly = MyMath.WrapAngle360(eccentricAnomaly * MyMath.RadiansToDegrees);

                        double argumentOfLatitude = _trueAnomaly + ArgumentOfPerigee;
                        _argumentOfLatitude = MyMath.WrapAngle360(argumentOfLatitude);

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double e11 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians));
                        e11 = MyMath.WrapAngle(e11);
                        double e22 = e11 - Eccentricity * Math.Sin(e11);

                        _timePastPerigee = _meanAnomaly / n;
                        _timePastAN = _timePastPerigee + e22 / n;
                    }
                    break;

                case nameof(OrbitState.EccentricAnomaly):
                    {
                        _eccentricAnomaly = value;

                        double E = _eccentricAnomaly * MyMath.DegreesToRadians;
                        _trueAnomaly = Math.Atan2(Math.Sin(E) * Math.Sqrt(1 - Eccentricity * Eccentricity), Math.Cos(E) - Eccentricity) * MyMath.RadiansToDegrees;

                        double meanAnomaly = _eccentricAnomaly - (Eccentricity * Math.Sin(_eccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                        _meanAnomaly = MyMath.WrapAngle360(meanAnomaly);

                        double argumentOfLatitude = _trueAnomaly + ArgumentOfPerigee;
                        _argumentOfLatitude = MyMath.WrapAngle360(argumentOfLatitude);

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians));
                        e1 = MyMath.WrapAngle(e1);
                        double e2 = e1 - Eccentricity * Math.Sin(e1);

                        _timePastPerigee = _meanAnomaly / n;
                        _timePastAN = _timePastPerigee + e2 / n;
                    }
                    break;

                case nameof(OrbitState.ArgumentOfLatitude):
                    {
                        _argumentOfLatitude = value;

                        var trueAnomaly = value - ArgumentOfPerigee;
                        _trueAnomaly = MyMath.WrapAngle360(trueAnomaly);

                        double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Tan(0.5 * _trueAnomaly * MyMath.DegreesToRadians));
                        _eccentricAnomaly = MyMath.WrapAngle360(eccentricAnomaly * MyMath.RadiansToDegrees);

                        double meanAnomaly = _eccentricAnomaly - (Eccentricity * Math.Sin(_eccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                        _meanAnomaly = MyMath.WrapAngle360(meanAnomaly);

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians));
                        e1 = MyMath.WrapAngle(e1);
                        double e2 = e1 - Eccentricity * Math.Sin(e1);

                        _timePastPerigee = _meanAnomaly / n;
                        _timePastAN = _timePastPerigee + e2 / n;
                    }
                    break;

                case nameof(OrbitState.TimePastAN):
                    {
                        _timePastAN = value;

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(ArgumentOfPerigee * MyMath.DegreesToRadians / 2.0), Math.Cos(ArgumentOfPerigee * MyMath.DegreesToRadians / 2.0));
                        e1 = MyMath.WrapAngle(e1);
                        double e2 = e1 - Eccentricity * Math.Sin(e1);

                        double M = (_timePastAN - e2 / n) * n;
                        e1 = M;
                        e2 = M + Eccentricity * Math.Sin(e1);
                        while (Math.Abs(e1 - e2) > 0.000001)
                        {
                            e1 = e2;
                            e2 = M + Eccentricity * Math.Sin(e1);
                        }

                        _trueAnomaly = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - Eccentricity * Eccentricity), Math.Cos(e2) - Eccentricity) * MyMath.RadiansToDegrees;

                        double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Tan(0.5 * _trueAnomaly * MyMath.DegreesToRadians));
                        _eccentricAnomaly = MyMath.WrapAngle360(eccentricAnomaly * MyMath.RadiansToDegrees);

                        double meanAnomaly = _eccentricAnomaly - (Eccentricity * Math.Sin(_eccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                        _meanAnomaly = MyMath.WrapAngle360(meanAnomaly);

                        double argumentOfLatitude = _trueAnomaly + ArgumentOfPerigee;
                        _argumentOfLatitude = MyMath.WrapAngle360(argumentOfLatitude);

                        _timePastPerigee = _meanAnomaly / n;
                    }

                    break;

                case nameof(OrbitState.TimePastPerigee):
                    {
                        _timePastPerigee = value;

                        double n = Math.Sqrt(Globals.GM) * Math.Pow(SemimajorAxis, -3.0 / 2.0);
                        double M = _timePastPerigee * n;
                        double e1 = M;
                        double e2 = M + Eccentricity * Math.Sin(e1);
                        while (Math.Abs(e1 - e2) > 0.000001)
                        {
                            e1 = e2;
                            e2 = M + Eccentricity * Math.Sin(e1);
                        }
                        _trueAnomaly = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - Eccentricity * Eccentricity), Math.Cos(e2) - Eccentricity) * MyMath.RadiansToDegrees;

                        double eccentricAnomaly = 2.0 * Math.Atan(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Tan(0.5 * _trueAnomaly * MyMath.DegreesToRadians));
                        _eccentricAnomaly = MyMath.WrapAngle360(eccentricAnomaly * MyMath.RadiansToDegrees);

                        double meanAnomaly = _eccentricAnomaly - (Eccentricity * Math.Sin(_eccentricAnomaly * MyMath.DegreesToRadians) * MyMath.RadiansToDegrees);
                        _meanAnomaly = MyMath.WrapAngle360(meanAnomaly);

                        double argumentOfLatitude = _trueAnomaly + ArgumentOfPerigee;
                        _argumentOfLatitude = MyMath.WrapAngle360(argumentOfLatitude);

                        double e11 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians), Math.Cos(0.5 * ArgumentOfPerigee * MyMath.DegreesToRadians));
                        e11 = MyMath.WrapAngle(e11);
                        double e22 = e11 - Eccentricity * Math.Sin(e11);

                        _timePastAN = _timePastPerigee + e22 / n;
                    }
                    break;

                default:
                    break;
            }
        }

        public double TrueAnomaly
        {
            get => _trueAnomaly;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.TrueAnomaly));
        }

        public double MeanAnomaly
        {
            get => _meanAnomaly;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.MeanAnomaly));
        }

        public double EccentricAnomaly
        {
            get => _eccentricAnomaly;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.EccentricAnomaly));
        }

        public double ArgumentOfLatitude
        {
            get => _argumentOfLatitude;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.ArgumentOfLatitude));
        }

        public double TimePastAN
        {
            get => _timePastAN;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.TimePastAN));
        }

        public double TimePastPerigee
        {
            get => _timePastPerigee;
            set => SynchronizeLocationProperties(value, nameof(OrbitState.TimePastPerigee));
        }

        private void SynchronizeOrientationProperties(double value, string name)
        {
            if (value == default)
            {
                return;
            }

            switch (name)
            {
                case nameof(OrbitState.Inclination):

                    _inclination = value;

                    break;

                case nameof(OrbitState.RAAN):
                    {
                        _raan = value;

                        double S = SiderealTime;
                        double tAN = TimePastAN;
                        _lonAscnNode = _raan - (tAN * Globals.Omega + S) * MyMath.RadiansToDegrees;
                    }

                    break;

                case nameof(OrbitState.LonAscnNode):
                    {
                        _lonAscnNode = value;

                        double S = SiderealTime;
                        double tAN = TimePastAN;
                        _raan = (tAN * Globals.Omega + S) * MyMath.RadiansToDegrees + _lonAscnNode;
                    }

                    break;

                case nameof(OrbitState.ArgumentOfPerigee):

                    _argumentOfPerigee = value;

                    SynchronizeLocationProperties(TrueAnomaly, nameof(OrbitState.TrueAnomaly));

                    break;

                default:
                    break;
            }
        }

        public double Inclination
        {
            get => _inclination;
            set => SynchronizeOrientationProperties(value, nameof(OrbitState.Inclination));
        }

        public double RAAN
        {
            get => _raan;
            set => SynchronizeOrientationProperties(value, nameof(OrbitState.RAAN));
        }

        public double LonAscnNode
        {
            get => _lonAscnNode;
            set => SynchronizeOrientationProperties(value, nameof(OrbitState.LonAscnNode));
        }

        public double ArgumentOfPerigee
        {
            get => _argumentOfPerigee;
            set => SynchronizeOrientationProperties(value, nameof(OrbitState.ArgumentOfPerigee));
        }
    }
}


