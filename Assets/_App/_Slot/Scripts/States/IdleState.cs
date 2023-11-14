using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.IdleState)]
public class IdleState : FSMState
{
    [Bind(Keys.OnClickSomeButton)]
    private void Exit(string nameButton)
    {
        if (nameButton != Keys.StartButton) return;

        Model.Set(Keys.OnStartButtonChanged, false);
        Parent.Change(Keys.MovingState);
    }
}
