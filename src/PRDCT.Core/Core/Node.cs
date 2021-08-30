using System;
using System.Collections.Generic;

namespace Periodicity.Core
{
    public enum TrackNodeQuarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public struct NodeQuarter
    {
        public TrackNodeQuarter Quart { get; set; }
        public double TimeBegin { get; set; }
        public double TimeEnd { get; set; }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<NodeQuarter> Quarts { get; protected set; } = new List<NodeQuarter>();
    }
}
