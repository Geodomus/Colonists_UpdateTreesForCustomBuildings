using System.Collections.Generic;
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
                int count = __instance.buildingsDict[(BuildingID)200].Count;
                while (count-- > 0)
                {
			/*
   			Make sure to edit the ID to match the Custom building ID you'd like to update
   			*/
                    if (__instance.buildingsDict[(BuildingID)200][count].IsIdle())
                        __instance.buildingsDict[(BuildingID)200][count].ForceIdleCheck();
                }
            }
        }
    }
}
