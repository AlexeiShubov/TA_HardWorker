using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(Keys.MovingState)]
public class MovingState : BaseFSMState
{
    [Enter]
    private void Enter()
    {
        Debug.Log("Enter MovingState");
        Model.EventManager.Invoke(Keys.MovingState);
    }

    [Bind(Keys.OnClickSomeButton)]
    private void Exit(string nameButton)
    {
        Debug.Log("Exit MovingState");
        if (nameButton != Keys.StopButton) return;
        
        Model.Set(Keys.OnStartButtonChanged, false);
        Model.Set(Keys.OnStopButtonChanged, false);
        Parent.Change(Keys.StoppingState);
    }
    
    [One(1)]
    private void EnableButton()
    {
        Model.Set(Keys.OnStopButtonChanged, true);
    }
}
