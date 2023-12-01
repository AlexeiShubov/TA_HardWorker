using AxGrid.FSM;
using AxGrid.Model;

namespace UniRxTask
{
    [State("GameState")]
    public class GameState : FSMState
    {
        [Enter]
        private void Enter()
        {
            
        }

        [Bind]
        private void OnBtn(string buttonName)
        {
            if (buttonName == "AddNewCardButton")
            {
                Model.EventManager.Invoke("CreateNewCard");
            }
        }
    }
}
