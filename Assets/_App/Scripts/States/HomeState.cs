using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(NamesEvent.HomeState)]
public sealed class HomeState : FSMState
{
    private readonly SOGlobalSettings _soGlobalSettings;
    
    public HomeState(SOGlobalSettings soGlobalSettings)
    {
        _soGlobalSettings = soGlobalSettings;
    }
    
    [Enter]
    public void Enter()
    {
        Model.EventManager.Invoke(NamesEvent.HomeState);
        Debug.Log($"Enter state {this}");
    }

    [Bind(NamesEvent.ExitState)]
    public void Exit(string newStateName)
    {
        Parent.Change(newStateName);
        Debug.Log($"Exit state {this}");
    }
}
