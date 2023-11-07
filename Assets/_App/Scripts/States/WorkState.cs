using AxGrid.FSM;

[State(NamesEvent.WorkState)]
public sealed class WorkState : BaseState
{
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.WorkState);
        Model.EventManager.Invoke(NamesEvent.WorkState);
        base.Enter();
    }
}
