namespace Periodicity.Core
{

    //class SimpleRange
    //{
    //    public double Left { get; set; }
    //    public double Right { get; set; }
    //}



    //public abstract class PRDCTPoint : IComparable<PRDCTPoint>
    //{
    //    public PRDCTPoint(double lon)
    //    {
    //        this.Longitude = lon;
    //    }

    //    public int CompareTo(PRDCTPoint obj)
    //    {
    //        return Longitude.CompareTo(obj.Longitude);
    //    }

    //    public abstract void Slicing<T>(double lon, T other);

    //   // public double Time { get; private set; }
    //    public double Longitude { get; protected set; }
    //}

    //public abstract class PRDCTPointValue<T> : PRDCTPoint
    //{
    //    public PRDCTPointValue(double lon, T value) : base(lon)
    //    {
    //        this.Value = value;
    //    }

    //    public abstract void Slicing(double lon, PRDCTPointValue<T> other);

    //    public T Value { get; protected set; }
    //}


    //public class PRDCTPointTime : PRDCTPointValue<double>
    //{
    //    public PRDCTPointTime(double lon, double t) : base(lon, t) { }

    //    public override void Slicing(double XXX, PRDCTPointValue<double> other)
    //    {            
    //        Time = ((right - XXX) * tLeft + (XXX - left) * tRight) / (right - left);
    //        Longitude = XXX;
    //    }

    //    public double Time
    //    {
    //        get
    //        {
    //            return base.Value;
    //        }
    //        protected set
    //        {
    //            base.Value = value;
    //        }
    //    }
    //}


    public class PRDCTInterval
    {
        public PRDCTInterval(double left, double right, double t1, double t2)
        {
            Left = left;
            Right = right;
            TimeBegin = t1;
            TimeEnd = t2;
        }

        public void SwapTest()
        {
            if (Left > Right)
            {
                double temp = Right;
                Right = Left;
                Left = temp;

                temp = TimeEnd;
                TimeEnd = TimeBegin;
                TimeBegin = temp;
            }
        }

        //public double Left
        //{
        //    get
        //    {
        //        return Begin.Longitude;
        //    }
        //}

        //public double Right
        //{
        //    get
        //    {
        //        return End.Longitude;
        //    }
        //}

        ////public double TimeBegin
        ////{
        ////    get
        ////    {
        ////        return Begin.Time;
        ////    }
        ////}

        ////public double TimeEnd
        ////{
        ////    get
        ////    {
        ////        return End.Time;
        ////    }
        ////}

        public void Cut(double leftCutter, double rightCutter)
        {
            double leftTemp = Left;
            double rightTemp = Right;

            if (MyFunction.CutSegments(leftCutter, rightCutter, ref leftTemp, ref rightTemp) == true)
            {
                double left = Left, right = Right, t1 = TimeBegin, t2 = TimeEnd;

                if (leftTemp != Left)
                {
                    t1 = ((Right - leftTemp) * TimeBegin + (leftTemp - Left) * TimeEnd) / (Right - Left);
                    left = leftTemp;
                }
                if (rightTemp != Right)
                {
                    t2 = ((Right - rightTemp) * TimeBegin + (rightTemp - Left) * TimeEnd) / (Right - Left);
                    right = rightTemp;
                }

                Left = left;
                Right = right;
                TimeBegin = t1;
                TimeEnd = t2;
            }
        }

        public double Left { get; protected set; }
        public double Right { get; protected set; }
        public double TimeBegin { get; protected set; }
        public double TimeEnd { get; protected set; }

        //public PRDCTPoint Begin { get; private set; }
        //public PRDCTPoint End { get; private set; }
    }

    //public class PRDCTPoint : IComparable<PRDCTPoint>
    //{
    //    public PRDCTPoint(Geo2D point, double t)
    //    {
    //        if (point != null)
    //            Position = new Geo2D(point.Lon, point.Lat, point.Type);
    //        Time = t;
    //    }

    //    public int CompareTo(PRDCTPoint obj)
    //    {
    //        return Position.Lon.CompareTo(obj.Position.Lon);
    //    }

    //    public double Time { get; private set; }
    //    public Geo2D Position { get; private set; }
    //}

    //public enum PRDCTIntervalTypes
    //{
    //    None,
    //    WithoutBegining,
    //    WithoutEnding,
    //    Complete
    //}

    //public class PRDCTInterval
    //{
    //    public PRDCTInterval(PRDCTPoint p1, PRDCTPoint p2)
    //    {
    //        if (p1.Position != null && p2.Position != null)
    //        {
    //            this.Begin = p1;
    //            this.End = p2;
    //            this.Type = PRDCTIntervalTypes.Complete;
    //        }
    //        else if (p1.Position != null && p2.Position == null)
    //        {
    //            this.Begin = p1;
    //            this.End = p2;
    //            this.Type = PRDCTIntervalTypes.WithoutEnding;
    //        }
    //        else if (p1.Position == null && p2.Position != null)
    //        {
    //            this.Begin = p1;
    //            this.End = p2;
    //            this.Type = PRDCTIntervalTypes.WithoutBegining;
    //        }
    //        else
    //            this.Type = PRDCTIntervalTypes.None;
    //    }

    //    public void SwapTest()
    //    {
    //        if (Left > Right)
    //        {
    //            PRDCTPoint temp = End;
    //            End = Begin;
    //            Begin = temp;
    //        }
    //    }

    //    public double Left
    //    {
    //        get
    //        {
    //            return Begin.Position.Lon;
    //        }
    //    }
    //    public double Right
    //    {
    //        get
    //        {
    //            return End.Position.Lon;
    //        }
    //    }
    //    public double Cutter
    //    {
    //        get
    //        {
    //            return Begin.Position.Lat;
    //        }
    //    }

    //    public PRDCTPoint Begin { get; private set; }
    //    public PRDCTPoint End { get; private set; }
    //    public PRDCTIntervalTypes Type { get; private set; }

    //    //public int IndexSatellite { get; set; }
    //}

}
