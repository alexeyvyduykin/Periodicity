using System;

namespace Periodicity.Core
{
    // all angles in Degree
    public class Orbit : OrbitState
    {
        public Orbit() : base()
        {
            
        }

        public double Anomalia(double tNorm, double tPastAN = 0.0)
        {
            double M, v, e1, e2;
            var nTemp = Math.Sqrt(Globals.GM / SemimajorAxis) / SemimajorAxis;

            M = nTemp * (tNorm + tPastAN);

            e1 = M;
            e2 = M + Eccentricity * Math.Sin(e1);
            while (Math.Abs(e1 - e2) > 0.001)
            {
                e1 = e2;
                e2 = M + Eccentricity * Math.Sin(e1);
            }
            v = Math.Atan2(Math.Sin(e2) * Math.Sqrt(1 - Eccentricity * Eccentricity), Math.Cos(e2) - Eccentricity);
            v = MyMath.WrapAngle(v);
            return v;
        }

        public double Radius(double tnorm)
        {
            if (Eccentricity == 0)
            {
                return SemimajorAxis;
            }

            var pTemp = SemimajorAxis * (1 - Eccentricity * Eccentricity);

            double u = Anomalia(tnorm) + ArgumentOfPerigee;
            return pTemp / (1 + Eccentricity * Math.Cos(u));
        }

        public double Semiaxis(double u)
        {
            var pTemp = SemimajorAxis * (1 - Eccentricity * Eccentricity);
            return pTemp / (1 + Eccentricity * Math.Cos(u));
        }

        public double TimeHalfPi()
        {
            double r = Math.PI / 4.0;
            double e1 = 2.0 * Math.Atan2(Math.Sqrt((1.0 - Eccentricity) / (1.0 + Eccentricity)) * Math.Sin(r), Math.Cos(r));
            if (e1 < 0)
            {
                e1 += 2.0 * Math.PI;
            }

            double e2 = e1 - Eccentricity * Math.Sin(e1);
            var nTemp = Math.Sqrt(Globals.GM / SemimajorAxis) / SemimajorAxis;

            return e2 / nTemp;
        }

        public double InclinationNormal()
        {
            if (Inclination >= 0.0 && Inclination <= 90.0)
            {
                return Inclination;
            }

            return 180.0 - Inclination;
        }

        public double Quart0 { get { return 0.0; } }
        public double Quart1 { get { return TimeHalfPi(); } }
        public double Quart2 { get { return Period / 2.0; } }
        public double Quart3 { get { return Period - TimeHalfPi(); } }
        public double Quart4 { get { return Period; } }

        public double[] Quarts { get { return new double[5] { 0.0, TimeHalfPi(), Period / 2.0, Period - TimeHalfPi(), Period }; } }
    }
}
