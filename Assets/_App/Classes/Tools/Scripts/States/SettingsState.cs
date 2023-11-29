using AxGrid.FSM;
using AxGrid.Model;

namespace ClassesTools
{
    [State(StateNames.SettingsState)]
    public class SettingsState : FSMState
    {
        [Enter]
        private void Enter()
        {
            Model.EventManager.Invoke(ProjectEvents.OnSettingsPanelActiveChanged, true);

            Model.Set(ButtonNames.BtnSettingsEnable, false);
            Model.Set(ButtonNames.BtnGameEnable, true);
            Model.Set(ButtonNames.BtnCollectionContentEnable, false);
        }

        [Bind]
        private void OnToggle(string toggleName)
        {
            switch (toggleName)
            {
                case ToggleNames.Music:
                    Model.Set(ToggleNames.OnToggleMusicClick, !Model.GetBool(ToggleNames.OnToggleMusicClick));
                    break;
                case ToggleNames.Sound:
                    Model.Set(ToggleNames.OnToggleSoundClick, !Model.GetBool(ToggleNames.OnToggleSoundClick));
                    break;
                case ToggleNames.Vibration:
                    Model.Set(ToggleNames.OnToggleVibrationClick, !Model.GetBool(ToggleNames.OnToggleVibrationClick));
                    break;
            }
        }

        [Bind]
        private void OnBtn(string buttonName)
        {
            switch (buttonName)
            {
                case ButtonNames.Game:
                    Parent.Change(StateNames.GameState);
                    break;
            }
        }
    }
}