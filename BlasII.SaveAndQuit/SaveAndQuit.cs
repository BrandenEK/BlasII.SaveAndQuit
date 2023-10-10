using BlasII.ModdingAPI;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.UI;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BlasII.SaveAndQuit
{
    public class SaveAndQuit : BlasIIMod
    {
        public SaveAndQuit() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        private UINavigableControl _saveButton;
        private bool _focused;

        protected override void OnUpdate()
        {
            if (CoreCache.Input.GetButtonDown("UI Confirm") && _focused && _saveButton != null && _saveButton.gameObject.activeInHierarchy)
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

        public void OnTabChange()
        {
            if (_saveButton != null)
                return;

            // Need to create the save button
            foreach (var menuButton in Object.FindObjectsOfType<UITextSelectable>())
            {
                if (menuButton.name == "Exit To Main Menu")
                {
                    CreateSaveButton(menuButton);
                    return;
                }
            }
        }

        private void CreateSaveButton(UITextSelectable exitButton)
        {
            Log("Adding S&Q button to menu");

            // Create object and change text
            var saveButton = Object.Instantiate(exitButton, exitButton.transform.parent);
            saveButton.name = "Save and Quit";
            saveButton.transform.SetSiblingIndex(1);
            foreach (var text in saveButton.GetComponentsInChildren<TMP_Text>())
                text.text = "Save And Quit To Menu";

            _saveButton = saveButton.GetComponent<UINavigableControl>();

            // Add button to selectable list
            var buttonGroup = saveButton.transform.parent.GetComponent<UINavigableListGroup>();
            buttonGroup.children.Insert(2, _saveButton);

            // Remove old and add new event handlers
            try
            {
                _saveButton.SetCommandHandlers(null);
            }
            catch (System.Exception)
            {

            }
            _saveButton.OnFocus.AddListener((UnityAction)FocusButton);
            _saveButton.OnFocusLost.AddListener((UnityAction)UnfocusButton);
        }

        private void FocusButton() => _focused = true;
        private void UnfocusButton() => _focused = false;
    }
}
