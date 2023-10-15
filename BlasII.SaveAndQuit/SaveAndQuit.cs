using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Audio;
using Il2CppI2.Loc;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.UI;
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

        protected override void OnSceneLoaded(string _)
        {
            _focused = false;
        }

        private void SaveAndReturnToMenu()
        {
            CoreCache.SaveData.SaveGame();
            CoreCache.SaveData.HideSavePopup();
            CoreCache.LoadSequenceManager.ReturnToMainMenu();
            AudioHandler.PlayEffectUI(UISFX.OpenMenu);
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

            // Create object
            _saveButton = Object.Instantiate(exitButton, exitButton.transform.parent).GetComponent<UINavigableControl>();
            _saveButton.name = "Save and Quit";
            _saveButton.transform.SetSiblingIndex(1);

            // Set text
            UIPixelTextWithShadow text = _saveButton.GetComponentInChildren<UIPixelTextWithShadow>();
            Object.Destroy(text.GetComponent<Localize>());
            text.SetText("Save and Quit to Menu");
            text.SetColor(Color.white);

            // Add button to selectable list
            var buttonGroup = _saveButton.transform.parent.GetComponent<UINavigableListGroup>();
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
