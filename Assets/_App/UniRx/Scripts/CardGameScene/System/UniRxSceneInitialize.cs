using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;

namespace UniRxTask
{
    public class UniRxSceneInitialize : MonoBehaviourExtBind
    {
        [OnAwake]
        private void CustomAwake()
        {
            Settings.Fsm = new FSM();

            Settings.Fsm.Add(new GameState());
        }

        [OnStart]
        private void CustomStart()
        {
            Settings.Fsm.Start("GameState");
        }
    }
}
