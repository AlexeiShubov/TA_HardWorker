using AxGrid.FSM;
using AxGrid.Model;

[State(NamesEvent.NeutralState)]
public sealed class NeutralState : BaseState
{
    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.NeutralState);
    }
    
    [Bind(NamesEvent.FinishPath)]
    [One(1f)]
    private void Exit()
    {
        var nextStateName = Model.Get<string>(NamesEvent.NextState);

        if (string.IsNullOrEmpty(nextStateName))
        {
            GoToNextState(NamesEvent.NeutralState);
            
            return;
        }

        GoToNextState(nextStateName);
    }
}
