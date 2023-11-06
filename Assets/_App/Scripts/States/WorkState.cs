using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(NamesEvent.WorkState)]
public sealed class WorkState : FSMState
{
    private readonly SOGlobalSettings _soGlobalSettings;

    public WorkState(SOGlobalSettings soGlobalSettings)
    {
        _soGlobalSettings = soGlobalSettings;
    }
    
    [Enter]
    public void Enter()
    {
        Model.EventManager.Invoke($"{NamesEvent.WorkState}");
        Debug.Log($"Enter state {this}");
    }

    [Bind(NamesEvent.ExitState)]
    public void Exit(string newStateName)
    {
        Parent.Change(newStateName);
        Debug.Log($"Exit state {this}");
    }
}
