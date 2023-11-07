using System.Collections.Generic;
using AxGrid.Base;
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
        _backGround.color = _colorsMap[nameState];
    }
}
