using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;

public class InitializeToolsScene : MonoBehaviourExt
{
    [OnAwake]
    private void CustomAwake()
    {
        Settings.Fsm = new FSM();
        
        Settings.Fsm.Add(new TogglesReactionState());
        
        Settings.Fsm.Start(StateNames.ToggleReactionState);
    }
}
