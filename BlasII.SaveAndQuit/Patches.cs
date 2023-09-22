using HarmonyLib;
using Il2CppTGK.Game.Components.UI;

namespace BlasII.SaveAndQuit
{
    //[HarmonyPatch(typeof(OptionsWidget), nameof(OptionsWidget.LoadMainMenu))]
    //class Options_QuitMenu_Patch
    //{
    //    public static void Prefix()
    //    {
    //        Main.SaveAndQuit.LogWarning("Quit to menu!");
    //    }
    //}

    //[HarmonyPatch(typeof(SavingPopupWindowLogic), nameof(SavingPopupWindowLogic.OnClose))]
    //class Save_Open_Patch
    //{
    //    public static void Postfix()
    //    {

    //    }
    //}

    //[HarmonyPatch(typeof(UINavigableControl), nameof(UINavigableControl.HandleCommand))]
    //class Navigable_Command_Patch
    //{
    //    public static void Postfix(UINavigableControl __instance)
    //    {
    //        Main.SaveAndQuit.LogWarning("Command for : " + __instance.name);
    //    }
    //}

}
