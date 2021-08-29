namespace Periodicity.Core
{
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
    }
}
