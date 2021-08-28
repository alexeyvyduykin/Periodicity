using System;
using System.Collections.Generic;

namespace PRDCT.Core
{
    [Serializable]
    public struct NodeQuarter
    {
        public int Quart { get; set; }
        public double TimeBegin { get; set; }
        public double TimeEnd { get; set; }
    }

    [Serializable]
    public class Node
    {
        public int Value { get; set; }
        public List<NodeQuarter> Quarts { get; protected set; } = new List<NodeQuarter>();


        //public void SetCalculationTime(DateTime dateTimeBegin, DateTime dateTimeEnd)
        //{
        //    int nodeBegin = 0, nodeEnd;

        //    TMyDate timeBeginDate = new TMyDate(dateTimeBegin/*Convert.ToDateTime(strTimeBegin)*/);
        //    TMyDate timeEndDate = new TMyDate(dateTimeEnd/*Convert.ToDateTime(strTimeEnd)*/);

        //    TimeBegin = timeBeginDate.SecondOf0h();
        //    TimeDuration = (timeEndDate.Julian() - timeBeginDate.Julian()) * 86400.0;

        //    //double s0 = timeBeginDate.S0Apparent_RAD();
        //    //PRDCTSubSatellitePoint::lonAN = PRDCTSubSatellitePoint::om - s0;

        //    //        base.fixationToTime(dateTimeBegin);
        //    NearLine.FixationToTime(dateTimeBegin);
        //    FarLine.FixationToTime(dateTimeBegin);

        //    double tAN = base.Period * (1 - base.w / 2.0 * Math.PI);

        //    while ((tAN - base.Period) > 0.0)
        //        tAN -= base.Period;
        //    ///////////////////////////////////////////////

        //    double tTemp = tAN - base.Period; //t_ascend_null_node;
        //    while ((tTemp += base.Period) <= TimeBegin)
        //        nodeBegin++;

        //    nodeEnd = nodeBegin;
        //    tTemp = tAN - base.Period + nodeEnd * base.Period;
        //    while ((tTemp += base.Period) < TimeEnd)  // !!!! временное решение , раньше было просто t_end
        //        nodeEnd++;

        //    numNodes = nodeEnd - nodeBegin + 1;
        //    /////////////////////////////////////////////

        //    julianDate = timeBeginDate.JulianDate0h();
        //    timeAN = tAN - base.Period;
        //    timeANNull = tAN - base.Period;
        //}
        //public static List<PRDCTNode> Nodes(Orbit orbit)
        //{
        //    double tBeginCur, tQuartBegin, tQuartEnd, temp;
        //    int nodeCur, i, size, nodeBegin = 0, nodeEnd;

        //    double TRUE_TIME_AN = timeAN;

        //    if (TimeBegin > TimeEnd)
        //        return null;

        //    while ((timeAN - Period) > TimeBegin)
        //        timeAN -= Period;
        //    while (timeAN < TimeBegin)
        //        timeAN += Period;

        //    // нахождение первого витка, отчёт от 0-го
        //    // (из-за сложности реализации, но при выводе можно увеличить на 1 или использовать другой способ индексации)
        //    temp = timeAN - orbit.Period; //t_ascend_null_node;
        //    while ((temp += orbit.Period) <= TimeBegin)
        //        nodeBegin++;
        //    ////////////////////////////////////////////////////////////////////////////////
        //    // нахождение последннего витка
        //    nodeEnd = nodeBegin;
        //    temp = timeAN - orbit.Period + nodeEnd * orbit.Period;
        //    while ((temp += orbit.Period) < TimeEnd - PRDCT_EPS_TIME_NODE_RESERVE)  // !!!! временное решение , раньше было просто t_end
        //        nodeEnd++;
        //    ////////////////////////////////////////////////////////////////////////////////
        //    size = nodeEnd - nodeBegin + 1;
        //    var nodes = new List<PRDCTNode>();
        //    numNodes = size;

        //    timeAN = TimeBegin + TRUE_TIME_AN;

        //    if (timeAN > TimeBegin)      // 0-ой виток
        //        timeAN -= orbit.Period; ;
        //    int count = 0;
        //    double TIME_BEGIN = 0.0;
        //    for (nodeCur = 1; nodeCur <= size; ++nodeCur)
        //    {
        //        tBeginCur = timeAN + (nodeCur - 1) * orbit.Period;

        //        PRDCTNode node = new PRDCTNode();

        //        for (i = 0; i < 4; ++i)
        //        {
        //            tQuartBegin = tBeginCur + orbit.Quarts[i];
        //            tQuartEnd = tBeginCur + orbit.Quarts[i + 1];

        //            if (tQuartEnd <= TimeBegin) { continue; }
        //            if (tQuartBegin < TimeBegin) { tQuartBegin = TimeBegin; }
        //            if (tQuartBegin > TimeEnd) { continue; }
        //            if (tQuartEnd > TimeEnd) { tQuartEnd = TimeEnd; }

        //            //    if( count++ == 0 )  {
        //            //      timeCorrection = PRDCT_CLASS_QUARTERS_CUR_NODE[ i + 1 ] - tQuartEnd;
        //            //     TIME_BEGIN = tQuartBegin + timeCorrection;
        //            //      }
        //            if (count++ == 0)     // 29.10.2014 !!!!!!!!!!!!!!!!!!!!1
        //            {
        //                //                     timeCorrection = -TimeBegin;
        //                //                     TIME_BEGIN = tQuartBegin + timeCorrection;
        //            }


        //            node.Quarts.Add(new PRDCTNodeQuarter
        //            {
        //                //                    TimeBegin = tQuartBegin + timeCorrection - TIME_BEGIN,
        //                //                    TimeEnd = tQuartEnd + timeCorrection - TIME_BEGIN,
        //                Quart = i + 1
        //            });
        //        }
        //        node.Value = nodeCur;
        //        nodes.Add(node);
        //    }

        //    return nodes;
        //}

    }
}
