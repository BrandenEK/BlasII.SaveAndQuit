using BlasII.ModdingAPI;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.UI;
using Il2CppTMPro;
using UnityEngine;

namespace BlasII.SaveAndQuit
{
    public class SaveAndQuit : BlasIIMod
    {
        public SaveAndQuit() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        protected override void OnInitialize()
        {
            LogError($"{ModInfo.MOD_NAME} is initialized");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P) && false)
            {
                foreach (var menuButton in Object.FindObjectsOfType<UITextSelectable>(true))
                {
                    if (menuButton.name != "Exit To Main Menu")
                        continue;

                    LogWarning("Adding S&Q button to menu");

                    var saveButton = Object.Instantiate(menuButton, menuButton.transform.parent);
                    saveButton.name = "Save and Quit";
                    saveButton.transform.SetSiblingIndex(1);
                    foreach (var text in saveButton.GetComponentsInChildren<TMP_Text>())
                        text.text = "Save And Quit To Menu";

                    var buttonGroup = saveButton.transform.parent.GetComponent<UINavigableListGroup>();
                    var saveNavigation = saveButton.GetComponent<UINavigableControl>();

                    UINavigableGroup.UINavigableControlList list = buttonGroup.children;
                    list.Insert(0, saveNavigation);

                    try
                    {
                        saveNavigation.SetCommandHandlers(null);
                    }
                    catch (System.Exception)
                    {
                        Log("Removing input handlers from save button");
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F5) && LoadStatus.GameSceneLoaded)
            {
                SaveAndReturnToMenu();
            }
        }

        private void SaveAndReturnToMenu()
        {
            CoreCache.SaveData.SaveGame();
            CoreCache.SaveData.HideSavePopup();
            CoreCache.LoadSequenceManager.ReturnToMainMenu();
        }
    }
}
