using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(StateNames.SettingsState)]
public class SettingsState : AbstractState
{
    [Enter]
    private void Enter()
    {
        EnableSettingsToggles(true);
        
        Model.Set(ButtonNames.OnBtnSettingsEnableChanged, false);
        Model.Set(ButtonNames.OnBtnGameEnableChanged, true);
        Model.Set(ButtonNames.OnBtnCollectionContentChanged, false);
        
        Debug.Log($"Enter to {StateNames.SettingsState}");
    }
    
    [Bind(ToggleNames.OnToggle)]
    private void OnToggle(string toggleName)
    {
        switch (toggleName)
        {
            case ToggleNames.MusicToggle:
                Model.Set(ToggleNames.OnToggleMusicClick, !Model.GetBool(ToggleNames.OnToggleMusicClick));
                break;
            case ToggleNames.SoundToggle:
                Model.Set(ToggleNames.OnToggleSoundClick, !Model.GetBool(ToggleNames.OnToggleSoundClick));
                break;
            case ToggleNames.VibrationToggle:
                Model.Set(ToggleNames.OnToggleVibrationClick, !Model.GetBool(ToggleNames.OnToggleVibrationClick));
                break;
        }
    }
    
    [Bind(ButtonNames.OnBtn)]
    private void OnSomeButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case ButtonNames.Game:
                Parent.Change(StateNames.GameState);
                break;
        }
    }
}
