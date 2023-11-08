using AxGrid.FSM;

[State(NamesEvent.WorkState)]
public sealed class WorkState : BaseState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.WorkState);
        base.Enter();
    }
}
