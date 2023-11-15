using AxGrid.FSM;
using UnityEngine;

[State(Keys.StoppingState)]
public class StoppingState : BaseFSMState
{
    [Enter]
    private void Enter()
    {
        Debug.Log("Enter StoppingState");
        Model.EventManager.Invoke(Keys.StoppingState);
    }

    [One(2)]
    private void Exit()
    {
        Debug.Log("Exit StoppingState");
        Model.Set(Keys.OnStartButtonChanged, true);
        Parent.Change(Keys.IdleState);
    }
}