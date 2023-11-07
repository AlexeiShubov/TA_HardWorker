using AxGrid.FSM;

[State(NamesEvent.HomeState)]
public sealed class HomeState : BaseState
{
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.HomeState);
        Model.EventManager.Invoke(NamesEvent.HomeState);
        base.Enter();
    }
}
