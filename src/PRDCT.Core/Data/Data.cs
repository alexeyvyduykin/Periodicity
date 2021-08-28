using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

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

        private static readonly System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();


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
        partial void OnValidate(System.Data.Linq.ChangeAction action);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionName", DbType = "NVarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionType", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionData", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionDescription", DbType = "NVarChar(MAX)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PeriodicityID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LatDEG", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LatRAD", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LonLeft", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LonRight", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PeriodicityID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SatelliteID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LatDEG", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LatRAD", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LonLeft", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LonRight", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Node", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeLeft", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeRight", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegionID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PeriodicityID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SatelliteID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Node", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeBegin", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeEnd", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Quart", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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
        partial void OnValidate(System.Data.Linq.ChangeAction action);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PeriodicityID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PeriodicityName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Epoch", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeStart", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeDuration", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LatitudeStep", DbType = "Float NOT NULL")]
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

        private System.Xml.Linq.XElement _XMLObjects;

        private EntityRef<ScenarioUserTable> _ScenarioUserTable;

        #region Определения метода расширяемости
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnXMLObjectsChanging(System.Xml.Linq.XElement value);
        partial void OnXMLObjectsChanged();
        #endregion

        public ScenarioTable()
        {
            _ScenarioUserTable = default(EntityRef<ScenarioUserTable>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_XMLObjects", DbType = "Xml NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement XMLObjects
        {
            get
            {
                return _XMLObjects;
            }
            set
            {
                if ((_XMLObjects != value))
                {
                    OnXMLObjectsChanging(value);
                    SendPropertyChanging();
                    _XMLObjects = value;
                    SendPropertyChanged("XMLObjects");
                    OnXMLObjectsChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ScenarioTable_ScenarioUserTable", Storage = "_ScenarioUserTable", ThisKey = "Id", OtherKey = "Id", IsUnique = true, IsForeignKey = false)]
        public ScenarioUserTable ScenarioUserTable
        {
            get
            {
                return _ScenarioUserTable.Entity;
            }
            set
            {
                ScenarioUserTable previousValue = _ScenarioUserTable.Entity;
                if (((previousValue != value)
                            || (_ScenarioUserTable.HasLoadedOrAssignedValue == false)))
                {
                    SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        _ScenarioUserTable.Entity = null;
                        previousValue.ScenarioTable = null;
                    }
                    _ScenarioUserTable.Entity = value;
                    if ((value != null))
                    {
                        value.ScenarioTable = this;
                    }
                    SendPropertyChanged("ScenarioUserTable");
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

        private EntityRef<ScenarioTable> _ScenarioTable;

        #region Определения метода расширяемости
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        partial void OnCategoryChanging(string value);
        partial void OnCategoryChanged();
        #endregion

        public ScenarioUserTable()
        {
            _ScenarioTable = default(EntityRef<ScenarioTable>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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
                    if (_ScenarioTable.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    OnIdChanging(value);
                    SendPropertyChanging();
                    _Id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Category", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "ScenarioTable_ScenarioUserTable", Storage = "_ScenarioTable", ThisKey = "Id", OtherKey = "Id", IsForeignKey = true)]
        public ScenarioTable ScenarioTable
        {
            get
            {
                return _ScenarioTable.Entity;
            }
            set
            {
                ScenarioTable previousValue = _ScenarioTable.Entity;
                if (((previousValue != value)
                            || (_ScenarioTable.HasLoadedOrAssignedValue == false)))
                {
                    SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        _ScenarioTable.Entity = null;
                        previousValue.ScenarioUserTable = null;
                    }
                    _ScenarioTable.Entity = value;
                    if ((value != null))
                    {
                        value.ScenarioUserTable = this;
                        _Id = value.Id;
                    }
                    else
                    {
                        _Id = default(string);
                    }
                    SendPropertyChanged("ScenarioTable");
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
