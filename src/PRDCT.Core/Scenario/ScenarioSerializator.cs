using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PRDCT.Core
{
    public static class Ext
    {
        private static XElement ToXElement<T>(this object obj)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, obj);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return XElement.Parse(xmlString);


            //using (var memoryStream = new MemoryStream())
            //{
            //    using (TextWriter streamWriter = new StreamWriter(memoryStream))
            //    {
            //        var xmlSerializer = new XmlSerializer(typeof(T));
            //        xmlSerializer.Serialize(streamWriter, obj);
            //        return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
            //    }
            //}
        }

        public static T FromXElement<T>(this XElement xElement)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }

        public static XElement ToXElement(this ScenarioCs scenario)
        {
            return scenario.ToXElement<ScenarioCs>();
        }

        public static XElement ToXElement(this BaseScenario scenario)
        {
            return scenario.ToXElement<BaseScenario>();
        }

        //public static XElement ToXElement(this ScenarioXML scenario)
        //{
        //    return scenario.ToXElement<ScenarioXML>();
        //}
    }


    //    [Serializable()]
    //    public class ScenarioXML
    //    {
    //        public ScenarioXML()
    //        {
    //            Objects = new List<ObjectXML>();
    //        }

    //        public ScenarioXML(Scenario scenario) : this()
    //        {
    //  //          this.Id = scenario.Id.ToString();
    //  //          this.Name = scenario.Name;
    //  //          this.Description = scenario.Description;

    //            foreach (var obj in scenario.ToObjects<ObjectHeader>())
    //            {
    //                Objects.Add(ObjectXML.Create(obj));
    //            }
    //        }

    //        public static Scenario ToScenario(Guid id, string name, string description, XElement XMLObjects)
    //        {
    //            var scXML = XMLObjects.FromXElement<ScenarioXML>();

    //            Scenario scenario = new Scenario(id/*Guid.Parse(scXML.Id)*/, name/*scXML.Name*/, description/*scXML.Description*/);

    //            foreach (var item in scXML.Objects.Where(i => i.ParentId == string.Empty))
    //            {
    //                scenario.Add(item.ToObjectHeader());
    //            }

    //            foreach (var item in scXML.Objects.Where(i => i.ParentId != string.Empty))
    //            {
    //                var parent = scXML.Objects.Where(i => i.Id == item.ParentId).Single();

    //                var obj = item.ToObjectHeader();
    //                obj.Parent = scenario.ToObjects<ObjectHeader>().Where(i => i.Id == Guid.Parse(parent.Id)).Single();// parent.ToObjectHeader();
    //                scenario.Add(obj);
    //            }

    //            return scenario;
    //        }

    ////        public string Id { get; set; }
    ////        public string Name { get; set; }
    ////        public string Description { get; set; }

    //        [XmlElement(typeof(SatelliteXML))]
    //        [XmlElement(typeof(RegionXML))]
    //        [XmlElement(typeof(SensorXML))]
    //        public List<ObjectXML> Objects { get; set; }
    //    }

    //    [Serializable()]
    //    [XmlRoot("ScenarioXML")]
    //    public abstract class ObjectXML
    //    {
    //        public ObjectXML() { }
    //        public ObjectXML(ObjectHeader obj)
    //        {
    //            this.Id = obj.Id.ToString();
    //            this.Name = obj.Name;
    //            this.Description = obj.Description;
    //        }

    //        public static ObjectXML Create(ObjectHeader obj)
    //        {
    //            if (obj is ObjectHeader<Satellite>)
    //                return new SatelliteXML(obj);
    //            if (obj is ObjectHeader<Region>)
    //                return new RegionXML(obj);
    //            if (obj is ObjectHeader<Sensor>)
    //                return new SensorXML(obj);
    //            return null;
    //        }

    //        public string Id { get; set; }
    //        public string Name { get; set; }
    //        public string Description { get; set; }

    //        public string ParentId { get; set; } = string.Empty;

    //        public abstract ObjectHeader ToObjectHeader();
    //    }

    //    [Serializable()]
    //    public class SatelliteXML : ObjectXML
    //    {
    //        public SatelliteXML() { }

    //        public SatelliteXML(ObjectHeader obj) : base(obj)
    //        {
    //            var sat = (obj as ObjectHeader<Satellite>).Tag;

    //            this.SemimajorAxis = sat.Orbit.SemimajorAxis;
    //            this.Eccentricity = sat.Orbit.Eccentricity;
    //            this.Inclination = sat.Orbit.Inclination;
    //            this.ArgumentOfPerigee = sat.Orbit.ArgumentOfPerigee;
    //            this.LonAscnNode = sat.Orbit.LonAscnNode;
    //            this.RAAN = sat.Orbit.RAAN;
    //            this.Period = sat.Orbit.Period;
    //            this.Epoch = sat.Orbit.Epoch;

    //            this.TrueAnomaly = sat.TrueAnomaly;
    //            this.StartTime = sat.StartTime;
    //            this.StopTime = sat.StopTime;
    //        }

    //        public override ObjectHeader ToObjectHeader()
    //        {
    //            ObjectHeader<Satellite> sattelite = new ObjectHeader<Satellite>(Guid.Parse(Id), Name, Description);

    //            Orbit orbit = new Orbit(SemimajorAxis, Eccentricity, Inclination, ArgumentOfPerigee, LonAscnNode, RAAN, Period, Epoch);
    //            sattelite.Tag = new Satellite(orbit, StartTime, StopTime, TrueAnomaly);

    //            return sattelite;
    //        }

    //        public double SemimajorAxis { get; set; }
    //        public double Eccentricity { get; set; }
    //        public double Inclination { get; set; }
    //        public double ArgumentOfPerigee { get; set; }
    //        public double LonAscnNode { get; set; }
    //        public double RAAN { get; set; }
    //        public double Period { get; set; }
    //        public DateTime Epoch { get; set; }

    //        public double TrueAnomaly { get; set; }

    //        public DateTime StartTime { get; set; }

    //        public DateTime StopTime { get; set; }
    //    }

    //    [Serializable()]
    //    public class SensorXML : ObjectXML
    //    {
    //        public SensorXML() { }

    //        public SensorXML(ObjectHeader obj) : base(obj)
    //        {
    //            var sensor = (obj as ObjectHeader<Sensor>).Tag;

    //            this.ParentId = obj.Parent.Id.ToString();
    //            this.VerticalHalfAngleDEG = sensor.VerticalHalfAngleDEG;
    //            this.RollAngleDEG = sensor.RollAngleDEG;
    //        }

    //        public override ObjectHeader ToObjectHeader()
    //        {
    //            var sens = new Sensor
    //            {
    //                VerticalHalfAngleDEG = VerticalHalfAngleDEG,
    //                RollAngleDEG = RollAngleDEG
    //            };

    //            var sensor = new ObjectHeader<Sensor>(Guid.Parse(Id), Name, Description)
    //            {
    //                Tag = sens
    //            };

    //            return sensor;
    //        }

    //        public double VerticalHalfAngleDEG { get; set; }

    //        public double RollAngleDEG { get; set; }

    //      //  [XmlElement(typeof(SatelliteXML))]
    //      //  public ObjectXML Parent { get; set; }
    //    }

    //    [Serializable()]
    //    public class RegionXML : ObjectXML
    //    {
    //        public RegionXML() { }

    //        public RegionXML(ObjectHeader obj) : base(obj)
    //        {
    //            var reg = (obj as ObjectHeader<Region>).Tag;

    //            this.VertsLon.AddRange(reg.Verts.Select(v => v.x));
    //            this.VertsLat.AddRange(reg.Verts.Select(v => v.y));
    //            this.Type = reg.Type;
    //        }

    //        public override ObjectHeader ToObjectHeader()
    //        {
    //            var region = new ObjectHeader<Region>(Guid.Parse(Id), Name, Description);
    //        //    region.Id = Guid.Parse(Id);
    //        //    region.Name = Name;
    //        //    region.Description = Description;

    //            List<dvec2> verts = new List<dvec2>();

    //            for (int i = 0; i < VertsLon.Count; i++)            
    //                verts.Add(new dvec2(VertsLon[i], VertsLat[i]));

    //            region.Tag = new Region(verts, Type);

    //            return region;
    //        }

    //        public List<double> VertsLon { get; set; } = new List<double>();
    //        public List<double> VertsLat { get; set; } = new List<double>();
    //        public RegionType Type { get; set; }
    //    }

    //public static class ScenarioSerializator
    //{
    //    public static string ToXML(Scenario scenario)
    //    {
    //        return ConvertObjectToXMLString(new ScenarioXML(scenario));
    //    }

    //    public static Scenario ToScenario(string XMLObjects)
    //    {
    //        var scXML = ConvertXMLStringToObject<ScenarioXML>(XMLObjects);

    //        Scenario scenario = new Scenario(Guid.Parse(scXML.Id), scXML.Name, scXML.Description);

    //        foreach (var item in scXML.Objects.Where(i => i.ParentId == string.Empty))
    //        {
    //            scenario.Add(item.ToObjectHeader());
    //        }

    //        foreach (var item in scXML.Objects.Where(i => i.ParentId != string.Empty))
    //        {
    //            var parent = scXML.Objects.Where(i => i.Id == item.ParentId).Single();

    //            var obj = item.ToObjectHeader();
    //            obj.Parent = parent.ToObjectHeader();
    //            scenario.Add(obj);
    //        }

    //        return scenario;
    //    }

    //    //private static string ConvertObjectToXMLString(object classObject)
    //    //{
    //    //    string xmlString = null;
    //    //    XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
    //    //    using (MemoryStream memoryStream = new MemoryStream())
    //    //    {
    //    //        xmlSerializer.Serialize(memoryStream, classObject);
    //    //        memoryStream.Position = 0;
    //    //        xmlString = new StreamReader(memoryStream).ReadToEnd();
    //    //    }
    //    //    return xmlString;
    //    //}

    //    //private static T ConvertXMLStringToObject<T>(string XMLString)
    //    //{
    //    //    var serializer = new XmlSerializer(typeof(T));
    //    //    T scXML;

    //    //    using (TextReader reader = new StringReader(XMLString))
    //    //    {
    //    //        scXML = (T)serializer.Deserialize(reader);
    //    //    }

    //    //    return scXML;
    //    //}

    //}
}
