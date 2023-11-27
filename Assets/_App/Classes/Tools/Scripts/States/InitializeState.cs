using AxGrid.FSM;

[State(StateNames.InitializeState)]
public class InitializeState : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.Set(ProjectEvents.OnSettingsPanelActiveChanged, false);
        
        Model.Set(ToggleNames.OnToggleMusicClick, true);
        Model.Set(ToggleNames.OnToggleSoundClick, true);
        Model.Set(ToggleNames.OnToggleVibrationClick, true);
        
        Parent.Change(StateNames.GameState);
    }
}
