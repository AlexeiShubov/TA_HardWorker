using AxGrid.FSM;

[State(NamesEvent.WorkState)]
public sealed class WorkState : BaseState
{
    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.WorkState);
    }
}
