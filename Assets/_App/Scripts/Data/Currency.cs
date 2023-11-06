using UnityEngine;

public class Currency
{
    private SOGlobalSettings _soGlobalSettings;
    private int _currentAmount;

    public Currency(SOGlobalSettings soGlobalSettings)
    {
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
        }
    }

    public void SpendCurrency(int amount)
    {
        if (!CheckCorrectedData(amount)) return;

        var newValue = _currentAmount - amount;

        if (newValue < 0f)
        {
            Debug.Log("Вас с воплями толкает продавец магазина: \nИди работай, бомж! У тебя закончились бабки!");

            return;
        }

        CurrentAmount = newValue;
    }


    private bool CheckCorrectedData(int amount)
    {
        if (amount > 0) return true;
        
        Debug.LogError("Incorrect value!");

        return false;
    }
}
