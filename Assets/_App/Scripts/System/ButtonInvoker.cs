using AxGrid;
using AxGrid.Base;
using UnityEngine;

public sealed class ButtonInvoker : MonoBehaviourExt
{
    private UnityEngine.UI.Button Button;
    
    [SerializeField] private States _nextStateName;

    [OnAwake]
    public void Init()
    {
        Button = GetComponent<UnityEngine.UI.Button>();
        Button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        if (Settings.Fsm.CurrentStateName == _nextStateName.ToString())
        {
            return;
        }
        
        Settings.Fsm.Invoke(NamesEvent.ExitState, _nextStateName.ToString());
    }
}