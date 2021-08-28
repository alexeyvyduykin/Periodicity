using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRDCT.Core
{
    public class ScenarioHeader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ScenarioPropertiesHeader Properties { get; set; }
    }

    public class ScenarioPropertiesHeader
    {
        public bool IsUser { get; set; }
        public string Category { get; set; }
    }

    //public class Scenario
    //{
    //    public event EventHandler Changed;

    //    protected virtual void OnChanged(EventArgs e)
    //    {
    //        if (Changed != null)
    //            Changed(this, e);
    //    }

    //    //   private Scenario() : this(Guid.NewGuid(), "Scenario1", "")
    //    //   {
    //    //       Objects.Clear();

    //    //       string ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = C:\Users\User\Выдуйкин А\Projects_OpenGL_C#\PeriodicityRebirth 1.0\PRDCT\PRDCT\dbPRDCT.mdf";

    //    //       var timeBegin = new DateTime(2007, 7, 1, 12, 0, 0);
    //    //       var timeEnd = timeBegin.AddDays(1);

    //    //       Orbit orbit1 = new Orbit(        
    //    //           7000.0,        
    //    //           0.0,        
    //    //           97.0 * MyMath.DegreesToRadians,        
    //    //           0.0 * MyMath.DegreesToRadians,        
    //    //           260.934 * MyMath.DegreesToRadians,        
    //    //           0.0 * MyMath.DegreesToRadians,        
    //    //           5828.52,        
    //    //           timeBegin);

    //    //       Orbit orbit2 = new Orbit(
    //    //           7000.0,
    //    //           0.0,
    //    //           97.0 * MyMath.DegreesToRadians,
    //    //           0.0 * MyMath.DegreesToRadians,
    //    //           267.022 * MyMath.DegreesToRadians,
    //    //           0.0 * MyMath.DegreesToRadians,
    //    //           5828.52,
    //    //           timeBegin);

    //    //       Sensor sensorLeft = new SensorSAR(11.0, 33.0);

    //    //       var satellite1 = new Satellite(orbit1, timeBegin, timeEnd, 0.0);

    //    ////       satellite1.Bands.Add(new Band(orbit1, 22.0, 44.0, BandMode.Left));

    //    //       var satHeader1 = new ObjectHeader<Satellite>()
    //    //       {
    //    //           Id = Guid.NewGuid(),
    //    //           Name = "Satellite1",
    //    //           Object = satellite1
    //    //       };

    //    //       var sensHeader1 = new ObjectHeader<Sensor>()
    //    //       {
    //    //           Id = Guid.NewGuid(),
    //    //           Name = "SensorLeft",
    //    //           Object = sensorLeft,
    //    //           Parent = satHeader1
    //    //       };

    //    //       var satellite2 = new Satellite(orbit2, timeBegin, timeEnd, 90.0 * MyMath.DegreesToRadians);

    //    // //      satellite2.Bands.Add(new Band(orbit2, 22.0, 44.0, BandMode.Left));

    //    //       var satHeader2 = new ObjectHeader<Satellite>()
    //    //       {
    //    //           Id = Guid.NewGuid(),
    //    //           Name = "Satellite2",
    //    //           Object = satellite2
    //    //       };

    //    //       var sensHeader3 = new ObjectHeader<Sensor>()
    //    //       {
    //    //           Id = Guid.NewGuid(),
    //    //           Name = "SensorLeft",
    //    //           Object = sensorLeft,
    //    //           Parent = satHeader2
    //    //       };

    //    //       Objects.Add(satHeader1);
    //    //       Objects.Add(satHeader2);

    //    //       Objects.Add(sensHeader1);
    //    //       Objects.Add(sensHeader3);

    //    //       dbPRDCTDataContext db = new dbPRDCTDataContext(ConnectionString);
    //    //       var region = db.Regions.Where(c => c.RegionName == "Северное полушарие Земли").Single();
    //    //       Objects.Add(Converter.ToRegionHeader(region));
    //    //   }

    //    // 1. Создание нового сценария

    //    public Scenario(Guid id, string name, string description)
    //    {
    //        this.Id = id;
    //        this.Name = name;
    //        this.Description = description;

    //        Objects = new List<ObjectHeader>();

    //       // ScenarioControl = new ScenarioDataBaseArchivator();
    //    }

    //    public static Scenario Clone(Scenario scenario)
    //    {
    //        var sc = new Scenario(Guid.NewGuid(), scenario.Name, scenario.Description)
    //        {
    //            Objects = scenario.Objects

    //        };

    //        sc.Changed += scenario.Changed;

    //        return sc;
    //    }

    //    // 3. Добавление в сценарий
    //    public void Add(Scenario scenario)
    //    {
    //        foreach (var item in scenario.Objects)
    //        {
    //            if(Objects.TrueForAll(obj => obj.Id != item.Id))
    //                Objects.Add(item);
    //        }
    //        OnChanged(EventArgs.Empty);
    //    }

    //    // 5. Очистка сценария
    //    public void Clear()
    //    {
    //        Objects.Clear();
    //        OnChanged(EventArgs.Empty);
    //    }

    //    // 6. Изменение сценария

    //    public void Add(ObjectHeader obj)
    //    {
    //        Objects.Add(obj);
    //        OnChanged(EventArgs.Empty);
    //    }

    //    public void Delete(ObjectHeader obj)
    //    {
    //        if( Objects.Contains(obj) == true )
    //        {
    //            foreach (var item in Objects.Where(b => b.Parent == obj).Reverse())  // delete all child objects                
    //                Objects.Remove(item);

    //            Objects.Remove(obj);
    //            OnChanged(EventArgs.Empty);
    //        }
    //    }

    //    public IEnumerable<T> ToObjects<T>() where T : ObjectHeader
    //    {
    //        return Objects.Where(obj => obj is T).Select(obj => (T)obj);
    //    }

    //    //public void LoadDefault()
    //    //{
    //    //    var defaultSceanrio = new Scenario();

    //    //    this.Id = defaultSceanrio.Id;
    //    //    this.Name = defaultSceanrio.Name;
    //    //    this.Description = defaultSceanrio.Description;
    //    //    this.Objects = defaultSceanrio.Objects;

    //    //    OnChanged(EventArgs.Empty);
    //    //}

    //    public Guid Id { get; protected set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }

    //    private List<ObjectHeader> Objects { get; set; }

    //    //private readonly ScenarioManager ScenarioControl;
    //}

}
