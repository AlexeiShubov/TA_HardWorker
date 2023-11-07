using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

public abstract class BaseState : FSMState
{
    [Bind("OnBtn")]
    protected void OnChangeState(string nextStateName)
    {
        WriteNextStateName(nextStateName);
        GoToNextState(NamesEvent.NeutralState);
    }
    
    protected virtual void GoToNextState(string nextStateName)
    {
        Debug.Log($"Exit state {this}");
        Parent.Change(nextStateName);
    }

    private void WriteNextStateName(string nextStateName)
    {
        Model.Set(NamesEvent.NextState, nextStateName);
    }
}
