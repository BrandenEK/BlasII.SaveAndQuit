using HarmonyLib;
using Il2CppTGK.Game.Components.UI;

namespace BlasII.SaveAndQuit;

/// <summary>
/// Call event when menu tab changes
/// </summary>
[HarmonyPatch(typeof(InventoryWindowLogic), nameof(InventoryWindowLogic.ChangeTab))]
class InventoryWindowLogic_ChangeTab_Patch
{
    public static void Postfix()
    {
        Main.SaveAndQuit.OnTabChange();
    }
}
