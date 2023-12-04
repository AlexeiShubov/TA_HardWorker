using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace UniRxTask
{
    public class UniRxSceneInitialize : MonoBehaviourExtBind
    {
        [SerializeField] private CollectionsViewController _collectionsViewController;
        
        [OnAwake]
        private void CustomAwake()
        {
            Settings.Fsm = new FSM();

            Settings.Fsm.Add(new GameState(_collectionsViewController));
        }

        [OnStart]
        private void CustomStart()
        {
            Settings.Fsm.Start("GameState");
        }
    }
}
