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
        
        Settings.Fsm.Add(new TogglesReactionState());
        Settings.Fsm.Add(new TogglesDisableReactionState());
        
        Settings.Fsm.Start(StateNames.ToggleReactionState);
    }

    [OnUpdate]
    private void CustomUpdate()
    {
        Settings.Fsm.Update(Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            var nextStateName 
                = Settings.Fsm.CurrentStateName == StateNames.ToggleReactionState 
                ? StateNames.ToggleDisableReactionState 
                : StateNames.ToggleReactionState;
            
            Settings.Fsm.Change(nextStateName);
        }
    }
}
