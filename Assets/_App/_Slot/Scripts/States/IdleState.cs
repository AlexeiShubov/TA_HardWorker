using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(Keys.IdleState)]
public class IdleState : BaseFSMState
{
    [Enter]
    private void Enter()
    {
        Debug.Log("Enter IdleState");
        Model.EventManager.Invoke(Keys.IdleState);
    }
    
    [Bind(Keys.OnClickSomeButton)]
    private void Exit(string nameButton)
    {
        Debug.Log("Exit IdleState");
        if (nameButton != Keys.StartButton) return;

        Model.Set(Keys.OnStartButtonChanged, false);
        Parent.Change(Keys.MovingState);
    }
}
