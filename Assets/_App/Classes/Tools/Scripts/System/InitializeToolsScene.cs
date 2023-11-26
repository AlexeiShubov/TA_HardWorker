using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

public class InitializeToolsScene : MonoBehaviourExt
{
    [OnAwake]
    private void CustomAwake()
    {
        Settings.Fsm = new FSM();

        Settings.Fsm.Add(new SettingsState());
        Settings.Fsm.Add(new GameState());

        Settings.Fsm.Start(StateNames.SettingsState);
    }

    [OnUpdate]
    private void CustomUpdate()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }
}
