using AxGrid.FSM;

[State(NamesEvent.ShopState)]
public sealed class ShopState : BaseState
{
    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.ShopState);
    }
}
