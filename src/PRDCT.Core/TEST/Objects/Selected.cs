namespace PRDCT.Core.TEST.Objects
{
    //    public static class Selected  // some procedures when object is choiced and fixed
    //    {

    //        static char chFixObject;
    //        static string stFixObject;

    //        //UnGloVar , // for BooFixObject
    //        //UnPlaceO , // for global var OBoxInform that chosen
    //        //PUnCRead , // for procedure ToGetStarFromHeap
    //        //UnForSta , // for procedure CentreWithFixStar
    //        //UnForPos , // for procedure CentreWithFixPlanet
    //        //UnForSat , // for procedure CentreWithFixSatellite
    //        //UnitEilP ; // for proc ElemDefault

    //        public static void InitForFixCase()
    //        { // no objectFix
    //            chFixObject = ' ';
    //            Global.BooFixObject = false;
    //            stFixObject = "";
    //            Global.MapSatNumber = 0;    // UnGloVar no satellites on the map
    //            Global.BooMapPoint = false; // UnGloVar no selected points on the map
    //        }

    //        public static void ToDefineFixStar()
    //        { // stars in memory in StarAllHeap all variables unit UnCoTVar
    //            int ns = ObjectTopHeap ^[OBoxInform].nall; // number for star in  StarAllHeap^[]
    //            StarOneFix = StarAllHeap ^[ns]; // inform from memory to record StarOneFix
    //        }

    //        public static void ToDefineFixPlanet()
    //        {  // number of planet that chosen to fix
    //            PlanetNumFix = ObjectTopHeap ^[OBoxInform].nall;
    //        }

    //        public static void ToDefineFixSatellite()
    //{
    // int ns = ObjectTopHeap^[OBoxInform].nall; // number of satellite in SatElemHeap^
    //  SatElemFix = SatElemHeap^[ns]; // record for satellite that chosen to fix
    //}

    //        public static void ToDefineFixObject() // UnitFixO for ObjectTopHeap^[OBoxInform]
    //        { // OBoxInform number in ObjectTopHeap^[] for object that choiced
    //            chFixObject = ObjectTopHeap ^[OBoxInform].chps;  // char chps for object
    //            stFixObject = ObjectTopHeap ^[OBoxInform].name;  // name for object
    //            switch (chFixObject)
    //            {
    //           case '*' : ToDefineFixStar();break;
    //           case 'P' : ToDefineFixPlanet(); break;
    //                case 'S' : ToDefineFixSatellite(); break;
    //            } // case char chps to define object
    //        }

    //        public static bool ToGetCentrePos() // for AzimutC AltitudeC values
    //        {  // BooFixObject from UnGloVar      
    //            if ((CharForView = 'S') && (!BooFixObject))
    //            // 'S' for sky option but not for 'L' look
    //            {
    //                if (AltitudeC < RinDegC - 1.0)
    //                    AltitudeC:= RinDegC - 1.0;
    //                return false; // no change in centre horizon position
    //            }
    //            switch (chFixObject)
    //            {
    //                case '*': return CentreWithFixStar;      // unit UnForSta
    //                case 'P': return CentreWithFixPlanet;    // unit UnForPos
    //                case 'S': return CentreWithFixSatellite; // unit UnForSat
    //            } // case char chps to define object
    //        }

    //        public static void ToGetFixSatellites; // if it is possible to fix satellites
    //        var                            // to project on the map world
    //          nus  : Byte ;
    //  stn  : ShortString ;
    //  boo  : Boolean ;             // a try to use any satellites
    //begin
    //  boo:=False;
    //  if  BooFixObject and(chFixObject = 'S')
    //    then // there is our satellite with the elements in SatElemFix
    //      begin
    //        boo:=True;
    //        MapSatNumber:=1; // the satellite that fixed
    //        SatElemMap[MapSatNumber]:=SatElemFix; // elements record UnGloVar
    //        stFixObject:=SatElemFix.satname; // name of the satellite that fixed
    //      end
    //    else
    //      if  NumSatElemInHeap > 0
    //        then  // information about the satellite is in heap
    //          begin // OBoxInform declared in UnPlaceO
    //            boo:=True;
    //            if  NumSatElemInHeap > 47
    //              then // random numbers for satellites
    //                begin
    //                  MapSatNumber:=8; // UnGloVar
    //                  for nus:=1 To MapSatNumber do
    //                    begin
    //                      OBoxInform:=NumFirstSatHeap+Random(NumSatElemInHeap);
    //        ToDefineFixObject;  // the random satellite from the list
    //                      SatElemMap[nus]:=SatElemFix; // elements record UnGloVar
    //                    end;
    //                end
    //              else
    //                begin
    //                  MapSatNumber:=8;
    //                  if  NumSatElemInHeap<MapSatNumber
    //                    then
    //                      MapSatNumber:=NumSatElemInHeap;
    //                  for nus:=1 To MapSatNumber do
    //                    begin
    //                      OBoxInform:=NumFirstSatHeap+nus-1;
    //                      ToDefineFixObject;  // the random satellite from the list
    //                      SatElemMap[nus]:=SatElemFix; // elements record UnGloVar
    //                    end;
    //                end;
    //          end;
    //  boo:= boo and(JulianDate > 2436147.0 ); // 5 october 1957 year
    //        BooFixObject:= boo ; // yes or no objects on teh screen
    //  if  boo
    //    then
    //      begin
    //        NumStepValue:=21;
    //        StepWithTime:=10.0/1440.0; // 10 minute in day
    //      end
    //    else
    //      InitForFixCase;
    //  Str(MapSatNumber:2, stn);
    //        stFixObject:='спутников:'+stn;
    //end;

    //public static void ToGetRandomSat(var el  : TElemRec ); // type UnCoTVar
    //        begin
    //  if NumSatElemInHeap< 1
    //    then
    //      ElemDefault(el) // no satellite
    //    else
    //      begin // random number
    //        OBoxInform:=NumFirstSatHeap+Random(NumSatElemInHeap); // number
    //        el:=SatElemHeap^[ObjectTopHeap^[OBoxInform].nall]; // record
    //      end;
    //end;

    //end.


    //    }
}
