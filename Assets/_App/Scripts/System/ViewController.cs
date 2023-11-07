using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviourExtBind
{
    [SerializeField] private Image _backGround;
    [Space(10)] 
    [SerializeField] private List<States> _states;
    [SerializeField] private List<Color> _stateColors;

    private Dictionary<string, Color> _colorsMap;

    public void Init()
    {
        ColorsMapInitialize();
    }
    
    public void OnStateChanged(string nameState)
    {
        ChangeColor(nameState);
    }

    [Bind(NamesEvent.HomeButton)]
    private void ChangeHomeButtonState()
    {
        Model.Set(NamesEvent.HomeButton, false);
        Model.Set(NamesEvent.WorkButton, true);
        Model.Set(NamesEvent.ShopButton, true);
    }
    
    [Bind(NamesEvent.ShopButton)]
    private void ChangeShopButtonState()
    {
        Model.Set(NamesEvent.HomeButton, true);
        Model.Set(NamesEvent.WorkButton, true);
        Model.Set(NamesEvent.ShopButton, false);
    }
    
    [Bind(NamesEvent.WorkButton)]
    private void ChangeWorkButtonState()
    {
        Model.Set(NamesEvent.HomeButton, true);
        Model.Set(NamesEvent.WorkButton, false);
        Model.Set(NamesEvent.ShopButton, true);
    }

    private void ColorsMapInitialize()
    {
        if (_states.Count != _stateColors.Count)
        {
            Debug.LogError("InCorrected ColorMap or StatesMap");
            
            return;
        }
        
        _colorsMap = new Dictionary<string, Color>();
        
        for (var i = 0; i < _states.Count; i++)
        {
            _colorsMap.Add(_states[i].ToString(), _stateColors[i]);
        }
    }

    private void ChangeColor(string nameState)
    {
        Path = new CPath();

        Path.EasingLinear(0f, 0f, 1f, f => _backGround.color = Color.Lerp(_backGround.color, _colorsMap[nameState], f));
    }
}
