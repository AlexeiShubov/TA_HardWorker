using AxGrid.FSM;
using UnityEngine;

[State(StateNames.ToggleDisableReactionState)]
public class TogglesDisableReactionState : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.Set(ToggleNames.OnToggleMusicEnableChanged, false);
        Model.Set(ToggleNames.OnToggleSoundEnableChanged, false);
        Model.Set(ToggleNames.OnToggleVibrationEnableChanged, false);
        
        Debug.LogError($"Enter to {StateNames.ToggleDisableReactionState}");
    }
}
