using System.Collections;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public sealed class Bank
{
    private readonly SOGlobalSettings _soGlobalSettings;
    private readonly MonoBehaviourExt _monoBehaviourExt;
    private readonly DynamicModel _model;
    private readonly Currency _currency;

    private Coroutine _currentCoroutine;

    public Bank(MonoBehaviourExt monoBehaviourExt, DynamicModel model, SOGlobalSettings soGlobalSettings)
    {
        _monoBehaviourExt = monoBehaviourExt;
        _model = model;
        _soGlobalSettings = soGlobalSettings;
        
        _currency = new Currency(_model, _soGlobalSettings);
        _model.Set(NamesEvent.Currency, _currency.CurrentAmount);
    }

    public void OnChangeState(string nameState)
    {
        switch (nameState)
        {
            case NamesEvent.NeutralState:
                OnNeutralState();
                break;
            case NamesEvent.HomeState:
                OnHomeState();
                break;
            case NamesEvent.WorkState:
                OnWorkState();
                break;
            case NamesEvent.ShopState:
                OnShopState();
                break;
        }
    }

    private void OnNeutralState()
    {
        StopCurrentCoroutine();
    }

    private void OnHomeState()
    {
        StopCurrentCoroutine();
    }

    private void OnShopState()
    {
        StartCoroutine(StartSpendCurrency());
    }

    private void OnWorkState()
    {
        StartCoroutine(StartAddCurrency());
    }

    private void StartCoroutine(IEnumerator coroutine)
    {
        StopCurrentCoroutine();
        
        _currentCoroutine = _monoBehaviourExt.StartCoroutine(coroutine);
    }

    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
        {
            _monoBehaviourExt.StopCoroutine(_currentCoroutine);
        }
    }
    
    private IEnumerator StartAddCurrency()
    {
        var delay = new WaitForSeconds(_soGlobalSettings.AddCurrencyDelay);

        while (true)
        {
            yield return delay;
            
            _currency.AddCurrency(_soGlobalSettings.CurrencyAmountAdd);
        }
    }

    private IEnumerator StartSpendCurrency()
    {
        var delay = new WaitForSeconds(_soGlobalSettings.SpendCurrencyDelay);

        while (true)
        {
            yield return delay;
            
            _currency.SpendCurrency(_soGlobalSettings.CurrencyAmountSpend);
        }
    }
}
