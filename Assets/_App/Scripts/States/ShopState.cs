using AxGrid.FSM;

[State(NamesEvent.ShopState)]
public sealed class ShopState : BaseState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.ShopState);
        base.Enter();
    }
}
