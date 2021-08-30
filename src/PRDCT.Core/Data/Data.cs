﻿using System;
using System.ComponentModel;

namespace Periodicity.Core
{
    public class RegionCuts
    {
        public string PeriodicityID { get; set; }
    
        public string RegionID { get; set; }
       
        public double LatDEG { get; set; }
     
        public double LatRAD { get; set; }
       
        public double LonLeft { get; set; }
   
        public double LonRight { get; set; }
    }


    public class Ivals
    {       
        public string PeriodicityID { get; set; }
  
        public string SatelliteID { get; set; }
        
        public double LatDEG { get; set; }
      
        public double LatRAD { get; set; }
   
        public double LonLeft { get; set; }
     
        public double LonRight { get; set; }
    
        public int Node { get; set; }
      
        public double TimeLeft { get; set; }
      
        public double TimeRight { get; set; }
      
        public string RegionID { get; set; }
    }


    public class TimeIvals
    {       
        public string PeriodicityID { get; set; }
    
        public string SatelliteID { get; set; }
        
        public int Node { get; set; }
      
        public double TimeBegin { get; set; }
    
        public double TimeEnd { get; set; }
    
        public TrackNodeQuarter Quart { get; set; }
    }
}
