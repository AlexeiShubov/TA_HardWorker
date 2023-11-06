using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class TextBinder : MonoBehaviourExtBind
{
    private UnityEngine.UI.Text _text;

    [OnAwake]
    public void Init()
    {
        _text = GetComponent<UnityEngine.UI.Text>();
    }

    [Bind("OnCurrencyChanged")]
    public void OnValueChanged(string value)
    {
        _text.text = $"{NamesEvent.Currency}: {value}";
    }    
}