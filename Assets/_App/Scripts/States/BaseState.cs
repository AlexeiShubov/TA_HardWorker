using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

public abstract class BaseState : FSMState
{
    [Enter]
    protected virtual void Enter()
    {
        Debug.Log($"Enter state {this}");
    }
    
    [Bind("OnBtn")]
    protected void OnChangeState(string nextStateName)
    {
        WriteNextStateName(nextStateName);
        GoToNextSTate(NamesEvent.NeutralState);
    }
    
    protected virtual void GoToNextSTate(string nextStateName)
    {
        Debug.Log($"Exit state {this}");
        Parent.Change(nextStateName);
    }

    private void WriteNextStateName(string nextStateName)
    {
        Model.Set(NamesEvent.NextState, nextStateName);
    }
}
