using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(StateNames.ToggleReactionState)]
public class TogglesReactionState : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.Set(ToggleNames.OnToggleMusicEnableChanged, true);
        Model.Set(ToggleNames.OnToggleSoundEnableChanged, true);
        Model.Set(ToggleNames.OnToggleVibrationEnableChanged, true);
        
        Debug.LogError($"Enter to {StateNames.ToggleReactionState}");
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
}
