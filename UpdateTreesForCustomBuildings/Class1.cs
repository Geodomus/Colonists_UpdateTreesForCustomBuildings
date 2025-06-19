using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
namespace UpdateTreesForCustomBuildings
{
    public class Class1
    {
        [StaticConstructorOnStartup]
        public static int Init()
        {
            //Harmony.DEBUG = true;

            var harmony = new Harmony("Update Idle Buildings when Tree is grown");

            harmony.PatchAll();

            return 2;
        }
        
        [HarmonyPatch(typeof(Player), "OnTreeGrown")]
        class Patch {
		
            static void Postfix(Player __instance)
            {
                var buildingsArray = new List<Building>();
                buildingsArray.AddRange( __instance.buildings.FindAll(building => (building.productionData.GetInfo() is BuildingProductionLumberjackDataInfo) && !(building.typeId == BuildingID.Lumberjack_Hut)));
                Debug.Log("buildings " + buildingsArray.Count);
                foreach (var building in buildingsArray.Where(building => building.IsIdle()))
                {
                    building.ForceIdleCheck();
                }
                /*int count = __instance.buildingsDict[(BuildingID)200].Count;
                while (count-- > 0)
                {
			/*
   			Make sure to edit the ID to match the Custom building ID you'd like to update
   			*/
                    if (__instance.buildingsDict[(BuildingID)200][count].IsIdle())
                        __instance.buildingsDict[(BuildingID)200][count].ForceIdleCheck();
                }*/
            }
        }
    }
}
