using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(NamesEvent.ShopState)]
public sealed class ShopState : FSMState
{
    private readonly SOGlobalSettings _soGlobalSettings;

    public ShopState(SOGlobalSettings soGlobalSettings)
    {
        _soGlobalSettings = soGlobalSettings;
    }

    [Enter]
    public void Enter()
    {
        Model.EventManager.Invoke($"{NamesEvent.ShopState}");
        Debug.Log($"Enter state {this}");
    }

    [Bind(NamesEvent.ExitState)]
    public void Exit(string newStateName)
    {
        Parent.Change(newStateName);
        Debug.Log($"Exit state {this}");
    }
}
