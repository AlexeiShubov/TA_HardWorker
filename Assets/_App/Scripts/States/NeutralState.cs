using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(NamesEvent.NeutralState)]
public sealed class NeutralState : FSMState
{
    private readonly SOGlobalSettings _soGlobalSettings;
    private readonly MonoBehaviourExt _monoBehaviourExt;

    private Coroutine _currentCoroutine;

    public NeutralState(SOGlobalSettings soGlobalSettings, MonoBehaviourExt monoBehaviourExt)
    {
        _soGlobalSettings = soGlobalSettings;
        _monoBehaviourExt = monoBehaviourExt;
    }
    
    [Enter]
    public void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.NeutralState);
        Debug.Log($"Enter state {this}");
    }

    [Bind(NamesEvent.ExitState)]
    public void Exit(string newStateName)
    {
        Parent.Change(newStateName);
        Debug.Log($"Exit state {this}");
    }
}
