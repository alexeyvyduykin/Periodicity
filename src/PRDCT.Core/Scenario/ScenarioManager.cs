using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRDCT.Data;
using System.Data.SqlClient;
using PRDCT.Core;

namespace PRDCT.Core
{
    public class Scenario
    {
        public Guid Id { get; set; }
    }

    public abstract class ScenarioManager
    {
        public abstract void Save(Scenario scenario/*, Guid id*/);   // FreeSave or ReSave

     //   public abstract void SaveAs(Scenario scenario, Guid id);  // FreeSave or NewSave

  //      public abstract Scenario Load(Guid id);

        public abstract void Delete(Guid id);

        public abstract IEnumerable<ScenarioHeader> AllScenaries();

        public abstract BaseScenario Load(Guid id);

        public abstract void Save(BaseScenario scenario);


    }

    public class ScenarioDataBaseArchivator : ScenarioManager
    {
        //public override Scenario Load(Guid id)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    var sc = db.Scenarios.Where(s => s.ScenarioID == id.ToString()).Single();

        //    Scenario scenario = new Scenario(id, sc.Name, sc.Description);

        //    foreach (var item in db.Scenarios.Where(s => s.ScenarioID == id.ToString()))
        //    {
        //        scenario.Add(FindObject(Type.GetType(item.ObjectType), Guid.Parse(item.ObjectID)));
        //    }

        //    return scenario;
        //}

        //public override Scenario Load(Guid id)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    var sc = db.ScenarioTable.Where(s => s.Id == id.ToString()).Single();

        //    return null;// ScenarioXML.ToScenario(Guid.Parse(sc.Id), sc.Name, sc.Description, sc.XMLObjects);
        //}

        public override BaseScenario Load(Guid id)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

            var sc = db.ScenarioTable.Where(s => s.Id == id.ToString()).Single();

            return Ext.FromXElement<BaseScenario>(sc.XMLObjects);
        }

        //private ObjectHeader FindObject(Type type, Guid id)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    if (type == typeof(Satellite))
        //    {
        //        var sat = db.Satellites.Where(s => s.SatelliteID == id.ToString()).Single();
        //        return Converter.ToSatelliteHeader(sat, new DateTime(2007, 7, 1, 12, 0, 0), new DateTime(2007, 7, 2, 12, 0, 0));
        //    }

        //    if (type == typeof(Region))
        //    {
        //        var region = db.Regions.Where(r => r.RegionID == id.ToString()).Single();
        //        return Converter.ToRegionHeader(region);
        //    }
        //    return null;
        //}

        //private void FreeSave(Scenario scenario)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    using (SqlConnection connection = new SqlConnection(MyData.ConnectionString))
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand() { Connection = connection };

        //        cmd.CommandText = "INSERT INTO Scenarios(ScenarioID, Name, Description, ObjectID, ObjectName, ObjectDescription, ObjectType) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7)";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@param1", "");
        //        cmd.Parameters.AddWithValue("@param2", "");
        //        cmd.Parameters.AddWithValue("@param3", "");
        //        cmd.Parameters.AddWithValue("@param4", "");
        //        cmd.Parameters.AddWithValue("@param5", "");
        //        cmd.Parameters.AddWithValue("@param6", "");
        //        cmd.Parameters.AddWithValue("@param7", "");

        //        foreach (var obj in scenario.ToObjects<ObjectHeader>())
        //        {
        //            cmd.Parameters["@param1"].Value = obj.Id.ToString();
        //            cmd.Parameters["@param2"].Value = obj.Name;
        //            cmd.Parameters["@param3"].Value = "";
        //            cmd.Parameters["@param4"].Value = obj.Id.ToString();
        //            cmd.Parameters["@param5"].Value = obj.Name;
        //            cmd.Parameters["@param6"].Value = obj.Description;
        //            cmd.Parameters["@param7"].Value = obj.Object.GetType().ToString();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public override void Save(Scenario scenario, bool saveAsNew)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    db.ExecuteCommand("DELETE FROM Scenarios");    // временное отладочное ограничение

        //    var count = db.Scenarios.Where(sc => sc.ScenarioID == scenario.Id.ToString()).Count();

        //    if (count == 0)    // такого сценария в БД ещё не было, сохраняем свободно
        //    {
        //        FreeSave(scenario);
        //    }
        //    else               // такой сценарий в БД уже есть
        //    {
        //        if (saveAsNew == true)      // сохраняем его как новый          
        //            SaveAsNew(scenario);                
        //        else
        //            Resave(scenario);       // перезаписываем его предварительно удалив
        //    }         
        //}

        public override void Save(Scenario scenario/*, Guid id*/)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
  //          db.ExecuteCommand("DELETE FROM ScenarioTable");    // временное отладочное ограничение

            var count = db.ScenarioTable.Where(sc => sc.Id == scenario.Id.ToString()).Count();

            if (db.ScenarioTable.All(sc => sc.Id != scenario.Id.ToString()) == true)
                FreeSave(scenario, scenario.Id);
            else
                Resave(scenario);

            //if (count == 0)    // такого сценария в БД ещё не было, сохраняем свободно
            //{
            //    FreeSave(scenario, scenario.Id);
            //}
            //else               // такой сценарий в БД уже есть
            //{
            //    Resave(scenario);       // перезаписываем его предварительно удалив
            //}
        }

        public override void Save(BaseScenario scenario)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

            var count = db.ScenarioTable.Where(sc => sc.Id == scenario.Id.ToString()).Count();

            if (db.ScenarioTable.All(sc => sc.Id != scenario.Id.ToString()) == true)
                FreeSave(scenario, scenario.Id);
            else
                Resave(scenario);
        }

        //public override void SaveAs(Scenario scenario, Guid id)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
        //    //          db.ExecuteCommand("DELETE FROM ScenarioTable");    // временное отладочное ограничение

        //    var count = db.ScenarioTable.Where(sc => sc.Id == scenario.Id.ToString()).Count();

        //    FreeSave(scenario, id);

        //    //if (count == 0)    // такого сценария в БД ещё не было, сохраняем свободно
        //    //{
        //    //    FreeSave(scenario, id);
        //    //}
        //    //else               // такой сценарий в БД уже есть
        //    //{
        //    //    SaveAsNew(scenario, id);  // сохраняем его как новый 
        //    //}
        //}

        private void FreeSave(Scenario scenario, Guid id)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

          //  var scenarioXML = new ScenarioXML(scenario);

            db.ScenarioTable.InsertOnSubmit(new ScenarioTable()
            {
                Id = id.ToString(),
                Name = "",// scenario.Name,
                Description ="",// scenario.Description,
                XMLObjects = null// scenarioXML.ToXElement(),    
          //      User = bool.FalseString           
            });
            db.SubmitChanges();
        }

        private void FreeSave(BaseScenario scenario, Guid id)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

            db.ScenarioTable.InsertOnSubmit(new ScenarioTable()
            {
                Id = id.ToString(),
                Name = scenario.Name,
                Description = scenario.Description,
                XMLObjects = scenario.ToXElement()
                //      User = bool.FalseString           
            });
            db.SubmitChanges();
        }

        //private void SaveAsNew(Scenario scenario, Guid id)
        //{
        //    dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

        //    var scenarioXML = new ScenarioXML(scenario);

        //    db.ScenarioTable.InsertOnSubmit(new ScenarioTable()
        //    {
        //        Id = id.ToString(),
        //        Name = scenario.Name,
        //        Description = scenario.Description,
        //        XMLObjects = scenarioXML.ToXElement(),        
        //    });
        //    db.SubmitChanges();
        //}

        private void Resave(Scenario scenario)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
        
            var source = db.ScenarioTable.Where(s => s.Id == scenario.Id.ToString()).Single();
            //   var scenarioXML = new ScenarioXML(scenario);

            source.Name = "";// scenario.Name;
            source.Description = "";// scenario.Description;

            source.XMLObjects = null;// scenarioXML.ToXElement();

            db.SubmitChanges();
        }

        private void Resave(BaseScenario scenario)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

            var source = db.ScenarioTable.Where(s => s.Id == scenario.Id.ToString()).Single();

            source.Name = scenario.Name;
            source.Description = scenario.Description;
            source.XMLObjects = scenario.ToXElement();

            db.SubmitChanges();
        }

        public override void Delete(Guid id)
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);

            var sc = db.ScenarioTable.Where(s => s.Id == id.ToString()).Single();

            if(sc.ScenarioUserTable != null)
                db.ScenarioUserTable.DeleteOnSubmit(sc.ScenarioUserTable);
            db.ScenarioTable.DeleteOnSubmit(sc);

            db.SubmitChanges();
        }

        public override IEnumerable<ScenarioHeader> AllScenaries()
        {
            dbPRDCTDataContext db = new dbPRDCTDataContext(MyData.ConnectionString);
            return db.ScenarioTable.Select(sc => 
            new ScenarioHeader
            {
                Id = Guid.Parse(sc.Id),
                Name = sc.Name,
                Description = sc.Description,
                Properties = new ScenarioPropertiesHeader()
                {
                    IsUser = sc.ScenarioUserTable != null,
                    Category = (sc.ScenarioUserTable != null) ? sc.ScenarioUserTable.Category : null                   
                }
            });
        }
    }
}
