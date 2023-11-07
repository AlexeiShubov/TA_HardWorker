using AxGrid.FSM;

[State(NamesEvent.ShopState)]
public sealed class ShopState : BaseState
{
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.EnterState, NamesEvent.ShopState);
        Model.EventManager.Invoke(NamesEvent.ShopState);
        base.Enter();
    }
}
