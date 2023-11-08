using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

public abstract class BaseState : FSMState
{
    protected virtual void Enter()
    {
        UnBlockButtons();
    }
    
    [Bind(NamesEvent.OnClickSomeButton)]
    protected void OnChangeState(string nextStateName)
    {
        WriteNextStateName(nextStateName);
        GoToNextState(NamesEvent.NeutralState);
    }

    protected virtual void GoToNextState(string nextStateName)
    {
        Parent.Change(nextStateName);
    }

    private void UnBlockButtons()
    {
        var currentState = Settings.Fsm.CurrentStateName;
        
        Model.Set(NamesEvent.HomeButton, currentState != NamesEvent.HomeState);
        Model.Set(NamesEvent.WorkButton, currentState != NamesEvent.WorkState);
        Model.Set(NamesEvent.ShopButton, currentState != NamesEvent.ShopState);
    }

    private void WriteNextStateName(string nextStateName)
    {
        Model.Set(NamesEvent.NextState, nextStateName);
    }
}
