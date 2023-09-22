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

        protected override void OnSceneLoaded(string sceneName)
        {
        }

        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P) && false)
            {
                //var widget = Object.FindObjectOfType<OptionsWidget>();
                //if (widget == null)
                //    return;

                //foreach (var text in Object.FindObjectsOfType<TMP_Text>())
                //{
                //    LogWarning(text.text);
                //    if (text.text.ToLower().Contains("main menu"))
                //        Log(text.transform.parent.parent.parent.transform.DisplayHierarchy(5, true));

                //}

                foreach (var menuButton in Object.FindObjectsOfType<UITextSelectable>(true))
                {
                    if (menuButton.name != "Exit To Main Menu")
                        continue;
                    menuButton.transform.position += Vector3.down * 100;

                    LogWarning("found main menu button");
                    var saveButton = Object.Instantiate(menuButton, menuButton.transform.parent);
                    saveButton.name = "Save and Quit";
                    saveButton.transform.position += Vector3.down * 150;
                    saveButton.transform.SetSiblingIndex(1);
                    foreach (var text in saveButton.GetComponentsInChildren<TMP_Text>())
                        text.text = "Save And Quit To Menu";

                    //LogError(saveButton.transform.DisplayHierarchy(4, true));

                    var buttonGroup = saveButton.transform.parent.GetComponent<UINavigableListGroup>();

                    //for (int i = 0; i < buttonGroup.transform.childCount; i++)
                    //{
                    //    Log(buttonGroup.transform.GetChild(i).position.ToString());
                    //}

                    var saveComonent = saveButton.GetComponent<UINavigableControl>();
                    //saveComonent.defaultHandler = new UINavigableControl.DefaultCommandHandler();
                    //saveComonent.defaultHandler.handler.callback.

                    UINavigableGroup.UINavigableControlList list = buttonGroup.children;
                    //list.Insert(1, saveComonent);
                    list.Insert(0, saveComonent);
                    try
                    {
                        saveComonent.SetCommandHandlers(null);
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
