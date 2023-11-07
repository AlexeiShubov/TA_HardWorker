using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(NamesEvent.NeutralState)]
public sealed class NeutralState : BaseState
{
    protected override void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.NeutralState);
        base.Enter();
    }

    [Bind(NamesEvent.ExitState)]
    [One(3f)]
    private void Exit()
    {
        var nextStateName = Model.Get<string>(NamesEvent.NextState);
        
        if (string.IsNullOrEmpty(nextStateName))
        {
            return;
        }

        Debug.Log($"Exit state {this}");
        GoToNextSTate(nextStateName);
    }

    protected override void GoToNextSTate(string nextStateNAme)
    {
        if (CheckDataForCorrectness(nextStateNAme))
        {
            base.GoToNextSTate(nextStateNAme);
        }
    }

    private bool CheckDataForCorrectness(string nextStateName)
    {
        return Model.Get<string>(NamesEvent.NeutralState) != nextStateName;
    }
}
