using AxGrid.FSM;

public abstract class AbstractState : FSMState
{
    protected void EnableSettingsToggles(bool status)
    {
        Model.Set(ToggleNames.OnToggleMusicEnableChanged, status);
        Model.Set(ToggleNames.OnToggleSoundEnableChanged, status);
        Model.Set(ToggleNames.OnToggleVibrationEnableChanged, status);
    }
}
