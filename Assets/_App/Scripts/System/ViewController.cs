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
    [Space(10)]
    [SerializeField] private Transform _shop;
    [SerializeField] private Transform _work;
    [SerializeField] private Transform _home;
    
    private Character _character;
    private SOGlobalSettings _soGlobalSettings;
    private Dictionary<string, Color> _colorsMap;

    public void Init(Character character, SOGlobalSettings soGlobalSettings)
    {
        Path = new CPath();
        
        _character = character;
        _soGlobalSettings = soGlobalSettings;
        _character.Init(new PathMove(Path, _soGlobalSettings));
        
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
        
        _character.Move(_home);
    }
    
    [Bind(NamesEvent.ShopButton)]
    private void ChangeShopButtonState()
    {
        Model.Set(NamesEvent.HomeButton, true);
        Model.Set(NamesEvent.WorkButton, true);
        Model.Set(NamesEvent.ShopButton, false);
        
        _character.Move(_shop);
    }
    
    [Bind(NamesEvent.WorkButton)]
    private void ChangeWorkButtonState()
    {
        Model.Set(NamesEvent.HomeButton, true);
        Model.Set(NamesEvent.WorkButton, false);
        Model.Set(NamesEvent.ShopButton, true);
        
        _character.Move(_work);
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
        Path.EasingLinear(0f, 0f, 1f, f => _backGround.color = Color.Lerp(_backGround.color, _colorsMap[nameState], f));
    }
}
