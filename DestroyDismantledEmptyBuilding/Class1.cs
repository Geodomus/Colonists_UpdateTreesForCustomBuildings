using HarmonyLib;
using UnityEngine;
namespace DestroyDismantledEmptyBuilding
{
    public class Class1
    {
        [StaticConstructorOnStartup]
        public static int Init()
        {
            Harmony.DEBUG = true;

            var harmony = new Harmony("Destroy Completely Dismantled and Empty Building");

            harmony.PatchAll();

            return 2;
        }
        
        [HarmonyPatch(typeof(Unit), "OnEnterIdle")]
        class Patch {
		
            static void Postfix(Unit __instance)
            {
                if (__instance.workerBuilding != null)
                {
                    if (IsDeconstructedAndStorageEmpty(__instance.workerBuilding))
                    {
                        Game.instance.DestroyBuilding(__instance.workerBuilding);
                    }
                }
            }

            private static bool IsDeconstructedAndStorageEmpty(Building building)
            {
                return building.deconstructionData != null &&
                       building.IsUnderDeconstruction() &&
                       building.deconstructionData.IsComplete() &&
                       building.constructionIncomingStorageData != null &&
                       building.constructionIncomingStorageData.IsEmpty() &&
                       IsStorageDataEmptyOrNull(building.outgoingStorageData) &&
                       IsStorageDataEmptyOrNull(building.productionStorageData) &&
                       IsStorageDataEmptyOrNull(building.productionIncomingStorageData) &&
                       IsStorageDataEmptyOrNull(building.constructionStorageData) &&
                       IsStorageDataEmptyOrNull(building.harbourExpeditionStorageData) &&
                       IsStorageDataEmptyOrNull(building.incomingStorageData) &&
                       IsStorageDataEmptyOrNull(building.specialStorageData) &&
                       IsStorageDataEmptyOrNull(building.tradeSaleStorageData) &&
                       IsStorageDataEmptyOrNull(building.tradePurchaseStorageData) &&
                       IsStorageDataEmptyOrNull(building.transportBufferStorageData) &&
                       IsStorageDataEmptyOrNull(building.transportIncomingStorageData) &&
                       IsStorageDataEmptyOrNull(building.transportOutgoingStorageData);
            }

            private static bool IsStorageDataEmptyOrNull(BuildingStorageData buildingStorageData)
            {
                return buildingStorageData == null || buildingStorageData.IsEmpty();
            }
        }
    }
}