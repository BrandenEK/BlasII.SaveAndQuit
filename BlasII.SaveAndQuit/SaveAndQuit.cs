using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Helpers;
using BlasII.ModdingAPI.Input;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.UI;
using UnityEngine;
using UnityEngine.Events;

namespace BlasII.SaveAndQuit;

/// <summary>
/// Allows the player to save and quit from anywhere
/// </summary>
public sealed class SaveAndQuit : BlasIIMod
{
    internal SaveAndQuit() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private UINavigableControl _saveButton;
    private bool _focused;

    /// <summary>
    /// Initialize handlers
    /// </summary>
    protected override void OnInitialize()
    {
        LocalizationHandler.RegisterDefaultLanguage("en");
    }

    /// <summary>
    /// Process UI input
    /// </summary>
    protected override void OnUpdate()
    {
        if (InputHandler.GetButtonDown(ButtonType.UIConfirm) && _focused && _saveButton != null && _saveButton.gameObject.activeInHierarchy)
        {
            SaveAndReturnToMenu();
        }
    }

    /// <summary>
    /// Unfocus the button
    /// </summary>
    protected override void OnSceneLoaded(string _)
    {
        _focused = false;
    }

    /// <summary>
    /// Perform the save and quit functionality
    /// </summary>
    private void SaveAndReturnToMenu()
    {
        CoreCache.SaveData.SaveGame();
        CoreCache.SaveData.HideSavePopup();
        CoreCache.LoadSequenceManager.ReturnToMainMenu();
        AudioHelper.PlayEffectUI(AudioHelper.SfxUI.OpenMenu);
    }

    /// <summary>
    /// Create save button if it doesn't exist yet
    /// </summary>
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

    /// <summary>
    /// Find and create the UI for the button
    /// </summary>
    private void CreateSaveButton(UITextSelectable exitButton)
    {
        ModLog.Info("Adding S&Q button to menu");

        // Create object
        _saveButton = Object.Instantiate(exitButton, exitButton.transform.parent).GetComponent<UINavigableControl>();
        _saveButton.name = "Save and Quit";
        _saveButton.transform.SetSiblingIndex(1);

        // Set text
        UIPixelTextWithShadow text = _saveButton.GetComponentInChildren<UIPixelTextWithShadow>();
        LocalizationHandler.AddPixelTextLocalizer(text, "text");
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
