using AxGrid.Model;
using UnityEngine;

public class Currency
{
    private readonly SOGlobalSettings _soGlobalSettings;
    private readonly DynamicModel _model;
    
    private int _currentAmount;

    public Currency(DynamicModel model, SOGlobalSettings soGlobalSettings)
    {
        _model = model;
        _soGlobalSettings = soGlobalSettings; 
        _currentAmount = _soGlobalSettings.DefaultCurrencyAmount;
    }

    public int CurrentAmount
    {
        get => _currentAmount;
        
        private set
        {
            _currentAmount = value;
            Debug.Log(_currentAmount);
        }
    }
    
    public void AddCurrency(int amount)
    {
        if (CheckCorrectedData(amount))
        {
            CurrentAmount += amount;
            _model.Set(NamesEvent.Currency, CurrentAmount);
        }
    }

    public void SpendCurrency(int amount)
    {
        if (!CheckCorrectedData(amount)) return;

        var newValue = _currentAmount - amount;

        if (newValue < 0f)
        {
            Debug.Log("Вас с воплями толкает продавец магазина: \n Иди работай, бомж! У тебя закончились бабки!");

            return;
        }

        CurrentAmount = newValue;
        _model.Set(NamesEvent.Currency, CurrentAmount);
    }


    private bool CheckCorrectedData(int amount)
    {
        if (amount > 0) return true;
        
        Debug.LogError("Incorrect value!");

        return false;
    }
}
