using AxGrid.FSM;
using AxGrid.Model;

[State(StateNames.ToggleReactionState)]
public class TogglesReactionState : FSMState
{
    [Bind(ToggleNames.OnToggle)]
    private void OnToggle(string toggleName)
    {
        switch (toggleName)
        {
            case ToggleNames.MusicToggle:
                Model.Set(ToggleNames.MusicToggleClick, !Model.Get<bool>(ToggleNames.MusicToggleClick));
                break;
            case ToggleNames.SoundToggle:
                Model.Set(ToggleNames.SoundToggleClick, !Model.Get<bool>(ToggleNames.SoundToggleClick));
                break;
            case ToggleNames.VibrationToggle:
                Model.Set(ToggleNames.VibrationToggleClick, !Model.Get<bool>(ToggleNames.VibrationToggleClick));
                break;
        }
    }
}
