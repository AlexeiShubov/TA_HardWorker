using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.MovingState)]
public class MovingState : FSMState
{
    [One(3)]
    private void Enter()
    {
        Model.Set(Keys.OnStopButtonChanged, true);
    }

    [Bind(Keys.OnClickSomeButton)]
    private void Exit(string nameButton)
    {
        if (nameButton != Keys.StopButton) return;
        
        Model.Set(Keys.OnStartButtonChanged, false);
        Model.Set(Keys.OnStopButtonChanged, false);
        Parent.Change(Keys.StopingState);
    }
}
