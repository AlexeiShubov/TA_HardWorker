using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Path;
using UnityEngine;

public sealed class Initializer : MonoBehaviourExt
{
    [SerializeField] private SOGlobalSettings _soGlobalSettings;
    [SerializeField] private ViewController _viewController;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Character _character;
    
    private Bank _bank;
    
    [OnAwake]
    private void Init()
    {
        _bank = new Bank(this, Model, _soGlobalSettings);
        _viewController.Init(_character, _soGlobalSettings);
        _gameController.Init(_bank, _viewController);
        
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
