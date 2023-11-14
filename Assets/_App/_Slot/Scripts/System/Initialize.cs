using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

public class Initialize : MonoBehaviourExt
{
    [SerializeField] private Block[] _blocks;
    
    [OnAwake]
    private void Init()
    {
        StatesInitialize();

        foreach (var block in _blocks)
        {
            block.Init(_blocks[^1].transform, _blocks[0].transform);
        }
    }

    [OnUpdate]
    private void UpdateFsm()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }

    private void StatesInitialize()
    {
        Settings.Fsm = new FSM();

        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new MovingState());
        Settings.Fsm.Add(new StopingState());

        Settings.Fsm.Start(Keys.IdleState);
    }
}
