using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

public class Initialize : MonoBehaviourExt
{
    [OnAwake]
    private void Init()
    {
        StatesInitialize();
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
