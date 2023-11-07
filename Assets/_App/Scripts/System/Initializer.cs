using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

public sealed class Initializer : MonoBehaviourExt
{
    [SerializeField] private SOGlobalSettings _soGlobalSettings;
    
    private Bank _bank;
    
    [OnStart]
    private void Init()
    {
        _bank = new Bank(this, Model, _soGlobalSettings);
        
        StatesInitialize();
    }
    
    [OnUpdate]
    public void UpdateFsm()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }
    
    private void StatesInitialize()
    {
        Settings.Fsm = new FSM();
        
        Settings.Fsm.Add(new NeutralState());
        Settings.Fsm.Add(new HomeState());
        Settings.Fsm.Add(new ShopState());
        Settings.Fsm.Add(new WorkState());
        
        Settings.Fsm.Start(_soGlobalSettings.DefaultStateName.ToString());
    }
}
