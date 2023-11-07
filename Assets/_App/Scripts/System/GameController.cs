using AxGrid.Base;
using AxGrid.Model;

public class GameController : MonoBehaviourExtBind
{
    private Bank _bank;
    private ViewController _viewController;

    public void Init(Bank bank, ViewController viewController)
    {
        _bank = bank;
        _viewController = viewController;
    }
    
    [Bind(NamesEvent.EnterState)]
    private void OnStateChanged(string nameState)
    {
        _bank.OnChangeState(nameState);
        _viewController.OnStateChanged(nameState);
    }
}
