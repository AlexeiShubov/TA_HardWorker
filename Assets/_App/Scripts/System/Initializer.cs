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
        
        Settings.Fsm.Add(new NeutralState(_soGlobalSettings, this));
        Settings.Fsm.Add(new HomeState(_soGlobalSettings));
        Settings.Fsm.Add(new ShopState(_soGlobalSettings));
        Settings.Fsm.Add(new WorkState(_soGlobalSettings));
        
        Settings.Fsm.Start(_soGlobalSettings.DefaultStateName.ToString());
    }
}
