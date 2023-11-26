using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(StateNames.GameState)]
public class GameState : AbstractState
{
    [Enter]
    private void Enter()
    {
        EnableSettingsToggles(false);

        Model.Set(ButtonNames.OnBtnGameEnableChanged, false);
        Model.Set(ButtonNames.OnBtnSettingsEnableChanged, true);
        
        Debug.LogError($"Enter to {StateNames.GameState}");
    }

    [Bind(ButtonNames.OnBtn)]
    private void OnSomeButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case ButtonNames.Settings:
                Parent.Change(StateNames.SettingsState);
                break;
        }
    }
}
