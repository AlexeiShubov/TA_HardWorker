using AxGrid.FSM;
using AxGrid.Model;

[State(NamesEvent.NeutralState)]
public sealed class NeutralState : BaseState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.NeutralState);
        Model.Set(NamesEvent.Defaulter, null);
        base.Enter();
    }
    
    [Bind(NamesEvent.FinishPath)]
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
