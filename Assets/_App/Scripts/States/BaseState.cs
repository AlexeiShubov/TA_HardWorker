using AxGrid.FSM;
using AxGrid.Model;

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
        Parent.Change(nextStateName);
    }

    private void WriteNextStateName(string nextStateName)
    {
        Model.Set(NamesEvent.NextState, nextStateName);
    }
}
