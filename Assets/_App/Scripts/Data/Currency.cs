using AxGrid.Model;
using UnityEngine;

public class Currency
{
    private const string MESSAGE = "Вас с воплями толкает продавец магазина: \nИди работай, бомж! У тебя недостаточ бабок!";
        
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
            _model.Set(NamesEvent.Defaulter, MESSAGE);

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
