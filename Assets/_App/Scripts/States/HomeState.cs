using AxGrid.FSM;

[State(NamesEvent.HomeState)]
public sealed class HomeState : BaseState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.HomeState);
        base.Enter();
    }
}
