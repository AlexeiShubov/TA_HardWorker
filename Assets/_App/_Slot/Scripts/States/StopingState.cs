using AxGrid.FSM;

[State(Keys.StopingState)]
public class StopingState : FSMState
{
    [One(2)]
    private void Exit()
    {
        Model.Set(Keys.OnStartButtonChanged, true);
        Parent.Change(Keys.IdleState);
    }
}