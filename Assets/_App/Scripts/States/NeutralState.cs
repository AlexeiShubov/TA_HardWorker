using AxGrid.FSM;
using AxGrid.Model;

[State(NamesEvent.NeutralState)]
public sealed class NeutralState : BaseState
{
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.NeutralState);
        Model.EventManager.Invoke(NamesEvent.NeutralState);
        base.Enter();
    }

    [Bind(NamesEvent.ExitState)]
    [One(2f)]
    private void Exit()
    {
        var nextStateName = Model.Get<string>(NamesEvent.NextState);

        if (string.IsNullOrEmpty(nextStateName))
        {
            GoToNextSTate(NamesEvent.NeutralState);
            
            return;
        }

        GoToNextSTate(nextStateName);
    }
}
