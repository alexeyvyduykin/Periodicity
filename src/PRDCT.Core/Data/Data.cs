using System;
using System.ComponentModel;

namespace Periodicity.Core
{
    public static class MyData
    {
        public const string ConnectionString =
            @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = C:\Users\User\AlexeyVyduykin\CSharpProjects\Projects_OpenGL_C#\PeriodicityRebirth 1.0\PRDCT\PRDCT\dbPRDCT.mdf";

        public static string FromQuart(int quart)
        {
            switch (quart)
            {
                case 1:
                    return "FIRST";
                case 2:
                    return "SECOND";
                case 3:
                    return "THIRD";
                case 4:
                    return "FOURTH";
                default:
                    return "";
            }
        }

        public static int ToQuart(string quart)
        {
            switch (quart)
            {
                case "FIRST":
                    return 1;
                case "SECOND":
                    return 2;
                case "THIRD":
                    return 3;
                case "FOURTH":
                    return 5;
                default:
                    return 0;
            }
        }

    }


    public partial class dbPRDCTDataContext
    {
        //public IQueryable<Satellites> SatelliteCollection(string periodicityID)
        //{
        //    var ids = TimeIvals.
        //        Where(t => t.PeriodicityID == periodicityID).
        //        Select(m => m.SatelliteID).
        //        Distinct();

        //    // fast method
        //    // var hs = new HashSet<string>(ids);
        //    // return Satellites.Where(o => hs.Contains(o.SatelliteID));

        //    return Satellites.Where(o => ids.Any(id => id == o.SatelliteID));            
        //}

        //public IQueryable<Regions> RegionCollection(string periodicityID)
        //{
        //    var ids = RegionCuts.
        //        Where(r => r.PeriodicityID == periodicityID).
        //        Select(m => m.RegionID).
        //        Distinct();
        //    return Regions.Where(o => ids.Any(id => id == o.RegionID));
        //}
    }


    public class RegionCutsType : RegionCuts
    {

        public double Lon
        {
            get
            {
                return base.LonLeft;
            }
        }
    }

    public partial class dbPRDCTDataContext
    {

        public Regions Regions { get; }

        public RegionCuts RegionCuts { get; }

        public Periodicities Periodicities { get; }


        public ScenarioTable ScenarioTable { get; }

        public ScenarioUserTable ScenarioUserTable { get; }
    }


    public partial class Regions : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _RegionID;

        private string _RegionName;

        private string _RegionType;

        private string _RegionData;

        private string _RegionDescription;

        #region Определения метода расширяемости
        partial void OnLoaded();
        partial void OnCreated();
        partial void OnRegionIDChanging(string value);
        partial void OnRegionIDChanged();
        partial void OnRegionNameChanging(string value);
        partial void OnRegionNameChanged();
        partial void OnRegionTypeChanging(string value);
        partial void OnRegionTypeChanged();
        partial void OnRegionDataChanging(string value);
        partial void OnRegionDataChanged();
        partial void OnRegionDescriptionChanging(string value);
        partial void OnRegionDescriptionChanged();
        #endregion

        public Regions()
        {
            OnCreated();
        }

        public string RegionID
        {
            get
            {
                return _RegionID;
            }
            set
            {
                if ((_RegionID != value))
                {
                    OnRegionIDChanging(value);
                    SendPropertyChanging();
                    _RegionID = value;
                    SendPropertyChanged("RegionID");
                    OnRegionIDChanged();
                }
            }
        }

        public string RegionName
        {
            get
            {
                return _RegionName;
            }
            set
            {
                if ((_RegionName != value))
                {
                    OnRegionNameChanging(value);
                    SendPropertyChanging();
                    _RegionName = value;
                    SendPropertyChanged("RegionName");
                    OnRegionNameChanged();
                }
            }
        }

        public string RegionType
        {
            get
            {
                return _RegionType;
            }
            set
            {
                if ((_RegionType != value))
                {
                    OnRegionTypeChanging(value);
                    SendPropertyChanging();
                    _RegionType = value;
                    SendPropertyChanged("RegionType");
                    OnRegionTypeChanged();
                }
            }
        }

        public string RegionData
        {
            get
            {
                return _RegionData;
            }
            set
            {
                if ((_RegionData != value))
                {
                    OnRegionDataChanging(value);
                    SendPropertyChanging();
                    _RegionData = value;
                    SendPropertyChanged("RegionData");
                    OnRegionDataChanged();
                }
            }
        }

        public string RegionDescription
        {
            get
            {
                return _RegionDescription;
            }
            set
            {
                if ((_RegionDescription != value))
                {
                    OnRegionDescriptionChanging(value);
                    SendPropertyChanging();
                    _RegionDescription = value;
                    SendPropertyChanged("RegionDescription");
                    OnRegionDescriptionChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public partial class RegionCuts
    {

        private string _PeriodicityID;

        private string _RegionID;

        private double _LatDEG;

        private double _LatRAD;

        private double _LonLeft;

        private double _LonRight;

        public RegionCuts()
        {
        }


        public string PeriodicityID
        {
            get
            {
                return _PeriodicityID;
            }
            set
            {
                if ((_PeriodicityID != value))
                {
                    _PeriodicityID = value;
                }
            }
        }

    
        public string RegionID
        {
            get
            {
                return _RegionID;
            }
            set
            {
                if ((_RegionID != value))
                {
                    _RegionID = value;
                }
            }
        }

       
        public double LatDEG
        {
            get
            {
                return _LatDEG;
            }
            set
            {
                if ((_LatDEG != value))
                {
                    _LatDEG = value;
                }
            }
        }

     
        public double LatRAD
        {
            get
            {
                return _LatRAD;
            }
            set
            {
                if ((_LatRAD != value))
                {
                    _LatRAD = value;
                }
            }
        }

       
        public double LonLeft
        {
            get
            {
                return _LonLeft;
            }
            set
            {
                if ((_LonLeft != value))
                {
                    _LonLeft = value;
                }
            }
        }

   
        public double LonRight
        {
            get
            {
                return _LonRight;
            }
            set
            {
                if ((_LonRight != value))
                {
                    _LonRight = value;
                }
            }
        }
    }


    public partial class Ivals
    {

        private string _PeriodicityID;

        private string _SatelliteID;

        private double _LatDEG;

        private double _LatRAD;

        private double _LonLeft;

        private double _LonRight;

        private int _Node;

        private double _TimeLeft;

        private double _TimeRight;

        private string _RegionID;

        public Ivals()
        {
        }

       
        public string PeriodicityID
        {
            get
            {
                return _PeriodicityID;
            }
            set
            {
                if ((_PeriodicityID != value))
                {
                    _PeriodicityID = value;
                }
            }
        }

  
        public string SatelliteID
        {
            get
            {
                return _SatelliteID;
            }
            set
            {
                if ((_SatelliteID != value))
                {
                    _SatelliteID = value;
                }
            }
        }

        
        public double LatDEG
        {
            get
            {
                return _LatDEG;
            }
            set
            {
                if ((_LatDEG != value))
                {
                    _LatDEG = value;
                }
            }
        }

      
        public double LatRAD
        {
            get
            {
                return _LatRAD;
            }
            set
            {
                if ((_LatRAD != value))
                {
                    _LatRAD = value;
                }
            }
        }

   
        public double LonLeft
        {
            get
            {
                return _LonLeft;
            }
            set
            {
                if ((_LonLeft != value))
                {
                    _LonLeft = value;
                }
            }
        }

     
        public double LonRight
        {
            get
            {
                return _LonRight;
            }
            set
            {
                if ((_LonRight != value))
                {
                    _LonRight = value;
                }
            }
        }

    
        public int Node
        {
            get
            {
                return _Node;
            }
            set
            {
                if ((_Node != value))
                {
                    _Node = value;
                }
            }
        }

      
        public double TimeLeft
        {
            get
            {
                return _TimeLeft;
            }
            set
            {
                if ((_TimeLeft != value))
                {
                    _TimeLeft = value;
                }
            }
        }

      
        public double TimeRight
        {
            get
            {
                return _TimeRight;
            }
            set
            {
                if ((_TimeRight != value))
                {
                    _TimeRight = value;
                }
            }
        }

      
        public string RegionID
        {
            get
            {
                return _RegionID;
            }
            set
            {
                if ((_RegionID != value))
                {
                    _RegionID = value;
                }
            }
        }
    }


    public partial class TimeIvals
    {

        private string _PeriodicityID;

        private string _SatelliteID;

        private int _Node;

        private double _TimeBegin;

        private double _TimeEnd;

        private string _Quart;

        public TimeIvals()
        {
        }

       
        public string PeriodicityID
        {
            get
            {
                return _PeriodicityID;
            }
            set
            {
                if ((_PeriodicityID != value))
                {
                    _PeriodicityID = value;
                }
            }
        }

    
        public string SatelliteID
        {
            get
            {
                return _SatelliteID;
            }
            set
            {
                if ((_SatelliteID != value))
                {
                    _SatelliteID = value;
                }
            }
        }

        
        public int Node
        {
            get
            {
                return _Node;
            }
            set
            {
                if ((_Node != value))
                {
                    _Node = value;
                }
            }
        }

      
        public double TimeBegin
        {
            get
            {
                return _TimeBegin;
            }
            set
            {
                if ((_TimeBegin != value))
                {
                    _TimeBegin = value;
                }
            }
        }

    
        public double TimeEnd
        {
            get
            {
                return _TimeEnd;
            }
            set
            {
                if ((_TimeEnd != value))
                {
                    _TimeEnd = value;
                }
            }
        }

    
        public string Quart
        {
            get
            {
                return _Quart;
            }
            set
            {
                if ((_Quart != value))
                {
                    _Quart = value;
                }
            }
        }
    }


    public partial class Periodicities : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _PeriodicityID;

        private string _PeriodicityName;

        private string _Epoch;

        private double _TimeStart;

        private double _TimeDuration;

        private double _LatitudeStep;

        #region Определения метода расширяемости
        partial void OnLoaded();
        partial void OnCreated();
        partial void OnPeriodicityIDChanging(string value);
        partial void OnPeriodicityIDChanged();
        partial void OnPeriodicityNameChanging(string value);
        partial void OnPeriodicityNameChanged();
        partial void OnEpochChanging(string value);
        partial void OnEpochChanged();
        partial void OnTimeStartChanging(double value);
        partial void OnTimeStartChanged();
        partial void OnTimeDurationChanging(double value);
        partial void OnTimeDurationChanged();
        partial void OnLatitudeStepChanging(double value);
        partial void OnLatitudeStepChanged();
        #endregion

        public Periodicities()
        {
            OnCreated();
        }

     
        public string PeriodicityID
        {
            get
            {
                return _PeriodicityID;
            }
            set
            {
                if ((_PeriodicityID != value))
                {
                    OnPeriodicityIDChanging(value);
                    SendPropertyChanging();
                    _PeriodicityID = value;
                    SendPropertyChanged("PeriodicityID");
                    OnPeriodicityIDChanged();
                }
            }
        }

        
        public string PeriodicityName
        {
            get
            {
                return _PeriodicityName;
            }
            set
            {
                if ((_PeriodicityName != value))
                {
                    OnPeriodicityNameChanging(value);
                    SendPropertyChanging();
                    _PeriodicityName = value;
                    SendPropertyChanged("PeriodicityName");
                    OnPeriodicityNameChanged();
                }
            }
        }

  
        public string Epoch
        {
            get
            {
                return _Epoch;
            }
            set
            {
                if ((_Epoch != value))
                {
                    OnEpochChanging(value);
                    SendPropertyChanging();
                    _Epoch = value;
                    SendPropertyChanged("Epoch");
                    OnEpochChanged();
                }
            }
        }

      
        public double TimeStart
        {
            get
            {
                return _TimeStart;
            }
            set
            {
                if ((_TimeStart != value))
                {
                    OnTimeStartChanging(value);
                    SendPropertyChanging();
                    _TimeStart = value;
                    SendPropertyChanged("TimeStart");
                    OnTimeStartChanged();
                }
            }
        }

       
        public double TimeDuration
        {
            get
            {
                return _TimeDuration;
            }
            set
            {
                if ((_TimeDuration != value))
                {
                    OnTimeDurationChanging(value);
                    SendPropertyChanging();
                    _TimeDuration = value;
                    SendPropertyChanged("TimeDuration");
                    OnTimeDurationChanged();
                }
            }
        }

      
        public double LatitudeStep
        {
            get
            {
                return _LatitudeStep;
            }
            set
            {
                if ((_LatitudeStep != value))
                {
                    OnLatitudeStepChanging(value);
                    SendPropertyChanging();
                    _LatitudeStep = value;
                    SendPropertyChanged("LatitudeStep");
                    OnLatitudeStepChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public partial class ScenarioTable : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _Id;

        private string _Name;

        private string _Description;


        private ScenarioUserTable _ScenarioUserTable;

        #region Определения метода расширяемости
        partial void OnLoaded();
 
        partial void OnCreated();
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();

        #endregion

        public ScenarioTable()
        {
   
            OnCreated();
        }

       
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if ((_Id != value))
                {
                    OnIdChanging(value);
                    SendPropertyChanging();
                    _Id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

      
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if ((_Name != value))
                {
                    OnNameChanging(value);
                    SendPropertyChanging();
                    _Name = value;
                    SendPropertyChanged("Name");
                    OnNameChanged();
                }
            }
        }


        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if ((_Description != value))
                {
                    OnDescriptionChanging(value);
                    SendPropertyChanging();
                    _Description = value;
                    SendPropertyChanged("Description");
                    OnDescriptionChanged();
                }
            }
        }
     
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class ScenarioUserTable : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static readonly PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _Id;

        private string _Category;

        //private ScenarioTable _ScenarioTable;

        #region Определения метода расширяемости
        partial void OnCreated();
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        partial void OnCategoryChanging(string value);
        partial void OnCategoryChanged();
        #endregion

        public ScenarioUserTable()
        {      
            OnCreated();
        }
  
        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                if ((_Category != value))
                {
                    OnCategoryChanging(value);
                    SendPropertyChanging();
                    _Category = value;
                    SendPropertyChanged("Category");
                    OnCategoryChanged();
                }
            }
        }
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
