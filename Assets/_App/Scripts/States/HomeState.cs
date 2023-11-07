using AxGrid.FSM;

[State(NamesEvent.HomeState)]
public sealed class HomeState : BaseState
{
    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.HomeState);
    }
}
