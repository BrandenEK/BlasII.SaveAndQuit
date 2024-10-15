using MelonLoader;

namespace BlasII.SaveAndQuit;

internal class Main : MelonMod
{
    public static SaveAndQuit SaveAndQuit { get; private set; }

    public override void OnLateInitializeMelon()
    {
        SaveAndQuit = new SaveAndQuit();
    }
}