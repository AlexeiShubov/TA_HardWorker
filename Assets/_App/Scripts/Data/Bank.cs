using System.Collections;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public sealed class Bank
{
    private readonly SOGlobalSettings _soGlobalSettings;
    private readonly MonoBehaviourExt _monoBehaviourExt;
    private readonly DynamicModel _model;

    private Coroutine _currentCoroutine;
    
    public Currency Currency { get; }

    public Bank(MonoBehaviourExt monoBehaviourExt, DynamicModel model, SOGlobalSettings soGlobalSettings)
    {
        _monoBehaviourExt = monoBehaviourExt;
        _model = model;
        _soGlobalSettings = soGlobalSettings;
        
        Currency = new Currency(_model, _soGlobalSettings);
        
        Subscribe();
    }

    private void Subscribe()
    {
        _model.Set(NamesEvent.Currency, Currency.CurrentAmount);

        _model.EventManager.AddAction(NamesEvent.NeutralState, OnNeutralState);
        _model.EventManager.AddAction(NamesEvent.HomeState, OnHomeState);
        _model.EventManager.AddAction(NamesEvent.WorkState, OnWorkState);
        _model.EventManager.AddAction(NamesEvent.ShopState, OnShopState);
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
            
            Currency.AddCurrency(_soGlobalSettings.CurrencyAmountAdd);
        }
    }

    private IEnumerator StartSpendCurrency()
    {
        var delay = new WaitForSeconds(_soGlobalSettings.SpendCurrencyDelay);

        while (true)
        {
            yield return delay;
            
            Currency.SpendCurrency(_soGlobalSettings.CurrencyAmountSpend);
        }
    }
}
