using HarmonyLib;
using Il2CppTGK.Game.Components.UI;

namespace BlasII.SaveAndQuit
{
    [HarmonyPatch(typeof(InventoryWindowLogic), nameof(InventoryWindowLogic.ChangeTab))]
    class Pause_Tab_Patch
    {
        public static void Postfix()
        {
            Main.SaveAndQuit.OnTabChange();
        }
    }
}
